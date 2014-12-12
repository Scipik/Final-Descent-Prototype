using UnityEngine;
using System.Collections;

// A node containing information on a unit at the current link position (Actions, movement to, etc)
public class ActionNode {
	public bool actionStart; // Whether this is the initial que node or one of the endings
	public int apCost;
	
	// Constructor
	public ActionNode(bool aS, int apC) {
		this.actionStart = aS;
		this.apCost = apC;
	}
}

public class MovementNode : ActionNode {
	public Tile initialPosition; // Position when  (for movement and some attacks)
	public Tile targetPosition; // The target of action is applicable
	public GameObject ghost; // Transparent character to show player the new position
	
	public MovementNode(bool aS, int apC, Tile iP, Tile tP, GameObject g) : base (aS, apC) {
		this.initialPosition = iP;
		this.targetPosition = tP;
		this.ghost = g;
	}
}

public class AttackNode : ActionNode {
	public Tile target; // Tile that the targe occupies
	
	public AttackNode(bool aS, int apC, Tile t) : base (aS, apC) {
		this.target = t;
	}
}