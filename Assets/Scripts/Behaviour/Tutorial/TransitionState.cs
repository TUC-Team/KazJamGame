using UnityEngine;

public sealed class TransitionState : TutorialState {
	readonly TransitionData _data;

	float _time;

	Vector3 StartPosition  => _data.Start.transform.position;
	Vector3 FinishPosition => _data.Finish.transform.position;

	public TransitionState(TransitionData data) {
		_data = data;
	}

	public override void OnStart() {
		Move(StartPosition);
	}

	public override bool Update() {
		_time += Time.deltaTime;
		var progress = _time / _data.Duration;
		var t        = _data.Curve.Evaluate(progress);
		Move(Vector3.Lerp(StartPosition, FinishPosition, t));
		return _time >= _data.Duration;
	}

	public override void OnFinish() {
		Move(FinishPosition);
	}

	void Move(Vector3 newPosition) {
		_data.Target.transform.position = newPosition;
	}
}