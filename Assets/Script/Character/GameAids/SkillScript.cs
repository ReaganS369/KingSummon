using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillScript : MonoBehaviour
{
    #region Stamina Settings
    [Header("Stamina Settings")]
    [SerializeField] private int maxStamina = 10;
    [SerializeField] private int slideStaminaCost = 3;
    [SerializeField] private int leftDashStaminaCost = 2;
    [SerializeField] private int rightDashStaminaCost = 2;
    #endregion

    #region Slide Skill Settings
    [Header("Slide Skill")]
    [SerializeField] private float slideCooldown = 5f;
    [SerializeField] private Text slideCooldownText;
    #endregion

    #region Left Dash Skill Settings
    [Header("Left Dash Skill")]
    [SerializeField] private float leftDashDistance = 2f;
    [SerializeField] private float leftDashSpeed = 80f;
    [SerializeField] private float leftDashCooldown = 3f;
    [SerializeField] private Text leftDashCooldownText;
    #endregion

    #region Right Dash Skill Settings
    [Header("Right Dash Skill")]
    [SerializeField] private float rightDashDistance = 2f;
    [SerializeField] private float rightDashSpeed = 80f;
    [SerializeField] private float rightDashCooldown = 3f;
    [SerializeField] private Text rightDashCooldownText;
    #endregion

    #region UI Elements
    [Header("UI Elements")]
    [SerializeField] private float spacing = 10f;
    [SerializeField] private Button slideSkillButton;
    [SerializeField] private Button leftDashSkillButton;
    [SerializeField] private Button rightDashSkillButton;
    [SerializeField] private GameObject staminaBlock;
    #endregion

    #region Player Components
    [Header("Player Components")]
    [SerializeField] private Rigidbody2D playerRigidbody;
    #endregion

    #region Private Variables
    private int currentStamina;
    private GameObject[] staminaBlocks;
    private bool isSlideSkillOnCooldown = false;
    private bool isLeftDashSkillOnCooldown = false;
    private bool isRightDashSkillOnCooldown = false;
    private Vector2 lastMovementDirection;
    #endregion

    #region Unity Methods
    private void Start()
    {
        InitializeStaminaUI();
        StartCoroutine(RechargeStamina());
        SubscribeToEvents();
        UpdateCooldownText();
    }
    #endregion

    #region Initialization
    private void InitializeStaminaUI()
    {
        currentStamina = maxStamina;
        staminaBlocks = new GameObject[maxStamina];
        staminaBlocks[0] = staminaBlock;

        for (int i = 1; i < maxStamina; i++)
        {
            GameObject newBlock = Instantiate(staminaBlock, staminaBlock.transform.parent);
            Vector3 newPosition = staminaBlock.transform.position;
            newPosition.x -= (staminaBlock.GetComponent<RectTransform>().rect.width + spacing) * i;
            newBlock.transform.position = newPosition;
            staminaBlocks[i] = newBlock;
        }
        UpdateStaminaUI();
    }

    private void SubscribeToEvents()
    {
        if (playerRigidbody != null)
        {
            PlayerDirection playerDirectionScript = playerRigidbody.GetComponent<PlayerDirection>();
            if (playerDirectionScript != null)
            {
                playerDirectionScript.OnDirectionChange += UpdateLastMovementDirection;
            }
        }
        slideSkillButton.onClick.AddListener(UseSlideSkill);
        leftDashSkillButton.onClick.AddListener(UseLeftDashSkill); // Update to include left dash button
        rightDashSkillButton.onClick.AddListener(UseRightDashSkill); // Update to include right dash button
    }
    #endregion

    #region Skill Usage
    private void UseSlideSkill()
    {
        if (!isSlideSkillOnCooldown && currentStamina >= slideStaminaCost)
        {
            currentStamina -= slideStaminaCost;
            UpdateStaminaUI();
            StartCoroutine(PerformSlideSkill(lastMovementDirection));
            StartCoroutine(StartSlideSkillCooldown());
        }
        else if (isSlideSkillOnCooldown)
        {
            Debug.Log("Slide skill is on cooldown.");
        }
        else
        {
            Debug.Log("Not enough stamina to use slide skill.");
        }
    }

    private void UseLeftDashSkill()
    {
        if (!isLeftDashSkillOnCooldown && currentStamina >= leftDashStaminaCost)
        {
            currentStamina -= leftDashStaminaCost;
            UpdateStaminaUI();
            StartCoroutine(PerformLeftDashSkill(lastMovementDirection));
            StartCoroutine(StartLeftDashSkillCooldown());
        }
        else if (isLeftDashSkillOnCooldown)
        {
            Debug.Log("Left dash skill is on cooldown.");
        }
        else
        {
            Debug.Log("Not enough stamina to use left dash skill.");
        }
    }

    private void UseRightDashSkill()
    {
        if (!isRightDashSkillOnCooldown && currentStamina >= rightDashStaminaCost)
        {
            currentStamina -= rightDashStaminaCost;
            UpdateStaminaUI();
            StartCoroutine(PerformRightDashSkill(lastMovementDirection));
            StartCoroutine(StartRightDashSkillCooldown());
        }
        else if (isRightDashSkillOnCooldown)
        {
            Debug.Log("Right dash skill is on cooldown.");
        }
        else
        {
            Debug.Log("Not enough stamina to use right dash skill.");
        }
    }
    #endregion

    #region Slide Skill Coroutine
    private IEnumerator PerformSlideSkill(Vector2 movementDirection)
    {
        PlayerMovement playerMovementScript = playerRigidbody.GetComponent<PlayerMovement>();
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        if (playerRigidbody != null)
        {
            float distance = 3f;
            float speed = 50f;
            float duration = distance / speed;

            playerRigidbody.AddForce(movementDirection.normalized * speed, ForceMode2D.Impulse);
            Debug.Log("Performing slide skill...");

            yield return new WaitForSeconds(duration);
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found on the player.");
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
    }
    #endregion

    #region Left Dash Skill Coroutine
    private IEnumerator PerformLeftDashSkill(Vector2 movementDirection)
    {
        PlayerMovement playerMovementScript = playerRigidbody.GetComponent<PlayerMovement>();
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        if (playerRigidbody != null)
        {
            Vector2 dashDirection = new Vector2(-movementDirection.y, movementDirection.x); // Rotate left
            float duration = leftDashDistance / leftDashSpeed;

            playerRigidbody.AddForce(dashDirection.normalized * leftDashSpeed, ForceMode2D.Impulse);
            Debug.Log("Performing left dash skill...");

            yield return new WaitForSeconds(duration);
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found on the player.");
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
    }
    #endregion

    #region Right Dash Skill Coroutine
    private IEnumerator PerformRightDashSkill(Vector2 movementDirection)
    {
        PlayerMovement playerMovementScript = playerRigidbody.GetComponent<PlayerMovement>();
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        if (playerRigidbody != null)
        {
            Vector2 dashDirection = new Vector2(movementDirection.y, -movementDirection.x); // Rotate right
            float duration = rightDashDistance / rightDashSpeed;

            playerRigidbody.AddForce(dashDirection.normalized * rightDashSpeed, ForceMode2D.Impulse);
            Debug.Log("Performing right dash skill...");

            yield return new WaitForSeconds(duration);
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found on the player.");
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
    }
    #endregion

    #region Cooldown Management
    private IEnumerator StartSlideSkillCooldown()
    {
        isSlideSkillOnCooldown = true;
        float cooldownTimer = slideCooldown;
        while (cooldownTimer > 0)
        {
            slideCooldownText.text = Mathf.CeilToInt(cooldownTimer).ToString();
            yield return new WaitForSeconds(1f);
            cooldownTimer -= 1f;
        }
        slideCooldownText.text = "";
        isSlideSkillOnCooldown = false;

    }

    private IEnumerator StartLeftDashSkillCooldown()
    {
        isLeftDashSkillOnCooldown = true;
        float cooldownTimer = leftDashCooldown;
        while (cooldownTimer > 0)
        {
            leftDashCooldownText.text = Mathf.CeilToInt(cooldownTimer).ToString();
            yield return new WaitForSeconds(1f);
            cooldownTimer -= 1f;
        }
        leftDashCooldownText.text = "";
        isLeftDashSkillOnCooldown = false;
    }

    private IEnumerator StartRightDashSkillCooldown()
    {
        isRightDashSkillOnCooldown = true;
        float cooldownTimer = rightDashCooldown;
        while (cooldownTimer > 0)
        {
            rightDashCooldownText.text = Mathf.CeilToInt(cooldownTimer).ToString();
            yield return new WaitForSeconds(1f);
            cooldownTimer -= 1f;
        }
        rightDashCooldownText.text = "";
        isRightDashSkillOnCooldown = false;
    }
    #endregion

    #region UI Updates
    private void UpdateStaminaUI()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            staminaBlocks[i].SetActive(i < currentStamina);
        }
    }

    private void UpdateCooldownText()
    {
        slideCooldownText.text = "";
        leftDashCooldownText.text = ""; // Update to include left dash cooldown text
        rightDashCooldownText.text = ""; // Update to include right dash cooldown text
    }
    #endregion

    #region Other Methods
    private void UpdateLastMovementDirection(Vector2 direction)
    {
        lastMovementDirection = direction;
    }

    private IEnumerator RechargeStamina()
    {
        while (true)
        {
            float rechargeDelay = CalculateRechargeDelay();
            yield return new WaitForSeconds(rechargeDelay);
            RechargeStaminaByOne();
        }
    }

    private float CalculateRechargeDelay()
    {
        int filledStaminaBlocks = maxStamina - currentStamina;
        if (filledStaminaBlocks >= 1 && filledStaminaBlocks <= 3)
        {
            return 8f;
        }
        else if (filledStaminaBlocks >= 4 && filledStaminaBlocks <= 6)
        {
            return 5f;
        }
        else if (filledStaminaBlocks >= 7 && filledStaminaBlocks <= 10)
        {
            return 3f;
        }
        else
        {
            return 0f; // No recharge if stamina is full or negative
        }
    }

    private void RechargeStaminaByOne()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina++;
            UpdateStaminaUI();
        }
    }
    #endregion
}
