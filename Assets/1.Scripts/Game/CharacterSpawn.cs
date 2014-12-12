// CharacterSpawn class
// Spawns our characters for battles, correctly setting them in the grids

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSpawn : MonoBehaviour {

	public GridSpawn grid;
	public PlayerController controller;
	
	public GameObject[] heroes = new GameObject[4]; // The heroes to spawn for this battle
	public GameObject enemy; // Enemies that spawn for this battle
	public Characters[] playerCharacters = new Characters[4]; // Reference to the heroes
	public Enemies[] enemies = new Enemies[3]; // reference to enemies
	public int[] startingX, startingY; // X and Y coordinates for the heroes staring positions
	public int[] enemyX, enemyY;
	
	
	void Awake() {
		//heroes = new GameObject[4];
	}

	// Use this for initialization
	void Start () {
		GameObject temp;
		Vector3 newPosition;
		
		// Instantiate our heroes
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
		
		// Instantiate enemies
		for (int i = 0; i < 3; i++) {
			temp = grid.gridColumns[enemyX[i]][enemyY[i]];
			
			newPosition = temp.transform.position;
			newPosition.y = newPosition.y + enemy.transform.position.y;
			
			GameObject ene = Instantiate(enemy, newPosition, Quaternion.identity) as GameObject;
			ene.transform.parent = transform;
			ene.GetComponent<Enemies>().tile = temp.GetComponent<Tile>();
			temp.GetComponent<Tile>().taken = ene;
			enemies[i] = ene.GetComponent<Enemies>();
		}
		
		controller.switchTurn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}