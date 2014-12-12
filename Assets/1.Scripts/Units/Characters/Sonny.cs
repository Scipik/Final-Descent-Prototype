using UnityEngine;
using System.Collections;

public class Sonny : Characters {

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
		
		maxHealth = 45;
		health = maxHealth;
		
		atkPow = 7;
		atkCost = 4;
		atkRange = 2;
	}
	
	public override void select() {
	}
}
