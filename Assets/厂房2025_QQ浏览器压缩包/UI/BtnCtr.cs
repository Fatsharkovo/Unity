using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCtr : MonoBehaviour
{
    public AudioSource m_as;
    public Animator chache1;
    public Animator chache2;
    public Animator chache3;
    public Animator chache4;

    public GameObject den1;
    public GameObject den2;

    public Text wenduText;
    public Text siduText;

    public void Kaiden()
    {
        //开灯
        den1.SetActive(true);
        den2.SetActive(true);

    }

    public void Guanden()
    {
        //关灯
        den1.SetActive(false);
        den2.SetActive(false);
    }

    //启动所有车
    public void QiDongChaChe()
    {
        chache1.speed = 1;
        chache2.speed = 1;

        chache3.speed = 1;
        chache4.speed = 1;
    }
    //停止所有车
    public void guanbiChaChe()
    {
        chache1.speed=0;
        chache2.speed = 0;
        chache3.speed = 0;
        chache4.speed = 0;
    }
    //退出exe应用
    public void ExitApp()
    {
        Application.Quit();
    }
    // 复选框开关背景音乐
    public void IsBgmp3(bool b)
    {
 
        if(m_as.isPlaying==false)
        {
            m_as.Play();
        }
        else
        {
            m_as.Stop();

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    public float time1;
    // Update is called once per frame
    void Update()
    {
        time1 += Time.deltaTime;
        if(time1>=1)
        {
            time1 = 0;

            wenduText.text ="温度:"+ Random.Range(25, 37).ToString()+"C";
            siduText.text = "湿度:" + Random.Range(0, 100).ToString()+"C";

        }
    }
}
