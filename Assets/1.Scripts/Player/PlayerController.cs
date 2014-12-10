// Class for our player controller
// Handles keyboard and mouse clicks and also our GUI (May move this to anther class later)

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public FieldMaster field;
	
	public GameObject highlightStats; // Reference to the obj we have highlighted for stat display
	
	private int guiDisplay; // int value so we know what gui's to display
	private Characters selection;

	// Use this for initialization
	void Start () {
		guiDisplay = 0;
	}
	
	// Update is called once per frame
	void Update () {
		leftClick ();
		rightClick();
	}
	
	// Functions for left clicking (Selection or Commands)
	private void leftClick() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				if (selection == null) {
					if (hit.collider.gameObject.tag == "Character") {
						selection = hit.collider.gameObject.GetComponent<Characters>();
						selection.select ();
						guiDisplay = 1;
					}
				} else {
					if (guiDisplay == 2 && hit.collider.gameObject.tag == "Tile") {
						if (hit.collider.gameObject.GetComponent<Tile>().highlighted) {
							selection.move(hit.collider.gameObject);
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
						selection.GetComponent<Characters>().removeMoveableArea();
						break;
					case 3: // Attack Menu
						guiDisplay = 1;
						break;
					default:
						break;
				}
			}
		}
	}
	
	void OnGUI() {
		if (selection != null) {
			GUI.TextField(new Rect(20, 20, 150, 20), selection.name, 25);
			// print ("fancy Action point and turn movement list");
			
			GUI.TextField(new Rect(20, 60, 150, 20), "AP Remaining: " + selection.actionsRemaining, 25);
			
			switch (guiDisplay) {
				case 1: // Basic Character actions
					if (GUI.Button(new Rect(20, 100, 150, 40), "Move (1)") || Input.GetKeyDown(KeyCode.Alpha1)) {
						selection.displayMoveableArea();
						guiDisplay = 2;
					}
					if (GUI.Button(new Rect(20, 160, 150, 40), "Attack (2)") || Input.GetKeyDown(KeyCode.Alpha2)) {
						Debug.Log("Attack Pressed");
						guiDisplay = 3;
					}
					break;
				case 2:
					if (highlightStats == null)
						GUI.TextField(new Rect(20, 100, 150, 20), "AP Cost: 0", 25);
					else
						GUI.TextField(new Rect(20, 100, 150, 20), "AP Cost: " + highlightStats.GetComponent<Tile>().distToSelectedUnit, 25);
					break;
				case 3: // Attack actions
					if (GUI.Button(new Rect(20, 100, 150, 40), "Normal Attack")) {
						Debug.Log("Normal attack");
						guiDisplay = 0;
					}
					if (GUI.Button(new Rect(20, 160, 150, 40), "Skills?")) {
						Debug.Log("Special Powa");
						guiDisplay = 0;
					}
					break;
				default:
					break;
			}
		} else {
			GUI.TextField(new Rect(20, 20, 150, 20), "Nothing Selected", 25);
		}
	}
}
