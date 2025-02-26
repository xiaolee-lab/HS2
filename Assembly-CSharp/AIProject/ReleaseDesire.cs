using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CE1 RID: 3297
	[TaskCategory("")]
	public class ReleaseDesire : AgentAction
	{
		// Token: 0x06006A7C RID: 27260 RVA: 0x002D5DE8 File Offset: 0x002D41E8
		public override TaskStatus OnUpdate()
		{
			int desireKey = Desire.GetDesireKey(this._desireType);
			base.Agent.SetDesire(desireKey, 0f);
			return TaskStatus.Success;
		}

		// Token: 0x04005A07 RID: 23047
		[SerializeField]
		private Desire.Type _desireType;
	}
}
