using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quiztohome : MonoBehaviour
{
    public void SneceLoad(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
