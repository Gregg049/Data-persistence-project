using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text bestPlayers;
    
    private void Start()
    {
        MainManager.LoadGame();
        bestPlayers.text = $"{MainManager.namePlayer}---{MainManager.bestPoint}";
    }
         
    public void StartNew()
    {
        SceneManager.LoadScene(1);
        
    }
    
    
}
