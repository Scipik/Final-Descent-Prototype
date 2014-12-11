// Action Link System
// Queue system for unit actions

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// A node containing information on a unit at the current link position (Actions, movement to, etc)
public class ActionNode {
	public int actionNum; // The action this unit is performing for this node (May need to replace later)
	public int apCost;
	public Vector3 position; // Position when  (for movement and some attacks)
	public GameObject target; // The target of action is applicable
	
	// Constructor
	public ActionNode(int aN, int apC, Vector3 pos, GameObject t) {
		this.actionNum = aN;
		this.target = t;
		this.position = pos;
		this.apCost = apC;
	}
}

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
	public int setAction(int unitNum, int actionNum, int apCost, Vector3 position, GameObject target) {
		if (unitNum == -1) {
			List<ActionNode> newUnitActions = new List<ActionNode>();
			unitNum = actionSequence.Count;
			actionSequence.Add(newUnitActions);
		}
		createActionSetChain(unitNum, actionNum, apCost, target);
		print (actionSequence[unitNum].Count);
		return unitNum;
	}
	
	
	// Sets initial actionNode, then fills in the number of action with empty nodes as needed
	private void createActionSetChain(int unitNum, int actionNum, int apCost, Vector3 position, GameObject target) {
		ActionNode newAction = new ActionNode(actionNum, apCost, target);
		actionSequence[unitNum].Add (newAction);
		
		for (int i = 0; i < apCost - 1; i++) {
			ActionNode blankAction = new ActionNode(-actionNum, 0, target);
			actionSequence[unitNum].Add (blankAction);
		}
	}
	
	
	// Cancels last command action and returns the Action node
	public ActionNode cancelLastAction(int unitNum) {
		ActionNode temp = null;
		if (unitNum != -1) {
			while(true) {
				temp = actionSequence[unitNum][actionSequence[unitNum].Count - 1];
				if (temp.actionNum < 0) {
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