using System.Text;
using TMPro;
using UnityEngine;

public class LetterInventory : MonoBehaviour
{
    public static LetterInventory Instance;

    public TMP_Text collectedText;

    public StringBuilder collected = new StringBuilder();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        RefreshUI();
    }

    public void AddLetter(char c)
    {
    collected.Append(char.ToUpper(c));
    collected.Append(" ");
    Debug.Log("Current letters: " + collected.ToString());
    RefreshUI();
    }
    private void RefreshUI()
    {
        if (collectedText != null)
            collectedText.text = "Letters: " + collected.ToString();
    }
}