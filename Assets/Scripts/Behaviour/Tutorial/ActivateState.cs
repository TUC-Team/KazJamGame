public sealed class ActivateState : TutorialState {
	readonly ActivateData _data;

	public ActivateState(ActivateData data) {
		_data = data;
	}

	public override void OnStart() {
		_data.Target.SetActive(true);
	}
}