using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShakeCamera : MonoBehaviour
{
    private ShakeCameraAnimation _shakeCamera;

    private void Start()
    {
        _shakeCamera = Camera.main.GetComponent<ShakeCameraAnimation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _shakeCamera.ShakeCamera();
    }
}
