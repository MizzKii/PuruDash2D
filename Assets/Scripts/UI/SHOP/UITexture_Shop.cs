using UnityEngine;
using System.Collections;

public class UITexture_Shop : MonoBehaviour {
	public Texture2D[] number, character, board, item, boost, buyCoin, upgrade;

	public int numSlice = 950, coin, shopCase, selectCh, unSelectCh, selectBd, unSelectBd, lvVacuum, lvCoinX2, lvJumpUp, lvRocket, lvGhost, boost_rocket, boost_boom, boost_revive;
	//picture
	public Texture2D bg_shop, box_shop, box_coin;
	//button
	public Texture2D bt_chracterMn, bt_boardMn, bt_itemMn, bt_boostMn, bt_moneyMn, bt_homeMn, bt_chracterLs, bt_boardLs, bt_itemLs, bt_boostLs, bt_buyCoinLs;

	public GUISkin box, button, labelFont40b;

	public GUIStyle scrollHor, scrollVer;

	public Vector2 scrollPositionHor = Vector2.zero, scrollPositionVer = Vector2.zero;

	void Start () {
		//PlayerPrefs.SetInt("LV_Ghost", 0);
		coin = PlayerPrefs.GetInt("Coin");
		
		selectCh = PlayerPrefs.GetInt("SELECT_CHAR");
		selectBd = PlayerPrefs.GetInt("SELECT_SKET");
		
		unSelectCh = PlayerPrefs.GetInt("CHARACTER_2");
		unSelectBd = PlayerPrefs.GetInt("SKET_2");
		
		lvVacuum = PlayerPrefs.GetInt("LV_Vacuum");
		lvCoinX2 = PlayerPrefs.GetInt("LV_CoinX2");
		lvJumpUp = PlayerPrefs.GetInt("LV_JumpUp");
		lvRocket = PlayerPrefs.GetInt("LV_Rocket");
		lvGhost  = PlayerPrefs.GetInt("LV_Ghost");

		boost_rocket = PlayerPrefs.GetInt("Boot_Rocket");
		boost_boom	= PlayerPrefs.GetInt("Boot_Boom");
		boost_revive		= PlayerPrefs.GetInt("Revive");
	}
}
