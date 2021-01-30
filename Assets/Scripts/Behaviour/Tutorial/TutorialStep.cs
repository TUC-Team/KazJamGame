using System;

[Serializable]
public sealed class TutorialStep {
	public TutorialStepMode Mode;
	public TransitionData   Transition;
	public WaitData         Wait;
	public MessageData      Message;
	public ClickData        Click;
	public ActivateData     Activate;
	public SceneData        Scene;
	public KeyData          Key;
}