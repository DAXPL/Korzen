using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseTile : MonoBehaviour, ITile
{
    [SerializeField] private UnityEvent job;
    public void OnTileEnter(PlayerMovement pm)
    {
        job.Invoke();
    }

    public void OnTileExit(PlayerMovement pm)
    {

    }

}
