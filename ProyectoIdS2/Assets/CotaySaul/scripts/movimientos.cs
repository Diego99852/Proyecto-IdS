using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI; // Necesario para manejar el bot�n
using UnityEngine.SceneManagement;

public class movimientos : MonoBehaviour
{
    public Vector3[] destinos;
    public Quaternion[] rotaciones;
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
        BotonAtras.onClick.AddListener(Move);
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

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("SetSelectorScene");
    }
}


