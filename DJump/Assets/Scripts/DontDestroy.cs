﻿using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        var objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
