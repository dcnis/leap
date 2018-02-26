using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;


public class DataController : MonoBehaviour {

	public static DataController control;

	List<HistoryData> history = new List<HistoryData>();

	private const string path = @"c:\temp\MyTest.txt";


	void Awake () {
		if (control == null){
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != null) {
			Destroy (gameObject);
		}
	}


	public void SaveValue(){

		if (File.Exists (path)) {
			File.Delete (path);
		}

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (path);
		bf.Serialize (file, "Das ist ein Test");
		file.Close ();
		Debug.Log ("Successfully saved Value!");

	}
		public void LoadValue() 
	{
		if (File.Exists (path)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (path, FileMode.Open);
			string value =  (string)bf.Deserialize (file);
			file.Close ();
			Debug.Log ("Das ist der geladene Wert: " + value);
		}
	}


	private static void AddText(FileStream fs, string value)
	{
		byte[] info = new UTF8Encoding(true).GetBytes(value);
		fs.Write(info, 0, info.Length);
	}


	public void Save(int correctCombinations, int wrongCombinations) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/AllData.dat");

		history.Add (new HistoryData() {HistoryName = "Dennis", HistoryId = 1 });
		history.Find (delegate(HistoryData item) {return item.HistoryName == "Dennis"; }).allRoundData.Add (new RoundData () {
			totalCorrectCombinations = correctCombinations,
			totalWrongCombintations = wrongCombinations,
			meanReaktionTime = 0.8,
		});
				
		bf.Serialize (file, history);
		file.Close ();
		Debug.Log("Successfully saved!");
	}

	public void Load() {

		if (File.Exists (Application.persistentDataPath + "/AllData.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/AllData.dat", FileMode.Open);
			ProfileData data = (ProfileData)bf.Deserialize (file);
			file.Close ();


			Debug.Log("Successfully loaded!");
		}
	}

	//public HistoryData getHistory(int id){
		//return ProfileData [id];
	//}


}
