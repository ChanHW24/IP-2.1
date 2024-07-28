/*
 * Author: Tan Tock Beng
 * Date: 19/06/2024
 * Description: 
 * Contains functions related to the enemy movement.
 */
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The radius within which the enemy can detect the player.
    /// </summary>
    public float lookRadius = 10f;

    public Transform target;
    public NavMeshAgent agent;

    /// <summary>
    /// Initializes the enemy controller by setting the target and agent references.
    /// </summary>
    void Start()
    {
        //target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Updates the enemy's behavior each frame.
    /// </summary>
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }


    /// <summary>
    /// Rotates the enemy to face its target.
    /// </summary>
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    /// <summary>
    /// Draws a wire sphere gizmo to visualize the enemy's detection range.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
