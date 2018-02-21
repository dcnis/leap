using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class saveHistoryController : MonoBehaviour {

	public static saveHistoryController control;

	public ProfilesData appData = new ProfilesData();

	void Awake () {
		if (control == null){
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != null) {
			Destroy (gameObject);
		}
	}

	public void Save(int correctCombinations, int wrongCombinations, int userId, int roundId) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/AllData.dat");

		ProfilesData data = new ProfilesData();

		data.allHistoryData[userId].allRoundData[roundId].totalCorrectCombinations = correctCombinations;
		data.allHistoryData[userId].allRoundData[roundId].totalWrongCombintations = wrongCombinations;
		data.allHistoryData[userId].allRoundData[roundId].meanReaktionTime = 120 / correctCombinations;

		bf.Serialize (file, data);
		file.Close ();
		Debug.Log("Successfully saved!");
	}

	public void Load() {

		if (File.Exists (Application.persistentDataPath + "/AllData.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/AllData.dat", FileMode.Open);
			ProfilesData data = (ProfilesData)bf.Deserialize (file);
			file.Close ();

			for (int history = 0; history < data.allHistoryData.Length; history++){
				for (int round = 0; round < data.allHistoryData[history].allRoundData.Length; round++){
					appData.allHistoryData[history].allRoundData[round].userId = data.allHistoryData[history].allRoundData[round].userId;
					appData.allHistoryData[history].allRoundData[round].totalCorrectCombinations = data.allHistoryData[history].allRoundData[round].totalCorrectCombinations;
					appData.allHistoryData[history].allRoundData[round].totalWrongCombintations = data.allHistoryData[history].allRoundData[round].totalWrongCombintations;
					appData.allHistoryData[history].allRoundData[round].meanReaktionTime = data.allHistoryData[history].allRoundData[round].meanReaktionTime;
				}
			}


			Debug.Log("Successfully loaded!");
		}
	}


}
