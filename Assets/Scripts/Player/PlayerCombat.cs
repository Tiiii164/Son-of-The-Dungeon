using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        private WeaponParent _weaponParent;
        private Vector2 pointerInput;


        private void Awake()
        {
            _weaponParent = GetComponentInChildren<WeaponParent>();
        }

        public void CombatFunctions()
        {
            WeaponFollowPointer();
            HandleAttackInput();
        }
        private void WeaponFollowPointer()
        {
            pointerInput = GetPointerInput();
            _weaponParent.PointerPosition = pointerInput;
        }
        private Vector2 GetPointerInput()
        {
            Vector3 mousePos = InputManager.PointerPosition;
            mousePos.z = Camera.main.nearClipPlane;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
        private void HandleAttackInput()
        {
            if (InputManager.Attack)
            {
                //Debug.Log("Attacked");
                _weaponParent.Attack();
            }
        }
    }
}

