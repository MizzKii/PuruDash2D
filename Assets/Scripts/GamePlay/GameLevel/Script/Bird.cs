using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	//private CharacterControl cc;
	private GameControl gc;
	private GamePlay gp;
	private float speedBird = 1.3F;
	private Transform _Character;
	private float down;
	private bool _do = false;

	// Use this for initialization
	void Start () {
		//cc = GameObject.Find("CharacterControl").GetComponent<CharacterControl>();
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
		if(!gc.ghost) {
			_Character = GameObject.Find("CharacterControl").transform;
			_do = true;
		}
		down = -2.5F + gp.lastFlowY;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.left,gc.SpeedFlow()*speedBird*Time.deltaTime);
		if(transform.position.x < -10)
			Destroy(transform.gameObject);
		else if(transform.position.x < 9 && transform.position.x > gc.stopCharacterX+7){
			if(_do){
				Vector3 pos = transform.position;
				pos.y = _Character.position.y;
				if(transform.position.y > down)
					transform.position = Vector3.Slerp(transform.position, pos,(2+gc.SpeedFlow()/10)*Time.deltaTime);
			}
		}
	}

	public Texture2D Warning;
	void OnGUI() {
		if(transform.position.x > 9) {
			SetUpGUIMatrix.Resolution(800,480);
			GUI.DrawTexture(new Rect(710,110,80,80),Warning);
		}
	}
}
