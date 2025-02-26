using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C80 RID: 3200
	[TaskCategory("")]
	public class ApplySituationParameter : AgentAction
	{
		// Token: 0x060068DE RID: 26846 RVA: 0x002CA12B File Offset: 0x002C852B
		public override TaskStatus OnUpdate()
		{
			base.Agent.ApplySituationResultParameter((int)this._situation);
			return TaskStatus.Success;
		}

		// Token: 0x04005973 RID: 22899
		[SerializeField]
		private Situation.Types _situation;
	}
}
