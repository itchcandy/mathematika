using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using FileHandlingNamespace;

public class SceneScript : MonoBehaviour {
	public Text hs;
	public GameObject muteSprite;
	private Configuration cfg;
	// Use this for initialization
	void Start () {
		cfg = Settings.ReadConf();
		if(hs!=null){
			hs.text = cfg.getHighScore().ToString();
		}

		if(muteSprite != null){
			if(cfg.getSoundStatus())
				muteSprite.SetActive(false);
			else
				muteSprite.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickHighscore(){
		SceneManager.LoadScene("HighScore");
	}

	public void ClickHome(){
		SceneManager.LoadScene("Home");
	}

	public void ClickInfo(){
		SceneManager.LoadScene("Info");
	}
		
	public void ClickPlay() {
		SceneManager.LoadScene("Arena");
	}

	public void ToggleSound(){
		cfg.toggleSound();
		Settings.UpdateConf(cfg);
		if(cfg.getSoundStatus())
			muteSprite.SetActive(false);
		else
			muteSprite.SetActive(true);
	}
}
