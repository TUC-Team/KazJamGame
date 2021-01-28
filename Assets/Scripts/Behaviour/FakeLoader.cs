using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeLoader : MonoBehaviour {
    void Update() {
	    if ( Input.anyKeyDown ) {
		    SceneManager.LoadScene(1);
	    }
    }
}
