using UnityEngine;
using System.Collections;

public class Film : MonoBehaviour {

	public GameObject film;
	private GameObject newFilm;
	
	public void StartFilm() {
		newFilm = Instantiate(film,film.transform.position,film.transform.rotation) as GameObject;
	}
	
	public void EndFilm() {
		Destroy(newFilm);
	}
}
