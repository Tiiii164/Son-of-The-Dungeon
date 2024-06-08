using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Interfaces
{
    public interface IPlayerCombat
    {
        void WeaponFollowPointer();
        Vector2 GetPointerInput();
        void HandleAttackInput();
    }
}

