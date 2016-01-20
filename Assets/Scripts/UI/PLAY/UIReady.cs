using UnityEngine;
using System.Collections;

public class UIReady : UIManager {

	private UITexture texture;
	private int boot_rocket;
	private UIReady() {}
	private void Start()
	{ 
		texture = GameObject.Find("GUI").GetComponent<UITexture>(); 
		boot_rocket = PlayerPrefs.GetInt("Boot_Rocket");
	}

	private static UIReady instance;
	public static UIManager Load
	{
		get{
			if(instance == null)
				instance = GameObject.Find("GUI").AddComponent<UIReady>();
			return instance;
		}
	}

	public override UIManager UI()
	{
		if(texture.gp.iLoad == 100)
		{
			CountDown();
			if(countNum == -1)
				return UIPause.Load;

			if(boot_rocket > 0)
				if(GUI.Button(new Rect(600,0 + 150+Mathf.PingPong(Time.time*50,50F), 150, 150), texture.ico_rocket)){ 
					texture.gc.Rocket = true; 
					texture.gp.checkrungame = true; 
					Instantiate(texture.item_rocket); 
					PlayerPrefs.SetInt("Boot_Rocket", --boot_rocket);
				}
			GUI.DrawTexture(new Rect(280,175,250,150),texture.ready[countNum]);
		}
		return instance;
	}
	
	private int countNum = 1, num = 0;
	private void CountDown(){
		//countNum = 1;
		if(countNum > 0 && ++num > 120){
			num = countNum = 0;
		}
		else if(countNum == 0 && ++num > 100) {
			if(!texture.gc.Rocket)
				texture.gp.checkrungame = true; 
			countNum = -1;
		}
		
	}
}
