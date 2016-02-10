using UnityEngine;
using System.Collections;

public class MaskVisibility : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer[] s = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
		s[1].enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		SpriteRenderer[] s = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
		s[1].enabled = !s[1].enabled;
	}
}
