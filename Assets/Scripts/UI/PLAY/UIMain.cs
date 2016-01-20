using UnityEngine;
using System.Collections;

public class UIMain : MonoBehaviour {

	private UIManager instance;

	// Use this for initialization
	void Start () {
		instance = UIMenu.Load;
		instance = UIStart.Load;
	}

	void OnGUI(){
		SetUpGUIMatrix.Resolution(800,480);
		instance = instance.UI();
	}
}
