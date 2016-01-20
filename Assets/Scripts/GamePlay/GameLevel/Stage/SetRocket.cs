using UnityEngine;
using System.Collections;

public class SetRocket : MonoBehaviour {
	
	private GameControl gameControl;
	private bool check = true,end = false;
	public	int size = 1;

	void Start() {
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		if(!gameControl.Rocket)
			Destroy(transform.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if(!end)
		if(transform.position.x < (size*18)*-1){
			Destroy(transform.gameObject);
		}else if(transform.position.x + ((size-1)*18) <= 0.25F && check) {
			int x = (int)Random.Range(0, 5.9F);
			Instantiate(Resources.Load("PatternRocket/Lv0_"+x.ToString(), typeof(GameObject)));
			check = false;
		}else {
			transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.left,gameControl.SpeedFlow()*Time.deltaTime);
		}
	}
	
	/*public void StartPattern() {
		transform.position = new Vector3(17.8F, 0, transform.position.z);
		check = true;
		end = false;
	}*/
}
