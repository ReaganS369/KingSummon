using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class CharacterSelectionManager : MonoBehaviour
{
    public event Action<CharacterDatabase.CharacterData> OnCharacterSelected; // Event to notify when a character is selected

    public CharacterDatabase characterDatabase;
    public GameObject characterChoose;
    public GameObject characterCellPrefab;
    public Image characterPreviewSprite;
    public TMP_Text characterPreviewText;
    public Button kingTeamButton;
    public Button pursuitTeamButton;

    private CharacterDatabase.TeamChoice selectedTeam = CharacterDatabase.TeamChoice.KingTeam; // Default to King Team

    void Start()
    {
        kingTeamButton.onClick.AddListener(() => OnTeamButtonClicked(CharacterDatabase.TeamChoice.KingTeam));
        pursuitTeamButton.onClick.AddListener(() => OnTeamButtonClicked(CharacterDatabase.TeamChoice.PursuitTeam));
        SpawnTeams();
    }

    private void SpawnTeams()
    {
        ClearCharacterCells(); // Clear existing character cells before spawning new ones

        foreach (CharacterDatabase.CharacterData characterData in characterDatabase.characters)
        {
            // Only spawn character cells that belong to the selected team
            if (characterData.team == selectedTeam)
            {
                GameObject newCharacterCell = Instantiate(characterCellPrefab, characterChoose.transform);
                CharacterCell cellScript = newCharacterCell.GetComponent<CharacterCell>();
                cellScript.UpdateProfileShow(characterData.characterProfile);

                // Add a click listener to each character cell
                newCharacterCell.GetComponent<Button>().onClick.AddListener(() => OnCharacterCellSelected(characterData));
            }
        }
    }

    private void OnTeamButtonClicked(CharacterDatabase.TeamChoice team)
    {
        selectedTeam = team;
        SpawnTeams(); // Respawn character cells for the selected team
    }
    private void OnCharacterCellSelected(CharacterDatabase.CharacterData selectedCharacter)
    {
        // Update the character preview sprite
        characterPreviewSprite.sprite = selectedCharacter.characterPreview;
        // Update the character preview text
        characterPreviewText.text = selectedCharacter.name;

        // Set the selected character and team in the GameManager
        GameManager.Instance.SelectedCharacter = selectedCharacter;
        GameManager.Instance.SelectedTeam = selectedCharacter.team;

        // Trigger the event
        OnCharacterSelected?.Invoke(selectedCharacter);
    }

    private void ClearCharacterCells()
    {
        foreach (Transform child in characterChoose.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
