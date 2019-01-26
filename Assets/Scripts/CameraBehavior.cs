using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBehavior : MonoBehaviour
{
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 posRelativeToPoint = new Vector3(1f, 2.5f, 0.7f);

    public Vector3 CameraPosition {get;set;}

    public Quaternion CameraRotation { get; set; }




    public void Start()
    {
        initialCameraPosition = transform.position;
        initialCameraRotation = transform.rotation;

        CameraPosition = initialCameraPosition;
        CameraRotation = initialCameraRotation;
    }

    public void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, CameraPosition, ref velocity, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, CameraRotation, 0.1f);
    }

    public void ResetCamera()
    {
        CameraPosition = initialCameraPosition;
        CameraRotation = initialCameraRotation;
    }
    public void SetCameraOverTransform(Transform point)
    {
        CameraPosition = point.position + posRelativeToPoint;
        CameraRotation = Quaternion.Euler(initialCameraRotation.eulerAngles+new Vector3(30f,0,0));
    }
}
