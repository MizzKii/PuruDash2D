using UnityEngine;
using System.Collections;

public class SetB : MonoBehaviour {
	
	//private GameControl gameControl;
	private GamePlay gp;
	//private bool check,end;
	public	int size = 1;

	private GameObject _camera;

	void Start () {
		//gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
		_camera		= GameObject.Find("Main Camera");
	}

	// Update is called once per frame
	void Update () {
		if(transform.position.x + ((size)*18) < _camera.transform.position.x)
			gameObject.SetActive(false);
		/*if(!end){
			transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.left,gameControl.SpeedFlow()*Time.deltaTime);
			if(transform.position.x < (size*18)*-1){
					gameObject.SetActive(false);
			}else if(transform.position.x + ((size-1)*18) <= 0.25F && check) {
				gp.nextHop = transform.position.x + ((size-1)*18)-0.2F;
				gp.PatterOn = false;
				check = false;
			}
		}*/
	}

	private int flowY;
	private CoinSet c;
	public void StartPattern() {
		if(gp == null)
			gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
		
		flowY = (int)Random.Range(-1.5F,1.5F);
		if(gp.lastFlowY > 0 && flowY > 0)
			flowY = 0;
		else if(gp.lastFlowY < 1 && flowY == -1)
			flowY = 0;
		gp.lastFlowY += flowY;

		transform.position = new Vector3(17.7F+gp.nextHop,gp.lastFlowY,transform.position.z);
		gp.nextHop = transform.position.x + ((size-1)*18);

		if(transform.FindChild("Coin") != null) {
			if(c == null)
				c = transform.FindChild("Coin").GetComponent("CoinSet") as CoinSet;
			c.TurnCoin(true);
		}
			
		gp.PatterOn = true;
		//check = true;
		//end = false;
	}
}
