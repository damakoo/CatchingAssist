using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class ballseize_minus : MonoBehaviour
{

    [SerializeField] GameObject _sphere;
    [SerializeField] ballsize _ballsize;
    [SerializeField] GameObject _sphere_orbital;
    [SerializeField] GameObject _sphere_orbital2;
    private bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<ButtonController>().ActionZoneEvent += args =>
        {
            if (!pressed)
            {
                pressed = true;
                _ballsize.ballsizevalue -= 0.01f;
                _sphere.gameObject.transform.localScale = new Vector3(1, 1, 1) * _ballsize.ballsizevalue;
                _sphere_orbital.gameObject.transform.localScale = new Vector3(1, 1, 1) * _ballsize.ballsizevalue;
                _sphere_orbital2.gameObject.transform.localScale = new Vector3(1, 1, 1) * _ballsize.ballsizevalue;
                Invoke("resetpress", 0.5f);

            }
        };
    }
    void resetpress()
    {
        pressed = false;
    }
}
