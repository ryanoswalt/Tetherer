using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCube : MonoBehaviour
{
    public float min;
    public float max;
    // Use this for initialization
    void Start()
    {

        min = transform.localPosition.y - 2;
        max = transform.localPosition.y + 2;

    }

    // Update is called once per frame
    void Update()
    {


        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.localPosition.z);

    }
}