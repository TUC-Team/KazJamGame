using UnityEngine;

public sealed class ShowMessageState : TutorialState {
	readonly Transform   _messagesRoot;
	readonly MessageData _data;

	public ShowMessageState(Transform messagesRoot, MessageData data) {
		_messagesRoot = messagesRoot;
		_data         = data;
	}

	public override void OnStart() {
		_messagesRoot.GetChild(_data.Index).gameObject.SetActive(true);
	}
}