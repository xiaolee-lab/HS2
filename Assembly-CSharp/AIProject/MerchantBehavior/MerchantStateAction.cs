using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIChara;
using AIProject.Definitions;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA1 RID: 3489
	public abstract class MerchantStateAction : MerchantAction
	{
		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06006CE9 RID: 27881 RVA: 0x002E7328 File Offset: 0x002E5728
		protected Merchant.EventType StateType
		{
			[CompilerGenerated]
			get
			{
				return this.stateType;
			}
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06006CEA RID: 27882 RVA: 0x002E7330 File Offset: 0x002E5730
		protected PlayerActor Player
		{
			[CompilerGenerated]
			get
			{
				return (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player;
			}
		}

		// Token: 0x06006CEB RID: 27883 RVA: 0x002E734C File Offset: 0x002E574C
		public override void OnStart()
		{
			base.OnStart();
			base.Merchant.SetActiveOnEquipedItem(false);
			base.Merchant.ChaControl.setAllLayerWeight(0f);
			this.prevIsTalkable = base.Merchant.Talkable;
			this.isLooking = false;
			this.isNearPlayer = false;
			this.distanceToPlayer = float.MaxValue;
			this.onActionPlay = null;
			this.onEndAction = null;
			this.animInfo = default(ActorAnimInfo);
			this.recoveryPoint = null;
			base.CurrentMerchantPoint = base.TargetInSightMerchantPoint;
			if (base.CurrentMerchantPoint == null)
			{
				return;
			}
			base.Merchant.SetPointIDInfo(base.CurrentMerchantPoint);
			Tuple<MerchantPointInfo, Transform, Transform> eventInfo = base.CurrentMerchantPoint.GetEventInfo(this.stateType);
			Transform item = eventInfo.Item2;
			this.recoveryPoint = eventInfo.Item3;
			if (base.Merchant.Talkable != eventInfo.Item1.isTalkable)
			{
				base.Merchant.Talkable = eventInfo.Item1.isTalkable;
			}
			this.isLooking = eventInfo.Item1.isLooking;
			this.actionID = eventInfo.Item1.eventID;
			this.poseID = eventInfo.Item1.poseID;
			Dictionary<int, Dictionary<int, PlayState>> merchantOnlyActionAnimStateTable = Singleton<Manager.Resources>.Instance.Animation.MerchantOnlyActionAnimStateTable;
			Dictionary<int, PlayState> dictionary;
			PlayState info;
			if (!merchantOnlyActionAnimStateTable.TryGetValue(this.actionID, out dictionary) || !dictionary.TryGetValue(this.poseID, out info))
			{
				return;
			}
			this.animInfo = base.Merchant.Animation.LoadActionState(this.actionID, this.poseID, info);
			base.Merchant.ActivateNavMeshObstacle(item.position);
			base.Merchant.Animation.StopAllAnimCoroutine();
			base.Merchant.Animation.PlayInAnimation(this.animInfo.inEnableBlend, this.animInfo.inBlendSec, this.animInfo.inFadeOutTime, this.animInfo.layer);
			this.onEndAction = new Subject<Unit>();
			this.onEndAction.TakeUntilDestroy(base.Merchant.gameObject).Take(1).Subscribe(delegate(Unit _)
			{
				base.Merchant.Animation.PlayOutAnimation(this.animInfo.outEnableBlend, this.animInfo.outBlendSec, this.animInfo.layer);
			});
			if (this.animInfo.hasAction)
			{
				this.onActionPlay.TakeUntilDestroy(base.Merchant.gameObject).Take(1).Subscribe(delegate(Unit _)
				{
					base.Merchant.Animation.PlayActionAnimation(this.animInfo.layer);
				});
			}
			base.Merchant.CurrentMerchantPoint.SetStand(base.Merchant, item, this.animInfo.inEnableBlend, this.animInfo.inFadeOutTime, this.animInfo.directionType, null);
			if (this.isLooking)
			{
				if (this.lookingDisposable != null)
				{
					this.lookingDisposable.Dispose();
				}
				this.lookingDisposable = (from _ in Observable.EveryUpdate().TakeUntilDestroy(base.Merchant.gameObject)
				where base.Merchant.isActiveAndEnabled
				select _).Subscribe(delegate(long _)
				{
					this.LookAtPlayer();
				});
			}
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06006CEC RID: 27884 RVA: 0x002E7668 File Offset: 0x002E5A68
		private bool IsCloseToPlayer
		{
			[CompilerGenerated]
			get
			{
				return this.distanceToPlayer <= Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.arrivedDistance;
			}
		}

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06006CED RID: 27885 RVA: 0x002E7698 File Offset: 0x002E5A98
		private bool IsFarPlayer
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting.leaveDistance < this.distanceToPlayer;
			}
		}

		// Token: 0x06006CEE RID: 27886 RVA: 0x002E76C4 File Offset: 0x002E5AC4
		private void LookAtPlayer()
		{
			if (!this.isLooking)
			{
				return;
			}
			PlayerActor player = this.Player;
			this.distanceToPlayer = Vector3.Distance(player.Position, base.Merchant.Position);
			if (this.IsCloseToPlayer && !this.isNearPlayer)
			{
				Transform trfTarg = player.FovTargetPointTable[Actor.FovBodyPart.Head];
				ChaControl chaControl = base.Merchant.ChaControl;
				chaControl.ChangeLookEyesTarget(1, trfTarg, 0.5f, 0f, 1f, 2f);
				chaControl.ChangeLookEyesPtn(1);
				chaControl.ChangeLookNeckTarget(1, trfTarg, 0.5f, 0f, 1f, 0.8f);
				chaControl.ChangeLookNeckPtn(1, 1f);
				this.isNearPlayer = true;
			}
			else if (this.IsFarPlayer && this.isNearPlayer)
			{
				ChaControl chaControl2 = base.Merchant.ChaControl;
				chaControl2.ChangeLookEyesPtn(3);
				chaControl2.ChangeLookNeckPtn(3, 1f);
				this.isNearPlayer = false;
			}
		}

		// Token: 0x06006CEF RID: 27887 RVA: 0x002E77C4 File Offset: 0x002E5BC4
		protected void Complete()
		{
			if (base.Merchant.CurrentMerchantPoint == null)
			{
				return;
			}
			base.Merchant.Animation.ResetDefaultAnimatorController();
			base.Merchant.CurrentMerchantPoint.SetStand(base.Merchant, this.recoveryPoint, this.animInfo.outEnableBlend, this.animInfo.outBlendSec, this.animInfo.directionType, null);
			base.PrevMerchantPoint = base.CurrentMerchantPoint;
			base.CurrentMerchantPoint = null;
		}

		// Token: 0x06006CF0 RID: 27888 RVA: 0x002E784C File Offset: 0x002E5C4C
		public override void OnEnd()
		{
			base.Merchant.ClearItems();
			base.Merchant.ClearParticles();
			base.Merchant.SetActiveOnEquipedItem(true);
			if (this.prevIsTalkable != base.Merchant.Talkable)
			{
				base.Merchant.Talkable = this.prevIsTalkable;
			}
			if (this.lookingDisposable != null)
			{
				this.lookingDisposable.Dispose();
			}
			base.Merchant.SetLookPtn(0, 3);
			base.Merchant.SetLookTarget(0, 0, null);
			base.OnEnd();
		}

		// Token: 0x04005B0F RID: 23311
		[SerializeField]
		private Merchant.EventType stateType;

		// Token: 0x04005B10 RID: 23312
		protected int actionID;

		// Token: 0x04005B11 RID: 23313
		protected int poseID;

		// Token: 0x04005B12 RID: 23314
		protected Subject<Unit> onActionPlay;

		// Token: 0x04005B13 RID: 23315
		protected Subject<Unit> onEndAction;

		// Token: 0x04005B14 RID: 23316
		protected ActorAnimInfo animInfo = default(ActorAnimInfo);

		// Token: 0x04005B15 RID: 23317
		protected Transform recoveryPoint;

		// Token: 0x04005B16 RID: 23318
		private bool prevIsTalkable = true;

		// Token: 0x04005B17 RID: 23319
		private bool isLooking;

		// Token: 0x04005B18 RID: 23320
		private bool isNearPlayer;

		// Token: 0x04005B19 RID: 23321
		private IDisposable lookingDisposable;

		// Token: 0x04005B1A RID: 23322
		private float distanceToPlayer = float.MaxValue;
	}
}
