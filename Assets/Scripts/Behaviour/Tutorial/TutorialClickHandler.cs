using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public sealed class TutorialClickHandler : MonoBehaviour {
	public event Action Clicked = () => {};

	public UnityEvent OnClickedEvent = new UnityEvent();

	void OnMouseUp() {
		OnClicked();
		gameObject.SetActive(false);
	}

	void OnClicked() {
		Clicked();
		OnClickedEvent?.Invoke();
	}
}