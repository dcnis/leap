using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class CombinationManagerScript : MonoBehaviour {

	public TextAsset configFile;
	public string[] combinationPool;
	public Text givenCombination;
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

		if (configFile != null){
			combinationPool = (configFile.text.Split ('\n'));
			for (int i = 0; i < combinationPool.Length; i++) {
				combinationPool [i] = combinationPool [i].Replace ("\r", "");
			}
		}
			
		endLine = combinationPool.Length - 1;

		givenCombination.supportRichText = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		givenCombination.text = combinationPool[currentLine];


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
		Combination.thrown = "";
		givenCombination.color = _white;
		currentLine = UnityEngine.Random.Range (0, endLine);
		isRunningCorrect = false;
	}

	IEnumerator WaitingTimeWrong() {
		isRunningFalse = true;
		yield return new WaitForSeconds (0.500F);
		Combination.thrown = "";
		givenCombination.color = _white;
		isRunningFalse = false;
	}


}