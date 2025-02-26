using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000BB5 RID: 2997
	public class AnimalActionPoint : AnimalPoint
	{
		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06005A4E RID: 23118 RVA: 0x00269E49 File Offset: 0x00268249
		public ActionTypes ActionType
		{
			[CompilerGenerated]
			get
			{
				return this._actionType;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x00269E51 File Offset: 0x00268251
		public AnimalTypes AnimalType
		{
			[CompilerGenerated]
			get
			{
				return this._animalType;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06005A50 RID: 23120 RVA: 0x00269E59 File Offset: 0x00268259
		public int MapItemID
		{
			[CompilerGenerated]
			get
			{
				return this._mapItemID;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06005A51 RID: 23121 RVA: 0x00269E61 File Offset: 0x00268261
		public bool EnabledNavMeshAgent
		{
			[CompilerGenerated]
			get
			{
				return this._enabledNavMeshAgent;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x00269E69 File Offset: 0x00268269
		public bool EnabledPositionValue
		{
			[CompilerGenerated]
			get
			{
				return this._enabledPositionValue;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06005A53 RID: 23123 RVA: 0x00269E71 File Offset: 0x00268271
		public AnimalActionPoint.DirectionKind Direction
		{
			[CompilerGenerated]
			get
			{
				return this._directionType;
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x00269E79 File Offset: 0x00268279
		public Vector3 Destination
		{
			[CompilerGenerated]
			get
			{
				return this._destination.position;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06005A55 RID: 23125 RVA: 0x00269E86 File Offset: 0x00268286
		// (set) Token: 0x06005A56 RID: 23126 RVA: 0x00269E8E File Offset: 0x0026828E
		public GameObject MapItem { get; set; }

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x00269E97 File Offset: 0x00268297
		// (set) Token: 0x06005A58 RID: 23128 RVA: 0x00269E9F File Offset: 0x0026829F
		public IAnimalActionPointUser User { get; private set; }

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x00269EA8 File Offset: 0x002682A8
		public bool InUsed
		{
			[CompilerGenerated]
			get
			{
				return this.User != null;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06005A5A RID: 23130 RVA: 0x00269EB6 File Offset: 0x002682B6
		// (set) Token: 0x06005A5B RID: 23131 RVA: 0x00269EBE File Offset: 0x002682BE
		public Dictionary<int, float> UsedCoolTime { get; private set; } = new Dictionary<int, float>();

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06005A5C RID: 23132 RVA: 0x00269EC7 File Offset: 0x002682C7
		// (set) Token: 0x06005A5D RID: 23133 RVA: 0x00269ECF File Offset: 0x002682CF
		public Dictionary<int, float> SearchedCoolTime { get; private set; } = new Dictionary<int, float>();

		// Token: 0x06005A5E RID: 23134 RVA: 0x00269ED8 File Offset: 0x002682D8
		public void SetSearchCoolTime(IAnimalActionPointUser _animal, float _time, bool _nonSetting)
		{
			int instanceID = _animal.InstanceID;
			if (_nonSetting && this.SearchedCoolTime.ContainsKey(instanceID))
			{
				return;
			}
			this.SearchedCoolTime[instanceID] = _time;
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x00269F14 File Offset: 0x00268314
		public void SetUsedCoolTime(IAnimalActionPointUser _animal, float _time, bool _nonSetting)
		{
			int instanceID = _animal.InstanceID;
			if (_nonSetting && this.UsedCoolTime.ContainsKey(instanceID))
			{
				return;
			}
			this.UsedCoolTime[instanceID] = _time;
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x00269F4D File Offset: 0x0026834D
		public new bool Available(IAnimalActionPointUser _animal)
		{
			return (this.AnimalType & _animal.AnimalType) != (AnimalTypes)0 && !this.InUsed;
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x00269F74 File Offset: 0x00268374
		public bool AvailableOutCoolTime(IAnimalActionPointUser _animal)
		{
			if ((this.AnimalType & _animal.AnimalType) == (AnimalTypes)0)
			{
				return false;
			}
			if (this.InUsed)
			{
				return false;
			}
			int instanceID = _animal.InstanceID;
			float num;
			return (!this.UsedCoolTime.TryGetValue(instanceID, out num) || 0f >= num) && (!this.SearchedCoolTime.TryGetValue(instanceID, out num) || 0f >= num);
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x00269FEA File Offset: 0x002683EA
		public bool HasCoolTime
		{
			get
			{
				return 0 < this.UsedCoolTime.Count || 0 < this.SearchedCoolTime.Count;
			}
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x0026A010 File Offset: 0x00268410
		private void CoolDown()
		{
			float deltaTime = Time.deltaTime;
			if (deltaTime != 0f)
			{
				this.CoolDownTable(this.UsedCoolTime, deltaTime);
				this.CoolDownTable(this.SearchedCoolTime, deltaTime);
			}
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x0026A048 File Offset: 0x00268448
		private void CoolDownTable(Dictionary<int, float> _coolTimeTable, float _deltaTime)
		{
			if (_coolTimeTable.IsNullOrEmpty<int, float>() || _deltaTime == 0f)
			{
				return;
			}
			List<int> list = ListPool<int>.Get();
			List<int> list2 = ListPool<int>.Get();
			foreach (int item in _coolTimeTable.Keys)
			{
				list.Add(item);
			}
			for (int i = 0; i < list.Count; i++)
			{
				int num = list[i];
				float num2 = _coolTimeTable[num];
				num2 = Mathf.Max(0f, num2 - _deltaTime);
				if (num2 <= 0f)
				{
					list2.Add(num);
				}
				else
				{
					_coolTimeTable[num] = num2;
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				_coolTimeTable.Remove(list2[j]);
			}
			ListPool<int>.Release(list);
			ListPool<int>.Release(list2);
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x0026A160 File Offset: 0x00268560
		public void ClearCoolTime()
		{
			this.UsedCoolTime.Clear();
			this.SearchedCoolTime.Clear();
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x0026A178 File Offset: 0x00268578
		public void AddBooking(IAnimalActionPointUser animal)
		{
			if (!this.bookings.Contains(animal))
			{
				this.bookings.Add(animal);
			}
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x0026A197 File Offset: 0x00268597
		public void RemoveBooking(IAnimalActionPointUser animal)
		{
			if (this.bookings.Contains(animal))
			{
				this.bookings.Remove(animal);
			}
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x0026A1B7 File Offset: 0x002685B7
		public void RemoveAllBooking()
		{
			this.bookings.Clear();
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x0026A1C4 File Offset: 0x002685C4
		public void RemoveAllBooking(Action<IAnimalActionPointUser> callback)
		{
			if (callback != null && !this.bookings.IsNullOrEmpty<IAnimalActionPointUser>())
			{
				for (int i = 0; i < this.bookings.Count; i++)
				{
					callback(this.bookings[i]);
				}
			}
			this.bookings.Clear();
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x0026A220 File Offset: 0x00268620
		public bool ContainsBooking(IAnimalActionPointUser animal)
		{
			return this.bookings.Contains(animal);
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x0026A22E File Offset: 0x0026862E
		public bool MyUse(IAnimalActionPointUser animal)
		{
			return animal == this.User;
		}

		// Token: 0x06005A6C RID: 23148 RVA: 0x0026A23C File Offset: 0x0026863C
		public bool SetUse(IAnimalActionPointUser animal)
		{
			if (animal == null)
			{
				return false;
			}
			this.RemoveBooking(animal);
			if (this.InUsed && this.User != animal)
			{
				animal.MissingActionPoint();
				return false;
			}
			this.RemoveAllBooking(delegate(IAnimalActionPointUser x)
			{
				if (x != null)
				{
					x.MissingActionPoint();
				}
			});
			if (this.User == null)
			{
				this.User = animal;
			}
			return true;
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x0026A2AD File Offset: 0x002686AD
		public bool StopUsing(IAnimalActionPointUser animal)
		{
			if (this.User == animal)
			{
				this.User = null;
				return true;
			}
			return false;
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x0026A2C5 File Offset: 0x002686C5
		public bool StopUsing()
		{
			if (this.User == null)
			{
				return false;
			}
			this.User = null;
			return true;
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x0026A2DC File Offset: 0x002686DC
		protected override void Start()
		{
			base.Start();
			(from _ in Observable.EveryUpdate().TakeUntilDisable(base.gameObject)
			where base.isActiveAndEnabled
			where this.HasCoolTime
			select _).Subscribe(delegate(long _)
			{
				this.CoolDown();
			});
		}

		// Token: 0x06005A70 RID: 23152 RVA: 0x0026A334 File Offset: 0x00268734
		public override void LocateGround()
		{
			base.LocateGround();
			foreach (AnimalActionPoint.AnimalActionSlot animalActionSlot in this._actionSlotTable)
			{
				if (animalActionSlot.Point != animalActionSlot.RecoveryPoint)
				{
					Point.LocateGround(animalActionSlot.RecoveryPoint);
				}
			}
			float num = 15f;
			LocateTypes locateType = base.LocateType;
			if (locateType != LocateTypes.Collider)
			{
				if (locateType == LocateTypes.NavMesh)
				{
					AnimalPoint.RelocationOnNavMesh(base.transform, num);
					foreach (AnimalActionPoint.AnimalActionSlot animalActionSlot2 in this._actionSlotTable)
					{
						if (animalActionSlot2.Point != animalActionSlot2.RecoveryPoint)
						{
							AnimalPoint.RelocationOnNavMesh(animalActionSlot2.RecoveryPoint, num);
						}
					}
				}
			}
			else
			{
				AnimalPoint.RelocationOnCollider(base.transform, num);
				foreach (AnimalActionPoint.AnimalActionSlot animalActionSlot3 in this._actionSlotTable)
				{
					if (animalActionSlot3.Point != animalActionSlot3.RecoveryPoint)
					{
						AnimalPoint.RelocationOnCollider(animalActionSlot3.RecoveryPoint, num);
					}
				}
			}
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x0026A4D0 File Offset: 0x002688D0
		public override void LoadObject()
		{
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x0026A4D4 File Offset: 0x002688D4
		public Tuple<Transform, Transform> GetSlot()
		{
			if (this._actionSlotTable.Count <= 0)
			{
				return new Tuple<Transform, Transform>(null, null);
			}
			AnimalActionPoint.AnimalActionSlot animalActionSlot = this._actionSlotTable[0];
			return new Tuple<Transform, Transform>(animalActionSlot.Point, animalActionSlot.RecoveryPoint);
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x0026A518 File Offset: 0x00268918
		public Tuple<Transform, Transform> GetSlot(ActionTypes type)
		{
			for (int i = 0; i < this._actionSlotTable.Count; i++)
			{
				AnimalActionPoint.AnimalActionSlot animalActionSlot = this._actionSlotTable[i];
				if (animalActionSlot.AcceptionKey.Contains(type))
				{
					return new Tuple<Transform, Transform>(animalActionSlot.Point, animalActionSlot.RecoveryPoint);
				}
			}
			return new Tuple<Transform, Transform>(null, null);
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x0026A578 File Offset: 0x00268978
		public Tuple<Transform, Transform>[] GetSlots(ActionTypes type)
		{
			return (from x in this._actionSlotTable
			where x.AcceptionKey.Contains(type)
			select new Tuple<Transform, Transform>(x.Point, x.RecoveryPoint)).ToArray<Tuple<Transform, Transform>>();
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x0026A5D0 File Offset: 0x002689D0
		public void SetStand(IAnimalActionPointUser animal, Transform t)
		{
			if (animal == null || t == null)
			{
				return;
			}
			IConnectableObservable<TimeInterval<float>> connectableObservable = ObservableEasing.Linear(0.2f, false).FrameTimeInterval(false).Publish<TimeInterval<float>>();
			connectableObservable.Connect();
			Vector3 position = animal.Position;
			Quaternion rotation = animal.Rotation;
			AnimalActionPoint.DirectionKind directionType = this._directionType;
			if (directionType != AnimalActionPoint.DirectionKind.Lock)
			{
				if (directionType == AnimalActionPoint.DirectionKind.Look)
				{
					Quaternion lookRotation = Quaternion.LookRotation(Vector3.Normalize(t.position - animal.Position));
					connectableObservable.Subscribe(delegate(TimeInterval<float> x)
					{
						animal.Rotation = Quaternion.Slerp(rotation, lookRotation, x.Value);
					});
				}
			}
			else
			{
				connectableObservable.Subscribe(delegate(TimeInterval<float> x)
				{
					animal.Rotation = Quaternion.Slerp(rotation, t.rotation, x.Value);
				});
			}
			if (this._enabledPositionValue)
			{
				connectableObservable.Subscribe(delegate(TimeInterval<float> x)
				{
					animal.Position = Vector3.Lerp(position, t.position, x.Value);
				});
			}
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x0026A6F0 File Offset: 0x00268AF0
		public void SetStand(IAnimalActionPointUser animal, Transform t, Action completeEvent)
		{
			if (animal == null || t == null)
			{
				if (completeEvent != null)
				{
					completeEvent();
				}
				return;
			}
			IConnectableObservable<TimeInterval<float>> connectableObservable = ObservableEasing.Linear(0.2f, false).FrameTimeInterval(false).Publish<TimeInterval<float>>();
			connectableObservable.Connect();
			Vector3 position = animal.Position;
			Quaternion rotation = animal.Rotation;
			AnimalActionPoint.DirectionKind directionType = this._directionType;
			if (directionType != AnimalActionPoint.DirectionKind.Lock)
			{
				if (directionType == AnimalActionPoint.DirectionKind.Look)
				{
					Quaternion lookRotation = Quaternion.LookRotation(Vector3.Normalize(t.position - animal.Position));
					connectableObservable.Subscribe(delegate(TimeInterval<float> x)
					{
						animal.Rotation = Quaternion.Slerp(rotation, lookRotation, x.Value);
					});
				}
			}
			else
			{
				connectableObservable.Subscribe(delegate(TimeInterval<float> x)
				{
					animal.Rotation = Quaternion.Slerp(rotation, t.rotation, x.Value);
				});
			}
			if (this._enabledPositionValue)
			{
				connectableObservable.Subscribe(delegate(TimeInterval<float> x)
				{
					animal.Position = Vector3.Lerp(position, t.position, x.Value);
				});
			}
			Action comEvent = completeEvent;
			Observable.WhenAll<TimeInterval<float>>(new IObservable<TimeInterval<float>>[]
			{
				connectableObservable
			}).TakeUntilDestroy(base.gameObject).Subscribe(delegate(TimeInterval<float>[] _)
			{
				Action comEvent = comEvent;
				if (comEvent != null)
				{
					comEvent();
				}
			});
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06005A77 RID: 23159 RVA: 0x0026A850 File Offset: 0x00268C50
		public int InstanceID
		{
			get
			{
				return ((this._hasCode == null) ? (this._hasCode = new int?(base.GetInstanceID())) : this._hasCode).Value;
			}
		}

		// Token: 0x04005249 RID: 21065
		[SerializeField]
		private ActionTypes _actionType;

		// Token: 0x0400524A RID: 21066
		[SerializeField]
		private AnimalTypes _animalType = AnimalTypes.All;

		// Token: 0x0400524B RID: 21067
		[SerializeField]
		private int _mapItemID = -1;

		// Token: 0x0400524C RID: 21068
		[SerializeField]
		private bool _enabledNavMeshAgent = true;

		// Token: 0x0400524D RID: 21069
		[SerializeField]
		private bool _enabledPositionValue = true;

		// Token: 0x0400524E RID: 21070
		[SerializeField]
		private AnimalActionPoint.DirectionKind _directionType = AnimalActionPoint.DirectionKind.Lock;

		// Token: 0x0400524F RID: 21071
		[SerializeField]
		private Transform _destination;

		// Token: 0x04005250 RID: 21072
		[SerializeField]
		private AnimalActionPoint.AnimalActionSlotTable _actionSlotTable = new AnimalActionPoint.AnimalActionSlotTable();

		// Token: 0x04005252 RID: 21074
		private List<IAnimalActionPointUser> bookings = new List<IAnimalActionPointUser>();

		// Token: 0x04005254 RID: 21076
		public TimeSpan coolTimeDuration = TimeSpan.MinValue;

		// Token: 0x04005257 RID: 21079
		private int? _hasCode;

		// Token: 0x02000BB6 RID: 2998
		public enum DirectionKind
		{
			// Token: 0x0400525B RID: 21083
			Free,
			// Token: 0x0400525C RID: 21084
			Lock,
			// Token: 0x0400525D RID: 21085
			Look
		}

		// Token: 0x02000BB7 RID: 2999
		[Serializable]
		public class AnimalActionSlotTable : IEnumerable<AnimalActionPoint.AnimalActionSlot>, IEnumerable
		{
			// Token: 0x170010FB RID: 4347
			// (get) Token: 0x06005A7E RID: 23166 RVA: 0x0026A8E0 File Offset: 0x00268CE0
			public List<AnimalActionPoint.AnimalActionSlot> Table
			{
				[CompilerGenerated]
				get
				{
					return this._table;
				}
			}

			// Token: 0x170010FC RID: 4348
			// (get) Token: 0x06005A7F RID: 23167 RVA: 0x0026A8E8 File Offset: 0x00268CE8
			public int Count
			{
				[CompilerGenerated]
				get
				{
					return this._table.Count;
				}
			}

			// Token: 0x170010FD RID: 4349
			public AnimalActionPoint.AnimalActionSlot this[int index]
			{
				get
				{
					return this._table[index];
				}
				set
				{
					this._table[index] = value;
				}
			}

			// Token: 0x06005A82 RID: 23170 RVA: 0x0026A912 File Offset: 0x00268D12
			public void Initialize()
			{
				this.Distinct();
			}

			// Token: 0x06005A83 RID: 23171 RVA: 0x0026A91C File Offset: 0x00268D1C
			private void Distinct()
			{
				List<AnimalActionPoint.AnimalActionSlot> list = ListPool<AnimalActionPoint.AnimalActionSlot>.Get();
				foreach (AnimalActionPoint.AnimalActionSlot animalActionSlot in this._table)
				{
					bool flag = true;
					foreach (AnimalActionPoint.AnimalActionSlot animalActionSlot2 in list)
					{
						if (animalActionSlot2.Point == animalActionSlot.Point)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						list.Add(animalActionSlot);
					}
				}
				List<AnimalActionPoint.AnimalActionSlot> list2 = ListPool<AnimalActionPoint.AnimalActionSlot>.Get();
				foreach (AnimalActionPoint.AnimalActionSlot item in this._table)
				{
					if (!list.Contains(item))
					{
						list2.Add(item);
					}
				}
				foreach (AnimalActionPoint.AnimalActionSlot item2 in list2)
				{
					this._table.Remove(item2);
				}
				ListPool<AnimalActionPoint.AnimalActionSlot>.Release(list2);
				ListPool<AnimalActionPoint.AnimalActionSlot>.Release(list);
			}

			// Token: 0x06005A84 RID: 23172 RVA: 0x0026AAAC File Offset: 0x00268EAC
			public AnimalActionPoint.AnimalActionSlotTable.Enumerator GetEnumerator()
			{
				return new AnimalActionPoint.AnimalActionSlotTable.Enumerator(this);
			}

			// Token: 0x06005A85 RID: 23173 RVA: 0x0026AAB4 File Offset: 0x00268EB4
			IEnumerator<AnimalActionPoint.AnimalActionSlot> IEnumerable<AnimalActionPoint.AnimalActionSlot>.GetEnumerator()
			{
				return new AnimalActionPoint.AnimalActionSlotTable.Enumerator(this);
			}

			// Token: 0x06005A86 RID: 23174 RVA: 0x0026AAC1 File Offset: 0x00268EC1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new AnimalActionPoint.AnimalActionSlotTable.Enumerator(this);
			}

			// Token: 0x0400525E RID: 21086
			[SerializeField]
			private List<AnimalActionPoint.AnimalActionSlot> _table = new List<AnimalActionPoint.AnimalActionSlot>();

			// Token: 0x02000BB8 RID: 3000
			public struct Enumerator : IEnumerator<AnimalActionPoint.AnimalActionSlot>, IDisposable, IEnumerator
			{
				// Token: 0x06005A87 RID: 23175 RVA: 0x0026AACE File Offset: 0x00268ECE
				public Enumerator(List<AnimalActionPoint.AnimalActionSlot> list)
				{
					this._list = list;
					this._index = 0;
					this._current = null;
				}

				// Token: 0x06005A88 RID: 23176 RVA: 0x0026AAE5 File Offset: 0x00268EE5
				public Enumerator(AnimalActionPoint.AnimalActionSlotTable table)
				{
					this._list = table._table;
					this._index = 0;
					this._current = null;
				}

				// Token: 0x170010FF RID: 4351
				// (get) Token: 0x06005A89 RID: 23177 RVA: 0x0026AB01 File Offset: 0x00268F01
				public AnimalActionPoint.AnimalActionSlot Current
				{
					[CompilerGenerated]
					get
					{
						return this._current;
					}
				}

				// Token: 0x170010FE RID: 4350
				// (get) Token: 0x06005A8A RID: 23178 RVA: 0x0026AB09 File Offset: 0x00268F09
				object IEnumerator.Current
				{
					get
					{
						if (this._index == 0 || this._index == this._list.Count + 1)
						{
							throw new InvalidOperationException();
						}
						return this._current;
					}
				}

				// Token: 0x06005A8B RID: 23179 RVA: 0x0026AB3A File Offset: 0x00268F3A
				public void Dispose()
				{
				}

				// Token: 0x06005A8C RID: 23180 RVA: 0x0026AB3C File Offset: 0x00268F3C
				public bool MoveNext()
				{
					if (this._index < this._list.Count)
					{
						this._current = this._list[this._index];
						this._index++;
						return true;
					}
					return this.MoveNextRare();
				}

				// Token: 0x06005A8D RID: 23181 RVA: 0x0026AB8C File Offset: 0x00268F8C
				private bool MoveNextRare()
				{
					this._index = this._list.Count + 1;
					this._current = null;
					return false;
				}

				// Token: 0x06005A8E RID: 23182 RVA: 0x0026ABA9 File Offset: 0x00268FA9
				void IEnumerator.Reset()
				{
					this._index = 0;
					this._current = null;
				}

				// Token: 0x0400525F RID: 21087
				private List<AnimalActionPoint.AnimalActionSlot> _list;

				// Token: 0x04005260 RID: 21088
				private int _index;

				// Token: 0x04005261 RID: 21089
				private AnimalActionPoint.AnimalActionSlot _current;
			}
		}

		// Token: 0x02000BB9 RID: 3001
		[Serializable]
		public class AnimalActionSlot
		{
			// Token: 0x17001100 RID: 4352
			// (get) Token: 0x06005A90 RID: 23184 RVA: 0x0026ABC1 File Offset: 0x00268FC1
			public ActionTypes AcceptionKey
			{
				[CompilerGenerated]
				get
				{
					return this._acceptionKey;
				}
			}

			// Token: 0x17001101 RID: 4353
			// (get) Token: 0x06005A91 RID: 23185 RVA: 0x0026ABC9 File Offset: 0x00268FC9
			public Transform Point
			{
				[CompilerGenerated]
				get
				{
					return this._point;
				}
			}

			// Token: 0x17001102 RID: 4354
			// (get) Token: 0x06005A92 RID: 23186 RVA: 0x0026ABD1 File Offset: 0x00268FD1
			public Transform RecoveryPoint
			{
				[CompilerGenerated]
				get
				{
					return this._recoveryPoint;
				}
			}

			// Token: 0x04005262 RID: 21090
			[SerializeField]
			private ActionTypes _acceptionKey;

			// Token: 0x04005263 RID: 21091
			[SerializeField]
			private Transform _point;

			// Token: 0x04005264 RID: 21092
			[SerializeField]
			private Transform _recoveryPoint;
		}
	}
}
