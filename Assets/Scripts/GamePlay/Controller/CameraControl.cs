using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public Transform character;
	public Transform[] BG;
	private Vector3 posY;
	//public GameObject coin;

	private GamePlay gp;
	private GameControl gc;

	// Use this for initialization
	void Start () {
		posY = transform.position;
		gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
		gc = GameObject.Find ("GameControl").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gp.iStart && !gc.EndTrue) {
			transform.Translate(Vector3.right*Time.deltaTime*6, Space.World);

			if(transform.position.x > gp.nextHop - 0.2F)
				gp.PatterOn = false;
		
			for(int i=0;i<BG.Length;i++) 
				BG[i].position = new Vector3(transform.position.x, BG[i].position.y, BG[i].position.z);
			if(transform.position.y != character.position.y + 2.5F) {
				posY = transform.position;
				posY.y = character.position.y+2.5F;
				transform.position = Vector3.Lerp(transform.position, posY,4 * Time.deltaTime);
			}
		}
	}
}
