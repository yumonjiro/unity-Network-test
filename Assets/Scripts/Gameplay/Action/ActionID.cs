using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace Unity.Col.Gameplay.Actions
{
    public struct ActionID : IEquatable<ActionID>, INetworkSerializeByMemcpy
    {
        // This number is given by GameDataSource at runtime.
        public int ID;

        public bool Equals(ActionID other)
        {
            return ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            return obj is ActionID other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public static bool operator ==(ActionID x, ActionID y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ActionID x, ActionID y)
        {
            return !(x == y);
        }

        public override string ToString()
        {
            return $"ActionID({ID})";
        }
    }
}