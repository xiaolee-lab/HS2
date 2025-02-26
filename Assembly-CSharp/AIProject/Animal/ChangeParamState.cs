using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B4F RID: 2895
	public struct ChangeParamState
	{
		// Token: 0x0600564D RID: 22093 RVA: 0x00258F2D File Offset: 0x0025732D
		public ChangeParamState(ChangeType _changeType, float _minRange, float _maxRange)
		{
			this.changeType = _changeType;
			this.minRange = Mathf.Min(_minRange, _maxRange);
			this.maxRange = Mathf.Max(_minRange, _maxRange);
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x0600564E RID: 22094 RVA: 0x00258F50 File Offset: 0x00257350
		public float RandomValue
		{
			[CompilerGenerated]
			get
			{
				return UnityEngine.Random.Range(this.minRange, this.maxRange);
			}
		}

		// Token: 0x04004FAB RID: 20395
		public ChangeType changeType;

		// Token: 0x04004FAC RID: 20396
		public float minRange;

		// Token: 0x04004FAD RID: 20397
		public float maxRange;
	}
}
