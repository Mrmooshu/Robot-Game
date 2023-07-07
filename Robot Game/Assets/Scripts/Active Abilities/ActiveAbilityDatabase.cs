using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActiveAbility/ActiveAbilityDatabase", fileName = "ActiveAbilityDatabase")]
public class ActiveAbilityDatabase : ScriptableObject
{
    public List<ActiveAbilityData> ActivesList;

    public List<ActiveAbilityData> GeneralActives;
    public List<ActiveAbilityData> GolemActives;

    public void Initialize()
    {
        ActivesList.Clear();
        ActivesList.AddRange(GeneralActives);
        ActivesList.AddRange(GolemActives);
    }
}