using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts;

public class EnemyWavesController : MonoBehaviour {

    public GameObject simpleEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public GameObject spawnAreaPlane;

    private int RADIUS_SPAWN_INTERSECTION = 5;

    float timeBetweenWaves = 3.3f;
    private float countdown = 0;

    public int enemiesPerWave = 5;

    public ArrayList enemies;

    ArrayList positions;
    ArrayList busySlots;
    ArrayList spawnAreas;

    public static EnemyWavesController instance;


    public bool isWallDestroyed = false;
    
    

    public void RemoveEnemy(GameObject e)
    {
        Debug.Log("Enemy removed (" + enemies.Count + ")");
        enemies.Remove(e);
    }


    void Update()
    {
        Debug.Log("Countdown: " + countdown);
        if (countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    void SpawnWave()
    {
        Debug.Log("Wave Incoming");

        System.Random rnd = new System.Random();

        for(int i = 0; i < enemiesPerWave; ++i)
        {
            int result = rnd.Next(0, 3);
            //SpawnOne(result == 0 ? simpleEnemyPrefab : rangedEnemyPrefab);
        }

        
        //for ( int i = 0; i < 5; ++i)
        {
            GameObject go = Instantiate(simpleEnemyPrefab);
            go.transform.position = new Vector3(rnd.Next(-70, 35), go.transform.position.y, rnd.Next(-137, -60));
        }
        
        busySlots = new ArrayList();
        
    }

    // Use this for initialization
    void Start()
    {
        spawnAreas = new ArrayList();
        spawnAreas.Add(GameObject.Find("spot1"));
        spawnAreas.Add(GameObject.Find("spot1 (1)"));
        spawnAreas.Add(GameObject.Find("spot1 (2)"));
        spawnAreas.Add(GameObject.Find("spot1 (3)"));
        spawnAreas.Add(GameObject.Find("spot1 (4)"));
        spawnAreas.Add(GameObject.Find("spot1 (5)"));
        spawnAreas.Add(GameObject.Find("spot1 (6)"));
        spawnAreas.Add(GameObject.Find("spot1 (7)"));
        spawnAreas.Add(GameObject.Find("spot1 (8)"));
        spawnAreas.Add(GameObject.Find("spot1 (9)"));
        enemies = new ArrayList();
        positions = new ArrayList();
        busySlots = new ArrayList();

        for (int i = 0; i < 20; ++i)
        {
            positions.Add(i * 10 + 50);
        }

        instance = this;
    }

    void SpawnOne(GameObject obj)
    {
        //Instantiate(obj, GeneratePosition(), transform.parent.rotation);
        Vector3 pos;
        int x = 0;
        do
        {
            x = (int)positions[Utils.rnd.Next(19)];


        }
        while (busySlots.Contains(x));
        busySlots.Add(x);
        obj.transform.position = new Vector3(x, 0 , 104);
        Instantiate(obj);
        Debug.Log("Instantiated");
    }

    Vector3 GeneratePosition()
    {
        Bounds bounds = spawnAreaPlane.GetComponent<MeshFilter>().mesh.bounds;
        //int x = rnd.Next((int)(bounds.center.x - bounds.size.x / 2), (int)(bounds.center.x + bounds.size.x / 2));
        int x = Utils.rnd.Next(60, 270);
        //int z = rnd.Next((int)(bounds.center.z - bounds.size.z / 2), (int)(bounds.center.z + bounds.size.z / 2));
        Vector3 result = new Vector3(x, 0, spawnAreaPlane.transform.position.z);
        return result;
    }

    bool PosIsNearEnemy(Vector3 p)
    {
        //50 - 270 0 104
        
        /*
        Bounds b = new Bounds(p, new Vector3(RADIUS_SPAWN_INTERSECTION, 1, RADIUS_SPAWN_INTERSECTION));
        foreach (GameObject e in enemies)
        {
            if (e.Equals(null))
            {
                enemies.Remove(e);
            }
            if (e.GetComponent<Mesh>().bounds.Intersects(b))
            {
                return true;
            }
        }
        return false;
        */
        return false;
    }

    /*
    private Vector3 RandomPointOnPlane(Vector3 position, Vector3 normal, float radius)
    {
        Vector3 randomPoint;

        do
        {
            randomPoint = Vector3.Cross(UnityEngine.Random.insideUnitSphere, normal);
        } while (randomPoint == Vector3.zero);

        randomPoint.Normalize();
        randomPoint *= radius;
        randomPoint += position;

        return randomPoint;
    }
    */
}
