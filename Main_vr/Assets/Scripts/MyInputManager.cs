using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour
{

    float speed = 10.0f;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
//         if (Input.GetAxis("Mouse X") > 0)
//         {
//             transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
//                                        0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
//         }
// 
//         else if (Input.GetAxis("Mouse X") < 0)
//         {
//             transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
//                                        0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
//         }

        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }


        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -2f, 0);
        }

        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 2f, 0);
        }

        if (Input.GetKey("up"))
        {
            transform.Rotate(-1, 0, 0);
        }

        if (Input.GetKey("down"))
        {
            transform.Rotate(+1, 0, 0);
        }


        transform.position = pos;
    }
}