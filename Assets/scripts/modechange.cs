using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;

public class modechange : MonoBehaviour
{

    public int mode = 3;
    [SerializeField] PhysicMaterial _bound;
    [SerializeField] PhysicMaterial _nonbound;
    [SerializeField] Text _nowmode;
    [SerializeField] SphereCollider _sphereCollider;
    [SerializeField] spheretouch _spheretouch;
    private bool pressed = false;
    
    // Start is called before the first frame update
    void Start()
    {

        this.gameObject.GetComponent<ButtonController>().ActionZoneEvent += args =>
        {
            if (!pressed)
            {
                pressed = true;
                _spheretouch.draworbit = true;
                if (mode == 1)
                {
                    mode = 2;
                    _spheretouch.randompos = new Vector3(-0.19f, 0.66f, 4.33f);
                    _spheretouch.randompower = new Vector3(Random.Range(-2f, 2f) * 10, -100f * Random.Range(1.4f, 1.6f), -100f * Random.Range(1.9f, 2.1f));
                    _nowmode.text = "Bound";
                    _sphereCollider.material = _bound;
                }
                else if (mode == 2)
                {
                    mode = 3;
                    _spheretouch.randompos = new Vector3(-0.19f, 0.02f, 4.33f);
                    _spheretouch.randompower = new Vector3(Random.Range(-2f, 2f) * 10, 0, -100f * Random.Range(1.5f, 2f));
                    _nowmode.text = "Rolling";
                    _sphereCollider.material = _nonbound;
                }
                else if (mode == 3)
                {
                    mode = 1;
                    _spheretouch.randompos = new Vector3(-0.19f, 0.66f, 4.33f);
                    _spheretouch.randompower = new Vector3(Random.Range(-2f, 2f) * 10, 80f * Random.Range(3.5f, 3.7f), -100f * Random.Range(1.7f, 2.0f));
                    _nowmode.text = "Fly";
                    _sphereCollider.material = _bound;
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