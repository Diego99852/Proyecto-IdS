using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI; // Necesario para manejar el botón
using UnityEngine.SceneManagement;

public class movimientos : MonoBehaviour
{
    public Vector3[] destinos;
    public Quaternion[] rotaciones;
    public Vector3[] destinosCaja;
    public float duracion;
    public float jumpPower = 2.0f;
    public int numJumps = 1;
    public GameObject[] legos;
    public Button BotonAdelante;
    public Button BotonAtras;

    private int currentLegoIndex = 0;
    private bool isMoving = false;

    void Start()
    {
        BotonAdelante.onClick.AddListener(Move);
        BotonAtras.onClick.AddListener(Return);
    }

    public void Move()
    {
        if (!isMoving && currentLegoIndex < legos.Length)
        {
            StartCoroutine(MoveLegoOneByOne());
        }
    }

    public void Return()
    {
        if (!isMoving && currentLegoIndex > 0)
        {
            StartCoroutine(ReturnLegoOneByOne());
        }
    }

    private IEnumerator MoveLegoOneByOne()
    {
        isMoving = true;
        BotonAdelante.interactable = false;
        BotonAtras.interactable = false;

        legos[currentLegoIndex].transform.DOJump(destinos[currentLegoIndex], jumpPower, numJumps, duracion);
        legos[currentLegoIndex].transform.DORotateQuaternion(rotaciones[currentLegoIndex], duracion);

        yield return new WaitForSeconds(duracion);

        currentLegoIndex++;

        isMoving = false;
        BotonAdelante.interactable = true;
        BotonAtras.interactable = true;
    }

    private IEnumerator ReturnLegoOneByOne()
    {
        isMoving = true;
        BotonAdelante.interactable = false;
        BotonAtras.interactable = false;

        currentLegoIndex--; 

        legos[currentLegoIndex].transform.DOJump(destinosCaja[currentLegoIndex], jumpPower, numJumps, duracion);
        legos[currentLegoIndex].transform.DORotateQuaternion(rotaciones[currentLegoIndex], duracion);

        yield return new WaitForSeconds(duracion);

        isMoving = false;
        BotonAdelante.interactable = true;
        BotonAtras.interactable = true;
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("SetSelectorScene");
    }
}

