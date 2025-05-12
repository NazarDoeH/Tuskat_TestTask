using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{   
    [SerializeField] private float delayToRestart = 2.0f;
    
    //Exits the game
    public void QuitGame()
    {
        Application.Quit(); 
    }

    //Restart scene after some time
    public void RestartSceneWithDelay()
    {
        StartCoroutine(RestartCoroutine(delayToRestart));
    }
    
    //Restart scene
    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    
    IEnumerator RestartCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartScene();
    }
}
