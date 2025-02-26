using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200027C RID: 636
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Pauses playback of the currently running playable.")]
	public class Pause : Action
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x0002C5BC File Offset: 0x0002A9BC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002C5FF File Offset: 0x0002A9FF
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				return TaskStatus.Failure;
			}
			this.playableDirector.Pause();
			return TaskStatus.Success;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002C620 File Offset: 0x0002AA20
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040009DF RID: 2527
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009E0 RID: 2528
		private PlayableDirector playableDirector;

		// Token: 0x040009E1 RID: 2529
		private GameObject prevGameObject;
	}
}
