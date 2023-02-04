using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaDeathVifon : MonoBehaviour, ITile
{
    [SerializeField] private GameEvent ev;
    public void OnTileEnter(PlayerMovement pm)
    {
        Debug.Log("INSTA DEATH");
        if(ev != null)
        {
            ev.Raise();
        }
        //pm.ForcePlayerToMove(new Vector2(0,10));
    }
}