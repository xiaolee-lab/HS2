using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000266 RID: 614
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedTransformList values from the Transforms. Returns Success.")]
	public class SharedTransformsToTransformList : Action
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x0002BCCC File Offset: 0x0002A0CC
		public override void OnAwake()
		{
			this.storedTransformList.Value = new List<Transform>();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002BCE0 File Offset: 0x0002A0E0
		public override TaskStatus OnUpdate()
		{
			if (this.transforms == null || this.transforms.Length == 0)
			{
				return TaskStatus.Failure;
			}
			this.storedTransformList.Value.Clear();
			for (int i = 0; i < this.transforms.Length; i++)
			{
				this.storedTransformList.Value.Add(this.transforms[i].Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002BD4E File Offset: 0x0002A14E
		public override void OnReset()
		{
			this.transforms = null;
			this.storedTransformList = null;
		}

		// Token: 0x040009A8 RID: 2472
		[Tooltip("The Transforms value")]
		public SharedTransform[] transforms;

		// Token: 0x040009A9 RID: 2473
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedTransformList storedTransformList;
	}
}
