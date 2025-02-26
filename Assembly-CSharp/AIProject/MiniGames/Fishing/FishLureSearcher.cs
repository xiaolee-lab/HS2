using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F3C RID: 3900
	public class FishLureSearcher : MonoBehaviour
	{
		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x060080EC RID: 33004 RVA: 0x0036B3BF File Offset: 0x003697BF
		private FishingDefinePack.FishParamGroup FishParam
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.FishParam;
			}
		}

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x060080ED RID: 33005 RVA: 0x0036B3D0 File Offset: 0x003697D0
		private FishingDefinePack.FishHitParamGroup HitParam
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.FishingDefinePack.FishParam.HitParam;
			}
		}

		// Token: 0x170019E0 RID: 6624
		// (get) Token: 0x060080EE RID: 33006 RVA: 0x0036B3E6 File Offset: 0x003697E6
		// (set) Token: 0x060080EF RID: 33007 RVA: 0x0036B3EE File Offset: 0x003697EE
		public float FollowPercentage { get; set; }

		// Token: 0x060080F0 RID: 33008 RVA: 0x0036B3F8 File Offset: 0x003697F8
		private void Awake()
		{
			if (!this.fish)
			{
				this.fish = base.GetComponent<Fish>();
			}
			if (!this.fish)
			{
				this.fish = base.GetComponentInChildren<Fish>();
			}
			if (!this.fish)
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		// Token: 0x170019E1 RID: 6625
		// (get) Token: 0x060080F1 RID: 33009 RVA: 0x0036B453 File Offset: 0x00369853
		public bool OnSearch
		{
			[CompilerGenerated]
			get
			{
				return !(this.fish == null) && this.fish.OnSearch;
			}
		}

		// Token: 0x060080F2 RID: 33010 RVA: 0x0036B477 File Offset: 0x00369877
		private void OnEnable()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDisable(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				if (this.OnSearch)
				{
					this.OnUpdate();
				}
				else
				{
					this.collisionState = CollisionState.None;
				}
			});
		}

		// Token: 0x060080F3 RID: 33011 RVA: 0x0036B4AC File Offset: 0x003698AC
		public void ResetFollowPercentage()
		{
			this.FollowPercentage = 0f;
			FishFoodInfo selectedFishFood = MapUIContainer.FishingUI.SelectedFishFood;
			if (selectedFishFood == null)
			{
				return;
			}
			FishInfo fishInfo = this.fish.fishInfo;
			StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(fishInfo.CategoryID, fishInfo.ItemID);
			if (item == null)
			{
				return;
			}
			Dictionary<int, Dictionary<int, float>> fishHitBaseRangeTable = Singleton<Manager.Resources>.Instance.Fishing.FishHitBaseRangeTable;
			int grade = (int)item.Grade;
			Dictionary<int, float> dictionary;
			float num;
			if (!fishHitBaseRangeTable.TryGetValue(fishInfo.SizeID, out dictionary) || !dictionary.TryGetValue(grade, out num))
			{
				return;
			}
			float num2 = (float)selectedFishFood.RarelityHitRange.GetElement(grade);
			this.FollowPercentage = Mathf.Clamp(num + num2, 0f, 100f);
		}

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x060080F4 RID: 33012 RVA: 0x0036B56E File Offset: 0x0036996E
		private float RandomPercentage
		{
			[CompilerGenerated]
			get
			{
				return UnityEngine.Random.Range(0f, 100f);
			}
		}

		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x060080F5 RID: 33013 RVA: 0x0036B57F File Offset: 0x0036997F
		private bool FollowEnter
		{
			[CompilerGenerated]
			get
			{
				return this.RandomPercentage <= this.FollowPercentage;
			}
		}

		// Token: 0x060080F6 RID: 33014 RVA: 0x0036B592 File Offset: 0x00369992
		private void ChangeFollowState()
		{
			this.collisionState = CollisionState.None;
			this.reFindCounter = 0f;
			this.fish.ChangeState(Fish.State.FollowLure);
		}

		// Token: 0x060080F7 RID: 33015 RVA: 0x0036B5B4 File Offset: 0x003699B4
		private void OnUpdate()
		{
			float num = (this.collisionState != CollisionState.Enter && this.collisionState != CollisionState.Stay) ? 1f : 1.05f;
			float worldDistanceFishEyeToLure = this.fish.GetWorldDistanceFishEyeToLure();
			bool flag = this.fish.CheckLureInAngleFindArea();
			bool flag2 = worldDistanceFishEyeToLure <= this.FishParam.FindDistance * num;
			bool flag3 = flag && flag2;
			if (flag3)
			{
				switch (this.collisionState)
				{
				case CollisionState.None:
				case CollisionState.Exit:
					if (this.FollowEnter)
					{
						this.ChangeFollowState();
						return;
					}
					this.collisionState = CollisionState.Enter;
					this.reFindCounter = 0f;
					break;
				case CollisionState.Enter:
				case CollisionState.Stay:
					this.collisionState = CollisionState.Stay;
					if (this.fish.state != Fish.State.FollowLure)
					{
						if (this.FishParam.ReFindTime <= this.reFindCounter)
						{
							if (this.FollowEnter)
							{
								this.ChangeFollowState();
								return;
							}
							this.reFindCounter = 0f;
						}
						else
						{
							this.reFindCounter += Time.deltaTime;
						}
					}
					break;
				}
			}
			else
			{
				switch (this.collisionState)
				{
				case CollisionState.None:
				case CollisionState.Exit:
					this.collisionState = CollisionState.None;
					break;
				case CollisionState.Enter:
				case CollisionState.Stay:
					this.collisionState = CollisionState.Exit;
					this.reFindCounter = 0f;
					if (this.fish.state == Fish.State.FollowLure)
					{
						this.fish.SetWaitOrSwim();
					}
					break;
				}
			}
		}

		// Token: 0x040067AA RID: 26538
		[SerializeField]
		private Fish fish;

		// Token: 0x040067AB RID: 26539
		private CollisionState collisionState;

		// Token: 0x040067AC RID: 26540
		private float reFindCounter;

		// Token: 0x040067AD RID: 26541
		private const float MaxRange = 100f;
	}
}
