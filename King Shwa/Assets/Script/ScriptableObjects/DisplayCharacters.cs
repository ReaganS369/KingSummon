using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DisplayCharacters : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private Button characterButtonPrefab;
    [SerializeField] private int numberOfCharactersPerTeam = 3;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private List<GameObject> kingTeamCharacters = new List<GameObject>();
    private List<GameObject> pursuitTeamCharacters = new List<GameObject>();

    void Start()
    {
        CharacterDatabase.CharacterData selectedCharacter = GameManager.Instance.SelectedCharacter;
        CharacterDatabase.TeamChoice selectedTeam = GameManager.Instance.SelectedTeam;

        if (!selectedCharacter.Equals(default(CharacterDatabase.CharacterData)))
        {
            Debug.Log("Selected Character: " + selectedCharacter.name);
            Debug.Log("Selected Team: " + selectedTeam);

            SpawnTeams(selectedTeam);
        }
        else
        {
            Debug.LogError("No character selected in GameManager.");
        }
    }


    void SpawnTeams(CharacterDatabase.TeamChoice selectedTeam)
    {
        List<CharacterDatabase.CharacterData> shuffledCharacters = characterDatabase.characters.ToList();
        shuffledCharacters = ShuffleList(shuffledCharacters);

        CharacterDatabase.TeamChoice kingTeam = selectedTeam == CharacterDatabase.TeamChoice.KingTeam ? CharacterDatabase.TeamChoice.PursuitTeam : CharacterDatabase.TeamChoice.KingTeam;

        CharacterDatabase.CharacterData selectedCharacter = GameManager.Instance.SelectedCharacter;

        // Get random characters for each team, excluding the selected character
        List<CharacterDatabase.CharacterData> kingTeamCharactersData = GetRandomCharacters(shuffledCharacters, kingTeam, selectedCharacter);
        List<CharacterDatabase.CharacterData> selectedTeamCharactersData = GetRandomCharacters(shuffledCharacters, selectedTeam, selectedCharacter);

        // Add the selected character to its selected team
        if (selectedCharacter.team == selectedTeam)
        {
            selectedTeamCharactersData.Add(selectedCharacter);
        }
        else
        {
            kingTeamCharactersData.Add(selectedCharacter);
        }

        // Spawn characters for each team
        SpawnTeamCharacters(kingTeamCharacters, kingTeamCharactersData, kingTeam);
        SpawnTeamCharacters(pursuitTeamCharacters, selectedTeamCharactersData, selectedTeam);
    }

    List<CharacterDatabase.CharacterData> ShuffleList(List<CharacterDatabase.CharacterData> list)
    {
        List<CharacterDatabase.CharacterData> shuffledList = new List<CharacterDatabase.CharacterData>(list);
        int n = shuffledList.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            var value = shuffledList[k];
            shuffledList[k] = shuffledList[n];
            shuffledList[n] = value;
        }
        return shuffledList;
    }

    List<CharacterDatabase.CharacterData> GetRandomCharacters(List<CharacterDatabase.CharacterData> characters, CharacterDatabase.TeamChoice team, CharacterDatabase.CharacterData selectedCharacter)
    {
        // Initialize an empty list to store randomly selected characters
        List<CharacterDatabase.CharacterData> randomCharacters = new List<CharacterDatabase.CharacterData>();

        // Initialize a counter to keep track of how many characters are added
        int count = 0;

        // Loop through each character in the provided list of characters
        foreach (var character in characters)
        {
            // Skip the selected character (if it matches the provided selectedCharacter)
            if (character.Equals(selectedCharacter))
                continue;

            // Check if the current character belongs to the specified team
            if (character.team == team)
            {
                // Add the character to the list of random characters
                randomCharacters.Add(character);

                // Increment the counter to keep track of the number of characters added
                count++;
            }
            if (team == GameManager.Instance.SelectedTeam && count >= numberOfCharactersPerTeam - 1)
                break; // If so, exit the loop
            else if (team != GameManager.Instance.SelectedTeam && count >= numberOfCharactersPerTeam)
                break; // If so, exit the loop
        }

        // Return the list of randomly selected characters
        return randomCharacters;
    }

    void SpawnTeamCharacters(List<GameObject> teamCharacters, List<CharacterDatabase.CharacterData> teamData, CharacterDatabase.TeamChoice team)
    {
        // Create a new parent object for this team
        GameObject teamParentObject = new GameObject(team.ToString() + "Parent");

        foreach (var characterData in teamData)
        {
            // Instantiate the character prefab under the team's parent
            GameObject character = Instantiate(characterData.prefab, teamParentObject.transform);
            teamCharacters.Add(character);
            character.transform.position = GetRandomFieldPosition(team);
        }
    }

    Vector3 GetRandomFieldPosition(CharacterDatabase.TeamChoice team)
    {
        string tag = team == CharacterDatabase.TeamChoice.KingTeam ? "GameField(KingTeam)" : "GameField(PursuitTeam)";
        GameObject[] gameFieldObjects = GameObject.FindGameObjectsWithTag(tag);

        if (gameFieldObjects.Length == 0)
        {
            Debug.LogError("No game field objects found with tag: " + tag);
            return Vector3.zero;
        }

        Transform randomField = gameFieldObjects[UnityEngine.Random.Range(0, gameFieldObjects.Length)].transform;

        Collider2D fieldCollider = randomField.GetComponent<Collider2D>();
        if (fieldCollider == null)
        {
            Debug.LogError("Collider2D not found on the field object: " + randomField.name);
            return randomField.position;
        }

        float minOffset = 1f;
        float maxOffset = 2f;

        float offsetX = UnityEngine.Random.Range(minOffset, maxOffset);
        float offsetY = UnityEngine.Random.Range(minOffset, maxOffset);

        Vector3 randomPosition = new Vector3(
            UnityEngine.Random.Range(fieldCollider.bounds.min.x + offsetX, fieldCollider.bounds.max.x - offsetX),
            UnityEngine.Random.Range(fieldCollider.bounds.min.y + offsetY, fieldCollider.bounds.max.y - offsetY),
            0f
        );
        return randomPosition;
    }
}
