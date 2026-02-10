using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Prefab a generar")]
    [SerializeField] GameObject prefab;

    [Header("Tiempo entre spawns (segundos)")]
    [SerializeField] float tiempoSpawn = 5f;

    [Header("Plano")]
    [SerializeField] GameObject plano;

    [Header("Altura Y")]
    public float alturaY = 0.5474575f;
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

        for (int i = 0; i < 15; i++)
        {
            Vector3 puntoAleatorio = GenerarPuntoAleatorio();
            Instantiate(prefab, puntoAleatorio, Quaternion.Euler(-90f, 0f, 0f),transform);
            yield return new WaitForSeconds(tiempoSpawn);
        }

    }

    Vector3 GenerarPuntoAleatorio()
    {
        if (plano == null)
        {
            return Vector3.zero;
        }

        if (!plano.TryGetComponent<Renderer>(out var renderer))
            return Vector3.zero;

        float ancho = renderer.bounds.size.x;
        float largo = renderer.bounds.size.z;

        // Generar posición aleatoria dentro de los límites
        float x = Random.Range(-ancho / 2f, ancho / 2f);
        float z = Random.Range(-largo / 2f, largo / 2f);

        Vector3 puntoAleatorio = plano.transform.position + new Vector3(x, alturaY, z);

        return puntoAleatorio;
    }




}
