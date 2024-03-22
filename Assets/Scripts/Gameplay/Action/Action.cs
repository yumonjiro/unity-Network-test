using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Col.Gameplay.Manager;
using Unity.Col.Gameplay.GameplayObjects.Units;
namespace Unity.Col.Gameplay.Actions
{

    public abstract class Action : ScriptableObject
    {
        [NonSerialized]
        public ActionID actionID;
        [NonSerialized]
        public UnitID unitID;

        protected ActionRequestData m_Data;

        public float TimeStarted { get; set; }
        public float TimeRunning { get { return (Time.time - TimeStarted); } }

        public ref ActionRequestData Data => ref m_Data;

        public ActionConfig Config;

        public abstract IEnumerator Perform(ServerActionManager serverActionManager);
        public void Initialize(ref ActionRequestData data)
        {
            Data = data;
            actionID = data.actionID;
            unitID = data.unitID;
        }

        public virtual void Reset()
        {
            m_Data = default;
            actionID = default;
            unitID = default;
            TimeStarted = 0;
        }


    }
}

