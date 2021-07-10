using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using System;
using TMPro;

public class HacerDB : MonoBehaviour
{

    string rutaDB;
    string strConexion;
    string DBfile = "Puntos.sqlite";
    public TMPro.TextMeshProUGUI pos;

    IDbConnection dbConnection;
    IDbCommand dbComand;
    IDataReader reader;

    public List<string> rankings = new List<string>();

    private static HacerDB _instance;

    public static HacerDB Instance
    {
        get { return _instance; }

    }

    void Awake()
    {

        if (_instance == null)
        {

            _instance = this;
            // DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }
    }

    public void MostrarRanking()
    {

        ObtenerRanking();

        CerrarDB();


    }

    void CerrarDB()
    {

        //cerrar

        dbComand.Dispose();
        dbComand = null;
        dbConnection.Close();
        dbConnection = null;
    }

    private void ObtenerRanking()
    {

        AbrirDB();

        dbComand = dbConnection.CreateCommand();

        string sqlQuery = "select * from Ranking order by score DESC limit 10";

        dbComand.CommandText = sqlQuery;

        reader = dbComand.ExecuteReader();


        int index = 1;

        while (reader.Read())
        {
            string name = reader["Name"].ToString();

            int score = Convert.ToInt32(reader["Score"]);

            pos.text = pos.text + "\n" + "Nº " + (index++).ToString() + " - " + name + " - " + score.ToString();

        }

        reader.Close();
        reader = null;

    }

   

    public void InsertScore(string name, float score)
    {

        AbrirDB();

        dbComand = dbConnection.CreateCommand();

        string sqlQuery = "INSERT INTO Ranking (Name, Score) values( \"" + name + "\", \"" + score + "\")";

        dbComand.CommandText = sqlQuery;
        dbComand.ExecuteScalar();

        CerrarDB();

    }



  


 

    void AbrirDB()
    {

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            rutaDB = Application.dataPath + "/StreamingAssets/" + DBfile;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {

            rutaDB = Application.persistentDataPath + "/" + DBfile;

            if (!File.Exists(rutaDB))
            {
                WWW loadDB = new WWW("jar;file://" + Application.dataPath + DBfile);

                while (!loadDB.isDone)
                {

                }
                File.WriteAllBytes(rutaDB, loadDB.bytes);
            }

        }

        // crear y abrir conexion
        rutaDB = Application.dataPath + "/StreamingAssets/" + DBfile;
        strConexion = "URI=file:" + rutaDB;
        dbConnection = new SqliteConnection(strConexion);

        dbConnection.Open();
        //CreateTable();
    }
}

