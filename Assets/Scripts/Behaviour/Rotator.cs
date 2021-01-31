using UnityEngine;

public class Rotator : MonoBehaviour {
	[SerializeField] Vector3 _axis;
	[SerializeField] float   _force;
	[SerializeField] float   _change;


	float _curForce;

	void Update() {
		var isShooting = Input.GetMouseButton(0);
		var nextForce  = _curForce + (isShooting ? _change : -_change);
		_curForce = Mathf.Min(Mathf.Max(nextForce, 0), _force);
		var value = _curForce * Time.deltaTime;
		transform.Rotate(_axis, -value);
	}
}
