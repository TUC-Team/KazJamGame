using System;
using UnityEngine;

public sealed class TutorialManager : MonoBehaviour {
	[SerializeField] Transform      _messagesRoot;
	[SerializeField] TutorialStep[] _steps;

	int _currentStepIndex = -1;
	TutorialState _currentState;

	void Update() {
		if ( _currentState == null ) {
			if ( !TryGetNextStep(out var nextStep) ) {
				Debug.Log("No steps found");
				return;
			}
			_currentState = CreateState(nextStep);
			Debug.Log($"New state is {_currentState.GetType().Name}");
			_currentState.OnStart();
			return;
		}
		if ( !_currentState.Update() ) {
			return;
		}
		Debug.Log($"Finish state {_currentState.GetType().Name}");
		_currentState.OnFinish();
		_currentState = null;
	}

	bool TryGetNextStep(out TutorialStep step) {
		_currentStepIndex++;
		if ( _currentStepIndex < _steps.Length ) {
			step = _steps[_currentStepIndex];
			return true;
		}
		step = null;
		return false;
	}

	TutorialState CreateState(TutorialStep step) {
		switch ( step.Mode ) {
			case TutorialStepMode.Transition:
				return new TransitionState(step.Transition);
			case TutorialStepMode.ShowMessage:
				return new ShowMessageState(_messagesRoot, step.Message);
			case TutorialStepMode.HideMessage:
				return new HideMessageState(_messagesRoot, step.Message);
			case TutorialStepMode.WaitClick:
				return new WaitClickState(step.Click);
			case TutorialStepMode.Activate:
				return new ActivateState(step.Activate);
			case TutorialStepMode.WaitTime:
				return new WaitTimeState(step.Wait);
			case TutorialStepMode.GoToGame:
				return new GoToGameState();
			default:
				throw new ArgumentOutOfRangeException(nameof(step), step.Mode.ToString());
		}
	}
}