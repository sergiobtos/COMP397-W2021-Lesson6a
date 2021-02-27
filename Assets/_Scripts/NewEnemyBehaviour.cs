using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Line of Sight")]
    public bool HasLOS;
    public GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
        }
            
        if(HasLOS && Vector3.Distance(transform.position, player.transform.position) < 2.5)
        {
            animator.SetInteger("AnimState", (int)EnemyState.IDLE);
            transform.LookAt(transform.position -player.transform.forward);
        }
        else
        {
            animator.SetInteger("AnimState", (int)EnemyState.RUN);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
        }
       
    }
}



public enum EnemyState
{
    IDLE, RUN, JUMP
}
