using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_music : MonoBehaviour
{
    public AK.Wwise.Event navedenie;
    public AK.Wwise.Event click;
    public void button_click()
    {
        
        click.Post(gameObject);
    }
    public void button_enter()
    {
        navedenie.Post(gameObject);

    }
}
