using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D1D RID: 3357
	[TaskCategory("")]
	public class SetTutorialProgress : AgentAction
	{
		// Token: 0x06006B67 RID: 27495 RVA: 0x002E0514 File Offset: 0x002DE914
		public override TaskStatus OnUpdate()
		{
			Map.SetTutorialProgressAndUIUpdate(this._number);
			return TaskStatus.Success;
		}

		// Token: 0x04005A83 RID: 23171
		[SerializeField]
		private int _number;
	}
}
