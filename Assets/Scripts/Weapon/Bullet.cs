using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float Speed;
    public GameObject explosionPrefab;
    new private Rigidbody2D rigidbody;
    // Start is called before the first frame update
   
    void Awake()
    {
       rigidbody = GetComponent<Rigidbody2D>();  
    }

    public void SetSpeed(Vector2 direction) 
    {
        rigidbody.velocity = direction * Speed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;
        //Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);

        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

        }
    }
}
