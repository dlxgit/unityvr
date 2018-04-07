using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotator : MonoBehaviour {


    Camera cam;
    GameObject gun;

	// Use this for initialization
	void Start () {
       cam = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
       gun = GameObject.Find("MachineGun_01");
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Cam tr rotation: " + cam.transform.rotation);
        gun.transform.rotation.SetLookRotation(cam.transform.rotation.ToEuler());

    }
}
