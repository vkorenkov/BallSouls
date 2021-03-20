using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    Rigidbody _rb;

    public bool isForward;
    public bool isBack;
    public bool isUp;
    public bool isDown;
    public bool isRight;
    public bool isLeft;
    public float forwardAcceleration = 2;
    public float backAcceleration = 2;
    public float upAcceleration = 2;
    public float DownAcceleration = 2;
    public float leftAcceleration = 2;
    public float rightAcceleration = 2;

    private void Start()
    {
        _rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag.ToLower() == "player")
    //    {
            
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.ToLower() == "player")
        {
            _rb.useGravity = false;

            if (isForward)
                _rb.AddForce(Vector3.forward * upAcceleration);
            if (isBack)
                _rb.AddForce(Vector3.back * rightAcceleration);
            if (isUp)
                _rb.AddForce(Vector3.up * upAcceleration);
            if (isRight)
                _rb.AddForce(Vector3.right * rightAcceleration);
            if (isLeft)
                _rb.AddForce(Vector3.left * leftAcceleration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.ToLower() == "player")
        {
            _rb.useGravity = true;
        }
    }
}
