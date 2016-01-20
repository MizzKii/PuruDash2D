using UnityEngine;
using System.Collections;

public class UIMenu : UIManager {

	private UITexture texture;
	private UIMenu(){}
	private void Start(){ texture = GameObject.Find("GUI").GetComponent<UITexture>(); }

	private static UIMenu instance;
	public static UIManager Load
	{
		get
		{
			if(instance == null)
				instance = GameObject.Find("GUI").AddComponent<UIMenu>();
			return instance;
		}
	}

	private bool menu = false, back = false;
	public override UIManager UI() {
		GUI.skin.button = texture.sk_Button.button;
		if(menu){
			TabSlice();
			if(GUI.Button(new Rect(x - 237,190,100,100), texture.bt_back))
				back = true;
			GUI.Box(new Rect(x - 150,0,150,480),"",texture.sk_Box.box);
			if(GUI.Button(new Rect(x - 125,(float)12.5,100,100), texture.bt_sound[texture.mod.SoundIndex])){
				texture.mod.SoundCheck ();
			}else if(GUI.Button(new Rect(x - 125,(float)132.5,100,100),texture.bt_rank)) {
				if(!FB.IsLoggedIn)
					texture.fb.loadHighScore();
				return UIRanking.Load;
				
			}else if(GUI.Button(new Rect(x - 125,(float)252.5,100,100),texture.bt_shop)){ 
				Application.LoadLevel("Shop");
			}else { 
				if(FB.IsLoggedIn) {
					if(GUI.Button(new Rect(x - 125,(float)372.5,100,100),texture.bt_facebookOn))
						texture.fb.Logout();
				}else {
					if(GUI.Button(new Rect(x - 125,(float)372.5,100,100),texture.bt_facebook))
						texture.fb.Login();
				}
			}
		}else
			if(GUI.Button(new Rect(710,190,100,100), texture.bt_back))
				menu = true;
		return instance;
	}

	private float x = 950;
	void TabSlice(){
		if(!back && x > 800)
			x -= 6;
		else if(x < 800)
			x = 800;
		else if(back)
		if(x > 950) {
			if(menu == true)
				menu = false;
			back = false;
		}else
			x += 7;
	}
}
