using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D73 RID: 3443
	[TaskCategory("")]
	public class MatchActorType : AgentConditional
	{
		// Token: 0x06006C4C RID: 27724 RVA: 0x002E6090 File Offset: 0x002E4490
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Partner == null)
			{
				return TaskStatus.Failure;
			}
			if (base.Agent.Partner.GetType().Name != this._typeName)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005AE7 RID: 23271
		[SerializeField]
		private string _typeName = string.Empty;
	}
}
