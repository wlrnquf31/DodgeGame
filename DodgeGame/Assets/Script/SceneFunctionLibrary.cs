using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFunctionLibrary
{
    public static void LoadSceneSafety(string SceneName, Action<AsyncOperation> Completed = null)
    {
        if(IsSceneLoading(SceneName))
        {
            return;
        }

        if(!SceneManager.GetActiveScene().name.Equals(SceneName))
        {
            if(Completed != null)
            {
                SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive).completed += Completed;
            }
            else
            {
                SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            }
        }
    }
    public static bool IsSceneLoading(string SceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name.Equals(SceneName))
                return true;
        }
        return false;
    }
    public static void ShowOptionMenu()
    {
        LoadSceneSafety("OptionScene", ShowOptionMenu_completed);
    }

    private static void ShowOptionMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("OptionScene")); // Must be Active Scene for instantiate prefabs
    }
}
