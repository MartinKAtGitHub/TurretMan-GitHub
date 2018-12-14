using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GagDeposit : MonoBehaviour
{
    public string RocketShipTag;
    public int GagWinConditionLimit;

    private bool CanDepositGag;
    private bool GameWon;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && CanDepositGag && !GameWon)
        {
            IsWinConditionMet();
        }
    }


    private void IsWinConditionMet()
    {
        if(GameManager.Instance.PlayerResources.CurrentResources >= GagWinConditionLimit)
        {

            GameManager.Instance.GameWin();
            Debug.Log("GAME WON START WIN EVENT");
        }
        else
        {
            Debug.Log("Win Condition not Met");
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == RocketShipTag)
        {
            Debug.Log("SHIP IN RANGE");
            CanDepositGag = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CanDepositGag = false;   
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // input in here lul `????? NO TIME!!
    }

    private void OnPlayerWinDepositAnimEvent()
    {

    }
}
