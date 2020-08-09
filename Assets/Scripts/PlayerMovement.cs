using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float step = 10;
    public float rotationSpeed = 2;
    public float jump = 10;
    private float _direction = 0;
    private float _movement = step;
    public float rotationAmount = 2;
    public MovementType movementType;

    void Start()
    {
        
    }

    void Update()
    {

        if(Input.GetKey("space"))
            this.gameObject.GetComponent<Rigidbody>().AddForce(0,jump,0);

        if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            switch(movementType)
            {
                case MovementType.CAMERA: CameraModel(); break;
                case MovementType.RACE: RaceModel();  break;
                default: break; 
            }
            Motion(_direction, _movement);
        }
        else
        {
            _direction = transform.eulerAngles.y;
        }
        /*if(!IsRotationFinished(transform.eulerAngles.y, _direction))
        {
            Motion(_direction, 0);
            Debug.Log(transform.eulerAngles.y - _direction);
        }*/
    }

    private void CameraModel()
    {
        float dir = 0f;
        float count = 0;
        if(Input.GetKey("a"))
        {
            count++;
            dir = -90;
        }
        if(Input.GetKey("d"))
        {
            count++;
            dir = 90;
        }
        if(Input.GetKey("w"))
        {
            count++;
            dir = dir / 2;
        }
        if(Input.GetKey("s"))
        {
            count++;
            dir = (180 * count)+(dir + dir/2);
        }
        _direction = dir;
    }

    private void RaceModel()
    {
        float forvard=0;
        float dir = 0f;

        if(Input.GetKey("a"))
        {
            dir -= rotationAmount;
        }
        if(Input.GetKey("d"))
        {
            dir += rotationAmount;
        }
        if(Input.GetKey("w"))
        {
            forvard = step;
        }
        if(Input.GetKey("s"))
        {
            forvard = -step;
        }
        _direction += dir;
        _movement = forvard;
    }

    private void Motion(float rotation = 0f, float position = 0f) //rotation for dir, position for forvard amount
    {
        Quaternion lookdir = new Quaternion(); 
        lookdir.eulerAngles = new Vector3(0,rotation,0);
        this.transform.localRotation = Quaternion.Lerp(transform.rotation, lookdir, rotationSpeed*Time.deltaTime);
        this.transform.Translate(new Vector3(0,0,position*Time.deltaTime), Space.Self);
    }

    private bool IsRotationFinished(float actualRotation, float destrotation) => Mathf.Abs(actualRotation - destrotation) < 5f ? true : false;

    public enum MovementType
    {
        CAMERA,
        RACE
    }
}
