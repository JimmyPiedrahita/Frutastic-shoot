using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControladorFrutas : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] frutas;

    private void Start()
    {
        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);
    }
    private void Update()
    {
        if (countFrutas() < 5)
        {
            crearFruta();
        }
    }
    private void crearFruta()
    {
        int numeroFruta = Random.Range(0, frutas.Length);
        Vector2 posicionAleatoria = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Instantiate(frutas[numeroFruta], posicionAleatoria, Quaternion.identity);
    }
    private int countFrutas()
    {
        GameObject[] banana = GameObject.FindGameObjectsWithTag("banana");
        GameObject[] pera = GameObject.FindGameObjectsWithTag("pera");
        GameObject[] manzana = GameObject.FindGameObjectsWithTag("manzana");
        GameObject[] sandia = GameObject.FindGameObjectsWithTag("sandia");
        GameObject[] naranja = GameObject.FindGameObjectsWithTag("naranja");
        GameObject[] coco = GameObject.FindGameObjectsWithTag("coco");
        GameObject[] fresa = GameObject.FindGameObjectsWithTag("fresa");
        GameObject[] piña = GameObject.FindGameObjectsWithTag("piña");
        GameObject[] cereza = GameObject.FindGameObjectsWithTag("cereza");
        return banana.Length+pera.Length+manzana.Length+sandia.Length+naranja.Length+coco.Length+fresa.Length+piña.Length+cereza.Length;
    }
}
