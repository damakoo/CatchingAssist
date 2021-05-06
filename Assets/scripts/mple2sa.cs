using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;

public class mple2sa : MonoBehaviour
{


    [SerializeField] GameObject _sphere1;
    [SerializeField] sample _sample;
    GameObject _sphere3;
   

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        if (_sample.launch)
        {
            _sample.launch = false;
            Invoke("launchball1", 0.4f);
            Invoke("_OnDestroy", 3.0f);
        }
    }


    void launchball1()
    {
        _sphere3 = Instantiate(_sphere1, _sample.randompos, _sample._initRotation) as GameObject;
        _sphere3.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _sphere3.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        _sphere3.transform.SetPositionAndRotation(_sample.randompos, _sample._initRotation);
        _sphere3.GetComponent<Rigidbody>().AddForce(_sample.randompower);
    }
    
    private void _OnDestroy()
    {
        Destroy(_sphere3);
    }

}