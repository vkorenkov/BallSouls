using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraAnimation : MonoBehaviour
{
    //[SerializeField] Transform _camera;
    private Quaternion _startRotation;
    private Quaternion? _targetRotation;
    private float _degVelocity;
    public static bool _isShaked;
    private Coroutine _shakeCoroutine;
    [SerializeField, Range(0, 10)] float duration = 0;
    [SerializeField, Range(1, 10)] float maxAngle = 5;
    [SerializeField, Range(1, 100)] float degVelocity = 10;

    private void Start()
    {
        _startRotation = /*_camera.*/transform.localRotation;
    }

    private void Update()
    {
        Quaternion target = _targetRotation == null ? _startRotation : _targetRotation.Value;

        if(target == /*_camera.*/transform.localRotation)
        {
            return;
        }

        float t = (Time.deltaTime * _degVelocity) / Quaternion.Angle(/*_camera.*/transform.localRotation, target);
        /*_camera.*/transform.localRotation = Quaternion.Lerp(/*_camera.*/transform.localRotation, target, t);

        if(/*_camera.*/transform.localRotation == _targetRotation)
        {
            _targetRotation = null;
        }
    }

    public void ShakeCamera()
    {
        if (_shakeCoroutine != null)
        {
            StopCoroutine(_shakeCoroutine);
        }

        _isShaked = false;

        _shakeCoroutine = StartCoroutine(VibrateCameraCoroutine(duration, maxAngle, degVelocity));
    }

    public void ShakeRotateCamera(Vector2 direction, float angleDeg, float degVelocity)
    {
        if(_isShaked)
        {
            return;
        }

        ShakeRotateCameraInternal(direction, angleDeg, degVelocity);
    }

    private void ShakeRotateCameraInternal(Vector2 direction, float angleDeg, float degVelocity)
    {
        _degVelocity = degVelocity;
        direction = direction.normalized;
        direction *= Mathf.Tan(angleDeg * Mathf.Deg2Rad);
        Vector3 resDirection = ((Vector3)direction + /*_camera.*/transform.forward).normalized;
        _targetRotation = Quaternion.FromToRotation(/*_camera.*/transform.forward, resDirection);
    }

    private IEnumerator VibrateCameraCoroutine(float duration, float maxAngle, float degVelocity)
    {
        _isShaked = true;

        float elapsed = 0;
        float timePassed = Time.realtimeSinceStartup;

        while(elapsed < duration)
        {
            float curentTime = Time.realtimeSinceStartup;
            elapsed += curentTime - timePassed;
            timePassed = curentTime;

            ShakeRotateCameraInternal(Random.insideUnitSphere, Random.Range(0, maxAngle), degVelocity);

            yield return new WaitForSeconds(0.05f);
        }

        _isShaked = false;
    }
}
