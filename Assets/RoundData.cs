using System.Collections;
using UnityEngine;
using System;


[System.Serializable]
public class RoundData 
{
	public string date;
	public int totalCorrectCombinations { get; set; }
	public int totalWrongCombintations { get; set; }
	public double meanReaktionTime { get; set; }

}
