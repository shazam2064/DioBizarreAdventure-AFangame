using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour
{
    Transform tr_Player;
    float f_RotSpeed = 10.0f, f_MoveSpeed = 7.0f;
    private Player thePlayer;
    [SerializeField] private int lives = 50;
    private SpawnManager _spawnManager;
    [SerializeField] private GameObject _1Damage;
    public bool isDead = false;


    // Use this for initialization
    void Start()
    {
        tr_Player = GameObject.FindGameObjectWithTag("Player").transform;
        thePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _spawnManager = GameObject.FindGameObjectWithTag("Territory").GetComponent<SpawnManager>();

        if (thePlayer == null)
        {
            Debug.Log("Player is null");
        }

        _1Damage.SetActive(false);

        f_MoveSpeed = Random.Range(7, 10);
        //Debug.Log(f_MoveSpeed);

        transform.position = new Vector3(Random.Range(-170, 190), Random.Range(1, 15), Random.Range(-170, 185));
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayer != null
            && tr_Player != null)
        {
            if (Vector3.Distance(transform.position, tr_Player.position) > Random.Range(1f, 5f))
            {
                if (thePlayer != null
                    && tr_Player != null)
                {
                    if (thePlayer.isSafe == false)
                    {
                        Debug.Log("Enemies are now following the player");
                        transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
                    }
                }
            }
        }

        // Look at Player
        if (tr_Player != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation
                , Quaternion.LookRotation(tr_Player.position - transform.position)
                , f_RotSpeed * Time.deltaTime);
        }
    }

    public void EnemyDamage()
    {
        StartCoroutine(DamageNumber());
        //Debug.Log("Coroutine has been called in EnemyDamage");
        --lives;

        if (lives < 0)
        {
            if (thePlayer != null
                && tr_Player != null)
            {
                _spawnManager.enemyCount--;
                isDead = true;
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator DamageNumber()
    {
        // Debug.Log("Coroutine has been succesfully called");
        _1Damage.SetActive(true);
        Vector3 posToSpawn = new Vector3((transform.position.x - Random.Range(1.38f, 2f)),
            (transform.position.y + 3.08f), (transform.position.z + 0.02f));
        GameObject newDamageToken = Instantiate(_1Damage, posToSpawn, Quaternion.identity);
        newDamageToken.transform.parent = this.gameObject.transform;
        yield return new WaitForSeconds(0.7f);
        Destroy(newDamageToken);
        // Debug.Log("Coroutine has been succesfully finished");
        yield break;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (thePlayer != null
                && tr_Player != null)
            {
                Player player = other.transform.GetComponent<Player>();

                if (player != null)
                {
                    player.PlayerDamage();
                }
            }
        }

        if (other.tag == "Territory")
        {
            Territory territory = other.transform.GetComponent<Territory>();

            if (territory != null)
            {
                transform.position = new Vector3(Random.Range(170, 190), Random.Range(1, 15), Random.Range(-170, 185));
            }
        }
    }
}


/*
using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour
{

    Transform tr_Player;
    [SerializeField] private GameObject _player;
    float f_RotSpeed = 15.0f, f_MoveSpeed = 15.0f;
    private bool _isSafe = false;

    // Use this for initialization
    void Start()
    {

        tr_Player = GameObject.FindGameObjectWithTag("Player").transform;

        _player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (_player.transform.position.x >= -25f && _player.transform.position.z <= 25f
                && _player.transform.position.x <= 12f && _player.transform.position.z >= -5.5f)
        {
            _isSafe = true;
        }

        if (Vector3.Distance(transform.position, tr_Player.position) > 2)
        {
            if (_player.transform.position.x >= -25f && _player.transform.position.z <= 25f
                && _player.transform.position.x <= 12f && _player.transform.position.z >= -5.5f )
            {
                transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
            }
        }

        // Look at Player
        transform.rotation = Quaternion.Slerp(transform.rotation
        , Quaternion.LookRotation(tr_Player.position - transform.position)
        , f_RotSpeed * Time.deltaTime);

        // Move at Player
        //   transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;

    }
}


  using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour
{

    Transform tr_Player;
    float f_RotSpeed = 15.0f, f_MoveSpeed = 15.0f;
    Player thePlayer;


    // Use this for initialization
    void Start()
    {
        tr_Player = GameObject.FindGameObjectWithTag("Player").transform;
        thePlayer = gameObject.GetComponent<Player>();

        if (thePlayer == null)
        {
            Debug.Log("Player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, tr_Player.position) > 3)
        {
            if (thePlayer != null)
            {
                if (thePlayer.isSafe == false)
                {
                    Debug.Log("This piece of code has been reached");
                    transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
                }
            }
        }

        // Look at Player
        transform.rotation = Quaternion.Slerp(transform.rotation
        , Quaternion.LookRotation(tr_Player.position - transform.position)
        , f_RotSpeed * Time.deltaTime);

        // Move at Player
        //   transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;

    }
}


*/