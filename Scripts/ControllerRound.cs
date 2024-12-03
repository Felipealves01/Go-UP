using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ControllerRound : MonoBehaviour {
    [SerializeField] private EndGameManager endGameManager;
    [SerializeField] private GameObject     fadeIn;

    [Header("Texts")]
    [SerializeField] private TMP_Text timerTxt;
    [SerializeField] private TMP_Text metersTxt;

    [Header("Timer")]
    [SerializeField] private float remainingTime = 30;
    private float fixedRemainingTime;

    [Header("Events")]
    [SerializeField] private UnityEvent OnEndGame;

    private float timeTotalRound;
    private float currentMeters;
    private float maxHeight;
    private float initialYPlayer;

    private bool isRound => GameManager.instance.IsRound;

    private Player _player;
    private PlayerUI _playerUI;

    private void Start() {
        GameManager.instance.ActualRound = this;

        _player = FindObjectOfType<Player>();
        _playerUI = FindObjectOfType<PlayerUI>();

        _player._playerUI = _playerUI;

        initialYPlayer = _player.transform.position.y;

        GameManager.instance.BGMusic.StartMusic();

        fixedRemainingTime = remainingTime;
        GameManager.instance.IsRound = true;

        _playerUI.UpdateIcons();
        _player.OnStart();

        StartCoroutine(nameof(StartRound));
    }

    private void Update() {
        if ( !isRound )
            return;

        currentMeters = _player.transform.position.y - initialYPlayer;

        if ( currentMeters < 0 ) {
            currentMeters = 0;
        }

        if ( currentMeters > maxHeight ) {
            maxHeight = currentMeters;
        }

        metersTxt.text = $"{Mathf.FloorToInt(currentMeters)}m";
    }

    private IEnumerator StartRound() {
        while ( remainingTime > 0 && isRound ) {
            yield return new WaitForSeconds(1);
            remainingTime--;
            timeTotalRound++;

            int timeRound = Mathf.FloorToInt(remainingTime);
            timerTxt.text = $"{timeRound}s";
        }

        if ( isRound )
            EndRound(false);
    }

    private int CalculateScore() {
        const int baseScore = 50;
        const float timeWeight = 2.5f;
        const float heightWeight = 1.5f;

        int timePenalty = (int)(TimeTotalRound * timeWeight);
        int heightBonus = (int)(maxHeight * heightWeight);

        int score = baseScore + heightBonus - timePenalty;

        return Mathf.Max(score, 0);
    }

    public void EndRound(bool isVictory) {
        GameManager.instance.IsRound = false;
        endGameManager.StartPanel(isVictory, CalculateScore());
        OnEndGame?.Invoke();
    }

    public void RestartLevel() {
        Invoke(nameof(OnRestartLevel), 1.0f);
        fadeIn.SetActive(true);
    }

    private void OnRestartLevel() {
        StopAllCoroutines();

        remainingTime = fixedRemainingTime;
        timeTotalRound = 0;
        maxHeight = 0;

        timerTxt.text = $"{Mathf.FloorToInt(remainingTime)}s";
        metersTxt.text = "0m";

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator OnMenu() {
        fadeIn.SetActive(true);
            yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Menu");
    }

    public IEnumerator GoLevel(int Index) {
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene($"Level_{Index}");
        GameManager.instance.Level = Index;
    }

    public int RemainingTime {
        get => Mathf.FloorToInt(remainingTime);
        set {
            remainingTime = value;
        }
    }

    public int TimeTotalRound {
        get => Mathf.FloorToInt(timeTotalRound);
        set {
            timeTotalRound = value;
        }
    }

    public int MaxHeight {
        get => Mathf.FloorToInt(maxHeight);
    }

    public Player Player {
        get => _player;
        set {
            _player = value;
        }
    }

    public PlayerUI PlayerUI {
        get => _playerUI;
        set {
            _playerUI = value;
        }
    }
}
