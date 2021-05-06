using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballsize : MonoBehaviour
{
    public float ballsizevalue = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = (Mathf.Round(ballsizevalue*100) / 10f).ToString();
    }
}
