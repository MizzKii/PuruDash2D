using UnityEngine;
using System.Collections;

public class UI_Shop : UIManager {
	private UITexture_Shop texture;
	public UI_Shop(){
		texture = GameObject.Find("GUI_Shop").GetComponent<UITexture_Shop>();
	}
	private static UI_Shop instance = null;
	public static UIManager Load() {
		if(instance == null)
			instance = new UI_Shop();
		return instance;
	}

	public override UIManager UI(){
		TabSlice();
		GUI.skin = texture.box;
		GUI.DrawTexture(new Rect(0, 0, 800, 480), texture.bg_shop);
		GUI.DrawTexture(new Rect(NumSlice(-5) * -1, 0, 645, 480), texture.box_shop);
		GUI.Box(new Rect(NumSlice(650), 0, 150, 480), "");
		GUI.DrawTexture(new Rect(10, 10, 90, 30), texture.box_coin);
		DrawNum(110, 10, texture.coin);

		#if UNITY_IOS || UNITY_ANDROID
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
			if(Input.mousePosition.x > 650)
				texture.scrollPositionVer.y += Input.GetTouch(0).deltaPosition.y;
		}
		#endif

		GUI.skin = texture.button;
		GUI.skin.scrollView = texture.scrollVer;
		texture.scrollPositionVer = GUI.BeginScrollView(new Rect(texture.numSlice - 150, 20, 135, 343), texture.scrollPositionVer, new Rect(0, 0, 0, 570));
		if(GUI.Button(new Rect(0, 0, 130, 130), texture.bt_chracterMn))
			texture.shopCase = 5;
		else if(GUI.Button(new Rect(0, 110, 130, 130), texture.bt_boardMn))
			texture.shopCase = 2;
		else if(GUI.Button(new Rect(0, 220, 130, 130), texture.bt_itemMn))
			texture.shopCase = 3;
		else if(GUI.Button(new Rect(0, 330, 130, 130), texture.bt_boostMn))
			texture.shopCase = 4;
		else if(GUI.Button(new Rect(0, 440, 130, 130), texture.bt_moneyMn))
			texture.shopCase = 1;
		GUI.EndScrollView();
		if(GUI.Button(new Rect(NumSlice(673), 370, 100, 100), texture.bt_homeMn))
			Application.LoadLevel("HighScore");
		ShopCase();

