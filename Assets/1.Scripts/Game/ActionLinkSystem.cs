// Action Link System
// Queue system for unit actions

using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ActionLinkSystem : MonoBehaviour {

	public List<Units> unitsSetToMove = new List<Units>();
	private List<List<ActionNode>> actionSequence = new List<List<ActionNode>>();
	private PlayerController controller;
	
	// Use this for initialization
	void Start() {
		controller = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update() {
	
	}
	
	
	// Called by a unit to set their action node
	// Creates a unitNum for them if -1 and returns it
	// else if just return it normally
	public int setAction(Units unit, int unitNum, int apCost, Tile initialPosition, Tile targetPosition, GameObject ghost) {
		if (unitNum == -1) {
			List<ActionNode> newUnitActions = new List<ActionNode>();
			unitNum = actionSequence.Count;
			actionSequence.Add(newUnitActions);
			unitsSetToMove.Add (unit);
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
	
	public int setAction(Units unit, int unitNum, int apCost, Tile target) {
		if (unitNum == -1) {
			List<ActionNode> newUnitActions = new List<ActionNode>();
			unitNum = actionSequence.Count;
			actionSequence.Add(newUnitActions);
			unitsSetToMove.Add (unit);
		}
		
		// Sets initial actionNode, then fills in the number of action with empty nodes as needed
		AttackNode newAction = new AttackNode(true, apCost, target);
		actionSequence[unitNum].Add (newAction);
		for (int i = 0; i < apCost - 1; i++) {
			AttackNode blankAction = new AttackNode(false, 0, target);
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
	
	
	// Executes actions in the list using coroutines
	// Each element of action should be .25s
	public IEnumerator excuteActions() {
		for (int i = 0; i < unitsSetToMove.Count; i++) {
			for (int j = 0; j < actionSequence[i].Count; j++) {
				if (typeof(MovementNode) == actionSequence[i][j].GetType()) {
					Destroy (((MovementNode)actionSequence[i][j]).ghost);
				}
			}
			StartCoroutine(UnitAction(i));
		}
		yield return new WaitForSeconds(3.0f); // For prototype, max actions per turn should be 10
		controller.switchTurn();
		clearActions ();
	}
	
	private IEnumerator UnitAction(int unit) {
		ActionNode temp;
		for (int i = 0; i < actionSequence[unit].Count; i++) {
			temp = actionSequence[unit][i];
			if (temp.actionStart) { // Skip over blank actions that are used for spacing
				unitsSetToMove[unit].doAction (actionSequence[unit][i]);
			}
			yield return new WaitForSeconds(0.25f);
		}
	}
	
	
	public void clearActions() {
		for (int i = 0; i < unitsSetToMove.Count; i++) {
			actionSequence[i].Clear ();
		}
		actionSequence.Clear ();
		
		for (int i = 0; i < unitsSetToMove.Count; i++) {
			unitsSetToMove[i].unitNum = -1;
		}
		unitsSetToMove.Clear ();
	}
}