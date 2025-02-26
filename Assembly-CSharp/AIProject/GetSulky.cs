using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace AIProject
{
	// Token: 0x02000CC7 RID: 3271
	public class GetSulky : AgentAction
	{
		// Token: 0x060069E6 RID: 27110 RVA: 0x002D164F File Offset: 0x002CFA4F
		public override void OnStart()
		{
			base.OnStart();
			base.Agent.Animation.Animator.CrossFadeInFixedTime("sulky", 0.1f, 0, 0.1f, 0f);
		}

		// Token: 0x060069E7 RID: 27111 RVA: 0x002D1681 File Offset: 0x002CFA81
		public override TaskStatus OnUpdate()
		{
			this._elapsedTime += Time.deltaTime;
			if (this._elapsedTime < this._duration)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x040059C7 RID: 22983
		[SerializeField]
		[FormerlySerializedAs("Duration")]
		private float _duration;

		// Token: 0x040059C8 RID: 22984
		private float _elapsedTime;
	}
}
