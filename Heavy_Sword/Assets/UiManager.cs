using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    GameObject levelTitle;
    public string levelName;

    void Start()
    {
        levelTitle = GameObject.Find("Level_Title");
        levelTitle.GetComponent<TextMeshProUGUI>().text = levelName;

        StartCoroutine(levelTitleLeave());

    }
    IEnumerator levelTitleLeave()
    {
        yield return new WaitForSeconds(5);

        levelTitle.GetComponent<Animator>().SetBool("Dissapear", true);
    }
    
}
