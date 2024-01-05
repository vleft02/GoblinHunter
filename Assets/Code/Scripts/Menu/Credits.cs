using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Animator creditsAnimator;

    private void OnEnable()
    {
        PlayCredits();
    }
    private void PlayCredits()
    {
        creditsAnimator.CrossFade("FinalCredits", 0.0f);
    }

    private void ReturnToTitle()
    {

        SceneManager.LoadScene("MainMenu");
    }

}
