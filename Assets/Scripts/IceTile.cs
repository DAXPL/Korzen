using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTile : MonoBehaviour, ITile
{
    public void OnTileEnter(PlayerMovement pm)
    {
        pm.IceSlide();
    }

    public void OnTileExit(PlayerMovement pm)
    {
    }
}
