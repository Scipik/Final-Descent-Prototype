﻿// Character class
// Base class that our heroes will inherit from
// Note: Might create one for enemies later,
//       thus code may be moved to general unit class and this will inherit from it

using UnityEngine;
using System.Collections;

public class Characters : MonoBehaviour, ISelectable, IActable, IDamageable<int>, IMoveable<GameObject, Tile, Vector3, float>, IAttackable<GameObject, GameObject[]> {

	// Number of actions a character has at the start of turn
	private int _maxActions;
	public int maxActions {
		get { return _maxActions; }
		set {
			if (value < 1) print ("Error: trying to set maxActions to less than zero");
			else _maxActions = value;
		}
	}
	public int actionsRemaining;
	
	public Tile onTile; // Tile unit is on
	
	private float movementAP; // Used to determine the speed of movment (Each grid movement is ideally .25s)
	private Vector3 newPosition;

	// Use this for initialization
	protected virtual void Start () {
		setInitialUnitValues ();
	}
	
	
	// Update is called once per frame
	protected virtual void Update () {
	}
	
	
	// Character stats are set here (Meant to be overridden)
	public virtual void setInitialUnitValues() {
		maxActions = 1;
	}
	
	
	// Select Interface implementation
	public virtual void select() {
		print ("Selected");
	}
	
	public virtual void deSelect() {
		print ("Deselected");
	}
	
	
	// Actor Interface
	public virtual void activate() {
		actionsRemaining = maxActions;
	}
	
	// Movement Interface implementation
	public virtual void displayMoveableArea() {
		onTile.enroachmentStart(actionsRemaining);
	}
	
	public virtual void removeMoveableArea() {
		onTile.deroachmentStart();
	}
	
	public virtual void setMove(GameObject moveTo) {
		newPosition = moveTo.transform.position;
		newPosition.y = newPosition.y + transform.position.y;
		
		print (moveTo.GetComponent<Tile>().distToSelectedUnit);
		movementAP = moveTo.GetComponent<Tile>().distToSelectedUnit;
		actionsRemaining -= moveTo.GetComponent<Tile>().distToSelectedUnit;
		removeMoveableArea();
		
		move (moveTo.GetComponent<Tile>());
	}
	
	public virtual void move(Tile newTile) {
		onTile.taken = null;
		
		StartCoroutine(MoveToPosition(newPosition, movementAP/4.0f));
		
		onTile = newTile;
		newTile.taken = gameObject;
	}
	
	
	// Movement coroutine, smooth movement
	public virtual IEnumerator MoveToPosition(Vector3 position, float timeToMove) {
		Vector3 currentPosition = transform.position;
		
		float t = 0f;
		while (t < 1) {
			t += Time.deltaTime/timeToMove;
			transform.position = Vector3.Lerp(currentPosition, position, t);
			yield return null;
		}
	}
	
	
	// Attack Interface implementation
	public virtual void attack(GameObject obj) {
		print ("Attack!");
	}
	
	public virtual void specialAttack(GameObject[] obj) {
		print ("I need more than just 1 special!");
	}
	
	
	// Damage Interface implementation
	public virtual void takeDamage(int damage) {
		print ("I'm hit!");
	}
	
	public virtual void healDamage(int heal) {
		print("My thanks");
	}
	
	public virtual void onDeath() {
		print ("Bye bye cruel world...");
	}
}
