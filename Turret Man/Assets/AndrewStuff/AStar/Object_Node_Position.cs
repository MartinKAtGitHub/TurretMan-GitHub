using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Node_Position : MonoBehaviour {

	//Center Node Is The Transform.Position Turned Into NodePosition.
	//	[HideInInspector]
	public int XNode = 0;
	//	[HideInInspector]
	public int YNode = 0;

	public void Update() {
		CalculateNodePos(transform.position);
	}


	public void CalculateNodePos(Vector3 pos) {

		XNode = Mathf.FloorToInt(pos.x / TurretMan_WorldChanger.DistanceBetweenNodes) + TurretMan_WorldChanger.NodesSizeHalf;
		YNode = Mathf.FloorToInt(pos.y / TurretMan_WorldChanger.DistanceBetweenNodes) + TurretMan_WorldChanger.NodesSizeHalf;

		if (XNode < 0) {
			XNode = 0;
		} else if (XNode >= TurretMan_WorldChanger.NodesSize) {
			XNode = TurretMan_WorldChanger.NodesSize - 1;
		}

		if (YNode < 0) {
			YNode = 0;
		} else if (YNode >= TurretMan_WorldChanger.NodesSize) {
			YNode = TurretMan_WorldChanger.NodesSize - 1;
		}

	}
}