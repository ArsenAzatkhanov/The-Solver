using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float currentSize;
    [SerializeField] float minSize, maxSize;

    private void Start()
    {
        currentSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        MoveCam();   
    }

    void MoveCam()
    {

        Camera.main.orthographicSize = currentSize;

        currentSize -= Input.mouseScrollDelta.y;

        currentSize = Mathf.Clamp(currentSize, minSize, maxSize);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        if (input.magnitude != 0)
            transform.position += input * speed * Time.deltaTime;

    }

}
