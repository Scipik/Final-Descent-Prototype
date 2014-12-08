// Character class
// Base class that our heroes will inherit from
// Note: Might create one for enemies later,
//       thus code may be moved to general unit class and this will inherit from it

using UnityEngine;
using System.Collections;

public class Characters : MonoBehaviour, ISelectable, IActable, IDamageable<int>, IMoveable<GameObject>, IAttackable<GameObject, GameObject[]> {

	// Number of actions a character has at the start of turn
	private int _maxActions;
	public int maxActions {
		get { return _maxActions; }
		set {
			if (value < 1) print ("Error: trying to set maxActions to less than zero");
			else _maxActions = value;
		}
	}
	protected int actionsRemaining;
	
	public Tile onTile; // Tile unit is on

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
		onTile.deroachmentStart(actionsRemaining);
	}
	
	public virtual void move(GameObject moveTo) {
		removeMoveableArea();
	
		Vector3 newPosition = moveTo.transform.position;
		newPosition.y = newPosition.y + transform.position.y;
		transform.position = newPosition;
		
		// Decrement actions remaining here
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
