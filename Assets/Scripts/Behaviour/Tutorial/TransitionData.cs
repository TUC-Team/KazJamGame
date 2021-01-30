using System;
using UnityEngine;

[Serializable]
public sealed class TransitionData {
	public Transform      Target;
	public Transform      Start;
	public Transform      Finish;
	public AnimationCurve Curve;
	public float          Duration;
}