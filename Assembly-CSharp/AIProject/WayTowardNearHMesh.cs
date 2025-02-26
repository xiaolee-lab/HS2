using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000D2D RID: 3373
	[TaskCategory("")]
	public class WayTowardNearHMesh : AgentAction
	{
		// Token: 0x06006BAA RID: 27562 RVA: 0x002E2968 File Offset: 0x002E0D68
		public override void OnStart()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(this._tagName);
			List<MeshFilter> list = ListPool<MeshFilter>.Get();
			foreach (GameObject gameObject in array)
			{
				MeshFilter[] componentsInChildren = gameObject.GetComponentsInChildren<MeshFilter>();
				foreach (MeshFilter item in componentsInChildren)
				{
					list.Add(item);
				}
			}
			float num = float.PositiveInfinity;
			Vector3 vector = Vector3.zero;
			Vector3 position = base.Agent.Position;
			foreach (MeshFilter meshFilter in list)
			{
				Vector3 vector2 = meshFilter.NearestVertexTo(position);
				float num2 = Vector3.Distance(position, vector2);
				if (num2 < num)
				{
					num = num2;
					vector = vector2;
				}
			}
			ListPool<MeshFilter>.Release(list);
			NavMeshHit navMeshHit;
			if (NavMesh.SamplePosition(vector, out navMeshHit, 100f, -1))
			{
				vector = navMeshHit.position;
			}
			base.Agent.DestPosition = new Vector3?(vector);
		}

		// Token: 0x06006BAB RID: 27563 RVA: 0x002E2A98 File Offset: 0x002E0E98
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			if (agent.DestPosition == null)
			{
				return TaskStatus.Failure;
			}
			if (agent.DestPosition != null)
			{
				agent.NavMeshAgent.SetDestination(agent.DestPosition.Value);
			}
			float num = Vector3.Distance(agent.DestPosition.Value, agent.Position);
			if (num > Singleton<Manager.Resources>.Instance.LocomotionProfile.ApproachDistanceActionPoint)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005AA3 RID: 23203
		[SerializeField]
		private string _tagName = string.Empty;
	}
}
