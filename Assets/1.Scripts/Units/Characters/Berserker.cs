﻿using UnityEngine;
using System.Collections;

public class Berserker : Characters {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
	
	public override void setInitialUnitValues () {
		actions = 8;
	}
	
	public override void select() {
		base.select();
		print ("Berserker");
	}
}