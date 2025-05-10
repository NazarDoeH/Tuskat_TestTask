using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTools : MonoBehaviour
{   
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
