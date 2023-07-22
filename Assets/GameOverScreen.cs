using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText; // Riferimento al componente TextMeshProUGUI per visualizzare il testo delle ondate raggiunte

    private void Start()
    {
        
        int waveCount = PlayerPrefs.GetInt("WaveCount", 0);
        // Impostiamo il testo delle ondate raggiunte nel componente TextMeshProUGUI
        waveNumberText.text = "Ondate Sopravvissute: " + waveCount;

       
    }
}