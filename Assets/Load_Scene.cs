using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Scene : MonoBehaviour
{

    /// <summary>
    /// 通过下标跳转场景
    /// </summary>
    /// <param name="Index"></param>
    public void  Load_Scene_Index(int Index)
    {
        SceneManager.LoadScene(Index);
    }


    /// <summary>
    /// 通过名字跳转场景
    /// </summary>
    /// <param name="Scene_Name"></param>
    public void Load_Scene_Name(string Scene_Name)
    {
        SceneManager.LoadScene(Scene_Name);
    }


    /// <summary>
    /// 重新开始当前场景
    /// </summary>
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
