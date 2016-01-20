using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Facebook.MiniJSON;

public sealed class GUIFB : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CallFBInit();
	}

	public bool Login () {
		if(!FB.IsLoggedIn)
			CallFBLogin();
		return FB.IsLoggedIn;
	}

	public void Logout() {
		CallFBLogout();
	}

	//////////////////////////////////////////////////////////////////

	private string ApiQuery = "";

	#region FB.Init() Start FB
	
	//private bool isInit = false;
	
	private void CallFBInit()
	{
		FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
		//isInit = true;
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	#endregion

	#region FB.Login() example
	
	private void CallFBLogin()
	{
		FB.Login("email,publish_actions", LoginCallback);
	}
	
	void LoginCallback(FBResult result)
	{
		if (result.Error != null)
			Debug.LogError("Error Response:\n" + result.Error);
		else if (!FB.IsLoggedIn) {
			Debug.Log("Login cancelled by Player");
		}
	}
	
	private void CallFBLogout()
	{
		FB.Logout();
	}
	#endregion

	#region FB.AppRequest() Friend Selector
	
	public string FriendSelectorTitle = "PuruDash";
	public string FriendSelectorMessage = "join now PuruDash Game.";
	public string FriendSelectorFilters = "[\"all\",\"app_users\",\"app_non_users\"]";
	public string FriendSelectorData = "{}";
	public string FriendSelectorExcludeIds = "";
	public string FriendSelectorMax = "";
	
	public void CallAppRequestAsFriendSelector()
	{
		// If there's a Max Recipients specified, include it
		int? maxRecipients = null;
		if (FriendSelectorMax != "")
		{
			try
			{
				maxRecipients = Int32.Parse(FriendSelectorMax);
			}
			catch (Exception e)
			{
				Debug.Log("status: "+e.Message);
			}
		}
		
		// include the exclude ids
		string[] excludeIds = (FriendSelectorExcludeIds == "") ? null : FriendSelectorExcludeIds.Split(',');
		
		FB.AppRequest(
			message: FriendSelectorMessage,
			filters: FriendSelectorFilters,
			excludeIds: excludeIds,
			maxRecipients: maxRecipients,
			data: FriendSelectorData,
			title: FriendSelectorTitle,
			callback: Callback
			);
	}
	#endregion

	/*#region FB.Feed() example
	
	public string FeedToId = "";
	public string FeedLink = "";
	public string FeedLinkName = "";
	public string FeedLinkCaption = "";
	public string FeedLinkDescription = "";
	public string FeedPicture = "";
	public string FeedMediaSource = "";
	public string FeedActionName = "";
	public string FeedActionLink = "";
	public string FeedReference = "";
	public bool IncludeFeedProperties = false;
	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();
	
	public void CallFBFeed()
	{
		Dictionary<string, string[]> feedProperties = null;
		if (IncludeFeedProperties)
		{
			feedProperties = FeedProperties;
		}
		FB.Feed(
			toId: FeedToId,
			link: FeedLink,
			linkName: FeedLinkName,
			linkCaption: FeedLinkCaption,
			linkDescription: FeedLinkDescription,
			picture: FeedPicture,
			mediaSource: FeedMediaSource,
			actionName: FeedActionName,
			actionLink: FeedActionLink,
			reference: FeedReference,
			properties: feedProperties,
			callback: Callback
			);
	}
	
	#endregion*/

	public IEnumerator TakeScreenshot() 
	{
		yield return new WaitForEndOfFrame();
		
		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] screenshot = tex.EncodeToPNG();
		
		var wwwForm = new WWWForm();
		wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
		wwwForm.AddField("message", "Hi! Friends, Can you beat me?");
		
		FB.API("me/photos", Facebook.HttpMethod.POST, Callback, wwwForm);
		gameObject.GetComponent<GUIEnd>().share = true;
	}
	
	//private Texture2D lastResponseTexture;
	void Callback(FBResult result)
	{
		//lastResponseTexture = null;
		if (result.Error != null)
			Debug.Log("Error Response:\n" + result.Error);
		else if (!ApiQuery.Contains("/picture"))
			Debug.Log("Success Response:\n" + result.Text);
		else
		{
			//lastResponseTexture = result.Texture;
			Debug.Log("Success Response:\n");
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////

	public void SaveScore(int score) {	
		var scoreData = new Dictionary<string, string>();
		scoreData["score"] = score.ToString();
		FB.API("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult r) { FbDebug.Log("Result: " + r.Text); }, scoreData);
	}
	
	/*public int GetMeScore() {
		int score = 0;
		FB.API ("/me/scores", Facebook.HttpMethod.GET, delegate(FBResult r)
		        {FbDebug.Log("Result: " + r.Text); score = Int32.Parse(r.Text);});
		return score;
	}

	public Texture loadProfile() {
		Texture profile = null;
		FB.API("/me/picture", Facebook.HttpMethod.GET, delegate(FBResult r){
			FbDebug.Log(r.Texture != null ? "got profile pic" : "no profile pic");
			profile = r.Texture;
		});
		return profile;
	}*/

	public void loadHighScore() {
		FB.API("/app/scores?fields=score,user.limit(20)&locale=th_TH", Facebook.HttpMethod.GET, HighScore);
	}

	///////////////////////////////////////////////////////////////////////////////////////////
	public List<object> ranking = null;
	public Dictionary<string, Texture>  friendImages    = new Dictionary<string, Texture>();

	void HighScore(FBResult response) {

		//ranking.Clear();
		//friendImages.Clear();

		if(response.Error != null) {
			Debug.Log("Load high Score error.");
		}
		////////////////
		var responseObject = Json.Deserialize(response.Text) as Dictionary<string, object>;
		object scoresh;
		var scores = new List<object>();
		if (responseObject.TryGetValue ("data", out scoresh)) 
		{
			scores = (List<object>) scoresh;
		}//////////////
		ranking = new List<object>();
		foreach(object score in scores) 
		{
			var entry = (Dictionary<string,object>) score;

			var user = (Dictionary<string,object>) entry["user"];
			string userId = (string)user["id"];

			ranking.Add(entry);

			if (!friendImages.ContainsKey(userId))
			{
				// We don't have this players image yet, request it now
				FB.API("/"+userId+"/picture", Facebook.HttpMethod.GET, pictureResult =>
				       {
					if (pictureResult.Error != null)
					{
						FbDebug.Error(pictureResult.Error);
					}
					else
					{
						friendImages.Add(userId, pictureResult.Texture);
					}
				});
			}
		}

		scores.Sort(delegate(object firstObj,
		                     object secondObj)
		            {
			return -getScoreFromEntry(firstObj).CompareTo(getScoreFromEntry(secondObj));
		}
		);
	}

	private int getScoreFromEntry(object obj)
	{
		Dictionary<string,object> entry = (Dictionary<string,object>) obj;
		return Convert.ToInt32(entry["score"]);
	}

	///////////////////////////////////////////////////////////
}
