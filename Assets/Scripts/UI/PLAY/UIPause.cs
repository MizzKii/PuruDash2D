using UnityEngine;
using System.Collections;

public class UIPause : UIManager {

	private UITexture texture;
	private UIPause(){}
	private void Start() {texture = gameObject.GetComponent<UITexture>(); }

	private static UIPause instance;
	public static UIManager Load
	{
		get{
			if(instance == null)
				instance = GameObject.Find("GUI").AddComponent<UIPause>();
			return instance;
		}
	}

	public override UIManager UI() {
		GUI.skin.button = texture.sk_Button.button;
		//
		texture.mod.DrawNum(70,25,(texture.gc.coin));
		GUI.DrawTexture(new Rect(370,20,30,30), texture.textM);
		texture.mod.DrawNum(420,20,texture.gc.score);
		GUI.DrawTexture(new Rect(680,420,50,50), texture.ico_fever);
		GUI.Label(new Rect(710, 420, 100,50), texture.gc.feverPoint.ToString() + "/5");
		//
		if(texture.gc.EndTrue){
			if(PlayerPrefs.GetInt("Revive") > 0)
				return UIRevive.Load;
			else if (PlayerPrefs.GetInt("Boot_Boom") > 0)
				return UIBonusEnd.Load;
			else
				return UIEndScore.Load;
		}else if(texture.gc.pause){
			TabSlice();
			
			GUI.DrawTexture(new Rect(0,0,800,480),texture.film);
			GUI.Box(new Rect(x - 150,0,150,480),"", texture.sk_Box.box);
			
			if(GUI.Button(new Rect(x - 150,15,150,150), texture.bt_resume)){	
				back = true;
			}else if(GUI.Button(new Rect(x - 150,170,150,150),texture.bt_sound[texture.mod.SoundIndex])){
				texture.mod.SoundCheck ();
			}else if(GUI.Button(new Rect(x - 150,(float)320.5,150,150), texture.bt_home)){
				Time.timeScale = texture.gc.speedtime;
				Application.LoadLevel("HighScore");
			}
		}else{
			if(GUI.Button(new Rect(700,0,100,100), texture.bt_pause)) {
				if (Time.timeScale == texture.gc.speedtime) {
					Time.timeScale = 0.0F;
					texture.gc.pause = true;
				}
			}
		}
		return instance;
	}

	private float x = 950;
	private bool back = false;
	void TabSlice(){
		if(!back){
			if(!back && x > 800)
				x -= 6;
			else if(!back && x < 800)
				x = 800;
		}else{
			if(x > 950) {
				texture.gc.pause = false;
				Time.timeScale = texture.gc.speedtime;
				back = false;
				x = 950;
			}else
				x += 7;
		}
	}
}
