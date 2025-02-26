using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200027E RID: 638
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Resume playing a paused playable.")]
	public class Resume : Action
	{
		// Token: 0x06000B1C RID: 2844 RVA: 0x0002C744 File Offset: 0x0002AB44
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
			this.playbackStarted = false;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002C790 File Offset: 0x0002AB90
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				return TaskStatus.Failure;
			}
			if (!this.playbackStarted)
			{
				this.playableDirector.Resume();
				this.playbackStarted = true;
				return (!this.stopWhenComplete.Value) ? TaskStatus.Success : TaskStatus.Running;
			}
			if (this.stopWhenComplete.Value && this.playableDirector.state == PlayState.Playing)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002C809 File Offset: 0x0002AC09
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stopWhenComplete = false;
		}

		// Token: 0x040009E8 RID: 2536
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009E9 RID: 2537
		[Tooltip("Should the task be stopped when the timeline has stopped playing?")]
		public SharedBool stopWhenComplete;

		// Token: 0x040009EA RID: 2538
		private PlayableDirector playableDirector;

		// Token: 0x040009EB RID: 2539
		private GameObject prevGameObject;

		// Token: 0x040009EC RID: 2540
		private bool playbackStarted;
	}
}
