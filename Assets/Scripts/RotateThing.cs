using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThing : MonoBehaviour
{
    public Vector3 rotAxis;
    public float rotSpeed;

    private void Update()
    {
        gameObject.transform.Rotate(rotAxis, rotSpeed * Time.deltaTime);
    }
}
