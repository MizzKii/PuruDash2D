using UnityEngine;
using System.Collections;

public class SoundControl : MonoBehaviour {
	
	public AudioSource coin,jump,dash;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void Fx_Coin(){
		coin.Play();	
	}
	
	public void Fx_Jump(){
		jump.Play();	
	}
	
	public void Fx_Dash(){
		dash.Play();	
	}
}