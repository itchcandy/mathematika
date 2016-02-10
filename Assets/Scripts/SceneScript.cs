using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickPlay() {
		SceneManager.LoadScene("Arena");
	}
}
