using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip[] musics;
    [SerializeField] private float volume = 0.3f;
    [SerializeField] private float fadeDuration = 2f;

    private Coroutine fadeCoroutine;

    private void Awake() {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.volume = 0;

        if ( musics == null || musics.Length == 0 ) {
            Debug.LogWarning("Nenhuma música atribuída à lista 'musics'.");
        }
    }

    public void StartMusic() {
        if ( fadeCoroutine != null )
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeInAndStart());
    }

    public void StopMusic() {
        if ( fadeCoroutine != null )
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOutAndStop());
    }

    private IEnumerator FadeInAndStart() {
        if ( musics != null && musics.Length > 0 ) {
            m_AudioSource.Stop();
            m_AudioSource.volume = 0;
            AudioClip clip = musics[Random.Range(0, musics.Length)];
            m_AudioSource.clip = clip;
            m_AudioSource.Play();

            float elapsedTime = 0f;
            while ( elapsedTime < fadeDuration ) {
                m_AudioSource.volume = Mathf.Lerp(0, volume, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            m_AudioSource.volume = volume;
        }
    }

    private IEnumerator FadeOutAndStop() {
        float startVolume = m_AudioSource.volume;
        float elapsedTime = 0f;
        while ( elapsedTime < fadeDuration ) {
            m_AudioSource.volume = Mathf.Lerp(startVolume, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        m_AudioSource.volume = 0;
        m_AudioSource.Stop();
    }
}
