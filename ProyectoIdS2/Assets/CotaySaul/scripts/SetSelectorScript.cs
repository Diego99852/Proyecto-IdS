using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System; // Asegúrate de agregar esta línea
using System.IO; // Asegúrate de agregar esta línea
using System.Data.SQLite; // Cambiado a System.Data.SQLite
using UnityEngine.UI;
using TMPro;

public class SetSelectorScript : MonoBehaviour
{

    [SerializeField] private string Nombre;
    public static int IdUsuario;
    public static int IdSet;
    public Button starButton;
    public List<TMP_Text> textMeshProList;
    public List<Button> buttonsList;
    public List<Button> favoritos;
    public List<string> sceneNamesList;
    public TMP_Dropdown dropdownEdad;
    public TMP_Dropdown dropdownCategoria;
    public List<int> listaIdSet;
    public int index;

    void Start()
    {
        IdUsuario = DataHolder.IdUsuario;
        getAllSets();
        dropdownEdad.onValueChanged.AddListener(delegate { DropdownValueChanged(dropdownEdad); });
        dropdownCategoria.onValueChanged.AddListener(delegate { DropdownValueChangedCategoria(dropdownCategoria); });
    }

    public void FuckGoBack()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void EscenaAvioneta()
    {
        SceneManager.LoadScene("AvionetaScene");
    }
    public void EscenaAvion()
    {
        SceneManager.LoadScene("AvionScene");
    }
    public void EscenaBarco()
    {
        SceneManager.LoadScene("BarquitoScene");
    }

    public void SearchSet() //Jalando, esta se manda llamar cuando le pican a la lupa
    {
        listaIdSet.Clear();
        foreach (var button in buttonsList)
        {
            button.gameObject.SetActive(false);
        }
        foreach (var textMesh in textMeshProList)
        {
            textMesh.gameObject.SetActive(false);
        }

        IDbConnection dbConnection = OpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        Debug.Log(Nombre);
        dbCommandReadValues.CommandText = string.Format("SELECT * FROM Sets WHERE Sets.Nombre LIKE '%{0}%'", Nombre);
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        int index = 0;
        while (dataReader.Read() && index < textMeshProList.Count)
        {
            string setText = "Nombre:" + dataReader.GetString(1) + "\n\nDificultad: " + dataReader.GetString(2) + "\n\nN° Piezas: " + dataReader.GetInt32(3) + "\n\nEdad: " + dataReader.GetInt32(4) + "\nCategoria: " + dataReader.GetString(5); ;
            //int setId = dataReader.GetInt32(0); // Suponiendo que el primer campo es el IdSet
            listaIdSet.Add(dataReader.GetInt32(0));
            //Debug.Log(listaIdSet[index].ToString());

            textMeshProList[index].text = setText;
            textMeshProList[index].gameObject.SetActive(true);
            buttonsList[index].gameObject.SetActive(true);

            string sceneName = dataReader.GetString(6); // Obtener el nombre de la escena correspondiente

            buttonsList[index].onClick.RemoveAllListeners();
            buttonsList[index].onClick.AddListener(() => OnSetButtonClicked(sceneName));
            index++;
        }
        dbConnection.Close(); // 20

    }

    public void getAllSets() //Jalando, esta la mandas llamar cuando quieras
    {
        listaIdSet.Clear();

        foreach (var button in buttonsList)
        {
            button.gameObject.SetActive(false);
        }
        foreach (var textMesh in textMeshProList)
        {
            textMesh.gameObject.SetActive(false);
        }
        IDbConnection dbConnection = OpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM Sets";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        int index = 0;
        while (dataReader.Read() && index < textMeshProList.Count)
        {
            textMeshProList[index].gameObject.SetActive(true);
            buttonsList[index].gameObject.SetActive(true);

            textMeshProList[index].text = "Nombre:" + dataReader.GetString(1) + "\n\nDificultad: " + dataReader.GetString(2) + "\n\nN° Piezas: " + dataReader.GetInt32(3) + "\n\nEdad: " + dataReader.GetInt32(4) + "\nCategoria: " + dataReader.GetString(5);
            listaIdSet.Add(dataReader.GetInt32(0));

            Debug.Log(listaIdSet[index].ToString());

            string sceneName = dataReader.GetString(6); 
            buttonsList[index].onClick.RemoveAllListeners();
            buttonsList[index].onClick.AddListener(() => OnSetButtonClicked(sceneName));
            index++;
            //textMeshPro.text = dataReader.GetString(1) + dataReader.GetString(2);
        }


        dbConnection.Close(); // 20
    }

    public void DropdownValueChanged(TMP_Dropdown change)
    {
        // Obtener la opción seleccionada del dropdown
        string selectedOption = change.options[change.value].text;

        // Llamar a una función para buscar sets basados en la opción seleccionada
        SearchSetByDropdownEdad(selectedOption);
    }

    public void DropdownValueChangedCategoria(TMP_Dropdown change)
    {
        // Obtener la opción seleccionada del dropdown
        string selectedOption = change.options[change.value].text;
        Debug.Log("Dropdown selection changed: " + selectedOption);

        // Llamar a una función para buscar sets basados en la opción seleccionada
        SearchSetByDropdownCategoria(selectedOption);
    }

