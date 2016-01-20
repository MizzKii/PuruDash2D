using UnityEngine;
using System.Collections;

public class UnderBox : MonoBehaviour {

	private GameObject character;
	private CharacterControl cc;
	
	// Use this for initialization
	void Start () {
		if(GameObject.Find("CharacterControl") != null)
			character = GameObject.Find("CharacterControl");
		else
			character = GameObject.Find("CC");
		cc = character.GetComponent<CharacterControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x - transform.localScale.x/2 <= character.transform.position.x && transform.position.x + transform.localScale.x/2 > character.transform.position.x && !cc.underObject && character.transform.position.y < transform.position.y)
			cc.underObject = true;
		else if(transform.position.x + transform.localScale.x/2 < character.transform.position.x && cc.underObject){
			cc.underObject = false;
			cc.Straight();
		}
	}
}
