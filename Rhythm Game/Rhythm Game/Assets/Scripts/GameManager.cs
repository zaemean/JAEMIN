using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static float Score = 0;
    public static float avg = 1;
    public Text ScoreText;
    public GameObject Result;
    public Text KanjiText;
    public Text TextText;
    public Dropdown Difficulty;
    public int Difficultnumber;
    public GameObject GameMain;
    public GameObject StartMenu;
    public GameObject MusicPlayer;
    public GameObject PauseMenu;
    private bool IsPause = false;
    public void Awake()
    {
        if (GameManager.gameManager == null)
        {
            GameManager.gameManager = this;
        }
        else
        {
            if (GameManager.gameManager != this)
            {
                Destroy(GameManager.gameManager.gameObject);
                GameManager.gameManager = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        InvokeRepeating("printScore",0f,1f);
    }

    public static void addQ() {
        Score += 1;
        avg += 1;
    }
    public static void addW()
    {
        Score += 2;
        avg += 1;
    }
    public static void addE()
    {
        Score += 3;
        avg += 1;
    }
    public static void addR()
    {
        Score += 4;
        avg += 1;
    }
    public static void addT()
    {
        Score += 0;
        avg += 1;
    }
    void printScore() {
        string Kanji;
        if (Score / avg >= 3.5) Kanji = "대길(大吉)";
        else if (Score / avg >= 2.5) Kanji = "길(吉)";
        else if (Score / avg >= 1.5) Kanji = "흉(凶)";
        else Kanji = "대흉(大凶)";
        ScoreText.text = (Score / avg).ToString("F2") + "/4.0" + "\n " + Kanji;
    }
    public void showResult() {
        Result.SetActive(true);
        if (Score / avg >= 3.5) {
            KanjiText.text = "대길(大吉)";
            TextText.text = "뭐든지 해내실 수 있습니다";
        }
        else if (Score / avg >= 2.5)
        {
            KanjiText.text = "길(吉)";
            TextText.text = "조금만 더 노력하시면 성공합니다";
        }
        else if (Score / avg >= 1.5)
        {
            KanjiText.text = "흉(凶)";
            TextText.text = "괜찮습니다, 포기하지 마세요";
        }
        else
        {
            KanjiText.text = "대흉(大凶)";
            TextText.text = "좌절하지 마시고 한번 더 해보세요";
        }
    }
    public void OnDifficultyChanged() {
        Debug.Log(Difficulty.value);
        Difficultnumber = Difficulty.value;
    }
    public float ReturnDifficulty() {
        return Difficultnumber;
    }
    public void OnStartClicked() {
        MusicPlayer.SetActive(true);
        GameMain.SetActive(true);
        StartMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPause == false)
            {
                IsPause = true;
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
            }
            else {
                IsPause = false;
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
        }
    }
    public void OnRestartClick() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
}
