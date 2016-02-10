using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour {
	public GameObject[] numberList, congoImage;
	public GameObject min, max, fakeStage, pauseButton, oops;
	public Canvas PauseCanvas, GameOverCanvas;
	public Text currentSumText, requiredSumText, scoreText;
	public bool interactivity;
	private bool isGameOver;
	private Vector3 minPosition, maxPosition, spawnPos;
	private List<GameObject> sumList;
	private int currentSum, requiredSum, score;

	// Use this for initialization
	void Start () {
		minPosition = min.transform.position;
		maxPosition = max.transform.position;
		InvokeRepeating("spawnNumber", 1.0f, 1.0f);
		currentSum = 0;
		requiredSum = Random.Range(1, 20);
		sumList = new List<GameObject>();
		fakeStage.SetActive(false);
		PauseCanvas.enabled = false;
		GameOverCanvas.enabled = false;
		for(int i=0; i<congoImage.Length; i++)
			congoImage[i].SetActive(false);
		oops.SetActive(false);
		interactivity = true;
		isGameOver = false;
	}

	// Update is called once per frame
	void Update () {
		currentSumText.text = currentSum.ToString();
		requiredSumText.text = requiredSum.ToString();
		scoreText.text = "Score " + score.ToString();
		if(currentSum > requiredSum)
			clearSumList();
		else if(currentSum == requiredSum)
			congoForSum();

		if(Input.GetKeyDown(KeyCode.Escape))
			togglePause();
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
		currentSum = 0;
		requiredSum = Random.Range(1, 20);
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
