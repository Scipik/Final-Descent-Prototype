// Scipik 

using UnityEngine;
using System.Collections;

public class Scipik : Characters {

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
		maxActions = 6;
		maxHealth = 50;
		health = maxHealth;
	}
	
	public override void select() {
		print ("Scipik");
	}
}
