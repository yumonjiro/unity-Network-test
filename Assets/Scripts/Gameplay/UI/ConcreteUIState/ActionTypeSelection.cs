using System;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Col.Gameplay.Manager;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.GameplayObjects.Units;

namespace Unity.Col.Gameplay.UI
{
	public class ActionTypeSelection : UIState
	{
        PlayerInputs playerInputs;
        public CameraManager cameraManager;
        public override string Name { get => "ActionTypeSelection"; }

        public override void Awake()
        {
            base.Awake();
            playerInputs = new();

            
            
            playerInputs.Player.Look.started += cameraManager.OnLook;
            playerInputs.Player.Look.performed += cameraManager.OnLook;
            playerInputs.Player.Look.canceled += cameraManager.OnLook;
            playerInputs.Player.TargetNextUnit.performed += TargetNextUnit;
            playerInputs.Player.TargetPreviousUnit.performed += TargetPreviousUnit;
            playerInputs.Player.MousePress.started += cameraManager.OnMousePress;
            playerInputs.Player.MousePress.canceled += cameraManager.OnMousePress;
        }

        public override void OnShow()
        {
            cameraManager.ChangeTarget(UIManager.Instance.activeUnit.followTransform);

            base.OnShow();
            

            playerInputs.Player.Enable();
        }
        public override void OnHide()
        {
            base.OnHide();
            playerInputs.Player.Disable();
        }
        public override void OnTileSelected(Tile tile)
		{
            // TODO implement this
		}

        public override void OnUnitSelected(Unit unit)
        {
            // TODO implement this
        }

        public void TargetNextUnit(InputAction.CallbackContext context)
        {
            ChangeActiveUnit(false);
        }
        public void TargetPreviousUnit(InputAction.CallbackContext context)
        {
            ChangeActiveUnit(true);
        }
        
        public void ChangeActiveUnit(bool reverseOrder)
        {
            //List<UnitID> unitIDs = UnitManager.Instance.unitIDs;

            List<UnitID> unitIDs = ClientGameStateManager.Instance.ownedUnits;

            // Change Unit to play action
            int i = 0;
            foreach(var id in unitIDs)
            {
                if(id == UIManager.Instance.activeUnit.unitID)
                {
                    UnitID nextUnitID;

                    if (reverseOrder)
                    {
                        if (i == 0)
                        {
                            nextUnitID = unitIDs[^1];
                        }
                        else
                        {
                            nextUnitID = unitIDs[i -1];
                        }
                    }
                    else
                    {
                        if (i == unitIDs.Count - 1)
                        {
                            nextUnitID = unitIDs[0];
                        }
                        else
                        {
                            nextUnitID = unitIDs[i + 1];
                        }
                    }
                    
                    UIManager.Instance.activeUnit = UnitManager.Instance.AllUnit[nextUnitID];
                    cameraManager.ChangeTarget(UIManager.Instance.activeUnit.followTransform);
                    break;
                }
                i++;
            }
        }
        public void OnMoveActionSelected()
        {
            UIManager.Instance.ShowAndHide("MoveTargetSelection", this);
        }
        public void OnSpawnUnitSelected()
        {
            UIManager.Instance.ShowAndHide("SpawnTargetSelection", this);
        }
        public void OnThrowSelected()
        {
            UIManager.Instance.ShowAndHide("ThrowDirectionSelection", this);
        }
    }

}