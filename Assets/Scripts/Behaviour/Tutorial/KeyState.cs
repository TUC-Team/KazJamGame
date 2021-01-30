using UnityEngine;

public sealed class KeyState : TutorialState {
	readonly KeyData _data;

	public KeyState(KeyData data) {
		_data = data;
	}

	public override bool Update() => (_data.Any && Input.anyKey) || Input.GetKey(_data.Key);
}