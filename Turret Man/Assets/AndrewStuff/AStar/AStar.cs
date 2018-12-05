using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {

	//float[] PathfindingNodeID = new float[TankMan_WorldChanger.PathCostSize];//Currently Normalground 0 - Wall 1 - Other Creatures 2. TODO add More And Remember To Update
	Node[] _OpenList = new Node[TurretMan_WorldChanger.NodesTotal];//list that holds nodes that i havent searched through
	Node[] _ClosedList = new Node[TurretMan_WorldChanger.NodesTotal];//list that have been searched through

/*	public Node[] ol() {//Delete this if done checking
		return _OpenList;
	}
	public Node[] cl() {//Delete this if done checking
		return _ClosedList;
	}*/

	Node[,] _NodeMap = new Node[TurretMan_WorldChanger.NodesSize, TurretMan_WorldChanger.NodesSize];

	public Node[,] getnodemap() {
		return _NodeMap;
	}


	Node _TheStartNode; //Will hold the refrence to the startnode 
	Node _EndNode; //Will hold the refrence to the endnode


	int _NodeIndexSaved = 0; //used to store the index of a node
	int _OpenListAtIndex = 0; //this holds the next position which to add the next element
	int _ClosedListAtIndex = 0; //closed list index holder

	Node _NodeHolder = null; //Holds The Neighbour Node Of CurrentNode
	Node _CurrentNode; //Holds The Node The A* Is Searching With, In The Current Loop Iteration 
	float _LowerstFScore = 100000; //This is just a value to get the "best" path to the target, if there is an enormous amount of nodes then this value must be increased

	int ArrayLengthSaver = 0;

	int _NodeXPos = 0;
	int _NodeYPos = 0;

	
	public void Setup() {

		//	_WalkCost.SetNodeSize (sceneStartup);//Creating Node Cost Array
		int Added = 0;

		for (int x = 0; x < TurretMan_WorldChanger.NodesSize; x++) {//Creating Nodes Nodes
			for (int y = 0; y < TurretMan_WorldChanger.NodesSize; y++) {
				if ((x == 0 && y == 0) || (x == 0 && y == TurretMan_WorldChanger.NodesSize - 1) || (x == TurretMan_WorldChanger.NodesSize - 1 && y == 0) || (x == TurretMan_WorldChanger.NodesSize - 1 && y == TurretMan_WorldChanger.NodesSize - 1)) {//Corners
					_NodeMap[x, y] = new Node(x, y, 0);
				} else if (x == 0 || x == TurretMan_WorldChanger.NodesSize - 1 || y == 0 || y == TurretMan_WorldChanger.NodesSize - 1) {//Sides
					_NodeMap[x, y] = new Node(x, y, 1);
				} else {//Middles
					_NodeMap[x, y] = new Node(x, y, 2);
				}
			}
		}


		for (int x = 1; x < TurretMan_WorldChanger.NodesSize - 1; x++) {//Giving Middle Nodes Neighbour Nodes
			for (int y = 1; y < TurretMan_WorldChanger.NodesSize - 1; y++) {
				Added = 0;
				for (int k = -1; k < 2; k++) {
					for (int h = -1; h < 2; h++) {
						if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
							_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
						}
					}
				}
			}
		}


		for (int x = 0; x < TurretMan_WorldChanger.NodesSize; x += (TurretMan_WorldChanger.NodesSize - 1)) {//Giving Corner Nodes Neighbour Nodes
			for (int y = 0; y < TurretMan_WorldChanger.NodesSize; y += (TurretMan_WorldChanger.NodesSize - 1)) {

				Added = 0;
				if (x == 0) {
					if (y == 0) {//Down Left
						for (int k = 0; k < 2; k++) {
							for (int h = 0; h < 2; h++) {
								if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
									_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
								}
							}
						}
					} else {//Up Left
						for (int k = 0; k < 2; k++) {
							for (int h = -1; h < 1; h++) {
								if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
									_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
								}
							}
						}
					}
				} else {//Down Right
					if (y == 0) {
						for (int k = -1; k < 1; k++) {
							for (int h = 0; h < 2; h++) {
								if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
									_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
								}
							}
						}
					} else {//Up Right
						for (int k = -1; k < 1; k++) {
							for (int h = -1; h < 1; h++) {
								if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
									_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
								}
							}
						}
					}
				}
			}
		}

		for (int x = 1; x < TurretMan_WorldChanger.NodesSize - 1; x++) {//Giving X-Side Nodes Neighbour Nodes
			for (int y = 0; y < TurretMan_WorldChanger.NodesSize; y += (TurretMan_WorldChanger.NodesSize - 1)) {

				Added = 0;
				if (y == 0) {

					for (int k = -1; k < 2; k++) {
						for (int h = 0; h < 2; h++) {
							if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
								_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
							}
						}
					}

				} else {

					for (int k = -1; k < 2; k++) {
						for (int h = -1; h < 1; h++) {
							if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
								_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
							}
						}
					}

				}
			}
		}

		for (int x = 0; x < TurretMan_WorldChanger.NodesSize; x += (TurretMan_WorldChanger.NodesSize - 1)) {//Creating Y-SideNodes
			for (int y = 1; y < TurretMan_WorldChanger.NodesSize - 1; y++) {

				Added = 0;
				if (x == 0) {

					for (int k = 0; k < 2; k++) {
						for (int h = -1; h < 2; h++) {
							if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
								_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
							}
						}
					}

				} else {

					for (int k = -1; k < 1; k++) {
						for (int h = -1; h < 2; h++) {
							if (_NodeMap[x, y] != _NodeMap[x + k, y + h]) {
								_NodeMap[x, y].NeighbourNodess[Added++] = _NodeMap[x + k, y + h];
							}
						}
					}

				}
			}
		}

		_TheStartNode = _NodeMap[((TurretMan_WorldChanger.NodesSize - 1) / 2), ((TurretMan_WorldChanger.NodesSize - 1) / 2)];
		_OpenListAtIndex = 0;
		_ClosedListAtIndex = 0;

	}

	//float one, two;

	public void StartRunning(Object_Node_Position me, Node_Info meNodeInfo, Object_Node_Position taget) {

		if (_TheStartNode == null) {
			Setup();
		}

		#region Startup Phase

		for (int i = 0; i < _OpenListAtIndex; i++) {//The Used Nodes Is In These Two Lists. So I Only Need To Reset The Nodes Used
			_OpenList[i].NodeSearchedThrough = false;
		}

		for (int i = 0; i < _ClosedListAtIndex; i++) {
			_ClosedList[i].NodeSearchedThrough = false;
		}

		_OpenListAtIndex = 0;
		_ClosedListAtIndex = 0;

		_NodeXPos = Mathf.FloorToInt(_TheStartNode.PosX + (taget.XNode - me.XNode));
		_NodeYPos = Mathf.FloorToInt(_TheStartNode.PosY + (taget.YNode - me.YNode));

		if (_NodeXPos >= TurretMan_WorldChanger.NodesSize) {
			_NodeXPos = TurretMan_WorldChanger.NodesSize - 1;
		} else if (_NodeXPos < 0) {
			_NodeXPos = 0;
		}
		if (_NodeYPos >= TurretMan_WorldChanger.NodesSize) {
			_NodeYPos = TurretMan_WorldChanger.NodesSize - 1;
		} else if (_NodeYPos < 0) {
			_NodeYPos = 0;
		}

		_EndNode = _NodeMap[_NodeXPos, _NodeYPos];
		_TheStartNode.SetStartNode(_TheStartNode, _EndNode);//Giving The StartNode its Costs

		_OpenList[_OpenListAtIndex++] = _TheStartNode;//Giving The Search Through List Its First Node

		#endregion

		#region A* Algorythm

		while (_ClosedListAtIndex < TurretMan_WorldChanger.NodesTotal) {//If The ClosedListAtIndex Is Equalt To Or Greater The Total Amount Of Nodes Then This Is False And The Search Is Stopped
			_LowerstFScore = 10000000;

			for (int i = 0; i < _OpenListAtIndex; i++) {//Iterating Through The List With Unused Nodes To Find The Node With The Lowerst FCost

				if (_OpenList[i].FCost < _LowerstFScore) {
					_CurrentNode = _OpenList[i];
					_NodeIndexSaved = i;
					_LowerstFScore = _CurrentNode.FCost;
				}
			}

			_ClosedList[_ClosedListAtIndex++] = _CurrentNode;//The Node That Was Chosen From Openlist Is Put In Here
			_OpenList[_NodeIndexSaved] = _OpenList[--_OpenListAtIndex];//The Node That Was Added To ClosedList Is Being Removed Here And Replaced With The Last Node In The Openlist


			if (_CurrentNode == _EndNode) {//If True Then A Path Was Found And The Search Is Complete
				RemakePath(meNodeInfo);
				return;
			}

			ArrayLengthSaver = _CurrentNode.NeighbourNodess.Length;//This Is An Improvement Rather Then Getting The Length Each i++ (Not Much But Some)

			for (int i = 0; i < ArrayLengthSaver; i++) {

				_NodeHolder = _CurrentNode.NeighbourNodess[i];

				if (_NodeHolder.NodeSearchedThrough == false) {//If false Then The Node Havent Been Searched Through And Info Need To Be Set
					_NodeHolder.SetParentAndHCost(_CurrentNode, _EndNode, meNodeInfo.PathfindingNodeID);
					_OpenList[_OpenListAtIndex++] = _NodeHolder;
				} else if (_CurrentNode.GCost < _NodeHolder._ParentNode.GCost) {//If Current.Gcost Is Less Then Nodeholder.parent.Gcost Then A New ParentNode Is Set  ...... If Errors Occur Use (_NodeHolder.GCost > _CurrentNode.GCost + (PathfindingNodeID[CollisionID[_NodeHolder.PosX, _NodeHolder.PosY]] * 1.4f))
					_NodeHolder.SetNewParent(_CurrentNode, meNodeInfo.PathfindingNodeID);
				}
			} 
		}

		#endregion

		Debug.LogWarning("No Path Detected, Initiating Self Destruct Algorythms.... 3..... 2..... 1..... .");

	}

	void RemakePath(Node_Info meNodeInfo) {//Backtracking From The EndNode. When The Backtracking Reaches The StartNode, Then The Path Is Set

		ArrayLengthSaver = 0;//Just A Reused Variable For Holding The Index Number For The Next Node To Enter In The Path

		_CurrentNode = _EndNode;
		meNodeInfo.MyNodePath[ArrayLengthSaver++] = _CurrentNode;


		if (_EndNode == _TheStartNode) {
			return;
		} else {
			_CurrentNode = _EndNode;
		}

		while (true) {//Going Backwards Parent To Parent To Parent.....
			if (_CurrentNode._ParentNode != _TheStartNode) {
				meNodeInfo.MyNodePath[ArrayLengthSaver++] = _CurrentNode._ParentNode;
				_CurrentNode = _CurrentNode._ParentNode;
			} else {
				meNodeInfo.MyNodePath[ArrayLengthSaver++] = _CurrentNode._ParentNode;
				meNodeInfo.MyNodePath[ArrayLengthSaver--] = null;//Setting This To Be 'null' To Symbolize That This Is The End

				for (int i = 0; i < ArrayLengthSaver / 2; i++) {//Turning The List Around
					_CurrentNode = meNodeInfo.MyNodePath[i];
					meNodeInfo.MyNodePath[i] = meNodeInfo.MyNodePath[ArrayLengthSaver - i];
					meNodeInfo.MyNodePath[ArrayLengthSaver - i] = _CurrentNode;
				}

				return;
			}
		}

	}

}