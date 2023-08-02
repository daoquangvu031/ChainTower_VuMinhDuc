using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHealth : MonoBehaviour
{
    public Transform camTransform;

    public void Start()
    {
        camTransform = Camera.main.transform;
    }
    void LateUpdate()
    {
        transform.forward = camTransform.forward;
    }
}
