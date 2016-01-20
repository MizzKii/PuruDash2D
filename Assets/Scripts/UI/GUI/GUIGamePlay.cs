using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GUIGamePlay : MonoBehaviour {
	
	public GUISkin			  	_guiSkin;
	public Texture2D 			Film, logo, icon, Resume, ranking, Home, Pause, rocket, Shop, Facebook, FacebookOn, Menu, Play, textCoin, textM, feverIcon, tapToPlay ,MenuBack, backPic, box436x100, box120x50, rankingPic;
	public Texture2D[] 			Sound, count;
	public Rect 				rCoin;
	private GameControl 		gameControl;
	private GamePlay 			gp;
	private int 				SoundIndex;
	private bool 				menu = false, missionTrue = false, start = true;
	private float 				x = 950;
	private bool 				back = false, check = false, ready = false, checkTapToPlay = false;


	/// scroll ////// scroll ////// scroll ////// scroll ////// scroll ////// scroll ////// scroll ////// scroll ////// scroll ///
	public Vector2 				scrollPosition = Vector2.zero;
	public GUIStyle 			style;
	public Vector2 				scrollPosition2 = Vector2.zero;
	public GUIStyle 			style2;
	/// scroll ////// scroll ////// scroll ////// scroll ////// scroll ////// scroll ////// scroll ////// scroll ////// scroll ///


	//Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking//
	public GUISkin 				text, text2;
	public Texture2D 			box, box2, invite, home, ranking2, picNull;
//	private bool 				checkRanking = true;
	//private bool 				checkUIRanking = false;
	//Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking////Ranking//

	//FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook//
	public bool 				checkFacebook = false, checkLogIn = true, checkLogOut = false;
	//FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook////FaceBook//

	private Modifier 			mod;
	//StartItems
	public GameObject			Rocket;
	private int 				boot_rocket;
	public GameObject 			COIN;
	private bool 				_coin = false;

	void Awake() {
		if(PlayerPrefs.GetInt("AGAIN") == 1)
			start = false;
	}

	private GUIFB _fb;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
		gp 			= GameObject.Find("GameControl").GetComponent<GamePlay>();
		mod 		= gameObject.GetComponent<Modifier>();
		boot_rocket = PlayerPrefs.GetInt("Boot_Rocket");
		if(AudioListener.volume > 0){
			SoundIndex = 0;
		}else{
			SoundIndex = 1;
		}
		_fb = gameObject.GetComponent<GUIFB>();
	}

	void OnGUI() {
		SetUpGUIMatrix.Resolution(800,480);
		GUI.skin = _guiSkin;
		if(!gameControl.EndTrue) {
			if(gp.checkrungame || gameControl.Rocket) {
				if(!missionTrue)
					PauseShow();
			}else if(!gameControl.Rocket){
				if(ready) {
					if(boot_rocket > 0)
					if(GUI.Button(new Rect(600,0 + 150+Mathf.PingPong(Time.time*50,50F), 150, 150), rocket)){ 
						gameControl.Rocket = true; 
						gp.checkrungame=true; 
						Instantiate(Rocket); 
						PlayerPrefs.SetInt("Boot_Rocket", --boot_rocket);
					}
					GUI.DrawTexture(new Rect(280,175,250,150),count[countDown]);
				}else if(!missionTrue)
					MenuShow();
			}
		}
	}

	void PauseShow() {

		mod.DrawNum(70,25,(gameControl.coin));
		if(!_coin){
			COIN.SetActive(true);
			_coin = true;
		}

		GUI.DrawTexture(new Rect(370,20,30,30), textM);
		mod.DrawNum(420,20,gameControl.score);

		GUI.DrawTexture(new Rect(680,420,50,50), feverIcon);
		GUI.Label(new Rect(710, 420, 100,50), gameControl.feverPoint.ToString() + "/5");

		if(gameControl.pause){
			TabSlice();

			GUI.DrawTexture(new Rect(0,0,800,480),Film);
			GUI.Box(new Rect(x - 150,0,150,480),"");

			if(GUI.Button(new Rect(x - 150,15,150,150),Resume)){
				check = true;		
				back = true;
			
			}else if(GUI.Button(new Rect(x - 150,170,150,150),Sound[SoundIndex])){
				SoundCheck ();
			}else if(GUI.Button(new Rect(x - 150,(float)320.5,150,150),Home)){
				back = true;
			}
		}else{
			if(GUI.Button(new Rect(700,0,100,100), Pause)) {
				if (Time.timeScale == gameControl.speedtime) {
					Time.timeScale = 0.0F;
					gameControl.pause = true;
				}
			}
		}
	}

	void SoundCheck(){
		if(AudioListener.volume > 0) {
			SoundIndex = 1;
			AudioListener.volume = 0;
		}else{
			SoundIndex = 0;
			AudioListener.volume = 1;
		}
		
	}

	private int countDown;
	IEnumerator Ready(){

		countDown = 1;
		yield return new WaitForSeconds(2F);
		countDown = 0; 
		yield return new WaitForSeconds(1.5F);
		if(!gameControl.Rocket)
			gp.checkrungame=true; 
		ready = false;

	}

	void MenuShow() {
		if(menu){
			TabSlice();
			if(checkTapToPlay){
				if(GUI.Button(new Rect(0,0,550,480), Play)){
					StartCoroutine("Ready");
					down = true;
					start = false;
					back = true;
				}
			}
			
			checkTapToPlay = true;

			if(start)
				GUI.DrawTexture(new Rect(185,50,429,250), logo);
			
			
			if(GUI.Button(new Rect(x - 237,190,100,100), MenuBack))back = true;
			GUI.Box(new Rect(x - 150,0,150,480),"");
			if(GUI.Button(new Rect(x - 125,(float)12.5,100,100), Sound[SoundIndex])){
				SoundCheck ();
			}else if(GUI.Button(new Rect(x - 125,(float)132.5,100,100),ranking)) {
				if(!FB.IsLoggedIn)
					_fb.Login();
				else
					_fb.loadHighScore();
				//checkUIRanking = true;
				checkFacebook = false;
				
			}else if(GUI.Button(new Rect(x - 125,(float)252.5,100,100),Shop)){ 
				Application.LoadLevel("Shop");
			}else { 
				if(FB.IsLoggedIn) {
					if(GUI.Button(new Rect(x - 125,(float)372.5,100,100),FacebookOn))
						_fb.Logout();
				}else {
					if(GUI.Button(new Rect(x - 125,(float)372.5,100,100),Facebook))
						_fb.Login();
				}
			}
		}else if(gp.iLoad == 100){
			if(down)
				SliceDown();
			if(start){
				GUI.DrawTexture(new Rect(185,50,429,250), logo);

				GUI.DrawTexture(new Rect(200,350,400,60), tapToPlay);
				if(GUI.Button(new Rect(x- 240,190,100,100), Menu)) menu = true;
				else if(GUI.Button(new Rect(0,0,800,480), Play)){
					StartCoroutine("Ready");
					down = true;
					start = false;
				}
				GUI.Box(new Rect(x- 160,0,150,480),"");
			}

		}
	}
	float y = 0;
	bool down = false;
	void SliceDown(){
		if(y < 350)
			y += 5;
		else {
			down = false;
			ready = true;
		}
	}

	void TabSlice(){
		if(!back && x > 800)
			x -= 6;
		else if(x < 800)
			x = 800;
		else if(back)
		if(x > 950) {
			if(menu == true)
				menu = false;
			else if(check == true){
				gameControl.pause = false;
				Time.timeScale = gameControl.speedtime;
				check = false;
			}else{
				Application.LoadLevel("HighScore");
				Time.timeScale = gameControl.speedtime;
			}
			back = false;
		}else
			x += 7;
	}
}
