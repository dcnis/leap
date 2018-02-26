using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;


[System.Serializable]
public class HistoryData: List<RoundData> {

	public string HistoryName { get; set; }
	public int HistoryId { get; set; }

	public void Add(int correctCombinations, int wrongCombinations, double reactionTime, string dateNow) {
		this.Add (new RoundData() {totalCorrectCombinations = correctCombinations, totalWrongCombintations = wrongCombinations, meanReaktionTime = reactionTime, date = dateNow});
	}

}
