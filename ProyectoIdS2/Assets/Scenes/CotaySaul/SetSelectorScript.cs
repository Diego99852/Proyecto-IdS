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


public class SetSelectorScript : MonoBehaviour
{

    [SerializeField] private string Nombre;
    public static int IdUsuario;
    public static int IdSet;
    public Button starButton;


    void Start()
    {
        IdUsuario = DataHolder.IdUsuario;
    }

    public void FuckGoBack()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void SearchSet() //Jalando, esta se manda llamar cuando le pican a la lupa
    {
        IDbConnection dbConnection = OpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        Debug.Log(Nombre);
        dbCommandReadValues.CommandText = string.Format("SELECT * FROM Sets WHERE Sets.Nombre = '{0}'", Nombre);
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {

        }
        dbConnection.Close(); // 20

    }

    public void getAllSets() //Jalando, esta la mandas llamar cuando quieras
    {
        IDbConnection dbConnection = OpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM Sets";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {

            
        }
        dbConnection.Close(); // 20
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
            Debug.Log("Database connection closed.");
        }
    }

    public void ReadStringInputBuscador(string input)
    {
        Nombre = input;
    }


    private IDbConnection OpenDatabase() // 3
    {
        try
        {
            string dbPath = Path.Combine(Application.dataPath, "Scenes", "CotaySaul", "DB", "legodb.db");
            string dbUri = "URI=file:" + dbPath;
            IDbConnection dbConnection = new SqliteConnection(dbUri);
            dbConnection.Open();
            Debug.Log("Database connection opened successfully.");
            return dbConnection;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to open database connection: " + e.Message);
            return null;
        }
    }
}
