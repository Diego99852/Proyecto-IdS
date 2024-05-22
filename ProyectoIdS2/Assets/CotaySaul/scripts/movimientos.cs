using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI; // Necesario para manejar el bot�n

public class movimientos : MonoBehaviour
{
    public Vector3[] destinos;
    public Quaternion[] rotaciones;
    public float duracion;
    public float jumpPower = 2.0f;
    public int numJumps = 1;
    public GameObject[] legos;
    public Button moveButton; // Referencia al bot�n

    private int currentLegoIndex = 0;
    private bool isMoving = false; // Bandera para controlar el movimiento

    void Start()
    {
        // Asignar el m�todo Move al bot�n
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
        moveButton.interactable = false; // Desactiva el bot�n para evitar m�s movimientos

        // Mueve el objeto actual en un arco y rota
        legos[currentLegoIndex].transform.DOJump(destinos[currentLegoIndex], jumpPower, numJumps, duracion);
        legos[currentLegoIndex].transform.DORotateQuaternion(rotaciones[currentLegoIndex], duracion);

        // Espera hasta que el movimiento y la rotaci�n terminen
        yield return new WaitForSeconds(duracion);

        // Incrementa el �ndice para mover el siguiente objeto en la pr�xima pulsaci�n
        currentLegoIndex++;
        Debug.Log("Pieza actual: " + currentLegoIndex);

        isMoving = false; // Termina el movimiento
        moveButton.interactable = true; // Reactiva el bot�n para permitir m�s movimientos
    }
}


