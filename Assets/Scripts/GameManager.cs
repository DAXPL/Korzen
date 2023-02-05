using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OnResetGame()
    {
        cvc.Follow=this.transform;
    }

    public CinemachineVirtualCamera GetCVC()
    {
        return cvc;
    }

    public void FinishGame()
    {
        StartCoroutine(FinishGameCorountine());
    }
    IEnumerator FinishGameCorountine()
    {
        //efekty
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(2);
    }
}
