using System;
using UnityEngine;
using Unity.Netcode;

namespace Unity.Col.Gameplay.GameplayObjects
{
    public struct TilePosition : IEquatable<TilePosition>, INetworkSerializeByMemcpy
    {
        // This class is wrapper class of Vector3 that represents local position of tile.
        // local positon is defined by Vecctor3Int

        public Vector3Int position;

        public int x
        {
            get => position.x;
            set
            {
                position.x = value;
            }
        }
        public int y
        {
            get => position.y;
            set
            {
                position.y = value;
            }
        }
        public int z
        {
            get => position.z;
            set
            {
                position.z = value;
            }
        }
        public TilePosition(Vector3 position)
        {
            this.position = ToTilePosition(position);
        }
        public TilePosition(int x, int y, int z)
        {
            this.position = new Vector3Int(x, y, z);
        }

        public static Vector3Int ToTilePosition(Vector3 position)
        {
            // CAUTION this method doesn't confirm if position parameter is legit.
            // Check in editor if each tile's each value of local position (x, y, z)'s decimal value is small enough for approximation
            return new Vector3Int((int)Math.Round(position.x), (int)Math.Round(position.y), (int)Math.Round((position.z)));
        }


        #region implementations of IEquatable
        public bool Equals(TilePosition tilePosition)
        {
            return position == tilePosition.position;
        }
        public override bool Equals(object obj)
        {
            return obj is TilePosition tilePosition && position == tilePosition.position;
        }

        public static bool operator ==(TilePosition p1, TilePosition p2)
        {
            return p1.position == p2.position;
        }
        public static bool operator !=(TilePosition p1, TilePosition p2)
        {
            return p1.position != p2.position;
        }
        #endregion

        //TODO 演算子は行き当たりばったりで定義しているので、一見するとよくわからない仕様になっているかもしれない。
        public static TilePosition operator +(TilePosition tp, Vector3Int addVector)
        {
            return new TilePosition(tp.position + addVector);
        }
        public static TilePosition operator -(TilePosition tp, Vector3Int addVector)
        {
            return new TilePosition(tp.position - addVector);
        }
        public static Vector3Int operator -(TilePosition tp, TilePosition tp2)
        {
            return tp.position - tp2.position;
        }
        public override int GetHashCode()
        {
            return position.GetHashCode();
        }
    }
}