// Character class
// Base class that our enemies will inherit from

using UnityEngine;
using System.Collections;

public class Enemies : Units {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
	
	public override void setInitialUnitValues () {
		base.setInitialUnitValues();
	}
	
	public override void select() {
		print ("Enemy");
	}
}
