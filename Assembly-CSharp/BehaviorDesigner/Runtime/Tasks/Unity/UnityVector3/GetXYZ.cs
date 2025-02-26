using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002BA RID: 698
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the X, Y, and Z values of the Vector3.")]
	public class GetXYZ : Action
	{
		// Token: 0x06000BEE RID: 3054 RVA: 0x0002E58C File Offset: 0x0002C98C
		public override TaskStatus OnUpdate()
		{
			this.storeX.Value = this.vector3Variable.Value.x;
			this.storeY.Value = this.vector3Variable.Value.y;
			this.storeZ.Value = this.vector3Variable.Value.z;
			return TaskStatus.Success;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002E5F4 File Offset: 0x0002C9F4
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeX = (this.storeY = (this.storeZ = 0f));
		}

		// Token: 0x04000ABA RID: 2746
		[Tooltip("The Vector3 to get the values of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000ABB RID: 2747
		[Tooltip("The X value")]
		[RequiredField]
		public SharedFloat storeX;

		// Token: 0x04000ABC RID: 2748
		[Tooltip("The Y value")]
		[RequiredField]
		public SharedFloat storeY;

		// Token: 0x04000ABD RID: 2749
		[Tooltip("The Z value")]
		[RequiredField]
		public SharedFloat storeZ;
	}
}
