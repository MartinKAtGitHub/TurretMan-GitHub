using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour {

	public float Health = 100;


	void OnCollisionEnter(Collision collision) {
		Debug.Log(collision.gameObject.name);
	}


}
