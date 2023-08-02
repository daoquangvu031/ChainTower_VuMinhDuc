using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoth : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    [SerializeField] private float currentCameraHeight;

    private Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;


    private void Awake()
    {
        offset = transform.position - target.position;
    }
    public void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.y = currentCameraHeight;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

    }
}
