using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GagDeposit : MonoBehaviour
{
    public string RocketShipTag;
   [HideInInspector] public int GagWinConditionLimit;
    public Animator NotEnoughMinerals_Animator;

    private bool CanDepositGag;
    private bool GameWon;

    void Start()
    {
        GagWinConditionLimit = GameManager.Instance.GagWinCondition;
    }

    // Update is called once per frame
    void Update()
    {
       /* if(Input.GetKeyDown(KeyCode.Space) && CanDepositGag && !GameWon)
        {
            IsWinConditionMet();
        }*/
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
            NotEnoughMinerals_Animator.SetTrigger("NotEnoughMinerals");
           //Debug.Log("Win Condition not Met !!!!!!!!!!MAKE UI");
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

        if (Input.GetKeyDown(KeyCode.Space) && !GameWon && collision.tag == RocketShipTag)
        {
            IsWinConditionMet();
        }
    }
}

  

