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

    public Action DeleteRecords;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
    public string GetName()
    {
        return _data.NAME;
    }
    public void Init(CharacterData data)
    {
        _data = data;
        _interactions = new List<Interaction>();
        // _name = data.NAME;
        // _description = data.DESCRIPTION;
        // _isSplitRealityOrigin = data.IS_SPLIT_REALITY_ORIGIN;

    }
    // public void ApplyAbilitys(List<Ability> abilitys)
    // {
    //     foreach (Ability ability in abilitys)
    //     {
    //         // this.gameObject.AddComponent<Ability
    //         // gameObject.AddComponent(typeof(Ability)) as ability;
    //         // gameObject.AddComponent<ability.GetComponent<>()>();
    //         // ability.
    //         gameObject.AddComponent<ability.>
    //     }
    // }
    public void InertInteractions(Interaction interaction)
    {
        _interactions.Add(interaction);
        Debug.Log(_interactions);
    }
    public void ReconstructRecord(List<Interaction> interactions)
    {
        _interactions = interactions;
        // _isReconstructing = true;

        // foreach (Interaction interaction in _records)
        // {
        //     switch (interaction.type)
        //     {
        //         case InteractionType.WALK:
        //             CharacterController cC = this.gameObject.GetComponent<CharacterController>();
        //             Vector3 direction = new Vector3(interaction.interactionPosition.position.x - transform.position.x, interaction.interactionPosition.position.y - transform.position.y, interaction.interactionPosition.transform.position.z - transform.position.z);
        //             cC.Move(direction);
        //             break;
        //         default:
        //             break;
        //     }
        // }

        // _isReconstructing = false;

        // DeleteRecords?.Invoke();
        // Delete();

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
