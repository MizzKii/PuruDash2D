using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public float speedtime = 1F;
	public float speedControl = 1F;
	public float stopCharacterX = -5F;
	
	public int feverPoint = 0;
	[HideInInspector]public int highScore = 0;
	[HideInInspector]public int score = 0;
	[HideInInspector]public int coin = 0;
	[HideInInspector]public int coinAll = 0;
	[HideInInspector]public bool pause = false;
	
	[HideInInspector]public bool eventRun = false;
	[HideInInspector]public bool EndTrue = false;
	private GamePlay gp;
	
	//items
	[HideInInspector]public bool vacuum = false;
	[HideInInspector]public bool CoinX2 = false;
	[HideInInspector]public bool Rocket = false;
	[HideInInspector]public bool jumpUp = false;
	[HideInInspector]public bool ghost = false;

	[HideInInspector]public bool fever = false;
	
	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		highScore = PlayerPrefs.GetInt("HighScore");
		coinAll = PlayerPrefs.GetInt("Coin");
		gp = gameObject.GetComponent<GamePlay>();
		SetSpeed(speedtime);
	}
	
	// Update is called once per frame
	void Update () {
		if (!gp.PatterOn || gp.Bonus)
			gp.Pattern();
		else if (!EndTrue && !pause && (gp.checkrungame || Rocket)){
			Score();
		}
	}
	
	private int _fi;
	void Score() {
		if(_fi >= 20 - speedControl * 10) {
			_fi = 0;
			score++;
		}_fi++;
	}

	public void EndGame() {
		if(score > highScore)
			SaveScore();
		AddCoin();
		if(FB.IsLoggedIn)
			GameObject.Find("GUI").GetComponent<GUIFB>().SaveScore(highScore);
	}
	
	public float FlowSpeed = 6;
	public GameObject character;
	private bool run = false;
	public float SpeedFlow() {
		if (EndTrue && speedControl == 0F)
			run = false;
		else if(character.transform.position.x >= stopCharacterX - 0.1f)
			run = true;
		
		if(run)
			return FlowSpeed * speedControl;
		else
			return 0F;
	}
	
	private void SaveScore(){
		PlayerPrefs.SetInt("HighScore",score);
		highScore = PlayerPrefs.GetInt("HighScore");
	}
	
	public void AddCoin(){
		coinAll = coin + PlayerPrefs.GetInt("Coin");
		PlayerPrefs.SetInt("Coin",coinAll);
		coin = 0;
	}

	public void SetSpeed(float time) {
		speedtime = time;
		Time.timeScale = speedtime;
	}

	private void FeverMode() {
		if(character.transform.position.y < 0) {
			Vector3 pos = character.transform.position;
			pos.y = 0;
			character.transform.position = Vector3.Slerp(character.transform.position, pos, 0.05F);
		}
	}
}
