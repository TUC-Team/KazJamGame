using UnityEngine;

public static class CursorHelper {
	public static void EnableFpsMode() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible   = false;
	}

	public static void DisableFpsMode() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible   = true;
	}
}