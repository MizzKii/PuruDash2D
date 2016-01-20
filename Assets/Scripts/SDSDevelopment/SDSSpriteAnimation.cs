using UnityEngine;
using System.Collections;

public class SDSSpriteAnimation : MonoBehaviour {
	
	public int count;
	public float offsetX;
	public float fr;
	public bool randomAnim;

	void Start () 
	{
		StartAnimation();
	}
	
	public void StartAnimation(){
		int i = randomAnim ? Random.Range(0,count):0;
		StartCoroutine(animate(i));
	}
	
	IEnumerator animate(int index){
		yield return new WaitForSeconds(fr);
		renderer.material.SetTextureOffset("_MainTex",new Vector2(index*offsetX,0));
		StartCoroutine(animate(index+1 < count?index+1:0));
	}
	
}
