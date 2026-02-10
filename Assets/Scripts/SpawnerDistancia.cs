using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDistancia : MonoBehaviour
{
    [Header("Prefab a generar")]
    [SerializeField] private GameObject prefab;

    [Header("Tiempo entre spawns (segundos)")]
    [SerializeField] private float tiempoSpawn = 5f;

    [Header("Plano")]
    [SerializeField] private GameObject plano;

    [Header("Altura Y")]
    [SerializeField] private float alturaY = 0.5474575f;

    [Header("Distancia mínima")]
    [SerializeField] private float distanciaMinima = 2f;
  
    private List<Vector3> puntosGenerados = new List<Vector3>();
    void Start()
    {

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        //while (true)
        //{
        //    Vector3 puntoAleatorio = GenerarPuntoAleatorio();
        //    Instantiate(prefab, puntoAleatorio, Quaternion.Euler(-90f, 0f, 0f),transform);
        //    yield return new WaitForSeconds(tiempoSpawn);
        //}

        for (int i = 0; i < 25; i++)
        {
            Vector3 puntoAleatorio = GenerarPuntoAleatorio();
            Instantiate(prefab, puntoAleatorio, Quaternion.Euler(-90f, 0f, 0f),transform);
            yield return new WaitForSeconds(tiempoSpawn);
        }

    }


    Vector3 GenerarPuntoAleatorio()
    {
        if (plano == null) return Vector3.zero;

        Renderer renderer = plano.GetComponent<Renderer>();
        if (renderer == null) return Vector3.zero;

        float ancho = renderer.bounds.size.x - 0.5f;
        float largo = renderer.bounds.size.z - 0.5f;

        Vector3 nuevoPunto;
        int intentos = 0;

        do
        {
            float x = Random.Range(-ancho / 2f, ancho / 2f);
            float z = Random.Range(-largo / 2f, largo / 2f);
            nuevoPunto = plano.transform.position + new Vector3(x, alturaY, z);

            intentos++;
            if (intentos > 100) break; // evita bucles infinitos si el plano está muy lleno

        } while (!EsDistanciaValida(nuevoPunto));

        puntosGenerados.Add(nuevoPunto);
        return nuevoPunto;
    }

    bool EsDistanciaValida(Vector3 punto)
    {
        foreach (Vector3 p in puntosGenerados)
        {
            if (Vector3.Distance(p, punto) < distanciaMinima)
                return false;
        }
        return true;
    }




}
