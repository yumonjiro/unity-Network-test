using UnityEngine;
using System.Collections;

namespace Unity.Col.Gameplay.Manager
{
    public class ActionManager : MonoBehaviour
    {
        public static ActionManager Instance;
        public ServerActionManager ServerActionmanager;
        public ClientActionManager clientActionmanager;
        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
        }
    }

}