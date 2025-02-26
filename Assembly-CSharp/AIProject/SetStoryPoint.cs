using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D1C RID: 3356
	[TaskCategory("")]
	public class SetStoryPoint : AgentAction
	{
		// Token: 0x06006B65 RID: 27493 RVA: 0x002E0498 File Offset: 0x002DE898
		public override TaskStatus OnUpdate()
		{
			StoryPoint storyPoint = null;
			if (Singleton<Map>.IsInstance())
			{
				PointManager pointAgent = Singleton<Map>.Instance.PointAgent;
				if (pointAgent == null)
				{
					return TaskStatus.Failure;
				}
				Dictionary<int, StoryPoint> storyPointTable = pointAgent.StoryPointTable;
				if (storyPointTable.IsNullOrEmpty<int, StoryPoint>())
				{
					return TaskStatus.Failure;
				}
				storyPointTable.TryGetValue(this._pointID, out storyPoint);
			}
			base.Agent.TargetStoryPoint = storyPoint;
			return (!(storyPoint == null)) ? TaskStatus.Success : TaskStatus.Failure;
		}

		// Token: 0x04005A82 RID: 23170
		[SerializeField]
		private int _pointID;
	}
}
