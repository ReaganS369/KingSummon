using UnityEngine;
using System.Collections;

public class ChaserLogic : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject attackSkill;
    [SerializeField] private GameObject shieldSkill;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject gameoverUI;

    private KingLogic kingLogic;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        kingLogic = FindObjectOfType<KingLogic>();

        if (kingLogic == null)
        {
            Debug.LogError("KingLogic not found!");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("King"))
        {
            if (gameoverUI != null)
            {
                gameoverUI.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("King"))
        {
            if (gameoverUI != null)
            {
                gameoverUI.SetActive(true);
            }
        }
        else if (other.gameObject == attackSkill)
        {
            Debug.Log("Chaser destroyed by attack skill.");
            Destroy(gameObject);
        }
        else if (other.gameObject == shieldSkill)
        {
            Destroy(gameObject);
            Debug.Log("Chaser touched the shield skill.");
            gameManager.DeactivateSpecialKingSkill();
            Destroy(shieldSkill);
            Destroy(attackSkill);

            if (kingLogic != null)
            {
                StartCoroutine(ReactivateKingColliderAfterDelay(1f));
            }
        }
    }

    private IEnumerator ReactivateKingColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        BoxCollider2D kingCollider = kingLogic.GetComponent<BoxCollider2D>();
        if (kingCollider != null)
        {
            kingCollider.enabled = true; // Activate the BoxCollider2D of the King
            Debug.Log("King's BoxCollider2D reactivated after delay.");
        }
    }
}
