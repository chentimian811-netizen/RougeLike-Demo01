using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGunControl : MonoBehaviour
{

    [SerializeField] private AudioSource weaponAudio;
    [SerializeField] private AudioClip shootClip;
    public float interval;

    public GameObject bulletPrefab;

    public GameObject shellPrefab;

    private Transform MuzzlePos;
    private Transform shellPos;
    private float flipy;

    private Vector2 mousePos;
    private Vector2 direction;

    private float timer;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        weaponAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        MuzzlePos = transform.Find("Muzzle");
        shellPos = transform.Find("ButtleShell");
        flipy = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(flipy, -flipy, 1);
        }
        else 
        { 
           transform.localScale = new Vector3(flipy, flipy, 1);
        }
        Shoot();
    }
    void Shoot() 
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;

        if(timer != 0) 
        {
           timer -= Time.deltaTime;
             if(timer <= 0) 
             {
               timer = 0;
             }
        }


        if (Input.GetButtonDown("Fire1"))
        {
            if (timer == 0) 
            {
                weaponAudio.PlayOneShot(shootClip);
                Fire();
               timer = interval;            
            }
        
        }
    }


    void Fire() 
    {
        animator.SetTrigger("Shoot");
        //GameObject bullet = Instantiate(bulletPrefab, MuzzlePos.position,Quaternion.identity);
        GameObject Bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        Bullet.transform.position = MuzzlePos.position;

        float angle = Random.Range(-5f, 5f);
        Bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angle,Vector3.forward) * direction);

        //Instantiate(shellPrefba,shellPos.position, shellPos.rotation);
        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }
}
