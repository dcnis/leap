using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System;

public class Sample : MonoBehaviour
{

	private Controller controller;
	private Hand leftHand;
	private Hand rightHand;
	private String punch = "none";
	const double PALM_Z_VELOCITY = 500.0;
	const string LEFT_PUNCH = "1";
	const string RIGHT_PUNCH = "2";
	private String combination = "";
	private String punchCombination = "";
	private bool isCombinationCreating = false;
	private bool firstPunchThrown = false; 

	// Use this for initialization
	void Start ()
	{
		controller = new Controller ();

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (controller.IsConnected) { //controller is a Controller object
			Frame frame = controller.Frame (); //The latest frame

			if (frame.Hands.Count > 1) {
				this.punch = "none";
				List<Hand> hands = frame.Hands;
				foreach (Hand hand in hands) {
					this.evaluatePunch (hand);
				}
					
			}
		}
	}

	private void evaluatePunch (Hand hand)
	{
		if (punch != "none") {
			return;
		}


		if (hand.PalmVelocity.Dot (new Vector (0, 0, -1)) > PALM_Z_VELOCITY) {
			if (hand.IsLeft) {
				punch = LEFT_PUNCH;
				Combination.thrown = this.punch;
			} else {
				punch = RIGHT_PUNCH;
				Combination.thrown = this.punch;
			}
//			this.punchCombination = string.Concat (this.punchCombination, punch);
			Debug.Log (punch);
			return;
		}

	}


	private void sendCombination ()
	{
//		if (punch != "none") {
//			return;
//		}
//		this.punchCombination = "";
//		this.isCombinationCreating = true;
//		this.punchCombination = string.Concat (this.punchCombination, punchCombo [0]);
//
//		for (int i = 0; i < punchCombo.Length; i++) {
//			if (punchCombo.Length > 2) {
//				if (i == punchCombo.Length){break;}
//				if (punchCombo [i] != punchCombo [i + 1]) {
//					this.punchCombination = string.Concat (this.punchCombination, punchCombo [i + 1]);
//				}
//			}
//		}
//
		Debug.Log ("PUNCH THROWN IS FOLLOWING: " + this.punch);

}
		


}