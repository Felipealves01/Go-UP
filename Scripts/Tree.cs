using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    [SerializeField] private GameObject [ ] gifts;

    public void Start() {
        if ( gifts != null ) {
            for ( int i = 0; i < gifts.Length; i++ ) {
                if ( i < PlayerPrefs.GetInt("faseAtual") - 1 ) {
                    gifts [ i ].SetActive(true);
                } else {
                    gifts [ i ].SetActive(false);
                }
            }
        }
    }

}
