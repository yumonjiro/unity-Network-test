using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using System;
namespace Unity.Col.Gameplay.Actions
{
    [Serializable]
    public class ActionConfig
    {
        [Tooltip("ActionLogic that drives this Actiom. This corresponds to the actual block of code that executes it.")]
        public ActionLogic Logic;

        [Tooltip("Could be damage, could be healing, or other things")]
        public int Amount;

        [Tooltip("The Animation Trigger that gets raised when visualizing this action")]
        public string Anim;

        [Tooltip("If this action spawns projectile, describe it")]
        public ProjectileInfo Projectiles;
    }
}