using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AIProject.Player;
using AIProject.SaveData;
using IllusionUtility.GetUtility;
using Manager;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject.Animal
{
	// Token: 0x02000BB2 RID: 2994
	public class PetHomePoint : Point, ICommandable
	{
		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06005A14 RID: 23060 RVA: 0x00268AE9 File Offset: 0x00266EE9
		// (set) Token: 0x06005A15 RID: 23061 RVA: 0x00268AF1 File Offset: 0x00266EF1
		public override int RegisterID
		{
			get
			{
				return this._pointID;
			}
			set
			{
				this._pointID = value;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06005A16 RID: 23062 RVA: 0x00268AFA File Offset: 0x00266EFA
		// (set) Token: 0x06005A17 RID: 23063 RVA: 0x00268B02 File Offset: 0x00266F02
		public int AnimalID
		{
			get
			{
				return this._animalID;
			}
			protected set
			{
				this._animalID = value;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06005A18 RID: 23064 RVA: 0x00268B0B File Offset: 0x00266F0B
		// (set) Token: 0x06005A19 RID: 23065 RVA: 0x00268B18 File Offset: 0x00266F18
		public Vector3 Position
		{
			get
			{
				return base.transform.position;
			}
			set
			{
				base.transform.position = value;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06005A1A RID: 23066 RVA: 0x00268B26 File Offset: 0x00266F26
		public Vector3 CommandCenter
		{
			[CompilerGenerated]
			get
			{
				return (!(this._commandBasePoint != null)) ? base.transform.position : this._commandBasePoint.position;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06005A1B RID: 23067 RVA: 0x00268B54 File Offset: 0x00266F54
		public Quaternion Rotation
		{
			[CompilerGenerated]
			get
			{
				return base.transform.rotation;
			}
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06005A1C RID: 23068 RVA: 0x00268B61 File Offset: 0x00266F61
		public Vector3 EulerAngle
		{
			[CompilerGenerated]
			get
			{
				return base.transform.eulerAngles;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06005A1D RID: 23069 RVA: 0x00268B6E File Offset: 0x00266F6E
		// (set) Token: 0x06005A1E RID: 23070 RVA: 0x00268B76 File Offset: 0x00266F76
		public AIProject.SaveData.Environment.PetHomeInfo SaveData { get; set; }

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06005A1F RID: 23071 RVA: 0x00268B7F File Offset: 0x00266F7F
		// (set) Token: 0x06005A20 RID: 23072 RVA: 0x00268B87 File Offset: 0x00266F87
		public int HousingID { get; set; }

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06005A21 RID: 23073 RVA: 0x00268B90 File Offset: 0x00266F90
		// (set) Token: 0x06005A22 RID: 23074 RVA: 0x00268B98 File Offset: 0x00266F98
		public Vector3Int GridPoint { get; set; } = Vector3Int.zero;

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x00268BA4 File Offset: 0x00266FA4
		public int InstanceID
		{
			get
			{
				return ((this._instanceID == null) ? (this._instanceID = new int?(base.GetInstanceID())) : this._instanceID).Value;
			}
		}

		// Token: 0x06005A24 RID: 23076 RVA: 0x00268BE8 File Offset: 0x00266FE8
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			Vector3 commandCenter = this.CommandCenter;
			commandCenter.y = 0f;
			float num = Vector3.Distance(basePosition, commandCenter);
			CommandType commandType = this.CommandType;
			bool flag;
			if (commandType != CommandType.Forward)
			{
				float num2 = (!this._enableRangeCheck) ? radiusB : (radiusB + this._checkRadius);
				flag = (distance <= num2);
			}
			else
			{
				float num3 = (!this._enableRangeCheck) ? radiusA : (radiusA + this._checkRadius);
				if (num3 < num)
				{
					return false;
				}
				Vector3 a = commandCenter;
				a.y = 0f;
				float num4 = angle / 2f;
				float num5 = Vector3.Angle(a - basePosition, forward);
				flag = (num5 <= num4);
			}
			if (!flag)
			{
				return false;
			}
			PlayerActor player = Map.GetPlayer();
			if (player != null)
			{
				IState state = player.PlayerController.State;
				if (state is Onbu)
				{
					return false;
				}
			}
			return flag;
		}

		// Token: 0x06005A25 RID: 23077 RVA: 0x00268CE8 File Offset: 0x002670E8
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (nmAgent == null || !nmAgent.isActiveAndEnabled)
			{
				return false;
			}
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool flag = true;
			if (nmAgent.isActiveAndEnabled)
			{
				PetHomePoint.HomeKind homeKind = this._homeKind;
				if (homeKind != PetHomePoint.HomeKind.PetMat)
				{
					if (homeKind == PetHomePoint.HomeKind.FishTank)
					{
						flag = false;
						int value = -1;
						if (!this._commandAreaInfos.IsNullOrEmpty<PetHomePoint.CommandAreaInfo>())
						{
							List<UnityEx.ValueTuple<int, Vector3>> list = ListPool<UnityEx.ValueTuple<int, Vector3>>.Get();
							for (int i = 0; i < this._commandAreaInfos.Length; i++)
							{
								PetHomePoint.CommandAreaInfo commandAreaInfo = this._commandAreaInfos[i];
								if (commandAreaInfo != null && commandAreaInfo.IsActive)
								{
									list.Add(new UnityEx.ValueTuple<int, Vector3>(i, commandAreaInfo.BasePoint.position));
								}
							}
							Vector3 position = nmAgent.transform.position;
							list.Sort((UnityEx.ValueTuple<int, Vector3> x, UnityEx.ValueTuple<int, Vector3> y) => (int)(Vector3.SqrMagnitude(x.Item2 - position) - Vector3.SqrMagnitude(y.Item2 - position)));
							for (int j = 0; j < list.Count; j++)
							{
								UnityEx.ValueTuple<int, Vector3> valueTuple = list[j];
								nmAgent.CalculatePath(valueTuple.Item2, this._pathForCalc);
								if (flag = (this._pathForCalc.status == NavMeshPathStatus.PathComplete))
								{
									value = valueTuple.Item1;
									break;
								}
							}
							ListPool<UnityEx.ValueTuple<int, Vector3>>.Release(list);
						}
						this._commandAreaIndex.Value = value;
						if (!flag)
						{
							nmAgent.CalculatePath(this.Position, this._pathForCalc);
							flag = (this._pathForCalc.status == NavMeshPathStatus.PathComplete);
						}
					}
				}
				else
				{
					nmAgent.CalculatePath(this.Position, this._pathForCalc);
					flag &= (this._pathForCalc.status == NavMeshPathStatus.PathComplete);
				}
				float num = 0f;
				Vector3[] corners = this._pathForCalc.corners;
				for (int k = 0; k < corners.Length - 1; k++)
				{
					float num2 = Vector3.Distance(corners[k], corners[k + 1]);
					num += num2;
					float num3 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
					if (num > num3)
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06005A26 RID: 23078 RVA: 0x00268F2F File Offset: 0x0026732F
		// (set) Token: 0x06005A27 RID: 23079 RVA: 0x00268F37 File Offset: 0x00267337
		public bool IsImpossible { get; protected set; }

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06005A28 RID: 23080 RVA: 0x00268F40 File Offset: 0x00267340
		// (set) Token: 0x06005A29 RID: 23081 RVA: 0x00268F48 File Offset: 0x00267348
		public Actor CommandPartner { get; set; }

		// Token: 0x06005A2A RID: 23082 RVA: 0x00268F54 File Offset: 0x00267354
		public bool SetImpossible(bool _value, Actor _actor)
		{
			if (this.IsImpossible == _value)
			{
				return false;
			}
			if (_value && this.CommandPartner != null)
			{
				return false;
			}
			this.IsImpossible = _value;
			this.CommandPartner = ((!_value) ? null : _actor);
			return true;
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06005A2B RID: 23083 RVA: 0x00268FA3 File Offset: 0x002673A3
		public bool IsNeutralCommand
		{
			get
			{
				return !this.TutorialHideMode();
			}
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x00268FB3 File Offset: 0x002673B3
		public bool TutorialHideMode()
		{
			return Map.TutorialMode;
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06005A2D RID: 23085 RVA: 0x00268FC2 File Offset: 0x002673C2
		public CommandLabel.CommandInfo[] Labels
		{
			[CompilerGenerated]
			get
			{
				return this._labels;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06005A2E RID: 23086 RVA: 0x00268FCA File Offset: 0x002673CA
		public CommandLabel.CommandInfo[] DateLabels
		{
			[CompilerGenerated]
			get
			{
				return PetHomePoint._emptyLabels;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06005A2F RID: 23087 RVA: 0x00268FD1 File Offset: 0x002673D1
		public ObjectLayer Layer
		{
			[CompilerGenerated]
			get
			{
				return ObjectLayer.Command;
			}
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06005A30 RID: 23088 RVA: 0x00268FD4 File Offset: 0x002673D4
		public CommandType CommandType
		{
			[CompilerGenerated]
			get
			{
				return this._commandType;
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06005A31 RID: 23089 RVA: 0x00268FDC File Offset: 0x002673DC
		public Transform CommandBasePoint
		{
			get
			{
				return (!(this._commandBasePoint != null)) ? base.transform : this._commandBasePoint;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06005A32 RID: 23090 RVA: 0x00269000 File Offset: 0x00267400
		public Transform LabelPoint
		{
			get
			{
				return (!(this._labelPoint != null)) ? base.transform : this._labelPoint;
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06005A33 RID: 23091 RVA: 0x00269024 File Offset: 0x00267424
		public bool EnableRangeCheck
		{
			[CompilerGenerated]
			get
			{
				return this._enableRangeCheck;
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06005A34 RID: 23092 RVA: 0x0026902C File Offset: 0x0026742C
		public float CheckRadius
		{
			[CompilerGenerated]
			get
			{
				return this._checkRadius;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06005A35 RID: 23093 RVA: 0x00269034 File Offset: 0x00267434
		public PetHomePoint.HomeKind Kind
		{
			[CompilerGenerated]
			get
			{
				return this._homeKind;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06005A36 RID: 23094 RVA: 0x0026903C File Offset: 0x0026743C
		public int AllowableSizeID
		{
			[CompilerGenerated]
			get
			{
				return this._allowableSizeID;
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06005A37 RID: 23095 RVA: 0x00269044 File Offset: 0x00267444
		public Transform[] RootPoints
		{
			[CompilerGenerated]
			get
			{
				return this._rootPoints;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06005A38 RID: 23096 RVA: 0x0026904C File Offset: 0x0026744C
		// (set) Token: 0x06005A39 RID: 23097 RVA: 0x00269054 File Offset: 0x00267454
		public IPetAnimal User { get; protected set; }

		// Token: 0x06005A3A RID: 23098 RVA: 0x0026905D File Offset: 0x0026745D
		public bool UsedPoint()
		{
			return this.User != null;
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x0026906C File Offset: 0x0026746C
		protected override void Start()
		{
			base.Start();
			this._firstLabelPosition = this.LabelPoint.position;
			this._commandAreaIndex.DistinctUntilChanged<int>().Subscribe(delegate(int index)
			{
				Transform labelPoint = this.LabelPoint;
				if (labelPoint == null)
				{
					return;
				}
				if (0 <= index && !this._commandAreaInfos.IsNullOrEmpty<PetHomePoint.CommandAreaInfo>() && index < this._commandAreaInfos.Length)
				{
					PetHomePoint.CommandAreaInfo commandAreaInfo = this._commandAreaInfos[index];
					if (commandAreaInfo != null && commandAreaInfo.LabelPoint != null)
					{
						labelPoint.position = commandAreaInfo.LabelPoint.position;
						return;
					}
				}
				labelPoint.position = this._firstLabelPosition;
			});
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			if (this._commandBasePoint == null)
			{
				GameObject gameObject = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.CommandTargetName);
				this._commandBasePoint = ((!(gameObject != null)) ? base.transform : gameObject.transform);
			}
			if (this._labelPoint == null)
			{
				GameObject gameObject2 = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.PetHomePointLabelTargetName);
				this._labelPoint = ((!(gameObject2 != null)) ? base.transform : gameObject2.transform);
			}
			if (this._rootPoints.IsNullOrEmpty<Transform>())
			{
				this.SetSizeRootPoints();
				if (this._rootPoints.IsNullOrEmpty<Transform>())
				{
					this._rootPoints = new Transform[]
					{
						base.transform
					};
				}
			}
			this.InitializeCommandLabels();
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x002691A4 File Offset: 0x002675A4
		private void InitializeCommandLabels()
		{
			Manager.Resources resources = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance;
			if (resources == null)
			{
				return;
			}
			CommonDefine.CommonIconGroup icon = resources.CommonDefine.Icon;
			Dictionary<int, UnityEx.ValueTuple<int, List<string>>> petHomeUIInfoTable = resources.AnimalTable.PetHomeUIInfoTable;
			UnityEx.ValueTuple<int, List<string>> valueTuple;
			petHomeUIInfoTable.TryGetValue((int)this._homeKind, out valueTuple);
			Dictionary<int, Sprite> actionIconTable = resources.itemIconTables.ActionIconTable;
			Sprite icon2 = null;
			actionIconTable.TryGetValue(valueTuple.Item1, out icon2);
			int index = (!Singleton<GameSystem>.IsInstance()) ? 0 : Singleton<GameSystem>.Instance.languageInt;
			PetHomePoint.HomeKind homeKind = this._homeKind;
			if (homeKind != PetHomePoint.HomeKind.PetMat)
			{
				if (homeKind == PetHomePoint.HomeKind.FishTank)
				{
					this._labels = new CommandLabel.CommandInfo[]
					{
						new CommandLabel.CommandInfo
						{
							Text = valueTuple.Item2.GetElement(index),
							Icon = icon2,
							IsHold = true,
							TargetSpriteInfo = icon.ActionSpriteInfo,
							Transform = this.LabelPoint,
							Condition = null,
							Event = delegate
							{
								PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
								if (playerActor == null)
								{
									return;
								}
								playerActor.CurrentPetHomePoint = this;
								MapUIContainer.SetActivePetHomeUI(true);
								playerActor.PlayerController.ChangeState("Harvest");
							}
						}
					};
				}
			}
			else
			{
				this._labels = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = valueTuple.Item2.GetElement(index),
						Icon = icon2,
						IsHold = true,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = this.LabelPoint,
						Condition = null,
						Event = delegate
						{
							PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
							if (playerActor == null)
							{
								return;
							}
							playerActor.CurrentPetHomePoint = this;
							MapUIContainer.SetActivePetHomeUI(true);
							playerActor.PlayerController.ChangeState("Harvest");
						}
					}
				};
			}
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x00269344 File Offset: 0x00267744
		public void SetUser(IPetAnimal animal)
		{
			if (animal == null)
			{
				return;
			}
			if (this.User != null)
			{
				this.User.Release();
			}
			this.User = animal;
			this.SaveData.AnimalData = animal.AnimalData;
			animal.Initialize(this);
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x00269382 File Offset: 0x00267782
		public void RemoveUser()
		{
			this.SaveData.AnimalData = null;
			if (this.User == null)
			{
				return;
			}
			this.User.Release();
			this.User = null;
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x002693B0 File Offset: 0x002677B0
		public void SetRootPoint(int pointID)
		{
			if (this.User == null)
			{
				return;
			}
			Transform transform = this._rootPoints.GetElement(pointID) ?? base.transform;
			if (transform == null)
			{
				return;
			}
			this.User.Position = transform.position;
			this.User.Rotation = transform.rotation;
		}

		// Token: 0x06005A40 RID: 23104 RVA: 0x00269414 File Offset: 0x00267814
		public void SetRootPoint(int pointID, IPetAnimal animal)
		{
			if (animal == null)
			{
				return;
			}
			Transform element = this._rootPoints.GetElement(pointID);
			Transform transform = (!(element != null)) ? base.transform : element;
			if (transform == null)
			{
				return;
			}
			animal.Position = transform.position;
			animal.Rotation = transform.rotation;
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x00269474 File Offset: 0x00267874
		public Transform GetRootPoint(int pointID)
		{
			Transform element = this._rootPoints.GetElement(pointID);
			return (!(element != null)) ? base.transform : element;
		}

		// Token: 0x06005A42 RID: 23106 RVA: 0x002694A8 File Offset: 0x002678A8
		private void OnDestroy()
		{
			if (this.User != null)
			{
				this.User.Release();
			}
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x002694C0 File Offset: 0x002678C0
		public void SetSizeRootPoints()
		{
			List<Transform> list = ListPool<Transform>.Get();
			base.transform.FindMatchLoop("^pet_point_[0-9]+$", ref list);
			if (!list.IsNullOrEmpty<Transform>())
			{
				Dictionary<int, Transform> dictionary = DictionaryPool<int, Transform>.Get();
				int num = 0;
				for (int i = 0; i < list.Count; i++)
				{
					Transform transform = list[i];
					string name = transform.name;
					Match match = Regex.Match(name, "[0-9]+");
					if (match.Success)
					{
						string value = match.Value;
						int num2;
						if (int.TryParse(value, out num2))
						{
							dictionary[num2] = transform;
							num = Mathf.Max(num, num2 + 1);
						}
					}
				}
				num = Mathf.Min(num, 100);
				this._rootPoints = new Transform[num];
				foreach (KeyValuePair<int, Transform> keyValuePair in dictionary)
				{
					int key = keyValuePair.Key;
					if (key >= 0 && this._rootPoints.Length > key)
					{
						this._rootPoints[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				DictionaryPool<int, Transform>.Release(dictionary);
			}
			ListPool<Transform>.Release(list);
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x00269608 File Offset: 0x00267A08
		public bool CanDelete()
		{
			if (this.SaveData == null || this.SaveData.AnimalData == null)
			{
				return true;
			}
			if (!Singleton<Map>.IsInstance() || !Singleton<Manager.Resources>.IsInstance())
			{
				return true;
			}
			AnimalData animalData = this.SaveData.AnimalData;
			int itemCategoryID = animalData.ItemCategoryID;
			int itemID = animalData.ItemID;
			if (Singleton<Manager.Resources>.Instance.GameInfo.GetItem(itemCategoryID, itemID) == null)
			{
				return true;
			}
			Map instance = Singleton<Map>.Instance;
			List<UnityEx.ValueTuple<int, List<StuffItem>>> inventoryList = instance.GetInventoryList();
			if (inventoryList.IsNullOrEmpty<UnityEx.ValueTuple<int, List<StuffItem>>>())
			{
				instance.ReturnInventoryList(inventoryList);
				return true;
			}
			StuffItem stuffItem = new StuffItem(itemCategoryID, itemID, 1);
			bool flag = false;
			foreach (UnityEx.ValueTuple<int, List<StuffItem>> valueTuple in inventoryList)
			{
				int item = valueTuple.Item1;
				List<StuffItem> item2 = valueTuple.Item2;
				int num = 0;
				item2.CanAddItem(item, stuffItem, out num);
				flag = (stuffItem.Count <= num);
				if (flag)
				{
					item2.AddItem(stuffItem);
					break;
				}
			}
			return flag;
		}

		// Token: 0x0400522D RID: 21037
		[SerializeField]
		[HideInEditorMode]
		[ReadOnly]
		private int _pointID = -1;

		// Token: 0x0400522E RID: 21038
		[SerializeField]
		[HideInEditorMode]
		[ReadOnly]
		private int _animalID = -1;

		// Token: 0x0400522F RID: 21039
		private IntReactiveProperty _commandAreaIndex = new IntReactiveProperty(-1);

		// Token: 0x04005230 RID: 21040
		private Vector3 _firstLabelPosition = Vector3.zero;

		// Token: 0x04005234 RID: 21044
		private int? _instanceID;

		// Token: 0x04005235 RID: 21045
		private NavMeshPath _pathForCalc;

		// Token: 0x04005238 RID: 21048
		private static CommandLabel.CommandInfo[] _emptyLabels = new CommandLabel.CommandInfo[0];

		// Token: 0x04005239 RID: 21049
		private CommandLabel.CommandInfo[] _labels;

		// Token: 0x0400523A RID: 21050
		[SerializeField]
		private CommandType _commandType;

		// Token: 0x0400523B RID: 21051
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x0400523C RID: 21052
		[SerializeField]
		private Transform _labelPoint;

		// Token: 0x0400523D RID: 21053
		[SerializeField]
		private bool _enableRangeCheck = true;

		// Token: 0x0400523E RID: 21054
		[SerializeField]
		private float _checkRadius = 1f;

		// Token: 0x0400523F RID: 21055
		[SerializeField]
		private PetHomePoint.HomeKind _homeKind;

		// Token: 0x04005240 RID: 21056
		[SerializeField]
		private int _allowableSizeID;

		// Token: 0x04005241 RID: 21057
		[SerializeField]
		private Transform[] _rootPoints;

		// Token: 0x04005242 RID: 21058
		[SerializeField]
		private PetHomePoint.CommandAreaInfo[] _commandAreaInfos;

		// Token: 0x02000BB3 RID: 2995
		public enum HomeKind
		{
			// Token: 0x04005245 RID: 21061
			PetMat,
			// Token: 0x04005246 RID: 21062
			FishTank
		}

		// Token: 0x02000BB4 RID: 2996
		[Serializable]
		public class CommandAreaInfo
		{
			// Token: 0x170010EA RID: 4330
			// (get) Token: 0x06005A4A RID: 23114 RVA: 0x00269886 File Offset: 0x00267C86
			public bool IsActive
			{
				[CompilerGenerated]
				get
				{
					return this._labelPoint != null && this._basePoint != null;
				}
			}

			// Token: 0x170010EB RID: 4331
			// (get) Token: 0x06005A4B RID: 23115 RVA: 0x002698A8 File Offset: 0x00267CA8
			public Transform LabelPoint
			{
				[CompilerGenerated]
				get
				{
					return this._labelPoint;
				}
			}

			// Token: 0x170010EC RID: 4332
			// (get) Token: 0x06005A4C RID: 23116 RVA: 0x002698B0 File Offset: 0x00267CB0
			public Transform BasePoint
			{
				[CompilerGenerated]
				get
				{
					return this._basePoint;
				}
			}

			// Token: 0x04005247 RID: 21063
			[SerializeField]
			private Transform _labelPoint;

			// Token: 0x04005248 RID: 21064
			[SerializeField]
			private Transform _basePoint;
		}
	}
}
