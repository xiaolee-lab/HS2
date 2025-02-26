using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CF4 RID: 3316
	[TaskCategory("")]
	public class WaitFrame : BehaviorDesigner.Runtime.Tasks.Action
	{
		// Token: 0x06006AC8 RID: 27336 RVA: 0x002D9A12 File Offset: 0x002D7E12
		public override void OnStart()
		{
			base.OnStart();
			this._updatedCount = 0;
		}

		// Token: 0x06006AC9 RID: 27337 RVA: 0x002D9A21 File Offset: 0x002D7E21
		public override TaskStatus OnUpdate()
		{
			this._updatedCount++;
			if (this._updatedCount >= this._waitCount)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x04005A3A RID: 23098
		[SerializeField]
		private int _waitCount;

		// Token: 0x04005A3B RID: 23099
		private int _updatedCount;
	}
}
