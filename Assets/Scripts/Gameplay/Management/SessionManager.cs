using UnityEngine;
using System.Collections.Generic;

namespace Unity.Col.Gameplay.Manager
{
    public enum PlayerNumber
    {
        Player1,
        Player2,
    }
    public class SessionPlayerData
    {
        bool IsConnected { get; set; }
        ulong ClientID { get; set; }

        PlayerNumber playerNumber;
       

    }
    public class SessionManager
    {

        public Dictionary<string, SessionPlayerData> clientData;
        public Dictionary<ulong, string> clientIdToPlayerId;
        SessionManager()
        {
            clientData = new();
            clientIdToPlayerId = new();
        }

        public static SessionManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new();
                }

                return instance;
            }
        }
        static SessionManager instance;
    }
}


