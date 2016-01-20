using UnityEngine;
using System.Collections;

public class InsertPrefab : MonoBehaviour {

	public GameObject prefab;

	// Use this for initialization
	void Start () {
		//transform.renderer.enabled = false;
		GameObject _pre = (GameObject)Instantiate(prefab,transform.position, prefab.transform.rotation);
		//_pre.transform.parent = transform;
		_pre.transform.parent = transform.parent;
		_pre.name = prefab.name;
		Destroy(transform.gameObject);
	}
}
