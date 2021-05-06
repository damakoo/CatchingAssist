using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;
using Chronos;

public class spheretouch : MonoBehaviour
{
    [SerializeField] modechange _modechange;
    [SerializeField] OVRHand _ovrHand_right;
    [SerializeField] OVRHand _ovrHand_left;
    [SerializeField] GameObject _rightHandAnchor;
    [SerializeField] GameObject _leftHandAnchor;
    [SerializeField] private ButtonController _resetbutton;


    /*[SerializeField] GameObject _gu;
    [SerializeField] GameObject _pa;
    [SerializeField] GameObject _gu2;
    [SerializeField] GameObject _pa2;
    [SerializeField] GameObject _touch;
    [SerializeField] GameObject _nottouch;
    [SerializeField] Text _release;
    [SerializeField] Text _throwing;*/
    [SerializeField] ovrHand _handpos_right;
    [SerializeField] ovrHand _handpos_left;
    //[SerializeField] Text _y;
    private GameObject _parent;
    private Vector3 _initPosition;
    public Quaternion _initRotation;
    private bool _touchIN_Right = false;
    private bool _touchIN_Left = false;
    private bool _catch_Right = false;
    private bool _hold_Right = false;
    private bool _catch_Left = false;
    private bool _hold_Left = false;
    private bool released = true;
    public bool isorbit = false;
    public bool draworbit = false;
    public bool draworbit2 = false;
    private float throwhigh = 1500;
    public Vector3 randompos;
    public Vector3 randompower;
    [SerializeField] PhysicMaterial _bound;
    [SerializeField] PhysicMaterial _nonbound;
    private bool pressed = false;
    List<GameObject> list_orbit = new List<GameObject>();

    private void Awake()
    {
        randompos = new Vector3(-0.19f, 0.66f, 4.33f);
        randompower = new Vector3(Random.Range(-2f, 2f) * 10, 80f * Random.Range(3.5f, 3.7f), -100f * Random.Range(1.7f, 2.0f));
    }

