using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private Health health;

    private void Start()
    {
        health.OnHealthChange += OnHealthChangeHandler;
    }

    private void OnDestroy()
    {
        health.OnHealthChange -= OnHealthChangeHandler;
    }

    private void OnHealthChangeHandler(float _newHp)
    {
        hpSlider.value = _newHp;
    }
}
