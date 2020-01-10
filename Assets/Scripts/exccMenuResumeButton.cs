using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exccMenuResumeButton : MonoBehaviour
{
    public void resume ()
    {
        globals.escapeMenu.gameObject.GetComponent<escMenuHandler>().toggle();
    }
}

