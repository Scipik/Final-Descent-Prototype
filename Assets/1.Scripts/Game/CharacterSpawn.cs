// CharacterSpawn class
// Spawns our characters for battles, correctly setting them in the grids

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSpawn : MonoBehaviour {

	public GridSpawn grid;
	public PlayerController controller;
	
	public GameObject[] heroes = new GameObject[2]; // The heroes to spawn for this battle
	public Characters[] playerCharacters = new Characters[2]; // Reference to the heroes
	public int [] startingX, startingY; // X and Y coordinates for the heroes staring positions
	
	
	void Awake() {
		//heroes = new GameObject[4];
	}

	// Use this for initialization
	void Start () {
		GameObject temp;
		Vector3 newPosition;
		
		// Initiate our heroes
		for (int i = 0; i < heroes.Length; i++) {
			temp = grid.gridColumns[startingX[i]][startingY[i]];
			
			newPosition = temp.transform.position;
			newPosition.y = newPosition.y + heroes[i].transform.position.y;
			
			GameObject hero = Instantiate(heroes[i], newPosition, Quaternion.identity) as GameObject;
			hero.transform.parent = transform;
			hero.GetComponent<Characters>().tile = temp.GetComponent<Tile>();
			temp.GetComponent<Tile>().taken = hero;
			playerCharacters[i] = hero.GetComponent<Characters>();
		}
		// transform.position = newPosition;
		
		controller.playersTurn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}