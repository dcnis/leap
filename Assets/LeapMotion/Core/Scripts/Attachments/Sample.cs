using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System;

public class Sample : MonoBehaviour {

	private Controller controller;
	private Hand leftHand;
	private Hand rightHand;


	// Use this for initialization
	void Start () {
		controller = new Controller ();

		Debug.Log (string.Format ("Controller = " + controller));

	}
	
	// Update is called once per frame
	void Update () {
		if (controller.IsConnected) { //controller is a Controller object
			Frame frame = controller.Frame (); //The latest frame
			Frame previous = controller.Frame (1); //The previous frame

			if(frame.Hands.Count > 1){
				List<Hand> hands = frame.Hands;
				if (hands[0].IsLeft){
					leftHand = hands [0];
					rightHand = hands [1];
				} else {
					rightHand = hands [0];
					leftHand = hands [1];
				}

			
					
				if (Math.Abs (Math.Abs(leftHand.PalmPosition.z) - Math.Abs((rightHand.PalmPosition.z))) > 80.0f){
//					Debug.Log ("Unterschied " + Math.Abs (Math.Abs (leftHand.PalmPosition.z) - Math.Abs ((rightHand.PalmPosition.z))));
					if (leftHand.PalmPosition.z < rightHand.PalmPosition.z) {
						Debug.Log ("JAB!!!");
					} else {
						Debug.Log ("CROSS!!!");
					}
				} 
					
			}
				
		}
	}

}