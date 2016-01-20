using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private CharacterControl character;
	private GameControl gameControl;
	private GamePlay gamePlay;
	private GameObject cc;

	// Use this for initialization
	void Start () {
		character	= gameObject.GetComponent<CharacterControl>();
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		gamePlay	= GameObject.Find("GameControl").GetComponent<GamePlay>();
		cc			= GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		if(gamePlay.iLoad == 100 && !gameControl.EndTrue){
			if(gamePlay.iStart)
				transform.Translate(Vector3.right*Time.deltaTime*6,Space.World);

			if(gamePlay.iStart && transform.position.x < cc.transform.position.x - 10)
				gameControl.EndTrue = true;
			else if(transform.position.x < cc.transform.position.x - 5)
				transform.Translate(Vector3.right*Time.deltaTime,Space.World);

			if(!gameControl.EndTrue) {
				if(!gameControl.Rocket && !gameControl.fever && gamePlay.checkrungame) {
					if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
						UseTouch();
					else
						UseMouse();
					if(transform.position.y < -7 || transform.position.x < -9)
						gameControl.EndTrue = true;
				}
				if(transform.position.x < gameControl.stopCharacterX)
					character.MoveTo(gameControl.stopCharacterX);
				else if(!gamePlay.iStart)
					gamePlay.iStart = true;
			}
			if(Input.GetKeyDown(KeyCode.Space)) {
				character.StartCoroutine("Jump");
			}else if(Input.GetKeyDown(KeyCode.LeftControl)) {
				character.StartCoroutine("Dash");
			}
		}
	}

	private Vector2 startPoition = Vector2.zero;//,nowPoition = Vector2.zero;
	private bool check = false;
	void UseMouse(){
		if(Input.GetMouseButtonDown(0)){
			startPoition = Input.mousePosition;	
			//nowPoition = Input.mousePosition;
			check = true;
		}
		
		if(check && Input.GetMouseButton(0)){
			//nowPoition = Input.mousePosition;

			if(Input.mousePosition.y > (startPoition.y + 50F)) {
				character.StartCoroutine("Jump");
				check = false;
			}
			if(Input.mousePosition.y < (startPoition.y - 50F)){
				character.StartCoroutine("Dash");
				check = false;
			}
		}
		
		if(Input.GetMouseButtonUp(0)){
			check = false;
		}
	}
	void UseTouch(){
		int indexTouch = Input.touchCount;
		int i = 0;
		while(i < indexTouch){
			Touch touch = Input.GetTouch(i);	
			if(touch.phase == TouchPhase.Began){
				startPoition = Input.mousePosition;
				//nowPoition = Input.mousePosition;
				check = true;
			}
			
			if(touch.phase == TouchPhase.Moved){
				//nowPoition = Input.mousePosition;

				if(Input.mousePosition.y > (startPoition.y + 50F)) {
					character.StartCoroutine("Jump");
					check = false;
				}
				if(Input.mousePosition.y < (startPoition.y - 50F)){
					character.StartCoroutine("Dash");
					check = false;
				}
			}
			
			if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended){
				check = false;
			}
			i++;
		}
	}
}
