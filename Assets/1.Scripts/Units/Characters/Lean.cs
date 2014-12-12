// Lean

using UnityEngine;
using System.Collections;

public class Lean : Characters {
	
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
		maxActions = 10;
		maxHealth = 35;
		health = maxHealth;
		
		atkPow = 5;
		atkCost = 2;
		atkRange = 1;
	}
	
	public override void select() {
	}
}
