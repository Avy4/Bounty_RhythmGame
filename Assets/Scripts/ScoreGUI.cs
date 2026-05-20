using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreGUI : MonoBehaviour
{
    const String scoreTextName = "Score";
    const String comboTextName = "Combo"; 
    const String healthSliderName = "HealthBar";
    [SerializeField] int amountOfLives = 5;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI comboText;
    private Image healthSlider;
    private float decreasePct;
    void Start()
    {
        scoreText = GameObject.Find(scoreTextName).GetComponent<TextMeshProUGUI>();
        comboText = GameObject.Find(comboTextName).GetComponent<TextMeshProUGUI>();
        healthSlider = GameObject.Find(healthSliderName).GetComponent<Image>();

        SliderInit();
    }

    void Update()
    {
        scoreText.text = ScoreManager.GetScore().ToString("D10");
        comboText.text = ScoreManager.GetCombo().ToString("0000x");
        CheckMissedBeat();
    }

    void SliderInit()
    {
        healthSlider.fillMethod = Image.FillMethod.Horizontal;
        healthSlider.fillAmount = 1;
        decreasePct = 1f / amountOfLives;
        Debug.Log(decreasePct);
    }

    void CheckMissedBeat()
    {
        if (ScoreManager.GetMissedLastBeat())
       {   
            Debug.Log("subtraced health");
            ScoreManager.ResetMissedLastBeat();
            healthSlider.fillAmount -= decreasePct;
            

            if (healthSlider.fillAmount <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
