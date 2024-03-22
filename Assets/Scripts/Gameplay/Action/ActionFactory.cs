using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Unity.Col.Gameplay.Actions
{
    public class ActionFactory : MonoBehaviour
    {
        private static Dictionary<ActionID, ObjectPool<Action>> s_ActionPools = new Dictionary<ActionID, ObjectPool<Action>>();

        private static ObjectPool<Action> GetActionPool(ActionID actionID)
        {
            if (!s_ActionPools.TryGetValue(actionID, out var actionPool))
            {
                actionPool = new ObjectPool<Action>(
                    createFunc: () => Object.Instantiate(GameDataSource.Instance.GetActionByID(actionID)),
                    actionOnRelease: action => action.Reset(),
                    actionOnDestroy: Object.Destroy);

                s_ActionPools.Add(actionID, actionPool);
            }

            return actionPool;
        }


        /// <summary>
        /// Factory method that creates Actions from their request data.
        /// </summary>
        /// <param name="data">the data to instantiate this skill from. </param>
        /// <returns>the newly created action. </returns>
        public static Action CreateActionFromData(ref ActionRequestData data)
        {
            var ret = GetActionPool(data.actionID).Get();
            ret.Initialize(ref data);
            return ret;
        }

        public static void ReturnAction(Action action)
        {
            var pool = GetActionPool(action.actionID);
            pool.Release(action);
        }

        public static void PurgePooledActions()
        {
            foreach (var actionPool in s_ActionPools.Values)
            {
                actionPool.Clear();
            }
        }
    }
}