using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200027F RID: 639
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Stops playback of the current Playable and destroys the corresponding graph.")]
	public class Stop : Action
	{
		// Token: 0x06000B20 RID: 2848 RVA: 0x0002C828 File Offset: 0x0002AC28
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002C86B File Offset: 0x0002AC6B
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				return TaskStatus.Failure;
			}
			this.playableDirector.Stop();
			return TaskStatus.Success;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002C88C File Offset: 0x0002AC8C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040009ED RID: 2541
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009EE RID: 2542
		private PlayableDirector playableDirector;

		// Token: 0x040009EF RID: 2543
		private GameObject prevGameObject;
	}
}
