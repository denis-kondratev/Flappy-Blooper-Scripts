﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FlappyBlooper
{
    public class CountableBar : MonoBehaviour
    {
#pragma warning disable CS0649
        [SerializeField] private CountableItem countableItem;
#pragma warning restore CS0649
        
        private int _value;
        private TextMeshProUGUI _valueText;
        private Image _icon;
        private Animator _iconAnimator;
        
        private void Awake()
        {
            _valueText = transform.Find("ValueText").GetComponent<TextMeshProUGUI>();
            _icon = transform.Find("Icon").GetComponent<Image>();
            _iconAnimator = transform.Find("Icon").GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _value = countableItem.Stock;
            _valueText.text = _value.ToString();
            _icon.sprite = countableItem.Icon;
            
            UpdatePanel();
            countableItem.OnStockChanged += CountableItemStockChanged;
        }

        private void OnDisable()
        {
            countableItem.OnStockChanged += CountableItemStockChanged;
        }

        private void CountableItemStockChanged(object sender, System.EventArgs e)
        {
            UpdatePanel();
        }

        private void UpdatePanel()
        {
            var newValue = countableItem.Stock;
            if (_value == newValue) return;
            
            _value = newValue;
            _valueText.text = _value.ToString();
            
            if (_iconAnimator)
            {
                _iconAnimator.SetTrigger(Triggers.Woop);
            }
        }
    }
}