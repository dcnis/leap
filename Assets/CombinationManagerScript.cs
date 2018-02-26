﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class CombinationManagerScript : MonoBehaviour {

	public TextAsset configFile;
	public Text displayCorrectPunches;
	public Text displayWrongPunches;
	public string[] combinationPool;
	public Text givenCombination;
	private int currentLine;
	private int endLine;
	private int randomLine;
	private Color _green = new Color (0, 255, 0, 1);
	private Color _red = new Color(255, 0, 0, 1);
	private Color _white = new Color(255,255,255);
	private bool isRunningCorrect = false;
	private bool isRunningFalse = false;
	public Text display_correctPunches;
	public Text display_wrongPunches;
	public Text display_reactionTime;
	private int currentCorrectPunches;
	private int currentWrongPunches;
	private float reactionTime;
	public GameObject gamePanel;
	public GameObject roundOverPanel;
	public int ROUND_TIME = 5;


	// Use this for initialization
	void Start () {

		currentCorrectPunches = 0;
		currentWrongPunches = 0;
		currentLine = 0;

		if (configFile != null){
			combinationPool = (configFile.text.Split ('\n'));
			for (int i = 0; i < combinationPool.Length; i++) {
				combinationPool [i] = combinationPool [i].Replace ("\r", "");
			}
		}
			
		endLine = combinationPool.Length - 1;

		givenCombination.supportRichText = true;

		StartCoroutine ("RoundTimer");
		gamePanel.SetActive (true);
		roundOverPanel.SetActive (false);

		HistoryData history = DataController.control.profile.getAllRoundsFromProfile ("Dennis");
		if(history != null){
			foreach(RoundData round in history) {
				Debug.Log (string.Format ("Round#: " + round.date));
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

		givenCombination.text = combinationPool[currentLine];
		displayCorrectPunches.text = currentCorrectPunches.ToString ();
		displayWrongPunches.text = currentWrongPunches.ToString ();


		if (checkCombination (combinationPool [currentLine])) {
			colorText (_green);
			if (!isRunningCorrect) StartCoroutine("WaitingTimeCorrect");
		} else if (!checkCombination (combinationPool [currentLine]) && Combination.thrown != ""){
			colorText(_red);
			if (!isRunningFalse) StartCoroutine("WaitingTimeWrong");
		}

		if (currentLine >= endLine) {
			currentLine = 0;
		}
	}
		

	private bool checkCombination(string currentCombo){
		Debug.Log (string.Format ("Ich vergleiche {0} mit {1} = {2}", currentCombo, Combination.thrown, String.Equals (currentCombo, Combination.thrown)));

		if (String.Equals (currentCombo, Combination.thrown)){
			return true;
		} 

		if (Combination.thrown.Length >= currentCombo.Length) {
			colorText(_red);
			if (!isRunningFalse) {StartCoroutine ("WaitingTimeWrong");}
			return false;
		}

		return false;


	}

	private void colorText(Color newColor) {
		givenCombination.color = newColor;
	}
		
	IEnumerator WaitingTimeCorrect() {
		isRunningCorrect = true;
		yield return new WaitForSeconds(0.500F);
		currentCorrectPunches++;
		Combination.thrown = "";
		givenCombination.color = _white;
		while(currentLine == randomLine)
			while(randomLine == currentLine){
				randomLine= UnityEngine.Random.Range (0, endLine);
			}
		currentLine = randomLine;
		isRunningCorrect = false;
	}

	IEnumerator WaitingTimeWrong() {
		isRunningFalse = true;
		yield return new WaitForSeconds (0.500F);
		currentWrongPunches++;
		Combination.thrown = "";
		givenCombination.color = _white;
		isRunningFalse = false;
	}

	IEnumerator RoundTimer() {
		yield return new WaitForSeconds(ROUND_TIME);
		display_wrongPunches.text = currentWrongPunches.ToString ();
		display_correctPunches.text = currentCorrectPunches.ToString ();
		reactionTime = ((float)currentCorrectPunches / (float)ROUND_TIME);
		display_reactionTime.text = reactionTime.ToString ();
		gamePanel.SetActive (false);
		roundOverPanel.SetActive (true);
		DataController.control.Save(currentCorrectPunches, currentWrongPunches);

		Debug.Log ("Successfully Saved RoundData");

	}


}