using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target; // The game object to follow
    [SerializeField] private float followRange = 5f; // The range within which the player does not move
    [SerializeField] private float followSpeed = 2f; // The speed at which the player follows
    [SerializeField] private float delayTime = 1f; // The delay time before following

    private bool isFollowing = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > followRange && !isFollowing)
        {
            StartCoroutine(FollowAfterDelay());
        }
    }

    private IEnumerator FollowAfterDelay()
    {
        isFollowing = true;
        yield return new WaitForSeconds(delayTime);

        while (Vector3.Distance(transform.position, target.position) > followRange)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
            yield return null;
        }

        isFollowing = false;
    }
}
