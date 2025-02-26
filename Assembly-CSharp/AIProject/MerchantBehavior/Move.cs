using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DBC RID: 3516
	[TaskCategory("商人")]
	public class Move : MerchantMoveAction
	{
		// Token: 0x06006D51 RID: 27985 RVA: 0x002E8B54 File Offset: 0x002E6F54
		public override void OnStart()
		{
			base.Merchant.EventKey = EventType.Move;
			base.CurrentPoint = base.Merchant.NavMeshAgent.currentOffMeshLinkData.offMeshLink.GetComponent<ActionPoint>();
			base.OnStart();
			base.Merchant.NavMeshObstacle.carveOnlyStationary = false;
		}

		// Token: 0x06006D52 RID: 27986 RVA: 0x002E8BAB File Offset: 0x002E6FAB
		public override TaskStatus OnUpdate()
		{
			base.Merchant.ObstaclePosition = base.Merchant.Position;
			base.Merchant.ObstacleRotation = base.Merchant.Rotation;
			return base.OnUpdate();
		}

		// Token: 0x06006D53 RID: 27987 RVA: 0x002E8BE0 File Offset: 0x002E6FE0
		public override void OnEnd()
		{
			MerchantActor merchant = base.Merchant;
			ActorAnimation animation = merchant.Animation;
			animation.Targets.Clear();
			animation.Animator.InterruptMatchTarget(false);
			merchant.NavMeshObstacle.carveOnlyStationary = true;
			base.OnEnd();
		}
	}
}
