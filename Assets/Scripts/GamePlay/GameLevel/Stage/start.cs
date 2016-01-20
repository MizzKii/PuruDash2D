using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

	//private GameControl gameControl;
	private GamePlay gp;
	private GameObject _camera;
	//private bool check,end;

	// Use this for initialization
	void Start () {
	//	gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
		gp			= GameObject.Find("GameControl").GetComponent<GamePlay>();
		_camera		= GameObject.Find("Main Camera");
	}
	// Update is called once per frame
	void Update () {
		if(transform.position.x < _camera.transform.position.x - 18)
			gameObject.SetActive(false);
		/*if(!end)
			transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.left,gameControl.SpeedFlow()*Time.deltaTime);
			if(transform.position.x <-18){
				gameObject.SetActive(false);
			}else if(transform.position.x <= 0.5F && check) {
				gp.nextHop = transform.position.x;
				gp.PatterOn = false;
				check = false;
			}*/
	}

	private Vector3 startPos;
	public void StartPattern() {
		if(gp == null)
			gp = GameObject.Find("GameControl").GetComponent<GamePlay>();

		startPos = transform.position;
		startPos.x = 17.8F+gp.nextHop;
		transform.position = startPos;

		gp.nextHop = transform.position.x;

		gp.PatterOn = true;
		//check = true;
		//end = false;
	}
}
