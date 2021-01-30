using UnityEngine.SceneManagement;

public sealed class LoadSceneState : TutorialState {
	readonly SceneData _data;

	public LoadSceneState(SceneData data) {
		_data = data;
	}

	public override void OnFinish() {
		SceneManager.LoadScene(_data.Name);
	}
}