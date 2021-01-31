using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_stop : MonoBehaviour
{
    // Start is called before the first frame update
    public AK.Wwise.Event LastPlayed { get; set; }

    private void OnDestroy()
    {
        LastPlayed.Stop(gameObject);
    }
}
