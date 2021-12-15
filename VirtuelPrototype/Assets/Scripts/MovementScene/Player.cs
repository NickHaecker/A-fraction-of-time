using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    private List<Interaction> _interactions = new List<Interaction>();
    [SerializeField]
    private bool _isReconstructing = false;

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
        _interactions = new List<Interaction>();


    }

    public void InsertInteractions(Interaction interaction)
    {
        _interactions.Add(interaction);

    }
    public void ReconstructRecord(float timestamp)
    {
        Debug.Log("current stamp " + timestamp);
        Interaction interaction = _interactions.Find(i => i.timestamp.Equals(timestamp));
        Debug.Log(interaction);
        //Debug.Log("start replay");

        //foreach(Interaction interaction in interactions)
        //{
        if(interaction != null)
        {
            switch(interaction.type)
            {
                case InteractionType.WALK:
                    transform.position = interaction.interactionPosition.position;
                    transform.rotation = interaction.interactionPosition.rotation;
                    transform.localScale = interaction.interactionPosition.localScale;
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

    //IEnumerator tu()
    //{

    //    yield return new WaitForSeconds(600);
    //    Debug.Log("Delete");
    //    Delete();
    //}
    public void InsertInteractions(List<Interaction> interactions)
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
    public List<Interaction> GetInteractions()
    {
        return _interactions;
    }
}
