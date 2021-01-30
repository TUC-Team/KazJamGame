using UnityEngine;

public sealed class WaitTimeState : TutorialState {
	readonly WaitData _data;

	float _time;

	public WaitTimeState(WaitData data) {
		_data = data;
	}

	public override bool Update() {
		_time += Time.deltaTime;
		return _time > _data.Duration;
	}
}