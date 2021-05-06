using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;

public class sample : MonoBehaviour
{

    private Vector3 _initPosition;
    public Quaternion _initRotation;
    public Vector3 randompos;
    public Vector3 randompower;
    [SerializeField] GameObject _sphere1;
    [SerializeField] GameObject _sphere2;
    GameObject _sphere; 
    public float _time;
    public bool launch = false;

    private void Awake()
    {
        randompos = new Vector3(-0.19f, 0.66f, 4.33f);
        randompower = new Vector3(Random.Range(-2f, 2f) * 10, 80f * Random.Range(3.5f, 3.7f), -100f * Random.Range(1.7f, 2.0f));

    }

    // Start is called before the first frame update
    void Start()
    {
        _initPosition = _sphere1.transform.position;
        _initRotation = _sphere1.transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if(_time > 4)
        {
            launch = true;
            _time = 0;
            Invoke("launchball1", 0.4f);
            Invoke("launchball2", 0.4f);
            Invoke("orbit2", 0.60f);
            Invoke("_OnDestroy", 3.0f);
        }
    }
    void orbit1()
    {
        randompos = new Vector3(-0.19f, 0.66f, 4.33f);
        randompower = new Vector3(Random.Range(-2f, 2f) * 10, 80f * Random.Range(3.5f, 3.7f), -100f * Random.Range(1.7f, 2.0f));

    }
    void orbit2()
    {
        randompos = new Vector3(-0.19f, 0.66f, 4.33f);
        randompower = new Vector3(Random.Range((int)-10, (int)11), -10f * Random.Range((int)16, (int)21), -5f * Random.Range((int)23, (int)26));

    }
    void orbit3()
    {
        randompos = new Vector3(-0.19f, 0.02f, 4.33f);
        randompower = new Vector3(Random.Range(-2f, 2f) * 7, 0, -70f * Random.Range(1.5f, 2f));

    }

    void launchball1()
    {
        _sphere = Instantiate(_sphere1, randompos, _initRotation) as GameObject;
        _sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        _sphere.transform.SetPositionAndRotation(randompos, _initRotation);
        _sphere.GetComponent<Rigidbody>().AddForce(randompower);
    }
    void launchball2()
    {
        _sphere2.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _sphere2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        _sphere2.transform.SetPositionAndRotation(randompos, _initRotation);
        _sphere2.GetComponent<Rigidbody>().AddForce(randompower);
    }
    private void _OnDestroy()
    {
        Destroy(_sphere);
    }

}