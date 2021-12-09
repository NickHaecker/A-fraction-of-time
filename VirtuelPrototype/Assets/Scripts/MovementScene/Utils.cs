using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class Utils
{
    public static List<Interaction> ConvertInteractions(List<InteractionSaveData> savedInteractions, List<CharacterData> data)
    {
        


        List<Interaction> interactions = new List<Interaction>();

        foreach(InteractionSaveData interaction in savedInteractions)
        {
            Interaction i = new Interaction();
            InteractionType type = (InteractionType)Enum.Parse(typeof(InteractionType),interaction.Type);
            Player actor = new GameObject("ut-18").AddComponent<Player>();
            actor.gameObject.hideFlags = HideFlags.HideInHierarchy;

            actor.Init(GetCharacterData(data,interaction.Actor));
            GameObject interactedObject = new GameObject("ut-21");
            interactedObject.hideFlags = HideFlags.HideInHierarchy;
  
            Debug.Log(interactedObject.name);
            float[] position = interaction.Source.Position;
            float[] scale = interaction.Source.Scale;
            float[] rotation = interaction.Source.Rotation;

            interactedObject.transform.position = new Vector3(position[0],position[1],position[2]);
            interactedObject.transform.rotation = Quaternion.Euler(new Vector3(rotation[0],rotation[1],rotation[2]));
            interactedObject.transform.localScale = new Vector3(scale[0],scale[1],scale[2]);

            interactedObject.name = interaction.Source.Name;
            interactedObject.layer = interaction.Source.Layer;
            interactedObject.tag = interaction.Source.Tag;

            Transform interactedPosition = new GameObject("ut-36").transform;
            interactedPosition.gameObject.hideFlags = HideFlags.HideInHierarchy;
            Debug.Log(interactedPosition.gameObject.name);
            float[] iposition = interaction.Target.Position;
            float[] iscale = interaction.Target.Scale;
            float[] irotation = interaction.Target.Rotation;

            interactedPosition.position = new Vector3(iposition[0],iposition[1],iposition[2]);
            interactedPosition.localScale = new Vector3(iscale[0],iscale[1],iscale[2]);
            interactedPosition.rotation = Quaternion.Euler(new Vector3(irotation[0],irotation[1],irotation[2]));

            int time = interaction.TimeStamp;

            i = i.Copy(type,actor,interactedObject,interactedPosition,time);
            interactions.Add(i);



        }
        return interactions;
    }

    private static CharacterData GetCharacterData(List<CharacterData> data, string whichOne)
    {
        CharacterData cD = null;
        foreach(CharacterData d in data)
        {
            if(d.NAME == whichOne)
            {
                cD = d;
            }
        }
        return cD;
    }
}
