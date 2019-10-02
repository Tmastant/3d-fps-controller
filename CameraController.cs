using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minX = -60f;
    public float maxX = 60f;

    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public Camera cam;

    private float rotX = 0;
    private float rotY = 0;

    Vector3 offset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        offset = cam.transform.position;
    }

    void Update()
    {
        rotY += Input.GetAxis("Mouse X") * sensitivityY;
        rotX += Input.GetAxis("Mouse Y") * sensitivityX;

        rotX = Mathf.Clamp(rotX, minX, maxX);

        transform.localEulerAngles = new Vector3(0, rotY, 0);

        cam.transform.localEulerAngles = new Vector3(-rotX, rotY, 0);

        if(Input.GetKey(KeyCode.Escape))
        {
            if(Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LateUpdate()
    {
        cam.transform.position = transform.position + offset;
    }
}
