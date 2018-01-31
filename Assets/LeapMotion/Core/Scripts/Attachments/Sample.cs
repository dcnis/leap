using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System;

public class Sample : MonoBehaviour {

	private Controller controller;
	private Hand leftHand;
	private Hand rightHand;
	private Hand rightHandPreviousFrame;
	private Hand leftHandPreviousFrame;


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


			getHandsfromFrame (frame);
			getHandsfromPreviousFrame (previous);
					
			if (punchIsThrown(leftHand, rightHand) && !punchIsThrown (leftHandPreviousFrame, rightHandPreviousFrame) && (punchForwardLeft() || punchForwardRight ())) {
					if (leftHand.PalmPosition.z < rightHand.PalmPosition.z) {
						Debug.Log ("JAB!!!");
						return;
					} else {
						Debug.Log ("CROSS!!!");
						return;
					}
				} 
					

				
		}
	}

	bool punchIsThrown (Hand _lefthand, Hand _righthand)
	{
		return Math.Abs (Math.Abs (_lefthand.PalmPosition.z) - Math.Abs ((_righthand.PalmPosition.z))) > 80.0f;
	}


	void getHandsfromFrame(Frame frame){
		if(frame.Hands.Count > 1){
			List<Hand> hands = frame.Hands;
			if (hands[0].IsLeft){
				leftHand = hands [0];
				rightHand = hands [1];
			} else {
				rightHand = hands [0];
				leftHand = hands [1];
			}
		}

	}

	void getHandsfromPreviousFrame(Frame frame){
		if(frame.Hands.Count > 1){
			List<Hand> hands = frame.Hands;
			if (hands[0].IsLeft){
				leftHandPreviousFrame = hands [0];
				rightHandPreviousFrame = hands [1];
			} else {
				rightHandPreviousFrame = hands [0];
				leftHandPreviousFrame = hands [1];
			}
		}

	}

	bool punchForwardLeft() {
		return leftHandPreviousFrame.PalmPosition.z > leftHand.PalmPosition.z;
}

	bool punchForwardRight(){
		return rightHandPreviousFrame.PalmPosition.z > rightHand.PalmPosition.z;;
	}

}