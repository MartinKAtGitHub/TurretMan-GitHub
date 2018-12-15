using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{

    public GameObject WinPnl;
    public Text GagsNeededToWin_txt;

    void Start()
    {
        GameManager.Instance.PlayerWinEvent += VictoryEvent;
        WinPnl.SetActive(false);

        GagsNeededToWin_txt.text = GameManager.Instance.GagWinCondition.ToString();
    }


    private void VictoryEvent()
    {
        Time.timeScale = 0;
        WinPnl.SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.Instance.PlayerWinEvent -= VictoryEvent;
        Time.timeScale = 1;
    }


    public void ReStartLevel()
    {
        GameManager.Instance.ReStartLevel();
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void GoToMainMenu()
    {
      GameManager.Instance.GoToMainMenu();
    }
}
