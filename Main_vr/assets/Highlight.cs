using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {


    Camera camera;
    Color defaultColor;
    Color effectColor;
    

	// Use this for initialization
	void Start () {
        //defaultColor = new Color(1, 1, 1);
        //effectColor = new Color(255, 255, 0);
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        var mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;
        Screen.lockCursor = true;

        /*
        RaycastHit hit;
        if (Physics.Raycast(camera.ViewportPointToRay(transform.c), out hit))
        {
            //Debug.Log("Ou yeah!");
            gameObject.GetComponent<MeshRenderer>().material.color = effectColor;
        }
        */
        //if (Physics.Raycast(CameraCenter, transform.forward, 100))
        //{
        //Debug.Log("Ou yeah!");
        //gameObject.GetComponent<MeshRenderer>().material.color = effectColor;
        // }

        //OtherPointerEnter


        /*
        Vector3 CameraCenter = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));

        //;
        RaycastHit hit;
        if (Physics.Raycast(camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)), out hit))
        {
            //Debug.Log("Ou yeah!");
            gameObject.GetComponent<MeshRenderer>().material.color = effectColor;
        }
            //if (Physics.Raycast(CameraCenter, transform.forward, 100))
        //{
            //Debug.Log("Ou yeah!");
            //gameObject.GetComponent<MeshRenderer>().material.color = effectColor;
       // }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = defaultColor;
        }
        */
    }
}
