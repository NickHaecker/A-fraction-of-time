using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private string _name = null;
    [SerializeField]
    private string _description = null;
    [SerializeField]
    private bool _isSplitRealityOrigin = false;
    [SerializeField]
    private List<Interaction> _records = new List<Interaction>();


    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (count < 15)
        {
            // Ability abilitys = this.gameObject.GetComponents<Ability>();

            Debug.Log(_records);
            count++;
        }
    }
    public string GetName()
    {
        return _name;
    }
    public void Init(CharacterData data)
    {
        _name = data.NAME;
        _description = data.DESCRIPTION;
        _isSplitRealityOrigin = data.IS_SPLIT_REALITY_ORIGIN;

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
    public void DeleteAbilities()
    {
        // this.gameObject.
        foreach (Ability ability in this.gameObject.GetComponents<Ability>())
        {
            Destroy(ability);
        }
    }
    public void InertInteractions(Interaction interaction)
    {
        _records.Add(interaction);
    }
    public void ReconstructRecord()
    {

    }
}
