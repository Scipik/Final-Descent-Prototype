using UnityEngine;
using System.Collections;

public class TestCharacter : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void move(Vector3 newPosition) {
		newPosition.y = newPosition.y + transform.position.y;
		transform.position = newPosition;
	}
}
