using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallController : MonoBehaviour {

    public static WallController instance;
    
    public int WALL_MAX_HEALTH;
    
    GameObject wallObject;

    int currentHealth;

    bool isWallDestroyed = false;


	// Use this for initialization
	void Start () {
        //text = GameObject.Find("Text").GetComponent<Text>();
        wallObject = GameObject.Find("fence");
        currentHealth = 200;
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        //text.GetComponent<Text>().text = "Wall hp: " + currentHealth;
	}

    public void TakeDamage(int amount)
    {
        Debug.Log("hp: " + amount);
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            EnemyWavesController.instance.isWallDestroyed = true;
            instance.isWallDestroyed = true;
            Destroy(wallObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //if (col.gameObject.name == "prop_powerCube")
        Debug.Log("Collsion Wall enter");
        if (col.collider.tag == "enemy")
        {
            //takedmg
            TakeDamage(5);
        }
    }

    public bool IsWallDestroyed()
    {
        return isWallDestroyed;
    }
    
    
}
