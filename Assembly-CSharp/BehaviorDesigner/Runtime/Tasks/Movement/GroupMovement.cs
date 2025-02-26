using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000B4 RID: 180
	public abstract class GroupMovement : Action
	{
		// Token: 0x06000429 RID: 1065
		protected abstract bool SetDestination(int index, Vector3 target);

		// Token: 0x0600042A RID: 1066
		protected abstract Vector3 Velocity(int index);
	}
}
