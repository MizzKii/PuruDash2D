using UnityEngine;
using System.Collections;

public class SelectCharacter : MonoBehaviour {

	public GameObject _character;

	// Use this for initialization
	void Start () {
		_character = Instantiate(_character) as GameObject;
		_character.name = "Penguin";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
