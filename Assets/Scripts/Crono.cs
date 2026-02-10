using TMPro;
using UnityEngine;
using System.Collections;

public class Crono : MonoBehaviour
{
    TMP_Text texto;
    int tiemposegundos;

    void Awake()
    {
        texto = GetComponent<TMP_Text>();
        texto.text = "0";
        tiemposegundos = 0;
        StartCoroutine(Contar());
    }

    IEnumerator Contar()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            tiemposegundos++;
            int minutos = tiemposegundos / 60;
            int segundos = tiemposegundos % 60;
            texto.text = $"{minutos:00}:{segundos:00}";
        }
    }
}
