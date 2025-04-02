using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{
    private Stack<string> sceneHistory = new Stack<string>();

    private void Awake()
    {
        // Ensure the GameObject persists between scenes
        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Method to load the previous scene
    public void LoadPreviousScene()
    {
        if (sceneHistory.Count > 0)
        {
            string previousSceneName = sceneHistory.Pop();
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("No previous scene in history.");
        }
    }

    // Method to record the current scene when loading a new scene
    public void RecordCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        sceneHistory.Push(currentSceneName);
    }

    // Method to quit the application
    public void QuitApplication()
    {
        Application.Quit();
#if UNITY_EDITOR
        // If running in the editor, stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
