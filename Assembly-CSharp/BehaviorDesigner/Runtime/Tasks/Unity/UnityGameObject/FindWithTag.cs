using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000178 RID: 376
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Finds a GameObject by tag. Returns Success.")]
	public class FindWithTag : Action
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x00024678 File Offset: 0x00022A78
		public override TaskStatus OnUpdate()
		{
			if (this.random.Value)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag(this.tag.Value);
				this.storeValue.Value = array[UnityEngine.Random.Range(0, array.Length - 1)];
			}
			else
			{
				this.storeValue.Value = GameObject.FindWithTag(this.tag.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000246DF File Offset: 0x00022ADF
		public override void OnReset()
		{
			this.tag.Value = null;
			this.storeValue.Value = null;
		}

		// Token: 0x0400068D RID: 1677
		[Tooltip("The tag of the GameObject to find")]
		public SharedString tag;

		// Token: 0x0400068E RID: 1678
		[Tooltip("Should a random GameObject be found?")]
		public SharedBool random;

		// Token: 0x0400068F RID: 1679
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedGameObject storeValue;
	}
}
