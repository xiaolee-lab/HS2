using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200027D RID: 637
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Instatiates a Playable using the provided PlayableAsset and starts playback.")]
	public class Play : Action
	{
		// Token: 0x06000B18 RID: 2840 RVA: 0x0002C634 File Offset: 0x0002AA34
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

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002C680 File Offset: 0x0002AA80
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				return TaskStatus.Failure;
			}
			if (!this.playbackStarted)
			{
				if (this.playableAsset == null)
				{
					this.playableDirector.Play();
				}
				else
				{
					this.playableDirector.Play(this.playableAsset);
				}
				this.playbackStarted = true;
				return (!this.stopWhenComplete.Value) ? TaskStatus.Success : TaskStatus.Running;
			}
			if (this.stopWhenComplete.Value && this.playableDirector.state == PlayState.Playing)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002C720 File Offset: 0x0002AB20
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.playableAsset = null;
			this.stopWhenComplete = false;
		}

		// Token: 0x040009E2 RID: 2530
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009E3 RID: 2531
		[Tooltip("An asset to instantiate a playable from.")]
		public PlayableAsset playableAsset;

		// Token: 0x040009E4 RID: 2532
		[Tooltip("Should the task be stopped when the timeline has stopped playing?")]
		public SharedBool stopWhenComplete;

		// Token: 0x040009E5 RID: 2533
		private PlayableDirector playableDirector;

		// Token: 0x040009E6 RID: 2534
		private GameObject prevGameObject;

		// Token: 0x040009E7 RID: 2535
		private bool playbackStarted;
	}
}
