using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTile : MonoBehaviour, ITile
{
    [SerializeField] private Transform place;
    public void OnTileEnter(PlayerMovement pm)
    {
        if (place == null) place = transform;
        GameManager.Instance.GetCVC().Follow = place;
    }

    public void OnTileExit(PlayerMovement pm)
    {

    }
}
