using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Col.Gameplay.Manager;
using Unity.Col.Gameplay.Actions;
using Action = Unity.Col.Gameplay.Actions.Action;
using System.Collections;
using Unity.Col.Gameplay.GameplayObjects;
namespace Unity.Col.Gameplay.Manager
{
    public class ClientActionManager : NetworkBehaviour
    {
        public ServerActionManager serverActionManager;

        // TODO 非同期処理用に実装しなければいけないかも？
        public Queue<Action> actionQueue = new();
        public bool isActionPlaying = false;
        public override void OnNetworkSpawn()
        {
            if (!IsClient)
            {
                return;
            }
            
        }

        [ClientRpc]
        public void ReceiveQueueActionClientRpc(ActionRequestData data)
        {
            ActionRequestData data1 = data;
            actionQueue.Enqueue(ActionFactory.CreateActionFromData(ref data1));
            // TODO Dequeueのタイミングの実装

        }

        [ClientRpc]
        public void ReceivePlayActionClientRpc()
        {
            if(!isActionPlaying)
            {
                StartCoroutine(PerformUnitActions());
            }
            
        }

        public IEnumerator PerformUnitActions()
        {
            isActionPlaying = true;
            while(true)
            {
                if(actionQueue.TryDequeue(out var action))
                {
                    yield return action.Perform(serverActionManager);
                }
                else
                {
                    isActionPlaying = false;
                    break;
                }
            }
        }
        public void OnActionEnd()
        {

        }

        [ClientRpc]
        public void testMoveCommandClientRpc(TilePosition tilePosition)
        {
            UIManager.Instance.activeUnit.transform.position = tilePosition.position;
        }
    }
}

