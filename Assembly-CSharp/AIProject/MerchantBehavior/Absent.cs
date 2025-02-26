using System;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA2 RID: 3490
	[TaskCategory("商人")]
	public class Absent : MerchantAction
	{
		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06006CF6 RID: 27894 RVA: 0x002E794F File Offset: 0x002E5D4F
		private PlayerActor Player
		{
			[CompilerGenerated]
			get
			{
				return (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player;
			}
		}

		// Token: 0x06006CF7 RID: 27895 RVA: 0x002E796C File Offset: 0x002E5D6C
		public override void OnStart()
		{
			base.OnStart();
			MerchantPoint merchantPoint = base.TargetInSightMerchantPoint;
			base.CurrentMerchantPoint = merchantPoint;
			if (merchantPoint == null)
			{
				merchantPoint = base.Merchant.ExitPoint;
				base.TargetInSightMerchantPoint = merchantPoint;
				base.CurrentMerchantPoint = merchantPoint;
			}
			this._currentPoint = base.CurrentMerchantPoint;
			if (this.prevTalkable = base.Merchant.Talkable)
			{
				base.Merchant.Talkable = false;
			}
			if (base.Merchant.ChaControl.visibleAll || (this._currentPoint != null && this._currentPoint.AnyActiveItemObjects()))
			{
				this.CrossFade();
			}
			base.Merchant.Hide();
			if (this._currentPoint != null)
			{
				this._currentPoint.HideItemObjects();
			}
			base.Merchant.DeactivateNavMeshElement();
			base.Merchant.Animation.StopAllAnimCoroutine();
			base.Merchant.Animation.Animator.Play(Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.MerchantIdleState, 0);
			string forwardMove = Singleton<Manager.Resources>.Instance.DefinePack.AnimatorParameter.ForwardMove;
			foreach (AnimatorControllerParameter animatorControllerParameter in base.Merchant.Animation.Animator.parameters)
			{
				if (animatorControllerParameter.name == forwardMove && animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					base.Merchant.Animation.Animator.SetFloat(forwardMove, 0f);
				}
			}
			if (this._currentPoint == null)
			{
				return;
			}
			base.Merchant.SetPointIDInfo(base.CurrentMerchantPoint);
			Tuple<MerchantPointInfo, Transform, Transform> eventInfo = base.CurrentMerchantPoint.GetEventInfo(AIProject.Definitions.Merchant.EventType.Wait);
			base.Merchant.Position = eventInfo.Item2.position;
			base.Merchant.Rotation = eventInfo.Item2.rotation;
		}

		// Token: 0x06006CF8 RID: 27896 RVA: 0x002E7B6E File Offset: 0x002E5F6E
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}

		// Token: 0x06006CF9 RID: 27897 RVA: 0x002E7B74 File Offset: 0x002E5F74
		private void CrossFade()
		{
			ActorCameraControl actorCameraControl = (!(this.Player != null)) ? null : this.Player.CameraControl;
			if (actorCameraControl != null)
			{
				bool flag = false;
				float crossFadeEnableDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.CrossFadeEnableDistance;
				flag |= (Vector3.Distance(base.Merchant.Position, actorCameraControl.transform.position) <= crossFadeEnableDistance);
				bool flag2 = flag;
				MerchantPoint currentMerchantPoint = base.CurrentMerchantPoint;
				bool? flag3 = (currentMerchantPoint != null) ? new bool?(currentMerchantPoint.CheckDistance(actorCameraControl.transform.position, crossFadeEnableDistance)) : null;
				flag = (flag2 | (flag3 != null && flag3.Value));
				if (flag)
				{
					actorCameraControl.CrossFade.FadeStart(-1f);
				}
			}
		}

		// Token: 0x06006CFA RID: 27898 RVA: 0x002E7C48 File Offset: 0x002E6048
		public override void OnEnd()
		{
			this.CrossFade();
			base.Merchant.Show();
			if (this._currentPoint != null)
			{
				this._currentPoint.ShowItemObjects();
			}
			if (base.CurrentMerchantPoint != null)
			{
				base.PrevMerchantPoint = base.CurrentMerchantPoint;
				base.CurrentMerchantPoint = null;
			}
			if (this.prevTalkable)
			{
				base.Merchant.Talkable = true;
			}
			base.Merchant.MerchantData.PointID = -1;
			base.OnEnd();
		}

		// Token: 0x04005B1B RID: 23323
		private bool prevTalkable = true;

		// Token: 0x04005B1C RID: 23324
		private MerchantPoint _currentPoint;
	}
}
