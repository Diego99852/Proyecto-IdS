using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class movimientos : MonoBehaviour
{
    public Vector3[] destinos; 
    public Quaternion[] rotaciones; 
    public float duracion;
    public GameObject[] legos;

    private int currentLegoIndex = 0; 

    public void Move()
    {
        if (currentLegoIndex < legos.Length)
        {
            StartCoroutine(MoveLegoOneByOne());
        }
    }

    private IEnumerator MoveLegoOneByOne()
    {

        legos[currentLegoIndex].transform.DOMove(destinos[currentLegoIndex], duracion);
        legos[currentLegoIndex].transform.DORotateQuaternion(rotaciones[currentLegoIndex], duracion);


        yield return new WaitForSeconds(duracion);

        
        currentLegoIndex++;
        Debug.Log("pieza actual" + currentLegoIndex);
    }
}
