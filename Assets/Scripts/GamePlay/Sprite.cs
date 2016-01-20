using UnityEngine;
using System.Collections;

public class Sprite : MonoBehaviour {
	
	//Texture
	public Texture textureRun;
	public Texture textureJump;
	public Texture wallfail;
	public Texture texturefail;

	//Sprite
	private int _uvTieX = 4;
	private int _uvTieY = 6;
	private int _fps = 24;
 
	private Vector2 _size;
	private Renderer _myRenderer;
	private int _lastIndex = -1;
	
	// Use this for initialization
	void Start () {
		renderer.material.mainTexture = textureRun;
		_size = new Vector2 (1.0f / _uvTieX , 1.0f / _uvTieY);
		_myRenderer = renderer;
		if(_myRenderer == null)
			enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		int index = (int)(Time.timeSinceLevelLoad * _fps) % (_uvTieX * _uvTieY);
    	if(index != _lastIndex)
		{
			// split into horizontal and vertical index
			int uIndex = index % _uvTieX;
			int vIndex = index / _uvTieY;
 
			// build offset
			// v coordinate is the bottom of the image in opengl so we need to invert.
			Vector2 offset = new Vector2 (uIndex * _size.x, 1.0f - _size.y - vIndex * _size.y);
 
			_myRenderer.material.SetTextureOffset ("_MainTex", offset);
			_myRenderer.material.SetTextureScale ("_MainTex", _size);
			
			_lastIndex = index;
		}
	}
	
	public void ChangeTexture(string str) {
		if(str == "run"){
			renderer.material.mainTexture = textureRun;
			_uvTieX = 4;
		 	_uvTieY = 6;
			_fps = 24;
		}else if(str == "jump"){
			renderer.material.mainTexture = textureJump;
			_uvTieX = 5;
		 	_uvTieY = 9;
			_fps = 24;
		}
		_size = new Vector2 (1.0f / _uvTieX , 1.0f / _uvTieY);
		_myRenderer = renderer;
		if(_myRenderer == null)
			enabled = false;
	}
}
