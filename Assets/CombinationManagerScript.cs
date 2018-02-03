using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationManagerScript : MonoBehaviour {

	public TextAsset textFile;
	public string[] textLines;
	public GameObject combinationBox;
	public Text combinationNumber;
	private int currentLine;
	private int endLine;

	// Use this for initialization
	void Start () {

		currentLine = 0;


		if (textFile != null){
			textLines = (textFile.text.Split ('\n'));
		}


		endLine = textLines.Length - 1;

		
	}
	
	// Update is called once per frame
	void Update () {

		combinationNumber.text = textLines[currentLine];

		if (Input.GetKeyDown (KeyCode.Return)){
			currentLine += 1;
		}

		if (currentLine > endLine){
			currentLine = 0;
		}

	}
}
