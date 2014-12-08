using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public FieldMaster field;
	
	private Characters selection;

	// Use this for initialization
	void Start () {
	
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
					}
				} else {
					if (hit.collider.gameObject.tag == "Tile") {
						selection.move(hit.collider.gameObject);
					}
				}
			}
		}
	}
	
	// Function for right clicking (Cancel selection or command generally)
	private void rightClick() {
		if (Input.GetMouseButtonDown(1)) {
			if (selection != null) {
				selection.deSelect();
				selection = null;
			}
		}
	}
}
