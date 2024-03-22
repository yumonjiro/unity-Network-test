using System;
using System.Collections;
using UnityEngine;
using Unity.Col.Gameplay.Manager;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.GameplayObjects.Units;

namespace Unity.Col.Gameplay.Actions
{
    [CreateAssetMenu(menuName = "Col/GameData/Actions/ThrowAction")]
	public class ThrowAction : Action
	{
        private Unit unit;
        public override IEnumerator Perform(ServerActionManager serverActionManager)
        {
            unit = UnitManager.Instance.AllUnit[unitID];
            var projectileInfo = Config.Projectiles;
            var projectile = Instantiate(projectileInfo.ProjectilePrefab);
            projectile.transform.position = unit.transform.position + Vector3.up+ unit.transform.forward;
            projectile.GetComponent<Rigidbody>().velocity = unit.transform.forward * projectileInfo.Speed_m_s;
            yield return null;
        }
    }

}