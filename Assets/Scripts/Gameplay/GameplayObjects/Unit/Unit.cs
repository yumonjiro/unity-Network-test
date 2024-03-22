using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Col.Gameplay.Manager;

namespace Unity.Col.Gameplay.GameplayObjects.Units
{
    public class Unit : NetworkBehaviour
    {

        public Transform followTransform;
        private UnitID _unitID;
        public UnitID unitID
        {
            get
            {
                if (_unitID == default)
                {
                    _unitID = new() { ID = NetworkObjectId };
                }
                return _unitID;
            }
        }
        public TilePosition tilePosition;

        public UnitData unitData;
        public Animator animator;
        
        public override void OnNetworkSpawn()
        {
               
            if(IsClient)
            {
                UnitManager.Instance.RegisterUnit(this);
                Debug.Log("Unit Spawned at client");

                //TODO very ver yhard coded
                lightObject = new();
                lightObject.lightPower = 3;
                lightObject.tilePosition = this.tilePosition;
                lightObject.UpdateIllumination();
            }
            else
            {
                Debug.Log("Unit Spawned at server");
            }
            Debug.Log($"UnitId is {unitID}");

        }

        [ClientRpc]
        public void UnitPositionChangeClientRpc(TilePosition tilePosition)
        {
            transform.position = tilePosition.position;
            this.tilePosition = tilePosition;
            lightObject.tilePosition = tilePosition;
            lightObject.UpdateIllumination();
        }

        public LightObject lightObject;
    }
}