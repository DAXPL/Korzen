using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 input;
    [SerializeField] private float moveTime = 1f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float deadzone = 0.5f;
    private bool inMove;
    private bool live = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private LayerMask obstaceMask;

    private Vector3 checkBox = new Vector3(0.25f, 1, 0.25f);
    private Vector2 lastDir = new Vector2(0, 0);

    [Header("AUDIO")]
    [SerializeField] private AudioClip iceGlide;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip[] steps;
    [SerializeField] private float stepGap;
    private AudioSource ass;
    [SerializeField] private Animator anims;
    private void Start()
    {
        ass = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        if(live && canMove && input.magnitude > 0 && inMove == false)
        {
            StartCoroutine(MovePlayer(input, waitTime));
        }
    }
    IEnumerator MovePlayer(Vector2 dest, float wt)
    {
        inMove = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position;
        endPos.x += dest.x;
        endPos.z += dest.y;
        lastDir = dest.normalized;
        anims.SetBool("walk", true);

        if (CanMoveHere(endPos))
        {
            CheckTiles(false);
            float timePassed = 0;
            float nextStep = Time.time;

            while (timePassed <= moveTime && moveTime != 0)
            {
                transform.position = Vector3.Lerp(startPos, endPos, timePassed / moveTime);

                if(Time.time >= nextStep)
                {
                    nextStep += stepGap;
                    ass.PlayOneShot(Step());
                }

                yield return 0;
                timePassed += Time.deltaTime;
            }
            CheckTiles(true);

            //dirty fix
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),transform.position.y, Mathf.RoundToInt(transform.position.z));
            anims.SetBool("walk", false);
            yield return new WaitForSeconds(wt);
        }
        

        inMove = false;
    }
    private void CheckTiles(bool didEnter)
    {
        Collider[] cols = Physics.OverlapBox(transform.position, checkBox);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].TryGetComponent(out ITile tile))
            {
                if(didEnter) tile.OnTileEnter(this);
                else tile.OnTileExit(this);
            }
        }
    }
    private bool CanMoveHere(Vector3 pos)
    {
        pos.y += 1.6f;
        return Physics.OverlapBox(pos, checkBox, Quaternion.identity,obstaceMask).Length<=0;
    }
    public void IceSlide()
    {
        StartCoroutine(ForceMovement());
    }
    IEnumerator ForceMovement()
    {
        canMove = false;
        while (inMove) { yield return null; }
        input = Vector2.zero;
        ass.PlayOneShot(iceGlide,0.8f);
        yield return MovePlayer(lastDir,0);
        yield return null;
        canMove = true;
    }

    private AudioClip Step()
    {
        return steps[Random.Range(0,steps.Length)];
    }
    public void MoveInput(InputAction.CallbackContext context)
    {
        if (!canMove) return;
        input = context.ReadValue<Vector2>();

        bool xDomin = (Mathf.Abs(input.x) >= Mathf.Abs(input.y));

        if (input.x > deadzone) input.x = 1;
        else if (input.x < -deadzone) input.x = -1;
        else input.x = 0;

        if (input.y > deadzone) input.y = 1;
        else if (input.y < -deadzone) input.y = -1;
        else input.y = 0;

        if (xDomin)
        {
            input.y = 0;
        }
        else
        {
            input.x = 0;
        }
    }

    public void ToogleMovement(bool newState)
    {
        canMove = newState;
    }
    public void KillPlayer()
    {
        live = false;
        canMove = false;
        input = Vector2.zero;
        ass.PlayOneShot(death);
        CheckTiles(false);
    }

    public void SpierdolSie()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}