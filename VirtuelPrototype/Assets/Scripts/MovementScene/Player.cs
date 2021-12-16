using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Player : MonoBehaviour
{
    // [SerializeField]
    // private string _name = null;
    // [SerializeField]
    // private string _description = null;
    // [SerializeField]
    // private bool _isSplitRealityOrigin = false;
    [SerializeField]
    private CharacterData _data = null;
    [SerializeField]
    private List<InteractionSaveData> _interactions;
    [SerializeField]
    private bool _isReconstructing = false;
    [SerializeField]
    private float _lastTimestamp = 0f;

    //public Action DeleteRecords;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //if(_data.IS_SPLIT_REALITY_ORIGIN)
        //{
        //    int y = 0;
        //    //Debug.Log(_interactions);
        //    foreach(Interaction i in _interactions)
        //    {
        //        Debug.Log(y + " " + i.type.ToString());
        //        y++;
        //    }
        //}
    }
    public string GetName()
    {
        return _data.NAME;
    }
    public void Init(CharacterData data)
    {
        _data = data;
        _interactions = new List<InteractionSaveData>();


    }

    public void InsertInteraction(InteractionSaveData interaction)
    {
        //Debug.Log("in Player " + interaction.S.position);
        _interactions.Add(interaction);
        //Debug.Log("in Player " + interaction.interactionPosition.position + " after add");
        Debug.Log(_interactions);

    }
    public void ReconstructRecord(float timestamp)
    {
        //Debug.Log("current stamp " + timestamp);
        
        InteractionSaveData interaction = _interactions.Find(i => (_lastTimestamp < i.TimeStamp && i.TimeStamp <= timestamp));

        SetLastTimestamp(timestamp);

        //Debug.Log(interaction);
        //Debug.Log("start replay");

        //foreach(Interaction interaction in interactions)
        //{
        if (interaction != null)
        {
            InteractionType type = (InteractionType)Enum.Parse(typeof(InteractionType),interaction.Type);
            switch(type)
            {
                case InteractionType.WALK:
                    //Debug.Log("interacted :" + interaction.interactionPosition.position);
                    transform.position = new Vector3( interaction.Source.Position[0],interaction.Source.Position[1],interaction.Source.Position[2]);
                    transform.rotation = Quaternion.Euler(new Vector3(interaction.Source.Rotation[0],interaction.Source.Rotation[1],interaction.Source.Rotation[2]));
                    transform.localScale = new Vector3( interaction.Source.Scale[0],interaction.Source.Scale[1],interaction.Source.Scale[2]);
                    //            //CharacterController cC = this.gameObject.GetComponent<CharacterController>();
                    //            //Vector3 direction = new Vector3(interaction.interactionPosition.position.x - transform.position.x,interaction.interactionPosition.position.y - transform.position.y,interaction.interactionPosition.transform.position.z - transform.position.z);
                    //            //cC.Move(direction);
                    break;
                default:
                    break;
            }
        }
        //}

        //StartCoroutine(tu());

    }
    public void SetLastTimestamp(float timestamp)
    {
        
        _lastTimestamp = timestamp;
    }
    //IEnumerator tu()
    //{

    //    yield return new WaitForSeconds(600);
    //    Debug.Log("Delete");
    //    Delete();
    //}
    public void InsertInteractions(List<InteractionSaveData> interactions)
    {
        _interactions = interactions;
    }
    public CharacterData GetCharacterData()
    {
        return _data;
    }
    public void Delete()
    {
        Destroy(this.gameObject);
    }
    public List<InteractionSaveData> GetInteractions()
    {
        return _interactions;
    }
    public void StartShadowing(bool state)
    {
        _isReconstructing = state;
    }
}
