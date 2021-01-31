using UnityEngine;

public class HandEffect : MonoBehaviour {
	[SerializeField] GameObject _effect;

	void Start() {
		ProjectileStandard.OnHitHappens += OnHitHappens;
	}

	void OnDestroy() {
		ProjectileStandard.OnHitHappens -= OnHitHappens;
	}

	void OnHitHappens(Vector3 point) {
		Instantiate(_effect, point, Quaternion.identity);
	}
}
