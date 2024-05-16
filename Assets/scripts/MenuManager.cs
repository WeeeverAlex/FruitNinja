using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
