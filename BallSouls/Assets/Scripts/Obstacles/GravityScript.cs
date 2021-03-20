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
    public bool isObstacle;
    public float acceleration = 2;
    float obstacleForce;
    [SerializeField] float firstObstacleForceValue = 10;
    [SerializeField] float secondOstacleForceValue = 20;
    [SerializeField] float defaultChangeForceTime = 3;
    float changeForceTime;

    #region old Variables
    //public float forwardAcceleration = 2;
    //public float backAcceleration = 2;
    //public float upAcceleration = 2;
    //public float DownAcceleration = 2;
    //public float leftAcceleration = 2;
    //public float rightAcceleration = 2;
    #endregion

    private void Start()
    {
        _rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        changeForceTime = defaultChangeForceTime;

        obstacleForce = Random.Range(firstObstacleForceValue, secondOstacleForceValue);
    }

    private void Update()
    {
        if (defaultChangeForceTime != -1)
            changeForceTime -= Time.deltaTime;

        if (changeForceTime <= 0)
        {
            obstacleForce = Random.Range(firstObstacleForceValue, secondOstacleForceValue);
            changeForceTime = defaultChangeForceTime;
        }
    }

    private void AddCharacterForce()
    {
        ForceMode forceMode;
        float force;

        if (isObstacle)
        {
            force = obstacleForce;
            forceMode = ForceMode.Impulse;
        }
        else
        {
            force = acceleration;
            forceMode = ForceMode.Acceleration;
        }

        if (isForward)
            _rb.AddForce(Vector3.forward * force, forceMode);
        if (isBack)
            _rb.AddForce(Vector3.back * force, forceMode);
        if (isUp)
            _rb.AddForce(Vector3.up * force, forceMode);
        if (isRight)
            _rb.AddForce(Vector3.right * force, forceMode);
        if (isLeft)
            _rb.AddForce(Vector3.left * force, forceMode);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "player" && isObstacle)
            AddCharacterForce();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.ToLower() == "player" && !isObstacle)
        {
            _rb.useGravity = false;

            AddCharacterForce();
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
