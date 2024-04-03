using UnityEngine;

public class SpawnCharacters : MonoBehaviour
{
    [SerializeField] private GameObject kingTeam;
    [SerializeField] private GameObject pursuitTeam;
    [SerializeField] private int numberOfPlayersPerTeam;
    [SerializeField] private CharacterDatabase characterDatabase;

    private void Start()
    {
        SpawnCharactersForTeam(kingTeam, "GameField(KingTeam)");
        SpawnCharactersForTeam(pursuitTeam, "GameField(PursuitTeam)");
    }

    private void SpawnCharactersForTeam(GameObject teamObject, string fieldTag)
    {
        for (int i = 0; i < numberOfPlayersPerTeam; i++)
        {
            GameObject newCharacter = Instantiate(GetRandomCharacter(), GetRandomPosition(fieldTag), Quaternion.identity, teamObject.transform);
            // Modify the character's scripts as needed
            // For example, turn on playerMovement and turn off botMovement
        }
    }

    private GameObject GetRandomCharacter()
    {
        // Get a random character from the character database
        int randomIndex = Random.Range(0, characterDatabase.characters.Length);
        return characterDatabase.characters[randomIndex].prefab;
    }

    private Vector3 GetRandomPosition(string fieldTag)
    {
        GameObject[] fields = GameObject.FindGameObjectsWithTag(fieldTag);
        if (fields.Length == 0)
        {
            Debug.LogError("No field found with tag: " + fieldTag);
            return Vector3.zero;
        }

        Transform randomField = fields[Random.Range(0, fields.Length)].transform;
        return randomField.position;
    }
}
