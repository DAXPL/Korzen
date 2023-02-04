using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTile : MonoBehaviour, ITile
{
    public void OnTileEnter(PlayerMovement pm)
    {
        GameManager.Instance.GetCVC().Follow = transform;
    }
}
