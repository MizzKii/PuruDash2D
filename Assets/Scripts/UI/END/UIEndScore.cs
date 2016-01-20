using UnityEngine;
using System.Collections;

public class UIEndScore : UIManager {

	private UITexture texture;
	private UIEndScore () {}
	private bool _start = false;
	private void Start () { texture = gameObject.GetComponent<UITexture>(); texture.gc.speedControl = 0; _start = true;}

	private static UIEndScore instance;
	public static UIManager Load
	{
		get{
			if(instance == null)
				instance = GameObject.Find("GUI").AddComponent<UIEndScore>();
			return instance;
		}
	}

	private bool share = false;
	public override UIManager UI() {
		if(_start){
		GUI.skin = texture.sk_Button;
		GUI.DrawTexture(new Rect(0, 0, 800, 480), texture.film);
		GUI.DrawTexture(new Rect(100,10,600,460), texture._score_BG01);
		GUI.DrawTexture(new Rect(145,55,500,300), texture._score_BG02);
		GUI.DrawTexture(new Rect(170,140,450,200), texture._score_BG03);
		GUI.Label(new Rect(275,130,250,50), "MISSION");
		GUI.DrawTexture(new Rect(180,75,150,60), texture._score_BG03);
		GUI.Label(new Rect(100,70,250,50), "Score " + texture.gc.score);
		GUI.Label(new Rect(200,105,250,50), "Best Score " + texture.gc.highScore, texture.sk_LabelFont30.label);
		
		if(texture.gc.score >= texture.gc.highScore){
			GUI.Label(new Rect(350,70,300,50), "NEW HIGHSCORE!",texture.sk_LabelFont40B.label);
		}
		
		if(GUI.Button(new Rect(155,360,80,80), texture.bt_home))Application.LoadLevel("HighScore");
		else if(GUI.Button(new Rect(255,355,160,100), texture.bt_length_shop))Application.LoadLevel("Shop");
		else if(GUI.Button(new Rect(450,360,200,100), texture.bt_playAgain)) {
			PlayerPrefs.SetInt("AGAIN", 1);
			Application.LoadLevel("HighScore");
		}else if(GUI.Button(new Rect(555,70,70,70), texture.bt_share)){
			if(FB.IsLoggedIn){
				texture.fb.StartCoroutine("TakeScreenshot");
			}else
				texture.fb.Login();
		}UIMission.Load.UI ();
		if(share) {
			GUI.DrawTexture(new Rect(0, 0, 800, 480), texture.film);
			GUI.DrawTexture(new Rect(250,140,300,200), texture._score_BG01);
			GUI.Label(new Rect(345,180,100,50), "Shared!", texture.sk_LabelFont70B.label);
			if(GUI.Button(new Rect(350,250,100,55),texture.BoxSelectShopYeShot)) share = false;
			GUI.Label(new Rect(345,250,100,50),"OK.",texture.sk_LabelFont40.label);
		}
		}
		return instance;
	}
}
