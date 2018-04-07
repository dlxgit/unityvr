using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public enum State
    {
        WALK,
        ATTACK,
        DIE
    }

    public float turnSpeed = 10f;
    public int MAX_HEALTH = 100;
    public float MOVE_SPEED = 0.002f;
    public int DAMAGE_AMOUNT = 10;
    public int ATTACK_COOLDOWN = 3;
    public int health;
    GameObject target; //new Vector3(138, 0, 150);
    GameObject hero;
    GameObject wallTarget;
    GameObject wall;
    GameObject stairs0;
    GameObject stairs1;
    GameObject stairs2;
    GameObject point1;
    Animation _anim;

    public bool shouldMove = true;

    public AudioSource[] sounds;

    //public Animation animationWalk;  //walk, attack, back_fall

    //public Animation animationDie;
    //public Animation animationAttack;

    private State state;

    float timeSinceLastAttack = 0;
    float nearHeroStartTime = 0;
    Animator animator;

    bool passedThroughPoint1;
    float timeFromLastSound = 4f;
    const float timeBetweenSounds = 7f;

    int currentAudioSource = 0;
    public Animation walk;
    public Animation attack;
    public Animation fall;
    
    

    // Use this for initialization
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        //target = Camera.main.transform.position;
        point1 = GameObject.Find("point1");
        target = point1;
        wallTarget = GameObject.Find("wallTarget");
        wall = GameObject.Find("fence");
        hero = GameObject.Find("Main Camera");
        stairs0 = GameObject.Find("stairs0");
        stairs1 = GameObject.Find("stairs1");
        stairs2 = GameObject.Find("stairs2");

        health = MAX_HEALTH;
        //this.animation = transform.GetChild(0).GetComponent<Animation>();
        //foreach (AnimationClip a in animation)

        this.animator = transform.GetComponent<Animator>();
        {
            Debug.Log("");
        }
        //animator.Play("walk");
        SetState(State.WALK);
        
    }

    void UpdateTarget()
    {
        if (WallController.instance.IsWallDestroyed())
        {
            
        }
    }


    void Update()
    {
        timeFromLastSound += Time.deltaTime;
        if (state == State.WALK && timeFromLastSound > timeBetweenSounds)
        {
            Debug.Log("Audio play!");
            sounds[currentAudioSource].Play();
            ++currentAudioSource;
            if (currentAudioSource > 2)
            {
                currentAudioSource = 0;
            }
            timeFromLastSound = 0;
        }

        Debug.Log("target: " + target.transform.position + " EnemyState: " + state);
        //Debug.Log("Enemy State: " + state + " health: " + health + " time: " + Time.realtimeSinceStartup);
        if (health < 0)
        {
            SetState(State.DIE);
        }

        //transform.position = new Vector3(transform.position.x, transform.position.y, gameObject.transform.position.z - MOVE_SPEED * Time.deltaTime);

        //Debug.Log("EnemyState: " + state.ToString() + "animationtime" + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        switch (state)
        {
            case State.WALK:
                if (!WallController.instance.IsWallDestroyed())
                {
                    if (IsNearObject(point1))
                    {
                        Debug.Log("Near to point!");
                        passedThroughPoint1 = true;
                        target = wallTarget;
                        //nearHeroStartTime = Time.time;
                    }
                    Debug.Log("Not near to point!");
                }
                else if (IsNearObject(stairs0))
                {
                    target = stairs1;
                }
                else if (IsNearObject(stairs1))
                {
                    target = stairs2;
                }
                else if (IsNearObject(stairs2))
                {
                    Debug.Log("near stars2");
                    SetState(State.ATTACK);
                    nearHeroStartTime = Time.time;
                }
                else
                {
                    target = stairs0;
                    
                    //if (IsNearHero())
                    {
                        //Debug.Log("near hero");
                        //SetState(State.ATTACK);
                    }
                }

                if (shouldMove)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, MOVE_SPEED * Time.deltaTime * 0.1f);
                    Vector3 _dir = target.transform.position - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), turnSpeed * Time.deltaTime);
                }
                //Debug.Log(target.ToString() + " " + _dir.ToString() + this.transform.position.ToString());
                if (transform.position.z < -40)
                {
                    //SetState(State.ATTACK);
                }
                break;
            case State.ATTACK:
                if (timeSinceLastAttack > ATTACK_COOLDOWN)
                {
                    timeSinceLastAttack = 0;
                    if (IsNearHero())
                    {
                        //hero dying
                        if (Time.time - nearHeroStartTime > 0.6f)
                        {
                            Debug.Log("Hero dead");
                            MainPoint.instance.lose();
                        }
                    }
                    else
                    {
                        WallController.instance.TakeDamage(DAMAGE_AMOUNT);
                        Debug.Log("FENCE TAKING DMG_");
                        if (WallController.instance.IsWallDestroyed())
                        {
                            Debug.Log("FENCE TOOK DMG_");
                            target = stairs0;
                            //animator.StopPlayback();
                            SetState(State.WALK);
                        }
                    }
                }
                break;
            case State.DIE:
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > animator.GetCurrentAnimatorStateInfo(0).length)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
        timeSinceLastAttack += Time.deltaTime;
        shouldMove = true;
    }


    void SetState(State _state)
    {
        this.state = _state;

        switch (_state)
        {
            case State.WALK:
                //animator.StopPlayback();
                animator.Play("walk");
                
                break;
            case State.ATTACK:
                animator.Play("attack");
                break;
            case State.DIE:
                animator.Play("back_fall");
                break;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Destroy(col.collider.gameObject);
        //if (col.gameObject.name == "prop_powerCube")
        Debug.Log("EnemyCollision with " + col.collider.name);
        if (col.collider.tag == "enemy")
        {
            col.collider.GetComponent<EnemyScript>().shouldMove = false;
        }
        else if (col.collider.tag == "weapon")
        {
            //takedmg
            //TakeDamage(ShootHelper.dmgBullet);
            //its handled inside shoothelper (bullet)
        }

        else if (col.collider.tag == "wall")
        {
            //wall dmg ??(можно обрабатывать внутри самой стены)
            Debug.Log("Enemy attacking!");
            if (!WallController.instance.IsWallDestroyed())
            {
                SetState(State.ATTACK);
            }
            
            //WallController.instance.TakeDamage(DAMAGE_AMOUNT);
        }
        else if (col.collider.tag == "environment")
        {
            //change direction
        }



        {
            //Destroy(gameObject);
        }

    }

    void OnDeath()
    {

    }

    public void TakeDamage(int amount)
    {

        this.health -= amount;

        Debug.Log("ENEMY_TAKING_DAMAGE!" + "dmg: " + amount + " hp: " + health + "(" + EnemyWavesController.instance.enemies.Count + ")");
    }




    void OnDestroy()
    {
        EnemyWavesController.instance.RemoveEnemy(this.gameObject);
    }

    bool IsNearObject(GameObject obj)
    {
        //if (!passedThroughPoint1)
        {
            Vector3 difference = obj.transform.position - transform.position;
            var distanceInX = Mathf.Abs(difference.x);
            var distanceInZ = Mathf.Abs(difference.z);
            return distanceInX < 1.2f && distanceInZ < 4f;
        }
        
        //return false;
    }

    bool IsNearHero()
    {
        if (transform.position.z > 0)
        {
            return true;
        }
        return false;

        Vector3 difference = hero.transform.position - transform.position;
        var distanceInX = Mathf.Abs(difference.x);
        var distanceInZ = Mathf.Abs(difference.z);
        //Debug.Log("Distance hero: " + distanceInX + " " + distanceInZ);
        return distanceInX < 1f && distanceInZ < 2f;
    }
}
