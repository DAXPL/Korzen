using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    void OnTileEnter(PlayerMovement pm);
    void OnTileExit(PlayerMovement pm);
}
