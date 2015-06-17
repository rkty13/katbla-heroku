using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleSystem : MonoBehaviour {

	int playerHealthValue = 100, enemyHealthValue = 100;

	bool continueDialogue = false, moveSelected = false, somebodyWon = false;
	public GameObject dialogueBox, selfStats, enemyStats, MoveButtons;

	Text dialogueText, playerHealthText, enemyHealthText;

	string moveName = "", introText = "Test Intro Text";

	public string[][] moves = new string[][] {
		// KatFer
		new string[] {"Whip", "This is one of Ms. Fernandez's signature moves", "10"},
		new string[] {"Wrist Slap", "This is also one of Ms. Fernandez's signature moves", "20"},
		new string[] {"Bang Bang", "This is Ms. Fernandez's ultimate move", "30"},

		// Hitler
		new string[] {"Scapegoat", "In order to secure his political platform, Adolf Hitler used the Jews as a scapegoat for Germany's problems during its economic depression.", "10"},
		new string[] {"Third Reich", "", "20"},
		new string[] {"Triumph of the Will", "", "30"},

		// Mussolini
		new string[] {"Fascism", "", "10"},
		new string[] {"Black Shirts", "", "20"},
		new string[] {"Propoganda", "", "30"},

		// Nosferatu
		new string[] {"Darkness", "", "10"},
		new string[] {"Blood Suck", "I mean, he is a vampire...", "20"},
		new string[] {"Expressionism", "", "30"}
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
		playerHealthText.text = playerHealthValue.ToString();
		enemyHealthText.text = enemyHealthValue.ToString();
		if (!somebodyWon) {
			if (dialogueBox.activeSelf) {
				if (continueDialogue) {
					dialogueBox.SetActive (false);
					continueDialogue = false;
					MoveButtons.SetActive (true);
				}
			}
			if (moveSelected) {
				int move = getMoveIndex (moveName);
				int damage = 10;
				string desc = "";
				if (move >= 0) {
					Int32.TryParse(moves[move][2], out damage);
					desc = moves[move][1];
				}
				dialogueText.text = moveName + ": " + desc;
				moveSelected = false;
				MoveButtons.SetActive (false);
				dialogueBox.SetActive (true);
				enemyHealthValue -= damage;
			}
			if (playerHealthValue <= 0 || enemyHealthValue <= 0) {
				somebodyWon = true;
			}
		} else {
			dialogueText.text = "Somebody Won";
		}
	}

	private int getMoveIndex(string name) {
		for (int i = 0; i < moves.Length; i++) {
			if (moves[i][0].Equals (name)) return i;
		}
		return -1;
	}

	private int getRandomMoveIndex(int start, int end) {
		return -1;
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
