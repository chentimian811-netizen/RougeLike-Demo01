 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Blinks;
    public float time;
    private Renderer myRender;

    public float PlayerCurrentHealth { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.HealthMax = health;    
        HealthBar.HealthCurrent = health;    
        myRender = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {

        health -= damage;

        if (health < 0) 
        {
            health = 0; 
        }


        HealthBar.HealthCurrent = health; 
    
        if (health <= 0)
        {
            UIManager.Instance.showGameOverPanel();
            Destroy(gameObject);
        }

        BlinkPlayer(Blinks, time); 
    }

    void BlinkPlayer(int numBlinks, float seconds)
   {
        StartCoroutine(DoBlinks(numBlinks,seconds)); 
   }

     IEnumerator DoBlinks(int numBlinks, float seconds)
   {
       for (int i = 0; i < numBlinks * 2; i++)
        {
          myRender.enabled = ! myRender.enabled;
           yield return new WaitForSeconds(seconds);
       }
          myRender.enabled = true; 
        
    }

    public void SaveDate()
    {
        //PlayerCurrentHealth = PlayerCurrentHealth.
    }

    public void LoadDate()
    {
        
    }
}

