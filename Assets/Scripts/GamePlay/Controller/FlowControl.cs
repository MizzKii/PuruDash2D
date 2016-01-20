using UnityEngine;
using System.Collections;

public class FlowControl : MonoBehaviour {
	
	private GameControl gameControl;
	
	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x <-18){
			Destroy(transform.gameObject);
		}else
		{
			transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		}
	}
}