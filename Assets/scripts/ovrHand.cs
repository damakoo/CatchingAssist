using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovrHand : MonoBehaviour
{
    public int length = 3;
    public List<Vector3> pos = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < length + 1; i++)
        {
            pos.Add(Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pos.RemoveAt(0);
        pos.Add(this.gameObject.transform.position);
    }
}
