using UnityEngine.SceneManagement;

public sealed class GoToGameState : TutorialState {
	public override void OnFinish() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}