using System;
using Unity;
using UnityEngine;
using TMPro;
using Unity.Col.Gameplay.Actions;
using Unity.Col.Gameplay.Manager;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.GameplayObjects.Units;

namespace Unity.Col.Gameplay.UI
{
    public class MoveTargetSelection : UIState
    {
        public override string Name { get => "MoveTargetSelection"; }

        public TextMeshProUGUI unitinfo;

        public override void OnShow()
        {
            base.OnShow();
            unitinfo.text = UIManager.Instance.activeUnit.unitData.InfoString();
        }
        public override void OnTileSelected(Tile tile)
        {
            // TODO implement this

            ActionRequestData data = new()
            {
                actionID = UIManager.Instance.activeUnit.unitData.MoveAction.actionID,
                unitID = UIManager.Instance.activeUnit.unitID,
                Path = new TilePosition[1] { tile.tilePosition },
            };
            ActionManager.Instance.ServerActionmanager.ReceiveActionRequestServerRpc(data);
            UIManager.Instance.ShowAndHide("ActionTypeSelection", this);
        }

        public override void OnUnitSelected(Unit unit)
        {
            // TODO implement this
        }

    }

}