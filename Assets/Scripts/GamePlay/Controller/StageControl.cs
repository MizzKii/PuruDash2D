using UnityEngine;
using System.Collections;

public class StageControl : MonoBehaviour {

	public  float   speed =0.0f;
	private float 	_offset = 0.0f;
	private GameControl gameControl;
	
	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameControl.SpeedFlow() > 0) {
			//_offset += 0.001F * gameControl.SpeedFlow()*speed;
			_offset += Mathf.Repeat(Time.smoothDeltaTime * 0.05F * gameControl.SpeedFlow()*speed,1);
			if(_offset > 1)
				_offset -= 1;
			renderer.material.mainTextureOffset = new Vector2 (_offset , 0);
		}
	}
}