    // Start is called before the first frame update
    void Start()
    { 
        //_gu.SetActive(false);
        //_nottouch.SetActive(false);
        _parent = transform.root.gameObject;
        _initPosition = this.transform.position;
        _initRotation = this.transform.rotation;
        resetVelocity();

        _resetbutton.ActionZoneEvent += args =>
        {
            if (!pressed)
            {
                pressed = true;
                this.GetComponent<Rigidbody>().useGravity = true;
                draworbit2 = true;
                if (_modechange.mode == 1)
                {
                    Invoke("launchball", 0.4f);
                    Invoke("orbit1", 0.50f);
                   
                }
                else if (_modechange.mode == 2)
                {
                    Invoke("launchball", 0.4f);
                    Invoke("orbit2", 0.50f);

                }
                else if (_modechange.mode == 3)
                {
                    Invoke("launchball", 0.4f);
                    Invoke("orbit3", 0.50f);
                }
                _touchIN_Right = false;
                _touchIN_Left = false;
                _catch_Right = false;
                _hold_Right = false;
                _catch_Left = false;
                _hold_Left = false;
                released = true;
                ReleaseSphere();
                Invoke("resetpress",1.9f);
            }
        };

    }
    // Update is called once per frame
    void Update()
    {
        //_y.text = throwhigh.ToString();
        _catch_Right = _ovrHand_right.GetFingerPinchStrength(OVRHand.HandFinger.Middle) >= 0.1f && _ovrHand_right.GetFingerPinchStrength(OVRHand.HandFinger.Index) >= 0.05f && _ovrHand_right.GetFingerPinchStrength(OVRHand.HandFinger.Thumb) >= 0.05f;
        _catch_Left = _ovrHand_left.GetFingerPinchStrength(OVRHand.HandFinger.Middle) >= 0.1f && _ovrHand_left.GetFingerPinchStrength(OVRHand.HandFinger.Index) >= 0.05f && _ovrHand_left.GetFingerPinchStrength(OVRHand.HandFinger.Thumb) >= 0.05f;
        touchSphere();
        if ((_catch_Right && _hold_Right) || (_catch_Left && _hold_Left))
        {
            if (released)
            {
                released = false;
                CatchSphere();
            }
        }
        else
        {
            if (!released)
            {
                ReleaseSphere();
                released = true;
            }
        }
        /*if (_catch_Left)
        {
            _gu2.SetActive(true);
            _pa2.SetActive(false);
        }
        else
        {
            _gu2.SetActive(false);
            _pa2.SetActive(true);
        }

        if (_catch_Right)
        {

            _gu.SetActive(true);
            _pa.SetActive(false);
        }
        else
        {
            _gu.SetActive(false);
            _pa.SetActive(true);
        }
        if (released)
        {
            _release.text = "release";
        }
        else
        {
            _release.text = "hold";
        }*/


    }
    void orbit1()
    {
        randompos = new Vector3(-0.19f, 0.66f, 4.33f);
        randompower = new Vector3(Random.Range(-2f, 2f) * 10, 80f * Random.Range(3.6f, 3.8f), -100f * Random.Range(1.6f, 1.8f));

    }
    void orbit2()
    {
        randompos = new Vector3(-0.19f, 0.66f, 4.33f);
        randompower = new Vector3(Random.Range((int)-10,(int)11), -10f * Random.Range((int)17,(int)22), -5f * Random.Range((int)23, (int)26));
    }
    void orbit3()
    {
        randompos = new Vector3(-0.19f, 0.02f, 4.33f);
        randompower = new Vector3(Random.Range(-2f, 2f) * 7, 0, -85f * Random.Range(1.5f, 2f));

    }
    void resetpress()
    {
        pressed = false;
    }
    void launchball()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.transform.SetPositionAndRotation(randompos, _initRotation);
        this.GetComponent<Rigidbody>().AddForce(randompower);
        //this.GetComponent<Timeline>().enabled = false;
        //this.GetComponent<Timeline>().enabled = true;

    }


    private void resetVelocity()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
   
    void touchSphere()
    {
        if (_touchIN_Right && _catch_Right)
        {
            _hold_Right = true;
        }
        if (_touchIN_Left && _catch_Left)
        {
            _hold_Left = true;
        }
    }
    void CatchSphere()
    {
        // 右手の子供になる
        if (_hold_Right)
        {
            gameObject.transform.SetParent(_rightHandAnchor.gameObject.transform);
            this.transform.localPosition = new Vector3(-0.1f, -0.05f, 0);
        }
        if (_hold_Left)
        {
            gameObject.transform.SetParent(_leftHandAnchor.gameObject.transform);
            this.transform.localPosition = new Vector3(0.1f, 0.05f, 0);
        }

        // 重力抜き　当たり判定しない
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        //this.gameObject.GetComponent<SphereCollider>().isTrigger = true;

    }

    void ReleaseSphere()
    {

        // 親関係解除
        gameObject.transform.parent = null;
        // 重力　当たり判定 復帰
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        //this.gameObject.GetComponent<SphereCollider>().isTrigger = false;
        // 位置移動可 物理復帰
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        if (_hold_Right)
        {
            _hold_Right = false;
            Vector3 _addforce = new Vector3((_handpos_right.pos[_handpos_right.length].x - _handpos_right.pos[0].x) * 600, (_handpos_right.pos[_handpos_right.length].y - _handpos_right.pos[0].y) * throwhigh, (_handpos_right.pos[_handpos_right.length].z - _handpos_right.pos[0].z) * 600);
           // this.gameObject.GetComponent<Rigidbody>().AddForce(_addforce);
            //this.gameObject.GetComponent<Rigidbody>().velocity += _addforce/10;
           //_throwing.text = _addforce.ToString();
        }
        if (_hold_Left)
        {
            _hold_Left = false;
            Vector3 _addforce = new Vector3((_handpos_left.pos[_handpos_left.length].x - _handpos_left.pos[0].x) * 600, (_handpos_left.pos[_handpos_left.length].y - _handpos_left.pos[0].y) * throwhigh, (_handpos_left.pos[_handpos_left.length].z - _handpos_left.pos[0].z) * 600);
            //this.gameObject.GetComponent<Rigidbody>().AddForce(_addforce);
            //_throwing.text = _addforce.ToString();
        }
        
        
    }
    private void touchoutleft()
    {
        _touchIN_Left = false;
        /*_touch.SetActive(false);
        _nottouch.SetActive(true);*/
    }
    private void touchoutright()
    {
        _touchIN_Right = false;
        /*_touch.SetActive(false);
        _nottouch.SetActive(true);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.name == "OVRHandPrefab_Right")
        {
            _touchIN_Right = true;
            //_touch.SetActive(true);
            //_nottouch.SetActive(false);
        }
        else if (other.transform.parent.gameObject.name == "OVRHandPrefab_Left")
        {
            _touchIN_Left = true;
            //_touch.SetActive(true);
            //_nottouch.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject.name == "OVRHandPrefab_Right")
        {
            Invoke("touchoutright", 0.1f);  
        }
        else if (other.transform.parent.gameObject.name == "OVRHandPrefab_Left")
        {
            Invoke("touchoutleft", 0.1f);
        }
    }
}