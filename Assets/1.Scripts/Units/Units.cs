// Unit class
// Base class that our heroes and enemies will inherit from

using UnityEngine;
using System.Collections;

public class Units : MonoBehaviour, ISelectable, IActable<ActionNode>, IDamageable<int>, IMoveable<int, GameObject, Tile, Vector3, float>, IAttackable<Tile, Units> {
	
	// Life Stats
	protected int maxHealth;
	public int health;
	
	// Attack Stats
	public int atkPow;
	public int atkCost;
	protected int atkRange;
	
	// Number of actions a character has at the start of turn
	public int maxActions;
	public int actionsRemaining;
	
	public GameObject transparentSelf;
	public int unitNum; // For the queue system, refertence to their queue in the list
	
	public Tile tile; // The position of unit when moving in the que
	protected Tile targetTile;
	
	protected Vector3 charOffSet = new Vector3(0.0f, 1.5f, 0.0f); // So capsule is on top of grid instead of in it
	protected ActionLinkSystem als;
	
	protected virtual void Awake() {
		setInitialUnitValues ();
	}
	
	// Use this for initialization
	protected virtual void Start () {
		als = GameObject.FindGameObjectWithTag("ActionLink").GetComponent<ActionLinkSystem>();
	}
	
	
	// Update is called once per frame
	protected virtual void Update () {
	}
	
	
	// Character stats are set here (Meant to be overridden)
	public virtual void setInitialUnitValues() {
		maxActions = 1;
		
		maxHealth = 10;
		health = maxHealth;
		
		atkPow = 1;
		atkCost = 1;
		atkRange = 1;
		
		// Move to Activate maybe
		unitNum = -1;
	}
	
	
	// Select Interface implementation
	public virtual void select() {
	}
	
	public virtual void deSelect() {
	}
	
	
	// Actor Interface
	public virtual void activate() {
		// unitNum = -1;
		targetTile = tile;
		actionsRemaining = maxActions;
		print (actionsRemaining);
	}
	
	// Removes last action from queue and returns the action points
	public virtual void cancelAction() {
		ActionNode temp = als.cancelLastAction(unitNum);
		if (temp != null) {
			if (typeof(MovementNode) == temp.GetType()) {
				MovementNode mTemp = (MovementNode)temp;
				targetTile = mTemp.initialPosition;
				Destroy(mTemp.ghost);
				actionsRemaining += mTemp.apCost;
			} else if (typeof(AttackNode) == temp.GetType()) {
				actionsRemaining += atkCost;
			}
		}
	}
	
	// Executes action depicted by the given actioNode
	public virtual void doAction(ActionNode action) {
		if (typeof(MovementNode) == action.GetType()) {
			MovementNode temp = (MovementNode) action;
			move (temp.initialPosition, temp.targetPosition, temp.apCost);
		} else if (typeof(AttackNode) == action.GetType()) {
			AttackNode temp = (AttackNode) action;
			if (temp.target != null) {
				attack (temp.target.taken.GetComponent<Units>());
			}
		}
	}
	
	// Movement Interface implementation
	public virtual void displayMoveableArea() {
		targetTile.enroachmentStart(actionsRemaining);
	}
	
	public virtual void removeMoveableArea() {
		targetTile.deroachmentStart();
	}
	
	public virtual void setMove(GameObject moveTo) {
		int movementAP = moveTo.GetComponent<Tile>().distToSelectedUnit;
		actionsRemaining -= moveTo.GetComponent<Tile>().distToSelectedUnit;
		removeMoveableArea();
		
		GameObject ghost = Instantiate(transparentSelf, moveTo.GetComponent<Tile>().transform.position + charOffSet, Quaternion.identity) as GameObject;
		ghost.transform.parent = transform.parent;
		moveTo.GetComponent<Tile>().taken = gameObject;
		
		unitNum = als.setAction(gameObject.GetComponent<Units>(), unitNum, movementAP, targetTile, moveTo.GetComponent<Tile>(), ghost);
		
		targetTile = moveTo.GetComponent<Tile>();
	}
	
	public virtual void move(Tile fromTile, Tile toTile, int apCost) {
		fromTile.taken = null;
		
		StartCoroutine(MoveToPosition(fromTile.gameObject.transform.position + charOffSet,
		                              toTile.gameObject.transform.position + charOffSet, ((float)apCost)/4.0f));
		
		tile = toTile;
		toTile.taken = gameObject;
	}
	
	
	// Movement coroutine, smooth movement
	public virtual IEnumerator MoveToPosition(Vector3 fromPos, Vector3 toPos, float timeToMove) {
		float t = 0f;
		while (t < 1) {
			t += Time.deltaTime/timeToMove;
			transform.position = Vector3.Lerp(fromPos, toPos, t);
			yield return null;
		}
		transform.position = toPos;
	}
	
	
	// Attack Interface implementation
	public virtual bool displayAttackableUnits() {
		return targetTile.atkEnroachmentStart(atkRange);
	}
	
	public virtual void removeAttackableUnits() {
		targetTile.atkDeroachmentStart(atkRange);
	}

	public virtual void setAttack(Tile tile) {
		actionsRemaining -= atkCost;
		removeAttackableUnits();
		
		unitNum = als.setAction(gameObject.GetComponent<Units>(), unitNum, atkCost, tile);
	}

	public virtual void attack(Units target) {
		StartCoroutine(AttackUnit(target));
	}
	
	// This was copied over from moveable, may not actually be neccessary for a basic attack
	public virtual IEnumerator AttackUnit(Units target) {
		target.takeDamage(atkPow);
		yield return new WaitForSeconds(0.25f);
	}
	
	// Damage Interface implementation
	public virtual void takeDamage(int damage) {
		health -= damage;
		if (health < 0)
			onDeath ();
	}
	
	public virtual void healDamage(int heal) {
		print("My thanks");
	}
	
	public virtual void onDeath() {
		print ("Bye bye cruel world...");
		Destroy (gameObject);
	}
}