using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
using OculusSampleFramework;
using UnityEngine.UI;

public class control_timescale_minus : MonoBehaviour
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
                if (_displaytimescale.version_timescale == 3)
                {
                    _displaytimescale.version_timescale = 2;
                    Time.timeScale = 0.825f;
                    _text.text = "slow";
                }
                else if (_displaytimescale.version_timescale == 2)
                {
                    _displaytimescale.version_timescale = 1;
                    Time.timeScale = 1.0f;
                    _text.text = "normal";
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
