using UnityEngine;
using UnityEngine.UI;

public class ToggleObjectVisibility : MonoBehaviour
{
    [Header("目标物体")]
    [SerializeField] private GameObject targetObject; // 要切换显示隐藏的物体

    [Header("按钮设置")]
    [SerializeField] private Button toggleButton;
    [SerializeField] private string showText = "显示物体";
    [SerializeField] private string hideText = "隐藏物体";

    [Header("初始状态")]
    [SerializeField] private bool startVisible = true; // 开始时是否可见

    private bool isVisible;

    private void Start()
    {
        // 初始化状态
        isVisible = startVisible;
        UpdateObjectVisibility();

        // 绑定按钮事件
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(OnToggleButtonClicked);
        }
        else
        {
            // 如果没有指定按钮，尝试获取当前物体上的按钮
            toggleButton = GetComponent<Button>();
            if (toggleButton != null)
            {
                toggleButton.onClick.AddListener(OnToggleButtonClicked);
            }
        }

        UpdateButtonText();
    }

    private void OnToggleButtonClicked()
    {
        // 切换显示状态
        isVisible = !isVisible;
        UpdateObjectVisibility();
        UpdateButtonText();

        Debug.Log($"物体 {(isVisible ? "显示" : "隐藏")}");
    }

    private void UpdateObjectVisibility()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(isVisible);
        }
    }

    private void UpdateButtonText()
    {
        if (toggleButton != null)
        {
            Text buttonText = toggleButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = isVisible ? hideText : showText;
            }
        }
    }

    // 公共方法：显示物体
    public void ShowObject()
    {
        if (!isVisible)
        {
            isVisible = true;
            UpdateObjectVisibility();
            UpdateButtonText();
        }
    }

    // 公共方法：隐藏物体
    public void HideObject()
    {
        if (isVisible)
        {
            isVisible = false;
            UpdateObjectVisibility();
            UpdateButtonText();
        }
    }

    // 公共方法：切换显示状态
    public void ToggleVisibility()
    {
        OnToggleButtonClicked();
    }

    // 公共方法：设置目标物体
    public void SetTargetObject(GameObject newTarget)
    {
        targetObject = newTarget;
        UpdateObjectVisibility();
    }

    // 公共方法：检查物体是否可见
    public bool IsObjectVisible()
    {
        return isVisible;
    }

    private void OnDestroy()
    {
        // 清理事件监听
        if (toggleButton != null)
        {
            toggleButton.onClick.RemoveListener(OnToggleButtonClicked);
        }
    }
}