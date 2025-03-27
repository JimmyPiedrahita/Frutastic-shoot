using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider progressBar;
    private float progressValue = 0f;
    private float waitTime = 0.0001f; // Tiempo de espera en segundos

    private void Start()
    {
        progressBar.value = progressValue;
        StartCoroutine(IncreaseProgress());
    }

    private IEnumerator IncreaseProgress()
    {
        while (progressValue < 1f)
        {
            yield return new WaitForSeconds(waitTime); // Espera 1 segundo
            progressValue += 0.001f; // Aumenta el progreso en 0.1 (puedes ajustar este valor según tu necesidad)
            progressBar.value = progressValue;
        }

        // Cuando se alcanza el progreso completo, carga la siguiente escena
        SceneManager.LoadScene("MenuInicial");
    }
}
