using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject targetObject;
    public Transform targetTransform;
    public float speed = 10;
    public float rotationSpeed = 5;
    public float health = 100;
    public int damage = 10;
    public StructureHitPoints structureHitPoints;

    void Update()
    {
        if (health <= 0){
            GameObject.Find("EventSystem").GetComponent<towerControl>().kill();
            Destroy(this.gameObject);
        }
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(targetObject.transform.position, path);

        agent.SetDestination(targetObject.transform.position);

        if (path.status == NavMeshPathStatus.PathComplete && agent.remainingDistance > 0.1f)
        {
            agent.Move(agent.desiredVelocity * speed * Time.deltaTime);

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                Vector3 direction = agent.desiredVelocity.normalized;

                //Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {   
            structureHitPoints.TakeDamage(damage);
            print("BONK");
        }
    }
}