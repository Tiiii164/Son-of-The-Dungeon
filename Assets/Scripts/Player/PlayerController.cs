using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Player.Movement;
using Player.Combat;
namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerCombat _playerCombat;
        private void Awake()
        {
            _playerCombat = GetComponent<PlayerCombat>();
            _playerMovement = GetComponent<PlayerMovement>();
        }
        private void Update()
        {
            _playerCombat.CombatFunctions();

            _playerMovement.Moving();

            _playerMovement.Dash();
        } 
    }
}

