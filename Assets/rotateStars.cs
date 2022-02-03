using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateStars : MonoBehaviour
{
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Speed * Time.deltaTime);
    }
}
