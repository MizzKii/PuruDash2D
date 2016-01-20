using UnityEngine;
using System.Collections;

public class UpDown : MonoBehaviour {
	
	private float posY;
	private GameControl gc;
	
	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		posY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(end == 0) {
			transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.left,gc.SpeedFlow()*Time.deltaTime);
			transform.position = new Vector3(transform.position.x+0.001F,(posY)+Mathf.PingPong(Time.time*(gc.SpeedFlow()*0.5F),5f),transform.position.z);
			if(transform.position.x < -10)
				Destroy(transform.gameObject);
		}else {
			if(end == 1) {
				transform.Translate(Vector3.up *10* Time.deltaTime, Space.World);
				if(transform.position.y >= tragetY)
					end = 2;
			}else if(end == 2){
				transform.Translate(Vector3.down *10* Time.deltaTime, Space.World);
				if(transform.position.y < -5)
					Destroy(transform.gameObject);
			}
		}
	}

	private int end = 0;
	private float tragetY;
	void OnTriggerEnter2D (Collider2D collision) {
		if (collision.gameObject.name == "CharacterControl") {
			end = 1;
			tragetY = transform.position.y+1;
			transform.position = new Vector3(transform.position.x, transform.position.y, -4);
		}
	}
}
