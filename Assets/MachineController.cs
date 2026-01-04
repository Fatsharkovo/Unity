using UnityEngine;
using UnityEngine.UI;
using System;

public class MachineController : MonoBehaviour
{
    [Header("UI组件")]
    [SerializeField] private Button controlButton;
    [SerializeField] private Text statusText; // 使用普通Text显示状态和时间

    [Header("机器设置")]
    [SerializeField] private int scheduledRunTime = 60; // 在编辑器设置运行时间（秒）

    public MyEvent Start_Event;

    public MyEvent Finish_Event;

    private enum MachineState
    {
        Stopped,
        Running
    }

    private MachineState currentState = MachineState.Stopped;
    private DateTime startTime;
    private DateTime scheduledStopTime;
    private float updateTimer;

    private void Start()
    {
        // 绑定按钮点击事件
        if (controlButton != null)
        {
            controlButton.onClick.AddListener(OnControlButtonClicked);
        }

        UpdateUI();
    }

    private void Update()
    {
        updateTimer += Time.deltaTime;

        // 每0.1秒更新一次UI，避免过于频繁的更新
        if (updateTimer >= 0.1f)
        {
            updateTimer = 0f;
            UpdateTimeDisplay();

            // 检查是否到达停止时间
            if (currentState == MachineState.Running && DateTime.Now >= scheduledStopTime)
            {
                StopMachine();
            }
        }
    }

    private void OnControlButtonClicked()
    {
        switch (currentState)
        {
            case MachineState.Stopped:
                StartMachine();
                break;
            case MachineState.Running:
                StopMachine();
                break;
        }
    }

    private void StartMachine()
    {
        currentState = MachineState.Running;
        startTime = DateTime.Now;
        scheduledStopTime = startTime.AddSeconds(scheduledRunTime);
        Start_Event?.Invoke();
        Debug.Log($"机器启动 - 计划运行时间: {scheduledRunTime}秒");
        UpdateUI();
    }

    private void StopMachine()
    {
        currentState = MachineState.Stopped;
        TimeSpan actualRunTime = DateTime.Now - startTime;
        Finish_Event?.Invoke();
        Debug.Log($"机器停止 - 实际运行时间: {actualRunTime.TotalSeconds:F1}秒");
        UpdateUI();
    }

    private void UpdateTimeDisplay()
    {
        if (statusText == null) return;

        if (currentState == MachineState.Running)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            TimeSpan remaining = scheduledStopTime - DateTime.Now;

            // 显示状态、已运行时间和剩余时间
            statusText.text = $"状态: 运行中\n" +
                             $"已运行: {elapsed:mm\\:ss}\n" +
                             $"剩余: {remaining:mm\\:ss}";

            // 剩余时间少于10秒时显示红色警告
            if (remaining.TotalSeconds <= 10)
            {
                statusText.color = Color.red;
            }
            else
            {
                statusText.color = Color.green;
            }
        }
        else
        {
            statusText.text = $"状态: 已停止\n" +
                             $"设置时间: {scheduledRunTime}秒\n" +
                             "点击按钮启动机器";
            statusText.color = Color.gray;
        }
    }

    private void UpdateUI()
    {
        if (controlButton != null)
        {
            // 更新按钮文本
            Text buttonText = controlButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = currentState == MachineState.Running ? "停止机器" : "启动机器";
            }

            // 更新按钮颜色
            var buttonImage = controlButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = currentState == MachineState.Running ? Color.red : Color.green;
            }
        }

        UpdateTimeDisplay();
    }

    // 公共方法，供其他脚本调用
    public bool IsMachineRunning()
    {
        return currentState == MachineState.Running;
    }

    public float GetRemainingTime()
    {
        if (currentState != MachineState.Running) return 0f;
        return (float)(scheduledStopTime - DateTime.Now).TotalSeconds;
    }

    public void SetRunTime(int seconds)
    {
        if (seconds > 0)
        {
            scheduledRunTime = seconds;
            UpdateUI();
        }
    }

    // 强制停止机器（供其他脚本调用）
    public void ForceStop()
    {
        if (currentState == MachineState.Running)
        {
            StopMachine();
        }
    }

    private void OnDestroy()
    {
        // 清理事件监听
        if (controlButton != null)
        {
            controlButton.onClick.RemoveListener(OnControlButtonClicked);
        }
    }
}