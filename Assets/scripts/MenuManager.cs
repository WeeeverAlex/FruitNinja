using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void jogo()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void options()
    {
        SceneManager.LoadScene("Options");
    }
}