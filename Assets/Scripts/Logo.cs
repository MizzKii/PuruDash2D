using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {
	public Texture2D trueDigital, arkavis;
	void OnGUI() {
		SetUpGUIMatrix.Resolution(800,480);
		if(num == 0)
			GUI.DrawTexture(new Rect(0,0,800,480),trueDigital);
		else if(num == 1)
			GUI.DrawTexture(new Rect(0,0,800,480),arkavis);
		else
			Application.LoadLevel("HighScore");
	}
	void Start() {
		StartCoroutine("Count");
	}

	int num = 0;
	IEnumerator Count() {
		yield return new WaitForSeconds(3);
		num = 1;
		yield return new WaitForSeconds(3);
		num = 2;
	}
}
