using UnityEngine;

public class AbilityButton : MonoBehaviour
{
    public Ability ability;

    public void OnButtonClick()
    {
        AbilityManager.Instance.SelectAbility(ability, 1);
    }
}
