using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public string SceneName;

	public void Load() {
		SceneManager.LoadScene(SceneName);
	}
}
