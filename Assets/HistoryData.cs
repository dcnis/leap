using System.Collections;
using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class HistoryData {

	public string HistoryName { get; set; }
	public int HistoryId { get; set; }

	public List<RoundData> allRoundData;

}
