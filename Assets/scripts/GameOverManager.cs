using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{    
    public void LoadSceneRestart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
