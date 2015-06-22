using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	public AudioClip myClip;
	public AudioSource audio;
	// Use this for initialization
	void Start () {
		audio.PlayOneShot (myClip);
	}
	

}
