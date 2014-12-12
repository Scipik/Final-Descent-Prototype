// Scipik 

using UnityEngine;
using System.Collections;

public class Scipik : Characters {

	protected override void Awake () {
		base.Awake ();
	}

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
		
		atkPow = 8;
		atkCost = 3;
		atkRange = 1;
	}
	
	public override void select() {
	}
}
