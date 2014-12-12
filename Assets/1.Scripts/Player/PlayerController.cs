// Class for our player controller
// Handles keyboard and mouse clicks and also our GUI (May move this to anther class later)

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int turn;
	public FieldMaster field;
	
	public GameObject highlightStats; // Reference to the obj we have highlighted for stat display
	public bool interactable; // Boolean to tell us if we can interact right now (Used for enemy turn and animations
	public CharacterSpawn heroes;
	
	private int guiDisplay; // int value so we know what gui's to display
	private Units selection;
	private ActionLinkSystem als;

	// Use this for initialization
	void Start () {
		guiDisplay = 0;
		als = GameObject.FindGameObjectWithTag("ActionLink").GetComponent<ActionLinkSystem>();
		// heroes = GameObject.FindGameObjectWithTag("Heroes").GetComponent<CharacterSpawn>();
		interactable = true;
		turn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (interactable) {
			leftClick ();
			rightClick();
			keyPresses();
		}
	}
	
	// Functions for left clicking (Selection or Commands)
	private void leftClick() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				if (selection == null) {
					if (hit.collider.gameObject.tag == "Character" && turn%2 == 1) {
						selection = hit.collider.gameObject.GetComponent<Characters>();
						selection.select ();
						guiDisplay = 1;
					} else if (hit.collider.gameObject.tag == "Enemy" && turn%2 == 0) {
						selection = hit.collider.gameObject.GetComponent<Enemies>();
						selection.select ();
						guiDisplay = 1;
					}
				} else {
					if (guiDisplay == 2 && hit.collider.gameObject.tag == "Tile") {
						if (hit.collider.gameObject.GetComponent<Tile>().highlighted) {
							selection.setMove(hit.collider.gameObject);
							guiDisplay = 1;
						}
					}
					if (guiDisplay == 4 && hit.collider.gameObject.tag == "Tile") {
						if (hit.collider.gameObject.GetComponent<Tile>().highlighted) {
							selection.setAttack(hit.collider.gameObject.GetComponent<Tile>());
							guiDisplay = 1;
						}
					}
				}
			}
		}
	}
	
	// Function for right clicking (Cancel selection or command generally)
	private void rightClick() {
		if (Input.GetMouseButtonDown(1)) {
			if (selection != null) {
				switch(guiDisplay) {
					case 1: // Inital menu
						selection.deSelect();
						selection = null;
						guiDisplay = 0;
						break;
					case 2: // Movement menu
						guiDisplay = 1;
						selection.GetComponent<Units>().removeMoveableArea();
						break;
					case 3: // Attack Menu
						guiDisplay = 1;
						break;
					case 4:
						selection.removeAttackableUnits();
						guiDisplay = 3;
						break;
					default:
						break;
				}
			}
		}
	}
	
	private void keyPresses() {
		switch (guiDisplay) {
			case 1: // Basic Character actions
				if (Input.GetKeyDown(KeyCode.Alpha1) && selection.actionsRemaining > 0) {
					selection.displayMoveableArea();
					guiDisplay = 2;
				}
				if (Input.GetKeyDown(KeyCode.Alpha2)) {
					guiDisplay = 3;
				}
				if (Input.GetKeyDown(KeyCode.Alpha3)) {
					Debug.Log("Wait Pressed");
					// Tells character to add action to que
				}
				if (Input.GetKeyDown(KeyCode.R)) {
					selection.cancelAction ();
				}
				break;
			case 2:
			case 3:
				if (Input.GetKeyDown(KeyCode.Alpha1) && selection.actionsRemaining >= selection.atkCost) {
					if (selection.displayAttackableUnits()) {
						guiDisplay = 4;
					} else {
						print("No attackable units in range");
					}
				}
				break;
			default:
				break;
		}
		if (Input.GetKeyDown (KeyCode.Space) && selection != null && (guiDisplay == 0 || guiDisplay == 1)) {
			StartCoroutine(als.excuteActions());
			guiDisplay = 0;
			interactable = false;
			selection = null;
		}
	}
	
	void OnGUI() {
		if (selection != null) {
			GUI.TextField(new Rect(20, 20, 150, 20), selection.name, 25);
			
			GUI.TextField(new Rect(20, 60, 150, 20), "AP Remaining: " + selection.actionsRemaining, 25);
			
			switch (guiDisplay) {
				case 1: // Basic Character actions
					if (GUI.Button(new Rect(20, 100, 150, 40), "Move (1)") && selection.actionsRemaining > 0) {
						selection.displayMoveableArea();
						guiDisplay = 2;
					}
					if (GUI.Button(new Rect(20, 160, 150, 40), "Attack (2)")) {
						guiDisplay = 3;
					}
					if (GUI.Button(new Rect(20, 220, 150, 40), "Wait (3)")) {
						Debug.Log("Wait Pressed");
						// Tells character to add action to que
					}
					if (GUI.Button(new Rect(20, 280, 150, 40), "Cancel Action (R)")) {
						selection.cancelAction ();
						break;
					}
					break;
				case 2:
					if (highlightStats == null)
						GUI.TextField(new Rect(20, 100, 150, 20), "AP Cost: 0", 25);
					else
						GUI.TextField(new Rect(20, 100, 150, 20), "AP Cost: " + highlightStats.GetComponent<Tile>().distToSelectedUnit, 25);
					break;
				case 3: // Attack actions
				if (GUI.Button(new Rect(20, 100, 150, 40), "Normal Attack: " + selection.atkCost + "AP (1)") && selection.actionsRemaining >= selection.atkCost) {
						if (selection.displayAttackableUnits()) {
							guiDisplay = 4;
						} else {
							print("No attackable units in range");
						}
					}
					break;
				default:
					break;
			}
		} else {
			GUI.TextField(new Rect(20, 20, 150, 20), "Nothing Selected", 25);
		}
	}
	
	public void switchTurn() {
		turn++;
		if (turn%2 == 1) {
			playersTurn ();
		} else {
			enemiesTurn();
		}
	}
	
	public void playersTurn() {
		interactable = true;
		for (int i = 0; i < heroes.playerCharacters.Length; i++) {
			heroes.playerCharacters[i].activate ();
		}
		print ("Player Turn");
	}
	
	public void enemiesTurn() {
		interactable = true;
		for (int i = 0; i < heroes.enemies.Length; i++) {
			heroes.enemies[i].activate ();
		}
		print ("Enemies Turn");
	}
}
