using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GameObject[] numberList;
	public GameObject min, max;
	public Text currentSumText, requiredSumText;
	private Vector3 minPosition, maxPosition, spawnPos;
	private List<GameObject> sumList;
	private int currentSum, requiredSum;
	// Use this for initialization
	void Start () {
		minPosition = min.transform.position;
		maxPosition = max.transform.position;
		InvokeRepeating("spawnNumber", 1.0f, 1.0f);
		currentSum = 0;
		requiredSum = Random.Range(1, 20);
	}

	// Update is called once per frame
	void Update () {
		currentSumText.text = currentSum.ToString();
		requiredSumText.text = requiredSum.ToString();
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
}
