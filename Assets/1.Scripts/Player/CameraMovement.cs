using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public int boundary;
	public int cameraSensitivity;
	
	// Limiters so player can't continuously scroll away
	public int pXLimit, nXLimit, pYLimit, nYLimit;

	private int screenWidth, screenHeight; // For edge panning
	private float tempXPos, tempYPos; // For temporay storing

	// Use this for initialization
	void Start () {
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		edgePanCheck ();
	}
	
	// Edge Panning Checks
	void edgePanCheck() {
		// +X axis checking
		if (transform.position.x != pXLimit && (Input.mousePosition.x > (screenWidth - boundary) || Input.GetKey(KeyCode.D))) {
			tempXPos = transform.position.x + cameraSensitivity * Time.deltaTime;
			if (tempXPos > pXLimit)
				tempXPos = pXLimit;
			transform.position = new Vector3(tempXPos, transform.position.y, transform.position.z);
		}
		
		// -X axis checking
		if (transform.position.x != nXLimit && (Input.mousePosition.x < boundary || Input.GetKey(KeyCode.A))) {
			tempXPos = transform.position.x - cameraSensitivity * Time.deltaTime;
			if (tempXPos < nXLimit)
				tempXPos = nXLimit;
			transform.position = new Vector3(tempXPos, transform.position.y, transform.position.z);
		}
		
		// +Z axis checking
		if (transform.position.z != pYLimit && (Input.mousePosition.y > (screenHeight - boundary) || Input.GetKey(KeyCode.W))) {
			tempYPos = transform.position.z + cameraSensitivity * Time.deltaTime;
			if (tempYPos > pYLimit)
				tempYPos = pYLimit;
			transform.position = new Vector3(transform.position.x, transform.position.y, tempYPos);
		}
			
		// -Z axis checking
		if (transform.position.z != nYLimit && (Input.mousePosition.y < boundary || Input.GetKey(KeyCode.S))) {
			tempYPos = transform.position.z - cameraSensitivity * Time.deltaTime;
			if (tempYPos < nYLimit)
				tempYPos = nYLimit;
			transform.position = new Vector3(transform.position.x, transform.position.y, tempYPos);
		}
	}
}
