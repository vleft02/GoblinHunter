using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public Animator creditsAnimator;
    public GameObject mainMenu;

    private void OnEnable()
    {
        PlayCredits();
    }
    private void PlayCredits() 
    {
        creditsAnimator.CrossFade("Credits", 0.0f);
    }

    private void ReturnToMainMenu() 
    {
        mainMenu.SetActive(true);
        GameObject.Find("CreditsMenu").SetActive(false);
    }
}
