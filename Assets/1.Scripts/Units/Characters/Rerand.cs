using UnityEngine;
using System.Collections;

public class Rerand : Characters {

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
		maxActions = 8;
		
		maxHealth = 30;
		health = maxHealth;
		
		atkPow = 4;
		atkCost = 3;
		atkRange = 4;
	}
	
	public override void select() {
	}
}
