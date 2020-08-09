using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public const float mainOffset = 5;
    public const float offsetY = 2;
    public Vector3 offset = new Vector3();
    public Vector3 offsetNew = new Vector3(0, offsetY, - mainOffset);
    public float speed = 10;
    public float smooth = 3;
    public bool isFreeCamera = false;
    bool isMouseKeyPresed = false;
    Vector3 oldPos = new Vector2();
    float maxdis = 10;
    
    void Start()
    {
        
    }

    void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isMouseKeyPresed = true;
            oldPos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isMouseKeyPresed = false;
        }
        if(!isFreeCamera)
        {
            offset.x = -Mathf.Sin(target.rotation.eulerAngles.y*Mathf.Deg2Rad) * mainOffset;
            offset.y = offsetY;
            offset.z = -Mathf.Cos(target.rotation.eulerAngles.y*Mathf.Deg2Rad) * mainOffset;
        }
        else
        {
            if(isMouseKeyPresed)
            {
                offsetNew.y +=MouseDelta().y * 0.0001f;
            }
            offset = offsetNew;
        }
        Vector3 look = target.position - this.transform.position;
        this.transform.position = Vector3.Lerp(this.transform.position, target.position + offset, smooth*Time.deltaTime);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(look), speed * Time.deltaTime);
    }

    public Vector3 MouseDelta()
    {
        Vector3 delta = new Vector3();
        /*if (Input.GetMouseButtonDown(0) && !isMouseKeyPresed)
        {
            isMouseKeyPresed = true;
            oldPos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isMouseKeyPresed = false;
            delta = Input.mousePosition - oldPos;
        }*/
        delta = Input.mousePosition - oldPos;
        return delta;
    }
}
