using UnityEngine;

public class CameraShake : MonoBehaviour {
	[SerializeField] ChickenManager _manager;
	[SerializeField] AnimationCurve _amount;
	[SerializeField] float          _maxForce;
	[SerializeField] float          _duration;

	float   _time;
	Vector3 _originalPos;

	void Start() {
		ResetState();
		_manager.ChickenRemovedEvent += OnChickenRemoved;
	}

	void OnDestroy() {
		_manager.ChickenRemovedEvent -= OnChickenRemoved;
	}

	void OnChickenRemoved(ChickenAIBase _) {
		Shake();
	}

	void Shake() {
		ResetState();
		enabled      = true;
		_originalPos = transform.localPosition;
	}

	void Update() {
		if ( _time > _duration ) {
			ResetState();
			transform.localPosition = _originalPos;
			return;
		}
		_time += Time.deltaTime;
		var progress = _time / _duration;
		var amount   = _maxForce * _amount.Evaluate(progress);
		transform.localPosition = _originalPos + Vector3.up * amount;
	}

	void ResetState() {
		_time   = 0;
		enabled = false;
	}
}