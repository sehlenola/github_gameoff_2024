using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Ability Pool")]
public class AbilityPool : ScriptableObject
{
    public List<Ability> abilities = new List<Ability>();

    // Method to get all abilities
    public List<Ability> GetAbilities()
    {
        return abilities;
    }

    // Method to get random abilities as rewards
    public List<Ability> GetRandomAbilities(int count)
    {
        List<Ability> randomAbilities = new List<Ability>();
        List<Ability> poolCopy = new List<Ability>(abilities);

        for (int i = 0; i < count && poolCopy.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, poolCopy.Count);
            Ability randomAbility = poolCopy[randomIndex];
            randomAbilities.Add(randomAbility);
            poolCopy.RemoveAt(randomIndex);
        }

        return randomAbilities;
    }

    public List<Ability> GetRandomAbilities(int count, List<Ability> excludeList = null)
    {
        List<Ability> randomAbilities = new List<Ability>();
        List<Ability> poolCopy = new List<Ability>(abilities);

        // Exclude abilities the player already has
        if (excludeList != null)
        {
            poolCopy.RemoveAll(a => excludeList.Contains(a));
        }

        for (int i = 0; i < count && poolCopy.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, poolCopy.Count);
            Ability randomAbility = poolCopy[randomIndex];
            randomAbilities.Add(randomAbility);
            poolCopy.RemoveAt(randomIndex);
        }

        return randomAbilities;
    }
}
