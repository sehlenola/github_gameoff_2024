using UnityEngine;

public class DiceArea : MonoBehaviour
{
    public Transform diceContainer;
    public GameObject dicePrefab;

    public void AddDice(Dice dice)
    {
        Debug.Log("Trying to add dice");
        dice.transform.SetParent(diceContainer);
        dice.SetPosition(diceContainer);
    }

    public void ClearDice()
    {
        foreach (Transform child in diceContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
