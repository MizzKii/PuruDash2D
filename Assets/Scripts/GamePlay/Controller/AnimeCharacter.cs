using UnityEngine;
using System.Collections;

public class AnimeCharacter : MonoBehaviour {

	private Animation _character,_sket;

	// Use this for initialization
	void Start () {
		_character  = gameObject.GetComponent<CharacterControl>()._character.animation;
		_sket		= gameObject.GetComponent<CharacterControl>()._sket.animation;
		Run();
	}

	public void Stand() {
		_character.PlayQueued("STAND",QueueMode.PlayNow);
		_sket.PlayQueued("STAND",QueueMode.PlayNow);
	}

	public void Run() {
		_character.PlayQueued("RUN",QueueMode.PlayNow);
		_sket.PlayQueued("RUN",QueueMode.PlayNow);
	}

	public void JumpLow() {
		_character.PlayQueued("JUMP_LOW",QueueMode.PlayNow);
		_character.PlayQueued("RUN",QueueMode.CompleteOthers);
		_sket.PlayQueued("JUMP_LOW",QueueMode.PlayNow);
		_sket.PlayQueued("RUN",QueueMode.CompleteOthers);
	}

	public void JumpHeight() {
		_character.PlayQueued("JUMP_HEIGHT",QueueMode.PlayNow);
		_character.PlayQueued("RUN",QueueMode.CompleteOthers);
		_sket.PlayQueued("JUMP_HEIGHT",QueueMode.PlayNow);
		_sket.PlayQueued("RUN",QueueMode.CompleteOthers);
	}

	public void Dash() {
		_character.PlayQueued("STARTDASH",QueueMode.PlayNow);
		_character.PlayQueued("DASH",QueueMode.CompleteOthers);
		_sket.PlayQueued("STARTDASH",QueueMode.PlayNow);
		_sket.PlayQueued("DASH",QueueMode.CompleteOthers);
	}

	public void EndDash() {
		_character.PlayQueued("ENDDASH",QueueMode.PlayNow);
		_character.PlayQueued("RUN",QueueMode.CompleteOthers);
		_sket.PlayQueued("ENDDASH",QueueMode.PlayNow);
		_sket.PlayQueued("RUN",QueueMode.CompleteOthers);
	}

	public void Hit() {
		_character.PlayQueued("HIT",QueueMode.PlayNow);
		_sket.PlayQueued("HIT",QueueMode.PlayNow);
	}

	public void Line() {
		_character.PlayQueued("LINE",QueueMode.PlayNow);
		_sket.PlayQueued("LINE",QueueMode.PlayNow);
	}

	public void Rocket() {
		_character.PlayQueued("JUMP_HEIGHT",QueueMode.PlayNow);
		_character.PlayQueued("LINE",QueueMode.CompleteOthers);
		_sket.PlayQueued("JUMP_HEIGHT",QueueMode.PlayNow);
		_sket.PlayQueued("LINE",QueueMode.CompleteOthers);
	}
}
