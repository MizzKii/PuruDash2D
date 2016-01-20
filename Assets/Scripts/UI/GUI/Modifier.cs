using UnityEngine;
using System.Collections;

public class Modifier : MonoBehaviour {

	public Texture2D[] number;
	public void DrawNum(int x, int y, int num) {
		char[] tmp = num.ToString().ToCharArray();
		for(int i=0;i<tmp.Length;i++) {
			int j = (int)tmp[i] -48;
			if(j > -1 && j < 10)
				GUI.DrawTexture(new Rect(x,y,32,32),number[j]);
			x += 23;
		}
	}

	public int SoundIndex = 0;
	public void SoundCheck(){
		if(AudioListener.volume > 0) {
			SoundIndex = 1;
			AudioListener.volume = 0;
		}else{
			SoundIndex = 0;
			AudioListener.volume = 1;
		}
		
	}

	public IEnumerator PlayAgain(float speedEnd) {
		GameControl gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.vacuum = true;
		GameObject _character;
		if(GameObject.Find("CharacterControl") != null)
			_character = GameObject.Find("CharacterControl");
		else
			_character = GameObject.Find("CC");
		CharacterControl cc = _character.GetComponent<CharacterControl>();
		cc.AddJump(30);
		float y = _character.transform.position.y;
		if(_character.transform.position.y < -5)
			y = -5;
		_character.transform.position = new Vector3(gc.stopCharacterX, y, -5);
		GamePlay gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
		gp.checkrungame = false;
		gc.Rocket = true;
		gc.speedControl = 3.5F;
		gc.EndTrue = false;
		
		yield return new WaitForSeconds(1F);
		_character.transform.position = new Vector3(gc.stopCharacterX, _character.transform.position.y, -2);
		gc.speedControl = speedEnd;
		yield return new WaitForSeconds(2F);
		gp.checkrungame = true;
		gc.Rocket = false;
		gc.vacuum = false;
	}

	private IEnumerator Bonus() {
		GameObject _character;
		GameControl gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.vacuum = true;
		if(GameObject.Find("CharacterControl") != null)
			_character = GameObject.Find("CharacterControl");
		else
			_character = GameObject.Find("CC");
		GamePlay gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
		CharacterControl cc = _character.GetComponent<CharacterControl>();
		cc.AddJump(30);
		float speedEnd = gc.speedControl;
		gc.speedControl = speedEnd*2;
		gc.EndTrue = false;
		gp.checkrungame = false;
		gc.Rocket = true;
		float y = _character.transform.position.y;
		if(_character.transform.position.y < -5)
			y = -5;
		_character.transform.position = new Vector3(gc.stopCharacterX, y, -5);
		yield return new WaitForSeconds(0.5F);
		_character.transform.position = new Vector3(gc.stopCharacterX, _character.transform.position.y, -2);
		yield return new WaitForSeconds(2);
		gc.speedControl = 0;
		gc.vacuum = false;
		gc.EndTrue = true;
		gc.EndGame();
	}
}
