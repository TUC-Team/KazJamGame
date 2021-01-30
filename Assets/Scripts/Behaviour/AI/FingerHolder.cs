using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerHolder : MonoBehaviour {
	[SerializeField] GameObject[] _fingers;

	int _currentIndex = 0;

	public bool TryRemoveFinger() {
		if ( _currentIndex > _fingers.Length - 1 ) {
			return false;
		}
		_fingers[_currentIndex].SetActive(false);
		_currentIndex++;
		return true;
	}
}
