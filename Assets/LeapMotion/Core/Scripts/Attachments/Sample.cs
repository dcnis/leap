using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System;
using UnityEngine.SceneManagement;

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
	private bool isRunning = false;
	private bool extractCombinationFinished = false;

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
					this.evaluatePunch (hands[0], hands[1]);
					this.evaluatePunch (hands[1], hands[0]);
			
				if (punch == "none") {
					if (!isRunning) {
						StartCoroutine ("IsCombinationDone");
					}
				}




			}




		}
	}

	private void evaluatePunch (Hand hand, Hand otherHand)
	{
		if (punch != "none") {
			return;
		}


		if (hand.PalmVelocity.Dot (new Vector (0, 0, -1)) > PALM_Z_VELOCITY) {
			if (hand.IsLeft) {
				punch = LEFT_PUNCH;
			} else {
				punch = RIGHT_PUNCH;
			}
			this.punchCombination = string.Concat (this.punchCombination, punch);
			Debug.Log ("My PunchCombination: " +  punchCombination);
			return;
		} else if (hand.PalmVelocity.Dot (new Vector (0, 0, 1)) > PALM_Z_VELOCITY &&
			otherHand.PalmVelocity.Dot (new Vector (0, 0, -1)) < PALM_Z_VELOCITY) {
			this.punchCombination = string.Concat (this.punchCombination, "|");
			return;
		}

	}

	IEnumerator IsCombinationDone() {
		isRunning = true;
		if(punch == "none"){
			yield return new WaitForSeconds(1.2F);
		}
		if (punch == "none" && this.punchCombination.Length > 0){
			this.extractCombination ();
			if(extractCombinationFinished){
				this.punchCombination = "";
				Debug.Log ("I DELETED IT!!!!!" + DateTime.Now.ToString ());
				this.combination = this.combination.Replace("|", "");
				Debug.Log (string.Format ("SINGLE COMBINATION: {0} at..........{1}", this.combination, DateTime.Now.ToString ()));
				this.extractCombinationFinished = false;
				Combination.thrown = this.combination;
			}

		}
		isRunning = false;
	}

	private void extractCombination(){

		this.combination = "";
		this.combination = string.Concat (this.combination, this.punchCombination [0]);

		for (int i = 0; i < this.punchCombination.Length; i++) {
			if (this.punchCombination.Length > 2) {
				if (i == this.punchCombination.Length - 1){break;}
				if (this.punchCombination [i] != this.punchCombination [i + 1]) {
					this.combination = string.Concat (this.combination, this.punchCombination [i + 1]);
				}
			}
		}

		this.extractCombinationFinished = true;
	}





}