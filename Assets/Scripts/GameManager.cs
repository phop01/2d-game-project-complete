using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverUI;
    public Text scoreUI;

    int score = 0;
    public AudioSource gameOverSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        scoreUI.text = "Score: " + score.ToString();
    }

    void Update()
    {

    }

    public void Gameover()
    {
        ObstacleSpawner.instance.isGameOver = true;
        StopScroll();
        gameOverUI.SetActive(true);
        gameOverSound.Play();
    }

    public void StopScroll()
    {
        TextureScroll[] scrollObjects = FindObjectsOfType<TextureScroll>();
        foreach (TextureScroll item in scrollObjects)
        {
            item.isScroll = false;
        }
    }

    public void PlayAgin()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // ฟังก์ชันเพิ่มคะแนนปกติ
    public void AddScore()
    {
        score += 1;
        scoreUI.text = "Score: " + score.ToString();
        CheckScore(); // ตรวจสอบคะแนนหลังจากเพิ่มคะแนน
    }

    // ฟังก์ชันเพิ่มคะแนนโบนัส
    public void AddBonusPoints(int points)
    {
        score += points;
        scoreUI.text = "Score: " + score.ToString();
        Debug.Log("Bonus points collected! Total Score: " + score);
        CheckScore(); // ตรวจสอบคะแนนหลังจากเพิ่มคะแนนโบนัส
    }

    // ฟังก์ชันตรวจสอบคะแนน
    private void CheckScore()
    {
        if (score >= 100)
        {
            ChangeScene(); // เปลี่ยนฉากเมื่อคะแนนถึง 100
        }
    }

    // ฟังก์ชันเปลี่ยนฉาก
    private void ChangeScene()
    {
        // เปลี่ยนไปยังฉาก Main1
        SceneManager.LoadScene("Main 1"); // เปลี่ยนชื่อเป็น "Main1"
    }

    public void StartGame()
    {
        // โหลดซีนหลักของเกม
        SceneManager.LoadScene("Main");
    }
}
