using System.Linq;
using UnityEngine;
public class ControladorEnemigos : MonoBehaviour
{
    private string enemyTag = "Enemy";
    private float minX, maxX, minY, maxY;
    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject enemigo;
    [SerializeField] public float tiempoEnemigos;
    private float tiempoSiguienteEnemigo;
    public static ControladorEnemigos instance;

    private void Start()
    {
        instance = this;
        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);
    }
    private void Update()
    {
        int cantidadEnemigos = countEnemies();
        tiempoSiguienteEnemigo += Time.deltaTime;
        if ( tiempoSiguienteEnemigo >= tiempoEnemigos && cantidadEnemigos < 150)
        {
            tiempoSiguienteEnemigo = 0;
            crearEnemigo();
        }
    }
    private void crearEnemigo()
    {
        Vector2  posicionAleatoria = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Instantiate(enemigo, posicionAleatoria, Quaternion.identity);
    }
    private int countEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        return enemies.Length;
    }
}
