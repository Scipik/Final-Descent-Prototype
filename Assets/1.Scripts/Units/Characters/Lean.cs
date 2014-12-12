// Lean

using UnityEngine;
using System.Collections;

public class Lean : Characters {
	
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
		maxActions = 10;
		maxHealth = 35;
		health = maxHealth;
	}
	
	public override void select() {
		print ("Lean");
	}
}
