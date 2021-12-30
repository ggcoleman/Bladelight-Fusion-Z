using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGameOver : MonoBehaviour
{
    ScoreKeeper scoreKeeper;

[SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
     private void Awake() {
         scoreKeeper = FindObjectOfType<ScoreKeeper>();
     }

    
    void Start()
    {
         scoreText.text = "High Score\n" + scoreKeeper.GetScore();
    }
}
