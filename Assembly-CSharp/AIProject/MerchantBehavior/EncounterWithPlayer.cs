using System;
using AIChara;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DBB RID: 3515
	[TaskCategory("商人")]
	public class EncounterWithPlayer : MerchantAction
	{
		// Token: 0x06006D4D RID: 27981 RVA: 0x002E89E4 File Offset: 0x002E6DE4
		public override void OnStart()
		{
			base.OnStart();
			this.counter = 0f;
			base.Merchant.ActivateNavMeshObstacle(base.Merchant.Position);
			this.prevTalkable = base.Merchant.Talkable;
			if (!this.prevTalkable)
			{
				base.Merchant.Talkable = true;
			}
			this._player = ((!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player);
			this._merchant = base.Merchant;
			ChaControl chaControl = base.Merchant.ChaControl;
			chaControl.ChangeLookNeckTarget(1, this._player.FovTargetPointTable[Actor.FovBodyPart.Head], 0.5f, 0f, 1f, 0.8f);
			chaControl.ChangeLookNeckPtn(1, 1f);
		}

		// Token: 0x06006D4E RID: 27982 RVA: 0x002E8AB0 File Offset: 0x002E6EB0
		public override TaskStatus OnUpdate()
		{
			if (this._player == null)
			{
				return TaskStatus.Failure;
			}
			if (this.hasMotivation)
			{
				this.counter += Time.deltaTime;
				if (this.motivationLimit <= this.counter)
				{
					return TaskStatus.Failure;
				}
			}
			return (!this._merchant.IsFarPlayer) ? TaskStatus.Running : TaskStatus.Failure;
		}

		// Token: 0x06006D4F RID: 27983 RVA: 0x002E8B17 File Offset: 0x002E6F17
		public override void OnEnd()
		{
			base.Merchant.ChaControl.ChangeLookNeckPtn(3, 1f);
			if (!this.prevTalkable)
			{
				base.Merchant.Talkable = false;
			}
			base.OnEnd();
		}

		// Token: 0x04005B34 RID: 23348
		[SerializeField]
		private bool hasMotivation;

		// Token: 0x04005B35 RID: 23349
		[SerializeField]
		private float motivationLimit = 30f;

		// Token: 0x04005B36 RID: 23350
		private PlayerActor _player;

		// Token: 0x04005B37 RID: 23351
		private MerchantActor _merchant;

		// Token: 0x04005B38 RID: 23352
		private float counter;

		// Token: 0x04005B39 RID: 23353
		private bool prevTalkable;
	}
}
