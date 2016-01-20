using UnityEngine;
using System.Collections;

public class Drop : MonoBehaviour {
	
	private GameControl gameControl;
	private GamePlay gp;
	
	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		gp			= GameObject.Find("GameControl").GetComponent<GamePlay>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < -10)
			Destroy(transform.gameObject);
		else if(transform.position.y > -3.6F + gp.lastFlowY)
			transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.down,gameControl.SpeedFlow()*2*Time.deltaTime);
		transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.left,gameControl.SpeedFlow()*Time.deltaTime);
	}
}
