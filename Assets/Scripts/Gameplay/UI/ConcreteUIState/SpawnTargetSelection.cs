using System;
using Unity;
using UnityEngine;
using TMPro;
using Unity.Col.Gameplay.Manager;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.GameplayObjects.Units;

namespace Unity.Col.Gameplay.UI
{
    public class SpawnTargetSelection : UIState
    {
        public override string Name { get => "SpawnTargetSelection"; }

        public override void OnShow()
        {
            base.OnShow();
        }
        public override void OnTileSelected(Tile tile)
        {
            // TODO implement this
            Debug.Log($"Tile {tile.tilePosition.position} Selected");
            //GameObject unit = Instantiate(UnitManager.Instance.testUnit, tile.tilePosition.position, Quaternion.identity);
            //unit.GetComponent<Unity.Netcode.NetworkObject>().Spawn();
            UnitManager.Instance.SpawnNewUnitServerRpc(tile.tilePosition);
            Debug.Log("SpawnNewUnitiServerRpc call ended");
            UIManager.Instance.ShowAndHide("ActionTypeSelection", this);
        }

        public override void OnUnitSelected(Unit unit)
        {
            // TODO implement this
        }

    }

}