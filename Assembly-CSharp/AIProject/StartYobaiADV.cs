using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;

namespace AIProject
{
	// Token: 0x02000D24 RID: 3364
	[TaskCategory("")]
	public class StartYobaiADV : AgentAction
	{
		// Token: 0x06006B87 RID: 27527 RVA: 0x002E1B74 File Offset: 0x002DFF74
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			IObservable<Unit> source = MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 1f, true);
			source.Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				Singleton<Manager.Map>.Instance.Player.PlayerController.ChangeState("Sex");
				Singleton<Manager.Map>.Instance.Player.StartSneakH(agent);
			});
			agent.ChangeBehavior(Desire.ActionType.Idle);
			Singleton<Manager.Map>.Instance.Player.PlayerController.ChangeState("Idle");
			MapUIContainer.SetActiveCommandList(false);
			Singleton<Manager.Map>.Instance.Player.SetScheduledInteractionState(false);
			Singleton<Manager.Map>.Instance.Player.ReleaseInteraction();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			return TaskStatus.Success;
		}
	}
}
