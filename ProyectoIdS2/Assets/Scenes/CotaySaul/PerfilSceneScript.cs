using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System; // Asegúrate de agregar esta línea
using System.IO; // Asegúrate de agregar esta línea
using System.Data.SQLite; // Cambiado a System.Data.SQLite





public class PerfilSceneScript : MonoBehaviour
{

    [SerializeField] private int IdUsuario;
    [SerializeField] private string Nombre;
    [SerializeField] private string Pass;

    private string inmput;
    private string dbUri = "URI=file:legodb.db"; // 4


    // Start is called before the first frame update
    public void CreateProfile()
    {
        IDbConnection dbConnection = OpenDatabase();
        if (dbConnection == null)
        {
            Debug.LogError("Failed to create profile: Database connection is not established.");
            return;
        }

        try
        {
            using (IDbCommand dbCommandInsertValue = dbConnection.CreateCommand())
            {
                dbCommandInsertValue.CommandText = string.Format("INSERT INTO Usuario(Nombre,Pass) VALUES('{0}','{1}');", Nombre, Pass);
                dbCommandInsertValue.ExecuteNonQuery();
                Debug.Log("Profile created successfully.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to create profile: " + e.Message);
        }
        finally
        {
            dbConnection.Close();
            Debug.Log("Database connection closed.");
        }
    }

    public void joker()
    {
        using (var dbConnection = new SqliteConnection(dbUri))
        {
            dbConnection.Open();
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Usuario;";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log("Nombre: " + reader["Nombre"] + "Pass: " + reader["Pass"]);

                    }
                    reader.Close();
                }
            }
            dbConnection.Close();
        }
    }

    public void ReadStringInputNombre(string input)
    {
        Nombre = input;
        Debug.Log(input);
    }

    public void ReadStringInputPass(string input)
    {
        Pass = input;
        Debug.Log(input);
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
