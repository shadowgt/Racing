using UnityEngine;
using System.Collections;
using System;
using System.IO;

using System.Data;
using System.Text;
using Mono.Data.Sqlite;
using System.Collections.Generic;

//using Mono.Data.Sqlite;

public  class dbAccess : MonoBehaviour
{
    
    private string connection;
    private IDbConnection dbcon;
    private IDbCommand dbcmd;
    private IDataReader reader;
    
    // Use this for initialization
    void Start()
    {

    }
    
    public void OpenDB(string p)
    {
       
        Debug.Log("DEBUG: Call to OpenDB:");
        Debug.Log("DEBUG: Call to OpenDB:" + p);
        
        // check if file exists in Application.persistentDataPath
        string filepath = Application.persistentDataPath + "/" + p;
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("DEBUG: File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/" + p);
            // if it doesn't ->
            // open StreamingAssets directory and load the db -> 
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);
        }
        else
        {
            Debug.LogWarning("DEBUG: " + p + " This database File is exist ");
        }

        //open db connection
        connection = "URI=file:" + filepath;
        Debug.Log("DEBUG: Stablishing connection to: " + connection);
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        Debug.Log("DEBUG: Call to OpenDB: End");
        
    }

    public void CloseDB()
    {
        if (reader != null)
        {
            reader.Close(); // clean everything up
            reader = null;
        }

        if (dbcmd != null)
        {
            dbcmd.Dispose();
            dbcmd = null;
        }

        if (dbcmd != null)
        {
            dbcon.Close();
            dbcon = null;
        }

        
    }
    
    public IDataReader BasicQuery(string query)
    { // run a basic Sqlite query
        using (dbcmd = dbcon.CreateCommand()) // create empty command
        {
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
        }

        return reader; // return the reader

    }



    public int InsertIntoSingle(string tableName, string colName, string value)
    { // single insert
        string query;
        query = "INSERT INTO " + tableName + "(" + colName + ") " + "VALUES (" + value + ")";
        try
        {
            
            dbcmd = dbcon.CreateCommand(); // create empty command
            Debug.Log("DEBUG:  dbcmd = dbcon.CreateCommand(); // create empty command ");
            dbcmd.CommandText = query; // fill the command
            Debug.Log("DEBUG:  dbcmd = dbcon.CreateCommand(); // create empty command ");
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader

        }
        catch (Exception e)
        {

            Debug.Log(e);
            return 0;
        }
        return 1;
    }



    public List<HighScore> SingleSelectWhere(string i_query)
    { // Selects a single Item
        string query;
        query = i_query;
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = query;
        reader = dbcmd.ExecuteReader();

        Debug.Log("DEBUG: data count : " + reader.FieldCount);
        List<HighScore> result = new List<HighScore>();




        return result; // return matches
    }
    
    // Update is called once per frame
    void Update()
    {

    }


    public int InsertScore(string i_name , string i_score ,string i_Time)
    { // basic Insert with just values

        string query = "INSERT INTO score_tb(name,time,score) VALUES ('" + i_name + "','" + i_Time+"','" + i_score + "')";
        try
        {
            Debug.Log("DEBUG:  dbcmd = dbcon.CreateCommand(); ");
            using (dbcmd = dbcon.CreateCommand())
            {
                Debug.Log("DEBUG:  dbcmd.CommandText = query; ");
                dbcmd.CommandText = query;
                Debug.Log("DEBUG:  dbcmd.ExecuteReader(); ");
                dbcmd.ExecuteReader();
                Debug.Log("DEBUG:  dbcmd.ExecuteReader(); end ");
            }
        }
        catch(Exception e)
        {
            Debug.Log("DEBUG: "+e);
        }

        return 1;
    }


    public bool CreateTable(string name, string[] col, string[] colType)
    { // Create a table, name, column array, column type array
        string query;
        query = "CREATE TABLE " + name + "(" + col[0] + " " + colType[0];
        for (var i = 1; i < col.Length; i++)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ")";
        try
        {
            dbcmd = dbcon.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
        }
        catch (Exception e)
        {

            Debug.Log(e);
            return false;
        }
        return true;
    }

}