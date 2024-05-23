using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public static string Nombre;
    public static int IdUsuario;
    public Text uiText;
    public GameObject pantallaOpciones;

    void Start()
    {
        Nombre = DataHolder.Nombre;
        IdUsuario = DataHolder.IdUsuario;
        UpdateText();
    }

    public void UpdateText()
    {
        uiText.text ="Bienvenido, " + Nombre;
        Debug.Log(uiText.text);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SetSelectorScene");
    }
    public void Options()
    {
        SceneManager.LoadScene("Opciones");
        pantallaOpciones.SetActive(true);
    }
    public void Perfil()
    {
        SceneManager.LoadScene("PerfilScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
