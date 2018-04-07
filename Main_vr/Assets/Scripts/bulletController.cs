using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    public float LIVING_MAX_TIME = 4;
    public float SPEED = 10;

    //Vector3 target;

    float currentLivingTime = 0;

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Created bullet");
    }

    // Update is called once per frame
    void Update()
    {
        //move pos to target
        transform.position += Vector3.forward * (SPEED * Time.deltaTime);
        if(currentLivingTime >= LIVING_MAX_TIME)
        {
            Destroy(gameObject);
        }
        currentLivingTime += Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        //if (col.gameObject.name == "prop_powerCube")

        //if (col.collider.tag == "enemy")
        {
            //col.collider.GetComponent<>
        }

        {
            Destroy(gameObject);
        }

    }

    void OnDestroy()
    {
        //Debug.Log("Dead bullet");
    }
}

