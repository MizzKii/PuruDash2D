using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIRanking : UIManager {

	private UITexture texture;
	private UIRanking() {}
	private void Start(){texture = GameObject.Find("GUI").GetComponent<UITexture>();}

	private static UIRanking instance = new UIRanking();
	public static UIManager Load
	{
		get{
			if(instance == null)
				instance = GameObject.Find("GUI").AddComponent<UIRanking>();
			return instance;
		}
	}

	private Vector2 scrollPosition;

	public override UIManager UI() {
		GUI.skin = texture.sk_Box;
		GUI.DrawTexture(new Rect(10,10,600,460), texture.rank_BG);
		if(GUI.Button(new Rect(450,310,120,120), texture.bt_back)){
			//texture.IsRank = false;
			return UIMenu.Load;
		}
		
		//if(texture.IsRank){
			if(GUI.Button(new Rect(430,50,155,70),texture.rank_invent)){
				if(FB.IsLoggedIn)
					texture.fb.CallAppRequestAsFriendSelector();
				else
					texture.fb.Login();
			}
			GUI.DrawTexture(new Rect(80,30,250,130), texture.rank_logo);
			#if UNITY_IOS || UNITY_ANDROID
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				if(Input.mousePosition.x > 75 && Input.mousePosition.x < 450)
					scrollPosition.y += Input.GetTouch(0).deltaPosition.y;
			}
			#endif
			/// scroll ///
			GUI.skin = texture.sk_LabelFont70B;
			GUI.skin.scrollView = texture.sk_Box.scrollView;
			
			if(!FB.IsLoggedIn) {
				GUI.Label (new Rect(120,220,500,100),"Please connect\nyour Facebook.");
				/*if(GUI.Button(new Rect(300,200,100,100),Facebook))
						_fb.Login();*/
			}else if(texture.fb.ranking != null) {
				int i = 0;
				scrollPosition = GUI.BeginScrollView(new Rect(75, 165, 350, 240), scrollPosition, new Rect(0, 0, 300, 100*texture.fb.ranking.Count));
				foreach(object rank in texture.fb.ranking) {
					
					Dictionary<string,object> entry = (Dictionary<string,object>) rank;
					Dictionary<string,object> user = (Dictionary<string,object>) entry["user"];
					
					string name     = ((string) user["name"]).Split(new char[]{' '})[0] + "\n";
					string score     = "SCORE: " + entry["score"];
					
					GUI.DrawTexture(new Rect(10,100*i, 330, 100), texture._score_BG03);
					GUI.Label(new Rect(30,30+100*i,50,90),(i+1).ToString()+".");
					GUI.Label(new Rect(140,17+100*i,200,90),name);
					GUI.Label(new Rect(140,45+100*i,200,90),score);
					
					Texture picture;
					if (texture.fb.friendImages.TryGetValue((string) user["id"], out picture)) 
					{
						GUI.DrawTexture(new Rect(60,15+100*i,70,70), picture);  // Profile picture
					}else
						GUI.DrawTexture(new Rect(60,15+100*i,70,70), texture.picNull);
					
					i++;
				}
				GUI.EndScrollView();
			}else {
				GUI.Label (new Rect(120,250,200,70),"Score Enply.");
			}
			/// scroll ///
		//}
		return instance;
	}
		//RANKING////RANKING////RANKING////RANKING////RANKING////RANKING////RANKING////RANKING////RANKING////RANKING//
}
