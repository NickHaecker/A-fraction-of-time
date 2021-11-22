using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField]
    private List<Ability> _ability = new List<Ability>();



    private void Start()
    {
        if (_ability.Count == 0)
        {
            PlayerController playerController = gameObject.GetComponent<PlayerController>();
            _ability = playerController.GetCharacterData().ABILITYS;
        }
    }
}
