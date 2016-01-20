using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		CharacterControl cc = GameObject.Find("CharacterControl").GetComponent<CharacterControl>();
		cc.OnTheLine();
	}
}
