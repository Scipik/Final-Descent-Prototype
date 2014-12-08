// FieldMaster Class
// Deals with rotation of field

using UnityEngine;
using System.Collections;

public class FieldMaster : MonoBehaviour {

	public int baseSmooth;

	private Quaternion newRotation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		rotateField();
		
		if(Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") > 0)
			rotateCounterClockWise ();
		if(Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") < 0)
			rotateClockWise ();
	}
	
	// Checks if current rotation is where we want it
	// If not starts/continues the lerp (Linear interpretation) rotation
	private void rotateField() {
		if (!equalsRotation(newRotation)) {
			transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, baseSmooth * Time.deltaTime);
		}
	}
	
	private bool equalsRotation(Quaternion check) {
		if (check.w == transform.rotation.w && check.x == transform.rotation.x && check.y == transform.rotation.y && check.z == transform.rotation.z)
		    return true;
		return false;
	}
	
	public void initialRotate() {
		transform.rotation = Quaternion.Euler(0, 45, 0);
		newRotation = transform.rotation;
	}
	
	// Base Rotation for our battles
	public void rotateClockWise() {
		newRotation = Quaternion.Euler (newRotation.eulerAngles.x, newRotation.eulerAngles.y + 90, newRotation.eulerAngles.z);
	}
	
	public void rotateCounterClockWise() {
		newRotation = Quaternion.Euler (newRotation.eulerAngles.x, newRotation.eulerAngles.y - 90, newRotation.eulerAngles.z);
	}
	
	// A mutable transition for cut/battle scenes and other cool stuff
	public void rotateDynamic(Vector3 newPosition, int newSmooth) {
		
	}
}
