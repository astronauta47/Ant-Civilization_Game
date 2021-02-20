using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;

    public float movementSpeed = 20.0f;
    public float mouseSpeed = 16.0f;
    protected float h, v, mX;
    protected Vector3 moveDir;

    float shiftSpeedMultiplier = 50.0f;
    float movementSpeedMultiplier = 20.0f;

    private float mouseMultiplier = 0.0f; // 0.0f or 1.0f only

    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        BasicMovement();
        Zooming();
    }

    void BasicMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            movementSpeed = shiftSpeedMultiplier;
        else
            movementSpeed = movementSpeedMultiplier;

        h = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        v = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Mouse2))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            mouseMultiplier = 1.0f;
        }

        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            mouseMultiplier = 0.0f;
        }

        mX = Input.GetAxis("Mouse X") * mouseMultiplier * mouseSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0.0f, mX, 0.0f));

        moveDir = new Vector3(h, 0.0f, v);
        moveDir = transform.TransformVector(moveDir);

        rb.MovePosition(transform.position + moveDir);
    }

    void Zooming()
    {
        float v = -Input.GetAxis("Mouse ScrollWheel") * 10;

        Vector3 newPos = new Vector3(cam.transform.position.x, cam.transform.position.y + v, cam.transform.position.z);

        newPos.y = Mathf.Clamp(newPos.y, 20.0f, 50.0f);

        cam.transform.position = newPos;
    }
}
