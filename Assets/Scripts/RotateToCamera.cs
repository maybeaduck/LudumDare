using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zlodey;

public class RotateToCamera : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    void LateUpdate()
    {
        if (!_camera) _camera = Service<SceneData>.Get().Camera;

        var worldPosition = transform.position + _camera.transform.rotation * Vector3.back;
        var worldUp = _camera.transform.rotation * Vector3.up;

        transform.LookAt(worldPosition, worldUp);
    }
}
