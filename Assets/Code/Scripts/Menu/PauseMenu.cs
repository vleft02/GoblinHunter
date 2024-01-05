using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void GoToTitle()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void resume()
    {
        Debug.Log("Resuming");
        EventManager.PauseEvent();
    }

}
