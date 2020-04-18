using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    private Vector3 mousePosRef;
    private float prevRot = 0f;
    public Transform cameraTarget;
    public float MoveSpeed = 1f;
    public float RotateSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        var posXIncr = (Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed);
        var posZIncr = (Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed);
        cameraTarget.position += cameraTarget.forward * posZIncr + cameraTarget.right * posXIncr;
        if (Input.GetButtonDown("Fire2"))
        {
            mousePosRef = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        } 
        
        if (Input.GetButton("Fire2"))
        {
            var rotVec = new Vector3(mousePosRef.y - Input.mousePosition.y, mousePosRef.x - Input.mousePosition.x, 0f);
            var rotation = Mathf.LerpAngle(prevRot, Mathf.Atan2(rotVec.y, rotVec.x) * Mathf.Rad2Deg, Time.deltaTime );
            cameraTarget.Rotate(Vector3.up, rotation * Time.deltaTime * RotateSpeed);
            prevRot = rotation;
        } else
        {
            prevRot = 0;
        }
    }
}
