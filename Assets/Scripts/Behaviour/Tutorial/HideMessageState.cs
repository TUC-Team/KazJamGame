using UnityEngine;

public sealed class HideMessageState : TutorialState {
	readonly Transform   _messagesRoot;
	readonly MessageData _data;

	public HideMessageState(Transform messagesRoot, MessageData data) {
		_messagesRoot = messagesRoot;
		_data         = data;
	}

	public override void OnStart() {
		_messagesRoot.GetChild(_data.Index).gameObject.SetActive(false);
	}
}