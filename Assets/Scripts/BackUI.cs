using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackUI : MonoBehaviour {
	private Image img;
	private Vector2 vec;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
		vec = img.rectTransform.sizeDelta;
		vec.y = Screen.width;
		img.rectTransform.sizeDelta = vec;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
