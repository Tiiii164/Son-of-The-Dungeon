using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Player.Movement;
using Player.Combat;
using Player.Interfaces;

namespace Player.Base
{
    public class PlayerController : MonoBehaviour
    {

        private IPlayerMovement _playerMovement;
        private IPlayerCombat _playerCombat;
        private void Awake()
        {
            _playerMovement = GetComponent<IPlayerMovement>();
            _playerCombat = GetComponent<IPlayerCombat>();
        }
        private void Update()
        {
            _playerCombat.HandleAttackInput();
            _playerCombat.WeaponFollowPointer();
            _playerMovement.Moving();
            _playerMovement.Dash();
        }
        
    }
}

