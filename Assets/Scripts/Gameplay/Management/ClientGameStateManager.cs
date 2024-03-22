using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Col.Gameplay.GameplayObjects.Units;

namespace Unity.Col.Gameplay.Manager
{
	public enum Team
	{
		CT,
		T,
	}
	public class ClientGameStateManager : NetworkBehaviour
	{
		public static ClientGameStateManager Instance;

		public PersistentPlayer persistentPlayer;
		public PlayerNumber playerNumber;
		public Team CurrentTeam {
			get
			{
				if(playerNumber == PlayerNumber.Player1)
				{
					return Team.T;
				}
				else if(playerNumber == PlayerNumber.Player2)
				{
					return Team.CT;
				}
				else
				{
					throw new System.Exception("No correspond Team to your player number");
				}
			}
		}
		public List<UnitID> ownedUnits;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (Instance != null)
            {
                throw new System.Exception("Duplicate instance detencted");
            }

            Instance = this;
            if (!IsClient)
            {
                return;
            }
            persistentPlayer = NetworkManager.LocalClient.PlayerObject.GetComponent<PersistentPlayer>();
            playerNumber = persistentPlayer.playerNumber;
            ownedUnits = new();
        }

		[ClientRpc]
		public void HireUnitClientRpc(ulong targetClientID, UnitID unitID)
		{
			if(NetworkManager.LocalClientId == targetClientID)
			{
				ownedUnits.Add(unitID);
				if(ownedUnits.Count == 1)

				{
					UIManager.Instance.activeUnit = UnitManager.Instance.AllUnit[unitID];
				}
			}
		}
	}
	

}