using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ProfileData: List<HistoryData> 
{

	public void AddProfile(string profileName, int historyId) {
		if (this.Find (delegate (HistoryData item) {return item.HistoryName == profileName; }) == null ){
			this.Add (new HistoryData() {HistoryName = profileName, HistoryId = historyId});
		}
	}

	public void AddRound(string historyName, int correctCombinations, int wrongCombinations){
		if(this.Find (delegate (HistoryData item) {return item.HistoryName == historyName; }) != null){
			this.Find (delegate (HistoryData item) {return item.HistoryName == historyName; }).Add (correctCombinations, wrongCombinations, 0.8, System.DateTime.Now.ToString ());
		}
		Debug.Log (string.Format ("Added following Line : " + correctCombinations + "|" + wrongCombinations + "Time" + System.DateTime.Now.ToString () + "for Profile: " + historyName));

	}

	public HistoryData getAllRoundsFromProfile(string profileName) {
		HistoryData history = this.Find (delegate (HistoryData item) {return item.HistoryName == profileName; });
		if(history != null){
			return history;
		}
		return null;
	}

}
