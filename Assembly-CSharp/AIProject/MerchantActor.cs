using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ADV;
using AIChara;
using AIProject.Definitions;
using AIProject.Player;
using AIProject.SaveData;
using AIProject.UI;
using IllusionUtility.GetUtility;
using Manager;
using MessagePack;
using ReMotion;
using RootMotion.FinalIK;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C69 RID: 3177
	public class MerchantActor : Actor, ICommandable, INavMeshActor
	{
		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x060066A3 RID: 26275 RVA: 0x002BACFF File Offset: 0x002B90FF
		public override string CharaName
		{
			[CompilerGenerated]
			get
			{
				return "シャン";
			}
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x060066A4 RID: 26276 RVA: 0x002BAD06 File Offset: 0x002B9106
		// (set) Token: 0x060066A5 RID: 26277 RVA: 0x002BAD0E File Offset: 0x002B910E
		private protected MerchantBehaviorTreeResources BehaviorResources { protected get; private set; }

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x002BAD17 File Offset: 0x002B9117
		public Merchant.ActionType BehaviorMode
		{
			[CompilerGenerated]
			get
			{
				return (!(this.BehaviorResources != null)) ? Merchant.ActionType.ToAbsent : this.BehaviorResources.Mode;
			}
		}

		// Token: 0x060066A7 RID: 26279 RVA: 0x002BAD3B File Offset: 0x002B913B
		public void EnableBehavior()
		{
			if (this.BehaviorResources != null && !this.BehaviorResources.enabled)
			{
				this.BehaviorResources.enabled = true;
			}
		}

		// Token: 0x060066A8 RID: 26280 RVA: 0x002BAD6A File Offset: 0x002B916A
		public void DisableBehavior()
		{
			if (this.BehaviorResources != null && this.BehaviorResources.enabled)
			{
				this.BehaviorResources.enabled = false;
			}
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x002BAD99 File Offset: 0x002B9199
		public void StopBehavior()
		{
			if (this.BehaviorResources != null)
			{
				this.BehaviorResources.StopBehaviorTree();
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x060066AA RID: 26282 RVA: 0x002BADB7 File Offset: 0x002B91B7
		// (set) Token: 0x060066AB RID: 26283 RVA: 0x002BADBF File Offset: 0x002B91BF
		public Merchant.ActionType CurrentMode { get; private set; } = Merchant.ActionType.Wait;

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x060066AC RID: 26284 RVA: 0x002BADC8 File Offset: 0x002B91C8
		// (set) Token: 0x060066AD RID: 26285 RVA: 0x002BADD0 File Offset: 0x002B91D0
		public Merchant.ActionType PrevMode { get; private set; } = Merchant.ActionType.Wait;

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x060066AE RID: 26286 RVA: 0x002BADD9 File Offset: 0x002B91D9
		// (set) Token: 0x060066AF RID: 26287 RVA: 0x002BADE1 File Offset: 0x002B91E1
		public Merchant.ActionType NextMode { get; private set; } = Merchant.ActionType.Wait;

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x060066B0 RID: 26288 RVA: 0x002BADEA File Offset: 0x002B91EA
		// (set) Token: 0x060066B1 RID: 26289 RVA: 0x002BADF2 File Offset: 0x002B91F2
		public Merchant.ActionType LastNormalMode { get; private set; } = Merchant.ActionType.Wait;

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x060066B2 RID: 26290 RVA: 0x002BADFB File Offset: 0x002B91FB
		// (set) Token: 0x060066B3 RID: 26291 RVA: 0x002BAE03 File Offset: 0x002B9203
		public int OpenAreaID { get; set; }

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x060066B4 RID: 26292 RVA: 0x002BAE0C File Offset: 0x002B920C
		// (set) Token: 0x060066B5 RID: 26293 RVA: 0x002BAE14 File Offset: 0x002B9214
		public bool IsActionMoving { get; set; }

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x060066B6 RID: 26294 RVA: 0x002BAE1D File Offset: 0x002B921D
		// (set) Token: 0x060066B7 RID: 26295 RVA: 0x002BAE25 File Offset: 0x002B9225
		public bool IsRunning { get; set; }

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x060066B8 RID: 26296 RVA: 0x002BAE2E File Offset: 0x002B922E
		// (set) Token: 0x060066B9 RID: 26297 RVA: 0x002BAE36 File Offset: 0x002B9236
		public MerchantPoint CurrentMerchantPoint { get; set; }

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x060066BA RID: 26298 RVA: 0x002BAE3F File Offset: 0x002B923F
		// (set) Token: 0x060066BB RID: 26299 RVA: 0x002BAE47 File Offset: 0x002B9247
		public MerchantPoint TargetInSightMerchantPoint { get; set; }

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x060066BC RID: 26300 RVA: 0x002BAE50 File Offset: 0x002B9250
		// (set) Token: 0x060066BD RID: 26301 RVA: 0x002BAE58 File Offset: 0x002B9258
		public MerchantPoint MainTargetInSightMerchantPoint { get; set; }

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060066BE RID: 26302 RVA: 0x002BAE61 File Offset: 0x002B9261
		// (set) Token: 0x060066BF RID: 26303 RVA: 0x002BAE69 File Offset: 0x002B9269
		public MerchantPoint PrevMerchantPoint { get; set; }

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060066C0 RID: 26304 RVA: 0x002BAE72 File Offset: 0x002B9272
		// (set) Token: 0x060066C1 RID: 26305 RVA: 0x002BAE7A File Offset: 0x002B927A
		public MerchantPoint StartPoint { get; private set; }

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060066C2 RID: 26306 RVA: 0x002BAE83 File Offset: 0x002B9283
		// (set) Token: 0x060066C3 RID: 26307 RVA: 0x002BAE8B File Offset: 0x002B928B
		public MerchantPoint ExitPoint { get; private set; }

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x060066C4 RID: 26308 RVA: 0x002BAE94 File Offset: 0x002B9294
		// (set) Token: 0x060066C5 RID: 26309 RVA: 0x002BAE9C File Offset: 0x002B929C
		public ActionPoint PrevActionPoint { get; set; }

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060066C6 RID: 26310 RVA: 0x002BAEA5 File Offset: 0x002B92A5
		// (set) Token: 0x060066C7 RID: 26311 RVA: 0x002BAEAD File Offset: 0x002B92AD
		public ActionPoint BookingActionPoint { get; set; }

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x060066C8 RID: 26312 RVA: 0x002BAEB6 File Offset: 0x002B92B6
		// (set) Token: 0x060066C9 RID: 26313 RVA: 0x002BAECD File Offset: 0x002B92CD
		public MerchantActor.MerchantSchedule CurrentSchedule
		{
			get
			{
				MerchantData merchantData = this.MerchantData;
				return (merchantData != null) ? merchantData.CurrentSchedule : null;
			}
			private set
			{
				if (this.MerchantData != null)
				{
					this.MerchantData.CurrentSchedule = value;
				}
			}
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x060066CA RID: 26314 RVA: 0x002BAEE6 File Offset: 0x002B92E6
		public List<MerchantActor.MerchantSchedule> ScheduleList
		{
			[CompilerGenerated]
			get
			{
				MerchantData merchantData = this.MerchantData;
				return (merchantData != null) ? merchantData.ScheduleList : null;
			}
		}

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x060066CB RID: 26315 RVA: 0x002BAEFD File Offset: 0x002B92FD
		// (set) Token: 0x060066CC RID: 26316 RVA: 0x002BAF14 File Offset: 0x002B9314
		public MerchantActor.MerchantSchedule PrevSchedule
		{
			get
			{
				MerchantData merchantData = this.MerchantData;
				return (merchantData != null) ? merchantData.PrevSchedule : null;
			}
			private set
			{
				if (this.MerchantData != null)
				{
					this.MerchantData.PrevSchedule = value;
				}
			}
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x060066CD RID: 26317 RVA: 0x002BAF2D File Offset: 0x002B932D
		// (set) Token: 0x060066CE RID: 26318 RVA: 0x002BAF35 File Offset: 0x002B9335
		public bool Talkable { get; set; } = true;

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x060066CF RID: 26319 RVA: 0x002BAF3E File Offset: 0x002B933E
		// (set) Token: 0x060066D0 RID: 26320 RVA: 0x002BAF46 File Offset: 0x002B9346
		public bool ActiveEncount { get; set; }

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x060066D1 RID: 26321 RVA: 0x002BAF4F File Offset: 0x002B934F
		// (set) Token: 0x060066D2 RID: 26322 RVA: 0x002BAF61 File Offset: 0x002B9361
		public Vector3 ObstaclePosition
		{
			get
			{
				return base.NavMeshObstacle.transform.position;
			}
			set
			{
				base.NavMeshObstacle.transform.position = value;
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x060066D3 RID: 26323 RVA: 0x002BAF74 File Offset: 0x002B9374
		// (set) Token: 0x060066D4 RID: 26324 RVA: 0x002BAF86 File Offset: 0x002B9386
		public Quaternion ObstacleRotation
		{
			get
			{
				return base.NavMeshObstacle.transform.rotation;
			}
			set
			{
				base.NavMeshObstacle.transform.rotation = value;
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x060066D5 RID: 26325 RVA: 0x002BAF99 File Offset: 0x002B9399
		public bool IsActiveAgnet
		{
			[CompilerGenerated]
			get
			{
				return base.NavMeshAgent.enabled;
			}
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x060066D6 RID: 26326 RVA: 0x002BAFA6 File Offset: 0x002B93A6
		public bool IsActiveObstacle
		{
			[CompilerGenerated]
			get
			{
				return base.NavMeshObstacle.enabled;
			}
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x060066D7 RID: 26327 RVA: 0x002BAFB3 File Offset: 0x002B93B3
		public bool IsActiveNavMeshElement
		{
			[CompilerGenerated]
			get
			{
				return this.IsActiveAgnet || this.IsActiveObstacle;
			}
		}

		// Token: 0x060066D8 RID: 26328 RVA: 0x002BAFCC File Offset: 0x002B93CC
		public void AddSequenceActionDisposable(IDisposable disposable)
		{
			CompositeDisposable compositeDisposable;
			if ((compositeDisposable = this.SequenceAction.Item1) == null)
			{
				compositeDisposable = (this.SequenceAction.Item1 = new CompositeDisposable());
			}
			compositeDisposable.Add(disposable);
		}

		// Token: 0x060066D9 RID: 26329 RVA: 0x002BB004 File Offset: 0x002B9404
		public void AddSequenceActionOnComplete(System.Action onComplete)
		{
			this.SequenceAction.Item2 = (System.Action)Delegate.Combine(this.SequenceAction.Item2, onComplete);
		}

		// Token: 0x060066DA RID: 26330 RVA: 0x002BB022 File Offset: 0x002B9422
		public void SetSequenceAction(IDisposable disposable, System.Action onComplete)
		{
			this.AddSequenceActionDisposable(disposable);
			this.AddSequenceActionOnComplete(onComplete);
		}

		// Token: 0x060066DB RID: 26331 RVA: 0x002BB032 File Offset: 0x002B9432
		public void ClearSequenceAction()
		{
			this.SequenceAction.Item1 = null;
			this.SequenceAction.Item2 = null;
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x002BB04C File Offset: 0x002B944C
		public void StopSequenceAction()
		{
			System.Action item = this.SequenceAction.Item2;
			if (item != null)
			{
				item();
			}
			this.SequenceAction.Item2 = null;
			CompositeDisposable item2 = this.SequenceAction.Item1;
			if (item2 != null)
			{
				item2.Clear();
			}
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x002BB08C File Offset: 0x002B948C
		public void DisposeSequenceAction()
		{
			this.SequenceAction.Item2 = null;
			CompositeDisposable item = this.SequenceAction.Item1;
			if (item != null)
			{
				item.Clear();
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x060066DE RID: 26334 RVA: 0x002BB0B3 File Offset: 0x002B94B3
		// (set) Token: 0x060066DF RID: 26335 RVA: 0x002BB0BB File Offset: 0x002B94BB
		public bool ElapsedDay { get; set; }

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x060066E0 RID: 26336 RVA: 0x002BB0C4 File Offset: 0x002B94C4
		// (set) Token: 0x060066E1 RID: 26337 RVA: 0x002BB0CC File Offset: 0x002B94CC
		public int ElapsedDayCount { get; protected set; }

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x060066E2 RID: 26338 RVA: 0x002BB0D5 File Offset: 0x002B94D5
		// (set) Token: 0x060066E3 RID: 26339 RVA: 0x002BB0DD File Offset: 0x002B94DD
		public List<MerchantPoint> MerchantPoints { get; set; } = new List<MerchantPoint>();

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x060066E4 RID: 26340 RVA: 0x002BB0E6 File Offset: 0x002B94E6
		// (set) Token: 0x060066E5 RID: 26341 RVA: 0x002BB0EE File Offset: 0x002B94EE
		public bool IsImpossible { get; private set; }

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x060066E6 RID: 26342 RVA: 0x002BB0F7 File Offset: 0x002B94F7
		// (set) Token: 0x060066E7 RID: 26343 RVA: 0x002BB0FF File Offset: 0x002B94FF
		public Actor CommandPartner { get; set; }

		// Token: 0x060066E8 RID: 26344 RVA: 0x002BB108 File Offset: 0x002B9508
		public bool SetImpossible(bool value, Actor actor)
		{
			if (this.IsImpossible == value)
			{
				return false;
			}
			if (value && this.CommandPartner != null)
			{
				return false;
			}
			this.IsImpossible = value;
			this.CommandPartner = ((!value) ? null : actor);
			return true;
		}

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x060066E9 RID: 26345 RVA: 0x002BB158 File Offset: 0x002B9558
		public override bool IsNeutralCommand
		{
			get
			{
				return !this.IsImpossible && this.CurrentSchedule != null && this.Talkable && this.MerchantData.Unlock && (Merchant.NormalModeList.Contains(this.CurrentMode) || this.CurrentMode == Merchant.ActionType.Encounter);
			}
		}

		// Token: 0x060066EA RID: 26346 RVA: 0x002BB1C0 File Offset: 0x002B95C0
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (radiusA < distance)
			{
				return false;
			}
			Vector3 position = base.Position;
			position.y = 0f;
			float num = angle / 2f;
			float num2 = Vector3.Angle(position - basePosition, forward);
			return num2 <= num;
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x002BB208 File Offset: 0x002B9608
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this.pathForCalc == null)
			{
				this.pathForCalc = new NavMeshPath();
			}
			bool flag = true;
			if (nmAgent.isActiveAndEnabled)
			{
				nmAgent.CalculatePath(base.Position, this.pathForCalc);
				flag &= (this.pathForCalc.status == NavMeshPathStatus.PathComplete);
				float num = 0f;
				Vector3[] corners = this.pathForCalc.corners;
				for (int i = 0; i < corners.Length - 1; i++)
				{
					float num2 = Vector3.Distance(corners[i], corners[i + 1]);
					num += num2;
					float num3 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
					if (num > num3)
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x060066EC RID: 26348 RVA: 0x002BB2D8 File Offset: 0x002B96D8
		public CommandLabel.CommandInfo[] Labels
		{
			get
			{
				PlayerActor playerActor = (!Singleton<Manager.Map>.IsInstance()) ? null : Singleton<Manager.Map>.Instance.Player;
				if (playerActor != null && playerActor.PlayerController.State is Onbu)
				{
					return null;
				}
				return this._talkLabel;
			}
		}

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x060066ED RID: 26349 RVA: 0x002BB329 File Offset: 0x002B9729
		public CommandLabel.CommandInfo[] DateLabels
		{
			[CompilerGenerated]
			get
			{
				return this._emptyLabel;
			}
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x060066EE RID: 26350 RVA: 0x002BB331 File Offset: 0x002B9731
		public ObjectLayer Layer
		{
			[CompilerGenerated]
			get
			{
				return this._layer;
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x060066EF RID: 26351 RVA: 0x002BB339 File Offset: 0x002B9739
		public CommandType CommandType
		{
			[CompilerGenerated]
			get
			{
				return CommandType.Forward;
			}
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x060066F0 RID: 26352 RVA: 0x002BB33C File Offset: 0x002B973C
		public override ActorAnimation Animation
		{
			[CompilerGenerated]
			get
			{
				return this._animation;
			}
		}

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x060066F1 RID: 26353 RVA: 0x002BB344 File Offset: 0x002B9744
		public ActorAnimationMerchant AnimationMerchant
		{
			[CompilerGenerated]
			get
			{
				return this._animation;
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x060066F2 RID: 26354 RVA: 0x002BB34C File Offset: 0x002B974C
		public override ActorLocomotion Locomotor
		{
			[CompilerGenerated]
			get
			{
				return this._locomotor;
			}
		}

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x060066F3 RID: 26355 RVA: 0x002BB354 File Offset: 0x002B9754
		public ActorLocomotionMerchant LocomotorMerchant
		{
			[CompilerGenerated]
			get
			{
				return this._locomotor;
			}
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x060066F4 RID: 26356 RVA: 0x002BB35C File Offset: 0x002B975C
		public override ActorController Controller
		{
			[CompilerGenerated]
			get
			{
				return this._controller;
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x060066F5 RID: 26357 RVA: 0x002BB364 File Offset: 0x002B9764
		public MerchantController MerchantController
		{
			[CompilerGenerated]
			get
			{
				return this._controller;
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x060066F6 RID: 26358 RVA: 0x002BB36C File Offset: 0x002B976C
		public override ICharacterInfo TiedInfo
		{
			[CompilerGenerated]
			get
			{
				return this.MerchantData;
			}
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x060066F7 RID: 26359 RVA: 0x002BB374 File Offset: 0x002B9774
		// (set) Token: 0x060066F8 RID: 26360 RVA: 0x002BB37C File Offset: 0x002B977C
		public MerchantData MerchantData { get; set; }

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x060066F9 RID: 26361 RVA: 0x002BB385 File Offset: 0x002B9785
		// (set) Token: 0x060066FA RID: 26362 RVA: 0x002BB38D File Offset: 0x002B978D
		public float DestinationSpeed { get; set; }

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x060066FB RID: 26363 RVA: 0x002BB396 File Offset: 0x002B9796
		// (set) Token: 0x060066FC RID: 26364 RVA: 0x002BB39E File Offset: 0x002B979E
		public List<GameObject> ShipObjects { get; private set; }

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x060066FD RID: 26365 RVA: 0x002BB3A7 File Offset: 0x002B97A7
		private int charaID
		{
			[CompilerGenerated]
			get
			{
				return this.MerchantData.param.charaID;
			}
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x060066FE RID: 26366 RVA: 0x002BB3B9 File Offset: 0x002B97B9
		private OpenData openData { get; } = new OpenData();

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x060066FF RID: 26367 RVA: 0x002BB3C1 File Offset: 0x002B97C1
		// (set) Token: 0x06006700 RID: 26368 RVA: 0x002BB3C9 File Offset: 0x002B97C9
		private MerchantActor.PackData packData { get; set; }

		// Token: 0x06006701 RID: 26369 RVA: 0x002BB3D4 File Offset: 0x002B97D4
		private void Awake()
		{
			this.BehaviorResources = base.GetComponentInChildren<MerchantBehaviorTreeResources>(true);
			ActorAnimation animation = this.Animation;
			bool? flag = (animation != null) ? new bool?(animation.enabled) : null;
			this._prevAnimEnable = (flag == null || flag.Value);
			bool? flag2 = (this._locomotor != null) ? new bool?(this._locomotor.enabled) : null;
			this._prevLocomEnable = (flag2 == null || flag2.Value);
			bool? flag3 = (this._controller != null) ? new bool?(this._controller.enabled) : null;
			this._prevConEnable = (flag3 == null || flag3.Value);
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x002BB4BC File Offset: 0x002B98BC
		protected override void OnStart()
		{
			this.BehaviorResources.Initialize();
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06006703 RID: 26371 RVA: 0x002BB4FC File Offset: 0x002B98FC
		public override IEnumerator LoadAsync()
		{
			this.Animation.enabled = false;
			this.Animation.enabled = false;
			if (this.MerchantData != null)
			{
				yield return this.LoadCharAsync(this.MerchantData.CharaFileName);
			}
			else
			{
				yield return this.LoadCharAsync(string.Empty);
			}
			Animator animator = base.ChaControl.animBody;
			GameObject commandTargetObj = base.ChaControl.transform.FindLoop("cf_J_Mune00");
			GameObject gameObject = commandTargetObj;
			Transform commandTarget = (gameObject != null) ? gameObject.transform : null;
			CommonDefine.CommonIconGroup iconDefine = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			this.packData = new MerchantActor.PackData();
			CharaPackData packData = this.packData;
			ADV.ICommandData[] array = new ADV.ICommandData[1];
			int num = 0;
			Game instance = Singleton<Game>.Instance;
			ADV.ICommandData commandData;
			if (instance == null)
			{
				commandData = null;
			}
			else
			{
				WorldData worldData = instance.WorldData;
				commandData = ((worldData != null) ? worldData.Environment : null);
			}
			array[num] = commandData;
			packData.SetCommandData(array);
			CharaPackData packData2 = this.packData;
			IParams[] array2 = new IParams[2];
			array2[0] = this.MerchantData;
			int num2 = 1;
			Game instance2 = Singleton<Game>.Instance;
			IParams @params;
			if (instance2 == null)
			{
				@params = null;
			}
			else
			{
				WorldData worldData2 = instance2.WorldData;
				@params = ((worldData2 != null) ? worldData2.PlayerData : null);
			}
			array2[num2] = @params;
			packData2.SetParam(array2);
			Sprite actorIcon;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActorIconTable.TryGetValue(this.ID, out actorIcon);
			this._talkLabel = new CommandLabel.CommandInfo[]
			{
				new CommandLabel.CommandInfo
				{
					Text = "商人と話す",
					Icon = actorIcon,
					TargetSpriteInfo = iconDefine.CharaSpriteInfo,
					Transform = commandTarget,
					Event = delegate
					{
						this.StartCommunication();
						this.openData.FindLoad("0", this.charaID, 0);
						this.packData.onComplete = delegate()
						{
							MapUIContainer.SetActiveCommandList(true, this.CharaName);
							MapUIContainer.SetVisibledSpendMoneyUI(true);
						};
						this.packData.OpenAreaID = this.OpenAreaID;
						this.packData.restoreCommands = this._normalCommandOptionInfo;
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				}
			};
			this._normalCommandOptionInfo = new CommCommandList.CommandInfo[]
			{
				new CommCommandList.CommandInfo("話す")
				{
					Event = delegate(int x)
					{
						this.Animation.StopAllAnimCoroutine();
						MapUIContainer.SetVisibledSpendMoneyUI(false);
						this.openData.FindLoad("2", this.charaID, 0);
						this.packData.onComplete = delegate()
						{
							if (this.packData.restoreCommands != null)
							{
								MapUIContainer.SetVisibledSpendMoneyUI(true);
							}
						};
						this.packData.OpenAreaID = this.OpenAreaID;
						this.packData.restoreCommands = this._normalCommandOptionInfo;
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				},
				new CommCommandList.CommandInfo("ショップ")
				{
					Event = delegate(int x)
					{
						this.Animation.StopAllAnimCoroutine();
						MapUIContainer.SetVisibledSpendMoneyUI(false);
						this.openData.FindLoad("3", this.charaID, 0);
						this.packData.onComplete = delegate()
						{
							MapUIContainer.CommandList.Visibled = false;
							MapUIContainer.ShopUI.OnClose = delegate()
							{
								this.packData.JumpTag = "OUT";
								this.packData.onComplete = delegate()
								{
									MapUIContainer.CommandList.Visibled = true;
									MapUIContainer.SetVisibledSpendMoneyUI(true);
								};
								Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
							};
							MapUIContainer.SetActiveShopUI(true);
						};
						this.packData.JumpTag = "IN";
						this.packData.restoreCommands = null;
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				},
				new CommCommandList.CommandInfo("エッチさせて")
				{
					Condition = delegate
					{
						string[] merchantHMeshTag = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.MerchantHMeshTag;
						return this.CanSelectHCommand(merchantHMeshTag);
					},
					Event = delegate(int x)
					{
						this.Animation.StopAllAnimCoroutine();
						MapUIContainer.SetVisibledSpendMoneyUI(false);
						this.openData.FindLoad("0", this.charaID, 9);
						this.packData.onComplete = delegate()
						{
							if (this.packData.isHSuccess)
							{
								this.packData.restoreCommands = null;
								this.HSceneEnter(this.packData.hMode);
							}
							else
							{
								MapUIContainer.SetVisibledSpendMoneyUI(true);
							}
						};
						this.packData.JumpTag = "IN";
						this.packData.isLesbian = false;
						this.packData.isWeaknessH = false;
						this.packData.restoreCommands = this._normalCommandOptionInfo;
						Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
					}
				},
				new CommCommandList.CommandInfo("立ち去る")
				{
					Event = delegate(int x)
					{
						this.LeaveADV();
					}
				}
			};
			FullBodyBipedIK ik = animator.GetComponentInChildren<FullBodyBipedIK>(true);
			GameObject ctrlOld = this._animation.gameObject;
			ActorAnimationMerchant actorAnimationMerchant = this._animation.CloneComponent(animator.gameObject);
			actorAnimationMerchant.IK = ik;
			actorAnimationMerchant.Actor = this;
			actorAnimationMerchant.Character = this._locomotor;
			actorAnimationMerchant.Animator = animator;
			this._animation = actorAnimationMerchant;
			ik.enabled = false;
			AssetBundleInfo animABInfo = default(AssetBundleInfo);
			RuntimeAnimatorController rac = Singleton<Manager.Resources>.Instance.Animation.GetMerchantAnimator(0, ref animABInfo);
			this.Animation.SetDefaultAnimatorController(rac);
			this.Animation.SetAnimatorController(rac);
			this.Animation.AnimABInfo = animABInfo;
			animator.Play("Locomotion", 0, 0f);
			this.Animation.enabled = true;
			UnityEngine.Object.Destroy(ctrlOld);
			this._locomotor.CharacterAnimation = this._animation;
			Merchant.ActionType actionType = this.MerchantData.ModeType;
			this.NextMode = actionType;
			actionType = actionType;
			this.PrevMode = actionType;
			this.CurrentMode = actionType;
			this.LastNormalMode = this.MerchantData.LastNormalModeType;
			this.ElapsedDay = (this.MerchantData.MapID == 0 && this.MerchantData.ElapsedDay);
			this.ElapsedDayCount = 0;
			this.IsActionMoving = false;
			this.InitializeIK();
			this._controller.StartBehavior();
			yield break;
		}

		// Token: 0x06006704 RID: 26372 RVA: 0x002BB518 File Offset: 0x002B9918
		protected override IEnumerator LoadCharAsync(string fileName)
		{
			ChaFileControl chaFile = null;
			string assetName = "ill_Default_Merchant";
			Dictionary<int, ListInfoBase> dict = Singleton<Character>.Instance.chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.cha_sample_f);
			foreach (KeyValuePair<int, ListInfoBase> keyValuePair in dict)
			{
				if (keyValuePair.Value.GetInfo(ChaListDefine.KeyType.MainData) == assetName)
				{
					chaFile = new ChaFileControl();
					chaFile.LoadFromAssetBundle(keyValuePair.Value.GetInfo(ChaListDefine.KeyType.MainAB), assetName, false, true);
					break;
				}
			}
			base.ChaControl = Singleton<Character>.Instance.CreateChara(1, base.gameObject, 0, chaFile);
			base.ChaControl.Load(false);
			yield return null;
			base.ChaControl.ChangeLookEyesPtn(3);
			base.ChaControl.ChangeLookNeckPtn(3, 1f);
			this.Controller.InitializeFaceLight(base.ChaControl.gameObject);
			yield return null;
			yield break;
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x002BB534 File Offset: 0x002B9934
		public void ChangeMap(int mapID)
		{
			MerchantData merchantData = this.MerchantData;
			base.MapID = mapID;
			merchantData.MapID = mapID;
			this.ResetState();
			if (base.MapID == 0)
			{
				this.DataUpdate(this.MerchantData);
			}
			else
			{
				this.ElapsedDay = false;
				if (this.StartPoint != null)
				{
					Transform basePoint = this.StartPoint.GetBasePoint(Merchant.EventType.Wait);
					if (base.NavMeshAgent.isActiveAndEnabled)
					{
						base.NavMeshAgent.Warp(basePoint.position);
					}
					else
					{
						base.Position = basePoint.position;
					}
					base.Rotation = basePoint.rotation;
					this.TargetInSightMerchantPoint = this.StartPoint;
					Merchant.ActionType actionType = Merchant.ActionType.Wait;
					this.LastNormalMode = actionType;
					actionType = actionType;
					this.PrevMode = actionType;
					this.CurrentMode = actionType;
				}
				else
				{
					Merchant.ActionType actionType = Merchant.ActionType.Absent;
					this.LastNormalMode = actionType;
					actionType = actionType;
					this.PrevMode = actionType;
					this.CurrentMode = actionType;
				}
			}
		}

		// Token: 0x06006706 RID: 26374 RVA: 0x002BB620 File Offset: 0x002B9A20
		public void ResetState()
		{
			if (Singleton<Voice>.IsInstance())
			{
				Singleton<Voice>.Instance.Stop(-90);
			}
			this.Animation.RecoveryPoint = null;
			this.Animation.EndIgnoreEvent();
			Game.Expression expression = Singleton<Game>.Instance.GetExpression(-90, "標準");
			if (expression != null)
			{
				expression.Change(base.ChaControl);
			}
			this.Animation.ResetDefaultAnimatorController();
		}

		// Token: 0x06006707 RID: 26375 RVA: 0x002BB68C File Offset: 0x002B9A8C
		private void OnUpdate()
		{
			this.DataUpdate();
			this.UpdateEncounter();
			if (this._mapAreaID != null && base.MapArea != null)
			{
				this._mapAreaID.Value = base.MapArea.AreaID;
			}
			if (base.MapArea != null)
			{
				this.MerchantData.AreaID = base.MapArea.AreaID;
			}
			MerchantData merchantData = this.MerchantData;
			StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
			EnvironmentSimulator simulator = Singleton<Manager.Map>.Instance.Simulator;
			if (simulator.EnabledTimeProgression)
			{
				Weather weather = simulator.Weather;
				if (base.AreaType == MapArea.AreaType.Indoor)
				{
					merchantData.Wetness += statusProfile.DrySpeed * Time.deltaTime;
				}
				else
				{
					if (weather != Weather.Rain)
					{
						if (weather != Weather.Storm)
						{
							merchantData.Wetness += statusProfile.DrySpeed * Time.deltaTime;
						}
						else
						{
							merchantData.Wetness += statusProfile.WetRateInStorm * Time.deltaTime;
						}
					}
					else
					{
						merchantData.Wetness += statusProfile.WetRateInRain * Time.deltaTime;
					}
					merchantData.Wetness = Mathf.Clamp(merchantData.Wetness, 0f, 100f);
				}
			}
			if (base.ChaControl != null)
			{
				float wetRate = Mathf.InverseLerp(0f, 100f, merchantData.Wetness);
				base.ChaControl.wetRate = wetRate;
			}
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x002BB814 File Offset: 0x002B9C14
		private void DataUpdate()
		{
			this.MerchantData.MapID = base.MapID;
			if (base.MapID != 0)
			{
				return;
			}
			if (!this.IsActionMoving && this._navMeshAgent.isActiveAndEnabled && this._navMeshAgent.isOnNavMesh)
			{
				this.MerchantData.Position = base.Position;
				this.MerchantData.Rotation = base.Rotation;
			}
			this.MerchantData.ChunkID = base.ChunkID;
			this.MerchantData.AreaID = base.AreaID;
			this.MerchantData.ModeType = this.CurrentMode;
			this.MerchantData.LastNormalModeType = this.LastNormalMode;
			MerchantData merchantData = this.MerchantData;
			Actor commandPartner = this.CommandPartner;
			int? num = (commandPartner != null) ? new int?(commandPartner.ID) : null;
			merchantData.ActionTargetID = ((num == null) ? -1 : num.Value);
			this.MerchantData.OpenAreaID = this.OpenAreaID;
			this.MerchantData.ElapsedDay = this.ElapsedDay;
		}

		// Token: 0x06006709 RID: 26377 RVA: 0x002BB938 File Offset: 0x002B9D38
		public void DataUpdate(MerchantData data)
		{
			if (base.NavMeshAgent.isActiveAndEnabled)
			{
				base.NavMeshAgent.Warp(data.Position);
			}
			else
			{
				base.Position = data.Position;
			}
			base.Rotation = data.Rotation;
			base.MapID = data.MapID;
			base.ChunkID = data.ChunkID;
			this.CurrentMode = data.ModeType;
			this.LastNormalMode = data.LastNormalModeType;
			this.OpenAreaID = data.OpenAreaID;
			this.ElapsedDay = (data.MapID == 0 && data.ElapsedDay);
		}

		// Token: 0x0600670A RID: 26378 RVA: 0x002BB9E0 File Offset: 0x002B9DE0
		public void SetOpenAreaID(Manager.Map map)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			IReadOnlyDictionary<int, List<UnityEx.ValueTuple<int, int>>> areaOpenState = Singleton<Manager.Resources>.Instance.MerchantProfile.AreaOpenState;
			if (areaOpenState == null || areaOpenState.Count == 0)
			{
				return;
			}
			List<UnityEx.ValueTuple<int, int>> list;
			if (!areaOpenState.TryGetValue(map.MapID, out list) || list.IsNullOrEmpty<UnityEx.ValueTuple<int, int>>())
			{
				return;
			}
			this.OpenAreaID = 0;
			if (this.MerchantData != null)
			{
				this.MerchantData.OpenAreaID = 0;
			}
			list.Sort((UnityEx.ValueTuple<int, int> x1, UnityEx.ValueTuple<int, int> x2) => x1.Item1 - x2.Item1);
			for (int i = 0; i < list.Count; i++)
			{
				UnityEx.ValueTuple<int, int> valueTuple = list[i];
				if (!map.GetOpenAreaState(valueTuple.Item1))
				{
					break;
				}
				this.OpenAreaID = valueTuple.Item2;
				if (this.MerchantData != null)
				{
					this.MerchantData.OpenAreaID = valueTuple.Item2;
				}
			}
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x0600670B RID: 26379 RVA: 0x002BBAE0 File Offset: 0x002B9EE0
		public bool IsCloseToPlayer
		{
			get
			{
				AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
				return this._distanceTweenPlayer <= rangeSetting.arrivedDistance && this._heightDistTweenPlayer <= rangeSetting.acceptableHeight;
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x0600670C RID: 26380 RVA: 0x002BBB24 File Offset: 0x002B9F24
		public bool IsFarPlayer
		{
			get
			{
				AgentProfile.RangeParameter rangeSetting = Singleton<Manager.Resources>.Instance.AgentProfile.RangeSetting;
				return rangeSetting.leaveDistance < this._distanceTweenPlayer || rangeSetting.acceptableHeight < this._heightDistTweenPlayer;
			}
		}

		// Token: 0x0600670D RID: 26381 RVA: 0x002BBB68 File Offset: 0x002B9F68
		private bool IsTraverseOffMeshLink()
		{
			if (base.NavMeshAgent.isOnOffMeshLink)
			{
				return true;
			}
			if (base.NavMeshAgent.currentOffMeshLinkData.valid)
			{
				return true;
			}
			if (base.NavMeshAgent.currentOffMeshLinkData.offMeshLink != null)
			{
				ActionPoint component = base.NavMeshAgent.currentOffMeshLinkData.offMeshLink.GetComponent<ActionPoint>();
				if (!component.IsNeutralCommand)
				{
					return true;
				}
			}
			return base.EventKey == EventType.Move || base.EventKey == EventType.DoorClose || base.EventKey == EventType.DoorOpen;
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x002BBC18 File Offset: 0x002BA018
		private void UpdateEncounter()
		{
			PlayerActor player = Manager.Map.GetPlayer();
			if (player == null)
			{
				return;
			}
			IState state = player.PlayerController.State;
			Vector3 position = base.Position;
			Vector3 position2 = player.Position;
			this._heightDistTweenPlayer = Mathf.Abs(position2.y - position.y);
			position.y = (position2.y = 0f);
			this._distanceTweenPlayer = Vector3.Distance(position, position2);
			if (state is Normal || state is Photo)
			{
				if (!this._prevCloseToPlayer && this.ActiveEncount && this.Talkable && this.IsCloseToPlayer && Merchant.EncountList.Contains(this.CurrentMode))
				{
					if (!this.IsTraverseOffMeshLink())
					{
						this._prevCloseToPlayer = true;
						this.ChangeBehavior(Merchant.ActionType.Encounter);
					}
				}
				else if (this._prevCloseToPlayer && !this.IsImpossible && this.IsFarPlayer)
				{
					this._prevCloseToPlayer = false;
				}
			}
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x002BBD30 File Offset: 0x002BA130
		public void SetPointIDInfo(MerchantPoint point)
		{
			if (point == null)
			{
				return;
			}
			if (this.MerchantData.MapID != 0)
			{
				return;
			}
			this.MerchantData.PointID = point.PointID;
			this.MerchantData.PointAreaID = point.AreaID;
			this.MerchantData.PointGroupID = point.GroupID;
			this.MerchantData.PointPosition = point.transform.position;
			this.MerchantData.MainPointPosition = new Vector3(-999f, -999f, -999f);
		}

		// Token: 0x06006710 RID: 26384 RVA: 0x002BBDC4 File Offset: 0x002BA1C4
		public override void OnDayUpdated(TimeSpan timeSpan)
		{
			this.ElapsedDayCount = Mathf.Max(timeSpan.Days, 1);
			this.ElapsedDay = true;
			if (!this.MerchantData.Unlock || base.MapID != 0)
			{
				this.ElapsedDay = false;
				return;
			}
			if (this.IsActionMoving)
			{
				return;
			}
			if (this.BehaviorResources != null && !this.BehaviorResources.enabled)
			{
				return;
			}
			if (Merchant.ChangeableModeList.Contains(this.CurrentMode))
			{
				this.ChangeNextSchedule();
				this.SetCurrentSchedule();
			}
		}

		// Token: 0x06006711 RID: 26385 RVA: 0x002BBE5D File Offset: 0x002BA25D
		public override void OnMinuteUpdated(TimeSpan timeSpan)
		{
		}

		// Token: 0x06006712 RID: 26386 RVA: 0x002BBE5F File Offset: 0x002BA25F
		protected override void LoadEquipedEventItem(EquipEventItemInfo eventItemInfo)
		{
		}

		// Token: 0x06006713 RID: 26387 RVA: 0x002BBE64 File Offset: 0x002BA264
		public override void LoadEventParticles(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>> dictionary;
			Dictionary<int, List<AnimeParticleEventInfo>> dictionary2;
			if (Singleton<Manager.Resources>.Instance.Animation.MerchantCommonParticleEventKeyTable.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out dictionary2) && dictionary2 != null)
			{
				base.LoadEventParticle(dictionary2);
			}
		}

		// Token: 0x06006714 RID: 26388 RVA: 0x002BBEA8 File Offset: 0x002BA2A8
		public void LoadMerchantEventParticles(int eventID, int poseID)
		{
			Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>> dictionary;
			Dictionary<int, List<AnimeParticleEventInfo>> dictionary2;
			if (Singleton<Manager.Resources>.Instance.Animation.MerchantOnlyParticleEventKeyTable.TryGetValue(eventID, out dictionary) && dictionary.TryGetValue(poseID, out dictionary2) && dictionary2 != null)
			{
				base.LoadEventParticle(dictionary2);
			}
		}

		// Token: 0x06006715 RID: 26389 RVA: 0x002BBEEC File Offset: 0x002BA2EC
		public void SetMerchantPoints(MerchantPoint[] merchantPoints)
		{
			this.StartPoint = null;
			this.ExitPoint = null;
			if (this.MerchantPoints == null)
			{
				this.MerchantPoints = new List<MerchantPoint>();
			}
			else
			{
				this.MerchantPoints.Clear();
			}
			if (this.MerchantPointTable == null)
			{
				this.MerchantPointTable = new Dictionary<int, Dictionary<int, List<MerchantPoint>>>();
			}
			else
			{
				this.MerchantPointTable.Clear();
			}
			if (merchantPoints.IsNullOrEmpty<MerchantPoint>())
			{
				return;
			}
			this.MerchantPoints.AddRange(merchantPoints);
			foreach (MerchantPoint merchantPoint in merchantPoints)
			{
				if (!this.MerchantPointTable.ContainsKey(merchantPoint.AreaID))
				{
					this.MerchantPointTable[merchantPoint.AreaID] = new Dictionary<int, List<MerchantPoint>>();
				}
				if (!this.MerchantPointTable[merchantPoint.AreaID].ContainsKey(merchantPoint.GroupID))
				{
					this.MerchantPointTable[merchantPoint.AreaID][merchantPoint.GroupID] = new List<MerchantPoint>();
				}
				this.MerchantPointTable[merchantPoint.AreaID][merchantPoint.GroupID].Add(merchantPoint);
				if (!(merchantPoint == null))
				{
					if (merchantPoint.IsStartPoint)
					{
						this.StartPoint = merchantPoint;
					}
					if (merchantPoint.IsExitPoint)
					{
						this.ExitPoint = merchantPoint;
					}
				}
			}
			if (this.ExitPoint != null && Singleton<Manager.Resources>.IsInstance())
			{
				int mapShipItemID = Singleton<Manager.Resources>.Instance.MerchantProfile.MapShipItemID;
				this.ShipObjects = MapItemData.Get(mapShipItemID);
				List<ForcedHideObject> list = new List<ForcedHideObject>();
				if (!this.ShipObjects.IsNullOrEmpty<GameObject>())
				{
					foreach (GameObject gameObject in this.ShipObjects)
					{
						if (!(gameObject == null))
						{
							ForcedHideObject orAddComponent = gameObject.GetOrAddComponent<ForcedHideObject>();
							if (!(orAddComponent == null))
							{
								if (!list.Contains(orAddComponent))
								{
									orAddComponent.Init();
									list.Add(orAddComponent);
								}
							}
						}
					}
				}
				this.ExitPoint.ItemObjects = list;
			}
		}

		// Token: 0x06006716 RID: 26390 RVA: 0x002BC140 File Offset: 0x002BA540
		public void ChangeBehavior(Merchant.ActionType mode)
		{
			if (mode != this.CurrentMode)
			{
				this.PrevMode = this.CurrentMode;
				this.CurrentMode = mode;
				if (Merchant.NormalModeList.Contains(this.CurrentMode))
				{
					this.LastNormalMode = this.CurrentMode;
				}
			}
			Merchant.StateType stateType;
			if (Merchant.StateTypeTable.TryGetValue(mode, out stateType))
			{
				this.MerchantData.StateType = stateType;
			}
			this.BehaviorResources.ChangeMode(mode);
		}

		// Token: 0x06006717 RID: 26391 RVA: 0x002BC1B8 File Offset: 0x002BA5B8
		private void OnEnable()
		{
			if (this.Animation != null && this._prevAnimEnable)
			{
				this.Animation.enabled = true;
			}
			if (this.Locomotor != null && this._prevLocomEnable)
			{
				this.Locomotor.enabled = true;
			}
			if (this.Controller != null && this._prevConEnable)
			{
				this.Controller.enabled = true;
			}
		}

		// Token: 0x06006718 RID: 26392 RVA: 0x002BC240 File Offset: 0x002BA640
		private void OnDisable()
		{
			if (this.Animation != null && (this._prevAnimEnable = this.Animation.enabled))
			{
				this.Animation.enabled = false;
			}
			if (this.Locomotor != null && (this._prevLocomEnable = this.Locomotor.enabled))
			{
				this.Locomotor.enabled = false;
			}
			if (this.Controller != null && (this._prevConEnable = this.Controller.enabled))
			{
				this.Controller.enabled = false;
			}
		}

		// Token: 0x06006719 RID: 26393 RVA: 0x002BC2F0 File Offset: 0x002BA6F0
		public void Show()
		{
			this.AnimationMerchant.enabled = true;
			this.LocomotorMerchant.enabled = true;
			this.MerchantController.enabled = true;
			base.ChaControl.visibleAll = true;
			this.AnimationMerchant.EnableItems();
			this.AnimationMerchant.EnableParticleRenderer();
		}

		// Token: 0x0600671A RID: 26394 RVA: 0x002BC344 File Offset: 0x002BA744
		public void Hide()
		{
			this.AnimationMerchant.enabled = false;
			this.LocomotorMerchant.enabled = false;
			this.MerchantController.enabled = false;
			base.ChaControl.visibleAll = false;
			this.AnimationMerchant.DisableItems();
			this.AnimationMerchant.DisableParticleRenderer();
		}

		// Token: 0x0600671B RID: 26395 RVA: 0x002BC397 File Offset: 0x002BA797
		public override void EnableEntity()
		{
			if (this.CurrentMode != Merchant.ActionType.Absent)
			{
				this.Show();
			}
			this.EnableBehavior();
		}

		// Token: 0x0600671C RID: 26396 RVA: 0x002BC3B1 File Offset: 0x002BA7B1
		public override void DisableEntity()
		{
			this.Hide();
			this.DisableBehavior();
		}

		// Token: 0x0600671D RID: 26397 RVA: 0x002BC3C0 File Offset: 0x002BA7C0
		public void ActivateLocomotionMotion()
		{
			int key = 0;
			this._locomotor.MovePoseID = 0;
			Dictionary<int, PlayState> merchantLocomotionStateTable = Singleton<Manager.Resources>.Instance.Animation.MerchantLocomotionStateTable;
			PlayState playState;
			merchantLocomotionStateTable.TryGetValue(key, out playState);
			if (playState != null)
			{
				this.Animation.StopAllAnimCoroutine();
				Animator animator = this.Animation.Animator;
				PlayState.Info[] stateInfos = playState.MainStateInfo.InStateInfo.StateInfos;
				if (!stateInfos.IsNullOrEmpty<PlayState.Info>() && !animator.GetCurrentAnimatorStateInfo(0).IsName(stateInfos.LastOrDefault<PlayState.Info>().stateName))
				{
					this.Animation.InitializeStates(playState);
					int layer = playState.Layer;
					bool enableFade = playState.MainStateInfo.InStateInfo.EnableFade;
					float fadeSecond = playState.MainStateInfo.InStateInfo.FadeSecond;
					this.Animation.PlayInLocoAnimation(enableFade, fadeSecond, layer);
				}
			}
			this.DestinationSpeed = this.GetLocomotionSpeed(this._locomotor.MovePoseID);
		}

		// Token: 0x0600671E RID: 26398 RVA: 0x002BC4C0 File Offset: 0x002BA8C0
		public void StartLocomotion(Vector3 destination)
		{
			float destinationSpeed = this.DestinationSpeed;
			float speed = base.NavMeshAgent.speed;
			this.ActivateNavMeshAgent();
			if (base.NavMeshAgent.isStopped)
			{
				base.NavMeshAgent.isStopped = false;
			}
			if (base.IsKinematic)
			{
				base.IsKinematic = false;
			}
			base.NavMeshAgent.SetDestination(destination);
			if (this._locomotionChangeSpeedDisposable != null)
			{
				this._locomotionChangeSpeedDisposable.Dispose();
			}
			this._locomotionChangeSpeedDisposable = ObservableEasing.Linear(1f, false).TakeUntilDestroy(base.gameObject).FrameTimeInterval(false).Subscribe(delegate(TimeInterval<float> x)
			{
				this.NavMeshAgent.speed = Mathf.Lerp(speed, destinationSpeed, x.Value);
			});
		}

		// Token: 0x0600671F RID: 26399 RVA: 0x002BC584 File Offset: 0x002BA984
		public float GetLocomotionSpeed(int movePoseID)
		{
			return Singleton<Manager.Resources>.Instance.LocomotionProfile.MerchantSpeed.walkSpeed;
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x002BC5AD File Offset: 0x002BA9AD
		public void DeactivateTransfer()
		{
			base.NavMeshAgent.isStopped = true;
			base.IsKinematic = true;
		}

		// Token: 0x06006721 RID: 26401 RVA: 0x002BC5C2 File Offset: 0x002BA9C2
		public new void ActivateNavMeshAgent()
		{
			if (base.NavMeshObstacle.enabled)
			{
				base.NavMeshObstacle.enabled = false;
			}
			if (!base.NavMeshAgent.enabled)
			{
				base.NavMeshAgent.enabled = true;
			}
		}

		// Token: 0x06006722 RID: 26402 RVA: 0x002BC5FC File Offset: 0x002BA9FC
		public new void DeactivateNavMeshAgent()
		{
			if (base.NavMeshAgent.enabled)
			{
				base.NavMeshAgent.enabled = false;
			}
		}

		// Token: 0x06006723 RID: 26403 RVA: 0x002BC61A File Offset: 0x002BAA1A
		public void ActivateNavMeshObstacle()
		{
			if (base.NavMeshAgent.enabled)
			{
				base.NavMeshAgent.enabled = false;
			}
			if (!base.NavMeshObstacle.enabled)
			{
				base.NavMeshObstacle.enabled = true;
			}
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x002BC654 File Offset: 0x002BAA54
		public void ActivateNavMeshObstacle(Vector3 position)
		{
			base.NavMeshObstacle.transform.position = position;
			if (base.NavMeshAgent.enabled)
			{
				base.NavMeshAgent.enabled = false;
			}
			if (!base.NavMeshObstacle.enabled)
			{
				base.NavMeshObstacle.enabled = true;
			}
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x002BC6AA File Offset: 0x002BAAAA
		public void DeactivateNavMeshObjstacle()
		{
			if (base.NavMeshObstacle.enabled)
			{
				base.NavMeshObstacle.enabled = false;
			}
		}

		// Token: 0x06006726 RID: 26406 RVA: 0x002BC6C8 File Offset: 0x002BAAC8
		public void DeactivateNavMeshElement()
		{
			if (base.NavMeshObstacle.enabled)
			{
				base.NavMeshObstacle.enabled = false;
			}
			if (base.NavMeshAgent.enabled)
			{
				base.NavMeshAgent.enabled = false;
			}
		}

		// Token: 0x06006727 RID: 26407 RVA: 0x002BC704 File Offset: 0x002BAB04
		public bool IsInvalidMoveDestination()
		{
			OffMeshLink offMeshLink = null;
			if (this._navMeshAgent.currentOffMeshLinkData.activated)
			{
				offMeshLink = this._navMeshAgent.currentOffMeshLinkData.offMeshLink;
			}
			if (offMeshLink == null && this._navMeshAgent.nextOffMeshLinkData.offMeshLink)
			{
				offMeshLink = this._navMeshAgent.nextOffMeshLinkData.offMeshLink;
			}
			if (offMeshLink == null)
			{
				return false;
			}
			ActionPoint component = offMeshLink.GetComponent<ActionPoint>();
			if (component == null)
			{
				return false;
			}
			if (!component.IsNeutralCommand)
			{
				return true;
			}
			List<ActionPoint> connectedActionPoints = component.ConnectedActionPoints;
			if (connectedActionPoints != null)
			{
				foreach (ActionPoint actionPoint in connectedActionPoints)
				{
					if (actionPoint != null && !actionPoint.IsNeutralCommand)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06006728 RID: 26408 RVA: 0x002BC82C File Offset: 0x002BAC2C
		public bool IsArrived()
		{
			NavMeshAgent navMeshAgent = base.NavMeshAgent;
			float approachDistanceActionPoint = Singleton<Manager.Resources>.Instance.LocomotionProfile.ApproachDistanceActionPoint;
			bool flag = false;
			if (navMeshAgent.isActiveAndEnabled && (navMeshAgent.hasPath || navMeshAgent.pathPending))
			{
				if (navMeshAgent.hasPath)
				{
					flag = (navMeshAgent.remainingDistance <= approachDistanceActionPoint);
					if (flag && this.TargetInSightMerchantPoint != null)
					{
						flag = (Vector3.Distance(base.Position, this.TargetInSightMerchantPoint.Destination) <= approachDistanceActionPoint);
					}
				}
			}
			else if (this.TargetInSightMerchantPoint != null)
			{
				flag = (Vector3.Distance(base.Position, this.TargetInSightMerchantPoint.Destination) <= approachDistanceActionPoint);
			}
			return flag;
		}

		// Token: 0x06006729 RID: 26409 RVA: 0x002BC8F4 File Offset: 0x002BACF4
		public bool IsArrivedToOffMesh()
		{
			OffMeshLink offMeshLink = base.NavMeshAgent.nextOffMeshLinkData.offMeshLink;
			if (offMeshLink == null)
			{
				return false;
			}
			Transform startTransform = offMeshLink.startTransform;
			if (startTransform == null)
			{
				return false;
			}
			Vector3 position = startTransform.position;
			float approachDistanceActionPoint = Singleton<Manager.Resources>.Instance.LocomotionProfile.ApproachDistanceActionPoint;
			float num = Vector3.Distance(base.Position, position);
			return num <= approachDistanceActionPoint;
		}

		// Token: 0x0600672A RID: 26410 RVA: 0x002BC968 File Offset: 0x002BAD68
		public void FirstAppear()
		{
			if (base.MapID != 0)
			{
				this.ChangeBehavior(this.CurrentMode);
			}
			else if (this.ScheduleList.IsNullOrEmpty<MerchantActor.MerchantSchedule>())
			{
				MerchantActor.MerchantSchedule item = new MerchantActor.MerchantSchedule
				{
					AbsenceDay = false,
					Event = Merchant.ScheduleTaskTable[Merchant.StateType.Wait]
				};
				if (this.StartPoint != null)
				{
					Tuple<MerchantPointInfo, Transform, Transform> eventInfo = this.StartPoint.GetEventInfo(Merchant.EventType.Wait);
					base.Position = eventInfo.Item2.position;
					base.Rotation = eventInfo.Item2.rotation;
				}
				this.ScheduleList.Clear();
				this.ScheduleList.Add(item);
				this.SetCurrentSchedule();
				MerchantPoint startPoint = this.StartPoint;
				this.TargetInSightMerchantPoint = startPoint;
				this.SetMerchantPoint(startPoint);
				this.ChangeBehavior(Merchant.ActionType.Wait);
			}
			else
			{
				if (0 <= this.MerchantData.PointAreaID && 0 <= this.MerchantData.PointGroupID)
				{
					int pointAreaID = this.MerchantData.PointAreaID;
					int pointGroupID = this.MerchantData.PointGroupID;
					Vector3 pointPosition = this.MerchantData.PointPosition;
					Vector3 mainPointPosition = this.MerchantData.MainPointPosition;
					List<MerchantPoint> list = ListPool<MerchantPoint>.Get();
					this.TargetInSightMerchantPoint = null;
					this.MainTargetInSightMerchantPoint = null;
					foreach (MerchantPoint merchantPoint in this.MerchantPoints)
					{
						if (!(merchantPoint == null))
						{
							if (this.TargetInSightMerchantPoint == null && this.SamePosition(merchantPoint.transform.position, pointPosition))
							{
								this.TargetInSightMerchantPoint = merchantPoint;
							}
							if (this.MainTargetInSightMerchantPoint == null && this.SamePosition(merchantPoint.transform.position, mainPointPosition))
							{
								this.MainTargetInSightMerchantPoint = merchantPoint;
							}
							if (merchantPoint.AreaID == pointAreaID)
							{
								if (merchantPoint.GroupID == pointGroupID)
								{
									list.Add(merchantPoint);
								}
							}
						}
					}
					if (this.TargetInSightMerchantPoint == null && !list.IsNullOrEmpty<MerchantPoint>())
					{
						this.TargetInSightMerchantPoint = list.GetElement(UnityEngine.Random.Range(0, list.Count));
					}
					ListPool<MerchantPoint>.Release(list);
				}
				bool elapsedDay = this.ElapsedDay;
				this.SetCurrentSchedule();
				this.ElapsedDay = elapsedDay;
			}
		}

		// Token: 0x0600672B RID: 26411 RVA: 0x002BCBF0 File Offset: 0x002BAFF0
		private bool SamePosition(Vector3 p0, Vector3 p1)
		{
			return Mathf.Approximately(p0.x, p1.x) && Mathf.Approximately(p0.y, p1.y) && Mathf.Approximately(p0.z, p1.z);
		}

		// Token: 0x0600672C RID: 26412 RVA: 0x002BCC44 File Offset: 0x002BB044
		public void SetMerchantPoint(MerchantPoint merchantPoint)
		{
			if (merchantPoint == null || merchantPoint == this.CurrentMerchantPoint)
			{
				return;
			}
			if (this.CurrentMerchantPoint != null)
			{
				this.PrevMerchantPoint = this.CurrentMerchantPoint;
			}
			this.CurrentMerchantPoint = merchantPoint;
			this.SetPointIDInfo(merchantPoint);
		}

		// Token: 0x0600672D RID: 26413 RVA: 0x002BCC9C File Offset: 0x002BB09C
		public void AddSchedule()
		{
			List<MerchantActor.MerchantSchedule> scheduleList = this.ScheduleList;
			int? num = (scheduleList != null) ? new int?(scheduleList.Count) : null;
			int num2 = (num == null) ? 0 : num.Value;
			bool flag = false;
			bool flag2 = false;
			int oneCycle = Singleton<Manager.Resources>.Instance.MerchantProfile.OneCycle;
			int num3;
			int num4;
			if (this.ScheduleList.IsNullOrEmpty<MerchantActor.MerchantSchedule>())
			{
				num3 = UnityEngine.Random.Range(2, oneCycle);
				num4 = num3 - 1;
			}
			else if (this.ScheduleList[num2 - 1].AbsenceDay)
			{
				num3 = UnityEngine.Random.Range(2, oneCycle);
				num4 = num3 - 1;
				flag2 = true;
			}
			else
			{
				num3 = UnityEngine.Random.Range(1, oneCycle);
				num4 = num3 - 1;
				MerchantActor.MerchantSchedule merchantSchedule = this.ScheduleList[num2 - 1];
				flag = merchantSchedule.IsSearch;
			}
			for (int i = 0; i < oneCycle; i++)
			{
				MerchantActor.MerchantSchedule merchantSchedule2 = new MerchantActor.MerchantSchedule
				{
					AbsenceDay = false,
					Event = null
				};
				if (num4 == i || num3 == i)
				{
					merchantSchedule2.Event = Merchant.ScheduleTaskTable[Merchant.StateType.Absent];
					merchantSchedule2.AbsenceDay = (num3 == i);
				}
				else if (flag || flag2)
				{
					merchantSchedule2.Event = Merchant.ScheduleTaskTable[Merchant.StateType.Wait];
				}
				else
				{
					float toSearchSelectedRange = Singleton<Manager.Resources>.Instance.MerchantProfile.ToSearchSelectedRange;
					float num5 = UnityEngine.Random.Range(0f, 100f);
					Merchant.StateType key;
					if (Mathf.Approximately(toSearchSelectedRange, 0f))
					{
						key = Merchant.StateType.Wait;
					}
					else if (Mathf.Approximately(toSearchSelectedRange, 100f))
					{
						key = Merchant.StateType.Search;
					}
					else
					{
						key = ((num5 > toSearchSelectedRange) ? Merchant.StateType.Wait : Merchant.StateType.Search);
					}
					merchantSchedule2.Event = Merchant.ScheduleTaskTable[key];
				}
				flag = merchantSchedule2.IsSearch;
				flag2 = merchantSchedule2.AbsenceDay;
				this.ScheduleList.Add(merchantSchedule2);
			}
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x002BCE98 File Offset: 0x002BB298
		public void ChangeNextSchedule()
		{
			int oneCycle = Singleton<Manager.Resources>.Instance.MerchantProfile.OneCycle;
			if (this.ScheduleList.Count < oneCycle)
			{
				this.AddSchedule();
			}
			if (this.CurrentSchedule != null)
			{
				this.PrevSchedule = this.CurrentSchedule;
			}
			this.CurrentSchedule = this.ScheduleList.Pop<MerchantActor.MerchantSchedule>();
			if (this.ScheduleList.Count < oneCycle)
			{
				this.AddSchedule();
			}
		}

		// Token: 0x0600672F RID: 26415 RVA: 0x002BCF0C File Offset: 0x002BB30C
		public void SetCurrentSchedule()
		{
			int oneCycle = Singleton<Manager.Resources>.Instance.MerchantProfile.OneCycle;
			if (this.ScheduleList.Count < oneCycle)
			{
				this.AddSchedule();
			}
			if (this.CurrentSchedule == null)
			{
				this.CurrentSchedule = this.ScheduleList.Pop<MerchantActor.MerchantSchedule>();
			}
			if (this.ScheduleList.Count < oneCycle)
			{
				this.AddSchedule();
			}
			this.ElapsedDay = false;
			if (this.CurrentSchedule.AbsenceDay)
			{
				if (!this.BehaviorResources.IsMatchCurrentTree(Merchant.ActionType.Absent))
				{
					this.ChangeBehavior(Merchant.ActionType.Absent);
				}
			}
			else
			{
				MerchantActor.MerchantSchedule prevSchedule = this.PrevSchedule;
				bool? flag = (prevSchedule != null) ? new bool?(prevSchedule.AbsenceDay) : null;
				if (flag != null && flag.Value)
				{
					this.ChangeBehavior(this.CurrentSchedule.Event.Purpose);
				}
				else
				{
					this.ChangeBehavior(this.CurrentSchedule.Event.Process);
				}
			}
		}

		// Token: 0x06006730 RID: 26416 RVA: 0x002BD01C File Offset: 0x002BB41C
		public void CrossFade()
		{
			Renderer componentInChildren = base.ChaControl.objBody.GetComponentInChildren<Renderer>();
			Renderer componentInChildren2 = base.ChaControl.objHead.GetComponentInChildren<Renderer>();
			bool flag = componentInChildren != null && componentInChildren.isVisible;
			flag |= (componentInChildren2 != null && componentInChildren2.isVisible);
			if (flag)
			{
				ActorCameraControl cameraControl = Singleton<Manager.Map>.Instance.Player.CameraControl;
				float num = Vector3.Distance(base.Position, cameraControl.transform.position);
				float crossFadeEnableDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.CrossFadeEnableDistance;
				if (num <= crossFadeEnableDistance)
				{
					cameraControl.CrossFade.FadeStart(-1f);
				}
			}
		}

		// Token: 0x06006731 RID: 26417 RVA: 0x002BD0D4 File Offset: 0x002BB4D4
		private void StartCommunication()
		{
			this.packData.Init();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.CommCompanion = this;
			player.Controller.ChangeState("Communication");
			this.ChangeBehavior(Merchant.ActionType.TalkWithPlayer);
			this.TurnToActorCrossFade(player);
			base.ChaControl.ChangeLookEyesTarget(0, null, 0.5f, 0f, 1f, 2f);
			base.ChaControl.ChangeLookNeckTarget(0, null, 0.5f, 0f, 1f, 0.8f);
			base.ChaControl.ChangeLookEyesPtn(1);
			base.ChaControl.ChangeLookNeckPtn(1, 1f);
			Observable.TimerFrame(1, FrameCountType.Update).Subscribe(delegate(long _)
			{
				ADV.ChangeADVCamera(this);
			});
			MapUIContainer.SetVisibleHUD(false);
			Singleton<ADV>.Instance.TargetMerchant = this;
			this.PopupCommands();
		}

		// Token: 0x06006732 RID: 26418 RVA: 0x002BD1AC File Offset: 0x002BB5AC
		private void TurnToActorCrossFade(Actor actor)
		{
			this.DisposeSequenceAction();
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			ActorCameraControl cameraControl = player.CameraControl;
			cameraControl.CrossFade.FadeStart(-1f);
			this.Animation.StopAllAnimCoroutine();
			this.Animation.Animator.Play(Singleton<Manager.Resources>.Instance.DefinePack.AnimatorState.MerchantIdleState, 0);
			Vector3 forward = actor.Position - base.Position;
			forward.y = 0f;
			base.Rotation = Quaternion.LookRotation(forward, Vector3.up);
		}

		// Token: 0x06006733 RID: 26419 RVA: 0x002BD240 File Offset: 0x002BB640
		private void EndCommunication()
		{
			MapUIContainer.SetVisibledSpendMoneyUI(false);
			MapUIContainer.SetActiveCommandList(false);
			this.VanishCommands();
			this.packData.Release();
		}

		// Token: 0x06006734 RID: 26420 RVA: 0x002BD260 File Offset: 0x002BB660
		private void LeaveADV()
		{
			MapUIContainer.SetVisibledSpendMoneyUI(false);
			this.openData.FindLoad("1", this.charaID, 0);
			this.packData.onComplete = delegate()
			{
				this.EndCommunication();
			};
			this.packData.OpenAreaID = this.OpenAreaID;
			this.packData.restoreCommands = null;
			Singleton<MapUIContainer>.Instance.OpenADV(this.openData, this.packData);
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x002BD2D4 File Offset: 0x002BB6D4
		public void PopupCommands()
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				this.LeaveADV();
			};
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x002BD301 File Offset: 0x002BB701
		public void OpenShopUI()
		{
			this.PopupCommands();
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x002BD309 File Offset: 0x002BB709
		public void InitiateHScene()
		{
			this.PopupCommands();
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x002BD311 File Offset: 0x002BB711
		private void HSceneEnter(int hMode)
		{
			this.ChangeBehavior(Merchant.ActionType.HWithPlayer);
			Singleton<HSceneManager>.Instance.HsceneEnter(this, hMode, null, HSceneManager.HEvent.Normal);
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x002BD328 File Offset: 0x002BB728
		public void VanishCommands()
		{
			Singleton<ADV>.Instance.Captions.EndADV(new System.Action(this.EnableBehavior));
			if (this.TargetInSightMerchantPoint == null)
			{
				this.TargetInSightMerchantPoint = this.PrevMerchantPoint;
			}
			MapUIContainer.SetVisibleHUD(true);
			PlayerActor player = Singleton<Manager.Map>.Instance.Player;
			player.CameraControl.Mode = CameraMode.Normal;
			player.Controller.ChangeState("Normal");
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			instance.ReserveState(Manager.Input.ValidType.Action);
			instance.SetupState();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			player.ChaControl.visibleAll = true;
			base.ChaControl.ChangeLookEyesPtn(0);
			base.ChaControl.ChangeLookNeckPtn(3, 1f);
			this.ChangeBehavior(this.LastNormalMode);
		}

		// Token: 0x0600673A RID: 26426 RVA: 0x002BD3F5 File Offset: 0x002BB7F5
		public bool CanSelectHCommand(string[] tagName)
		{
			return Game.isAdd01 && this.IsOnHMesh(tagName);
		}

		// Token: 0x0600673B RID: 26427 RVA: 0x002BD40C File Offset: 0x002BB80C
		private bool IsOnHMesh(string[] tagName = null)
		{
			if (tagName.IsNullOrEmpty<string>())
			{
				return false;
			}
			LayerMask hlayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.HLayer;
			Vector3 position = base.Position;
			position.y += 15f;
			int num = Physics.SphereCastNonAlloc(position, 7.5f, Vector3.down, this._raycastHits, 25f, hlayer);
			num = Mathf.Min(num, this._raycastHits.Length);
			if (num == 0)
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				RaycastHit raycastHit = this._raycastHits[i];
				string tag = raycastHit.collider.tag;
				if (!tag.IsNullOrEmpty() && !(tag == "Untagged"))
				{
					foreach (string a in tagName)
					{
						if (flag = (a == tag))
						{
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x040058AD RID: 22701
		public UnityEx.ValueTuple<CompositeDisposable, System.Action> SequenceAction = new UnityEx.ValueTuple<CompositeDisposable, System.Action>(null, null);

		// Token: 0x040058B1 RID: 22705
		public Dictionary<int, Dictionary<int, List<MerchantPoint>>> MerchantPointTable = new Dictionary<int, Dictionary<int, List<MerchantPoint>>>();

		// Token: 0x040058B4 RID: 22708
		private NavMeshPath pathForCalc;

		// Token: 0x040058B5 RID: 22709
		private CommandLabel.CommandInfo[] _emptyLabel = new CommandLabel.CommandInfo[0];

		// Token: 0x040058B6 RID: 22710
		private CommandLabel.CommandInfo[] _talkLabel;

		// Token: 0x040058B7 RID: 22711
		[SerializeField]
		private ObjectLayer _layer = ObjectLayer.Command;

		// Token: 0x040058B8 RID: 22712
		[SerializeField]
		private ActorAnimationMerchant _animation;

		// Token: 0x040058B9 RID: 22713
		[SerializeField]
		private ActorLocomotionMerchant _locomotor;

		// Token: 0x040058BA RID: 22714
		[SerializeField]
		private MerchantController _controller;

		// Token: 0x040058C0 RID: 22720
		private float _distanceTweenPlayer = float.MaxValue;

		// Token: 0x040058C1 RID: 22721
		private float _heightDistTweenPlayer = float.MaxValue;

		// Token: 0x040058C2 RID: 22722
		private bool _prevCloseToPlayer;

		// Token: 0x040058C3 RID: 22723
		private bool _prevAnimEnable = true;

		// Token: 0x040058C4 RID: 22724
		private bool _prevLocomEnable = true;

		// Token: 0x040058C5 RID: 22725
		private bool _prevConEnable = true;

		// Token: 0x040058C6 RID: 22726
		private IDisposable _locomotionChangeSpeedDisposable;

		// Token: 0x040058C7 RID: 22727
		private CommCommandList.CommandInfo[] _normalCommandOptionInfo;

		// Token: 0x040058C8 RID: 22728
		private RaycastHit[] _raycastHits = new RaycastHit[10];

		// Token: 0x02000C6A RID: 3178
		public enum ADV_CATEGORY
		{
			// Token: 0x040058CB RID: 22731
			TALK,
			// Token: 0x040058CC RID: 22732
			EVENT,
			// Token: 0x040058CD RID: 22733
			H = 9,
			// Token: 0x040058CE RID: 22734
			HScene,
			// Token: 0x040058CF RID: 22735
			TUTORIAL = 100
		}

		// Token: 0x02000C6B RID: 3179
		private class PackData : CharaPackData
		{
			// Token: 0x170014B9 RID: 5305
			// (get) Token: 0x06006743 RID: 26435 RVA: 0x002BD57A File Offset: 0x002BB97A
			// (set) Token: 0x06006744 RID: 26436 RVA: 0x002BD582 File Offset: 0x002BB982
			public CommCommandList.CommandInfo[] restoreCommands { get; set; }

			// Token: 0x170014BA RID: 5306
			// (get) Token: 0x06006745 RID: 26437 RVA: 0x002BD58B File Offset: 0x002BB98B
			public bool isHSuccess
			{
				get
				{
					return this.hMode > 0;
				}
			}

			// Token: 0x170014BB RID: 5307
			// (get) Token: 0x06006746 RID: 26438 RVA: 0x002BD598 File Offset: 0x002BB998
			public int hMode
			{
				get
				{
					ValData valData;
					if (base.Vars == null || !base.Vars.TryGetValue("hMode", out valData))
					{
						return 0;
					}
					return (int)valData.o;
				}
			}

			// Token: 0x170014BC RID: 5308
			// (get) Token: 0x06006747 RID: 26439 RVA: 0x002BD5D4 File Offset: 0x002BB9D4
			// (set) Token: 0x06006748 RID: 26440 RVA: 0x002BD5DC File Offset: 0x002BB9DC
			public string JumpTag { get; set; } = string.Empty;

			// Token: 0x170014BD RID: 5309
			// (get) Token: 0x06006749 RID: 26441 RVA: 0x002BD5E5 File Offset: 0x002BB9E5
			// (set) Token: 0x0600674A RID: 26442 RVA: 0x002BD5ED File Offset: 0x002BB9ED
			public int OpenAreaID { get; set; }

			// Token: 0x170014BE RID: 5310
			// (get) Token: 0x0600674B RID: 26443 RVA: 0x002BD5F8 File Offset: 0x002BB9F8
			public bool isClearFlag
			{
				get
				{
					if (!Singleton<Game>.IsInstance())
					{
						return false;
					}
					WorldData worldData = Singleton<Game>.Instance.WorldData;
					bool? flag = (worldData != null) ? new bool?(worldData.Cleared) : null;
					return flag != null && flag.Value;
				}
			}

			// Token: 0x170014BF RID: 5311
			// (get) Token: 0x0600674C RID: 26444 RVA: 0x002BD651 File Offset: 0x002BBA51
			// (set) Token: 0x0600674D RID: 26445 RVA: 0x002BD659 File Offset: 0x002BBA59
			public bool isLesbian { get; set; }

			// Token: 0x170014C0 RID: 5312
			// (get) Token: 0x0600674E RID: 26446 RVA: 0x002BD662 File Offset: 0x002BBA62
			// (set) Token: 0x0600674F RID: 26447 RVA: 0x002BD66A File Offset: 0x002BBA6A
			public bool isWeaknessH { get; set; }

			// Token: 0x06006750 RID: 26448 RVA: 0x002BD674 File Offset: 0x002BBA74
			public override List<Program.Transfer> Create()
			{
				List<Program.Transfer> list = base.Create();
				list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
				{
					"int",
					"OpenAreaID",
					string.Format("{0}", this.OpenAreaID)
				}));
				list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
				{
					"bool",
					"isClearFlag",
					string.Format("{0}", this.isClearFlag)
				}));
				list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
				{
					"bool",
					"isLesbian",
					string.Format("{0}", this.isLesbian)
				}));
				list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
				{
					"bool",
					"isWeaknessH",
					string.Format("{0}", this.isWeaknessH)
				}));
				list.Add(Program.Transfer.Create(true, Command.VAR, new string[]
				{
					"string",
					"JumpTag",
					this.JumpTag
				}));
				return list;
			}

			// Token: 0x06006751 RID: 26449 RVA: 0x002BD7A4 File Offset: 0x002BBBA4
			public override void Receive(TextScenario scenario)
			{
				base.Receive(scenario);
				if (this.restoreCommands != null)
				{
					CommCommandList commandList = MapUIContainer.CommandList;
					commandList.Refresh(this.restoreCommands, commandList.CanvasGroup, null);
				}
			}
		}

		// Token: 0x02000C6C RID: 3180
		[MessagePackObject(false)]
		public class MerchantSchedule
		{
			// Token: 0x06006752 RID: 26450 RVA: 0x002BD7DC File Offset: 0x002BBBDC
			public MerchantSchedule()
			{
			}

			// Token: 0x06006753 RID: 26451 RVA: 0x002BD7E4 File Offset: 0x002BBBE4
			public MerchantSchedule(MerchantActor.MerchantSchedule _source)
			{
				if (_source == null)
				{
					return;
				}
				this.AbsenceDay = _source.AbsenceDay;
				this.SetEvent(_source.Event);
			}

			// Token: 0x170014C1 RID: 5313
			// (get) Token: 0x06006754 RID: 26452 RVA: 0x002BD80B File Offset: 0x002BBC0B
			// (set) Token: 0x06006755 RID: 26453 RVA: 0x002BD813 File Offset: 0x002BBC13
			[Key(0)]
			public bool AbsenceDay { get; set; }

			// Token: 0x06006756 RID: 26454 RVA: 0x002BD81C File Offset: 0x002BBC1C
			public bool IsMatchState(Merchant.StateType stateType)
			{
				Merchant.StateType stateType2;
				return !this.AbsenceDay && this.Event != null && Merchant.StateTypeTable.TryGetValue(this.Event.Process, out stateType2) && stateType2 == stateType;
			}

			// Token: 0x170014C2 RID: 5314
			// (get) Token: 0x06006757 RID: 26455 RVA: 0x002BD867 File Offset: 0x002BBC67
			[IgnoreMember]
			public bool IsAbsent
			{
				[CompilerGenerated]
				get
				{
					return this.AbsenceDay || this.IsMatchState(Merchant.StateType.Absent);
				}
			}

			// Token: 0x170014C3 RID: 5315
			// (get) Token: 0x06006758 RID: 26456 RVA: 0x002BD87E File Offset: 0x002BBC7E
			[IgnoreMember]
			public bool IsSearch
			{
				[CompilerGenerated]
				get
				{
					return this.IsMatchState(Merchant.StateType.Search);
				}
			}

			// Token: 0x170014C4 RID: 5316
			// (get) Token: 0x06006759 RID: 26457 RVA: 0x002BD887 File Offset: 0x002BBC87
			[IgnoreMember]
			public bool IsWait
			{
				[CompilerGenerated]
				get
				{
					return this.IsMatchState(Merchant.StateType.Wait);
				}
			}

			// Token: 0x0600675A RID: 26458 RVA: 0x002BD890 File Offset: 0x002BBC90
			public void SetEvent(MerchantActor.MerchantSchedule.MerchantEvent @event)
			{
				if (@event == null)
				{
					return;
				}
				if (this.Event == null)
				{
					this.Event = new MerchantActor.MerchantSchedule.MerchantEvent(@event.Process, @event.Purpose);
				}
				else
				{
					this.Event.SetEvent(@event);
				}
			}

			// Token: 0x040058D6 RID: 22742
			[Key(1)]
			public MerchantActor.MerchantSchedule.MerchantEvent Event;

			// Token: 0x02000C6D RID: 3181
			[MessagePackObject(false)]
			public class MerchantEvent
			{
				// Token: 0x0600675B RID: 26459 RVA: 0x002BD8CC File Offset: 0x002BBCCC
				public MerchantEvent(MerchantActor.MerchantSchedule.MerchantEvent @event)
				{
					this.SetEvent(@event);
				}

				// Token: 0x0600675C RID: 26460 RVA: 0x002BD8DB File Offset: 0x002BBCDB
				public MerchantEvent(Tuple<Merchant.ActionType, Merchant.ActionType> @event)
				{
					this.SetEvent(@event);
				}

				// Token: 0x0600675D RID: 26461 RVA: 0x002BD8EA File Offset: 0x002BBCEA
				public MerchantEvent(Merchant.ActionType process, Merchant.ActionType purpose)
				{
					this.SetEvent(process, purpose);
				}

				// Token: 0x170014C5 RID: 5317
				// (get) Token: 0x0600675E RID: 26462 RVA: 0x002BD8FA File Offset: 0x002BBCFA
				// (set) Token: 0x0600675F RID: 26463 RVA: 0x002BD902 File Offset: 0x002BBD02
				[Key(0)]
				public Merchant.ActionType Process { get; set; }

				// Token: 0x170014C6 RID: 5318
				// (get) Token: 0x06006760 RID: 26464 RVA: 0x002BD90B File Offset: 0x002BBD0B
				// (set) Token: 0x06006761 RID: 26465 RVA: 0x002BD913 File Offset: 0x002BBD13
				[Key(1)]
				public Merchant.ActionType Purpose { get; set; }

				// Token: 0x06006762 RID: 26466 RVA: 0x002BD91C File Offset: 0x002BBD1C
				public void SetEvent(Tuple<Merchant.ActionType, Merchant.ActionType> @event)
				{
					this.Process = @event.Item1;
					this.Purpose = @event.Item2;
				}

				// Token: 0x06006763 RID: 26467 RVA: 0x002BD936 File Offset: 0x002BBD36
				public void SetEvent(Merchant.ActionType process, Merchant.ActionType purpose)
				{
					this.Process = process;
					this.Purpose = purpose;
				}

				// Token: 0x06006764 RID: 26468 RVA: 0x002BD946 File Offset: 0x002BBD46
				public void SetEvent(MerchantActor.MerchantSchedule.MerchantEvent @event)
				{
					this.Process = @event.Process;
					this.Purpose = @event.Purpose;
				}
			}
		}
	}
}
