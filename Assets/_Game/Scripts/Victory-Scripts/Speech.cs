using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour
{
    [SerializeField] private Text _congratsText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CongratsSpeechRoutine());
    }

    IEnumerator CongratsSpeechRoutine()
    {
        while (true)
        {
            _congratsText.text = "You have succeded where many have failed";
            yield return new WaitForSeconds(4f);
            _congratsText.text = "";
            yield return new WaitForSeconds(1f);
            _congratsText.text = "You have achieved something few have";
            yield return new WaitForSeconds(4f);
            _congratsText.text = "";
            yield return new WaitForSeconds(1f);
            _congratsText.text = "You have figured out the riddle";
            yield return new WaitForSeconds(4f);
            _congratsText.text = "";
            yield return new WaitForSeconds(1f);
            _congratsText.text = "You have brought the mysterious \n sphere to the portal";
            yield return new WaitForSeconds(4f);
            _congratsText.text = "";
            yield return new WaitForSeconds(1f);
            _congratsText.text = "You have fought foes who are feared by many";
            yield return new WaitForSeconds(4f);
            _congratsText.text = "";
            yield return new WaitForSeconds(1f);
            _congratsText.text = "You have become more powerful";
            yield return new WaitForSeconds(4f);
            _congratsText.text = "";
            yield return new WaitForSeconds(1f);
            _congratsText.text = "You have completed the purpose of this game";
            yield return new WaitForSeconds(4f);
            _congratsText.text = "";
            yield return new WaitForSeconds(1f);
            _congratsText.text = "Still, your journey has just begun...";
            yield return new WaitForSeconds(4f);
            _congratsText.text = "";
            yield return new WaitForSeconds(1f);
            _congratsText.text = "For this game, it's going...";
            yield return new WaitForSeconds(4f);
            yield break;
        }
    }
}