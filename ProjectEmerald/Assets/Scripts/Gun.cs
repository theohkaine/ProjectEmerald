using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

    float nextShotTime;
    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);
        }
        
    }
    public void ShootShotGun()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            Projectile newProjectile2 = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            Projectile newProjectile3 = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);
            newProjectile2.SetSpeed(muzzleVelocity);
            newProjectile3.SetSpeed(muzzleVelocity);
        }

    }
}
