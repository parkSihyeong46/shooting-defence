﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Popup_SelectStage : UI_Popup
{
    public override void Init()
    {
        base.Init();
        gameObject.SetActive(false);
    }

    public void StartStage()
    {
        SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
    }
}