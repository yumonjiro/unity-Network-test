using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Col.Gameplay.GameplayObjects.Units;
using Unity.Col.Gameplay.GameplayObjects;
namespace Unity.Col.Gameplay.Manager
{
    public class UnitManager : NetworkBehaviour
    {

        public static UnitManager Instance;

        public Dictionary<UnitID, Unit> AllUnit = new();
        public List<UnitID> unitIDs = new();

        private void Awake()
        {
            if(Instance != null)
            {
                throw new System.Exception("Multiple UnitManager detected");
            }

            Instance = this;
        }


        // This unit class is for test. not in reeas build
        public GameObject testUnit;
        public void RegisterUnit(Unit unit)
        {
            UnitID unitID = new UnitID { ID = unit.NetworkObjectId };
            if(AllUnit.ContainsKey(unitID))
            {
                throw new System.Exception("The unit is already registered");
            }
            else
            {
                AllUnit.Add(unitID, unit);
                unitIDs.Add(unitID);
            }
        }
        [ServerRpc(RequireOwnership = false)]
        public void SpawnNewUnitServerRpc(TilePosition tilePosition, ServerRpcParams rpcParams = default)
        {
            var clientId = rpcParams.Receive.SenderClientId;
            Debug.Log("Spawning test unit");
            GameObject unit = Instantiate(testUnit, tilePosition.position, Quaternion.identity);
            unit.GetComponent<NetworkObject>().Spawn();
            unit.GetComponent<Unit>().UnitPositionChangeClientRpc(tilePosition);
            ClientGameStateManager.Instance.HireUnitClientRpc(clientId, new UnitID { ID = unit.GetComponent<Unit>().NetworkObjectId });
            Debug.Log("unit Spawned");
        }
    }
}