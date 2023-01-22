using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControll : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public string Name;
    public Text characterName;

    private void Start()
    {
        characterName.text = Name;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void ResetNameAndHealth(int health, string name)
    {
        characterName.text = name;
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
