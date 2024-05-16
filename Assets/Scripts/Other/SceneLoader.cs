using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Функция для загрузки заданной сцены по ее имени
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
