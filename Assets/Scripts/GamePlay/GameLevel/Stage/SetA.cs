using UnityEngine;
using System.Collections;

public class SetA : MonoBehaviour {
	
	public GameObject[] stone;
	public GameObject[] ice;
	public GameObject[] pricker;
	public GameObject[] stalactite;
	private GameControl gameControl;
	private GamePlay gp;
	
	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
	}
	bool check = true;
	// Update is called once per frame
	void Update () {
		if(transform.position.x <-18){
			Destroy(transform.gameObject);
		}else if(transform.position.x <= 0 && check) {
			gp.PatterOn = false;
			check = false;	
		}else {
			transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		}
	}
	
	public void StartPattern() {
		gameControl = (GameControl)GameObject.Find("GameControl").GetComponent("GameControl");
		gp			= GameObject.Find("GameControl").GetComponent<GamePlay>();
		gp.PatterOn = true;
		
		float flowY = (int)Random.Range(0F,1.9F);
		transform.position = new Vector3(transform.position.x, transform.position.y + flowY, transform.position.z);
		
		int i = 0;
		while(i < stone.Length) {
			StoneRealTime s = stone[i].GetComponent("StoneRealTime") as StoneRealTime;
			s.StartEvent(stone[i].transform.position.x, stone[i].transform.position.y, stone[i].transform.position.z);
			i++;
		}
		i = 0;
		while(i < ice.Length) {
			IceRealTime s = ice[i].GetComponent("IceRealTime") as IceRealTime;
			s.StartEvent(ice[i].transform.position.x, ice[i].transform.position.y);
			i++;
		}
		i = 0;
		while(i < pricker.Length) {
			PrickerRealTime s = pricker[i].GetComponent("PrickerRealTime") as PrickerRealTime;
			s.StartEvent(pricker[i].transform.position.x, pricker[i].transform.position.y);
			i++;
		}
		i = 0;
		while(i < stalactite.Length) {
			StalactiteRealTime s = stalactite[i].GetComponent("StalactiteRealTime") as StalactiteRealTime;
			s.StartEvent(stalactite[i].transform.position.x, stalactite[i].transform.position.y, flowY);
			i++;
		}
	}
}
