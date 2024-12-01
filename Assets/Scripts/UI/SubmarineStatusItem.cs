using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmarineStatusItem : MonoBehaviour
{
    public TextMeshProUGUI lengthText;
    public TextMeshProUGUI statusText;
    public Image submarineImage;

    public void SetStatus(int length, int destroyed, int total, Sprite sprite)
    {
        lengthText.text = $"Length {length}";
        statusText.text = $"{destroyed} / {total} Destroyed";
        submarineImage.sprite = sprite;
    }
}
