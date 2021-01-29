using UnityEngine;

public sealed class AnyKeyState : TutorialState {
	public override bool Update() => Input.anyKey;
}