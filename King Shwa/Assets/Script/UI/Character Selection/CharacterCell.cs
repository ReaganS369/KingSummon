using UnityEngine;
using UnityEngine.UI;

public class CharacterCell : MonoBehaviour
{
    public Image characterProfileShow;

    public void UpdateProfileShow(Sprite profileSprite)
    {
        characterProfileShow.sprite = profileSprite;
    }
}
