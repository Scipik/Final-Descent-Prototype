﻿using UnityEngine;
using System.Collections;



public class Tile : MonoBehaviour {

	public int num; // Tile num, when it was created in list
	public int x, y; // Coordinate of tile in the list
	
	public GameObject taken; // If position is taken by an object
	
	public GameObject[] neighbors; // Tiles adjacent to this tile
	
	private Color baseColor;

	void Awake() {
		baseColor = renderer.material.color;
		neighbors = new GameObject[4];
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*
	void OnMouseOver() {
		renderer.material.color = Color.red;
	}
	
	void OnMouseExit() {
		renderer.material.color = baseColor;
	}
	
	void OnMouseDown() {
		string message = "";
		for (int i = 0; i < neighbors.Length; i++) {
			if (neighbors[i] != null) {
				message += i + ": " + neighbors[i].GetComponent<Tile>().num + "; ";
			} else {
				message += i + ": Null; ";
			}
			print(message);
		}
	}
	*/
}