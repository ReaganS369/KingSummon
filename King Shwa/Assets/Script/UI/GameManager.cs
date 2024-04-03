using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManager).Name;
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private CharacterDatabase.CharacterData selectedCharacter;

    public CharacterDatabase.CharacterData SelectedCharacter
    {
        get { return selectedCharacter; }
        set { selectedCharacter = value; }
    }

    private CharacterDatabase.TeamChoice selectedTeam;

    public CharacterDatabase.TeamChoice SelectedTeam
    {
        get { return selectedTeam; }
        set { selectedTeam = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedCharacter(CharacterDatabase.CharacterData character)
    {
        selectedCharacter = character;
    }
}
