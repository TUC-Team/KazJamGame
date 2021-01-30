public sealed class WaitClickState : TutorialState {
	readonly ClickData _data;

	bool _isClicked;

	public WaitClickState(ClickData data) {
		_data = data;
		_data.Target.Clicked += OnClicked;
	}

	void OnClicked() {
		_isClicked = true;
		_data.Target.Clicked -= OnClicked;
	}

	public override bool Update() => _isClicked;
}