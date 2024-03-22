using System;
using Unity.Col.Gameplay.Actions;
using Unity.Netcode;

namespace Unity.Col.Gameplay.GameplayObjects.Units
{
    public struct UnitID : IEquatable<UnitID>, INetworkSerializeByMemcpy
    {
        // NetworkObjectId Wrapperclass
        public ulong ID;

        public bool Equals(UnitID other)
        {
            return ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            return obj is UnitID other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(UnitID x, UnitID y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(UnitID x, UnitID y)
        {
            return !(x == y);
        }

        public override string ToString()
        {
            return $"UnitID({ID})";
        }
    }

}