using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    static ScoreKeeper instance;
    private void Awake()
    {
        ManageSingleton();
    }
 
    void ManageSingleton()
    { 
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private int _score = 0;

    public int GetScore()
    {
        return _score;
    }

    public void ModifyScore(int value)
    {
        _score += value;
        Mathf.Clamp(_score, 0, int.MaxValue);
        Debug.Log(_score);

    }

    public void ResetScore()
    {
        _score = 0;
    }
 
}
