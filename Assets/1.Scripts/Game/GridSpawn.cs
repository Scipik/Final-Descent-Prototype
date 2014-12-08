// GridSpawn class
// Spawns the grid of the world

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSpawn : MonoBehaviour {

	public GameObject tile;
	public FieldMaster field;
	public List<List<GameObject>> gridColumns = new List<List<GameObject>>();

	void Awake() {
		float scale = 2f;
		
		// Insert List of tiles into the 2d List
		for (int y = -10; y < 11; y++) {
			
			List<GameObject> gridRows = new List<GameObject>();
			
			for (int x = -10; x < 11; x++) {
				Vector3 temp = new Vector3(transform.position.x + x * scale, transform.position.y, transform.position.z - y * scale);
				GameObject newTile = Instantiate(tile, temp, Quaternion.identity) as GameObject;
				newTile.transform.parent = transform;
				
				gridRows.Add (newTile);
			}	
			gridColumns.Add (gridRows);
		}
		
		setTile ();
		field.initialRotate ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Set the tile values and neighbors
	void setTile() {
		int i = 0;
		
		for (int y = 0; y < 21; y++) {
			for (int x = 0; x < 21; x++) {
				Tile data = gridColumns[y][x].GetComponent<Tile>();
				
				data.num = i++;
				data.x = x;
				data.y = y;
				
				// Left neighbor
				if (x - 1 < 0)
					data.neighbors[0] = null;
				else
					data.neighbors[0] = gridColumns[y][x - 1];
				
				// Top neighbor
				if (y - 1 < 0)
					data.neighbors[1] = null;
				else
					data.neighbors[1] = gridColumns[y - 1][x];
				
				// Right neightbor
				if (x + 1 >= 21)
					data.neighbors[2] = null;
				else
					data.neighbors[2] = gridColumns[y][x + 1];
				
				// Bottom neightbor
				if (y + 1 >= 21)
					data.neighbors[3] = null;
				else
					data.neighbors[3] = gridColumns[y + 1][x];
			}
		}
	}
}
