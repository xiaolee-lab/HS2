using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D64 RID: 3428
	[TaskCategory("")]
	public class IsPhaseGreater : AgentConditional
	{
		// Token: 0x06006C2D RID: 27693 RVA: 0x002E5C3B File Offset: 0x002E403B
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.ChaControl.fileGameInfo.phase < this._comparisonValue)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x04005ADB RID: 23259
		[SerializeField]
		private int _comparisonValue = 2;
	}
}
