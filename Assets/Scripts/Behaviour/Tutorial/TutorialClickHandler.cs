using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class TutorialClickHandler : MonoBehaviour {
	public event Action Clicked = () => {};

	void OnMouseUp() {
		OnClicked();
		gameObject.SetActive(false);
	}

	void OnClicked() {
		Clicked();
	}
}