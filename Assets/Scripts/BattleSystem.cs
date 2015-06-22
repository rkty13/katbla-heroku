using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleSystem : MonoBehaviour {

	public AudioClip dignity, fire, honesty, ohio;
	
	int playerHealthValue = 100, enemyHealthValue = 100;

	bool continueDialogue = false, moveSelected = false, playerMove = true, enemyMove = false, somebodyWon = false;
	public GameObject dialogueBox, selfStats, enemyStats, MoveButtons;

	Text dialogueText, playerHealthText, enemyHealthText;

	string moveName = "", introText = "A challenger has appeared!";

	public AudioSource audio;
	public AudioClip xClip;

	public string[][] moves = new string[][] {
		// KatFer
		new string[] {"Whip", "This is one of Ms. Fernandez's signature moves", "10"},
		new string[] {"Wrist Slap", "A metaphorical slap on the wrist!", "20"},
		new string[] {"Bang Bang", "This is Ms. Fernandez's ultimate move", "30"},

		// Hitler
		new string[] {"Scapegoat", "In order to secure his political platform, Adolf Hitler used the Jews as a scapegoat for Germany's problems during its economic depression.", "10"},
		new string[] {"Third Reich", "The Rise of Nazi Germany. Hitler's Pride joy.", "20"},
		new string[] {"Triumph of the Will", "The Ultimate Trump Card. This is a propaganda Film chronicling the Nazi Party and its rise to power.", "30"},

		// Mussolini
		new string[] {"Fascism", "Through strong feelings of Nationalism, Mussolini is able to take over Italy and lash out at other countries.", "10"},
		new string[] {"Black Shirts", "Mussolini has sent out his personal Goons to deal with you. They hurt alot.", "20"},
		new string[] {"Propoganda", "Using Propaganda increased Mussolini's popularity and transformed him into the embodiment of Italy itself. This is his ultimate move.", "30"},

		// Nosferatu
		new string[] {"Darkness", "Vampires thrive in the night... And you have been caught!", "10"},
		new string[] {"Blood Suck", "I mean, he is a vampire...", "20"},
		new string[] {"Expressionism", "Kek", "30"}
	};

	int [][] moveSection = new int[][] { 
		new int[] {3, 5},
		new int[] {6, 8},
		new int[] {9, 11}
	};

	string [] enemyNames = new string[] {"Hitler", "Mussolini", "Nosferatu"};

	AudioClip[] winningSounds, losingSounds;

	// Use this for initialization
	void Awake () {

		MoveButtons.SetActive (false);

		dialogueText = dialogueBox.GetComponentInChildren<Text>();
		playerHealthText = selfStats.GetComponentInChildren<Text>();
		enemyHealthText = enemyStats.GetComponentInChildren<Text>();

		playerHealthText.text = "Fernandez's Health:\n" + playerHealthValue.ToString();
		enemyHealthText.text = enemyNames[Application.loadedLevel-1] + "'s Health:\n" + enemyHealthValue.ToString();

		dialogueText.text = introText;

		winningSounds = new AudioClip[] { fire, ohio };
		losingSounds = new AudioClip[] { dignity, honesty };
	}
	
	// Update is called once per frame
	void Update () {
		playerHealthText.text = "Fernandez's Health:\n" + playerHealthValue.ToString();
		enemyHealthText.text = enemyNames[Application.loadedLevel-1] + "'s Health:\n" + enemyHealthValue.ToString();
		if (!somebodyWon) {
			if (dialogueBox.activeSelf) {
				if (continueDialogue) {
					dialogueBox.SetActive (false);
					continueDialogue = false;
					MoveButtons.SetActive (true);
				}
			}
			else {
				if (moveSelected && playerMove) {
					MoveButtons.SetActive (false);
					int move = getMoveIndex (moveName);

					//audio = gameObject.AddComponent<AudioSource>();

					audio.PlayOneShot(xClip);

					int damage = 10;
					string desc = "";
					if (move >= 0) {
						Int32.TryParse(moves[move][2], out damage);
						desc = moves[move][1];
					}
					dialogueText.text = moveName + ": " + desc;
					moveSelected = false;
					enemyMove = true;
					playerMove = false;
					MoveButtons.SetActive (false);
					dialogueBox.SetActive (true);
					enemyHealthValue -= damage;
				} else if (enemyMove) {
					int move = getRandomMoveIndex (moveSection[Application.loadedLevel-1][0], moveSection[Application.loadedLevel-1][1]);
					int damage = 10;
					string desc = "";
					if (move >= 0) {
						Int32.TryParse (moves[move][2], out damage);
						desc = moves[move][1];
					}
					moveName = moves[move][0];
					dialogueText.text = "Enemy used " + moveName + ": " + desc;
					playerMove = true;
					enemyMove = false;
					MoveButtons.SetActive (false);
					dialogueBox.SetActive (true);
					playerHealthValue -= damage;
				}
				if (playerHealthValue <= 0 || enemyHealthValue <= 0) {
					somebodyWon = true;
				}
			}
		} else {
			dialogueText.text = (playerHealthValue<=0?enemyNames[Application.loadedLevel-1]:"Ms. Fernandez") + " Won!";
			if (playerHealthValue<=0){
				AudioSource audio = new AudioSource();
				audio.PlayOneShot(losingSounds[UnityEngine.Random.Range(0, 1)]);
			} else {
				AudioSource audio = new AudioSource();
				audio.PlayOneShot(winningSounds[UnityEngine.Random.Range(0, 1)]);
			}
		}
	}

	private int getMoveIndex(string name) {
		for (int i = 0; i < moves.Length; i++) {
			if (moves[i][0].Equals (name)) return i;
		}
		return -1;
	}

	private int getRandomMoveIndex(int start, int end) {
		System.Random r = new System.Random ();
		return r.Next (start, end);
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
