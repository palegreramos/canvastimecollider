using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Reloj : MonoBehaviour
{
    private TMPro.TextMeshProUGUI texto;
    void Start()
    {
        texto = (TextMeshProUGUI)GetComponent<TMP_Text>();
        StartCoroutine(Clock());
    }

  

    IEnumerator Clock()
    {
        while (true)
        {
            texto.text = DateTime.Now.ToString("HH:mm:ss");
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
