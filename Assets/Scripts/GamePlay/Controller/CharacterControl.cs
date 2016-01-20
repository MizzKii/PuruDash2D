using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

	//FX
	public ParticleEmitter _smoke;
	private SoundControl sound;
	
	//Variable
	public float speed = 5;
	public float force = 8;
	private int jumpNum, dashNum, dashMission, jumpMission;
	private Vector2 sizeCollider;
	//private bool isEvent = false;

	[HideInInspector]
	public GameObject _character, _sket, sketOb;

	//Script
	private AnimeCharacter anime;
	private GameControl gameControl;
	private BoxCollider2D _collider;

	void Awake() {
		Vector3 pos = transform.position;
		pos.y -= 1F;
		_character = Instantiate(Resources.Load("Penguin/Character_"+PlayerPrefs.GetInt("SELECT_CHAR"),typeof(GameObject))) as GameObject;
		_character.transform.position = pos;
		_character.transform.rotation = new Quaternion(0,180,0,0);
		_character.name = "Penguin";
		_character.transform.parent = transform;
		//////////////////sket///////////////////////////////////
		pos.x += 0.1F;
		pos.y += 0.25F;
		_sket = Instantiate(Resources.Load("Penguin/Sket_"+PlayerPrefs.GetInt("SELECT_SKET"),typeof(GameObject))) as GameObject;
		_sket.transform.position = pos;
		_sket.transform.rotation = new Quaternion(0,180,0,0);
		_sket.transform.parent = sketOb.transform;
		_sket.name = "Sket";
	}

	// Use this for initialization
	void Start () {
		gameControl 		 = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
		sound				 = GameObject.Find("Sound").GetComponent("SoundControl") as SoundControl;
		anime				 = gameObject.GetComponent<AnimeCharacter>();
		_collider			 = (BoxCollider2D)GetComponent(typeof(BoxCollider2D));
		sizeCollider		 = _collider.size;
		_smoke.emit			 = false;

		jumpMission = PlayerPrefs.GetInt("LV_Mission_Jump");
		dashMission = PlayerPrefs.GetInt("LV_Mission_Dash");
		if(jumpMission < 1)
			jumpNum = PlayerPrefs.GetInt("Jump_Count");
		if(dashMission < 1)
			dashNum = PlayerPrefs.GetInt("Dash_Count");
	}



	
	public string flowName = "FLOW";
	private bool isGrounded = false;
	void OnCollisionEnter2D(Collision2D coll) {
		if (!isGrounded) {
			isGrounded = true;
			if(dashTrue)
				_smoke.emit = true;
		}
	}public bool GetIsGrounded() { return isGrounded; }

	public bool MoveTo(float x){
		if(transform.position.x < x - 0.1F){
			transform.Translate(Vector3.right * (Time.deltaTime*speed));
			return false;
		}else if(transform.position.x > x + 0.1F){
			transform.Translate(Vector3.left * (Time.deltaTime*speed));
			return false;
		}else{
			return true;
		}
	}

	//private bool mission = true;

	private int _jump = 0;
	public IEnumerator Jump ()
	{
		if (isGrounded &&!underObject) { 
			if(Straight()) {
				rigidbody2D.velocity = new Vector2(0,force);
				sound.Fx_Jump();
				anime.JumpLow();
				_jump = 1;
				yield return new WaitForSeconds(0.2F);
				isGrounded = false;
				if(jumpMission < 1)
					PlayerPrefs.SetInt("Jump_Count",++jumpNum);
			}
		}else if(_jump < 2) {
			rigidbody2D.velocity = new Vector2(0,force);
			anime.JumpHeight();
			_jump++;
		}else if(gameControl.jumpUp && _jump < 3) {
			rigidbody2D.velocity = new Vector2(0,force);
			_jump++;
		}
	}

	[HideInInspector]
	public bool underObject = false;
	[HideInInspector]
	public bool dashTrue = false;
	public IEnumerator Dash() {
		if(!dashTrue){
			dashTrue = true;
			rigidbody2D.velocity = new Vector2(0,-force);
			_collider.size /= 2;
			_collider.center -= _collider.size/2;
			sound.Fx_Dash();
			anime.Dash();
			if(isGrounded && dashTrue)
				_smoke.emit = true;
			if(dashMission < 1)
				PlayerPrefs.SetInt("Dash_Count",++dashNum);
			yield return new WaitForSeconds(1F);
			Straight();
		}
	}

	public bool Straight() {
		if(!underObject){
			if(dashTrue){
				_smoke.emit = false;
				_collider.center += _collider.size/2;
				_collider.size = sizeCollider;
				anime.EndDash();
				dashTrue = false;
			}return true;
		}return false;
	}

	public void Hit() {
		anime.Hit();
	} 
	
	public void OnTheLine() {
		anime.Line();
	}
	
	public IEnumerator JumpRocket() {
		_collider.isTrigger = true;
		rigidbody2D.velocity = new Vector2(0,14);
		Vector3 pos = transform.position;
		pos.z -= 3;
		transform.position = pos;
		anime.Rocket();
		yield return new WaitForSeconds(0.5F);
		_collider.isTrigger = false;
		pos = transform.position;
		pos.z += 3;
		transform.position = pos;
	}

	/*public void HitUp () {
		rigidbody2D.velocity = new Vector2(0,force);
	}*/

	public void AddJump(int y) {
		rigidbody2D.velocity = new Vector2(0,y);
		anime.JumpHeight();
	}
}