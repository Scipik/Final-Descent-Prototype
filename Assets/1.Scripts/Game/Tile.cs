// Tile Code
// The tiles that represent our grid
// Hold position and unit information

using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public int num; // Tile num, when it was created in list
	public int x, y; // Coordinate of tile in the list
	
	public bool highlighted;
	public GameObject taken; // Object that is on this tile
	
	public GameObject[] neighbors; // Tiles adjacent to this tile
	
	private Color baseColor;

	void Awake() {
		baseColor = renderer.material.color;
		neighbors = new GameObject[4];
		highlighted = false;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Methods used to highlight moveable/attackable area
	// This is called where the unit is
	public void enroachmentStart(int step) {
		if (step > 0) {
			Tile temp;
			
			foreach (GameObject nei in neighbors) {
				if (nei == null) continue;
				
				temp = nei.GetComponent<Tile>();
				if (!temp.taken) {
					temp.enroach(step);
				} 
			}
		}
	}
	
	// Used to expand the area from initial
	public void enroach(int step) {
		highlighted = true;
		renderer.material.color = Color.green;
		step--;
		
		if (step > 0) {
			Tile temp;
			
			foreach (GameObject nei in neighbors) {
				if (nei == null) continue;
				
				temp = nei.GetComponent<Tile>();
				if (!temp.taken) {
					temp.enroach(step);
				} 
			}
		}
	}
	
	// Removes highlight starting with units initial position
	public void deroachmentStart(int step) {
		if (step > 0) {
			Tile temp;
			
			foreach (GameObject nei in neighbors) {
				if (nei == null) continue;
				
				temp = nei.GetComponent<Tile>();
				if (!temp.taken) {
					temp.deroach(step);
				} 
			}
		}
	}
	
	// Used to remove the expanded area from initial;
	public void deroach(int step) {
		highlighted = false;
		renderer.material.color = baseColor;
		step--;
	
		if (step > 0) {
			Tile temp;
			
			foreach (GameObject nei in neighbors) {
				if (nei == null) continue;
				
				temp = nei.GetComponent<Tile>();
				if (!temp.taken) {
					temp.deroach(step);
				} 
			}
		}
	}
			
	
	
	void OnMouseOver() {
		if (highlighted) {
			renderer.material.color = Color.blue;
		}
	}
	
	void OnMouseExit() {
		if (highlighted) {
			renderer.material.color = Color.green;
		} else {
			renderer.material.color = baseColor;
		}
	}
}
