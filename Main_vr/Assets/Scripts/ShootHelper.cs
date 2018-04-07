using UnityEngine;
using System.Collections;

public class ShootHelper : MonoBehaviour
{
    public enum State
    {
        SHOOT,
        RELOAD,
        DEFAULT
    }

    Animation _animation;
    public AudioSource reloadSound;
    public AudioSource fireSound;

    public Vector3 tt1;
    public Vector3 tt2;
    public GameObject impactEffectPrefab;

    public GameObject bulletPrefab;
    //public Vector3 pos;
    public float cooldownBetweenShots = 0.1f;
    //public float cooldownBetweenShots = 5f;
    public float impactDuration = 1.5f;
    public float range = 150;
    //public GameObject impactEffect;

    public int dmgBullet = 15;

    private float timeSinceLastShot = 0;
    bool shooting = false;

    public Camera camera;

    bool shootButtonPressed = false;
    bool reloadPressed = false;

    float axisX = 0;
    float axisY = 0;

    public float reloadTime = 4;
    public float currentReloadTime;

    int ammo;
    public int maxAmmo;

    Animator animator;
    State state = State.DEFAULT;
    AudioSource[] audioSource;
    
    void Start()
    {
        _animation = GetComponent<Animation>();
        this.animator = transform.GetComponent<Animator>();
        audioSource = GetComponents<AudioSource>();
        maxAmmo = 100;
        ammo = maxAmmo;
        currentReloadTime = reloadTime;
        //camera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        SetState(State.SHOOT);
    }

    // Update is called once per frame
    void Update()
    {
        
       
        //Debug.Log("Current Reload time: " + currentReloadTime + " ammo: " + ammo);
        //Debug.Log("IsFirePressed: " + IsFirePressed());
        //Debug.Log("IsFirePressed: " + IsFirePressed().ToString());
        float _axisX = Input.GetAxis("Mouse X");
        float _axisY = Input.GetAxis("Mouse Y");
        if (axisX != _axisX || axisY != _axisY || Input.GetKey(KeyCode.Z))
        {
            //Debug.Log("Axis changed: " + axisX + " " + axisY + " " + _axisX + " " + _axisY);
            shootButtonPressed = true;
            axisX = _axisX;
            axisY = _axisY;
        }
        else
        {
            Debug.Log("Axis the same");
            shootButtonPressed = false;
            //if (axisX < _axisY)
            //reload
            if (Input.GetKey(KeyCode.X))
            {
                reloadPressed = true;
                if (currentReloadTime > reloadTime)
                {
                    reloadSound.Play();
                    SetState(State.RELOAD);
                    currentReloadTime = 0;
                }
            }
            else
            {
                reloadPressed = false;
            }
        }

        if (currentReloadTime + Time.deltaTime > reloadTime)
        {
            if (currentReloadTime < reloadTime)
            {
                ammo = maxAmmo;
            }
            //ammo = maxAmmo;
            if (!shootButtonPressed && timeSinceLastShot > 1)
            {
                //Debug.Log("SetState Default: " + timeSinceLastShot);
                SetState(State.DEFAULT);
            }
        }

        bool fire = false;
        Debug.Log("MouseDownTest: " + IsFirePressed());
        if (IsFirePressed() && IsAbleToShoot())
        {
            Debug.Log("Fire");
            fire = Fire();
        }
        else if (ammo == 0 && currentReloadTime > reloadTime)
        {
            reloadSound.Play();
            SetState(State.RELOAD);
            currentReloadTime = 0;
        }

        if (fire)
        {
            timeSinceLastShot = 0;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }

        currentReloadTime += Time.deltaTime;
    }

    bool Fire()
    {
        --ammo;
        SetState(State.SHOOT);
        if (!audioSource[1].isPlaying)
        {
            audioSource[1].Play();
        }
        else if (!audioSource[2].isPlaying)
        {
            audioSource[2].Play();
        }
        else if (!audioSource[3].isPlaying)
        {
            audioSource[3].Play();
        }

        timeSinceLastShot = 0;
        RaycastHit hit;
        Debug.Log("Fire()");

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Debug.Log("Fire()->IF()");
            GameObject o = hit.collider.gameObject;

            if (o.tag == "enemy")
            {

                EnemyScript sc = o.GetComponent<EnemyScript>();
                if(sc != null)
                {
                    o.GetComponent<EnemyScript>().TakeDamage(dmgBullet);
                    Debug.Log("Shooting enemy1");
                    //Instantiate(impactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                }
                else if (sc == null)
                {
                    Debug.Log("doesn't have enemyscript");
                }
            }
            else
            {
                if (o.name == "tt1")
                {
                    PlayerController.instance.MoveTo(tt1);
                }
                else if (o.name == "tt2")
                {
                    PlayerController.instance.MoveTo(tt2);
                }
            }
            Debug.Log("Instantiate impact");
            GameObject impactGO = Instantiate(impactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2);


        }
        CreateBullet();
        return true;
    }


    void OnMouseDown()
    {
        shootButtonPressed = true;
    }

    void OnMouseUp()
    {
        shootButtonPressed = false;
    }
    

    bool IsFirePressed()
    {
        if (Input.GetKeyDown("2") || Input.GetKeyUp("1") || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Debug.Log("keker");
            return true;
        }

        if (Input.GetMouseButton(0))
        {
            //shootButtonPressed = true;
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetKeyDown("2") || Input.GetKeyUp("1"))
        {
            //shootButtonPressed = false;
            PlayerController.instance.ToggleMuzzle(false);
        }
        if (shootButtonPressed == false)
        {
            PlayerController.instance.ToggleMuzzle(false);
        }
        return shootButtonPressed;
    }

    void CreateBullet()
    {
        GameObject player = GameObject.Find("MachineGun_01");
        //Vector3 pos = player.transform.position + new Vector3(0, 2, 0);
        //GameObject.Instantiate(bulletPrefab, pos, GameObject.Find("MachineGun_01").transform.rotation);
        Debug.Log("Creating Bullet!");
        PlayerController.instance.ToggleMuzzle(true);
    }

    bool IsAbleToShoot()
    {
        if (timeSinceLastShot < cooldownBetweenShots || currentReloadTime < reloadTime || ammo == 0)
        {
            Debug.Log("NotAbleToShot");
            return false;
        }

        return true;
    }


    void SetState(State _state)
    {
        this.state = _state;

        switch (_state)
        {
            case State.SHOOT:
                animator.speed = 1;
                animator.Play("MachineGin_shoot");
                break;
            case State.RELOAD:
                animator.speed = 5;
                animator.Play("MachineGun_reload");
                break;
            case State.DEFAULT:
                animator.speed = 1;
                animator.Play("None");
                //((Animation)GetComponent<Animation>()).Stop();
                //animator.runtimeAnimatorController.animationClips[2];
                break;
        }
    }
}
