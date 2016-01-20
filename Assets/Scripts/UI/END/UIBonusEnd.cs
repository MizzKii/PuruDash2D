using UnityEngine;
using System.Collections;

public class UIBonusEnd : UIManager {

	private UITexture texture;
	private UIBonusEnd () {}
	private void Start () { texture = gameObject.GetComponent<UITexture>(); boot_boom = PlayerPrefs.GetInt("Boot_Boom"); }

	private static UIBonusEnd instance;
	public static UIManager Load {
		get {
			if(instance == null)
				instance = GameObject.Find("GUI").AddComponent<UIBonusEnd>();
			return instance;
		}
	}
	
	private int boot_boom = 0;
	public override UIManager UI() {
		if(boot_boom > 0) {
			texture.mod.StartCoroutine("Bonus");
			PlayerPrefs.SetInt("Boot_Boom", --boot_boom);
			return UIEndScore.Load;
		}
		return instance;
	}
}
