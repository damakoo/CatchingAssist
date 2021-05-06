using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Chronos;
using OculusSampleFramework;

public class control_timescale_plus : MonoBehaviour
{
    [SerializeField] displaytimescale _displaytimescale;
    private bool pressed = false;
    [SerializeField] Text _text;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<ButtonController>().ActionZoneEvent += args =>
        {
            if (!pressed)
            {
                pressed = true;
                if(_displaytimescale.version_timescale == 1)
                {
                    _displaytimescale.version_timescale = 2;
                    Time.timeScale = 0.825f;
                    _text.text = "slow";
                }
                else if (_displaytimescale.version_timescale == 2)
                {
                    _displaytimescale.version_timescale = 3;
                    Time.timeScale = 0.65f;
                    _text.text = "very slow";
                }
                Invoke("resetpress", 0.5f);

            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void resetpress()
    {
        pressed = false;
    }
}
