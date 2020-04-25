using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float M_sensivity = 100f;
    public Transform playerBody;
    private float _localRotX = 0f;
    private float _localRotY = 0f;

    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _localRotX += Input.GetAxis("Mouse X") * M_sensivity * Time.deltaTime;
        _localRotX = _localRotX % 360;
        _localRotY += Input.GetAxis("Mouse Y") * M_sensivity * Time.deltaTime;
        _localRotY = Mathf.Clamp(_localRotY,-90f,90f);

        var angleRightLeft = Quaternion.AngleAxis(_localRotX,Vector3.up);
        var angleUpDown = Quaternion.AngleAxis(_localRotY, Vector3.left);
        playerBody.rotation = angleRightLeft;
        transform.localRotation = angleUpDown;
        
    }
}
