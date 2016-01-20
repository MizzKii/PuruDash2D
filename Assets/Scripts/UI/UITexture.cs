using UnityEngine;
using System.Collections;

public class UITexture : MonoBehaviour {

	public GUISkin sk_Box, sk_Button, sk_LabelFont15, sk_LabelFont25, sk_LabelFont30, sk_LabelFont40, sk_LabelFont40B, sk_LabelFont70B;
	//Button//
	public Texture2D bt_back, bt_facebook, bt_facebookOn, bt_home, bt_length_shop, bt_pause, bt_play, bt_playAgain, bt_rank, bt_resume, bt_share, bt_shop;
	public Texture2D[] bt_sound, ready;
	//
	public Texture2D logo ,ico_fever, ico_rocket, film, boxSelectShopGr, boxSelectShopYe, BoxSelectShopYeShot,
	_mission, _noLife, _panguin, _score_BG01, _score_BG02, _score_BG03, _shopBoots,
	_shopItem, _shopMoney, _shopPenguin, _shopSnowboard, _tapToPlay, _touch, textM;
	//Ranking//
	public Texture2D rank_BG, rank_invent, rank_logo, picNull;

	public GameObject item_rocket;

	[HideInInspector]public GUIFB fb;
	[HideInInspector]public Modifier mod;
	[HideInInspector]public GamePlay gp;
	[HideInInspector]public GameControl gc;
	void Start () {
		fb  = gameObject.GetComponent<GUIFB>();
		mod = gameObject.GetComponent<Modifier>();
		gp 	= GameObject.Find("GameControl").GetComponent<GamePlay>();
		gc  = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
	}
}
