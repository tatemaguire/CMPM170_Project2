using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUpdater : MonoBehaviour
{
    TextMeshProUGUI textMeshPro = null;

    [Tooltip("1 or 2? red or blue?")]
    [SerializeField] private int player = 1;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (player == 1) 
        {
            textMeshPro.text = TileCardController.redPoints.ToString();
        }
        else if (player == 2)
        {
            textMeshPro.text = TileCardController.bluePoints.ToString();
        }
    }
}
