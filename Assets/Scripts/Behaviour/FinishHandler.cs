using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishHandler : MonoBehaviour {
	public void OnFinish(bool success) {
		var sceneName = success ? "WinScene" : "LoseScene";
		SceneManager.LoadScene(sceneName);
	}
}
