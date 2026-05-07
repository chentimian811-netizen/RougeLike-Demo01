using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PickUP : MonoBehaviour
{
    public enum PickupType
    {
        Reward
    }

    [SerializeField]private PickupType pickupType;
    [SerializeField]private int value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControler>())
        {
            CollectPickup();
        }
    }

    private void CollectPickup()
    {
        switch(pickupType)
        {
            case PickupType.Reward:
                break;
        }
        Destroy(gameObject);
    }


}
