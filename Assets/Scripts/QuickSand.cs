using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class QuickSand : MonoBehaviour, ITile
{
    [SerializeField] private float time = 3f;
    private float timer;
    PlayerMovement lpm;
    [SerializeField] private GameEvent deathEvent;
    AudioSource ass;
    [SerializeField] private AudioClip quicksandSound;

    void Start()
    {
        ass = GetComponent<AudioSource>();
    }
    public void OnTileEnter(PlayerMovement pm)
    {
        timer = 0;
        lpm = pm;
        ass.PlayOneShot(quicksandSound);
    }

    public void OnTileExit(PlayerMovement pm)
    {
        lpm = null;
        ass.Stop();
    }

    void Update()
    {
        if (lpm == null) return;
        timer += Time.deltaTime;
        if (timer > time)
        {
            Debug.Log("Wziol i zdech");
            deathEvent.Raise();
            timer =0;
            lpm = null;
        }
    }
}
