using System;
using UnityEngine;
using Unity.Col.Gameplay.Manager;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.GameplayObjects.Units;
using Unity.Col.Gameplay.Actions;
using static Unity.Collections.AllocatorManager;
using UnityEngine.InputSystem;

namespace Unity.Col.Gameplay.UI
{
    public class ThrowDirectionSelection : UIState
    {

        public override string Name { get => "ThrowDirectionSelection"; }

        PlayerInputs playerInputs;
        public CameraManager cameraManager;

        public GameObject directionGuidePrefab;
        public GameObject guideInstance;

        private Vector2 _look;
        public float lookPower = 200;
        public Vector3 center;
        public override void Awake()
        {
            base.Awake();
            playerInputs = new();
            playerInputs.Throw.Look.started += cameraManager.OnLook;
            playerInputs.Throw.Look.performed += cameraManager.OnLook;
            playerInputs.Throw.Look.canceled += cameraManager.OnLook;
            playerInputs.Throw.MousePress.started += cameraManager.OnMousePress;
            playerInputs.Throw.MousePress.canceled += cameraManager.OnMousePress;
            playerInputs.Throw.Look.started += OnLook;
            playerInputs.Throw.Look.performed += OnLook;
            playerInputs.Throw.Look.canceled += OnLook;
        }
        public override void OnShow()
        {
            base.OnShow();
            playerInputs.Enable();
            guideInstance = Instantiate(directionGuidePrefab);
            center = UIManager.Instance.activeUnit.tilePosition.position + Vector3.up + Vector3.up;
            guideInstance.transform.position = center + Vector3.forward;

            cameraManager.AimCamera.transform.position = center;
            cameraManager.AimCamera.LookAt = guideInstance.transform;

            cameraManager.AimCamera.Priority = 10;
            cameraManager.FreeCamera.Priority = 0;


        }
        public override void OnHide()
        {
            base.OnHide();
            playerInputs.Disable();
            Destroy(guideInstance);
            cameraManager.AimCamera.Priority = 0;
            cameraManager.FreeCamera.Priority = 10;
        }
        public override void OnTileSelected(Tile tile)
        {
            // TODO implement this
        }

        public override void OnUnitSelected(Unit unit)
        {
            // TODO implement this
        }
        public void OnSubmit()
        {
            ActionRequestData data = new()
            {
                actionID = UIManager.Instance.activeUnit.unitData.ThrowAction.actionID,
                unitID = UIManager.Instance.activeUnit.unitID,
                
            };
            ActionManager.Instance.ServerActionmanager.ReceiveActionRequestServerRpc(data);
            UIManager.Instance.ShowAndHide("ActionTypeSelection", this);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _look = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            var tr = guideInstance.transform;
            var pos = tr.position;
            var anglex = Quaternion.AngleAxis(_look.x * lookPower * Time.deltaTime, Vector3.up);
            //Vertical camera rotation
            var side1 = tr.position - center;
            var side2 = center - (center + Vector3.up);
            var axisR = Vector3.Cross(side1, side2);

            var angley = Quaternion.AngleAxis(-_look.y * lookPower * Time.deltaTime, axisR);
            pos -= center;
            pos = anglex * pos;
            pos = angley * pos;
            pos += center;
            tr.position = pos;
        }
    }

}