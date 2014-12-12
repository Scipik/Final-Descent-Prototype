// Unit class
// Base class that our heroes and enemies will inherit from

using UnityEngine;
using System.Collections;

public class Units : MonoBehaviour, ISelectable, IActable<ActionNode>, IDamageable<int>, IMoveable<int, GameObject, Tile, Vector3, float>, IAttackable<GameObject, GameObject[]> {
	
	// Life Stats
	protected int maxHealth;
	public int health;
	
	// Number of actions a character has at the start of turn
	public int maxActions;
	public int actionsRemaining;
	
	public GameObject transparentSelf;
	public int unitNum; // For the queue system, refertence to their queue in the list
	
	public Tile tile; // The position of unit when moving in the que
	protected Tile targetTile;
	
	protected Vector3 charOffSet = new Vector3(0.0f, 1.5f, 0.0f); // So capsule is on top of grid instead of in it
	protected ActionLinkSystem als;
	
	// Use this for initialization
	protected virtual void Start () {
		setInitialUnitValues ();
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
		
		// Move to Activate maybe
		unitNum = -1;
		targetTile = tile;
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
		// unitNum = -1;
		actionsRemaining = maxActions;
		print(actionsRemaining);
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
			}
		}
	}
	
	// Executes action depicted by the given actioNode
	public virtual void doAction(ActionNode action) {
		if (typeof(MovementNode) == action.GetType()) {
			MovementNode temp = (MovementNode) action;
			move (temp.initialPosition, temp.targetPosition, temp.apCost);
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
		
		unitNum = als.setAction(gameObject.GetComponent<Characters>(), unitNum, movementAP, targetTile, moveTo.GetComponent<Tile>(), ghost);
		
		targetTile = moveTo.GetComponent<Tile>();
	}
	
	public virtual void move(Tile fromTile, Tile toTile, int apCost) {
		fromTile.taken = null;
		
		StartCoroutine(MoveToPosition(fromTile.gameObject.transform.position + charOffSet,
		                              toTile.gameObject.transform.position + charOffSet, ((float)apCost)/4.0f));
		
		fromTile = toTile;
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