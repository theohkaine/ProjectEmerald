using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{

    public enum State { idle, Chasing, Attacking, gunIdle, gunWalking, rifleIdle, rifleWalking };
    State currentState;

    NavMeshAgent pathFinder;
    Transform target;
    LivingEntity targetEntity;

    GunController gunController;
    
    //ANIMATIONS
    static Animator anim;
    bool isEquippedAnim;

    Material skinMaterial;

    Color originalColor;

    float attackDistanceThreshold = 0.8f;
    float timeBetweenAttacks = 0.8f;

    float damage = 1;

    float nextAttackTime;

    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;
    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();

        gunController = GetComponent<GunController>();
        anim = GetComponent<Animator>();

        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            currentState = State.gunWalking;
            gunWalkingAnim();
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(UpdatePath());
        }

    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.gunIdle;
        gunIdleAnim();

    }

    void Update()
    {

        if (hasTarget)
        {
            float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;

            if (sqrDistanceToTarget < 22)   
            {
                if (Time.time > nextAttackTime)
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;

                    //Equip Weapon
                    gunController.EquipGun(gunController.pistol);
                    StartCoroutine(Shooting());
                    currentState = State.gunIdle;
                    gunIdleAnim();
                }
            }
            else
            {
                //Unequip Weapon
                gunController.EquipGun(gunController.flashlight);
                StartCoroutine(UpdatePath());
                currentState = State.gunWalking;
                gunWalkingAnim();
            }
        }

    }

    IEnumerator Shooting()
    {
        pathFinder.enabled = false;

        while (currentState == State.gunIdle)
        {
            gunController.Shoot();
            
            yield return null;
        }

        pathFinder.enabled = true;
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.yellow;

        bool hasAppliedDamage = false;

        while (percent <= 1)
        {

            if (percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;    //PARABOLE
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }
        skinMaterial.color = originalColor;
        currentState = State.gunWalking;
        pathFinder.enabled = true;

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.55f;

        while (hasTarget)
        {
            if (currentState == State.gunWalking)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2); //CHANGE : Enemy needs to stop moving when shooting.
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }

            }
            yield return new WaitForSeconds(refreshRate);

        }
    }

    //ANIMATIONS
    //======================================================================
    //
    void gunIdleAnim()
    {
        anim.SetBool("isEquipped", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isEquippedWithRifle", false);
    }

    void gunWalkingAnim()
    {
        anim.SetBool("isEquipped", true);
        anim.SetBool("isWalking", true);
        anim.SetBool("isEquippedWithRifle", false);
    }

    void rifleIdle()
    {
        anim.SetBool("isEquippedWithRifle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isEquipped", false);
    }

    void rifleWalking()
    {
        anim.SetBool("isEquippedWithRifle", true);
        anim.SetBool("isWalking", true);
        anim.SetBool("isEquipped", false);
    }
}
