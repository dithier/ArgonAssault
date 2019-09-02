﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float delay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadFirstScene", delay);

    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}