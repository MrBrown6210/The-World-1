using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float findingDistance = 10f;
    public float findingObstacle = 1f;

    private NavMeshAgent navMesh;
    private Animator animator;

    private GameObject target;
    private bool isFoundObstacle;

    private GameObject go;

    private bool isAttacked = false;

    private void Awake()
    {
        go = new GameObject();
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navMesh.updateRotation = false;
        animator.SetBool("isDead", true);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameLevelSystem.Instance.currentLevel < 1) return;
        if (GameLevelSystem.Instance.isCutscenePlaying) return;

        animator.SetBool("isDead", false);

        animator.SetBool("isWalking", navMesh.velocity.magnitude > 0.1f);

        if (isAttacked) return;

        if (isFoundObstacle)
        {
            navMesh.isStopped = true;
            return;
        }
        FindObstacle();

        FindHuman();
        if (target == null) return;

        navMesh.SetDestination(target.transform.position);

        if (navMesh.destination == default) return;

        if (navMesh.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(navMesh.velocity.normalized), Time.deltaTime * 5);
        }

        if (Vector3.Distance(transform.position, navMesh.destination) <= navMesh.stoppingDistance)
        {
            isAttacked = true;
            StartCoroutine(Attack());
            GameLevelSystem.Instance.Failed("Bear kill human");
        }
    }

    IEnumerator Attack()
    {
        animator.SetBool("isStanding", true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("isStanding", false);
    }

    private void FindHuman()
    {
        if (target != null)
        {
            if (target.CompareTag("NPC")) return;
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, findingDistance);
        foreach (var col in hitColliders)
        {

            if (col.CompareTag("NPC"))
            {
                target = col.gameObject;
                return;
            }

            if (col.CompareTag("Player"))
            {
                target = col.gameObject;
                return;
            }
        }
        target = null;
    }

    private void FindObstacle()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, findingObstacle);
        foreach (var col in hitColliders)
        {
            if (col.CompareTag("Obstacle"))
            {
                isFoundObstacle = true;
                GameLevelSystem.Instance.PassLevel(3);
                return;
            }
        }
    }

    private Transform LookTarget(Vector3 position)
    {
        go.transform.position = position;
        return go.transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, findingDistance);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, navMesh.stoppingDistance);
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }

}
