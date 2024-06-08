using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Interfaces;
namespace Player.Combat
{
    public class PlayerCombat : MonoBehaviour, IPlayerCombat
    {
        private WeaponParent _weaponParent;
        private Vector2 pointerInput;


        private void Awake()
        {
            _weaponParent = GetComponentInChildren<WeaponParent>();
        }

        public void WeaponFollowPointer()
        {
            pointerInput = GetPointerInput();
            _weaponParent.PointerPosition = pointerInput;
        }
        public Vector2 GetPointerInput()
        {
            Vector3 mousePos = InputManager.PointerPosition;
            mousePos.z = Camera.main.nearClipPlane;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
        public void HandleAttackInput()
        {
            if (InputManager.Attack)
            {
                _weaponParent.Attack();
            }
        }

        
    }
}

