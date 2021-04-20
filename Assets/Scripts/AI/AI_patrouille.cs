using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_patrouille : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    
    
    public Vector3 destination;
    public bool ispatroling;
    public float patrolrange;

    public float sightrange;
    public bool isinsight;
    public bool gameover;
    
    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        detection();
        if (isinsight)
            gameover = true;
        else
            Patrol();
    }

    void Patrol()
    {
        if (!ispatroling)
        {
            float randomX = Random.Range(-patrolrange, patrolrange);
            float randomZ = Random.Range(-patrolrange, patrolrange);

            destination = new Vector3(player.position.x + randomX, player.position.y, player.position.z + randomZ);
            agent.SetDestination(destination);
            ispatroling = true;
        }
        Vector3 distanceToWalkpoint = player.position - destination;
        if (distanceToWalkpoint.magnitude < 1f)
        {
            ispatroling = false;
        }
    }

    void detection()
    {
        isinsight = Physics.CheckSphere(player.position, sightrange);
    }
}
