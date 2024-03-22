using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Col.Gameplay.GameplayObjects.Units;

namespace Unity.Col.Gameplay.Manager
{

    public class ServerGameStateManager : NetworkBehaviour
    {
        public static ServerGameStateManager Instance;

        
        public PlayerNumber playerNumber;

        public Dictionary<string, PlayerNumber> playerIdToNumber;

        private void Awake()
        {

            if (Instance != null)
            {
                throw new System.Exception("Duplicate instance detencted");
            }

            Instance = this;
        }
    }


}