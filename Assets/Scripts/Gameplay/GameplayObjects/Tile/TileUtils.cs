using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Col.Gameplay.GameplayObjects;
using UnityEngine;
using Unity.Col.Gameplay.Manager;
using Unity.Col.Gameplay.GameplayObjects.Units;

public static class TileUtils
{
    #region Methods for A* search algorithm
    public static List<TilePosition> Neighbors(TilePosition position)
    {
        // This method takes TilePosition, and returns TilePositions
        // which is touching the TilePosition
        List<Vector3Int> dirPlane = new List<Vector3Int>() {
            new Vector3Int(1,0, 0),new Vector3Int(-1,0, 0),new Vector3Int(0,0, 1),new Vector3Int(0,0, -1)};
        List<TilePosition> neighbors = new List<TilePosition>();

        foreach (Vector3Int dir in dirPlane)
        {
            TilePosition neighborPlace = position + dir;
            // xz座標が等しく、y座標が異なるマスを検索する
            for (int y = 0; y < 3; y++)
            {
                TilePosition p = neighborPlace;
                p.y -= y;
                if (TileManager.Instance.level.ContainsKey(p))
                {
                    neighbors.Add(p);
                }
                p.y += 2 * y;
                if (TileManager.Instance.level.ContainsKey(p))
                {
                    neighbors.Add(p);
                }
            }
        }

        return neighbors;
    }

    public static bool Passable(TilePosition p1, TilePosition p2, Unit unit)
    {
        //TODO　判定の仕方がマジて適当だしごちゃごちゃしてるのでちゃんと書き直さないといけない
        //p1, p2はx,z座標の差の合計が１となっている。y座標は０〜２離れている。
        //まず、真上と真下は除外（真上が引数に来ることは多分ないけど）
        if (p1.x == p2.x && p1.z == p2.z && (int)Math.Abs(p1.y - p2.y) == 1)
        {
            return false;
        }
        //目的地の上にタイルがあったらだめ
        if (TileManager.Instance.level.ContainsKey(p2 + new Vector3Int(0, 1, 0)))
        {
            return false;
        }
        //斜め移動の場合、途中に障害物がないか確認
        if ((p2 - p1).magnitude > 1.1)
        {
            //斜め上
            //自分の上にタイルがあったら辿り着けない
            if ((p2.y - p1.y) > 0)
            {
                int height = (int)(p2.y - p1.y);
                for (int i = 0; i < height; i++)
                {
                    if (TileManager.Instance.level.ContainsKey(p1 + new Vector3Int(0, i + 2, 0)))
                    {
                        return false;
                    }
                }

            }
            //斜め下
            //x,z座標の差はどちらかが1,どちらかが0であることを利用して、目的地のタイルの２つ上から出発地点の高さの一つ上までの高さにタイルがないかを調べる
            //p2.y +2 ~ p1.y+1
            if ((p1.y - p2.y) > 0)
            {
                int height = (int)(p1.y - p2.y);
                for (int i = 0; i < height; i++)
                {
                    if (TileManager.Instance.level.ContainsKey(p2 + new Vector3Int(0, 2 + i, 0)))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
    // return manhattan distance of two position
    public static double Heuristic(TilePosition a, TilePosition b)
    {

        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z);
    }
    public static double Cost(TilePosition fromTile, TilePosition toTile)
    {
        // TODO とりあえずcostは1に設定してあるが、そのうち仕様を決めたらちゃんとロジックを書く
        return 1;
    }
    #endregion
}
