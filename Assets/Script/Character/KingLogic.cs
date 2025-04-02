using UnityEngine;
using Cinemachine;
using System.Collections;

using UnityEngine.UI;

public class KingLogic : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject attackSkill;
    [SerializeField] private GameObject shieldSkill;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject gameoverUI;


    private RunnersLogic runnersLogic;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        runnersLogic = FindObjectOfType<RunnersLogic>();

        attackSkill.SetActive(false);
        shieldSkill.SetActive(false);
    }

    public IEnumerator ObjectActivationRoutine()
    {
        while (true)
        {
            attackSkill.SetActive(true);
            shieldSkill.SetActive(false);
            yield return new WaitForSeconds(2f);

            attackSkill.SetActive(false);
            shieldSkill.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Castle"))
        {
            Debug.Log("King reached the finish");
            if (victoryUI != null)
            {
                victoryUI.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Castle"))
        {
            Debug.Log("King reached the finish");
            if (victoryUI != null)
            {
                victoryUI.SetActive(true);
            }
        }
    }

}