using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Info : MonoBehaviour {

	//	[HideInInspector]
	//	float[] BasePathfindingNodeCost = new float[TankMan_WorldChanger.PathCostSize];//This Is The List That Stores Standard Values. These Can Changed, (scenairo) An Ice Creature Turns Info A Fire Creature, Then You Might Want Fire Nodes To Be Much More Desirable To Walk On. Then The Standard Is Also Changed. But If Its A Spell Effect Then Change The Array Above
	//	[HideInInspector]
	//	public int[,] MyNodes = new int[TankMan_WorldChanger.NodesSize, TankMan_WorldChanger.NodesSize];//Holds The Collision ID Of the NodeMAp

	public AStar MyAStar;

	[Tooltip("PathfindingNodeID is the cost to move to the different nodes //// 0 = normal nodes //// 1 = undestructable walls //// 2 = other units")]
	public float[] PathfindingNodeID = new float[TurretMan_WorldChanger.PathCostSize];//Currently Normalground 0 - Wall 1 - Other Creatures 2. TODO add More

	[HideInInspector]
	public Node[] MyNodePath = new Node[TurretMan_WorldChanger.NodesTotal / 10];//TODO Need To Do Something Else Here. Its Going To Take To Much Memory If All Enemies Do This.



	public void Start() {

		MyAStar.Setup();

	}




/*	Vector2 test = Vector2.zero;

	void OnDrawGizmos() {
		Debug.Log("ellos");

		for (int i = 0; i < TankMan_WorldChanger.NodesSize; i++) {

			for (int j = 0; j < TankMan_WorldChanger.NodesSize; j++) {
				test.x = (MyAStar.getnodemap()[i, j].PosX - (TankMan_WorldChanger.NodesSize / 2)) * TankMan_WorldChanger.DistanceBetweenNodes + (TankMan_WorldChanger.DistanceBetweenNodes / 2);
				test.y = (MyAStar.getnodemap()[i, j].PosY - (TankMan_WorldChanger.NodesSize / 2)) * TankMan_WorldChanger.DistanceBetweenNodes + (TankMan_WorldChanger.DistanceBetweenNodes / 2);

				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere(test, TankMan_WorldChanger.DistanceBetweenNodes / 4);


			}
		}
	}*/




	/*	public void AddOrRemoveNodeCost(int index, float cost) {//Add/Remove Cost

			PathfindingNodeID[index] += cost;

		}

		public void SetNewBaseNodeCost(float[] cost) {//TODO Apply CurrentNodeCost Increase Over? (Spell Active)

			for (int i = 0; i < TankMan_WorldChanger.PathCostSize; i++) {

				BasePathfindingNodeCost[i] = cost[i];

			}

		}

		public void UpdatePathCost() {//TODO If PathfindingNodeID Is Affected By A Spell Then BasePathfinding Needs To Apply That To?

			for (int i = 0; i < TankMan_WorldChanger.PathCostSize; i++) {

				PathfindingNodeID[i] = BasePathfindingNodeCost[i];

			}

		}*/

}
