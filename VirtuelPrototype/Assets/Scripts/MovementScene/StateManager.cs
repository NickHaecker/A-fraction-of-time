using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

using System;
[Serializable]
public static class StateManager
{
    static string path = "/{{player}}.data";
    public static SavePlayerData LoadPlayer(string name)
    {
        string path = Application.persistentDataPath + StateManager.path.Replace("{{player}}",name);

        if(File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            SavePlayerData data = formatter.Deserialize(stream) as SavePlayerData;
            stream.Close();

            Debug.Log("Load - COMPLETED - " + name);

            return data;

        }
        return null;
    }

    public static void SavePlayer(Player player)
    {
        SavePlayerData savedDataFromPlayer = LoadPlayer(player.GetCharacterData().NAME);

       

        GameObjectSaveData gameObjectSaveData = new GameObjectSaveData();

        SavePlayerData playerToSafe = new SavePlayerData();
        List<InteractionSaveData> interactions = player.GetInteractions();

        if(savedDataFromPlayer != null)
        {
            playerToSafe = savedDataFromPlayer;
            //List<InteractionSaveData> currentSaved = savedDataFromPlayer.Interactions;
            //if(currentSaved.Count > 0 && interactions.Count > 0)
            //{
                //Debug.Log(2);
            interactions.AddRange(savedDataFromPlayer.Interactions);
            //}
        }

        playerToSafe.Interactions = interactions;
        CharacterData characterData = player.GetCharacterData();
        GameObject playerObject = player.gameObject;

        playerToSafe.Name = characterData.NAME;
        playerToSafe.UID = characterData.NAME;
        gameObjectSaveData.Name = playerObject.name;
        gameObjectSaveData.Tag = playerObject.tag;
        gameObjectSaveData.Layer = playerObject.layer;

        gameObjectSaveData.Position[0] = playerObject.transform.position.x;
        gameObjectSaveData.Position[1] = playerObject.transform.position.y;
        gameObjectSaveData.Position[2] = playerObject.transform.position.z;

        gameObjectSaveData.Scale[0] = playerObject.transform.localScale.x;
        gameObjectSaveData.Scale[1] = playerObject.transform.localScale.y;
        gameObjectSaveData.Scale[2] = playerObject.transform.localScale.z;

        gameObjectSaveData.Rotation[0] = playerObject.transform.rotation.x;
        gameObjectSaveData.Rotation[1] = playerObject.transform.rotation.y;
        gameObjectSaveData.Rotation[2] = playerObject.transform.rotation.z;

        playerToSafe.Position = gameObjectSaveData;





        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + StateManager.path.Replace("{{player}}",playerToSafe.Name);
        FileStream stream = new FileStream(path,FileMode.Create);


        formatter.Serialize(stream,playerToSafe);
        stream.Close();


        Debug.Log("Save - COMPLETED");

    }

    public static void DeleteData(List<string> names)
    {
        foreach(string name in names)
        {
            string path = Application.persistentDataPath + StateManager.path.Replace("{{player}}",name);

            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

}