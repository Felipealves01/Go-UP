using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fadeIn;
    [SerializeField] private AudioClip [ ] clips;
    [SerializeField] private GameObject theEndPanel;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        GameManager.instance.BGMusic.StartMusic();
        if ( PlayerPrefs.GetInt("faseAtual") == 5 ) {
            theEndPanel.SetActive(true);
        }
    }

    public void PlayGame() {
        StartCoroutine(nameof(OnPlay));
        audioSource.PlayOneShot(clips [ 0 ]);
    }

    public void ExitGame() {
        Invoke(nameof(OnExit), 2.0f);
        audioSource.PlayOneShot(clips [ 1 ]);
    }

    void OnExit() {
        Application.Quit();
    }

    IEnumerator OnPlay() {
        float timer = 0;
        player.GetComponent<Animator>().SetBool("Play", true);

        while ( timer < 3.0f ) {
            timer += Time.deltaTime;
            player.transform.position = new Vector2(player.transform.position.x + 3 * Time.deltaTime, player.transform.position.y);
            yield return null;
        }

        fadeIn.SetActive(true);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("LevelSelect");
    }

    
}
