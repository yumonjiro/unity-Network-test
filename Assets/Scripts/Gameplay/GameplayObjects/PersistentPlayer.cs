using UnityEngine;
using System.Collections;
using Unity.Netcode;

using Unity.Col.Gameplay.Manager;

public class PersistentPlayer : NetworkBehaviour
{
    public PlayerNumber playerNumber;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsHost)
        {
            playerNumber = PlayerNumber.Player1;
        }
        else if(IsClient)
        {
            playerNumber = PlayerNumber.Player2;
        }
        
    }
}

