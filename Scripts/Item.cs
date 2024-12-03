using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item_Type {
    Clock
}

public class Item : MonoBehaviour {
    [SerializeField] private Item_Type type;
    [SerializeField] private int value;

    private AudioSource m_AudioSource;

    private void Awake() {
        m_AudioSource = GetComponent<AudioSource>();
        if ( m_AudioSource == null ) {
            Debug.LogWarning("AudioSource não encontrado no GameObject: " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.gameObject.CompareTag("Player") ) {
            ControllerRound roundController = GameManager.instance.ActualRound;
            switch ( type ) {
                case Item_Type.Clock:
                if ( roundController != null ) {
                    roundController.RemainingTime += value;
                } else {
                    Debug.LogError("GameManager.instance não está configurado.");
                }
                break;
            }

            if ( m_AudioSource != null ) {
                m_AudioSource.Play();
                Destroy(gameObject, m_AudioSource.clip.length);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
