using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	
	public int point = 1;
	private int _point;
	private float x;
	
	void OnTriggerEnter2D (Collider2D collision) {
		if(collision.transform.name == "CharacterControl" || collision.transform.name == "CC" || collision.transform.name == "Rocket(Clone)" || collision.transform.name == "RocketFever(Clone)"){
			
			Instantiate(fx,new Vector3(collision.transform.position.x+0.2F,collision.transform.position.y,collision.transform.position.z),fx.transform.rotation);
			
			gameObject.SetActive(false);
			GameControl gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
			gameControl.coin += point;
			SoundControl sound = GameObject.Find("Sound").GetComponent("SoundControl") as SoundControl;
			transform.position = new Vector3((transform.parent.position.x + pos.x - 18), pos.y + transform.parent.position.y, pos.z);
			sound.Fx_Coin();
			Vacuum = false;
			CoinX2 = false;
			point = _point;
		}
    }
	public GameObject fx;
	private GameObject go;
	private GameControl gameControl;
	private Vector3 pos = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		_point = point;
		setCoin();
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		if(GameObject.Find("CharacterControl") != null)
			go = GameObject.Find("CharacterControl");
		else
			go = GameObject.Find("CC");

		pos = transform.position;
		if(transform.parent.position.y > 0)
			pos.y -= transform.parent.position.y;
		
		x= transform.position.x*20;
	}
	
	private bool Vacuum = false;
	private bool CoinX2 = false;
	
	// Update is called once per frame
	void Update () {

		if(gameControl.vacuum && !Vacuum && transform.position.x < go.transform.position.x + 5){
			Vacuum = true;
		}else if(gameControl.CoinX2 && !CoinX2) {
			CoinX2 = true;
			point *= 2;
		}
			
		if(Vacuum)
			if(transform.position.x > go.transform.position.x - 5 && transform.position.x < go.transform.position.x)
				transform.position = Vector3.Lerp(transform.position, go.transform.position, gameControl.SpeedFlow()/2 + 3 * Time.deltaTime);
			else if(transform.position.x < go.transform.position.x + 5)
				transform.position = Vector3.Lerp(transform.position, go.transform.position, 3 * Time.deltaTime);
			else
				transform.position = new Vector3(transform.position.x,(pos.y + transform.parent.position.y)+Mathf.PingPong(Time.time+x,0.5f),transform.position.z);
		else
			transform.position = new Vector3(transform.position.x,(pos.y + transform.parent.position.y)+Mathf.PingPong(Time.time+x,0.5f),transform.position.z);
	}
	
	void setCoin(){
		transform.renderer.enabled = false;
		Vector3 position = transform.localPosition;
		transform.localPosition = position;
		string name;
		if(point == 1)
			name = "CoinCopper";
		else if(point == 2)
			name = "CoinSilver";
		else
			name = "CoinGold";
		GameObject instance = Instantiate(Resources.Load("Coin/"+name, typeof(GameObject)),Vector3.zero,Quaternion.identity) as GameObject;
		//instance.name = "coinanim";
		instance.transform.parent = transform;
		instance.transform.localPosition = Vector3.zero;
		instance.transform.localEulerAngles = Vector3.zero;
		instance.transform.localScale = transform.localScale;
	}
}
