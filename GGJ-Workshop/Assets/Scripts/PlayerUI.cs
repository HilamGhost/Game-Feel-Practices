using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hilam
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [Header("Health UI")] 
        [SerializeField] private Slider _healthBar;

        private void Start()
        {
            _healthBar.maxValue = _player.MaxHealth;
            ChangeHealthBar(_player.MaxHealth);
        }

        private void OnEnable()
        {
            _player.PlayerHealthChanged.AddListener(ChangeHealthBar);
        }

        private void OnDisable()
        {
            _player.PlayerHealthChanged.RemoveListener(ChangeHealthBar);
        }


        private void ChangeHealthBar(int health)
        {
            _healthBar.value = health;
        }
    }
}
