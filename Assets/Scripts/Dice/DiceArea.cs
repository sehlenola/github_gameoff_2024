using UnityEngine;

public class DiceArea : MonoBehaviour
{
    public Transform diceContainer;
    public GameObject dicePrefab;

    public void AddDice(Dice dice)
    {
        dice.transform.SetParent(diceContainer);
        dice.ResetPosition();
    }

    public void ClearDice()
    {
        foreach (Transform child in diceContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
