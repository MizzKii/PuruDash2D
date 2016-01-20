using UnityEngine;
using System.Collections;

public class CoinSet : MonoBehaviour {

	public void TurnCoin(bool on) {
		if(on)
			for(int i = 0; i < transform.childCount; i++) {
				GameObject c = transform.GetChild(i).gameObject;
				c.SetActive(true);
				foreach(Transform child in c.transform){
					if(child!=null){
						SDSSpriteAnimation sanim = child.GetComponent<SDSSpriteAnimation>();
						if(sanim){
							sanim.StartAnimation();
						}
					}
				}
			}
		else
			for(int i = 0; i < transform.childCount; i++) {
				GameObject c = transform.GetChild(i).gameObject;
				c.SetActive(false);
			}
	}
}
