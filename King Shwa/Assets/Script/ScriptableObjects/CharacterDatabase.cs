using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "CharacterDatabase")]
public class CharacterDatabase : ScriptableObject
{
    public enum TeamChoice
    {
        KingTeam,
        PursuitTeam
    }

    [System.Serializable]
    public struct CharacterData
    {
        public string name;
        public GameObject prefab;
        public TeamChoice team; 
        public Sprite characterPreview;
        public Sprite characterProfile;
    }

    public CharacterData[] characters;
}
