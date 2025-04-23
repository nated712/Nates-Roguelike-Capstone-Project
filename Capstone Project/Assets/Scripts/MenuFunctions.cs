using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuFunctions : MonoBehaviour
{

    public void GoToGame(){

        SceneManager.LoadScene(2);

    }
    public void GoToCredits(){

        SceneManager.LoadScene(1);

    }

    public void GoToMainMenu(){

        SceneManager.LoadScene(0);

    }

    public void QuitGame(){

        Application.Quit();

    }
}
