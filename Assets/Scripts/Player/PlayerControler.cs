using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControler : MonoBehaviour
{
    public GameObject[] guns;
    public float MoveSpeed;
    Vector2 moveDirection;
    private Animator ani;
    private int gunNum;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 8f;
        rb = GetComponent<Rigidbody2D>(); 
        ani = GetComponent<Animator>();
        guns[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    { 
     
         SwithchGun();
        //获得玩家输入
        float movex = Input.GetAxis("Horizontal");
        float movey = Input.GetAxis("Vertical");

        ani.SetFloat("Horizontal", movex);
        ani.SetFloat("Vertical", movey);
        ani.SetFloat("Speed", moveDirection.sqrMagnitude);
        //Debug.Log("movex:" + movex + ",movey:" + movey); 


        moveDirection = new Vector2(movex, movey).normalized;
        rb.velocity = moveDirection * MoveSpeed;

    }

    void SwithchGun()  
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            guns[gunNum].SetActive(false);
            if(--gunNum < 0) 
            {
               gunNum = guns.Length - 1;
            }
            guns[gunNum].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            guns[gunNum].SetActive(false) ;
            if (++gunNum >guns.Length-1) 
            {
                gunNum = 0;
            }
            guns[gunNum].SetActive(true);
        }
    }
  
}
