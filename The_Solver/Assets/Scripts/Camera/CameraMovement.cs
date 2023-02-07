using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed;
   
    void Update()
    {
        MoveCam();   
    }

    void MoveCam()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        if(input.magnitude != 0)
            transform.position += input * speed * Time.deltaTime;
    }

}
