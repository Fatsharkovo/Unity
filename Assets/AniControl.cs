using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动画控制
/// </summary>
[RequireComponent(typeof(Animator ))]
public class AniControl : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    /// <summary>
    /// 设置bool参数为true
    /// </summary>
    /// <param name="parmName"></param>
    public void SetBoolTrue(string parmName)
    {
        anim.SetBool(parmName, true); 
    }
    /// <summary>
    /// 设置bool参数为false
    /// </summary>
    /// <param name="parmName"></param>
    public void SetBoolFalse(string parmName)
    {
        anim.SetBool(parmName, false );

    }

    string  intParam;
    public void SetInterParm(string parm)
    {
        intParam = parm;
    }
    public void SetInterValue(int value)
    {
        if(!string .IsNullOrEmpty(intParam))
        {
            anim.SetInteger(intParam, value);
        }else
        {
            Debug.LogError("请先设置参数");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parm"></param>
    public void SetTrigger(string parm)
    {
        anim.SetTrigger(parm);
    }
    public List<MyEvent> aniEves;
    public void DoEve(int index)
    {
        if(index < aniEves .Count )
        {
            aniEves[index].Invoke();
        }else
        {
            Debug.LogError("越界：" + index+":"+gameObject .name );
        }
    }
}
