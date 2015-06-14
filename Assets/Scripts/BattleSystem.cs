using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleSystem : MonoBehaviour {

	int playerHealthValue = 100, enemyHealthValue = 100;

	bool continueDialogue = false, moveSelected=false, somebodyWon = false;
	public GameObject dialogueBox, selfStats, enemyStats, MoveButtons;

	Text dialogueText, playerHealthText, enemyHealthText;

	string moveName = "", introText = "Test Intro Text";

	public string[][] moves = new string[][] {
		// KatFer
		new string[] {"Whip", "10"},
		new string[] {"Slap on the Wrist", "20"},
		new string[] {"Bang Bang", "30"},

		// Hitler
		new string[] {"Scapegoat", "10"},
		new string[] {"Third Reich", "20"},
		new string[] {"Triumph of the Will", "30"},

		// Mussolini
		new string[] {"Fascism", "10"},
		new string[] {"Black Shirts", "20"},
		new string[] {"Propoganda", "30"},

		// Nosferatu
		new string[] {"Darkness", "10"},
		new string[] {"Blood Suck", "20"},
		new string[] {"Expressionism", "30"}
	};

	// Use this for initialization
	void Awake () {

		MoveButtons.SetActive (false);

		dialogueText = dialogueBox.GetComponentInChildren<Text>();
		playerHealthText = selfStats.GetComponentInChildren<Text>();
		enemyHealthText = enemyStats.GetComponentInChildren<Text>();

		playerHealthText.text = playerHealthValue.ToString();
		enemyHealthText.text = enemyHealthValue.ToString();

		dialogueText.text = introText;
	}
	
	// Update is called once per frame
	void Update () {
		if (!somebodyWon) {
			if (playerHealthValue<=0 || enemyHealthValue<=0) somebodyWon = true;
			playerHealthText.text = playerHealthValue.ToString();
			enemyHealthText.text = enemyHealthValue.ToString();
			if(dialogueBox.activeSelf) {
				if(continueDialogue) {
					dialogueBox.SetActive (false);
					continueDialogue = false;
					MoveButtons.SetActive (true);
				}
			}
			if(moveSelected) {
				dialogueText.text = moveName;
				moveSelected = false;
				MoveButtons.SetActive (false);
				dialogueBox.SetActive (true);
				enemyHealthValue-=10;
			}
		}
	}

	public void setContinueDialogue(bool val) {
		continueDialogue = val;
	}

	public void setMoveSelected(bool val) {
		moveSelected = val;
	}

	public void setMoveName(string name) {
		moveName = name;
	}
}
