using UnityEngine;
using System.Collections;

public class GenericEnemy : Enemies {

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
		maxActions = 7;
		maxHealth = 45;
		health = maxHealth;
	}
	
	public override void select() {
		print ("Generic");
	}
}
