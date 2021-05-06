using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;

public class isorbital : MonoBehaviour
{
    [SerializeField] spheretouch _spheretouch;
    [SerializeField] Text _onoff;

    private bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<ButtonController>().ActionZoneEvent += args =>
        {
            if (!pressed)
            {
                pressed = true;
                if (_spheretouch.isorbit)
                {
                    _spheretouch.isorbit = false;
                    _onoff.text = "OFF";
                    _onoff.color = Color.blue;
                }
                else
                {
                    _spheretouch.isorbit = true;
                    _onoff.text = "ON";
                    _onoff.color = Color.red;
                }
                Invoke("resetpress", 1.0f);
            }
        };
    }

    void resetpress()
    {
        pressed = false;
    }

}
