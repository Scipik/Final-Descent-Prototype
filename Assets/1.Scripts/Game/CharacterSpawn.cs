// CharacterSpawn class
// Spawns our characters for battles, correctly setting them in the grids

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSpawn : MonoBehaviour {

	public GridSpawn grid;
	
	public GameObject[] heroes = new GameObject[4]; // The heroes to spawn for this battle
	public int [] startingX, startingY; // X and Y coordinates for the heroes staring positions
	
	
	void Awake() {
		//heroes = new GameObject[4];
	}

	// Use this for initialization
	void Start () {
		GameObject temp = grid.gridColumns[startingX[0]][startingY[0]];
		
		Vector3 newPosition = temp.transform.position;
		newPosition.y = newPosition.y + heroes[0].transform.position.y;
		transform.position = newPosition;
		
		GameObject hero = Instantiate(heroes[0], newPosition, Quaternion.identity) as GameObject;
		hero.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}