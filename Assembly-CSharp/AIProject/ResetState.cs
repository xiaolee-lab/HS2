using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CE3 RID: 3299
	[TaskCategory("")]
	public class ResetState : AgentAction
	{
		// Token: 0x06006A80 RID: 27264 RVA: 0x002D5E33 File Offset: 0x002D4233
		public override TaskStatus OnUpdate()
		{
			Singleton<Map>.Instance.Player.CameraControl.CrossFade.FadeStart(-1f);
			base.Agent.ActivateTransfer(true);
			return TaskStatus.Success;
		}
	}
}
