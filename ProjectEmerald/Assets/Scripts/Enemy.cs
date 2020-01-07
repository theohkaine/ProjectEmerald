using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{

    public enum State { idle, Chasing, Attacking };
    State currentState;

    NavMeshAgent pathFinder;
    Transform target;
    LivingEntity targetEntity;

    Material skinMaterial;

    Color originalColor;

    float attackDistanceThreshold = 0.5f;
    float timeBetweenAttacks = 1;

    float damage = 1;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;
    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();

        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            currentState = State.Chasing;
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
        currentState = State.idle;
    }

    void Update()
    {

        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;

                if (sqrDistanceToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }

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
        currentState = State.Chasing;
        pathFinder.enabled = true;

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }

            }
            yield return new WaitForSeconds(refreshRate);

        }
    }
}
