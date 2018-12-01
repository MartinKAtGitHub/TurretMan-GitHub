using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Node_Position : MonoBehaviour {

	//Center Node Is The Transform.Position Turned Into NodePosition.
	//	[HideInInspector]
	public int XNode = 0;
	//	[HideInInspector]
	public int YNode = 0;

	float SaveVariable = 0;

	public void Update() {
		CalculateNodePos(transform.position);
	}


	public void CalculateNodePos(Vector3 pos) {

		XNode = Mathf.FloorToInt(pos.x / TankMan_WorldChanger.DistanceBetweenNodes) + TankMan_WorldChanger.NodesSizeHalf;
		YNode = Mathf.FloorToInt(pos.y / TankMan_WorldChanger.DistanceBetweenNodes) + TankMan_WorldChanger.NodesSizeHalf;

		if (XNode < 0) {
			XNode = 0;
		} else if (XNode >= TankMan_WorldChanger.NodesSize) {
			XNode = TankMan_WorldChanger.NodesSize - 1;
		}

		if (YNode < 0) {
			YNode = 0;
		} else if (YNode >= TankMan_WorldChanger.NodesSize) {
			YNode = TankMan_WorldChanger.NodesSize - 1;
		}

	}
}