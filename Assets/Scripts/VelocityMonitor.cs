using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMonitor : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.velocity = velocity;
    }
}
