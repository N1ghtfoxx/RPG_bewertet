using UnityEngine;

public class PauseMenueManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private BaseCharacterController baseCC;

    private void Start()
    {
        baseCC = FindObjectOfType<BaseCharacterController>();
    }

    public void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        DoStuff(pauseMenuUI.activeSelf);

        baseCC.PausePlayer(pauseMenuUI.activeSelf);
    }

    private void DoStuff(bool isEnabled)
    {
        if (isEnabled)
        {
            // populate
        }
        else
        {
            // clear
        }
    }
}
