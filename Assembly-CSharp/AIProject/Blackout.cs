using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C81 RID: 3201
	[TaskCategory("")]
	public class Blackout : AgentAction
	{
		// Token: 0x060068E0 RID: 26848 RVA: 0x002CA194 File Offset: 0x002C8594
		public override void OnStart()
		{
			base.OnStart();
			float value = UnityEngine.Random.value;
			float num = Mathf.Lerp(this._minHour * 60f, this._maxHour * 60f, value);
			this._velocity = this._revivalBorderLine / num;
			base.Agent.DeactivateNavMeshAgent();
			if (!base.Agent.Animation.Animator.HasState(0, this._stateNameHash))
			{
				return;
			}
			base.Agent.Animation.Animator.CrossFadeInFixedTime(this._stateNameHash, this._fadeTime, 0);
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x002CA22C File Offset: 0x002C862C
		public override TaskStatus OnUpdate()
		{
			Dictionary<int, float> statsTable;
			(statsTable = base.Agent.AgentData.StatsTable)[3] = statsTable[3] + this._velocity * Time.deltaTime;
			float num;
			base.Agent.AgentData.StatsTable.TryGetValue(3, out num);
			if (num > this._revivalBorderLine)
			{
				(statsTable = base.Agent.AgentData.StatsTable)[4] = statsTable[4] - 3f;
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x04005974 RID: 22900
		[SerializeField]
		private float _revivalBorderLine = 100f;

		// Token: 0x04005975 RID: 22901
		[SerializeField]
		private float _minHour = 1f;

		// Token: 0x04005976 RID: 22902
		[SerializeField]
		private float _maxHour = 1f;

		// Token: 0x04005977 RID: 22903
		[SerializeField]
		private string _stateName = string.Empty;

		// Token: 0x04005978 RID: 22904
		[SerializeField]
		private float _fadeTime = 0.1f;

		// Token: 0x04005979 RID: 22905
		private int _stateNameHash = -1;

		// Token: 0x0400597A RID: 22906
		private float _velocity;
	}
}
