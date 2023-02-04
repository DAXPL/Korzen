using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 input;
    [SerializeField] private float moveTime = 1f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float deadzone = 0.5f;
    private bool inMove;
    [SerializeField] private bool canMove = true;

    private Vector3 checkBox = new Vector3(0.25f, 1, 0.25f);
    private Vector2 lastDir = new Vector2(0, 0);

    void LateUpdate()
    {
        if(canMove && input.magnitude > 0 && inMove == false)
        {
            StartCoroutine(MovePlayer(input));
        }
    }
    IEnumerator MovePlayer(Vector2 dest)
    {
        //while (inMove) { yield return 0; }
        inMove = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position;
        endPos.x += dest.x;
        endPos.z += dest.y;
        lastDir = dest.normalized;
        float timePassed = 0;

        while (timePassed<=moveTime && moveTime != 0)
        {
            transform.position = Vector3.Lerp(startPos, endPos, timePassed/moveTime);
            yield return 0;
            timePassed += Time.deltaTime;
        }
        transform.position = endPos;

        CheckTiles();

        //dirty fix
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),transform.position.y, Mathf.RoundToInt(transform.position.z));
        
        yield return new WaitForSeconds(waitTime);

        inMove = false;
    }
    private void CheckTiles()
    {
        Collider[] cols = Physics.OverlapBox(transform.position, checkBox);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].TryGetComponent(out ITile tile))
            {
                tile.OnTileEnter(this);
            }
        }
    }
    
    public void ForcePlayerToMove(Vector2 vec)
    {
        StartCoroutine(ForceMovement(lastDir));
    }
    IEnumerator ForceMovement(Vector2 vec)
    {
        canMove = false;
        while (inMove) { yield return null; }
        input = Vector2.zero;
        yield return MovePlayer(lastDir);
        yield return null;
        canMove = true;
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
        ToogleMovement(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, checkBox);
    }
}