using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 50f;
    private bool MousePointerToggled = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            if(MousePointerToggled)
             Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            MousePointerToggled = !MousePointerToggled;
        }

        if (Input.GetAxis("Vertical") != 0|| Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"))*Speed*Time.deltaTime);
        }
    }
}
