using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class movimientos : MonoBehaviour
{
    public Vector3[] destinos; // Array de destinos
    public float duracion;
    public GameObject[] legos;

    private int currentLegoIndex = 0; // Índice para rastrear el objeto actual a mover

    public void Move()
    {
        if (currentLegoIndex < legos.Length)
        {
            StartCoroutine(MoveLegoOneByOne());
        }
    }

    private IEnumerator MoveLegoOneByOne()
    {
        // Mueve el objeto actual a su destino correspondiente
        legos[currentLegoIndex].transform.DOMove(destinos[currentLegoIndex], duracion);

        // Espera hasta que el movimiento termine
        yield return new WaitForSeconds(duracion);

        // Incrementa el índice para mover el siguiente objeto en la próxima pulsación
        currentLegoIndex++;
    }
}
