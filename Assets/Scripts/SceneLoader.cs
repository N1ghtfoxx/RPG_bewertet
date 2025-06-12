using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//<summary>
// SceneLoader is a script that loads a specified scene when an object with a specific tag enters its trigger collider.
// It is attached to a GameObject with a Collider2D component set as a trigger.
// The scene to load is specified by the 'sceneName' variable, and the tag to check is specified by 'tagToCheck'.
// </summary>
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
