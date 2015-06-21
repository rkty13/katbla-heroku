using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public GameObject BattleSelect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void change(string which) {
		Application.LoadLevel (which);
	}
}
