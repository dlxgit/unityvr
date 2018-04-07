using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    static Ship instance;
    GameObject target;
    GameObject target0;
    GameObject target1;
    public float MOVE_SPEED = 30;
    bool arrived = false;

    float speed = 10f;
	// Use this for initialization
	void Start () {
        instance = this;
        target0 = GameObject.Find("boatTarget0");
        target1 = GameObject.Find("boatTarget1");
        target = target0;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time < 95)
        {
            return;
        }

        if (target == target0 && IsNearObject(target0))
        {
            target = target1;
        }
        else if (IsNearObject(target1))
        {
            Debug.Log("Success");
        }


        //Vector3 dir = GameObject.Find("Main Camera").transform.position - transform.position;
        //dir.y = 0; // keep the direction strictly horizontal
        //Quaternion rot = Quaternion.LookRotation(dir);
        // slerp to the desired rotation over time

        //transform.LookAt(GameObject.Find("Main Camera").transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.5f);
        if (arrived)
        {

        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - MOVE_SPEED * Time.deltaTime);
            if (transform.position.z < 133)
            {
                arrived = true;
                Debug.Log("Success");
                GetComponent<AudioSource>().Play();
                MainPoint.instance.win();
                
            }
        }
    }

    bool IsNearObject(GameObject obj)
    {
        //if (!passedThroughPoint1)
        {
            Vector3 difference = obj.transform.position - transform.position;
            var distanceInX = Mathf.Abs(difference.x);
            var distanceInZ = Mathf.Abs(difference.z);
            return distanceInX < 4f && distanceInZ < 4f;
        }

        //return false;
    }
}
