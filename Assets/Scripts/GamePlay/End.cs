using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {
	
	public GameObject BG,playAgain,Menu;
	private GameObject newPlayAgain;
	//private GameObject newBG,newMenu;
	private bool endTrue = false;
	private GameControl gameControl;
	
	void Start() {
		gameControl = gameObject.GetComponent("GameControl") as GameControl;
	}
	
	// Update is called once per frame
	void Update () {
		if(endTrue){
			if (Input.touchCount > 0){
				if(TouchObject(newPlayAgain))
					Again();
				//if(TouchObject(newMenu))
					//Application.LoadLevel ("Menu");
			}
			if (Input.GetMouseButtonDown(0) == true){
				if(ClickObject(newPlayAgain))
					Again();
				//if(ClickObject(newMenu))
					//Application.LoadLevel ("Menu");
			}
		}
	}
	
	void OnGUI () {
		if(endTrue){
			GUI.color = Color.black;
			GUI.Label(new Rect(Screen.width/2-60, Screen.height/3 -10, 120, 20), "High score :" + gameControl.highScore);
			GUI.Label(new Rect(Screen.width/2-60, Screen.height/3 +10, 120, 20), "New score  :" + gameControl.score);
		}
	}
	
	public void EndGame(){
		GameObject character = GameObject.Find("CharacterControl") as GameObject;
		character.transform.rigidbody.isKinematic = true;
		//newBG = 
		Instantiate(BG,new Vector3(0F,0F,-5F),BG.transform.rotation);// as GameObject;
		newPlayAgain = Instantiate(playAgain,new Vector3(2.5F, -1.85F, -7F),playAgain.transform.rotation) as GameObject;
		//newMenu = 
		Instantiate(Menu,new Vector3(-3F, -1.85F, -7F),Menu.transform.rotation); //as GameObject;
		endTrue = true;
	}
	
	private void Again() {
		Application.LoadLevel(0);
		/*endTrue = false;
		CharacterControl character = GameObject.Find("CharacterControl").GetComponent("CharacterControl") as CharacterControl;
		character.RunAgian();
		GameObject characterObject = GameObject.Find("CharacterControl") as GameObject;
		characterObject.transform.rigidbody.isKinematic = false;
		GameControl gameControl = gameObject.GetComponent("GameControl") as GameControl;
		gameControl.speedControl = 1F;
		gameControl.score = 0;
		gameControl.CountNow = 0;
		gameControl.score = 0;
		gameControl.eventRun = false;
		gameControl.EndTrue = false;
		gameControl.end = 0;
		gameControl.setCountSpeedUp = 5;
		
		Destroy(newMenu);
		Destroy(newPlayAgain);
		Destroy(newBG);
		Film film = GameObject.Find("EventControl").GetComponent("Film") as Film;
		film.EndFilm();*/
	}

	private bool TouchObject(GameObject box){
		Vector2 touch = Camera.main.ScreenToWorldPoint( Input.GetTouch(0).deltaPosition );
		if(touch.x >= (box.transform.position.x - (box.transform.localScale.x/2))
			&&  touch.x <= (box.transform.position.x + (box.transform.localScale.x/2))
			&& touch.y >= (box.transform.position.y - (box.transform.localScale.z/2))
			&&  touch.y <= (box.transform.position.y + (box.transform.localScale.z/2)))
			return true;
		return false;
	}
	private bool ClickObject(GameObject box){
		Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(mouse.x >= (box.transform.position.x - (box.transform.localScale.x/2))
			&&  mouse.x <= (box.transform.position.x + (box.transform.localScale.x/2))
			&& mouse.y >= (box.transform.position.y - (box.transform.localScale.z/2))
			&&  mouse.y <= (box.transform.position.y + (box.transform.localScale.z/2)))
			return true;
		return false;
	}
}
