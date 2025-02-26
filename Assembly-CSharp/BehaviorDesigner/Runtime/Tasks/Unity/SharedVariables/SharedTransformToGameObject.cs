using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000267 RID: 615
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Gets the GameObject from the Transform component. Returns Success.")]
	public class SharedTransformToGameObject : Action
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x0002BD66 File Offset: 0x0002A166
		public override TaskStatus OnUpdate()
		{
			if (this.sharedTransform.Value == null)
			{
				return TaskStatus.Failure;
			}
			this.sharedGameObject.Value = this.sharedTransform.Value.gameObject;
			return TaskStatus.Success;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002BD9C File Offset: 0x0002A19C
		public override void OnReset()
		{
			this.sharedTransform = null;
			this.sharedGameObject = null;
		}

		// Token: 0x040009AA RID: 2474
		[Tooltip("The Transform component")]
		public SharedTransform sharedTransform;

		// Token: 0x040009AB RID: 2475
		[RequiredField]
		[Tooltip("The GameObject to set")]
		public SharedGameObject sharedGameObject;
	}
}
