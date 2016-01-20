using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

	public GUISkin 		_GUISkin;
	public bool 		shop = false;
	private int			sho = 1, k = 950;
	private int[]		i;

	public Texture2D[] _chracter, sket;

	public Texture2D 	boost_1, boost_2, boost_3, coin_pic, BGOption, BGShop,
						Penguin, Skate, Item, Boost, More, MainMenu,texture, bt_up ,bt_down , 
	item_Magnet, item_Rocket, item_DoubleCoin, item_HighJump, item_Shield, bt_Item, bt_Penguin, bt_Skate, bt_Boost, coinBig_pic, textCoin;
	
	public Texture2D	Button;
	
	private int coin;
	private int lvVacuum, lvCoinX2, lvJumpUp, lvRocket, lvGhost;
	private int boot_rocket, boot_boom, revive;
	public Texture2D[] exp;
	private Modifier mod;

	/// scroll ///
	public Vector2 scrollPosition = Vector2.zero, scrollPosition2 = Vector2.zero;
	public GUIStyle style, style2;
	/// scroll ///
	 
	private int seChar, seSket;
	private int unChar_2 = 0, unSket_2 = 0;
	
	void Start () {
		/*
		PlayerPrefs.SetInt("LV_Vacuum", 0);
		PlayerPrefs.SetInt("LV_CoinX2", 0);
		PlayerPrefs.SetInt("LV_JumpUp", 0);
		PlayerPrefs.SetInt("LV_Rocket", 0);
		PlayerPrefs.SetInt("SELECT_CHAR", 0);
		PlayerPrefs.SetInt("SELECT_SKET", 0);
		PlayerPrefs.SetInt("CHARACTER_2", 0);
		PlayerPrefs.SetInt("SKET_2", 0);
		*/

		PlayerPrefs.SetInt("LV_Ghost", 0);
		coin = PlayerPrefs.GetInt("Coin");

		seChar = PlayerPrefs.GetInt("SELECT_CHAR");
		seSket = PlayerPrefs.GetInt("SELECT_SKET");

		unChar_2 = PlayerPrefs.GetInt("CHARACTER_2");
		unSket_2 = PlayerPrefs.GetInt("SKET_2");
		
		lvVacuum = PlayerPrefs.GetInt("LV_Vacuum");
		lvCoinX2 = PlayerPrefs.GetInt("LV_CoinX2");
		lvJumpUp = PlayerPrefs.GetInt("LV_JumpUp");
		lvRocket = PlayerPrefs.GetInt("LV_Rocket");
		lvGhost  = PlayerPrefs.GetInt("LV_Ghost");
		coin	 = PlayerPrefs.GetInt("Coin");

		boot_rocket = PlayerPrefs.GetInt("Boot_Rocket");
		boot_boom	= PlayerPrefs.GetInt("Boot_Boom");
		revive		= PlayerPrefs.GetInt("Revive");
	}

	public Texture2D[] number;
	void DrawNum(int x, int y, int num) {
		char[] tmp = num.ToString().ToCharArray();
		for(int i=0;i<tmp.Length;i++) {
			int j = (int)tmp[i] -48;
			if(j > -1 && j < 10)
				GUI.DrawTexture(new Rect(x,y+3,25,25),number[j]);
			x += 17;
		}
	}

	// Use this for initialization
	void OnGUI() {

		TabSlice();
		GUI.skin = _GUISkin;
		SetUpGUIMatrix.Resolution(800,480);
		GUI.DrawTexture(new Rect(0,0,800,480),BGShop);
		GUI.DrawTexture(new Rect(k*-1+795,0,645,480),BGOption);
		GUI.Box(new Rect(k - 150,0,150,480),"");
		GUI.DrawTexture(new Rect(10,10,90,30), textCoin);
		DrawNum(110,10,coin);

		#if UNITY_IOS || UNITY_ANDROID
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			if(Input.mousePosition.x > 650)
				scrollPosition.y += Input.GetTouch(0).deltaPosition.y;
		}
		#endif
		/// scroll ///
		GUI.skin.scrollView = style;

		scrollPosition = GUI.BeginScrollView(new Rect(k - 150, 20, 135, 343), scrollPosition, new Rect(0, 0, 0, 570));

		if(GUI.Button(new Rect(0,0,130,130),Penguin))sho=5;
		else if(GUI.Button(new Rect(0,110 ,130,130),Skate)) sho=2;
		else if(GUI.Button(new Rect(0,220,130,130),Item)) sho=3;
		else if(GUI.Button(new Rect(0,330,130,130),Boost)) sho=4;
		else if(GUI.Button(new Rect(0,440,130,130), More)) sho=1;
	

		GUI.EndScrollView();
		/// scroll ///

		if(GUI.Button(new Rect(k-127,370,100,100), MainMenu)){
			Application.LoadLevel("HighScore");
		}

		switch(sho) {
		case 1 :
			GUI.DrawTexture(new Rect(100,20,180,200), coin_pic);
			GUI.DrawTexture(new Rect(350,20,180,200), coinBig_pic);
			if(GUI.Button(new Rect(100,170,180,50), Button)||GUI.Button(new Rect(100,20,180,200), texture)){
				coin += 500; 
				PlayerPrefs.SetInt("Coin", coin);
			} 
			else if(GUI.Button(new Rect(350,170,180,50), Button)){} 
			GUI.Label(new Rect(100,170,180,50), "$0.99");
			GUI.Label(new Rect(350,170,180,50), "$1.99");
			break;
		case 2 :	
			GUI.DrawTexture(new Rect(100,20,180,200), sket[0]);
			GUI.DrawTexture(new Rect(350,50,180,150), sket[1]);

			if(GUI.Button(new Rect(100,170,180,50), bt_Skate)||GUI.Button(new Rect(100,20,180,200), texture)){
				if(seSket != 0) {
					seSket = 0; PlayerPrefs.SetInt("SELECT_SKET", seSket);
				}
			}  
			if(GUI.Button(new Rect(350,170,180,50), bt_Skate)||GUI.Button(new Rect(350,20,180,200), texture)){
				if(unSket_2 < 1 && coin >= 3000){
					unSket_2 = 1; PlayerPrefs.SetInt("SKET_2", unSket_2);
					coin -= 3000; PlayerPrefs.SetInt("Coin", coin);
				}else if(unSket_2 == 1 && seSket != 1) {
					seSket = 1; PlayerPrefs.SetInt("SELECT_SKET", seSket);
				}
			} 
			if(seSket == 0)
				GUI.Label(new Rect(k*-1+900,170,180,50), "SELECTED");
			else
				GUI.Label(new Rect(k*-1+900,170,180,50), "EQUIP");
			if(seSket == 1)
				GUI.Label(new Rect(k*-1+1150,170,180,50), "SELECTED");
			else if(unSket_2 == 1)
				GUI.Label(new Rect(k*-1+1150,170,180,50), "EQUIP");
			else
				GUI.Label(new Rect(k*-1+1150,170,180,50), "UNLOCK 3,000 C"); 
			break;
		case 3 :
			#if UNITY_IOS || UNITY_ANDROID
				if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					if(Input.mousePosition.x < 650)
						scrollPosition2.x -= Input.GetTouch(0).deltaPosition.x;
				}
				#endif
			/// scroll ///
			GUI.skin.scrollView = style2;
			scrollPosition2 = GUI.BeginScrollView(new Rect(55, 40, 540, 410), scrollPosition2, new Rect(0, 0, 780, 0));
			
			GUI.skin = _GUISkin;
			GUI.DrawTexture(new Rect(50,0,180,200), item_Magnet);
			GUI.DrawTexture(new Rect(300,0,180,200), item_DoubleCoin);
			GUI.DrawTexture(new Rect(50,200,180,200), item_Rocket);
			GUI.DrawTexture(new Rect(300,200,180,200), item_HighJump);
			GUI.DrawTexture(new Rect(550,0,180,200), item_Shield);
			GUI.DrawTexture(new Rect(90,120,100,50), exp[lvVacuum]);
			GUI.DrawTexture(new Rect(340,120,100,50), exp[lvCoinX2]);
			GUI.DrawTexture(new Rect(90,325,100,50), exp[lvRocket]);
			GUI.DrawTexture(new Rect(340,325,100,50), exp[lvJumpUp]);
			GUI.DrawTexture(new Rect(590,120,100,50), exp[lvGhost]);
			if(GUI.Button(new Rect(50,150,180,50), bt_Item)||GUI.Button(new Rect(50,0,180,200), texture)) {
				if(lvVacuum < 4)
					if(coin > Price(lvVacuum)){
						coin -= Price(lvVacuum);
						PlayerPrefs.SetInt("LV_Vacuum", ++lvVacuum);
						PlayerPrefs.SetInt("Coin", coin);
					} 
			}else if(GUI.Button(new Rect(300,150,180,50), bt_Item)||GUI.Button(new Rect(300,0,180,200), texture)){
				if(lvCoinX2 < 4)
					if(coin > Price(lvCoinX2)){
						coin -= Price(lvCoinX2);
						PlayerPrefs.SetInt("LV_CoinX2", ++lvCoinX2);
						PlayerPrefs.SetInt("Coin", coin);
					} 
			}else if(GUI.Button(new Rect(50,350,180,50), bt_Item)||GUI.Button(new Rect(50,200,180,200), texture)){
				if(lvRocket < 4)
					if(coin > Price(lvRocket)){
						coin -= Price(lvRocket);
						PlayerPrefs.SetInt("LV_Rocket", ++lvRocket);
						PlayerPrefs.SetInt("Coin", coin);
					} 
			}else if(GUI.Button(new Rect(300,350,180,50), bt_Item)|GUI.Button(new Rect(300,200,180,200), texture)){
				if(lvJumpUp < 4)
					if(coin > Price(lvJumpUp)){
						coin -= Price(lvJumpUp);
						PlayerPrefs.SetInt("LV_JumpUp", ++lvJumpUp);
						coin-=100; PlayerPrefs.SetInt("Coin", coin);
					} 
			}else if(GUI.Button(new Rect(550,150,180,50), bt_Item)|GUI.Button(new Rect(550,0,180,200), texture)){
				if(lvGhost < 4)
					if(coin > Price(lvGhost)){
						coin -= Price(lvGhost);
						PlayerPrefs.SetInt("LV_Ghost", ++lvGhost);
						coin-=100; PlayerPrefs.SetInt("Coin", coin);
					} 
			}

			GUI.Label(new Rect(50,150,180,50), "LEVEL"+(lvVacuum+1)+" C:"+Price(lvVacuum));
			GUI.Label(new Rect(300,150,180,50), "LEVEL"+(lvCoinX2+1)+" C:"+Price(lvCoinX2));
			GUI.Label(new Rect(50,350,180,50), "LEVEL"+(lvRocket+1)+" C:"+Price(lvRocket));
			GUI.Label(new Rect(300,350,180,50), "LEVEL"+(lvJumpUp+1)+" C:"+Price(lvJumpUp));
			GUI.Label(new Rect(550,150,180,50), "LEVEL"+(lvGhost+1)+" C:"+Price(lvGhost));
			
			GUI.EndScrollView();
			/// scroll ///

			break;
		case 4 :	
			GUI.DrawTexture(new Rect(100,30,180,200), boost_1);
			GUI.DrawTexture(new Rect(350,30,180,200), boost_2);
			GUI.DrawTexture(new Rect(100,230,180,200), boost_3);
			if(GUI.Button(new Rect(100,170,180,70), Button)||GUI.Button(new Rect(100,30,180,200),texture)){
				if(boot_rocket < 5 && coin >= 500) {
					PlayerPrefs.SetInt("Boot_Rocket", ++boot_rocket);
					coin -= 500; PlayerPrefs.SetInt("Coin", coin);
				}
			}else if(GUI.Button(new Rect(350,170,180,70), Button)||GUI.Button(new Rect(350,30,180,200), texture)){
				if(boot_boom < 5 && coin >= 500) {
					PlayerPrefs.SetInt("Boot_Boom", ++boot_boom);
					coin -= 500; PlayerPrefs.SetInt("Coin", coin);
				}
			}else if(GUI.Button(new Rect(100,370,180,70), Button)||GUI.Button(new Rect(100,230,180,200), texture)){
				if(revive < 1 && coin >= 500) {
					PlayerPrefs.SetInt("Revive", ++revive);
					coin -= 500; PlayerPrefs.SetInt("Coin", coin);
				}else if(revive < 2 && coin >= 1000) {
					PlayerPrefs.SetInt("Revive", ++revive);
					coin -= 1000; PlayerPrefs.SetInt("Coin", coin);
				}else if(revive < 3 && coin >= 2500) {
					PlayerPrefs.SetInt("Revive", ++revive);
					coin -= 2500; PlayerPrefs.SetInt("Coin", coin);
				}else if(revive < 4 && coin >= 5000) {
					PlayerPrefs.SetInt("Revive", ++revive);
					coin -= 5000; PlayerPrefs.SetInt("Coin", coin);
				}else if(revive < 5 && coin >= 8000) {
					PlayerPrefs.SetInt("Revive", ++revive);
					coin -= 8000; PlayerPrefs.SetInt("Coin", coin);
				}
			}
			GUI.Label(new Rect(100,180,180,50), "BUY 500  " + boot_rocket + "/5");
			GUI.Label(new Rect(350,180,180,50), "BUY 500  " + boot_boom + "/5");
			if(revive == 0)
				GUI.Label(new Rect(100,380,180,50), "BUY 500  " + revive + "/5");
			else if(revive == 1)
				GUI.Label(new Rect(100,380,180,50), "BUY 1000  " + revive + "/5");
			else if(revive == 2)
				GUI.Label(new Rect(100,380,180,50), "BUY 2500  " + revive + "/5");
			else if(revive == 3)
				GUI.Label(new Rect(100,380,180,50), "BUY 5000  " + revive + "/5");
			else if(revive == 4)
				GUI.Label(new Rect(100,380,180,50), "BUY 8000  " + revive + "/5");
			else
				GUI.Label(new Rect(100,380,180,50), "Full " + revive + "/5");

			break;
		case 5 :
			GUI.DrawTexture(new Rect(k*-1+900,20,180,200), _chracter[0]);
			GUI.DrawTexture(new Rect(k*-1+1150,20,180,200), _chracter[1]);

			if(GUI.Button(new Rect(100,170,180,50), bt_Penguin)||GUI.Button(new Rect(100,20,180,200), texture)){
				if(seChar != 0) {
					seChar = 0; PlayerPrefs.SetInt("SELECT_CHAR", seChar);
				}
			} 
			if(GUI.Button(new Rect(350,170,180,50), bt_Penguin)||GUI.Button(new Rect(350,20,180,200), texture)){
				if(unChar_2 < 1 && coin >= 5000){
					unChar_2 = 1; PlayerPrefs.SetInt("CHARACTER_2", unChar_2);
					coin -= 5000; PlayerPrefs.SetInt("Coin", coin);
				}else if(unChar_2 == 1 && seChar != 1) {
					seChar = 1; PlayerPrefs.SetInt("SELECT_CHAR", seChar);
				}
			}
			if(seChar == 0)
				GUI.Label(new Rect(k*-1+900,170,180,50), "SELECTED");
			else
				GUI.Label(new Rect(k*-1+900,170,180,50), "EQUIP");
			if(seChar == 1)
				GUI.Label(new Rect(k*-1+1150,170,180,50), "SELECTED");
			else if(unChar_2 == 1)
				GUI.Label(new Rect(k*-1+1150,170,180,50), "EQUIP");
			else
				GUI.Label(new Rect(k*-1+1150,170,180,50), "UNLOCK 5,000 C");
			break;


		}
	}
	
	int Price(int price) {
		if(price == 0)
			return 200;
		else if(price == 1)
			return 500;
		else if(price == 2)
			return 1000;
		else if(price == 3)
			return 2000;
		else
			return 0;
	}

	void TabSlice(){
		if(/*!back && */k > 810)
			k -= 6;
		else if(k < 810)
			k = 810;

		else
			k -= 7;
	}

}

