using System;
using System.Collections.Generic;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D61 RID: 3425
	[TaskCategory("")]
	public class IsMatchMaxDesire : AgentConditional
	{
		// Token: 0x06006C27 RID: 27687 RVA: 0x002E5A78 File Offset: 0x002E3E78
		public override TaskStatus OnUpdate()
		{
			float num = 0f;
			Desire.Type type = Desire.WithPlayerDesireTable[0];
			if (this._withPlayer)
			{
				foreach (Desire.Type type2 in Desire.WithPlayerDesireTable)
				{
					int desireKey = Desire.GetDesireKey(type2);
					float num2;
					if (base.Agent.AgentData.DesireTable.TryGetValue(desireKey, out num2))
					{
						if (num < num2)
						{
							num = num2;
							type = type2;
						}
					}
				}
			}
			else
			{
				foreach (KeyValuePair<int, float> keyValuePair in base.Agent.AgentData.DesireTable)
				{
					if (num < keyValuePair.Value)
					{
						num = keyValuePair.Value;
						type = (Desire.Type)keyValuePair.Key;
					}
				}
			}
			if (type == Desire.Type.None)
			{
				return TaskStatus.Failure;
			}
			if (type == this._desire)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AD8 RID: 23256
		[SerializeField]
		private Desire.Type _desire;

		// Token: 0x04005AD9 RID: 23257
		[SerializeField]
		private bool _withPlayer;
	}
}
