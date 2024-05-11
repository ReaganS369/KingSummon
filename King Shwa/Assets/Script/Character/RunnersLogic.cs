using UnityEngine;
using Cinemachine;

public class RunnersLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject king;
    [SerializeField] private GameObject[] uiElements;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chaser"))
        {
            Debug.Log("Player dies");
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Castle"))
        {
            Debug.Log("Player reached the finish");
            SpecialKingSkillActive();
        }
    }

    private void SpecialKingSkillActive()
    {
        Debug.Log("Activating special king skill...");

        foreach (GameObject player in players)
        {
            if (player != null)
            {
                Debug.Log("Teleporting player to king's position...");
                player.transform.position = king.transform.position;
                player.SetActive(false);
            }
        }

        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement != null)
            {
                Debug.Log("Disabling UI element...");
                uiElement.SetActive(false);
            }
        }

        if (virtualCamera != null)
        {
            Debug.Log("Switching virtual camera follow target to king...");
            virtualCamera.m_Follow = king.transform;
        }
    }
}
