using UnityEngine;
using System.Collections;

public class BearAttack : MonoBehaviour {

	public GameObject gco;
	private GameControl gc;
	private GamePlay gp;

	public GameObject character;

	public GameObject bear;
	private GameObject _bear;

	public int step = 0;

	// Use this for initialization
	void Start () {
		gc = gco.GetComponent<GameControl>();
		gp = gco.GetComponent<GamePlay>();

		pos.y = -3.5F;
		pos.z = -2;
	}
	
	// Update is called once per frame
	void Update () {
		if(step == 1)
			Step1();
		else if(step == 2)
			Step2();
		else if(step == 3)
			EndEvent();
		else if(step == 4)
			Fail();
	}

	public void StartEvent() {
		_bear = Instantiate(bear,new Vector3(-10,-3.5F,-2),bear.transform.rotation) as GameObject;
		gp.free = true;
		pos.x = gc.stopCharacterX -1F;
		Count = 5;
		StartCoroutine("Ready");
	}

	private IEnumerator Ready() {
		yield return new WaitForSeconds(3);
		step = 1;
	}

	private Vector3 pos;
	private void Step1() {
		if(_bear.transform.position.x < gc.stopCharacterX -1.1F)
			_bear.transform.position = Vector3.Lerp(_bear.transform.position, pos, 0.05F);
		else{
			pos.x = -11;
			step = 2;
		}
	}

	private bool down = false,up = false, enable = false;
	private int Y, Count,i;
	private void Step2() {
		if(gc.EndTrue) {
			step = 4;
			gp.free = false;
		}else if(Count == 0 && !enable)
			step = 3;
		else if(!enable && !gc.EndTrue){
			i = (int)Random.Range(0,2);
			if(i == 1)
				StartCoroutine("Down");
			else
				StartCoroutine("Up");
			enable = true;
			Count--;
		}
	}
	public Texture2D pointer,warning,jump,dash;
	private bool	warn = false, _jump = false, _dash = false;
	void OnGUI() {
		SetUpGUIMatrix.Resolution(800,480);
		if(up) {
			if(Y < 100)
				Y = 280;
			GUI.DrawTexture(new Rect(600,Y-=2,100,100),pointer);
		}else if(down) {
			if(Y > 380)
				Y = 200;
			GUI.DrawTexture(new Rect(600,Y+=2,100,100),pointer);
		}
		if(warn)
			GUI.DrawTexture(new Rect(350,190,100,100),warning);
		else if(_jump)
			GUI.DrawTexture(new Rect(272,190,256,101),jump);
		else if(_dash)
			GUI.DrawTexture(new Rect(272,190,256,101),dash);
	}

	IEnumerator Down() {
		down = true;
		warn = true;
		Y = 200;
		_bear.animation.PlayQueued("UP_1",QueueMode.PlayNow);
		_bear.animation.PlayQueued("UP_2",QueueMode.CompleteOthers);
		yield return new WaitForSeconds(2);
		warn = false;
		yield return new WaitForSeconds(0.2F);
		warn = true;
		yield return new WaitForSeconds(0.2F);
		warn = false;
		_dash = true;
		yield return new WaitForSeconds(0.2F);
		_bear.animation.PlayQueued("ATTACK_UP",QueueMode.PlayNow);
		yield return new WaitForSeconds(0.5F);
		_dash = false;
		yield return new WaitForSeconds(0.2F);
		down = false;
		
		if(!character.GetComponent<CharacterControl>().dashTrue)
			gc.EndTrue = true;
		else {
			_bear.animation.PlayQueued("UP_TO_RUN",QueueMode.CompleteOthers);
			_bear.animation.PlayQueued("RUN",QueueMode.CompleteOthers);
		}
		yield return new WaitForSeconds(1);
		enable = false;
	}

	IEnumerator Up() {
		up = true;
		warn = true;
		Y = 280;
		_bear.animation.PlayQueued("DOWN_1",QueueMode.PlayNow);
		_bear.animation.PlayQueued("DOWN_2",QueueMode.CompleteOthers);
		yield return new WaitForSeconds(2);
		warn = false;
		yield return new WaitForSeconds(0.2F);
		warn = true;
		yield return new WaitForSeconds(0.2F);
		warn = false;
		_jump = true;
		_bear.animation.PlayQueued("ATTACK_DOWN",QueueMode.PlayNow);
		yield return new WaitForSeconds(0.7F);
		_jump = false;
		yield return new WaitForSeconds(0.2F);
		up = false;

		if(character.GetComponent<CharacterControl>().GetIsGrounded())
			gc.EndTrue = true;
		else{
			_bear.animation.PlayQueued("DOWN_TO_RUN",QueueMode.CompleteOthers);
			_bear.animation.PlayQueued("RUN",QueueMode.CompleteOthers);
		}
		yield return new WaitForSeconds(1);
		enable = false;
	}

	private void EndEvent() {
		if(_bear.transform.position.x >= -10) {
			_bear.transform.position = Vector3.Lerp(_bear.transform.position, pos, 0.05F);
		}else if(!gp.PatterOn) {
			gp.free = false;
			gp.PatterOn = true;
			gp.Bonus = true;
			Destroy(_bear);
			step = 0;
		}
	}

	private void Fail() {
		if(_bear.transform.position.x >= -10 && !gc.EndTrue) {
			_bear.transform.position = Vector3.Lerp(_bear.transform.position, pos, 0.1F);
		}else if(_bear.transform.position.x < -9) {
			Destroy(_bear);
			step = 0;
		}
	}
}
