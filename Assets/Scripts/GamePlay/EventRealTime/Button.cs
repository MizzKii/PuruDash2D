using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	//Texture
	public GameObject b3;
	public Texture b2;
	public Texture b1;
	public Texture touch;
	public Texture perfect;
	public Texture great;
	public Texture cool;
	public Texture miss;

	//private GameObject button;
	public int buttonInt;
	private int count;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public GameObject newButton(Vector3 local) {
		return Instantiate(b3,local,transform.rotation) as GameObject;
	}

	public void ButtonCount(int x, GameObject button) {	
		if (x == 3 ){
			button.renderer.material.mainTexture = b2;
		}
		else if(x == 2) {
			button.renderer.material.mainTexture = b1;
		}
		else if(x == 1) {
			button.renderer.material.mainTexture = touch;
		}
		else if(x == 0) {
			button.renderer.material.mainTexture = miss;
		}
		else if(x == -1) {
			Destroy(button);
		}
	}

	public int score(int x, GameObject button) {
		buttonInt = 0;
		if (x == 0) {
			button.renderer.material.mainTexture = perfect;
			return 50;
		}
		else if (x == 1) {
			button.renderer.material.mainTexture = great;
			return 30;
		}
		else if (x == 2) {
			button.renderer.material.mainTexture = cool;
			return 10;
		}
		else {
			button.renderer.material.mainTexture = miss;
			return 0;
		}
	}

	public bool TouchObject(GameObject button) {
		Vector2 touch = Camera.main.ScreenToWorldPoint( Input.GetTouch(0).deltaPosition );
		if(touch.x >= (button.transform.position.x - (button.transform.localScale.x/2))
			&&  touch.x <= (button.transform.position.x + (button.transform.localScale.x/2))
			&& touch.y >= (button.transform.position.y - (button.transform.localScale.z/2))
			&&  touch.y <= (button.transform.position.y + (button.transform.localScale.z/2)))
			return true;
		return false;
	}

	public bool ClickObject(GameObject button) {
		Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(mouse.x >= (button.transform.position.x - (button.transform.localScale.x/2))
			&&  mouse.x <= (button.transform.position.x + (button.transform.localScale.x/2))
			&& mouse.y >= (button.transform.position.y - (button.transform.localScale.z/2))
			&&  mouse.y <= (button.transform.position.y + (button.transform.localScale.z/2)))
			return true;
		return false;
	}
}
