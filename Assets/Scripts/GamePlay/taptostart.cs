/*using UnityEngine;
using System.Collections;

public class taptostart : MonoBehaviour {
	private GamePlay gp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(checkrungame){
	 		}else{
						 int i = (int)Random.Range(0F, _patternstart.Length);
							if(i != lastPattern) {
							lastPattern = i;
							_patternstart[i].SetActive(true);
							start b = _patternstart[i].GetComponent("start") as start;
							b.StartPattern();
			}
			
					Debug.Log(checkrungame);
							if(Input.GetMouseButtonDown(0)){
					Debug.Log(checkrungame);
						checkrungame=true;	
			}
		}
	}
}*/
