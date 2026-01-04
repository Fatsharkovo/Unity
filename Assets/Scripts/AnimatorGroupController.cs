using UnityEngine;
using System.Collections.Generic;

public class AnimatorGroupController : MonoBehaviour
{
    [Header("Animator数组")]
    public Animator[] animators;

    [Header("调试模式")]
    public bool debugMode = false;

    void Start()
    {
        // 验证数组是否为空
        if (animators == null || animators.Length == 0)
        {
            Debug.LogWarning("Animator数组为空，请添加Animator组件。");
        }
    }

    /// <summary>
    /// 禁用所有Animator组件
    /// </summary>
    public void DisableAllAnimators()
    {
        if (animators == null) return;

        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                animator.enabled = false;
                if (debugMode)
                {
                    Debug.Log($"已禁用Animator: {animator.gameObject.name}");
                }
            }
        }

        if (debugMode)
        {
            Debug.Log($"已禁用 {animators.Length} 个Animator组件");
        }
    }

    /// <summary>
    /// 启用所有Animator组件
    /// </summary>
    public void EnableAllAnimators()
    {
        if (animators == null) return;

        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                animator.enabled = true;
                if (debugMode)
                {
                    Debug.Log($"已启用Animator: {animator.gameObject.name}");
                }
            }
        }

        if (debugMode)
        {
            Debug.Log($"已启用 {animators.Length} 个Animator组件");
        }
    }

    /// <summary>
    /// 设置特定索引的Animator状态
    /// </summary>
    /// <param name="index">数组索引</param>
    /// <param name="enabled">是否启用</param>
    public void SetAnimatorState(int index, bool enabled)
    {
        if (animators == null || index < 0 || index >= animators.Length)
        {
            Debug.LogError($"索引 {index} 超出范围或数组为空");
            return;
        }

        if (animators[index] != null)
        {
            animators[index].enabled = enabled;
            if (debugMode)
            {
                Debug.Log($"设置Animator [{index}] {animators[index].gameObject.name} 为: {enabled}");
            }
        }
    }

    /// <summary>
    /// 通过名称设置特定Animator的状态
    /// </summary>
    /// <param name="animatorName">Animator所在GameObject的名称</param>
    /// <param name="enabled">是否启用</param>
    public void SetAnimatorStateByName(string animatorName, bool enabled)
    {
        if (animators == null) return;

        foreach (Animator animator in animators)
        {
            if (animator != null && animator.gameObject.name == animatorName)
            {
                animator.enabled = enabled;
                if (debugMode)
                {
                    Debug.Log($"设置Animator {animatorName} 为: {enabled}");
                }
                return;
            }
        }

        Debug.LogWarning($"未找到名为 {animatorName} 的Animator");
    }

    /// <summary>
    /// 获取所有Animator的启用状态
    /// </summary>
    /// <returns>启用状态数组</returns>
    public bool[] GetAllAnimatorStates()
    {
        if (animators == null) return new bool[0];

        bool[] states = new bool[animators.Length];
        for (int i = 0; i < animators.Length; i++)
        {
            states[i] = animators[i] != null && animators[i].enabled;
        }
        return states;
    }

    /// <summary>
    /// 获取启用的Animator数量
    /// </summary>
    /// <returns>启用的数量</returns>
    public int GetEnabledAnimatorCount()
    {
        if (animators == null) return 0;

        int count = 0;
        foreach (Animator animator in animators)
        {
            if (animator != null && animator.enabled)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 添加Animator到数组
    /// </summary>
    /// <param name="newAnimator">要添加的Animator</param>
    public void AddAnimator(Animator newAnimator)
    {
        if (newAnimator == null) return;

        List<Animator> animatorList = new List<Animator>();
        if (animators != null)
        {
            animatorList.AddRange(animators);
        }

        if (!animatorList.Contains(newAnimator))
        {
            animatorList.Add(newAnimator);
            animators = animatorList.ToArray();

            if (debugMode)
            {
                Debug.Log($"已添加Animator: {newAnimator.gameObject.name}");
            }
        }
    }

    /// <summary>
    /// 从数组中移除Animator
    /// </summary>
    /// <param name="animatorToRemove">要移除的Animator</param>
    public void RemoveAnimator(Animator animatorToRemove)
    {
        if (animators == null || animatorToRemove == null) return;

        List<Animator> animatorList = new List<Animator>(animators);
        if (animatorList.Remove(animatorToRemove))
        {
            animators = animatorList.ToArray();

            if (debugMode)
            {
                Debug.Log($"已移除Animator: {animatorToRemove.gameObject.name}");
            }
        }
    }

    /// <summary>
    /// 清空Animator数组
    /// </summary>
    public void ClearAnimators()
    {
        animators = new Animator[0];

        if (debugMode)
        {
            Debug.Log("已清空Animator数组");
        }
    }
}