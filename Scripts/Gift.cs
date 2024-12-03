using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour {

    [SerializeField] private ParticleSystem giftVFX;
    [SerializeField] private ParticleSystem dustVFX;

    [SerializeField] private SkillManager skillManager;

    private void OnTriggerEnter2D(Collider2D collision) {
        if ( collision.CompareTag("Player") ) {
            Player p = collision.GetComponent<Player>();
            if ( p != null ) {
                switch ( GameManager.instance.Level ) {
                    case 1:
                        if ( !p.AcquireDoubleJump ) {
                            p.AcquireDoubleJump = true;
                            PlayerPrefs.SetInt("AcquireDoubleJump", 1);
                            skillManager.OpenSkillPanel(SkillType.DoubleJump);
                        }
                        p.PlayEndGame(0);
                        GameManager.instance.WinGame(2);
                        GameManager.instance.ActualRound.EndRound(true);
                    break;

                    case 2:
                        p.PlayEndGame(1);
                        GameManager.instance.WinGame(3);
                        GameManager.instance.ActualRound.EndRound(true);
                    break;

                    case 3:
                        if ( !p.AcquireDash ) {
                            p.AcquireDash = true;
                            PlayerPrefs.SetInt("AcquireDash", 1);
                            skillManager.OpenSkillPanel(SkillType.Dash);
                        }
                        p.PlayEndGame(2);
                        GameManager.instance.WinGame(4);
                        GameManager.instance.ActualRound.EndRound(true);
                    break;

                    case 4:
                        p.PlayEndGame(3);
                        GameManager.instance.WinGame(5);
                        GameManager.instance.ActualRound.EndRound(true);
                    break;

                }
            }

            PlayerPrefs.Save();

            giftVFX?.Play();
            dustVFX?.Play();

            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Rigidbody2D>().isKinematic = true;
            Destroy(gameObject, dustVFX.main.duration);
        }
    }
}
