using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Col.Gameplay.GameplayObjects;

namespace Unity.Col.Gameplay.Manager
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager Instance;

        public Dictionary<TilePosition, Tile> level = new();

        public Tile tileSelected;

        void Awake()
        {
            if (Instance != null)
            {
                throw new System.Exception("Multiple TileManager Found!");
            }
            Instance = this;
        }

        private void Start()
        {
            // TODO Tile.Start確認して
            var tiles = GameObject.FindGameObjectsWithTag("Tile");
            foreach(var tile in tiles)
            {
                this.AddTile(tile.GetComponent<Tile>());
            }
        }

        public void AddTile(Tile tile)
        {
            level.Add(tile.tilePosition, tile);
        }
    }
}