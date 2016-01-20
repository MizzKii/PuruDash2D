using UnityEngine;
using System.Collections;

public class Main_Shop : MonoBehaviour {
	private UIManager instance;
	//private UITexture_Shop texture;

	void Start () {
		instance = UI_Shop.Load();
		//texture = gameObject.GetComponent<UITexture_Shop>();
	}
	
	void OnGUI(){
		SetUpGUIMatrix.Resolution(800,480);
		instance = instance.UI();
	}
}
