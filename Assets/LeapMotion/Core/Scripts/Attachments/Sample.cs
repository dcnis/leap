using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System;

public class Sample : MonoBehaviour {

	private Controller controller; 
	private SampleListener listener;

	// Use this for initialization
	void Start () {
		Controller controller = new Controller ();
		SampleListener listener = new SampleListener ();

		controller.Connect += listener.OnServiceConnect;
		controller.Device += listener.OnConnect;
		controller.FrameReady += listener.OnFrame;
	}
	
	// Update is called once per frame
	void Update () {

	}
}

public class SampleListener {
	public void OnServiceConnect(object sender, ConnectionEventArgs args)
	{
		Debug.Log ("Service connected");
	}

	public void OnConnect(object sender, DeviceEventArgs args)
	{
		Debug.Log ("Connected");
	}

	public void OnFrame(object sender, FrameEventArgs args)
	{
		//Get the most recent frame and report some basic information
		Frame frame = args.frame;

		Debug.Log (string.Format("" +
			"Frame id: {0}, timestamp: {1}, hands: {2}",
			frame.Id, frame.Timestamp, frame.Hands.Count));
		foreach (Hand hand in frame.Hands) {
			Debug.Log (string.Format("Hand isLeft:  {0}, Hand isRight: {1}, fingers: {2}, timestamp: {3}", hand.IsLeft, hand.IsRight, hand.Fingers.Count, hand.TimeVisible));
			//Get the hands normal vector and direction
			Vector normal = hand.PalmNormal;
			Vector direction = hand.Direction;


		}
	}
}
