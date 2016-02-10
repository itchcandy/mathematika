using UnityEngine;
using System.Collections;

public class MaskVisibility : MonoBehaviour {
	private GameControllerScript GCScript;
	private int n;
	private bool onBrim = false;
	private float f;
	// Use this for initialization
	void Start () {
		SpriteRenderer[] s = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
		s[1].enabled = false;
		GCScript = (GameControllerScript)FindObjectOfType(typeof(GameControllerScript));
		switch(gameObject.tag){
			case "One" : n = 1; break;
			case "Two" : n = 2; break;
			case "Three" : n = 3; break;
			case "Four" : n = 4; break;
			case "Five" : n = 5; break;
			case "Six" : n = 6; break;
			case "Seven" : n = 7; break;
			case "Eight" : n = 8; break;
			case "Nine" : n = 9; break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(onBrim){
			if((Time.time-f)>3.0f)
				GCScript.gameOver();
		}
	}

	void OnMouseDown(){
		if(GCScript.interactivity){
			SpriteRenderer[] s = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
			if(s[1].enabled)
				GCScript.removeFromSumList(this.gameObject, n);
			else
				GCScript.addToSumList(this.gameObject, n);
			s[1].enabled = !s[1].enabled;
		}
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "Brim"){
			f = Time.time;
			onBrim = true;
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if(c.gameObject.tag == "Brim"){
			onBrim = false;
		}
	}
}
