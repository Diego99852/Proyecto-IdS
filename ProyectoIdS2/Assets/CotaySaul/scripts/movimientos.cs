using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI; // Necesario para manejar el botón

public class movimientos : MonoBehaviour
{
    public Vector3[] destinos;
    public Quaternion[] rotaciones;
    public float duracion;
    public float jumpPower = 2.0f;
    public int numJumps = 1;
    public GameObject[] legos;
    public Button moveButton; // Referencia al botón

    private int currentLegoIndex = 0;
    private bool isMoving = false; // Bandera para controlar el movimiento

    void Start()
    {
        // Asignar el método Move al botón
        moveButton.onClick.AddListener(Move);
    }

    public void Move()
    {
        if (!isMoving && currentLegoIndex < legos.Length)
        {
            StartCoroutine(MoveLegoOneByOne());
        }
    }

    private IEnumerator MoveLegoOneByOne()
    {
        isMoving = true; // Inicia el movimiento
        moveButton.interactable = false; // Desactiva el botón para evitar más movimientos

        // Mueve el objeto actual en un arco y rota
        legos[currentLegoIndex].transform.DOJump(destinos[currentLegoIndex], jumpPower, numJumps, duracion);
        legos[currentLegoIndex].transform.DORotateQuaternion(rotaciones[currentLegoIndex], duracion);

        // Espera hasta que el movimiento y la rotación terminen
        yield return new WaitForSeconds(duracion);

        // Incrementa el índice para mover el siguiente objeto en la próxima pulsación
        currentLegoIndex++;
        Debug.Log("Pieza actual: " + currentLegoIndex);

        isMoving = false; // Termina el movimiento
        moveButton.interactable = true; // Reactiva el botón para permitir más movimientos
    }
}


