using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Congrats : MonoBehaviour
{
    [SerializeField] public AudioSource _roundabout;
    [SerializeField] private Text _congratsText;


    // Start is called before the first frame update
    void Start()
    {
        //  Renderer[] _tbcText = GetComponentsInChildren<SpriteRenderer>();
        //  _tbcText.renderer.enabled = false;
        StartCoroutine(CongratsFLickerRoutine());
        _roundabout.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (_roundabout.time < 0.44f)
        {
            // _tbcText.renderer.enabled = true;
        }
    }

    IEnumerator CongratsFLickerRoutine()
    {
        while (true)
        {
            _congratsText.text = "CONGRAGULATIONS";
            yield return new WaitForSeconds(0.5f);
            _congratsText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}