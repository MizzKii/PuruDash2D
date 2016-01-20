using UnityEngine;
using System.Collections;

public class SetUpGUIMatrix : MonoBehaviour {
	public static void Resolution(float defaultScreenWidth,float defaultScreenHeight ){
	  Vector3 scale = Vector3.zero;
	  scale.x = Screen.width / defaultScreenWidth; 
	  scale.y = Screen.height / defaultScreenHeight; 
	  scale.z = 1;
	  GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
	}
}
