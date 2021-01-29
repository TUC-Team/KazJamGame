using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TMP_Text))]
public class BuildNumberView : MonoBehaviour {
	void Start() {
		GetComponent<TMP_Text>().text = Application.version;
	}


}