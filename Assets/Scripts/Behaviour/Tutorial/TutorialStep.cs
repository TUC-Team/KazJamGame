using System;

[Serializable]
public sealed class TutorialStep {
	public string name;

	public TutorialStepMode Mode;
	public TransitionData   Transition;
	public WaitData         Wait;
	public MessageData      Message;
	public ClickData        Click;
	public ActivateData     Activate;
	public SceneData        Scene;
	public KeyData          Key;

	public void Refresh() {
		name = ToString();
	}

	public override string ToString() {
		switch ( Mode ) {
			case TutorialStepMode.Transition:
				return $"{Mode} ({Transition.Start} - {Transition.Finish})";
			case TutorialStepMode.ShowMessage:
				return $"{Mode} ({Message.Index})";
			case TutorialStepMode.HideMessage:
				return $"{Mode} ({Message.Index})";
			case TutorialStepMode.WaitClick:
				return $"{Mode} ({Click.Target})";
			case TutorialStepMode.Activate:
				return $"{Mode} ({Activate.Target})";
			case TutorialStepMode.WaitTime:
				return $"{Mode} ({Wait.Duration})";
			case TutorialStepMode.LoadScene:
				return $"{Mode} ({Scene.Name})";
			case TutorialStepMode.PressKey:
				return $"{Mode} ({Key.Any}, {Key.Key})";
		}
		return string.Empty;
	}
}