using System;
using System.Collections;
using Unity;
using UnityEngine;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.GameplayObjects.Units;
using Unity.Col.Gameplay.Manager;
namespace Unity.Col.Gameplay.Actions
{
	[CreateAssetMenu(menuName = "Col/GameData/Actions/MoveAction")]
	public class MoveAction : Action
	{
		private TilePosition[] path;
		private Unit unit;
		public override IEnumerator Perform(ServerActionManager serverActionManager)
		{

			if(!UnitManager.Instance.AllUnit.TryGetValue(Data.unitID, out unit))
			{
				throw new System.Exception("Designated unit didnt found");
			}
			// TODO 実装
			path = Data.Path;
			if(path.Length == 0)
			{
				throw new System.Exception("No path foubd");
			}
			
			yield return Move();
		}
		public IEnumerator Move()
		{
			unit.animator.SetBool(Config.Anim, true);
			float speed = 2.0f;
			int pathIndex = 0;
			Tile currentTile = TileManager.Instance.level[path[0]];
			Vector3 dir;
			float turnSpeed = 20f;
			Rigidbody rigidbody = unit.GetComponent<Rigidbody>();
			Vector3 desiredForward;
			while(true)
			{
				dir = currentTile.transform.position - unit.transform.position;
				desiredForward = Vector3.RotateTowards(unit.transform.forward, dir, turnSpeed * Time.deltaTime, 0f);
				Quaternion rotation = Quaternion.LookRotation(desiredForward);
				rigidbody.MoveRotation(rotation);
				if(dir.magnitude < 0.1)
				{
					unit.tilePosition = currentTile.tilePosition;
					//TODO
					unit.lightObject.tilePosition = currentTile.tilePosition;
					unit.lightObject.UpdateIllumination();
					if(pathIndex == path.Length - 1)
					{
						Debug.Log($"Unit Reached Destination {currentTile.tilePosition}");
						break;
					}
					else
					{
						pathIndex += 1;
						currentTile = TileManager.Instance.level[path[pathIndex]];
					}
				}
				unit.transform.position += speed * Time.deltaTime * dir.normalized;
				yield return null;
			}
            unit.animator.SetBool(Config.Anim, false);
            yield return null;
		}
	}

}