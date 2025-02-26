using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D23 RID: 3363
	[TaskCategory("")]
	public class StartTutorialADV : AgentAction
	{
		// Token: 0x06006B83 RID: 27523 RVA: 0x002E1B2F File Offset: 0x002DFF2F
		public override void OnStart()
		{
			base.OnStart();
		}

		// Token: 0x06006B84 RID: 27524 RVA: 0x002E1B38 File Offset: 0x002DFF38
		public override TaskStatus OnUpdate()
		{
			int storyID = this._storyID;
			if (storyID == 0)
			{
				MapUIContainer.OpenStorySupportUI(Popup.StorySupport.Type.ExamineAround);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06006B85 RID: 27525 RVA: 0x002E1B63 File Offset: 0x002DFF63
		public override void OnEnd()
		{
			base.OnEnd();
		}

		// Token: 0x04005A8D RID: 23181
		[SerializeField]
		private int _storyID;
	}
}
