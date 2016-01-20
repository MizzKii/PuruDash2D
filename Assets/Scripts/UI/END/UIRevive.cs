using UnityEngine;
using System.Collections;

public class UIRevive : UIManager {

	private UITexture texture;
	private UIRevive() {}
	private bool _start = false;
	private void Start() { 
		texture = gameObject.GetComponent<UITexture>(); 
		speedEnd = texture.gc.speedControl; 
		texture.gc.speedControl = 0; 
		revive = PlayerPrefs.GetInt("Revive"); 
		_start = true;
	}

	private static UIRevive instance;
	public static UIManager Load {
		get {
			if(instance == null)
				instance = GameObject.Find("GUI").AddComponent<UIRevive>();
			return instance;
		}
	}

	private bool IsRevive = false;
	public override UIManager UI() {
		if(_start){
		texture.mod.DrawNum(380,220,count);
		if(GUI.Button(new Rect(325,255,150,100),texture.bt_play)){
			texture.mod.StartCoroutine("PlayAgain",speedEnd);
			PlayerPrefs.SetInt("Revive",--revive);
			IsRevive = false;
			count = 3;
			return UIPause.Load;
		}GUI.Label(new Rect(350,255,100,100),"Revive X"+revive, texture.sk_LabelFont25.label);

		if(!IsRevive)
			Revive();
		else
		{
			IsRevive = false;
			if(PlayerPrefs.GetInt("Boot_Boom") > 0)
				return UIBonusEnd.Load;
			else
				return UIEndScore.Load;
		}

		}
		return instance;
	}

	private float speedEnd;
	private int count = 3, revive = 0, num = 0;
	private void Revive() {
		if(count == 0) {
			count = 3;
			IsRevive = true;
			texture.gc.EndGame();
		}else if(num++ > 80) {
			count--;
			num = 0;
		}
	}
}
