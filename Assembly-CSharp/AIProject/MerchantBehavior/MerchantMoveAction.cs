using System;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000D9F RID: 3487
	public abstract class MerchantMoveAction : MerchantAction
	{
		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06006CD7 RID: 27863 RVA: 0x002E6DFC File Offset: 0x002E51FC
		// (set) Token: 0x06006CD8 RID: 27864 RVA: 0x002E6E09 File Offset: 0x002E5209
		protected ActionPoint CurrentPoint
		{
			get
			{
				return base.Merchant.CurrentPoint;
			}
			set
			{
				base.Merchant.CurrentPoint = value;
			}
		}

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06006CD9 RID: 27865 RVA: 0x002E6E18 File Offset: 0x002E5218
		protected bool IsActiveNavMeshAgent
		{
			[CompilerGenerated]
			get
			{
				return base.Merchant != null && base.Merchant.NavMeshAgent != null && base.Merchant.NavMeshAgent.isActiveAndEnabled && base.Merchant.NavMeshAgent.isOnNavMesh;
			}
		}

		// Token: 0x06006CDA RID: 27866 RVA: 0x002E6E74 File Offset: 0x002E5274
		public override void OnStart()
		{
			if (this.prevTalkable = base.Merchant.Talkable)
			{
				base.Merchant.Talkable = false;
			}
			base.OnStart();
			if (this.CurrentPoint == null)
			{
				return;
			}
			base.Merchant.SetActiveOnEquipedItem(false);
			base.Merchant.ChaControl.setAllLayerWeight(0f);
			base.Merchant.IsActionMoving = true;
			ActionPointInfo actionPointInfo;
			this.CurrentPoint.TryGetAgentActionPointInfo(base.Merchant.EventKey, out actionPointInfo);
			GameObject gameObject = this.CurrentPoint.transform.FindLoop(actionPointInfo.baseNullName);
			Transform t = (!(gameObject == null)) ? gameObject.transform : this.CurrentPoint.transform;
			GameObject gameObject2 = this.CurrentPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
			this.recoveryPoint = ((!(gameObject2 != null)) ? null : gameObject2.transform);
			this.actionID = actionPointInfo.eventID;
			this.poseID = actionPointInfo.poseID;
			PlayState info = Singleton<Manager.Resources>.Instance.Animation.MerchantCommonActionAnimStateTable[this.actionID][this.poseID];
			this.animInfo = base.Merchant.Animation.LoadActionState(this.actionID, this.poseID, info);
			base.Merchant.ActivateNavMeshObstacle(base.Merchant.Position);
			base.Merchant.Animation.StopAllAnimCoroutine();
			base.Merchant.Animation.PlayInAnimation(this.animInfo.inEnableBlend, this.animInfo.inBlendSec, this.animInfo.inFadeOutTime, this.animInfo.layer);
			this.onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				base.Merchant.Animation.PlayOutAnimation(this.animInfo.outEnableBlend, this.animInfo.outBlendSec, this.animInfo.layer);
			});
			if (this.animInfo.hasAction)
			{
				this.onActionPlay.Take(1).Subscribe(delegate(Unit _)
				{
					base.Merchant.Animation.PlayActionAnimation(this.animInfo.layer);
				});
			}
			this.CurrentPoint.SetSlot(base.Merchant);
			base.Merchant.SetStand(t, this.animInfo.inEnableBlend, this.animInfo.inBlendSec, this.animInfo.directionType);
		}

		// Token: 0x06006CDB RID: 27867 RVA: 0x002E70CC File Offset: 0x002E54CC
		public override TaskStatus OnUpdate()
		{
			if (base.Merchant.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			if (this.onEndAction != null)
			{
				this.onEndAction.OnNext(Unit.Default);
			}
			if (base.Merchant.Animation.PlayingOutAnimation)
			{
				return TaskStatus.Running;
			}
			this.Complete();
			return TaskStatus.Success;
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x002E712C File Offset: 0x002E552C
		private void Complete()
		{
			this.OnCompletedStateTask();
			base.Merchant.Animation.ResetDefaultAnimatorController();
			if (this.CurrentPoint != null)
			{
				ActionPoint currentPoint = this.CurrentPoint;
				this.CurrentPoint = null;
				base.Merchant.PrevActionPoint = currentPoint;
				if (this.IsActiveNavMeshAgent)
				{
					base.Merchant.NavMeshAgent.CompleteOffMeshLink();
				}
				Merchant.ActionType currentMode = base.Merchant.CurrentMode;
				if (AIProject.Definitions.Merchant.NormalModeList.Contains(currentMode))
				{
					base.Merchant.SetStand(this.recoveryPoint, this.animInfo.outEnableBlend, this.animInfo.outBlendSec, this.animInfo.directionType);
				}
				currentPoint.ReleaseSlot(base.Merchant);
				base.Merchant.ClearItems();
				base.Merchant.ClearParticles();
			}
			base.Merchant.EventKey = (EventType)0;
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x002E7211 File Offset: 0x002E5611
		protected virtual void OnCompletedStateTask()
		{
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x002E7214 File Offset: 0x002E5614
		public override void OnEnd()
		{
			if (this.CurrentPoint != null)
			{
				this.Complete();
			}
			base.Merchant.SetActiveOnEquipedItem(true);
			if (this.prevTalkable)
			{
				base.Merchant.Talkable = true;
			}
			base.Merchant.IsActionMoving = false;
			base.Merchant.ClearItems();
			base.Merchant.ClearParticles();
			base.OnEnd();
		}

		// Token: 0x04005B06 RID: 23302
		private int actionID;

		// Token: 0x04005B07 RID: 23303
		private int poseID;

		// Token: 0x04005B08 RID: 23304
		protected Subject<Unit> onActionPlay = new Subject<Unit>();

		// Token: 0x04005B09 RID: 23305
		protected Subject<Unit> onEndAction = new Subject<Unit>();

		// Token: 0x04005B0A RID: 23306
		protected ActorAnimInfo animInfo = default(ActorAnimInfo);

		// Token: 0x04005B0B RID: 23307
		protected Transform recoveryPoint;

		// Token: 0x04005B0C RID: 23308
		private bool prevTalkable;
	}
}
