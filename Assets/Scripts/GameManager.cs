using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private CinemachineVirtualCamera cvc;
    [SerializeField] private GameEvent startGame;

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        startGame.Raise();
    }

    public CinemachineVirtualCamera GetCVC()
    {
        return cvc;
    }
}
