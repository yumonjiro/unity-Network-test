using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.GameplayObjects.Units;

namespace Unity.Col.Gameplay.Actions
{
    public struct ActionRequestData : INetworkSerializable
    {
        public UnitID unitID;
        public ActionID actionID;

        // Center position of Action. This is realative value from Unit
        public TilePosition Position;

        // Direction of Action
        public TilePosition Direction;
        // THis is used with gun fire and throw action
        public Vector3 FreeDirection;

        // path for unit move action
        public TilePosition[] Path;

        // 
        public int TargetUnitId;

        [Flags]
        private enum PackFlags
        {
            None = 0,
            HasPosition = 1,
            HasDirection = 1 << 1,
            HasFreeDirection = 1 << 2,
            HasTargetId = 1 << 3,
            HasPath = 1 << 4

        }

        private PackFlags GetPackFlags()
        {
            PackFlags flags = PackFlags.None;
            if (Position.position != Vector3Int.zero) { flags |= PackFlags.HasPosition; }
            if (Direction.position != Vector3Int.zero) { flags |= PackFlags.HasDirection; }
            if (TargetUnitId != -1) { flags |= PackFlags.HasTargetId; }
            if (Path != null) { flags |= PackFlags.HasPath; }
            if (FreeDirection != Vector3.zero) { flags |= PackFlags.HasFreeDirection; }
            return flags;
        }

        public static ActionRequestData Create(Action action) =>
            new()
            {
                actionID = action.actionID,
                unitID = action.unitID,
            };
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            PackFlags flags = PackFlags.None;
            if (!serializer.IsReader)
            {
                flags = GetPackFlags();
            }
            
            serializer.SerializeValue(ref actionID);
            serializer.SerializeValue(ref unitID);
            serializer.SerializeValue(ref flags);
            if ((flags & PackFlags.HasPosition) != 0)
            {
                serializer.SerializeValue(ref Position);
            }
            if ((flags & PackFlags.HasDirection) != 0)
            {
                serializer.SerializeValue(ref Position);
            }
            if ((flags & PackFlags.HasTargetId) != 0)
            {
                serializer.SerializeValue(ref Position);
            }
            if ((flags & PackFlags.HasPath) != 0)
            {
                serializer.SerializeValue(ref Path);
            }
            if((flags & PackFlags.HasFreeDirection) != 0)
            {
                serializer.SerializeValue(ref FreeDirection);
            }
        }
    }
}