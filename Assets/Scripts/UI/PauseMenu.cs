using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	[SerializeField] GameObject _holder;

	bool _paused;

	void Update() {
		if ( Input.GetKeyDown(KeyCode.Escape) ) {
			if ( _paused ) {
				Resume();
			} else {
				Pause();
			}
			return;
		}
		if ( Input.GetKeyDown(KeyCode.Return) ) {
			if ( _paused ) {
				GoToMenu();
			}
		}
	}

	void Pause() {
		_paused = true;
		Time.timeScale = 0;
		_holder.SetActive(true);
	}

	void Resume() {
		_paused = false;
		Time.timeScale = 1;
		_holder.SetActive(false);
	}

	void GoToMenu() {
		SceneManager.LoadScene("MenuScene");
	}
}
