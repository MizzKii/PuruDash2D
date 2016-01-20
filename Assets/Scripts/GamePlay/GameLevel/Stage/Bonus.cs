using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

	private GameControl gameControl;
	private GamePlay gp;
	private bool check,end;
	private CoinSet c;
	public	int size = 1;

	// Update is called once per frame
	void Update () {
		if(!end)
		if(transform.position.x < (size*18)*-1){
				Destroy(transform.gameObject);
				end = true;
			}else if(transform.position.x + ((size-1)*18) <= 0.25F && check) {
				gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
				gp.PatterOn = false;
				check = false;	
			}else {
				transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.left,gameControl.SpeedFlow()*Time.deltaTime);
			}	
	}
	
	public void StartPattern() {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
		gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
		gp.Bonus = false;
		
		transform.position = new Vector3(18, gp.lastFlowY, transform.position.z);
		gp.LastP = transform.gameObject;
		
		c = transform.FindChild("Coin").GetComponent("CoinSet") as CoinSet;
		c.TurnCoin(true);
			
		check = true;
		end = false;
	}
}
