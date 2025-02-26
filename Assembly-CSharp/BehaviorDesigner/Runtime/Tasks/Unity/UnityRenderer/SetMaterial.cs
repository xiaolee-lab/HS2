using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRenderer
{
	// Token: 0x02000206 RID: 518
	[TaskCategory("Unity/Renderer")]
	[TaskDescription("Sets the material on the Renderer.")]
	public class SetMaterial : Action
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x00028C60 File Offset: 0x00027060
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.renderer = defaultGameObject.GetComponent<Renderer>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00028CA3 File Offset: 0x000270A3
		public override TaskStatus OnUpdate()
		{
			if (this.renderer == null)
			{
				return TaskStatus.Failure;
			}
			this.renderer.material = this.material.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00028CCF File Offset: 0x000270CF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.material = null;
		}

		// Token: 0x04000869 RID: 2153
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400086A RID: 2154
		[Tooltip("The material to set")]
		public SharedMaterial material;

		// Token: 0x0400086B RID: 2155
		private Renderer renderer;

		// Token: 0x0400086C RID: 2156
		private GameObject prevGameObject;
	}
}
