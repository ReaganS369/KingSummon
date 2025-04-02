using UnityEngine;
using System.Collections;

public class CharacterCell : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public float duration = 2f;

    IEnumerator Start()
    {
        while (true)
        {
            // Activate object 1, deactivate object 2
            object1.SetActive(true);
            object2.SetActive(false);
            yield return new WaitForSeconds(duration);

            // Deactivate object 1, activate object 2
            object1.SetActive(false);
            object2.SetActive(true);
            yield return new WaitForSeconds(duration);
        }
    }
}
