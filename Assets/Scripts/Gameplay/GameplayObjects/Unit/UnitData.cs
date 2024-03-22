using System;
using UnityEngine;
using Unity.Col.Gameplay.Actions;

namespace Unity.Col.Gameplay.GameplayObjects.Units
{
    [CreateAssetMenu(menuName = "Col/GameData/UnitClass")]
    public class UnitData : ScriptableObject
    {
        public int BaseHP;

        public MoveAction MoveAction;
        public ThrowAction ThrowAction;

        public Actions.Action[] Actions;

        public int JumpPower;

        public int MovePower;

        public int ThrowPower;

        public GameObject CharacterPrefab;
        public int groundOffset;

        public string InfoString()
        {
            return
                $"JumoPower:{JumpPower}\n" +
                $"MovePower:{MovePower}\n";
        }
    }
}

