using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _healthText;
    [SerializeField] private Image _gameOverImg;
    [SerializeField] private Image _damageImg;
    [SerializeField] private GameObject _coin;
    [SerializeField] private bool _isGameOver;
    [SerializeField] private Image _helpIMG;
    [SerializeField] private Image _dropdownIMG;
    [SerializeField] private Image _loadingIMG;
    [SerializeField] private bool _hasBeenClicked = false;

    void Start()
    {
        _gameOverImg.gameObject.SetActive(false);
        _damageImg.gameObject.SetActive(false);
        _helpIMG.gameObject.SetActive(true);
        _dropdownIMG.gameObject.SetActive(false);
        _hasBeenClicked = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); // Current Game Scene
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_hasBeenClicked == false)
            {
                _helpIMG.gameObject.SetActive(false);
                _dropdownIMG.gameObject.SetActive(true);
                _hasBeenClicked = true;
                Debug.Log("Info hidden");
            }

            else if (_hasBeenClicked == true)
            {
                _helpIMG.gameObject.SetActive(true);
                _dropdownIMG.gameObject.SetActive(false);
                _hasBeenClicked = false;
                Debug.Log("Info shown");
            }
        }
    }

    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;
    }

    public void CollectedCoin()
    {
        _coin.SetActive(true);
    }

    public void RemoveCoin()
    {
        _coin.SetActive(false);
    }

    public void UpdateHealth(int health)
    {
        _healthText.text = "Health: " + health;
        StartCoroutine(DamageOverlay());
    }

    public void ActivateGameOver()
    {
        _gameOverImg.gameObject.SetActive(true);
        _isGameOver = true;
    }

    public void ActivateLoading()
    {
        StartCoroutine(LoadingCoroutine());
    }

    IEnumerator LoadingCoroutine()
    {
        _loadingIMG.gameObject.SetActive(true);
        AudioSource[] audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource aud in audios)
            aud.volume = 0;
        yield return new WaitForSeconds(6f);
        yield break;
    }

    IEnumerator DamageOverlay()
    {
        _damageImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _damageImg.gameObject.SetActive(false);
        yield break;
    }
}