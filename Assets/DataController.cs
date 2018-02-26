using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;


public class DataController : MonoBehaviour {

	public static DataController control;

	public ProfileData profile = new ProfileData();

	public string profileName; 
	public FileStream fileStream;

	private const string path = @"c:\temp\MyTest.txt";


	void Awake () {
		if (control == null){
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != null) {
			Destroy (gameObject);
		}
	}

	void Start() {
		if (File.Exists (path)) {
			this.Load ();
		}
	}


	private static void AddText(FileStream fs, string value)
	{
		byte[] info = new UTF8Encoding(true).GetBytes(value);
		fs.Write(info, 0, info.Length);
	}


	public void Save(int correctCombinations, int wrongCombinations) {

		profile.AddProfile ("Dennis", 2);
		profile.AddRound ("Dennis", 10, 10);

		BinaryFormatter bf = new BinaryFormatter ();
		if (!File.Exists (path)){
			FileStream file = File.Create (path);
			bf.Serialize (file, profile);
			file.Close ();
		} else {
			FileStream file = File.Open (path, FileMode.Open);
			bf.Serialize (file, profile);
			file.Close ();
		}
	
		Debug.Log("Profile Data: " + profile.ToString ());
		Debug.Log("Successfully saved ProfileData!");


		HistoryData history = DataController.control.profile.getAllRoundsFromProfile ("Dennis");
		if (history != null){
			foreach(RoundData round in history) {
				Debug.Log (string.Format ("Round#: " + round.date));
			}
		}

	}

	public void Load ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (path, FileMode.Open);
		this.profile = (ProfileData)bf.Deserialize (file);
		Debug.Log ("Successfully loaded all Profiles!");
		file.Close ();	
	}


}
