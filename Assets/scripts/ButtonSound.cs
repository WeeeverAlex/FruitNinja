using System.Collections; 
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;
    public string sceneName; // Nome da cena para carregar

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        StartCoroutine(PlaySoundAndChangeScene());
    }

    private IEnumerator PlaySoundAndChangeScene()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(sceneName);
    }
}