using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_music : MonoBehaviour
{
    public AK.Wwise.Event menu_music_ev;

    public void load ()
    {

        menu_music_ev.Post(gameObject);

    }
}
