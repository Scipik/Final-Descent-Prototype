using UnityEngine;
using System.Collections;

public class Characters : MonoBehaviour, ISelectable, IDamageable<int>, IMoveable<GameObject>, IAttackable<GameObject, GameObject[]> {

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
	

	// Use this for initialization
	protected virtual void Start () {
		setInitialUnitValues ();
	}
	
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
	
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
	
	
	// Movement Interface implementation
	public virtual void displayMoveableArea() {
		print ("Display moveable area");
	}
	
	public virtual void move(GameObject moveTo) {
		Vector3 newPosition = moveTo.transform.position;
		print ("Moving to: " + newPosition);
		newPosition.y = newPosition.y + transform.position.y;
		transform.position = newPosition;
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
