using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D77 RID: 3447
	[TaskCategory("")]
	public class RollTheDice : AgentConditional
	{
		// Token: 0x06006C54 RID: 27732 RVA: 0x002E6138 File Offset: 0x002E4538
		public override TaskStatus OnUpdate()
		{
			float num = UnityEngine.Random.Range(0f, 100f);
			if (num <= this._border.Value)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AE9 RID: 23273
		[SerializeField]
		private SharedFloat _border = 100f;
	}
}
