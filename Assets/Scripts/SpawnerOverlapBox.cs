using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOverlapBox : MonoBehaviour
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

    int intentos;

    void Start()
    {

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {

        while (true)
        {
            Vector3 puntoAleatorio = GenerarPuntoAleatorio();
            if (intentos > 100)
            {
                Debug.LogWarning("No se pudieron generar más objetos.");
                yield break; // termina la corrutina
            }
            GameObject objeto=Instantiate(prefab, puntoAleatorio, Quaternion.Euler(-90f, 0f, 0f),transform);
                objeto.layer = LayerMask.NameToLayer("Prefabs"); // asigna el objeto a la capa "Prefabs"
                puntosGenerados.Add(objeto.transform.position);
            yield return new WaitForSeconds(tiempoSpawn);
        }

    }


    Vector3 GenerarPuntoAleatorio()
    {
        if (plano == null) return Vector3.zero;

        Renderer renderer = plano.GetComponent<Renderer>();
        if (renderer == null) return Vector3.zero;

        //float ancho = renderer.bounds.size.x-0.5f;
        //float largo = renderer.bounds.size.z-0.5f;

        // Limites automáticos usando extents
        float halfAncho = renderer.bounds.extents.x;
        float halfLargo = renderer.bounds.extents.z;

        // Margen para que el prefab no sobresalga del plano
        float halfAnchoPrefab = prefab.GetComponent<Renderer>().bounds.extents.x;
        float halfLargoPrefab = prefab.GetComponent<Renderer>().bounds.extents.z;

        float limiteX = halfAncho - halfAnchoPrefab;
        float limiteZ = halfLargo - halfLargoPrefab;




        Vector3 nuevoPunto;
        intentos = 0;

        do
        {
            float x = Random.Range(-limiteX, limiteX);
            float z = Random.Range(-limiteZ, limiteZ);
            nuevoPunto = plano.transform.position + new Vector3(x, alturaY, z);

            intentos++;
            if (intentos > 100)
            {
                break; // evita bucle infitino si el plano está muy lleno

            }
        } while (!EsPuntoLibre(nuevoPunto));

        return nuevoPunto;
    }

    bool EsPuntoLibre(Vector3 punto)
    {
        // Suponiendo que todos los puntos tienen un collider de tamaño similar
        // halfExtents = mitad del tamaño del collider + margen si quieres
        Vector3 halfExtents = new Vector3(distanciaMinima / 2f, 0.5f, distanciaMinima / 2f);

        // Revisar solo colliders en cierta capa
        int layerMask = LayerMask.GetMask("Prefabs");

        // Revisar colisiones con OverlapBox
        Collider[] hits = Physics.OverlapBox(punto, halfExtents, Quaternion.identity, layerMask);

        return hits.Length == 0; // si no hay colisiones, el punto es válido
    }





}
