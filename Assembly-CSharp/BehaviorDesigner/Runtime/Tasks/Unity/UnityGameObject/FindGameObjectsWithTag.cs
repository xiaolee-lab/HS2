using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000177 RID: 375
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Finds a GameObject by tag. Returns Success.")]
	public class FindGameObjectsWithTag : Action
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x00024610 File Offset: 0x00022A10
		public override TaskStatus OnUpdate()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(this.tag.Value);
			for (int i = 0; i < array.Length; i++)
			{
				this.storeValue.Value.Add(array[i]);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00024656 File Offset: 0x00022A56
		public override void OnReset()
		{
			this.tag.Value = null;
			this.storeValue.Value = null;
		}

		// Token: 0x0400068B RID: 1675
		[Tooltip("The tag of the GameObject to find")]
		public SharedString tag;

		// Token: 0x0400068C RID: 1676
		[Tooltip("The objects found by name")]
		[RequiredField]
		public SharedGameObjectList storeValue;
	}
}
