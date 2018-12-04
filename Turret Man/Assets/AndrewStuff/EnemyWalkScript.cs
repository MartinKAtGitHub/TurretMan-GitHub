using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkScript : MonoBehaviour {

	Transform Target;
	public float speed = 0.05f;

	void Start() {
		Target = GameObject.Find("Base").transform;
	}


	// Update is called once per frame
	void Update () {
		transform.position += (Target.position - transform.position).normalized * speed; 	
	}
}
