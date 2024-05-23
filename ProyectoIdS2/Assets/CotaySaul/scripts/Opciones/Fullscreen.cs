using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fullscreen : MonoBehaviour
{
    public Toggle toggle;
    private void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }
    public void Update()
    {
        
    }
    public void FullscreenActivate(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}
