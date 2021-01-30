using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseAudioSystem : MonoBehaviour
{
    public AK.Wwise.Event weapon_start;
    public AK.Wwise.Event weapon_stop;
    private bool _shotFired;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartShootingSerie()
    {
        if (!_shotFired)
        {
            _shotFired = true;
            weapon_start.Post(gameObject);
        }
    }

    public void StopShootingSerie()
    {
        if (_shotFired)
        {
            // play sound
            _shotFired = false;
            weapon_stop.Post(gameObject);
        }
    }
}