		return instance;
	}

	void TabSlice(){
		if(texture.numSlice > 810)
			texture.numSlice -= 6;
		else if(texture.numSlice < 810)
			texture.numSlice = 810;
		else
			texture.numSlice -= 7;
	}

	int NumSlice(int x){
		x = 800 - x;
		int result = texture.numSlice - x;
		return result;
	}

	void DrawNum(int x, int y, int num) {
		char[] tmp = num.ToString().ToCharArray();
		for(int i = 0; i < tmp.Length; i++) {
			int j = (int)tmp[i] -48;
			if(j > -1 && j < 10)
				GUI.DrawTexture(new Rect(x,y+3,25,25), texture.number[j]);
			x += 17;
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

	void ShopCase(){
		switch(texture.shopCase) {
		case 1 :
			MoneyCase();
			break;
		case 2 :
			BoardCase();
			break;
		case 3 :
			ItemCase();
			break;
		case 4 :	
			BoostCase();
			break;
		case 5 :
			PenguinCase();
			break;
		default :
			PenguinCase();
			break;
		}
	}

	void PenguinCase(){
		GUI.DrawTexture(new Rect(NumSlice(-100) * -1, 20, 180, 200), texture.character[0]);
		GUI.DrawTexture(new Rect(NumSlice(-350) * -1, 20, 180, 200), texture.character[1]);
		
		if(GUI.Button(new Rect(NumSlice(-100) * -1, 20, 180, 200), texture.bt_chracterLs)){
			if(texture.selectCh != 0) {
				texture.selectCh = 0; 
				PlayerPrefs.SetInt("SELECT_CHAR", texture.selectCh);
			}
		} 
		if(GUI.Button(new Rect(NumSlice(-350), 20, 180, 200), texture.bt_chracterLs)){
			if(texture.unSelectCh < 1 && texture.coin >= 5000){
				texture.unSelectCh = 1; 
				PlayerPrefs.SetInt("CHARACTER_2", texture.unSelectCh);
				texture.coin -= 5000; 
				PlayerPrefs.SetInt("Coin", texture.coin);
			}else if(texture.unSelectCh == 1 && texture.selectCh != 1){
				texture.selectCh = 1; 
				PlayerPrefs.SetInt("SELECT_CHAR", texture.selectCh);
			}
		}
		if(texture.selectCh == 0)
			GUI.Label(new Rect(NumSlice(-100) * -1, 170, 180, 50), "SELECTED");
		else
			GUI.Label(new Rect(NumSlice(-100) * -1, 170, 180, 50), "EQUIP");
		if(texture.selectCh == 1)
			GUI.Label(new Rect(NumSlice(-350) * -1, 170, 180, 50), "SELECTED");
		else if(texture.unSelectCh == 1)
			GUI.Label(new Rect(NumSlice(-350) * -1, 170, 180, 50), "EQUIP");
		else
			GUI.Label(new Rect(NumSlice(-350) * -1, 170, 180, 50), "UNLOCK 5,000 C");
	}

	void BoardCase(){
		GUI.DrawTexture(new Rect(100,20,180,200), texture.board[0]);
		GUI.DrawTexture(new Rect(350,50,180,150), texture.board[1]);
		
		if(GUI.Button(new Rect(100,20,180,200), texture.bt_boardLs)){
			if(texture.selectBd != 0) {
				texture.selectBd = 0; 
				PlayerPrefs.SetInt("SELECT_SKET", texture.selectBd);
			}
		}  
		if(GUI.Button(new Rect(350,20,180,200), texture.bt_boardLs)){
			if(texture.unSelectBd < 1 && texture.coin >= 3000){
				texture.unSelectBd = 1; 
				PlayerPrefs.SetInt("SKET_2", texture.unSelectBd);
				texture.coin -= 3000; 
				PlayerPrefs.SetInt("Coin", texture.coin);
			}else if(texture.unSelectBd == 1 && texture.selectBd != 1) {
				texture.selectBd = 1; 
				PlayerPrefs.SetInt("SELECT_SKET", texture.selectBd);
			}
		} 
		if(texture.selectBd == 0)
			GUI.Label(new Rect(100, 170, 180, 50), "SELECTED");
		else
			GUI.Label(new Rect(100, 170, 180, 50), "EQUIP");
		if(texture.selectBd == 1)
			GUI.Label(new Rect(350, 170, 180, 50), "SELECTED");
		else if(texture.unSelectCh == 1)
			GUI.Label(new Rect(350, 170, 180, 50), "EQUIP");
		else
			GUI.Label(new Rect(350, 170, 180, 50), "UNLOCK 3,000 C");
	}

	void ItemCase(){
		#if UNITY_IOS || UNITY_ANDROID
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			if(Input.mousePosition.x < 650)
				texture.scrollPositionHor.x -= Input.GetTouch(0).deltaPosition.x;
		}
		#endif

		GUI.skin.scrollView = texture.scrollHor;
		texture.scrollPositionHor = GUI.BeginScrollView(new Rect(55, 40, 540, 410), texture.scrollPositionHor, new Rect(0, 0, 780, 0));

		GUI.DrawTexture(new Rect(50, 0, 180, 200), texture.item[0]);
		GUI.DrawTexture(new Rect(300, 0, 180, 200), texture.item[1]);
		GUI.DrawTexture(new Rect(50, 200, 180, 200), texture.item[2]);
		GUI.DrawTexture(new Rect(300, 200, 180, 200), texture.item[3]);
		GUI.DrawTexture(new Rect(550, 0, 180, 200), texture.item[4]);
		GUI.DrawTexture(new Rect(90, 120, 100, 50), texture.upgrade[texture.lvVacuum]);
		GUI.DrawTexture(new Rect(340, 120, 100, 50), texture.upgrade[texture.lvCoinX2]);
		GUI.DrawTexture(new Rect(90, 325, 100, 50), texture.upgrade[texture.lvRocket]);
		GUI.DrawTexture(new Rect(340, 325, 100, 50), texture.upgrade[texture.lvJumpUp]);
		GUI.DrawTexture(new Rect(590, 120, 100, 50), texture.upgrade[texture.lvGhost]);
		if(GUI.Button(new Rect(50, 0, 180, 200), texture.bt_itemLs)) {
			if(texture.lvVacuum < 4)
			if(texture.coin > Price(texture.lvVacuum)){
				texture.coin -= Price(texture.lvVacuum);
				PlayerPrefs.SetInt("LV_Vacuum", ++texture.lvVacuum);
				PlayerPrefs.SetInt("Coin", texture.coin);
			} 
		}else if(GUI.Button(new Rect(300, 0, 180, 200), texture.bt_itemLs)){
			if(texture.lvCoinX2 < 4)
			if(texture.coin > Price(texture.lvCoinX2)){
				texture.coin -= Price(texture.lvCoinX2);
				PlayerPrefs.SetInt("LV_CoinX2", ++texture.lvCoinX2);
				PlayerPrefs.SetInt("Coin", texture.coin);
			} 
		}else if(GUI.Button(new Rect(50, 200, 180, 200), texture.bt_itemLs)){
			if(texture.lvRocket < 4)
			if(texture.coin > Price(texture.lvRocket)){
				texture.coin -= Price(texture.lvRocket);
				PlayerPrefs.SetInt("LV_Rocket", ++texture.lvRocket);
				PlayerPrefs.SetInt("Coin", texture.coin);
			} 
		}else if(GUI.Button(new Rect(300, 200, 180, 200), texture.bt_itemLs)){
			if(texture.lvJumpUp < 4)
			if(texture.coin > Price(texture.lvJumpUp)){
				texture.coin -= Price(texture.lvJumpUp);
				PlayerPrefs.SetInt("LV_JumpUp", ++texture.lvJumpUp);
				texture.coin-=100; PlayerPrefs.SetInt("Coin", texture.coin);
			} 
		}else if(GUI.Button(new Rect(550, 0, 180, 200), texture.bt_itemLs)){
			if(texture.lvGhost < 4)
			if(texture.coin > Price(texture.lvGhost)){
				texture.coin -= Price(texture.lvGhost);
				PlayerPrefs.SetInt("LV_Ghost", ++texture.lvGhost);
				texture.coin-=100; PlayerPrefs.SetInt("Coin", texture.coin);
			} 
		}
		GUI.Label(new Rect(50, 150, 180, 50), "LEVEL" + (texture.lvVacuum + 1) + " C:" + Price(texture.lvVacuum));
		GUI.Label(new Rect(300, 150, 180, 50), "LEVEL" + (texture.lvCoinX2 + 1) + " C:" + Price(texture.lvCoinX2));
		GUI.Label(new Rect(50, 350, 180, 50), "LEVEL" + (texture.lvRocket + 1) + " C:" + Price(texture.lvRocket));
		GUI.Label(new Rect(300, 350, 180, 50), "LEVEL" + (texture.lvJumpUp + 1) + " C:" + Price(texture.lvJumpUp));
		GUI.Label(new Rect(550, 150, 180, 50), "LEVEL" + (texture.lvGhost + 1) + " C:" + Price(texture.lvGhost));
		GUI.EndScrollView();
	}

	void BoostCase(){
		GUI.DrawTexture(new Rect(100,30,180,200), texture.boost[0]);
		GUI.DrawTexture(new Rect(350,30,180,200), texture.boost[1]);
		GUI.DrawTexture(new Rect(100,230,180,200), texture.boost[2]);
		if(GUI.Button(new Rect(100,30,180,200),texture.bt_boostLs)){
			if(texture.boost_rocket < 5 && texture.coin >= 500) {
				PlayerPrefs.SetInt("Boot_Rocket", ++texture.boost_rocket);
				texture.coin -= 500; PlayerPrefs.SetInt("Coin", texture.coin);
			}
		}else if(GUI.Button(new Rect(350,30,180,200), texture.bt_boostLs)){
			if(texture.boost_boom < 5 && texture.coin >= 500) {
				PlayerPrefs.SetInt("Boot_Boom", ++texture.boost_boom);
				texture.coin -= 500; PlayerPrefs.SetInt("Coin", texture.coin);
			}
		}else if(GUI.Button(new Rect(100,230,180,200), texture.bt_boostLs)){
			if(texture.boost_revive < 1 && texture.coin >= 500) {
				PlayerPrefs.SetInt("Revive", ++texture.boost_revive);
				texture.coin -= 500; PlayerPrefs.SetInt("Coin", texture.coin);
			}else if(texture.boost_revive < 2 && texture.coin >= 1000) {
				PlayerPrefs.SetInt("Revive", ++texture.boost_revive);
				texture.coin -= 1000; PlayerPrefs.SetInt("Coin", texture.coin);
			}else if(texture.boost_revive < 3 && texture.coin >= 2500) {
				PlayerPrefs.SetInt("Revive", ++texture.boost_revive);
				texture.coin -= 2500; PlayerPrefs.SetInt("Coin", texture.coin);
			}else if(texture.boost_revive < 4 && texture.coin >= 5000) {
				PlayerPrefs.SetInt("Revive", ++texture.boost_revive);
				texture.coin -= 5000; PlayerPrefs.SetInt("Coin", texture.coin);
			}else if(texture.boost_revive < 5 && texture.coin >= 8000) {
				PlayerPrefs.SetInt("Revive", ++texture.boost_revive);
				texture.coin -= 8000; PlayerPrefs.SetInt("Coin", texture.coin);
			}
		}
		GUI.Label(new Rect(100,180,180,50), "BUY 500  " + texture.boost_rocket + "/5");
		GUI.Label(new Rect(350,180,180,50), "BUY 500  " + texture.boost_boom + "/5");
		if(texture.boost_revive == 0)
			GUI.Label(new Rect(100,380,180,50), "BUY 500  " + texture.boost_revive + "/5");
		else if(texture.boost_revive == 1)
			GUI.Label(new Rect(100,380,180,50), "BUY 1000  " + texture.boost_revive + "/5");
		else if(texture.boost_revive == 2)
			GUI.Label(new Rect(100,380,180,50), "BUY 2500  " + texture.boost_revive + "/5");
		else if(texture.boost_revive == 3)
			GUI.Label(new Rect(100,380,180,50), "BUY 5000  " + texture.boost_revive + "/5");
		else if(texture.boost_revive == 4)
			GUI.Label(new Rect(100,380,180,50), "BUY 8000  " + texture.boost_revive + "/5");
		else
			GUI.Label(new Rect(100,380,180,50), "Full " + texture.boost_revive + "/5");
	}

	void MoneyCase(){
		GUI.DrawTexture(new Rect(100,20,180,200), texture.buyCoin[0]);
		GUI.DrawTexture(new Rect(350,20,180,200), texture.buyCoin[1]);
		if(GUI.Button(new Rect(100,20,180,200), texture.bt_buyCoinLs)){
			texture.coin += 500; 
			PlayerPrefs.SetInt("Coin", texture.coin);
		} 
		else if(GUI.Button(new Rect(350,170,180,200), texture.bt_buyCoinLs)){} 
		GUI.Label(new Rect(100,170,180,50), "$0.99");
		GUI.Label(new Rect(350,170,180,50), "$1.99");
	}
}
