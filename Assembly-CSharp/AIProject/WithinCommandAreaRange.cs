using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D93 RID: 3475
	[TaskCategory("")]
	public class WithinCommandAreaRange : AgentConditional
	{
		// Token: 0x06006C90 RID: 27792 RVA: 0x002E68C0 File Offset: 0x002E4CC0
		public override TaskStatus OnUpdate()
		{
			PlayerActor player = Singleton<Map>.Instance.Player;
			CommandArea commandArea = player.PlayerController.CommandArea;
			Vector3 offset = Vector3.zero;
			if (commandArea.BaseTransform != null)
			{
				Vector3 eulerAngles = commandArea.BaseTransform.eulerAngles;
				eulerAngles.x = (eulerAngles.z = 0f);
				Quaternion rotation = Quaternion.Euler(eulerAngles);
				offset = rotation * commandArea.Offset;
			}
			if (commandArea.WithinRange(player.NavMeshAgent, base.Agent, offset))
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}
