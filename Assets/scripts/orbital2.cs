using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;


public class orbital2 : MonoBehaviour
{
    spheretouch _spheretouch;
    modechange _modechange;
    [SerializeField] GameObject _sphereorbital;

    List<GameObject> list_orbit = new List<GameObject>();
    GameObject orbital_sphere;

    //private float _time0 = 0;
    //private float _time = 0;
    //private bool _addforce = false;
    // Start is called before the first frame update
    private void Start()
    {
        _spheretouch = GameObject.Find("Sphere").GetComponent<spheretouch>();
        _modechange = GameObject.Find("SceneSetup/mode").GetComponent<modechange>();
        orbital_sphere = Instantiate(_sphereorbital, new Vector3(-100, -100, -100), new Quaternion(0, 0, 0, 0)) as GameObject;
        list_orbit.Add(orbital_sphere);

    }

    // Update is called once per frame
    void Update()
    {
        if (_spheretouch.draworbit)
        {
            //_spheretouch.draworbit = false;
            if (_spheretouch.isorbit)//(_time0 > 0.001f)
            {
                Destroy(list_orbit[0]);
                list_orbit.RemoveAt(0);
                orbital_sphere = Instantiate(_sphereorbital, _spheretouch.randompos, _spheretouch._initRotation) as GameObject;
                list_orbit.Add(orbital_sphere);
                list_orbit[0].GetComponent<Rigidbody>().velocity = Vector3.zero;
                list_orbit[0].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                list_orbit[0].transform.SetPositionAndRotation(_spheretouch.randompos, _spheretouch._initRotation);
                list_orbit[0].GetComponent<Rigidbody>().AddForce(_spheretouch.randompower);
            }
            /*if (list_orbit.Count > 30)
            {
                Destroy(list_orbit[0]);
                list_orbit.RemoveAt(0);
            }*/
        }
    }
}
