using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class orbitalchange : MonoBehaviour
{
    [SerializeField] spheretouch _spheretouch;
    [SerializeField]modechange _modechange;
    [SerializeField] PhysicMaterial _bound;
    [SerializeField] PhysicMaterial _nonbound;
    [SerializeField] GameObject sphere1;
    [SerializeField] GameObject sphere2;
    [SerializeField] Material _clear;
    [SerializeField] Material _trail;
    GameObject sphere1_orbit;
    GameObject sphere2_orbit;
    private bool isone = false;
    //private float _time0 = 0;
    //private float _time = 0;
    //private bool _addforce = false;
    // Start is called before the first frame update
    private void Start()
    {
        //_spheretouch = GameObject.Find("Sphere").GetComponent<spheretouch>();
        //_modechange = GameObject.Find("SceneSetup/mode").GetComponent<modechange>();
        Invoke("launch1",0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_spheretouch.draworbit)
        {
            _spheretouch.draworbit = false;
            if (_modechange.mode == 1)
            {
                sphere1.GetComponent<SphereCollider>().material = _bound;
                sphere2.GetComponent<SphereCollider>().material = _bound;
            }
            else if (_modechange.mode == 2)
            {
                sphere1.GetComponent<SphereCollider>().material = _bound;
                sphere2.GetComponent<SphereCollider>().material = _bound;
            }
            else if (_modechange.mode == 3)
            {
                sphere1.GetComponent<SphereCollider>().material = _nonbound;
                sphere2.GetComponent<SphereCollider>().material = _nonbound;
            }
            if (isone)
            {
                isone = false;
                Invoke("launch1", 0.2f);
                Invoke("fadeorbit2", 0f);
            }
            else
            {
                isone = true;
                Invoke("launch2", 0.2f);
                Invoke("fadeorbit1", 0f);
            }
        }

        if (_spheretouch.draworbit2)
        {
            _spheretouch.draworbit2 = false;
            if (!isone)
            {
                if (_spheretouch.isorbit)
                {
                    sphere1_orbit.gameObject.GetComponent<TrailRenderer>().material = _trail;
                }
                Invoke("fadeorbit1", 2.5f);
                isone = true;
                Invoke("launch2", 0.6f);
            }
            else
            {
                if (_spheretouch.isorbit)
                {
                    sphere2_orbit.gameObject.GetComponent<TrailRenderer>().material = _trail;
                }
                Invoke("fadeorbit2", 2.5f);
                isone = false;
                Invoke("launch1", 0.6f);
            }
        }
    }

    void make1()
    {
        sphere1_orbit = Instantiate(sphere1, _spheretouch.randompos, _spheretouch._initRotation) as GameObject;

    }
    void launch1()
    {
        sphere1_orbit = Instantiate(sphere1, _spheretouch.randompos, _spheretouch._initRotation) as GameObject;
        sphere1_orbit.GetComponent<Rigidbody>().velocity = Vector3.zero;
        sphere1_orbit.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        sphere1_orbit.transform.SetPositionAndRotation(_spheretouch.randompos, _spheretouch._initRotation);
        sphere1_orbit.GetComponent<Rigidbody>().AddForce(_spheretouch.randompower);

    }
    void make2()
    {
        sphere2_orbit = Instantiate(sphere2, _spheretouch.randompos, _spheretouch._initRotation) as GameObject;

    }
    void launch2()
    {
        sphere2_orbit = Instantiate(sphere2, _spheretouch.randompos, _spheretouch._initRotation) as GameObject;
        sphere2_orbit.GetComponent<Rigidbody>().velocity = Vector3.zero;
        sphere2_orbit.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        sphere2_orbit.transform.SetPositionAndRotation(_spheretouch.randompos, _spheretouch._initRotation);
        sphere2_orbit.GetComponent<Rigidbody>().AddForce(_spheretouch.randompower);

    }

    void fadeorbit1()
    {
        sphere1_orbit.gameObject.GetComponent<TrailRenderer>().material = _clear;
        Destroy(sphere1_orbit);
    }
    void fadeorbit2()
    {
        sphere2_orbit.gameObject.GetComponent<TrailRenderer>().material = _clear;
        Destroy(sphere2_orbit);
    }
}
