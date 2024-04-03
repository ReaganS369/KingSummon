using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGameScene);
    }

    private void StartGameScene()
    {
        if (!GameManager.Instance.SelectedCharacter.Equals(default(CharacterDatabase.CharacterData)))
        {
            // Load the game scene
            SceneManager.LoadScene("GameHUD");
        }
        else
        {
            Debug.LogWarning("No character selected. Please select a character before starting the game.");
        }
    }
}
