using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bal : MonoBehaviour
{
    private GameObject wayPoint;
    private Vector3 wayPointPos;
    private float speed = 8.5f;
    [SerializeField] private bool _hasCollided = false;
    [SerializeField] private GameObject _fx;
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if (_hasCollided == true && Input.GetKey(KeyCode.E))
        {
            wayPoint = GameObject.Find("wayPoint");
            FollowPlayer();
            Destroy(_fx);
        }

        if (transform.position.z > 341f
            && transform.position.x > 3
            && transform.position.x < 7)
        {
            _uiManager.ActivateLoading();
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                _hasCollided = true;
            }
        }
    }


    void FollowPlayer()
    {
        wayPointPos = new Vector3((wayPoint.transform.position.x + 1.5f), (wayPoint.transform.position.y + 1),
            (wayPoint.transform.position.z + 1.5f));
        transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
    }
}