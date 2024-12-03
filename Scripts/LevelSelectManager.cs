using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct LevelSelect {
    public GameObject content;
    public GameObject padLock;
    public Button     action;
    public TMP_Text levelTxt;
    public bool isLocked;
    public int levelNumber;

    public LevelSelect(int levelNumber, GameObject content, Button action, bool isLocked = true) {
        this.levelNumber = levelNumber;
        this.content = content;
        this.action  = action;
        this.isLocked = isLocked;
        this.levelTxt = content.transform.GetChild(0).GetComponent<TMP_Text>();
        this.padLock = content.transform.GetChild(1).gameObject;
    }

    public void Unlock() {
        isLocked = false;
        if ( content != null ) {
            content.SetActive(true);
        }
        if ( padLock != null ) {
            padLock.SetActive(false);
        }
        Button button = content.GetComponent<Button>();
        if ( button != null ) {
            button.interactable = true;
        }
    }

    public void Lock() {
        isLocked = true;
        if ( content != null ) {
            content.SetActive(true); 
        }
        if ( padLock != null ) {
            padLock.SetActive(true); 
        }
        Button button = content.GetComponent<Button>();
        if ( button != null ) {
            button.interactable = false;
        }
    }


    public bool IsUnlocked() {
        return !isLocked;
    }

    public void UpdateLevelText() {
        if ( levelTxt != null ) {
            levelTxt.text = levelNumber.ToString();
        }
    }
}

public class LevelSelectManager : MonoBehaviour {

    [SerializeField] private GameObject[] levelContents;
    [SerializeField] private GameObject fadeIn;
    private List<LevelSelect> levelSelects = new List<LevelSelect>();
    private AudioSource audioSource;

    void Start() {
        for ( int i = 0; i < levelContents.Length; i++ ) {
            GameObject levelContent = levelContents[i];
            int levelIndex = i + 1;

            LevelSelect levelSelect = new LevelSelect(
            levelNumber: levelIndex,
            content: levelContent,
            action: levelContent.GetComponent<Button>()
        );

            levelSelect.UpdateLevelText();
            levelSelects.Add(levelSelect);

            levelSelect.action.onClick.AddListener(() => StartCoroutine(GoLevel(levelIndex)));
        }

        for ( int i = 0; i < levelContents.Length; i++ ) {
            if ( i < PlayerPrefs.GetInt("faseAtual", 1) ) {
                levelSelects [ i ].Unlock();
            } else {
                levelSelects [ i ].Lock();
            }
        }

        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator GoLevel(int Index) {
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene($"Level_{Index}");
        GameManager.instance.Level = Index;
    }



    public void GoMenu() {
        fadeIn.SetActive( true );
        Invoke(nameof(OnMenu), 1.0f);
    }

    private void OnMenu() {
        SceneManager.LoadScene("Menu");
    }
}

