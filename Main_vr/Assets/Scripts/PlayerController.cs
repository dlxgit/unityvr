using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * TODO:
 * 
 * стрельба
 * стена
 * враги, перемещение
 * убийство врагов
 * волны
 * 
 * оружия
 * подбор оружия
 * 
 * UI
 * 
 * 
 * 
 * 
 * 
 * */

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    public GameObject muzz;
    ParticleSystem muzzP;
    GameObject fpsController;

	// Use this for initialization
	void Start () {
        //muzz = transform.Find("Particle System").gameObject;
        muzzP = muzz.GetComponent<ParticleSystem>();
        muzzP.Play();
        muzz.SetActive(false);
        
        fpsController = GameObject.Find("FPSController");
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleMuzzle(bool active)
    {
        //if()
        if (!active)
        {
            muzzP.Stop();
            //muzzP.Emit(0);
        }
        else
        {
            muzzP.Play();
        }
        
        muzz.SetActive(active);
    }

    public void MoveTo(Vector3 pos)
    {
        fpsController.transform.position = pos;
    }

}
