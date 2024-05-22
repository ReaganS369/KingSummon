using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    [SerializeField] private GameObject[] potentialTargets; // Array of potential targets
    [SerializeField] private float chaseSpeed = 5f; // Speed at which the object chases the target
    [SerializeField] private float chaseRange = 10f; // Range within which the object starts chasing the target

    private Transform currentTarget;

    void Update()
    {
        // Find the closest target within the chase range
        currentTarget = FindClosestTargetWithinRange();

        if (currentTarget != null)
        {
            // Move towards the current target
            Chase(currentTarget);
        }
    }

    private Transform FindClosestTargetWithinRange()
    {
        Transform closestTarget = null;
        float closestDistance = chaseRange;

        foreach (GameObject target in potentialTargets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target.transform;
            }
        }
        return closestTarget;
    }

    private void Chase(Transform target)
    {
        // Calculate the direction towards the target
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // Move towards the target
        transform.position += directionToTarget * chaseSpeed * Time.deltaTime;
    }
}
