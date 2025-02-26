using System;
using AIChara;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CEF RID: 3311
	[TaskCategory("")]
	public class LookAtPlayer : AgentAction
	{
		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06006AB4 RID: 27316 RVA: 0x002D8AF0 File Offset: 0x002D6EF0
		private bool IsCloseToPlayer
		{
			get
			{
				AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
				return this._distanceTweenPlayer <= rangeSetting.arrivedDistance && this._heightDistTweenPlayer <= rangeSetting.acceptableHeight;
			}
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06006AB5 RID: 27317 RVA: 0x002D8B34 File Offset: 0x002D6F34
		private bool IsFarPlayer
		{
			get
			{
				AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
				return rangeSetting.leaveDistance < this._distanceTweenPlayer || rangeSetting.acceptableHeight < this._heightDistTweenPlayer;
			}
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x002D8B78 File Offset: 0x002D6F78
		public override void OnStart()
		{
			base.OnStart();
			this._agent = base.Agent;
			this._chara = ((this._agent != null) ? this._agent.ChaControl : null);
			this._player = ((!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player);
			this._missing = (this._agent == null || this._chara == null || this._player == null);
			if (this._missing)
			{
				return;
			}
			this._prevCloseToPlayer = false;
			this._distanceTweenPlayer = float.MaxValue;
			this._heightDistTweenPlayer = float.MaxValue;
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x002D8C38 File Offset: 0x002D7038
		public override TaskStatus OnUpdate()
		{
			if (this._missing)
			{
				return TaskStatus.Failure;
			}
			Vector3 position = this._agent.Position;
			Vector3 position2 = this._player.Position;
			Vector3 forward = this._agent.Forward;
			this._heightDistTweenPlayer = Mathf.Abs(position.y - position2.y);
			position.y = (position2.y = (forward.y = 0f));
			this._distanceTweenPlayer = Vector3.Distance(position, position2);
			if (!this._prevCloseToPlayer && this.IsCloseToPlayer)
			{
				this._prevCloseToPlayer = true;
				this.LookAtPlayerHead(true);
			}
			else if (this._prevCloseToPlayer && this.IsFarPlayer)
			{
				this._prevCloseToPlayer = false;
				this.LookAtPlayerHead(false);
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x002D8D0C File Offset: 0x002D710C
		private void LookAtPlayerHead(bool flag)
		{
			if (this._chara == null)
			{
				return;
			}
			if (flag)
			{
				Transform trfTarg = this._player.FovTargetPointTable[Actor.FovBodyPart.Head];
				this._chara.ChangeLookEyesTarget(1, trfTarg, 0.5f, 0f, 1f, 2f);
				this._chara.ChangeLookEyesPtn(1);
				this._chara.ChangeLookNeckTarget(1, trfTarg, 0.5f, 0f, 1f, 0.8f);
				this._chara.ChangeLookNeckPtn(1, 1f);
			}
			else
			{
				Transform transform = this._player.CameraControl.CameraComponent.transform;
				this._chara.ChangeLookEyesTarget(0, transform, 0.5f, 0f, 1f, 2f);
				this._chara.ChangeLookEyesPtn(0);
				this._chara.ChangeLookNeckTarget(3, transform, 0.5f, 0f, 1f, 0.8f);
				this._chara.ChangeLookNeckPtn(3, 1f);
			}
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x002D8E1C File Offset: 0x002D721C
		public override void OnEnd()
		{
			this.LookAtPlayerHead(false);
			base.OnEnd();
		}

		// Token: 0x04005A2F RID: 23087
		private bool _prevCloseToPlayer;

		// Token: 0x04005A30 RID: 23088
		private float _distanceTweenPlayer = float.MaxValue;

		// Token: 0x04005A31 RID: 23089
		private float _heightDistTweenPlayer = float.MaxValue;

		// Token: 0x04005A32 RID: 23090
		private AgentActor _agent;

		// Token: 0x04005A33 RID: 23091
		private ChaControl _chara;

		// Token: 0x04005A34 RID: 23092
		private PlayerActor _player;

		// Token: 0x04005A35 RID: 23093
		private bool _missing;
	}
}
