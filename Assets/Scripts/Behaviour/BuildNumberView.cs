using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class BuildNumberView : MonoBehaviour {
	void Start() {
		GetComponent<TMP_Text>().text = Application.version;
	}
}