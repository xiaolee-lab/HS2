using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.MiniGames.Fishing;
using AIProject.SaveData;
using Manager;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BB1 RID: 2993
	public class PetFish : AnimalBase, IPetAnimal, INicknameObject
	{
		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x060059F0 RID: 23024 RVA: 0x002685BE File Offset: 0x002669BE
		// (set) Token: 0x060059F1 RID: 23025 RVA: 0x002685C6 File Offset: 0x002669C6
		public AnimalData AnimalData { get; set; }

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x060059F2 RID: 23026 RVA: 0x002685CF File Offset: 0x002669CF
		// (set) Token: 0x060059F3 RID: 23027 RVA: 0x002685F3 File Offset: 0x002669F3
		public string Nickname
		{
			get
			{
				AnimalData animalData = this.AnimalData;
				return ((animalData != null) ? animalData.Nickname : null) ?? base.Name;
			}
			set
			{
				if (this.AnimalData != null)
				{
					this.AnimalData.Nickname = value;
				}
				Action changeNickNameEvent = this.ChangeNickNameEvent;
				if (changeNickNameEvent != null)
				{
					changeNickNameEvent();
				}
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x060059F4 RID: 23028 RVA: 0x00268620 File Offset: 0x00266A20
		// (set) Token: 0x060059F5 RID: 23029 RVA: 0x00268628 File Offset: 0x00266A28
		public PetHomePoint HomePoint { get; private set; }

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x060059F6 RID: 23030 RVA: 0x00268634 File Offset: 0x00266A34
		public override ItemIDKeyPair ItemID
		{
			[CompilerGenerated]
			get
			{
				return (this.AnimalData == null) ? base.ItemID : new ItemIDKeyPair
				{
					categoryID = this.AnimalData.ItemCategoryID,
					itemID = this.AnimalData.ItemID
				};
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x060059F7 RID: 23031 RVA: 0x00268684 File Offset: 0x00266A84
		public override bool WaitPossible
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x060059F8 RID: 23032 RVA: 0x00268687 File Offset: 0x00266A87
		public Transform NicknameRoot
		{
			[CompilerGenerated]
			get
			{
				return this._nicknameRoot;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x060059F9 RID: 23033 RVA: 0x0026868F File Offset: 0x00266A8F
		// (set) Token: 0x060059FA RID: 23034 RVA: 0x002686A5 File Offset: 0x00266AA5
		public bool NicknameEnabled
		{
			get
			{
				return this._nicknameEnabled && this.BodyEnabled;
			}
			set
			{
				this._nicknameEnabled = value;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x060059FB RID: 23035 RVA: 0x002686AE File Offset: 0x00266AAE
		// (set) Token: 0x060059FC RID: 23036 RVA: 0x002686B6 File Offset: 0x00266AB6
		public Action ChangeNickNameEvent { get; set; }

		// Token: 0x060059FD RID: 23037 RVA: 0x002686BF File Offset: 0x00266ABF
		protected override void Awake()
		{
			base.Awake();
		}

		// Token: 0x060059FE RID: 23038 RVA: 0x002686C8 File Offset: 0x00266AC8
		protected override void OnUpdate()
		{
			base.OnUpdate();
			if (base.FollowTarget != null)
			{
				this.Position = base.FollowTarget.position;
				base.Rotation = base.FollowTarget.rotation;
			}
			if (this.AnimalData != null)
			{
				this.AnimalData.Position = this.Position;
				this.AnimalData.Rotation = base.Rotation;
			}
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x0026873C File Offset: 0x00266B3C
		public void Initialize(PetHomePoint _homePoint)
		{
			this.Clear();
			this.HomePoint = _homePoint;
			if (_homePoint == null)
			{
				return;
			}
			if (this.AnimalData == null)
			{
				return;
			}
			Manager.Resources.FishingTable fishingTable = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.Fishing;
			if (fishingTable == null)
			{
				return;
			}
			Dictionary<int, Dictionary<int, FishInfo>> fishInfoTable = fishingTable.FishInfoTable;
			Dictionary<int, Tuple<GameObject, RuntimeAnimatorController>> fishModelTable = fishingTable.FishModelTable;
			Dictionary<int, FishInfo> dictionary;
			FishInfo fishInfo;
			Tuple<GameObject, RuntimeAnimatorController> tuple;
			if (!fishInfoTable.IsNullOrEmpty<int, Dictionary<int, FishInfo>>() && !fishModelTable.IsNullOrEmpty<int, Tuple<GameObject, RuntimeAnimatorController>>() && fishInfoTable.TryGetValue(this.AnimalData.ItemCategoryID, out dictionary) && !dictionary.IsNullOrEmpty<int, FishInfo>() && dictionary.TryGetValue(this.AnimalData.ItemID, out fishInfo) && fishModelTable.TryGetValue(fishInfo.ModelID, out tuple))
			{
				base.LoadBody(tuple.Item1);
				if (this.animator != null)
				{
					this.animator.runtimeAnimatorController = tuple.Item2;
				}
				_homePoint.SetRootPoint(fishInfo.TankPointID, this);
				base.FollowTarget = _homePoint.GetRootPoint(fishInfo.TankPointID);
				if (this.bodyObject != null)
				{
					this._nicknameRoot = new GameObject("Nickname Root").transform;
					this._nicknameRoot.SetParent(this.bodyObject.transform, false);
					Renderer componentInChildren = this.bodyObject.GetComponentInChildren<Renderer>(true);
					if (componentInChildren != null)
					{
						Vector3 position = this.Position;
						Quaternion rotation = base.Rotation;
						this.Position = Vector3.zero;
						base.Rotation = Quaternion.identity;
						Vector3 center = componentInChildren.bounds.center;
						Vector3 extents = componentInChildren.bounds.extents;
						this._nicknameRoot.localPosition = new Vector3(0f, extents.y / 2f + center.y + this._nicknameHeightOffset, center.z);
						this.Position = position;
						base.Rotation = rotation;
					}
					else
					{
						this._nicknameRoot.localPosition = new Vector3(0f, this._nicknameHeightOffset, 0f);
					}
				}
			}
			if (this._nicknameRoot == null)
			{
				this._nicknameRoot = new GameObject("Nickname Root").transform;
				this._nicknameRoot.SetParent(base.transform, false);
				this._nicknameRoot.localPosition = new Vector3(0f, this._nicknameHeightOffset, 0f);
			}
			if (base.FollowTarget == null)
			{
				base.FollowTarget = this.HomePoint.transform;
			}
			base.SetStateData();
			bool flag = false;
			base.MarkerEnabled = flag;
			this.BodyEnabled = flag;
			this.SetState(AnimalState.Start, null);
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x00268A04 File Offset: 0x00266E04
		public void Initialize(AnimalData _animalData)
		{
			this.AnimalData = _animalData;
			if (_animalData == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x00268A2A File Offset: 0x00266E2A
		public void Release()
		{
			this.SetState(AnimalState.Destroyed, null);
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x00268A35 File Offset: 0x00266E35
		protected override void OnDestroy()
		{
			this.Active = false;
			base.OnDestroy();
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06005A03 RID: 23043 RVA: 0x00268A44 File Offset: 0x00266E44
		public override bool IsNeutralCommand
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06005A04 RID: 23044 RVA: 0x00268A47 File Offset: 0x00266E47
		public override CommandLabel.CommandInfo[] Labels
		{
			[CompilerGenerated]
			get
			{
				return AnimalBase.emptyLabels;
			}
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x00268A4E File Offset: 0x00266E4E
		protected override void EnterStart()
		{
			this.ChangeState(AnimalState.Idle, null);
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x00268A58 File Offset: 0x00266E58
		protected override void ExitStart()
		{
			bool flag = true;
			base.MarkerEnabled = flag;
			this.BodyEnabled = flag;
			this.Active = true;
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x00268A7C File Offset: 0x00266E7C
		protected override void EnterIdle()
		{
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x00268A7E File Offset: 0x00266E7E
		protected override void OnIdle()
		{
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x00268A80 File Offset: 0x00266E80
		protected override void ExitIdle()
		{
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x00268A82 File Offset: 0x00266E82
		protected override void AnimationIdle()
		{
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x00268A84 File Offset: 0x00266E84
		protected override void EnterSwim()
		{
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x00268A86 File Offset: 0x00266E86
		protected override void OnSwim()
		{
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x00268A88 File Offset: 0x00266E88
		protected override void ExitSwim()
		{
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x00268A8A File Offset: 0x00266E8A
		protected override void AnimationSwim()
		{
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x00268A8C File Offset: 0x00266E8C
		protected override void EnterEat()
		{
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x00268A8E File Offset: 0x00266E8E
		protected override void OnEat()
		{
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x00268A90 File Offset: 0x00266E90
		protected override void AnimationEat()
		{
		}

		// Token: 0x06005A12 RID: 23058 RVA: 0x00268A92 File Offset: 0x00266E92
		protected override void ExitEat()
		{
		}

		// Token: 0x04005226 RID: 21030
		[SerializeField]
		private float _nicknameHeightOffset = 0.5f;

		// Token: 0x04005229 RID: 21033
		protected Transform _nicknameRoot;

		// Token: 0x0400522A RID: 21034
		private bool _nicknameEnabled;

		// Token: 0x0400522C RID: 21036
		protected CommandLabel.CommandInfo[] giveFoodLabels;
	}
}
