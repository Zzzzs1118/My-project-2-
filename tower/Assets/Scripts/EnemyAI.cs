using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour {
    private NavMeshAgent agent;
    private Transform targetPos;
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        targetPos = GameObject.Find("EVE").transform.GetChild(0);
        agent.destination = targetPos.position;
    }
	void Update () {
        //Debug.Log(Vector3.Distance(transform.position, targetPos.position));
        if (Vector3.Distance(transform.position, targetPos.position) <= 1f) Destroy(gameObject);

        
	}
}
