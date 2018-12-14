using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour {

  //  public delegate void DeathDelegate();
  //  public event DeathDelegate DeathEvent;

    public int MaxHP;
    public int CurrentHP;

    private bool isEnemyDead;
    // Use this for initialization
    void Start ()
    {
        CurrentHP = MaxHP;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void TakeDmg(int dmg)
    {
        CurrentHP -= dmg;
        if(CurrentHP <= 0)
        {
            EnemyDeath();
        }
    }

    public bool IsEnemyDead()
    {
        return isEnemyDead;
    }

    private void EnemyDeath()
    {
        //if(DeathEvent != null)
        //{
        //    DeathEvent();
        //}

        //play death anim
        isEnemyDead = true;
        Debug.Log("Enemy dead");
        Destroy(gameObject);
    }

}
