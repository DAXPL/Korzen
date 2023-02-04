using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernFlower : MonoBehaviour, ITile
{
    [SerializeField] private GameEvent endGame;
    public void OnTileEnter(PlayerMovement pm)
    {
        pm.ToogleMovement(false);
        endGame.Raise();
    }

    public void OnTileExit(PlayerMovement pm)
    {
    }
}
