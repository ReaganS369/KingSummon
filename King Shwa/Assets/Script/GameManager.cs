using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] runners;
    [SerializeField] private GameObject[] chasers;
    [SerializeField] private GameObject king;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Button switchButton;
    [SerializeField] private Text numberOfAliveRunnersText;
    [SerializeField] private Text numberOfAliveChasersText;
    [SerializeField] private GameObject newZone; // New serialized field for the new zone

    private int currentRunnerIndex = 0;
    private int numberOfAliveRunners;
    private int numberOfAliveChasers;
    private List<GameObject> deactivatedRunners = new List<GameObject>();

    private void Start()
    {
        numberOfAliveRunners = runners.Length;
        numberOfAliveChasers = chasers.Length;
        UpdateAliveCounts();
        InvokeRepeating(nameof(CheckAliveCounts), 1f, 1f); // Check alive counts every second
    }

    public void RunnerDied(GameObject runner)
    {
        numberOfAliveRunners--;
        UpdateAliveCounts();
    }

    public void ChaserDied(GameObject chaser)
    {
        numberOfAliveChasers--;
        UpdateAliveCounts();
    }

    public void RunnerReachedFinish(GameObject runner)
    {
        numberOfAliveRunners--;
        UpdateAliveCounts();
        ActivateSpecialKingSkill();
    }

    public void ActivateSpecialKingSkill()
    {
        Debug.Log("Activating special king skill...");

        if (king != null)
        {
            KingLogic kingLogic = king.GetComponent<KingLogic>();
            if (kingLogic != null)
            {
                kingLogic.StartCoroutine(kingLogic.ObjectActivationRoutine());
            }
        }

        if (virtualCamera != null)
        {
            virtualCamera.m_Follow = king.transform;
        }

        // Temporarily deactivate all alive runners
        foreach (GameObject runner in runners)
        {
            if (runner != null && runner.activeSelf)
            {
                runner.SetActive(false);
                deactivatedRunners.Add(runner);
            }
        }

        switchButton.gameObject.SetActive(false); // Disable switch button when king skill is activated
    }
    public void DeactivateSpecialKingSkill()
    {
        Debug.Log("Deactivating special king skill...");

        // Reactivate all previously deactivated runners and move them to the king's position
        foreach (GameObject runner in deactivatedRunners)
        {
            if (runner != null)
            {
                runner.transform.position = king.transform.position;
                runner.SetActive(true);
            }
        }
        deactivatedRunners.Clear(); // Clear the list after reactivating runners

        // Activate the new zone and place it at the king's position
        if (newZone != null)
        {
            newZone.SetActive(true);
            newZone.transform.position = king.transform.position;
        }

        // Activate the king's BoxCollider2D
        BoxCollider2D kingCollider = king.GetComponent<BoxCollider2D>();
        if (kingCollider != null)
        {
            kingCollider.enabled = true;
        }
    }


    public void SwitchCameraToNextAvailableRunner()
    {
        Debug.Log("Activating cam");
        int startIndex = currentRunnerIndex;
        int newIndex = (currentRunnerIndex + 1) % (runners.Length + 1); // +1 for the king

        while (newIndex != startIndex)
        {
            if (newIndex < runners.Length && IsRunnerAlive(newIndex))
            {
                virtualCamera.m_Follow = runners[newIndex].transform;
                currentRunnerIndex = newIndex;
                return;
            }
            else if (newIndex == runners.Length && king != null && king.activeSelf)
            {
                virtualCamera.m_Follow = king.transform;
                currentRunnerIndex = newIndex;
                return;
            }

            newIndex = (newIndex + 1) % (runners.Length + 1);
        }
    }

    public void SwitchPlayer()
    {
        Debug.Log("Switching player");
        int startIndex = currentRunnerIndex;
        int newIndex = (currentRunnerIndex + 1) % (runners.Length + 1); // +1 for the king

        while (newIndex != startIndex)
        {
            if (newIndex < runners.Length && IsRunnerAlive(newIndex))
            {
                virtualCamera.m_Follow = runners[newIndex].transform;
                ActivatePlayerMovement(runners[newIndex]);
                DeactivateFollow(runners[newIndex]);
                currentRunnerIndex = newIndex;
                return;
            }
            else if (newIndex == runners.Length && king != null && king.activeSelf)
            {
                virtualCamera.m_Follow = king.transform;
                ActivatePlayerMovement(king);
                currentRunnerIndex = newIndex;
                return;
            }

            newIndex = (newIndex + 1) % (runners.Length + 1);
        }
    }

    private void ActivatePlayerMovement(GameObject player)
    {
        // Deactivate all player movement scripts first
        foreach (GameObject runner in runners)
        {
            if (runner != null)
            {
                PlayerMovement pm = runner.GetComponent<PlayerMovement>();
                if (pm != null)
                {
                    pm.enabled = false;
                }
            }
        }
        foreach (GameObject chaser in chasers)
        {
            if (chaser != null)
            {
                PlayerMovement pm = chaser.GetComponent<PlayerMovement>();
                if (pm != null)
                {
                    pm.enabled = false;
                }
            }
        }
        if (king != null)
        {
            PlayerMovement pm = king.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.enabled = false;
            }
        }

        // Activate the selected player's movement script
        if (player != null)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.enabled = true;
            }
        }

    }

    private void DeactivateFollow(GameObject player)
    {
        // Activate all FollowTarget scripts first
        foreach (GameObject runner in runners)
        {
            if (runner != null)
            {
                FollowTarget ft = runner.GetComponent<FollowTarget>();
                if (ft != null)
                {
                    ft.enabled = true;
                }
            }
        }
        foreach (GameObject chaser in chasers)
        {
            if (chaser != null)
            {
                FollowTarget ft = chaser.GetComponent<FollowTarget>();
                if (ft != null)
                {
                    ft.enabled = true;
                }
            }
        }
        if (king != null)
        {
            FollowTarget ft = king.GetComponent<FollowTarget>();
            if (ft != null)
            {
                ft.enabled = true;
            }
        }

        // Deactivate the selected player's FollowTarget script
        if (player != null)
        {
            FollowTarget ft = player.GetComponent<FollowTarget>();
            if (ft != null)
            {
                ft.enabled = false;
            }
        }
    }


    private bool IsRunnerAlive(int index)
    {
        return runners[index] != null && runners[index].activeSelf;
    }

    private bool IsChaserAlive(int index)
    {
        return chasers[index] != null && chasers[index].activeSelf;
    }

    private void UpdateAliveCounts()
    {
        numberOfAliveRunnersText.text = (numberOfAliveRunners + 1).ToString(); // Add +1 to the number of alive runners
        numberOfAliveChasersText.text = numberOfAliveChasers.ToString();
    }

    private void CheckAliveCounts()
    {
        int aliveRunners = 0;
        foreach (GameObject runner in runners)
        {
            if (runner != null && runner.activeSelf)
            {
                aliveRunners++;
            }
        }
        numberOfAliveRunners = aliveRunners;

        int aliveChasers = 0;
        foreach (GameObject chaser in chasers)
        {
            if (chaser != null && chaser.activeSelf)
            {
                aliveChasers++;
            }
        }
        numberOfAliveChasers = aliveChasers;

        UpdateAliveCounts();
    }
}