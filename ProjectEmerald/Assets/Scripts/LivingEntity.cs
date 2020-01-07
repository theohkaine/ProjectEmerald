using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{

    public float startingHealth;
    protected float health;
    protected bool dead;

    public event System.Action OnDeath;

    Display display;

    protected virtual void Start()
    {
        display = GetComponent<Display>();
        health = startingHealth;
        
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        //Do some stuff here with hit var. Ex particles
        TakeDamage(damage);
    }

    protected void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            display.SetHealth(health.ToString());
        }

        if (health <= 0 && !dead)
        {
            Die();
        }
    }
}
