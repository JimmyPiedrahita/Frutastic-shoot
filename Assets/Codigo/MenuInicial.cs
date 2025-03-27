using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
    public AudioSource audioSource;
    private void Start()
    {
        audioSource.Play();
    }
    public void jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void salir()
    {
        Application.Quit();
    }
}
