using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image _loadingImg;
    [SerializeField] private Text _startText;
    [SerializeField] private AudioSource _backgroundAudio;

    // Start is called before the first frame update
    void Start()
    {
        _loadingImg.gameObject.SetActive(false);
        _backgroundAudio.time = 8.85f;
        _backgroundAudio.Play();
        StartCoroutine(StartFlickerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                return;
            }
            else
            {
                StartCoroutine(LoadingCoroutine());
            }
        }

        if (_backgroundAudio.time == 268)
        {
            _backgroundAudio.time = 8.85f;
            _backgroundAudio.Play();
        }
    }

    IEnumerator LoadingCoroutine()
    {
        _loadingImg.gameObject.SetActive(true);
        AudioSource[] audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource aud in audios)
            aud.volume = 0;
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(1);
        yield break;
    }

    IEnumerator StartFlickerRoutine()
    {
        while (true)
        {
            _startText.text = "Press Any Key To Start";
            yield return new WaitForSeconds(0.5f);
            _startText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}