using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200027A RID: 634
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Is the timeline currently paused?")]
	public class IsPaused : Conditional
	{
		// Token: 0x06000B0C RID: 2828 RVA: 0x0002C4B8 File Offset: 0x0002A8B8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002C4FB File Offset: 0x0002A8FB
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				return TaskStatus.Failure;
			}
			return (this.playableDirector.state != PlayState.Paused) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002C527 File Offset: 0x0002A927
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040009D9 RID: 2521
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009DA RID: 2522
		private PlayableDirector playableDirector;

		// Token: 0x040009DB RID: 2523
		private GameObject prevGameObject;
	}
}
