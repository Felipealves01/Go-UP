using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    [SerializeField] private BackgroundMusic _backgroundMusic;
    [SerializeField] private Tree tree;

    private bool            isRound = true;

    [SerializeField] private int curLevel = 1;

    private ControllerRound roundController;

    private void Awake() {
        if ( instance == null ) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        curLevel = PlayerPrefs.GetInt("faseAtual");
    }

    public void ResetProgress() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if( tree != null )
            tree.Start();

        Debug.Log("PlayerPrefs apagados para testes!");
    }


    public void WinGame(int index) {
        if ( index > PlayerPrefs.GetInt("faseAtual") ) {
            PlayerPrefs.SetInt("faseAtual", index);
            PlayerPrefs.Save();
        }
    }

    public bool IsRound {
        get => isRound;
        set {
            isRound = value;
        }
    }

    public int Level {
        get => curLevel;
        set {
            curLevel = value;
        }
    }

    public BackgroundMusic BGMusic {
        get => _backgroundMusic;
    }

    public ControllerRound ActualRound {
        get => roundController;
        set {
            roundController = value;
        }
    }
}
