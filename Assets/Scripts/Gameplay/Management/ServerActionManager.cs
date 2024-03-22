using System;
using System.Collections.Generic;
using Unity.Netcode;

using Unity.Col.Gameplay.Actions;
using Action = Unity.Col.Gameplay.Actions.Action;
using System.Collections;
using Unity.Col.Gameplay.GameplayObjects;
namespace Unity.Col.Gameplay.Manager
{
    public class ServerActionManager : NetworkBehaviour
    {
        public ClientActionManager clientActionManager;
        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                return;
            }
        }

        [ServerRpc(RequireOwnership = false)]
        public void ReceiveActionRequestServerRpc(ActionRequestData data)
        {
            ActionRequestData data1 = data;
            clientActionManager.ReceiveQueueActionClientRpc(data);
            clientActionManager.ReceivePlayActionClientRpc();
        }

        

        public void PerformUnitActions()
        {
            clientActionManager.ReceivePlayActionClientRpc();
        }
        [ServerRpc(RequireOwnership = false)]
        public void testMoveCommandServerRpc(TilePosition tilePosition)
        {
           clientActionManager.testMoveCommandClientRpc(tilePosition);
        }
    }
}

