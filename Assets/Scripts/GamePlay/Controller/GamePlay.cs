using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {
	
	public float speedUp = 1.15F;
	//[HideInInspector]
	public bool PatterOn = false,Bonus = false;
	[HideInInspector]public int	eventSet = -1;
	private int lastPattern = -1;
	[HideInInspector]public int lastFlowY = 0;
	[HideInInspector]public int gameLevel = 0;
	[HideInInspector]public int CountNow = 0;
	[HideInInspector]public int countSpeedUp;
	[HideInInspector]public bool checkrungame = false;
	
	[HideInInspector]public float nextHop = 0;

	public int noEnemyNoPatternLv0 = 5, noEnemyNoPatternLv1 = 5, noEnemyNoPatternLv2 = 5;
	private bool hardPattern = false;

	public GameObject LastP;
	public GameObject specialEvent;
	 
	
	private GameControl gameControl;
	private GameObject[] _patternstart = new GameObject[3];
	public GameObject[] patternLv0 = new GameObject[11];
	public GameObject[] patternLv1 = new GameObject[9];
	public GameObject[] patternLv2 = new GameObject[2];
 	
	public GameObject Bird,_upDown,forwardAttack,drop;
	
	//Items
	public GameObject fever;
	public GameObject[] items;
	public GameObject[] Sitems;
	[HideInInspector]
	public int _randomItems;
	private int feverCount;
	
	public float iLoad = 0;
	public bool iStart = false;
	private IEnumerator LoadPattern() {
		if(_patternstart.Length >0){
			float per = 20/_patternstart.Length;
			for(int i=0 ;i <_patternstart.Length ;i++){
				string tmp = "patternstart/";
				tmp += "patternstart_" + i.ToString();
				_patternstart[i] = Instantiate(Resources.Load(tmp,typeof(GameObject))) as GameObject;
				_patternstart[i].SetActive(false);
				iLoad += per;
			}
			
		}iLoad = 30;
		yield return new WaitForSeconds(0.2F);
		if(patternLv0.Length > 0){
			float per = 20/_patternstart.Length;
			for(int i = 0; i < patternLv0.Length ; i++){
				string tmp = "level0/";
				tmp += "Lv0_" + i.ToString();
				patternLv0[i] = Instantiate(Resources.Load(tmp, typeof(GameObject))) as GameObject;
				patternLv0[i].SetActive(false);
				iLoad += per;
			}
		}iLoad = 50;
		yield return new WaitForSeconds(0.2F);
		if(patternLv1.Length > 0){
			float per = 20/_patternstart.Length;
			for(int i = 0; i < patternLv1.Length ; i++){
				string tmp = "level1/";
				tmp += "Lv1_" + i.ToString();
				patternLv1[i] = Instantiate(Resources.Load(tmp, typeof(GameObject))) as GameObject;
				patternLv1[i].SetActive(false);
				iLoad += per;
			}
		}iLoad = 70;
		yield return new WaitForSeconds(0.2F);
		if(patternLv2.Length > 0){
			float per = 20/_patternstart.Length;
			for(int i = 0; i < patternLv2.Length ; i++){
				string tmp = "level2/";
				tmp += "Lv2_" + i.ToString();
				patternLv2[i] = Instantiate(Resources.Load(tmp, typeof(GameObject))) as GameObject;
				patternLv2[i].SetActive(false);
				iLoad += per;
			}
		}iLoad = 90;
		yield return new WaitForSeconds(0.5F);
		iLoad = 100;
	}

	public Texture2D bgLoad;
	public GUISkin skin;
	void OnGUI() {
		if(iLoad < 100) {
			SetUpGUIMatrix.Resolution(800,480);
			GUI.skin = skin;
			GUI.DrawTexture(new Rect(0,0,800,480),bgLoad);
			GUI.Label(new Rect(350,190,100,100),iLoad.ToString());
		}
	}
	
	void Start () {
		if(PlayerPrefs.GetInt("AGAIN") == 1) {
			PlayerPrefs.SetInt("AGAIN", 0);
			checkrungame = true;
		}
		iLoad = 10;
		StartCoroutine("LoadPattern");

		gameControl  = gameObject.GetComponent<GameControl>();
		countSpeedUp = (int)Random.Range(gameControl.speedControl*20/2, gameControl.speedControl*20/2+5);
		_randomItems  = (int)Random.Range(countSpeedUp/3-3, countSpeedUp/3+2);
		feverCount = (int)Random.Range(2,4);
	}


	[HideInInspector]
	public bool free = false;
	public void Pattern() {
		if(checkrungame && !free && iLoad == 100){
			if(Bonus) {
				GameObject ob = Instantiate(Resources.Load("Bonus/BonusLv1", typeof(GameObject))) as GameObject;
				ob.SendMessage("StartPattern");
			}else if(gameControl.feverPoint >= 5) {
				gameControl.feverPoint = 0;
				PatterOn = true;
				GameObject ob = Instantiate(Resources.Load("Fever/Fever_"+(int)Random.Range(0,2.99F), typeof(GameObject))) as GameObject;
				ob.SendMessage("StartPattern");
			}else if (!PatterOn && !gameControl.EndTrue && !Bonus) {
				if(CountNow == countSpeedUp) {
					int i;
					if(eventSet < 0) {
						i = (int)Random.Range(0F, 3F);
					}else {
						i = eventSet;
						eventSet = -1;
					}
					if(i == 0) {
						specialEvent.GetComponent<WallRealTime>().StartEvent();
						PatterOn = true;
					}else if(i == 1) {
						specialEvent.GetComponent<Puzzle>().StartEvent();
						PatterOn = true;
					}else {
						specialEvent.GetComponent<throwthing>().StartEvent();
					}
					CountNow++;
					lastFlowY = 0;
				}else if(CountNow >= countSpeedUp) {
					CountNow = 0;
					countSpeedUp = (int)Random.Range(gameControl.speedControl*20/2, gameControl.speedControl*20/2+5);
					gameControl.SetSpeed(gameControl.speedtime+0.1F);
					if(gameControl.speedtime > 2.6F)
						gameLevel = 4;
					else if(gameControl.speedtime > 2F)
						gameLevel = 3;
					else if(gameControl.speedtime > 1.6F)
						gameLevel = 2;
					else if(gameControl.speedtime > 1.4F)
						gameLevel = 1;
					else
						gameLevel = 0;
				}else {
					if (GameLevel()) {
						RandomItems();
						FeverMode();
						Enemy();
					}
				}
			}
		 }else{
			int i = (int)Random.Range(0F, _patternstart.Length);
			if(i != lastPattern) {
				lastPattern = i;
				_patternstart[i].SetActive(true);
				_patternstart[i].SendMessage("StartPattern");
			}
		}
	}
	private bool en = false;
	private void Enemy() {
		if(en)
			en = false;
		else
		if((int)Random.Range(0, 20) < 5){
			en = true;
			int countItem = (int)Random.Range(0,6F);
			if(countItem < 2)
				Instantiate(Bird);
			else if(countItem < 3){
				Instantiate(_upDown,new Vector3(18+Random.Range(0,7),_upDown.transform.position.y+lastFlowY,-2),_upDown.transform.rotation);
			}
			else if(countItem < 6)
				Instantiate(forwardAttack);
			else {
				int num = (int)Random.Range(0,1.99F);
				if(num == 0){
					float x = Random.Range(0,10);
					Instantiate(drop,new Vector3(drop.transform.position.x+x, drop.transform.position.y, drop.transform.position.z), drop.transform.rotation);
				}else{
					Instantiate(drop,new Vector3(15, drop.transform.position.y, drop.transform.position.z), drop.transform.rotation);
					Instantiate(drop,new Vector3(22, drop.transform.position.y, drop.transform.position.z), drop.transform.rotation);
				}
			}
		}
	}

	private void RandomItems() {
		if(items.Length > 0 || Sitems.Length > 0)
		if(_randomItems-- <= 0) {
			_randomItems  = (int)Random.Range(countSpeedUp/3-3, countSpeedUp/3+2);
			int s = (int)Random.Range(0,4);
			if(s == 0) {
				if((countSpeedUp-CountNow) > 2){
					int num = (int)Random.Range(0,Sitems.Length - 0.0001F);
					int x = (int)Random.Range(10,26);
					Instantiate(Sitems[num],new Vector3(x,lastFlowY-2, -2),Sitems[num].transform.rotation);
				}else
					_randomItems += 3;
			}else {
				int num = (int)Random.Range(0,items.Length - 0.0001F);
				int x = (int)Random.Range(10,26);
				Instantiate(items[num],new Vector3(x,lastFlowY-2, -2),items[num].transform.rotation);
			}
		}
	}

	private void FeverMode() {
		if(--feverCount <= 0) {
			feverCount = (int)Random.Range(2,4);
			if((countSpeedUp-CountNow) > 2){
				int x = (int)Random.Range(10,26);
				Instantiate(fever,new Vector3(x,lastFlowY-2, -2),fever.transform.rotation);
			}else
				_randomItems += 4;
		}
	}

	bool GameLevel(){
		int i;
		if(gameLevel == 4){
			if(hardPattern){
				i = (int)Random.Range(0F, noEnemyNoPatternLv2);
				hardPattern = false;
			}else
				i = (int)Random.Range(0F, patternLv2.Length);

			if(i != lastPattern) {
				lastPattern = i;
				patternLv2[i].SetActive(true);
				patternLv2[i].SendMessage("StartPattern");
				CountNow++;
				if(i < noEnemyNoPatternLv2)
					return true;
				else
					hardPattern = true;
				return false;
			}
		}else if(gameLevel == 3){
			int x = (int)Random.Range(0F, 2F);
			if(x == 0){ ////////////////////////////// PatterLv2 ////////////////////
				if(hardPattern){
					i = (int)Random.Range(0F, noEnemyNoPatternLv2);
					hardPattern = false;
				}else
					i = (int)Random.Range(0F, patternLv2.Length);
				if(i != lastPattern) {
					lastPattern = i;
					patternLv2[i].SetActive(true);
					patternLv2[i].SendMessage("StartPattern");
					CountNow++;
					if(i < noEnemyNoPatternLv2)
						return true;
					else
						hardPattern = true;
					return false;
				}
			}else { ////////////////////////////// PatterLv1 ////////////////////
				if(hardPattern){
					i = (int)Random.Range(0F, noEnemyNoPatternLv1);
					hardPattern = false;
				}else
					i = (int)Random.Range(0F, patternLv1.Length);
				if(i != lastPattern) {
					lastPattern = i;
					patternLv1[i].SetActive(true);
					patternLv1[i].SendMessage("StartPattern");
					CountNow++;
					if(i < noEnemyNoPatternLv1)
						return true;
					else
						hardPattern = true;
					return false;
				}
			}
		}else if(gameLevel == 2){ ////////////////////////////// PatterLv1 ////////////////////
			if(hardPattern){
				i = (int)Random.Range(0F, noEnemyNoPatternLv1);
				hardPattern = false;
			}else
				i = (int)Random.Range(0F, patternLv1.Length);
			if(i != lastPattern) {
				lastPattern = i;
				patternLv1[i].SetActive(true);
				patternLv1[i].SendMessage("StartPattern");
				CountNow++;
				if(i < noEnemyNoPatternLv1)
					return true;
				else
					hardPattern = true;
				return false;
			}
		}else if(gameLevel == 1){
			int x = (int)Random.Range(0F, 2F);
			if(x == 0){ ////////////////////////////// PatterLv1 ////////////////////
				if(hardPattern){
					i = (int)Random.Range(0F, noEnemyNoPatternLv1);
					hardPattern = false;
				}else
					i = (int)Random.Range(0F, patternLv1.Length);
				if(i != lastPattern) {
					lastPattern = i;
					patternLv1[i].SetActive(true);
					patternLv1[i].SendMessage("StartPattern");
					CountNow++;
					if(i < noEnemyNoPatternLv1)
						return true;
					else
						hardPattern = true;
					return false;
				}
			}else { ////////////////////////////// PatterLv0 ////////////////////
				if(hardPattern){
					i = (int)Random.Range(0F, noEnemyNoPatternLv0);
					hardPattern = false;
				}else
					i = (int)Random.Range(0F, patternLv0.Length);
				if(i != lastPattern) {
					lastPattern = i;
					patternLv0[i].SetActive(true);
					patternLv0[i].SendMessage("StartPattern");
					CountNow++;
					if(i < noEnemyNoPatternLv0)
						return true;
					else
						hardPattern = true;
					return false;
				}
			}
		}else if(gameLevel == 0){ ////////////////////////////// PatterLv0 ////////////////////
			if(hardPattern){
				i = (int)Random.Range(0F, noEnemyNoPatternLv0);
				hardPattern = false;
			}else
				i = (int)Random.Range(0F, patternLv0.Length);
			if(i != lastPattern) {
				lastPattern = i;
				patternLv0[i].SetActive(true);
				patternLv0[i].SendMessage("StartPattern");
				CountNow++;
				if(i < noEnemyNoPatternLv0)
					return true;
				else
					hardPattern = true;
				return false;
			}
		}
		return false;
	}
}
