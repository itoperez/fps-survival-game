using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private HealthScript health_Script;

    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4.5f;

    public float chase_Distance = 20f;
    private float current_Chase_Distance;
    public float attack_Distance = 1f;
    public float chase_After_Attack_Distance = 1f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;

    public GameObject attackPoint;

    private EnemyAudio enemy_Audio;

    private EnemyStats enemy_Stats;

    public bool is_Cannibal, is_Boar;

    private void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        health_Script = GetComponent<HealthScript>();

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        enemy_Audio = GetComponentInChildren<EnemyAudio>();

        enemy_Stats = GetComponent<EnemyStats>();
    }

    private void Start()
    {
        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        attack_Timer = wait_Before_Attack;

        current_Chase_Distance = chase_Distance;
    }

    private void Update()
    {
        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemy_State == EnemyState.CHASE)
        {
            Chase();
        }

        if (enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        // turn off enemy health UI
        enemy_Stats.TurnOffEnemyDisplayHealth(is_Cannibal, is_Boar);
        
        // enable nav agent movement
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;

        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();

            patrol_Timer = 0f;
        }

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);
        } else
        {
            enemy_Anim.Walk(false);
        }

        // chase if player is close enough to enemy 
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {
            enemy_Anim.Walk(false);

            enemy_State = EnemyState.CHASE;

            // Reset current enemy on all enemies to false
            EnemyManager.instance.ClearIsCurrentEnemiesFlag(is_Cannibal);

            // sets current enemey
            enemy_Stats.SetCurrentEnemy(is_Cannibal);

            // play spotted audio
            enemy_Audio.Play_ScreamSound();
        }

    } // patrol

    void Chase()
    {
        /*
        // Reset current enemy on all enemies to false
        EnemyManager.instance.ClearIsCurrentEnemiesFlag(is_Cannibal);

        // sets current enemey
        enemy_Stats.SetCurrentEnemy(is_Cannibal);
        */

        enemy_Stats.TurnOnEnemyDisplayHealth(health_Script.health, is_Cannibal, is_Boar);

        // enable nav agent movement
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;

        // set player's position as chase destination
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Run(true);
        }
        else
        {
            enemy_Anim.Run(false);
        }

        // attack if enemy is close enough to player 
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            // reset chase distance
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }

        } else if (Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            enemy_Anim.Run(false);

            enemy_State = EnemyState.PATROL;

            patrol_Timer = patrol_For_This_Time;

            // reset chase distance
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }

    } // chase

    void Attack()
    {
        // display enemy health UI
        enemy_Stats.TurnOnEnemyDisplayHealth(health_Script.health, is_Cannibal, is_Boar);

        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if (attack_Timer > wait_Before_Attack)
        {
            enemy_Anim.Attack();

            attack_Timer = 0f;

            // play attack sound
            enemy_Audio.Play_AttackSound();
        }

        if (Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }



    } // attack

    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        navAgent.SetDestination(navHit.position);
    }

    void Turn_On_AttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    public EnemyState Enemy_State
    {
        get; set;
        /*
        get {
            return enemy_State;
        }
        set {
            enemy_State = value;
        } */
    }


}
