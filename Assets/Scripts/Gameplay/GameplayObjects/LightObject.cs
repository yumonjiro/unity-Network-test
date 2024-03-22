using UnityEngine;
using System.Collections.Generic;
using Unity.Col.Gameplay.Manager;
using System.Linq;
namespace Unity.Col.Gameplay.GameplayObjects
{
	public class LightObject
	{
		public int lightPower;
		public TilePosition tilePosition;
		public List<TilePosition> illuminatingTiles = new();

		public void UpdateIllumination()
		{
            List<TilePosition> newIlluminateTiles = new();

            List<Vector3Int> illuminateTiles = new();
			for(int i = 0; i<lightPower; i++)
			{
				for (int j = 0; j < lightPower; j++)
				{
					if(i+j <= lightPower)
					{
						illuminateTiles.Add(new Vector3Int(i, 0, j));
						illuminateTiles.Add(new Vector3Int(-i, 0, j));
						illuminateTiles.Add(new Vector3Int(i, 0, -j));
						illuminateTiles.Add(new Vector3Int(-i,0, -j));

                    }
				}
			}
			foreach(var pos in illuminateTiles)
			{
				newIlluminateTiles.Add(new TilePosition(tilePosition.position + pos));
			}

			var delta = illuminatingTiles.Except<TilePosition>(newIlluminateTiles);
			foreach(var t in delta)
			{
				if(TileManager.Instance.level.TryGetValue(t, out var tile))
				{
					tile.isIlluminated = false;
				}
			}
			illuminatingTiles = newIlluminateTiles;
            foreach (var t in illuminatingTiles)
            {
                if (TileManager.Instance.level.TryGetValue(t, out var tile))
                {
                    tile.isIlluminated = true;
                }
            }
        }	
	}
}

