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

    public void Perfil()
    {
        SceneManager.LoadScene("PerfilScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
