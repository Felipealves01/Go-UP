using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text title_Txt;
    [SerializeField] private TMP_Text timerTxt;
    [SerializeField] private TMP_Text metersTxt;
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private GameObject otherBtn, homeBtn;

    [SerializeField] private Sprite [] spritesBtn;

    public void StartPanel(bool victory, int score) {
        timerTxt.text   = $"{GameManager.instance.ActualRound.TimeTotalRound}s";
        metersTxt.text  = $"{GameManager.instance.ActualRound.MaxHeight}m";
        homeBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        homeBtn.GetComponent<Button>().onClick.AddListener(GoMenu);

        otherBtn.GetComponent<Button>().onClick.RemoveAllListeners();

        if ( victory ) {
            title_Txt.text = "Missão Completa";
            scoreTxt.text = $"Pontos: {score}";
            title_Txt.color = Color.blue;

            if ( GameManager.instance.Level == 4 ) {
                otherBtn.SetActive(false);
            }

            otherBtn.GetComponent<Image>().sprite = spritesBtn [ 0 ];
            otherBtn.GetComponent<Button>().onClick.AddListener(NextLevel);
        } else {
            title_Txt.text = "Game Over";
            scoreTxt.text = $"Pontos: {0}";
            title_Txt.color = Color.red;

            otherBtn.GetComponent<Image>().sprite = spritesBtn [ 1 ];
            otherBtn.GetComponent<Button>().onClick.AddListener(Restart);
        }
    }

    void GoMenu() {
        GameManager.instance.ActualRound.StartCoroutine(nameof(GameManager.instance.ActualRound.OnMenu));
    }

    void Restart() {
        GameManager.instance.ActualRound.RestartLevel();
    }

    void NextLevel() {
        int faseAtual = PlayerPrefs.GetInt("faseAtual");
        StartCoroutine(GameManager.instance.ActualRound.GoLevel(faseAtual));
    }

}
