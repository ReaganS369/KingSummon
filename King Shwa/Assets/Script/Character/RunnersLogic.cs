using UnityEngine;

public class RunnersLogic : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private KingLogic kingLogic;
    [SerializeField] private UIManager uiManager; // Reference to the UIManager

    private BoxCollider2D kingCollider;

    private void Start()
    {
        gameManager ??= FindObjectOfType<GameManager>();
        uiManager ??= FindObjectOfType<UIManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene. Please assign it in the Inspector.");
        }

        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene. Please assign it in the Inspector.");
        }

        if (kingLogic != null)
        {
            kingCollider = kingLogic.GetComponent<BoxCollider2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chaser"))
        {
            Destroy(gameObject);
            gameManager?.RunnerDied(gameObject);
            uiManager?.HideUIElements();
            uiManager?.ShowButton();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        if (other.CompareTag("Chaser"))
        {
            Destroy(gameObject);
            Debug.Log("Player dies");
            gameManager?.RunnerDied(gameObject);
            uiManager?.HideUIElements();
            uiManager?.ShowButton();
        }
        else if (other.CompareTag("Castle"))
        {
            Debug.Log("Runner reached the castle");
            gameManager?.RunnerReachedFinish(gameObject);
            uiManager?.HideUIElements();

            if (kingCollider != null)
            {
                kingCollider.enabled = false;
            }
        }
    }
}
