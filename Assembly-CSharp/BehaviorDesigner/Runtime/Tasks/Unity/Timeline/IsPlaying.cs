using System;
using UnityEngine;
using UnityEngine.Playables;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Timeline
{
	// Token: 0x0200027B RID: 635
	[TaskCategory("Unity/Timeline")]
	[TaskDescription("Is the timeline currently playing?")]
	public class IsPlaying : Conditional
	{
		// Token: 0x06000B10 RID: 2832 RVA: 0x0002C538 File Offset: 0x0002A938
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.playableDirector = defaultGameObject.GetComponent<PlayableDirector>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002C57B File Offset: 0x0002A97B
		public override TaskStatus OnUpdate()
		{
			if (this.playableDirector == null)
			{
				return TaskStatus.Failure;
			}
			return (this.playableDirector.state != PlayState.Playing) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002C5A8 File Offset: 0x0002A9A8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040009DC RID: 2524
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009DD RID: 2525
		private PlayableDirector playableDirector;

		// Token: 0x040009DE RID: 2526
		private GameObject prevGameObject;
	}
}
