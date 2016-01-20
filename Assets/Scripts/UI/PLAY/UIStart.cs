using UnityEngine;
using System.Collections;

public class UIStart : UIManager {
	
	private UITexture texture;
	private UIStart(){}
	void Start(){ texture = GameObject.Find("GUI").GetComponent<UITexture>(); }

	private static UIStart instance;
	public static UIManager Load 
	{
		get{
			if(instance == null)
				instance = GameObject.Find("GUI").AddComponent<UIStart>();
			return instance;
		}
	}
	
	public override UIManager UI()
	{
		if(texture.gp.iLoad == 100)
		{
			if(GUI.Button(new Rect(0,0,550,480), texture._tapToPlay, texture.sk_Button.button))
				return UIReady.Load;
			GUI.DrawTexture(new Rect(185,50,429,250), texture.logo);
			UIMenu.Load.UI ();
		}
		return instance;
	}
}
