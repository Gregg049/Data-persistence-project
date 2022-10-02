using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using File = System.IO.File;
using Input = UnityEngine.Input;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject MenuText;
    public GameObject ExitText;
    public Text BestScore;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    public TMP_Text nameText;
    public static int bestPoint;
    public static string namePlayer;
    
      
    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
        BestScore.text = "Best score: " + bestPoint;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
       
        nameText.text ="Name: "+ TMPInput.tpInput.text;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        MenuText.SetActive(true);
        ExitText.SetActive(true);
        if(bestPoint<m_Points)  SaveGame();
    }
    [System.Serializable]
    class SaveDate
    {       
        public int m_Points;
        public string name;        
    }
    public void SaveGame()
    {
        var date = new SaveDate();
        date.m_Points=m_Points;
        date.name=TMPInput.tpInput.text;
        string json=JsonUtility.ToJson(date);
        File.WriteAllText(Application.persistentDataPath+"/savefile.json",json);
    }
    public static void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var date = JsonUtility.FromJson<SaveDate>(json);
                bestPoint = date.m_Points;
            namePlayer = date.name;
        }
        else { bestPoint = 0; namePlayer = "Player"; }
    }
 }
