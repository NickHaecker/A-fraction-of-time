using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class StateManager
{
    static string path = "/player.data";
    public static Player LoadPlayer(Player player)
    {
        // string path = Application.persistentDataPath + StateManager.path;

        // if (File.Exists(path))
        // {
        //     BinaryFormatter formatter = new BinaryFormatter();
        //     FileStream stream = new FileStream(path, FileMode.Open);

        //     PlayerData data = formatter.Deserialize(stream) as PlayerData;
        //     stream.Close();
        //     return data;
        // }

        // Debug.Log("Load - LOADED PLAYER COMPLETED");
        // return CreateNewPlayerData();
        return null;
    }

    public static void SaveRecordData(Player player)
    {
        if (player.GetCharacterData().IS_SPLIT_REALITY_ORIGIN)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + StateManager.path;
            FileStream stream = new FileStream(path, FileMode.Create);

            SaveRecordData record = new SaveRecordData();
            record.name = player.GetCharacterData().NAME;
            record.records = player.GetInteractions();

            formatter.Serialize(stream, record);
            stream.Close();

            // Player.RealodPlayerData();
            Debug.Log("Save - COMPLETED");
        }
    }

    // public static PlayerData CreateNewPlayerData()
    // {
    //     PlayerData newData = new PlayerData();
    //     newData.NAME = "";

    //     PlayerAureaData golem = new PlayerAureaData();
    //     golem.aureaName = "Golem";
    //     golem.aureaLevel = 1;
    //     newData.AddAurea(golem);
    //     newData.AddAureaToSquad("Golem");

    //     PlayerAureaData inkubus = new PlayerAureaData();
    //     inkubus.aureaName = "Inkubus";
    //     inkubus.aureaLevel = 1;
    //     newData.AddAurea(inkubus);
    //     newData.AddAureaToSquad("Inkubus");

    //     PlayerAureaData crystal = new PlayerAureaData();
    //     crystal.aureaName = "Crystal";
    //     crystal.aureaLevel = 1;
    //     newData.AddAurea(crystal);
    //     newData.AddAureaToSquad("Crystal");


    //     SavePlayer(newData);

    //     Debug.Log(Application.persistentDataPath + StateManager.path);
    //     return newData;
    // }

    // public static bool DeletePlayerData()
    // {
    //     //TODO
    //     Debug.Log("Delete - DELETION OF PLAYER DATA COMPLETED");
    //     return false;
    // }
}