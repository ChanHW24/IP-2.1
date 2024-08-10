/*
 * Author: Tan Tock Beng, Chan Hong Wei, Lee Ming Zhe
 * Date: 05/08/2024
 * Description: 
 * Contains enemy AI chasing functions.
 */
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
    public Vector3 rayCastOffset = new Vector3(0, 1, 0);
    public string deathScene;
    public float deathRange = .80f; // the distance at which the enemy kills the player
    public float aiDistance;
    public GameObject hideText, stopHideText;
    public AudioSource walkSound, runSound, heySound;

    // For the Caught menu UI
    public static bool GameIsPause = false;
    public GameObject caughtMenuUI; // Reference to the pause menu
    public GameObject backgroundImage; // Reference to the background image
    public GameObject crossHair; // Reference to the crosshair
    public MonoBehaviour playerCameraController;

    public float playerHeight = 2f;

    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    private void Update()
    {

        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        Debug.DrawRay(transform.position + rayCastOffset, direction * sightDistance, Color.red); // Visualize the Raycast
        aiDistance = Vector3.Distance(player.position, this.transform.position);
        if(Physics.Raycast(transform.position + rayCastOffset, direction, out hit,sightDistance))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("chase");
                walking = false;
                StopCoroutine("idleState");
                StopCoroutine("chaseState");
                StartCoroutine("chaseState");
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
            if (aiDistance <= catchDistance)
            {
                //player.gameObject.SetActive(false);
                aiAnim.ResetTrigger("walk");
                aiAnim.ResetTrigger("idle");
                hideText.SetActive(false);
                stopHideText.SetActive(false);
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
                StopCoroutine("idleState");
                StartCoroutine("idleState");
                walking = false;
            }
        }

        // calculate the distance between the enemy and the player
        float distance = Vector3.Distance(player.position, transform.position);
        PlayerDeath(distance); // method that reloads the level when enemy catches player
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            heySound.Play();
        }
    }

    public void stopChase()
    {
        walking = true;
        chasing = false;
        StopCoroutine("chaseState");
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    IEnumerator idleState()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    IEnumerator chaseState()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        stopChase();
    }

    private void PlayerDeath(float distance)
    {
        // if the distance is close enough to the player it reloads the scene
        if (distance < deathRange)
        {
            Debug.Log("Pause");
            caughtMenuUI.SetActive(true); // Show pause menu
            backgroundImage.SetActive(true); // Show background image
            crossHair.SetActive(false); // Turn off crosshair
            //Time.timeScale = 0f; // Freeze time
            GameIsPause = true;
            Cursor.visible = true; // Show cursor
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
            playerCameraController.enabled = false; // Disable camera movement

            // loads the active scene
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // this can be easily change to another scene.
        }
    }

    public void ToggleHeySound()
    {
        if (heySound != null)
        {
            if (heySound.isPlaying)
            {
                heySound.Pause(); // Pause the audio if it is playing
            }
            else
            {
                heySound.Play(); // Play the audio if it is not playing
            }
        }
        else
        {
            Debug.LogWarning("AudioSource component is not assigned!");
        }
    }
}