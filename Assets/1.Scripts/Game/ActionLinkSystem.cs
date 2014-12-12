// Action Link System
// Queue system for unit actions

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ActionLinkSystem : MonoBehaviour {

	private List<List<ActionNode>> actionSequence = new List<List<ActionNode>>();
	
	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}
	
	
	// Called by a unit to set their action node
	// Creates a unitNum for them if -1 and returns it
	// else if just return it normally
	public int setAction(int unitNum, int apCost, Tile initialPosition, Tile targetPosition, GameObject ghost) {
		if (unitNum == -1) {
			List<ActionNode> newUnitActions = new List<ActionNode>();
			unitNum = actionSequence.Count;
			actionSequence.Add(newUnitActions);
		}
		
		// Sets initial actionNode, then fills in the number of action with empty nodes as needed
		MovementNode newAction = new MovementNode(true, apCost, initialPosition, targetPosition, ghost);
		actionSequence[unitNum].Add (newAction);
		for (int i = 0; i < apCost - 1; i++) {
			MovementNode blankAction = new MovementNode(false, 0, initialPosition, targetPosition, ghost);
			actionSequence[unitNum].Add (blankAction);
		}
		
		
		return unitNum;
	}
	
	
	// Cancels last command action and returns the Action node
	public ActionNode cancelLastAction(int unitNum) {
		ActionNode temp = null;
		if (unitNum != -1 && actionSequence[unitNum].Count > 0) {
			while(true) {
				temp = actionSequence[unitNum][actionSequence[unitNum].Count - 1];
				if (!temp.actionStart) {
					actionSequence[unitNum].RemoveAt(actionSequence[unitNum].Count - 1);
					continue;
				} else {
					actionSequence[unitNum].RemoveAt(actionSequence[unitNum].Count - 1);
					break;
				}
			}
		}
		return temp;
	}
	
	public void excuteActions() {
	
	}
	
	public void clearActions() {
	
	}
}