using System;
using AIProject.Player;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D8F RID: 3471
	[TaskCategory("")]
	public class IsPlayerAccessableYobai : AgentConditional
	{
		// Token: 0x06006C84 RID: 27780 RVA: 0x002E65F0 File Offset: 0x002E49F0
		public override TaskStatus OnUpdate()
		{
			if (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.MapShortcutUI != null)
			{
				return TaskStatus.Failure;
			}
			if (Singleton<Map>.IsInstance() && Singleton<Map>.Instance.IsWarpProc)
			{
				return TaskStatus.Failure;
			}
			PlayerActor player = Singleton<Map>.Instance.Player;
			if (player.ProcessingTimeSkip)
			{
				return TaskStatus.Failure;
			}
			bool flag = player.Controller.State is Lie;
			if (flag)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}
