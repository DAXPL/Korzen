using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    private Transform startPos;
    [SerializeField] private Transform player;
    private PlayerMovement pm;

    private void Awake()
    {
        startPos = GameObject.FindGameObjectWithTag("StartPos").transform;
        pm = player.GetComponent<PlayerMovement>();
    }
    public void SetPlayer()
    {
        if(startPos == null) startPos = GameObject.FindGameObjectWithTag("StartPos").transform;

        if (startPos != null)
        {
            player.transform.position = startPos.position;
        }
        else
        {
            player.transform.position = new Vector3(0,1,0);
        }
        
        pm.ToogleMovement(true);
        gameOverUI.SetActive(false);
    }
    public void EndPlayer()
    {
        pm.KillPlayer();
        gameOverUI.SetActive(true);
    }
}
