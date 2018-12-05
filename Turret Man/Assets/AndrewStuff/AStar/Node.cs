using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Just Some Variables And Methods That Is Needed To Calculate A*
public class Node {

	public bool NodeSearchedThrough = false;//if bool is used to check if the node have been searched through

	public float[] PathfindingNodeID;//this array holds the move cost to each tile (wall , water, sand .....)
	public Node[] NeighbourNodess;//neighbours for the nodes

	public int PosX = 0;
	public int PosY = 0;

	public int MapCollision = 0;//this is used to set the correct id for the tile (wall, water, sand .....)
	public float NodePathValue = 0;

	int _XValue = 0, _YValue = 0; //just some values that im using

	public Node _ParentNode = null;
	public float GCost = 0;//GCost is the cost that have been used to get to this node
	public float FCost = 0;//Gcost+Hcost
	public int _HCost = 0;//HCost is how far away the end node is from this node


	public Node(int posX, int posY, int placement) {

		PosX = posX;
		PosY = posY;

		if (placement == 0) {//Corner
			NeighbourNodess = new Node[3];
		} else if (placement == 1) {//Side
			NeighbourNodess = new Node[5];
		} else {//Middle
			NeighbourNodess = new Node[8];
		}

	}

	public void SetStartNode(Node theParent, Node theEnd) {//setting parent gcost and hcost
		NodeSearchedThrough = true;
		_ParentNode = theParent;

		_XValue = theEnd.PosX - PosX;
		_YValue = theEnd.PosY - PosY;

		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;
		GCost = 0;
		FCost = _HCost;
	}

	public void SetParentAndHCost(Node theParent, Node theEnd, float[] pathnodeid) {//setting parent gcost and hcost
		NodeSearchedThrough = true;
		_ParentNode = theParent;

		_XValue = theEnd.PosX - PosX;
		_YValue = theEnd.PosY - PosY;


		if (_XValue < 0)
			_XValue *= -1;
		if (_YValue < 0)
			_YValue *= -1;

		_HCost = _XValue + _YValue;

		

		if (theParent.PosX - PosX + theParent.PosY - PosY == 0 || theParent.PosX - PosX + theParent.PosY - PosY == 2 || theParent.PosX - PosX + theParent.PosY - PosY == -2) {
			GCost = (pathnodeid[MapCollision] * 1.4f) + _ParentNode.GCost;
		} else {
			GCost = pathnodeid[MapCollision] + _ParentNode.GCost;
		}
		FCost = _HCost + GCost;
	}

	public void SetNewParent(Node theParent, float[] pathnodeid) {//Adding the parent GCost to this nodes gcost and adding the distance the parent had to travel to this node gcost

		_ParentNode = theParent;
		if (theParent.PosX - PosX + theParent.PosY - PosY == 0 || theParent.PosX - PosX + theParent.PosY - PosY == 2 || theParent.PosX - PosX + theParent.PosY - PosY == -2) {
			GCost = (pathnodeid[MapCollision] * 1.4f) + _ParentNode.GCost;
		} else {
			GCost = pathnodeid[MapCollision] + _ParentNode.GCost;
		}

		FCost = _HCost + GCost;
	}

}