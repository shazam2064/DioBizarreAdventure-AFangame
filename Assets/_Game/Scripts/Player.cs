using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    // [SerializeField] private float _speed = 3.5f;
    // [SerializeField] private float _gravity = 9.81f; 
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private GameObject _hitMarkerPrefab;
    [SerializeField] private GameObject _deadPrefab;

    [SerializeField] private AudioSource _weaponAudio;
    [SerializeField] private AudioSource _explosionAudio;
    [SerializeField] private AudioSource _reloadAudio;
    [SerializeField] private AudioSource _gameOverAudio;

    [SerializeField] private int currentAmmo;
    [SerializeField] private int maxAmmo = 300;
    [SerializeField] private bool _reloading = false;

    private UIManager _uiManager;

    public bool hasCoin = false;
    public bool isSafe = false;
    private bool _canShoot = false;

    public GameObject wayPoint;
    private float timer = 0.000000000000001f;

    [SerializeField] private int _playerLives = 100;

    [SerializeField] private bool _suffering = false;

    [SerializeField] private GameObject _weapon;


    // Start is called before the first frame update
    void Start()
    {
        //  _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentAmmo = maxAmmo;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _suffering = false;
        _canShoot = false;

        _weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_canShoot == true)
        {
            ShootNoobs();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            //The position of the waypoint will update to the player's position
            UpdatePosition();
            timer = 0.000000000000001f;
        }
    }

    void ShootNoobs()
    {
        if (Input.GetMouseButton(0)
            && currentAmmo != 0)
        {
            _muzzleFlash.SetActive(true);
            currentAmmo--;
            _uiManager.UpdateAmmo(currentAmmo);

            if (_weaponAudio.isPlaying == false)
            {
                _weaponAudio.Play();
            }

            if (_explosionAudio.isPlaying == false)
            {
                _explosionAudio.Play();
            }

            // Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.75f, -8f));
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                //Debug.Log("MUDA MUDA MUDA " + hitInfo.transform.name);
                GameObject hitMarker =
                    Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                Destroy(hitMarker, 1f);

                Destructable crate = hitInfo.transform.GetComponent<Destructable>();
                if (crate != null)
                {
                    crate.DestroyCrate();
                }

                if (hitInfo.collider.gameObject.tag == "ghost")
                {
                    Enemy_AI enemy = hitInfo.transform.GetComponent<Enemy_AI>();

                    if (enemy != null)
                    {
                        enemy.EnemyDamage();

                        if (enemy.isDead == true)
                        {
                            GameObject deadMarker =
                                Instantiate(_deadPrefab, hitInfo.point,
                                    Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                            Destroy(deadMarker, 10f);
                        }
                    }
                }
            }
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _weaponAudio.Stop();
            _explosionAudio.Stop();

            if (currentAmmo == 0)
            {
                //Debug.Log("Oofie, you ran out of ammo. Press 'R' to reload");
            }
        }

        if (Input.GetKeyDown(KeyCode.R)
            && _reloading == false
            && currentAmmo != maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        _reloading = true;
        _reloadAudio.Play();
        yield return new WaitForSeconds(1.7f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _reloading = false;
    }

    void UpdatePosition()
    {
        //The wayPoint's position will now be the player's current position.
        wayPoint.transform.position = transform.position;
    }

    public void PlayerDamage()
    {
        if (_suffering != true)
        {
            StartCoroutine(TakeLives());
        }

        if (_playerLives < 1)
        {
            if (this.gameObject != null)
            {
                StartCoroutine(GameOver());
            }
        }
    }

    public void EnableWeapons()
    {
        _canShoot = true;
        _weapon.SetActive(true);
    }

    IEnumerator TakeLives()
    {
        _suffering = true;
        --_playerLives;
        _uiManager.UpdateHealth(_playerLives);
        yield return new WaitForSeconds(0.5f);
        _suffering = false;
    }

    public IEnumerator GameOver()
    {
        _canShoot = false;
        AudioSource[] audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource aud in audios)
            aud.volume = 0;
        _uiManager.ActivateGameOver();
        _gameOverAudio.volume = 1;
        _gameOverAudio.Play();
        //yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
        yield break;
    }
}


//This code is outside the file
//  void CalculateMovement()
//  {
//      float horizontalInput = Input.GetAxis("Horizontal");
//      float verticalInput = Input.GetAxis("Vertical");

//      Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
//      Vector3 velocity = direction * _speed;
//      velocity.y -= _gravity;

//      velocity = transform.transform.TransformDirection(velocity);
//      _controller.Move(velocity * Time.deltaTime);
//  }