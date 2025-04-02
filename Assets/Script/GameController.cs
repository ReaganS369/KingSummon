using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject king;
    public GameObject[] runners;
    public GameObject castle;
    public bool specialSkillActive = false;

    private float specialSkillTimer = 0f;
    private float specialSkillDuration = 2f;
    private bool isAttacking = true;

    void Update()
    {
        if (specialSkillActive)
        {
            specialSkillTimer += Time.deltaTime;
            if (specialSkillTimer >= specialSkillDuration)
            {
                specialSkillTimer = 0f;
                isAttacking = !isAttacking;
                if (isAttacking)
                {
                    // Code for starting attack mode
                    Debug.Log("King is now Attacking");
                }
                else
                {
                    // Code for starting defend mode
                    Debug.Log("King is now Defending");
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chaser"))
        {
            if (collision.otherCollider.gameObject == king)
            {
                if (specialSkillActive)
                {
                    if (isAttacking)
                    {
                        // Eliminate chaser
                        Destroy(collision.gameObject);
                    }
                    else
                    {
                        // Break special skill
                        specialSkillActive = false;
                        Debug.Log("Special Skill Broken");
                        // Unmerge runners from the king (implement your unmerge logic here)
                    }
                }
                else
                {
                    // Game over
                    Debug.Log("Game Over!");
                    // Implement game over logic
                }
            }
            else if (collision.otherCollider.CompareTag("Runner"))
            {
                // Runner dies (optional depending on your game logic)
                Destroy(collision.otherCollider.gameObject);
            }
        }
        else if (collision.gameObject == castle)
        {
            if (collision.otherCollider.gameObject == king)
            {
                // Win the game
                Debug.Log("You Win!");
                // Implement win logic
            }
            else if (collision.otherCollider.CompareTag("Runner"))
            {
                // Activate king's special skill
                specialSkillActive = true;
                MergeRunnersWithKing();
                Debug.Log("Special Skill Activated!");
            }
        }
    }

    void MergeRunnersWithKing()
    {
        foreach (GameObject runner in runners)
        {
            if (runner != null)
            {
                // Implement your merge logic here (e.g., disable runner and increase king's size or power)
                runner.SetActive(false);
            }
        }
    }
}
