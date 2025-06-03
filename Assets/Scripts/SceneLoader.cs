using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string tagToCheck;
    public string sceneName;
 
    public void LoadMyScene(string SceneName)
    {
        // Load the scene with the name passed as a parameter
        SceneManager.LoadScene(SceneName);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the collider has the specified tag
        if (col.CompareTag(tagToCheck))
        {
            // Load the scene
            LoadMyScene(sceneName);
        }
    }
}
