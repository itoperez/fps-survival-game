using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navMeshAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    public bool is_Player, is_Boar, is_Cannibal;

    private bool is_Dead;

    private EnemyAudio enemy_Audio;

    private PlayerStats player_Stats;

    private EnemyStats enemy_Stats;                             // ------------- NEW -------------

    private void Awake()
    {
        if (is_Boar || is_Cannibal)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            // get enemy audio
            enemy_Audio = GetComponentInChildren<EnemyAudio>();

            enemy_Stats = GetComponent<EnemyStats>();               // ------------- NEW -------------

        }

        if (is_Player)
        {
            player_Stats = GetComponent<PlayerStats>();
        }
    }

    public void ApplyDamage(float damage)
    {
        if (is_Dead)
        {
            return;
        }

        health -= damage;

        if (is_Player)
        {
            // display player UI value
            player_Stats.Display_HealthStats(health);
        }

        if (is_Cannibal || is_Boar)
        {
            // Reset current enemy on all enemies to false
            EnemyManager.instance.ClearIsCurrentEnemiesFlag(is_Cannibal);

            // sets current enemey
            enemy_Stats.SetCurrentEnemy(is_Cannibal);

            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }

            enemy_Stats.TurnOnEnemyDisplayHealth(health, is_Cannibal, is_Boar);
                       
            enemy_Audio.Play_InjuredSound();                                              // ------------- NEW -------------
        }

        if (health <= 0)
        {
            EntityDied();

            is_Dead = true;
        }


    } // apply damage

    void EntityDied()
    {
        // only done because we don't have a death animation for the cannibal
        if (is_Cannibal)
        {
            int rnd = Random.Range(-1, 2);            

            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().isKinematic = false;                           // needed bug from before with nav mesh agent, actually has cool death effect now
            GetComponent<Rigidbody>().AddTorque(rnd * transform.forward * 50f);      // [-] enemy rotates and falls left [+] enemy rotates and falls right
            GetComponent<Rigidbody>().AddForce(-transform.forward * 100f);            // [-] enemy falls backwards [+] enemy falls forward

            enemy_Controller.enabled = false;
            navMeshAgent.enabled = false;
            enemy_Anim.enabled = false;

            enemy_Stats.TurnOffEnemyDisplayHealth(is_Cannibal, is_Boar);
            
            StartCoroutine(DeadSound());
             
            // EnemyManager spawn more enemies
            EnemyManager.instance.EnemyDied(true);
        }

        // done when we have a death animation
        if (is_Boar)
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();

            enemy_Stats.TurnOffEnemyDisplayHealth(is_Cannibal, is_Boar);
            
            StartCoroutine(DeadSound());

            // EnemyManager spawn more enemies
            EnemyManager.instance.EnemyDied(false);
        }

        if (is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            // call enemy managaer to stop spawning enemies
            EnemyManager.instance.StopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        } else
        {
            Invoke("TurnOffGameObject", 3f);
        }

    } // entity died

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemy_Audio.Play_DeadSound();
    }

}
