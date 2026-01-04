using UnityEngine;
using UnityEngine.UI;

public class EnvironmentMonitor : MonoBehaviour
{
    [Header("UI References")]
    public Text temperatureText;
    public Text humidityText;

    [Header("Temperature Settings")]
    public float minTemperature = 15f;
    public float maxTemperature = 35f;
    public string temperatureUnit = "°C";

    [Header("Humidity Settings")]
    public float minHumidity = 30f;
    public float maxHumidity = 90f;
    public string humidityUnit = "%";

    [Header("Update Settings")]
    public float updateInterval = 2f; // 更新间隔（秒）

    private float timer = 0f;

    void Start()
    {
        // 初始显示
        UpdateDisplay();

        // 验证UI组件是否设置
        if (temperatureText == null || humidityText == null)
        {
            Debug.LogError("请将温度或湿度Text组件拖拽到对应的字段上！");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            UpdateDisplay();
            timer = 0f;
        }
    }

    void UpdateDisplay()
    {
        // 生成随机温度
        float randomTemperature = Random.Range(minTemperature, maxTemperature);

        // 生成随机湿度
        float randomHumidity = Random.Range(minHumidity, maxHumidity);

        // 更新UI显示
        if (temperatureText != null)
        {
            temperatureText.text = $"温度: {randomTemperature:F1}{temperatureUnit}";
        }

        if (humidityText != null)
        {
            humidityText.text = $"湿度: {randomHumidity:F1}{humidityUnit}";
        }
    }

    // 公共方法：允许其他脚本手动更新显示
    public void ForceUpdate()
    {
        UpdateDisplay();
        timer = 0f;
    }

    // 公共方法：设置温度范围
    public void SetTemperatureRange(float min, float max)
    {
        minTemperature = min;
        maxTemperature = max;
    }

    // 公共方法：设置湿度范围
    public void SetHumidityRange(float min, float max)
    {
        minHumidity = min;
        maxHumidity = max;
    }

    // 公共方法：设置更新间隔
    public void SetUpdateInterval(float interval)
    {
        updateInterval = interval;
    }
}