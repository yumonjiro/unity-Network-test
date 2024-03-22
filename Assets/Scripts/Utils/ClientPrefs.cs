using UnityEngine;
using Unity.Netcode;
namespace Unity.BossRoom.Utils
{
    /// <summary>
    /// Singleton class which saves/loads local-client settings.
    /// (This is just a wrapper around the PlayerPrefs system,
    /// so that all the calls are in the same place.)
    /// </summary>
    public static class ClientPrefs
    {
        const string k_ClientGUIDKey = "client_guid";

        
        /// <summary>
        /// Either loads a Guid string from Unity preferences, or creates one and checkpoints it, then returns it.
        /// </summary>
        /// <returns>The Guid that uniquely identifies this client install, in string form. </returns>
        ///
        //TODO　リリースビルドではこっちを使う
        //public static string GetGuid()
        //{
        //    if (PlayerPrefs.HasKey(k_ClientGUIDKey))
        //    {
        //        return PlayerPrefs.GetString(k_ClientGUIDKey);
        //    }

        //    var guid = System.Guid.NewGuid();
        //    var guidString = guid.ToString();

        //    PlayerPrefs.SetString(k_ClientGUIDKey, guidString);
        //    return guidString;
        //}

        public static string GetGuid()
        {
            if(NetworkManager.Singleton.IsHost)
            {
                return "Host";
            }
            else if(NetworkManager.Singleton.IsClient)
            {
                return "Client";
            }
            else
            {
                throw new System.Exception("this is not host nor client");
            }
        }

        

    }
}
