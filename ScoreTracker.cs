using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

	// Use this for initialization
	private int score;
	public Text uiScoreText;

	void Start () {
		score = 0;
		uiScoreText.text = ("Score: " + score.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addScore(int toAdd)
	{
		score += toAdd;
		uiScoreText.text = ("Score: " + score.ToString());
	}
}
