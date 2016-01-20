using UnityEngine;
using System.Collections;

public class GUIEnd : MonoBehaviour {
	
	public GUISkin _GUISkin, text30, text40;
	public Texture2D 	Film, BG_Result1, BG_Result2, BG_mission,
						Home, Shop2, playAgain2, Share, btShare;
	public Texture2D BGrevive;
	public Rect RBG, again, menu;

	private Modifier mod;
	//private bool missionTrue = false;

	private GUIFB _fb;

	//private Shop shop;
	private GameControl gameControl;
	private int boot_boom, item_revive;
	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
		mod			= gameObject.GetComponent<Modifier>();
		boot_boom	= PlayerPrefs.GetInt("Boot_Boom");
		item_revive		= PlayerPrefs.GetInt("Revive");

		_fb = gameObject.GetComponent<GUIFB>();
	}
	
	void OnGUI () {
		if(gameControl.EndTrue){
			SetUpGUIMatrix.Resolution(800,480);
			GUI.skin = _GUISkin;
			if(revive)
				if(item_revive > 0)
					StartCoroutine("Revive");
				else{
					revive = false;
					speedEnd = gameControl.speedControl;
					gameControl.EndGame();
					gameControl.speedControl = 0;
					if(boot_boom > 0)
						StartCoroutine("BonusBoom");
					else
						end = true;
						ReMission();
				}
			else if(end){
				ShowEnd();
			}else if(!bonus){
				mod.DrawNum(380,220,count);
				if(GUI.Button(new Rect(325,255,150,100),BGrevive)){
					StartCoroutine("PlayAgain");
					PlayerPrefs.SetInt("Revive",--item_revive);
				}GUI.Label(new Rect(350,255,100,100),"Revive X"+item_revive);
			}
		}
	}
	float speedEnd;
	int count;
	bool revive = true,end = false, bonus = false;
	IEnumerator Revive() {
		revive = false;
		gameControl.EndGame();
		speedEnd = gameControl.speedControl;
		gameControl.speedControl = 0;
		count = 3;
		yield return new WaitForSeconds(1);
		count = 2;
		yield return new WaitForSeconds(1);
		count = 1;
		yield return new WaitForSeconds(1);
		count = 0;
		yield return new WaitForSeconds(0.3F);
		if(gameControl.EndTrue)
			if(boot_boom > 0){
				StartCoroutine("BonusBoom");
			}else{
				end = true;
				ReMission();
				gameControl.EndGame();
			}
	}
	
	IEnumerator PlayAgain() {
		gameControl.vacuum = true;
		GameObject _character;
		if(GameObject.Find("CharacterControl") != null)
			_character = GameObject.Find("CharacterControl");
		else
			_character = GameObject.Find("CC");
		CharacterControl cc = _character.GetComponent<CharacterControl>();
		cc.AddJump(30);
		float y = _character.transform.position.y;
		if(_character.transform.position.y < -5)
			y = -5;
		_character.transform.position = new Vector3(gameControl.stopCharacterX, y, -5);
		GamePlay gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
		gp.checkrungame = false;
		gameControl.Rocket = true;
		gameControl.speedControl = 3.5F;
		gameControl.EndTrue = false;
		revive = true;

		yield return new WaitForSeconds(1F);
		_character.transform.position = new Vector3(gameControl.stopCharacterX, _character.transform.position.y, -2);
		gameControl.speedControl = speedEnd;
		yield return new WaitForSeconds(2F);
		gp.checkrungame = true;
		gameControl.Rocket = false;
		gameControl.vacuum = false;
	}
	public bool share = false;
	void ShowEnd() {
		GUI.DrawTexture(new Rect(0, 0, 800, 480), Film);
		GUI.DrawTexture(new Rect(100,10,600,460), BG_Result1);
		GUI.DrawTexture(new Rect(145,55,500,300), BG_Result2);
	    GUI.DrawTexture(new Rect(170,140,450,200), BG_mission);
		GUI.Label(new Rect(275,130,250,50), "MISSION");
		GUI.DrawTexture(new Rect(180,75,150,60), BG_mission);
		GUI.Label(new Rect(100,70,250,50), "Score " + gameControl.score);
		GUI.skin = text30;
		GUI.Label(new Rect(200,105,250,50), "Best Score " + gameControl.highScore);

		if(gameControl.score >= gameControl.highScore){
			GUI.skin = text40;
			GUI.Label(new Rect(350,70,300,50), "NEW HIGHSCORE!");
			GUI.skin = _GUISkin;
		}
		
		if(GUI.Button(new Rect(155,360,80,80), Home))Application.LoadLevel("HighScore");
		else if(GUI.Button(new Rect(255,355,160,100), Shop2))Application.LoadLevel("Shop");
		else if(GUI.Button(new Rect(450,360,200,100), playAgain2)) {
			PlayerPrefs.SetInt("AGAIN", 1);
			Application.LoadLevel("HighScore");
		}else if(GUI.Button(new Rect(555,70,70,70), Share)){
			if(FB.IsLoggedIn){
				_fb.StartCoroutine("TakeScreenshot");
			}else
				_fb.Login();
		}MissionShow();
		if(share) {
			GUI.skin = BigSkin;
			GUI.DrawTexture(new Rect(0, 0, 800, 480), Film);
			GUI.DrawTexture(new Rect(250,140,300,200), BG_Result1);
			GUI.Label(new Rect(345,180,100,50), "Shared!");
			if(GUI.Button(new Rect(350,250,100,55),btShare)) share = false;
			GUI.Label(new Rect(345,250,100,50),"OK.");
		}
	}
	public GUISkin BigSkin;

	IEnumerator BonusBoom() {
		bonus = true;
		GameObject _character;
		gameControl.vacuum = true;
		if(GameObject.Find("CharacterControl") != null)
			_character = GameObject.Find("CharacterControl");
		else
			_character = GameObject.Find("CC");
		GamePlay gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
		PlayerPrefs.SetInt("Boot_Boom", --boot_boom);
		CharacterControl cc = _character.GetComponent<CharacterControl>();
		cc.AddJump(30);
		gameControl.speedControl = speedEnd*2;
		gameControl.EndTrue = false;
		gp.checkrungame = false;
		gameControl.Rocket = true;
		float y = _character.transform.position.y;
		if(_character.transform.position.y < -5)
			y = -5;
		_character.transform.position = new Vector3(gameControl.stopCharacterX, y, -5);
		yield return new WaitForSeconds(0.5F);
		_character.transform.position = new Vector3(gameControl.stopCharacterX, _character.transform.position.y, -2);
		yield return new WaitForSeconds(2);
		gameControl.speedControl = 0;
		gameControl.vacuum = false;
		end = true;
		ReMission();
		gameControl.EndTrue = true;
		gameControl.EndGame();
	}

	///////////////////>>>>>>>>MISSION///////////////////////////////////////////////////////////

	public Texture2D	bg_MissionDetail, bg_MissionClear;
	private int 		jumpMission = 0, dashMission = 0, coinMission = 0;
	private int			jumpNum,dashNum;
	private int			page = 0;
	public void ReMission() {
		jumpNum		= PlayerPrefs.GetInt("Jump_Count");
		jumpMission = PlayerPrefs.GetInt("LV_Mission_Jump");
		dashNum = PlayerPrefs.GetInt("Dash_Count");
		dashMission = PlayerPrefs.GetInt("LV_Mission_Dash");
		coinMission = PlayerPrefs.GetInt("LV_Mission_Coin");

		page = PlayerPrefs.GetInt("PAGE_MISSION");
	}

	private int			bonusCoin = 0;
	public void MissionShow() {

		int maxjump = 0, maxcoin = 0, maxdash = 0;

		/////////////////////////Page 1/////////////////////////////////////////////////////////
		if(page == 0) {

			maxjump = 20;
			if(jumpNum >= maxjump && jumpMission < 1) {
				gameControl.coin += 100; gameControl.AddCoin(); jumpMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Jump",jumpMission);
				bonusCoin += 100;
			}

			maxcoin = 200;
			if((gameControl.coinAll+gameControl.coin) >= maxcoin && coinMission < 1) {
				gameControl.coin += 100; gameControl.AddCoin(); coinMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Coin",coinMission);
				bonusCoin += 100;
			}

			maxdash = 20;
			if(dashNum >= maxdash && dashMission < 1) {
				gameControl.coin += 100; gameControl.AddCoin(); dashMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Dash",dashMission);
				bonusCoin += 100;
			}

			if(dashMission+coinMission+jumpMission > 2) {
				page = 1;
				PlayerPrefs.SetInt("PAGE_MISSION",page);
				jumpMission = 0; dashMission = 0; coinMission = 0;
				PlayerPrefs.SetInt("LV_Mission_Jump",jumpMission);
				PlayerPrefs.SetInt("LV_Mission_Coin",coinMission);
				PlayerPrefs.SetInt("LV_Mission_Dash",dashMission);
				jumpNum = 0; dashNum = 0;
				PlayerPrefs.SetInt("Jump_Count",0);
				PlayerPrefs.SetInt("Dash_Count", 0);

				gameControl.coin += 300; gameControl.AddCoin();
				bonusCoin += 300;
			}
		}
		///////////////////////Page 2///////////////////////////////////////////////////////////
		else if(page == 1) {

			maxjump = 30;
			if(jumpNum >= maxjump && jumpMission < 1) {
				gameControl.coin += 200; gameControl.AddCoin(); jumpMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Jump",jumpMission);
				bonusCoin += 200;
			}

			maxcoin = 500;
			if((gameControl.coinAll+gameControl.coin) >= maxcoin && coinMission < 1) {
				gameControl.coin += 200; gameControl.AddCoin(); coinMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Coin",coinMission);
				bonusCoin += 200;
			}

			maxdash = 30;
			if(dashNum >= maxdash && dashMission < 1) {
				gameControl.coin += 200; gameControl.AddCoin(); dashMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Dash",dashMission);
				bonusCoin += 200;
			}

			if(dashMission+coinMission+jumpMission > 2) {
				page = 2;
				PlayerPrefs.SetInt("PAGE_MISSION",page);
				jumpMission = 0; dashMission = 0; coinMission = 0;
				PlayerPrefs.SetInt("LV_Mission_Jump",jumpMission);
				PlayerPrefs.SetInt("LV_Mission_Coin",coinMission);
				PlayerPrefs.SetInt("LV_Mission_Dash",dashMission);
				
				PlayerPrefs.SetInt("Jump_Count",0);
				PlayerPrefs.SetInt("Dash_Count", 0);
				
				gameControl.coin += 300; gameControl.AddCoin();
				bonusCoin += 600;
			}
		}
		//////////////////////Page 3////////////////////////////////////////////////////////////
		else {
			
			maxjump = 50;
			if(jumpNum >= maxjump && jumpMission < 1) {
				gameControl.coin += 500; gameControl.AddCoin(); jumpMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Jump",jumpMission);
				bonusCoin += 300;
			}
			
			maxcoin = 1500;
			if((gameControl.coinAll+gameControl.coin) >= maxcoin && coinMission < 1) {
				gameControl.coin += 500; gameControl.AddCoin(); coinMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Coin",coinMission);
				bonusCoin += 300;
			}
			
			maxdash = 50;
			if(dashNum >= maxdash && dashMission < 1) {
				gameControl.coin += 500; gameControl.AddCoin(); dashMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Dash",dashMission);
				bonusCoin += 300;
			}
			
			/*if(dashMission+coinMission+jumpMission > 2) {
				page = 3;
				PlayerPrefs.SetInt("PAGE_MISSION",page);
				jumpMission = 0; dashMission = 0; coinMission = 0;
				PlayerPrefs.SetInt("LV_Mission_Jump",jumpMission);
				PlayerPrefs.SetInt("LV_Mission_Coin",coinMission);
				PlayerPrefs.SetInt("LV_Mission_Dash",dashMission);
				
				PlayerPrefs.SetInt("Jump_Count",0);
				PlayerPrefs.SetInt("Dash_Count", 0);
				
				gameControl.coin += 1000; gameControl.AddCoin();
				bonusCoin += 900;
			}*/
		}
		//////////////////////////////////////////////////////////////////////////////////
		
		if(jumpMission < 1) {
			if(jumpNum > maxjump)
				jumpNum = maxjump;

			GUI.DrawTexture(new Rect(195,197,400,40), bg_MissionDetail);
			GUI.Label(new Rect(210,202,400,30), "Jump " + jumpNum +"/"+ maxjump+" time");
		}else {
			GUI.DrawTexture(new Rect(195,197,400,40), bg_MissionClear);
			GUI.Label(new Rect(210,202,400,30), "Mission Jump Complete.");
		}

		if(coinMission < 1) {
			GUI.DrawTexture(new Rect(195,237,400,40), bg_MissionDetail);
			GUI.Label(new Rect(210,242,400,30), "Get coin " + (gameControl.coinAll+gameControl.coin) +"/"+ maxcoin+ " coin");
		}else {
			GUI.DrawTexture(new Rect(195,237,400,40), bg_MissionClear);
			GUI.Label(new Rect(210,242,400,30), "Mission Coin Complete.");
		}

		if(dashMission < 1) {
			if(dashNum > maxdash)
				dashNum = maxdash;
			GUI.DrawTexture(new Rect(195,277,400,40), bg_MissionDetail);
			GUI.Label(new Rect(210,282,400,30), "Dash " + dashNum +"/"+ maxdash+" time");
		}else {
			GUI.DrawTexture(new Rect(195,277,400,40), bg_MissionClear);
			GUI.Label(new Rect(210,282,400,30), "Mission Dash Complete.");
		}

		//////////////////////////////////////////////////////////////////////////////////////
		if(bonusCoin > 0) {
			GUI.skin = BigSkin;
			GUI.DrawTexture(new Rect(0, 0, 800, 480), Film);
			GUI.DrawTexture(new Rect(250,140,300,200), BG_Result1);
			GUI.Label(new Rect(295,160,200,50), "Mission Clear!");
			GUI.Label(new Rect(295,200,200,50),"Bonus Coin : +"+bonusCoin.ToString());
			if(GUI.Button(new Rect(340,260,120,55),btShare)) bonusCoin = 0;
			GUI.Label(new Rect(335,260,120,50),"OK.");
		}
		//////////////////////////////////////////////////////////////////////////////////////
	}
}