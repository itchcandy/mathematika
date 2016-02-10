using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using FileHandlingNamespace;

public class GameControllerScript : MonoBehaviour {
	public GameObject[] numberList, congoImage;
	public GameObject min, max, fakeStage, pauseButton, oops;
	public Canvas PauseCanvas, GameOverCanvas;
	public Text currentSumText, requiredSumText, scoreText, finalScore;
	public bool interactivity;
	private bool isGameOver;
	private Vector3 minPosition, maxPosition, spawnPos;
	private List<GameObject> sumList;
	private int currentSum, requiredSum, score;
	private Configuration cfg;

	// Use this for initialization
	void Start () {
		minPosition = min.transform.position;
		maxPosition = max.transform.position;
		InvokeRepeating("spawnNumber", 1.0f, 1.0f);
		currentSum = 0;
		mathExpression();
		sumList = new List<GameObject>();
		fakeStage.SetActive(false);
		PauseCanvas.enabled = false;
		GameOverCanvas.enabled = false;
		for(int i=0; i<congoImage.Length; i++)
			congoImage[i].SetActive(false);
		oops.SetActive(false);
		interactivity = true;
		isGameOver = false;
		cfg = Settings.ReadConf();
		Time.timeScale = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		currentSumText.text = currentSum.ToString();
		scoreText.text = "Score " + score.ToString();
		if(currentSum > requiredSum)
			clearSumList();
		else if(currentSum == requiredSum)
			congoForSum();

		if(Input.GetKeyDown(KeyCode.Escape))
			togglePause();

		if(cfg.isScoreHigher(score)){
			cfg.updateHighScore(score);
			Settings.UpdateConf(cfg);
		}
	}

	void spawnNumber(){
		int spawnIndex = Random.Range(0, numberList.Length);
		float pos = Random.Range(minPosition.x, maxPosition.x);
		spawnPos = new Vector3(pos, maxPosition.y, maxPosition.z);
		Instantiate(numberList[spawnIndex], spawnPos, max.transform.rotation);
	}

	public int addToSumList(GameObject obj, int n){
		sumList.Add(obj);
		currentSum += n;
		return sumList.Count - 1;
	}

	public void removeFromSumList(GameObject obj, int n){
		for(int i=0; i<sumList.Count; i++){
			if(sumList[i].Equals(obj)){
				sumList[i]=null;
				currentSum-=n;
				break;
			}
		}
	}

	void clearSumList(){
		for(int i=0; i<sumList.Count; i++){
			if(sumList[i] != null){
				SpriteRenderer[] s = sumList[i].gameObject.GetComponentsInChildren<SpriteRenderer>();
				s[1].enabled = !s[1].enabled;
			}
		}
		StartCoroutine(hideOops(1.0f));
		sumList.Clear();
		currentSum = 0;
	}

	void congoForSum(){
		score += sumList.Count*10 + 120;
		for(int i=0; i<sumList.Count; i++){
			if(sumList[i] != null){
				Destroy(sumList[i]);
			}
		}
		StartCoroutine(hideCongo(1.0f));
		mathExpression();
		currentSum = 0;
	}

	IEnumerator hideCongo(float f){
		int n = Random.Range(0, congoImage.Length);
		congoImage[n].SetActive(true);
		yield return new WaitForSeconds(f);
		congoImage[n].SetActive(false);
	}

	IEnumerator hideOops(float f){
		oops.SetActive(true);
		yield return new WaitForSeconds(f);
		oops.SetActive(false);
	}

	void mathExpression(){
		int x = Random.Range(0,26);
		if(x<8) numbExpression();
		else if(x<14) subtExpression();
		else if(x<19) addiExpression();
		else if(x<23) diviExpression();
		else multExpression();
	}

	void numbExpression(){
		requiredSum = Random.Range(1, 25);
		requiredSumText.text = requiredSum.ToString();
	}

	void addiExpression(){
		int a = Random.Range(8, 15);
		int b = Random.Range(0, 20-a);
		requiredSum = a+b;
		requiredSumText.text = a.ToString() + "+" + b.ToString();
	}

	void subtExpression(){
		int a = Random.Range(3, 30);
		int b = Random.Range(0, a);
		requiredSum = a-b;
		requiredSumText.text = a.ToString() + "-" + b.ToString();
	}

	void diviExpression(){
		int a = Random.Range(8, 100);
		int b = Random.Range(2, a);
		while(a%b != 0){
			b++;
		}
		requiredSum = a/b;
		requiredSumText.text = a.ToString() + "/" + b.ToString();
	}

	void multExpression(){
		int a = Random.Range(2, 10);
		int b = Random.Range(2, 10);
		requiredSum = a*b;
		requiredSumText.text = a.ToString() + "x" + b.ToString();
	}

	public void togglePause(){
		if(isGameOver)
			return;
		
		if(Time.timeScale > 0.0f){
			Time.timeScale = 0.0f;
			fakeStage.SetActive(true);
			PauseCanvas.enabled = true;
			pauseButton.SetActive(false);
			interactivity = false;
		}
		else{
			Time.timeScale = 1.0f;
			fakeStage.SetActive(false);
			PauseCanvas.enabled = false;
			pauseButton.SetActive(true);
			interactivity = true;
		}
	}

	public void gameOver(){
		finalScore.text = "Your score : " + score.ToString();
		if(cfg.isScoreHigher(score)){
			cfg.updateHighScore(score);
			Settings.UpdateConf(cfg);
		}

		Time.timeScale = 0.0f;
		fakeStage.SetActive(true);
		GameOverCanvas.enabled = true;
		isGameOver = true;
	}

	public void ClickHome(){
		SceneManager.LoadScene("Home");
	}

	public void ClickPlay() {
		SceneManager.LoadScene("Arena");
	}
}
