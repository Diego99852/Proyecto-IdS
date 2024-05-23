using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicaEntreEscenas : MonoBehaviour
{
    private void Awake()
    {
        var noDestruirEntreEscenas = FindObjectsOfType<logicaEntreEscenas>();
        if(noDestruirEntreEscenas.Length > 1 )
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
