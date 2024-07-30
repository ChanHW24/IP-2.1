using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime;
    public bool walking, chasing;
    public Transform player;
    Transform currentDest; 
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    public string deathScene;
    public float deathRange = .80f; // the distance at which the enemy kills the player

    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
    }

    private void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if(Physics.Raycast(transform.position + rayCastOffset, direction, out hit,sightDistance))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                chasing = true;
            }
        }

        if(chasing == true)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            aiAnim.ResetTrigger("walk");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("sprint");
            if (ai.remainingDistance <= catchDistance)
            {
                //player.gameObject.SetActive(false);
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                aiAnim.ResetTrigger("sprint");
                chasing = false;
            }
        }

        if(walking == true)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            aiAnim.ResetTrigger("sprint");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("walk");
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                aiAnim.ResetTrigger("sprint");
                aiAnim.ResetTrigger("walk");
                aiAnim.SetTrigger("idle");
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
                /*
                randNum2 = Random.Range(0, 2);
                if(randNum2 == 0)
                {
                    randNum = Random.Range(0, destinationAmount);
                    currentDest = destinations[randNum];
                }
                if(randNum2 == 1)
                {
                    aiAnim.ResetTrigger("sprint");
                    aiAnim.ResetTrigger("walk");
                    aiAnim.SetTrigger("idle");
                    StopCoroutine("stayIdle");
                    StartCoroutine("stayIdle");
                    walking = false;
                }
                */
            }
        }

        // calculate the distance between the enemy and the player
        float distance = Vector3.Distance(player.position, transform.position);
        PlayerDeath(distance); // method that reloads the level when enemy catches player
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
    }

    private void PlayerDeath(float distance)
    {
        // if the distance is close enough to the player it reloads the scene
        if (distance < deathRange)
        {
            // loads the active scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // this can be easily change to another scene.
        }
    }
}