using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private float maxHealth = 5;
    private float health;
    // Use this for initialization
    void Start()
    {
        health = maxHealth;
    }

    public void Hit()
    {
        health -= 1;
        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, (health/maxHealth));
        Debug.Log("Health: " + health);
        if (health == 0)
        {
            Debug.Break();
        }
    }
    
}