    public void SearchSetByDropdownEdad(string option)
    {
        listaIdSet.Clear();
        IDbConnection dbConnection = OpenDatabase();
        if (dbConnection == null)
        {
            Debug.LogError("Failed to search sets by dropdown option: Database connection is not established.");
            return;
        }

        try
        {
            IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
            dbCommandReadValues.CommandText = string.Format("SELECT * FROM Sets WHERE Sets.Edad <= '{0}'", option); // Asumiendo que hay una columna 'Category'
            IDataReader dataReader = dbCommandReadValues.ExecuteReader();

            // Desactivar todos los botones y textos al inicio
            foreach (var button in buttonsList)
            {
                button.gameObject.SetActive(false);
            }
            foreach (var textMesh in textMeshProList)
            {
                textMesh.gameObject.SetActive(false);
            }

            int index = 0;
            while (dataReader.Read() && index < textMeshProList.Count)
            {
                string setText = "Nombre:" + dataReader.GetString(1) + "\n\nDificultad: " + dataReader.GetString(2) + "\n\nN° Piezas: " + dataReader.GetInt32(3) + "\n\nEdad: " + dataReader.GetInt32(4) + "\nCategoria: " + dataReader.GetString(5);
                listaIdSet.Add(dataReader.GetInt32(0));

                textMeshProList[index].text = setText;
                textMeshProList[index].gameObject.SetActive(true);
                buttonsList[index].gameObject.SetActive(true);

                string sceneName = dataReader.GetString(6); // Obtener el nombre de la escena correspondiente

                buttonsList[index].onClick.RemoveAllListeners();
                buttonsList[index].onClick.AddListener(() => OnSetButtonClicked(sceneName));

                index++;
            }

            dataReader.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to search sets by dropdown option: " + e.Message);
        }
        finally
        {
            dbConnection.Close();
            Debug.Log("Database connection closed.");
        }
    }

    public void SearchSetByDropdownCategoria(string option)
    {
        listaIdSet.Clear();
        IDbConnection dbConnection = OpenDatabase();
        if (dbConnection == null)
        {
            Debug.LogError("Failed to search sets by dropdown option: Database connection is not established.");
            return;
        }

        try
        {
            IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
            dbCommandReadValues.CommandText = string.Format("SELECT * FROM Sets WHERE Sets.Categoria = '{0}'", option); // Asumiendo que hay una columna 'Category'
            IDataReader dataReader = dbCommandReadValues.ExecuteReader();

            // Desactivar todos los botones y textos al inicio
            foreach (var button in buttonsList)
            {
                button.gameObject.SetActive(false);
            }
            foreach (var textMesh in textMeshProList)
            {
                textMesh.gameObject.SetActive(false);
            }

            int index = 0;
            while (dataReader.Read() && index < textMeshProList.Count)
            {
                string setText = "Nombre:" + dataReader.GetString(1) + "\n\nDificultad: " + dataReader.GetString(2) + "\n\nN° Piezas: " + dataReader.GetInt32(3) + "\n\nEdad: " + dataReader.GetInt32(4) + "\nCategoria: " + dataReader.GetString(5);
                listaIdSet.Add(dataReader.GetInt32(0));

                textMeshProList[index].text = setText;
                textMeshProList[index].gameObject.SetActive(true);
                buttonsList[index].gameObject.SetActive(true);

                string sceneName = dataReader.GetString(6); // Obtener el nombre de la escena correspondiente

                buttonsList[index].onClick.RemoveAllListeners();
                buttonsList[index].onClick.AddListener(() => OnSetButtonClicked(sceneName));
                index++;
            }

            dataReader.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to search sets by dropdown option: " + e.Message);
        }
        finally
        {
            dbConnection.Close();
            Debug.Log("Database connection closed.");
        }
    }

    public void addToFavourites() //Ya tiene el IdUsuario que lo esta usando, solo falta que busques una manera de agarrar el IdSet y listo
                                  //Pienso que se puede hacer mediante el click del botón grande, no de la estrella, puedes escribir el ID en el botón y despues buscarlo con el .text del string
                                  //esta se manda llamar cada que se le pica al botón de la estrella y la variable de hasta arriba que se llama starButton esta asociado al botón grande (no a la estrella) para que puedas sacar el texto de esa variable
    {
        IDbConnection dbConnection = OpenDatabase();
        if (dbConnection == null)
        {
            Debug.LogError("Failed to add to favourites: Database connection is not established.");
            return;
        }

        try
        {
            for (int i = 0; i < favoritos.Count; i++)
            {
                if (favoritos[i] == starButton)
                {
                    index = i;
                }
            }
            Debug.Log(index);
            using (IDbCommand dbCommandInsertValue = dbConnection.CreateCommand())
            {
                dbCommandInsertValue.CommandText = string.Format("INSERT INTO Favoritos(IdSet_Sets,IdUsuario_Usuario) VALUES('{0}','{1}');", IdSet, IdUsuario);
                dbCommandInsertValue.ExecuteNonQuery();
                Debug.Log("Added to favourites successfully.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to add to favourites: " + e.Message);
        }
        finally
        {
            dbConnection.Close();
            //Debug.Log("Database connection closed.");
        }
    }

    public void OnSetButtonClicked(string sceneName)
    {
        // Cambiar a la escena correspondiente
        SceneManager.LoadScene(sceneName);
    }

    public void GetClickedButton()
    {
        starButton = this.gameObject.GetComponent<Button>();
        addToFavourites();
    }

    public void ReadStringInputBuscador(string input)
    {
        Nombre = input;
    }


    private IDbConnection OpenDatabase() // 3
    {
        try
        {
            string dbPath = Path.Combine(Application.dataPath, "CotaySaul", "DB", "legodb.db");
            string dbUri = "URI=file:" + dbPath;
            IDbConnection dbConnection = new SqliteConnection(dbUri);
            dbConnection.Open();
            //Debug.Log("Database connection opened successfully.");
            return dbConnection;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to open database connection: " + e.Message);
            return null;
        }
    }
}
