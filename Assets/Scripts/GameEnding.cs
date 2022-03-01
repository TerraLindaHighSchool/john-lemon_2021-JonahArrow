using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    public CanvasGroup Static;
    public AudioSource staticAudio;
    public GameObject MainCamera;
    public GameObject Camera;
    public GameObject Static1;
    public GameObject Scare2;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    [SerializeField] bool m_IsPlayerStatic;
    float m_Timer;
    float waitcount = 0;
    bool m_HasAudioPlayed;
    private bool isStatic;
    public bool isStaticCaught;
    float count;
    bool hasstart = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
            hasstart = true;
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
            isStatic = true;
        }
        if (isStatic)
        {
            MainCamera.gameObject.SetActive(false);
            Camera.gameObject.SetActive(true);
            Static1.gameObject.SetActive(true);
        }
        if (waitcount == 100)
        {
            Application.Quit();
        }
        if (waitcount == 101)
        {
            SceneManager.LoadScene(0);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
            Scare2.gameObject.SetActive(true);
        }

        if (hasstart)
        {
            waitcount++;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}



