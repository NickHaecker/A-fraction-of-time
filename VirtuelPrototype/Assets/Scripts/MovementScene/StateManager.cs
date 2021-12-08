using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
//using System;
public static class StateManager
{
    static string path = "/{{player}}.data";
    public static SavePlayerData LoadPlayer(string name)
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

    public static void SavePlayer(Player player)
    {
        GameObjectSaveData gameObjectSaveData = new GameObjectSaveData();
        //InteractionSaveData interactionSaveData = new InteractionSaveData();
        SavePlayerData playerToSafe = new SavePlayerData();
        List<Interaction> interactions = player.GetInteractions();
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

        if(interactions.Count > 0) {
            foreach(Interaction interaction in interactions)
            {
                InteractionSaveData interactionSaveData = new InteractionSaveData();
                interactionSaveData.Type = interaction.type.ToString();
                interactionSaveData.Actor = interaction.actor.GetCharacterData().name;

                GameObjectSaveData source = new GameObjectSaveData();
                source.Name = interaction.actor.gameObject.name;
                source.Tag = interaction.actor.gameObject.tag;
                source.Layer = interaction.actor.gameObject.layer;

                source.Position[0] = interaction.actor.gameObject.transform.position.x;
                source.Position[1] = interaction.actor.gameObject.transform.position.y;
                source.Position[2] = interaction.actor.gameObject.transform.position.z;

                source.Scale[0] = interaction.actor.gameObject.transform.localScale.x;
                source.Scale[1] = interaction.actor.gameObject.transform.localScale.y;
                source.Scale[2] = interaction.actor.gameObject.transform.localScale.z;

                source.Rotation[0] = interaction.actor.gameObject.transform.rotation.x;
                source.Rotation[1] = interaction.actor.gameObject.transform.rotation.y;
                source.Rotation[2] = interaction.actor.gameObject.transform.rotation.z;
                interactionSaveData.Source = source;
                GameObjectSaveData target = new GameObjectSaveData();
                target.Name = interaction.interactedObject.name;
                target.Tag = interaction.interactedObject.tag;
                target.Layer = interaction.interactedObject.layer;

                target.Position[0] = interaction.interactedObject.transform.position.x;
                target.Position[1] = interaction.interactedObject.transform.position.y;
                target.Position[2] = interaction.interactedObject.transform.position.z;

                target.Scale[0] = interaction.interactedObject.transform.localScale.x;
                target.Scale[1] = interaction.interactedObject.transform.localScale.y;
                target.Scale[2] = interaction.interactedObject.transform.localScale.z;

                target.Rotation[0] = interaction.interactedObject.transform.rotation.x;
                target.Rotation[1] = interaction.interactedObject.transform.rotation.y;
                target.Rotation[2] = interaction.interactedObject.transform.rotation.z;
                interactionSaveData.Target = target;

                interactionSaveData.TimeStamp = interaction.timestamp;

                playerToSafe.Interactions.Add(interactionSaveData);
            }
        }



        // if (player.GetCharacterData().IS_SPLIT_REALITY_ORIGIN)
        // {
             BinaryFormatter formatter = new BinaryFormatter();
             string path = Application.persistentDataPath + StateManager.path.Replace("{{player}}",playerToSafe.Name);
             FileStream stream = new FileStream(path, FileMode.Create);

        //     SaveRecordData record = new SaveRecordData();
        //     record.name = player.GetCharacterData().NAME;
        //     record.records = player.GetInteractions();

             formatter.Serialize(stream,playerToSafe);
             stream.Close();

        //     // Player.RealodPlayerData();
             Debug.Log("Save - COMPLETED");
        // }
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