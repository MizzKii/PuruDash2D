using UnityEngine;
using System.Collections;

public class UIMission : UIManager {

	private UITexture texture;
	private UIMission() {}
	private bool _start = false;
	private void Start () { texture = gameObject.GetComponent<UITexture>(); ReMission(); _start = true;}

	private static UIMission instance;
	public static UIManager Load
	{
		get {
			if(instance == null)
				instance = GameObject.Find("GUI").AddComponent<UIMission>();
			return instance;
		}
	}

	///////////////////>>>>>>>>MISSION///////////////////////////////////////////////////////////

	private int 		jumpMission = 0, dashMission = 0, coinMission = 0;
	private int			jumpNum,dashNum;
	private int			page = 0;
	private void ReMission() {
		jumpNum		= PlayerPrefs.GetInt("Jump_Count");
		jumpMission = PlayerPrefs.GetInt("LV_Mission_Jump");
		dashNum = PlayerPrefs.GetInt("Dash_Count");
		dashMission = PlayerPrefs.GetInt("LV_Mission_Dash");
		coinMission = PlayerPrefs.GetInt("LV_Mission_Coin");
		
		page = PlayerPrefs.GetInt("PAGE_MISSION");
	}
	
	private int			bonusCoin = 0;
	public override UIManager UI() {
		if(_start){
		int maxjump = 0, maxcoin = 0, maxdash = 0;
		
		/////////////////////////Page 1/////////////////////////////////////////////////////////
		if(page == 0) {
			
			maxjump = 20;
			if(jumpNum >= maxjump && jumpMission < 1) {
				texture.gc.coin += 100; texture.gc.AddCoin(); jumpMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Jump",jumpMission);
				bonusCoin += 100;
			}
			
			maxcoin = 200;
			if((texture.gc.coinAll+texture.gc.coin) >= maxcoin && coinMission < 1) {
				texture.gc.coin += 100; texture.gc.AddCoin(); coinMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Coin",coinMission);
				bonusCoin += 100;
			}
			
			maxdash = 20;
			if(dashNum >= maxdash && dashMission < 1) {
				texture.gc.coin += 100; texture.gc.AddCoin(); dashMission = 1;
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
				
				texture.gc.coin += 300; texture.gc.AddCoin();
				bonusCoin += 300;
			}
		}
		///////////////////////Page 2///////////////////////////////////////////////////////////
		else if(page == 1) {
			
			maxjump = 30;
			if(jumpNum >= maxjump && jumpMission < 1) {
				texture.gc.coin += 200; texture.gc.AddCoin(); jumpMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Jump",jumpMission);
				bonusCoin += 200;
			}
			
			maxcoin = 500;
			if((texture.gc.coinAll+texture.gc.coin) >= maxcoin && coinMission < 1) {
				texture.gc.coin += 200; texture.gc.AddCoin(); coinMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Coin",coinMission);
				bonusCoin += 200;
			}
			
			maxdash = 30;
			if(dashNum >= maxdash && dashMission < 1) {
				texture.gc.coin += 200; texture.gc.AddCoin(); dashMission = 1;
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
				
				texture.gc.coin += 300; texture.gc.AddCoin();
				bonusCoin += 600;
			}
		}
		//////////////////////Page 3////////////////////////////////////////////////////////////
		else {
			
			maxjump = 50;
			if(jumpNum >= maxjump && jumpMission < 1) {
				texture.gc.coin += 500; texture.gc.AddCoin(); jumpMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Jump",jumpMission);
				bonusCoin += 300;
			}
			
			maxcoin = 1500;
			if((texture.gc.coinAll+texture.gc.coin) >= maxcoin && coinMission < 1) {
				texture.gc.coin += 500; texture.gc.AddCoin(); coinMission = 1;
				PlayerPrefs.SetInt("LV_Mission_Coin",coinMission);
				bonusCoin += 300;
			}
			
			maxdash = 50;
			if(dashNum >= maxdash && dashMission < 1) {
				texture.gc.coin += 500; texture.gc.AddCoin(); dashMission = 1;
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
			
			GUI.DrawTexture(new Rect(195,197,400,40), texture.boxSelectShopYe);
			GUI.Label(new Rect(210,202,400,30), "Jump " + jumpNum +"/"+ maxjump+" time");
		}else {
			GUI.DrawTexture(new Rect(195,197,400,40), texture.boxSelectShopGr);
			GUI.Label(new Rect(210,202,400,30), "Mission Jump Complete.");
		}
		
		if(coinMission < 1) {
			GUI.DrawTexture(new Rect(195,237,400,40), texture.boxSelectShopYe);
			GUI.Label(new Rect(210,242,400,30), "Get coin " + (texture.gc.coinAll+texture.gc.coin) +"/"+ maxcoin+ " coin");
		}else {
			GUI.DrawTexture(new Rect(195,237,400,40), texture.boxSelectShopGr);
			GUI.Label(new Rect(210,242,400,30), "Mission Coin Complete.");
		}
		
		if(dashMission < 1) {
			if(dashNum > maxdash)
				dashNum = maxdash;
			GUI.DrawTexture(new Rect(195,277,400,40), texture.boxSelectShopYe);
			GUI.Label(new Rect(210,282,400,30), "Dash " + dashNum +"/"+ maxdash+" time");
		}else {
			GUI.DrawTexture(new Rect(195,277,400,40), texture.boxSelectShopGr);
			GUI.Label(new Rect(210,282,400,30), "Mission Dash Complete.");
		}
		
		//////////////////////////////////////////////////////////////////////////////////////
		if(bonusCoin > 0) {
			GUI.DrawTexture(new Rect(0, 0, 800, 480), texture.film);
			GUI.DrawTexture(new Rect(250,140,300,200), texture._score_BG01);
			GUI.Label(new Rect(295,160,200,50), "Mission Clear!", texture.sk_LabelFont70B.label);
			GUI.Label(new Rect(295,200,200,50),"Bonus Coin : +"+bonusCoin.ToString());
			if(GUI.Button(new Rect(340,260,120,55),texture.BoxSelectShopYeShot)) bonusCoin = 0;
			GUI.Label(new Rect(335,260,120,50),"OK.",texture.sk_LabelFont40.label);
		}
		//////////////////////////////////////////////////////////////////////////////////////
		}
		return instance;
	}
}
