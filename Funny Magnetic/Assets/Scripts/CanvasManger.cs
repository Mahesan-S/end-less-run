using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CanvasManger : MonoBehaviour
{
    [SerializeField] GameObject TapToStart;
    [SerializeField] GameObject OptionsPanels,retartPanel,resumePanel;

    [SerializeField] GameObject StartGameBtn,ResartBtn,ResumeBtn;
    [SerializeField] GameObject VolumeSlider;

    TextMeshProUGUI Score;
    

    string point;

    bool GameisPause = true;
    void Start(){
        Score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameisPause) {
                Isresume();
            }
            else {
                IsPause();
            }
            
        }
    }
    
    public void StartTheGame() {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().StartRun();
        TapToStart.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Options() {
        TapToStart.SetActive(false);
        EventSystem.current.SetSelectedGameObject(VolumeSlider);
        OptionsPanels.SetActive(true);
    }
    public void BackBtn() {
        TapToStart.SetActive(true);
        EventSystem.current.SetSelectedGameObject(StartGameBtn);
        OptionsPanels.SetActive(false);
        
    }
    public void ResartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateScore(int points = 1) {
        Score.text = $"Score {points}";
    }

    public void ResartPanelOpen() {
        retartPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(ResartBtn);
    }

    public void quit(){
        Application.Quit();
    }

    public void Isresume() {
        resumePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
        GameisPause = false;

    }
    public void IsPause(){
        resumePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(ResumeBtn);
        Time.timeScale = 0f;
        GameisPause = true;
    }
}
