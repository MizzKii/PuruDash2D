using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {
	
	//private List<Vector3> StartPos = new List<Vector3>();
	Ray ray;
	GameControl gameControl;
	
	void Start(){
		gameControl = gameObject.GetComponent("GameControl") as GameControl;
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameControl.EndTrue){
			if(Input.GetMouseButtonDown(0)){
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Pause(ray);	
			}
			if(Input.GetMouseButton(0)){
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Debug.DrawRay(ray.origin, ray.direction * 20,Color.red);
			}
			if(Input.GetMouseButtonUp(0)){
			}
		}
	}
	void Pause(Ray ray) {
		RaycastHit rch;
		if(Physics.Raycast(ray, out rch))
        {
			GameControl gameControl = gameObject.GetComponent("GameControl") as GameControl;
			if(rch.collider.name == "Pause")
			{       
				Film fx = GameObject.Find("EventControl").GetComponent("Film") as Film;
				if (Time.timeScale == 1.0F) {
		    		Time.timeScale = 0.0F;
					fx.StartFilm();
					gameControl.pause = true;
				}else {
					gameControl.pause = false;
					fx.EndFilm();
		    		Time.timeScale = 1.0F;
				}
			}
		}
	}
}
