using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class orbital : MonoBehaviour
{
    spheretouch _spheretouch;
    modechange _modechange;
    [SerializeField] GameObject _sphereorbital;
    [SerializeField] PhysicMaterial _bound;
    [SerializeField] PhysicMaterial _nonbound;
    Clock _clock;
    List<GameObject> list_orbit = new List<GameObject>();
    GameObject orbital_sphere;

    // Start is called before the first frame update
    private void Start()
    {
        _spheretouch = GameObject.Find("Sphere").GetComponent<spheretouch>();
        _modechange = GameObject.Find("SceneSetup/mode").GetComponent<modechange>();
        orbital_sphere = Instantiate(_sphereorbital, new Vector3(-100,-100,-100), new Quaternion(0, 0, 0, 0)) as GameObject;
        list_orbit.Add(orbital_sphere);
        _clock = Timekeeper.instance.Clock("Root");
    }

    // Update is called once per frame
    void Update()
    {

       
        //_time0 += Time.deltaTime;
        //_time += Time.deltaTime;

        if (_spheretouch.draworbit)
        {
            _spheretouch.draworbit = false;
            if (_modechange.mode == 1)
            {
                _sphereorbital.GetComponent<SphereCollider>().material = _bound;
            }
            else if (_modechange.mode == 2)
            {
                _sphereorbital.GetComponent<SphereCollider>().material = _bound;
            }
            else if (_modechange.mode == 3)
            {
                _sphereorbital.GetComponent<SphereCollider>().material = _nonbound;
            }
            Invoke("settimescaleone",1.0f);
            Invoke("settimescaletwenty",0.1f);
            if (_spheretouch.isorbit)//(_time0 > 0.001f)
            {
                Destroy(list_orbit[0]);
                list_orbit.RemoveAt(0);
                orbital_sphere = Instantiate(_sphereorbital, _spheretouch.randompos, _spheretouch._initRotation) as GameObject;
                list_orbit.Add(orbital_sphere);
                list_orbit[0].GetComponent<Rigidbody>().velocity = Vector3.zero;
                list_orbit[0].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                list_orbit[0].transform.SetPositionAndRotation (_spheretouch.randompos,_spheretouch._initRotation);
                list_orbit[0].GetComponent<Rigidbody>().AddForce(_spheretouch.randompower);
            }
            /*if (list_orbit.Count > 30)
            {
                Destroy(list_orbit[0]);
                list_orbit.RemoveAt(0);
            }*/
        }
    }

    void settimescaleone()
    {
        _clock.localTimeScale = 1f;
    }
    void settimescaletwenty()
    {
        _clock.localTimeScale = 1f;
    }
}
