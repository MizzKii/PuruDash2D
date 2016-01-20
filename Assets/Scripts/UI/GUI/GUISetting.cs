using UnityEngine;
using System.Collections;

public class GUISetting : MonoBehaviour {
	
	private bool gameSetting = false;
	
	private float speedControl;
	private int CountSpeedUp;
	private float SpeedUp;
	
	private GameControl gameControl;
	private GamePlay gp;
	//private CharacterControl cc;
	
	public GameObject Bird,_upDown,backAttack,forwardAttack,drop;
	public GameObject Rocket,character;
	
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
		gp			= GameObject.Find("GameControl").GetComponent<GamePlay>();
		speedControl = gameControl.speedControl;
		CountSpeedUp = gp.countSpeedUp;
		SpeedUp = gp.speedUp;
	}
	private bool Setting = false;
	void OnGUI () {
		SetUpGUIMatrix.Resolution(800,480);
		GUI.color = Color.white;


		if (GUI.Button(new Rect (550,10,150,50), "GUI Setting"))
			if (Setting)
				Setting = false;
			else
				Setting = true;
		if(Setting) {
			if (GUI.Button(new Rect (400,10,150,50), "ReUpgate")){
				PlayerPrefs.SetInt("LV_Vacuum", 0);
				PlayerPrefs.SetInt("LV_CoinX2", 0);
				PlayerPrefs.SetInt("LV_JumpUp", 0);
				PlayerPrefs.SetInt("LV_Rocket", 0);
				PlayerPrefs.SetInt("LV_Ghost", 0);
				PlayerPrefs.SetInt("SELECT_CHAR", 0);
				PlayerPrefs.SetInt("SELECT_SKET", 0);
				PlayerPrefs.SetInt("CHARACTER_2", 0);
				PlayerPrefs.SetInt("SKET_2", 0);
			}
			if (GUI.Button(new Rect (250,10,150,50), "ReMission")){
				PlayerPrefs.SetInt("Jump_Count",0);
				PlayerPrefs.SetInt("LV_Mission_Jump",0);
				PlayerPrefs.SetInt("Dash_Count",0);
				PlayerPrefs.SetInt("LV_Mission_Dash",0);
				PlayerPrefs.SetInt("LV_Mission_Coin",0);
			}

			GameSetting();
			SZone();
			if (GUI.Button (new Rect (20,430,100,50), "Vacuum"))
				if(gameControl.vacuum)
					gameControl.vacuum = false;
				else {
					gameControl.vacuum = true;
				}
			if (GUI.Button (new Rect (120,430,100,50), "CoinX2"))
				if(gameControl.CoinX2)
					gameControl.CoinX2 = false;
				else
					gameControl.CoinX2 = true;
			if (GUI.Button (new Rect (220,430,100,50), "JumpH"))
				if(gameControl.jumpUp)
					gameControl.jumpUp = false;
				else
					gameControl.jumpUp = true;
			if (GUI.Button (new Rect (320,430,100,50), "Ghost"))
				if(gameControl.ghost){
					GameObject cc = GameObject.Find("CC");
					cc.name = "CharacterControl";
					gameControl.ghost = false;
				}else{
					GameObject cc = GameObject.Find("CharacterControl");
					cc.name = "CC";
					gameControl.ghost = true;
				}
		

			if (GUI.Button (new Rect (700,340,100,50), "ForwardAttack"))
				Instantiate(forwardAttack);
			if (GUI.Button (new Rect (700,390,100,50), "Bird"))
				Instantiate(Bird);
			if (GUI.Button (new Rect (700,440,100,50), "UpDown"))
				Instantiate(_upDown,new Vector3(9+Random.Range(0,3),_upDown.transform.position.y+gp.lastFlowY,-2),_upDown.transform.rotation);
			if (GUI.Button (new Rect (600,440,100,50), "Rocket"))
				Instantiate(Rocket);
		
			GUI.color = Color.red;
			GUI.Label(new Rect(150, 5, 100, 20), "Count " + gp.CountNow + "/" + gp.countSpeedUp);
			GUI.Label(new Rect(150, 20, 120, 20), "Speed " + gameControl.speedControl);
			GUI.Label(new Rect(150, 35, 120, 20), "Level " + gp.gameLevel);
			GUI.Label(new Rect(150, 50, 120, 20), "Items " + gp._randomItems);
			ShowItems();
		}
	}
	
	public Texture2D itemX2,itemVacuum,itemJump;
	public GUISkin _GUISkin;
	void ShowItems() {
		GUI.skin = _GUISkin;
		if(gameControl.CoinX2)
			GUI.DrawTexture(new Rect (145,430,50,50), itemX2);
		if(gameControl.vacuum)
			GUI.DrawTexture(new Rect (45,430,50,50), itemVacuum);
		if(gameControl.jumpUp)
			GUI.DrawTexture(new Rect (245,430,50,50), itemJump);
		if(gameControl.ghost)
			GUI.DrawTexture(new Rect (345,430,50,50), itemJump);
	}
	
	void GameSetting () {
		if (GUI.Button (new Rect (20,10,100,25), "Game Control")) {
			if (gameSetting)
				gameSetting = false;
			else
				gameSetting = true;
		}
		
		if (gameSetting) {
			GUI.Label(new Rect(20, 35, 120, 20), "Speed " + gameControl.speedControl);
			gameControl.speedControl = GUI.HorizontalSlider(new Rect(120, 55, 100, 30), gameControl.speedControl, 0.0F, 5.0F);
			
			GUI.Label(new Rect(20, 65, 100, 20), "Count " + gp.countSpeedUp);
			gp.countSpeedUp = (int)GUI.HorizontalSlider(new Rect(120, 85, 100, 30), gp.countSpeedUp, 1, 10);
			
			GUI.Label(new Rect(20, 95, 120, 20), "Speed UP " + gp.speedUp);
			gp.speedUp = GUI.HorizontalSlider(new Rect(120, 115, 100, 30), gp.speedUp, 1F, 2F);
			
			if (GUI.Button (new Rect (20,140,100,25), "Default")) {
				gameControl.speedControl = speedControl;
				gp.countSpeedUp = CountSpeedUp;
				gp.speedUp = SpeedUp;
			}
		}
	}

	public GameObject rocketFever;
	void SZone() {
		if(GUI.Button(new Rect(650,50,150,50), "Wall")) {
			gp.CountNow = gp.countSpeedUp;
			gp.eventSet = 0;
		}
		else if(GUI.Button(new Rect(650,100,150,50), "Puzzle")) {
			gp.CountNow = gp.countSpeedUp;
			gp.eventSet = 1;
		}
		/*else if(GUI.Button(new Rect(650,150,150,50), "Bear Attack")) {
			gp.CountNow = gp.countSpeedUp;
			gp.eventSet = 2;
		}*/else if(GUI.Button(new Rect(650,150,150,50),"Throwthing")) {
			gp.CountNow = gp.countSpeedUp;
			gp.eventSet = 2;
		}
		else if(GUI.Button(new Rect(500,150,150,50), "Fever")) {
			gameControl.feverPoint = 5;
			gameControl.fever = true;
			Instantiate(rocketFever);
			character.transform.name = "CC";
			//if(gameControl.speedtime < 2)
			//	Time.timeScale = 2;
		}
	}
}
