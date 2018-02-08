using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CombinationManagerScript : MonoBehaviour {

	public TextAsset textFile;
	public string[] textLines;
	public Text combinationNumber;
	private int currentLine;
	private int endLine;
	private Color _green = new Color (0, 255, 0, 1);
	private Color _red = new Color(255, 0, 0, 1);
	private Color _white = new Color(255,255,255);
	private bool isRunningCorrect = false;
	private bool isRunningFalse = false;


	// Use this for initialization
	void Start () {

		currentLine = 0;

		if (textFile != null){
			textLines = (textFile.text.Split ('\n'));
			for (int i = 0; i < textLines.Length; i++) {
				textLines [i] = textLines [i].Replace ("\r", "");
			}
		}
			
		endLine = textLines.Length - 1;

		combinationNumber.supportRichText = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		combinationNumber.text = textLines[currentLine];


		if(!isRunningFalse && !isRunningCorrect){
			if (checkCombination (textLines[currentLine])){
				colorText (_green);
				if (!isRunningCorrect) StartCoroutine("WaitingTimeCorrect");
			}

			if (currentLine > endLine){
				currentLine = 0;
			}
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
		combinationNumber.color = newColor;
	}

	IEnumerator WaitingTimeCorrect() {
		isRunningCorrect = true;
		print(Time.time);
		yield return new WaitForSeconds(2);
		Combination.thrown = "";
		combinationNumber.color = _white;
		currentLine += 1;
		print(Time.time);
		isRunningCorrect = false;
	}

	IEnumerator WaitingTimeWrong() {
		isRunningFalse = true;
		print(Time.time);
		yield return new WaitForSeconds (2);
		Combination.thrown = "";
		combinationNumber.color = _white;
		print(Time.time);
		isRunningFalse = false;
	}



}