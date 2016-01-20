using UnityEngine;
using System.Collections;

public class FeverMode : MonoBehaviour {

	private GameControl gameControl;
	private GamePlay gp;
	private bool check;
	private CoinSet c;
	public	int size = 1;

	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		gp			= GameObject.Find("GameControl").GetComponent<GamePlay>();
	}

	// Update is called once per frame
	void Update () {
		/*if(gameControl.fever && transform.position.x < 5) {
			GameObject.Find("CC").name = "CharacterControl";
			//gameControl.fever = false;
		}else*/ if(transform.position.x < (size*18)*-1){
			Destroy(transform.gameObject);
		}else if(transform.position.x + ((size-1)*18) <= 0.25F && check) {
			gameControl.fever = false;
			GameObject.Find("CC").name = "CharacterControl";
			//gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
			gp.PatterOn = false;
			check = false;
			Time.timeScale = gameControl.speedtime;
		}else {
			transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.left,gameControl.SpeedFlow()*Time.deltaTime);
		}	
	}
	
	public void StartPattern() {
		transform.position = new Vector3(18, 0, transform.position.z);
		check = true;
	}
}
