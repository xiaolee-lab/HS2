using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D71 RID: 3441
	[TaskCategory("")]
	public class IsWeatherMatch : Conditional
	{
		// Token: 0x06006C48 RID: 27720 RVA: 0x002E5FD8 File Offset: 0x002E43D8
		public override TaskStatus OnUpdate()
		{
			if (this._weather == Singleton<Map>.Instance.Simulator.Weather)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AE6 RID: 23270
		[SerializeField]
		private Weather _weather;
	}
}
