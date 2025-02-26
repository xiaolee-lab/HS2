using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B61 RID: 2913
	public class AnimalDesireController : MonoBehaviour
	{
		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x060056C4 RID: 22212 RVA: 0x00259549 File Offset: 0x00257949
		// (set) Token: 0x060056C5 RID: 22213 RVA: 0x00259551 File Offset: 0x00257951
		public bool Active { get; private set; }

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x060056C6 RID: 22214 RVA: 0x0025955A File Offset: 0x0025795A
		private Manager.Resources.AnimalTables AnimalTable
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.AnimalTable;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x060056C7 RID: 22215 RVA: 0x00259566 File Offset: 0x00257966
		private AnimalDefinePack.AllAnimalInfoGroup AllAnimalInfo
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Manager.Resources>.Instance.AnimalDefinePack.AllAnimalInfo;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x060056C8 RID: 22216 RVA: 0x00259577 File Offset: 0x00257977
		public bool AnimalActive
		{
			[CompilerGenerated]
			get
			{
				return this.animal != null && this.animal.ActiveState;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x060056C9 RID: 22217 RVA: 0x00259598 File Offset: 0x00257998
		public AnimalTypes AnimalType
		{
			[CompilerGenerated]
			get
			{
				return (!(this.animal != null)) ? ((AnimalTypes)0) : this.animal.AnimalType;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x060056CA RID: 22218 RVA: 0x002595BC File Offset: 0x002579BC
		public BreedingTypes BreedingType
		{
			[CompilerGenerated]
			get
			{
				return (!(this.animal != null)) ? BreedingTypes.Wild : this.animal.BreedingType;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x060056CB RID: 22219 RVA: 0x002595E0 File Offset: 0x002579E0
		// (set) Token: 0x060056CC RID: 22220 RVA: 0x002595E8 File Offset: 0x002579E8
		public Dictionary<DesireType, List<AnimalState>> TargetStateTable { get; private set; } = new Dictionary<DesireType, List<AnimalState>>();

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x060056CD RID: 22221 RVA: 0x002595F1 File Offset: 0x002579F1
		// (set) Token: 0x060056CE RID: 22222 RVA: 0x002595F9 File Offset: 0x002579F9
		public Func<DesireType, bool> DesireFilledEvent { get; set; }

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x060056CF RID: 22223 RVA: 0x00259602 File Offset: 0x00257A02
		// (set) Token: 0x060056D0 RID: 22224 RVA: 0x0025960A File Offset: 0x00257A0A
		public Func<DesireType, bool> ChangedCandidateDesireEvent { get; set; }

		// Token: 0x060056D1 RID: 22225 RVA: 0x00259614 File Offset: 0x00257A14
		public float GetParam(DesireType _desireType)
		{
			float result = 0f;
			this.ParamTable.TryGetValue(_desireType, out result);
			return result;
		}

		// Token: 0x060056D2 RID: 22226 RVA: 0x00259638 File Offset: 0x00257A38
		public bool SetParam(DesireType _desireType, float _value)
		{
			if (!this.ParamTable.ContainsKey(_desireType))
			{
				return false;
			}
			Tuple<float, float> tuple;
			this.ParamTable[_desireType] = ((!this.BorderTable.TryGetValue(_desireType, out tuple)) ? Mathf.Max(_value, 0f) : Mathf.Clamp(_value, 0f, tuple.Item2));
			this.SetDesireType();
			this.CheckCandidateDesire();
			return true;
		}

		// Token: 0x060056D3 RID: 22227 RVA: 0x002596A8 File Offset: 0x00257AA8
		public bool ResetCurrentDesrie()
		{
			if (this.CurrentDesire == DesireType.None)
			{
				return false;
			}
			if (this.ParamTable.ContainsKey(this.CurrentDesire))
			{
				this.ParamTable[this.CurrentDesire] = 0f;
			}
			this.CurrentDesire = DesireType.None;
			return true;
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x060056D4 RID: 22228 RVA: 0x002596F7 File Offset: 0x00257AF7
		// (set) Token: 0x060056D5 RID: 22229 RVA: 0x002596FF File Offset: 0x00257AFF
		public DesireType CurrentDesire { get; private set; } = DesireType.None;

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x060056D6 RID: 22230 RVA: 0x00259708 File Offset: 0x00257B08
		public bool HasCurrentDesire
		{
			[CompilerGenerated]
			get
			{
				return this.CurrentDesire != DesireType.None;
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x00259716 File Offset: 0x00257B16
		// (set) Token: 0x060056D8 RID: 22232 RVA: 0x0025971E File Offset: 0x00257B1E
		public DesireType CandidateDesire { get; private set; } = DesireType.None;

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x060056D9 RID: 22233 RVA: 0x00259727 File Offset: 0x00257B27
		public bool HasCandidateDesire
		{
			[CompilerGenerated]
			get
			{
				return this.CandidateDesire != DesireType.None;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x060056DA RID: 22234 RVA: 0x00259735 File Offset: 0x00257B35
		public bool HasOnlyCandidateDesire
		{
			[CompilerGenerated]
			get
			{
				return this.CandidateDesire != DesireType.None && this.CurrentDesire == DesireType.None;
			}
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x00259750 File Offset: 0x00257B50
		private void Awake()
		{
			(from _ in Observable.EveryUpdate().TakeUntilDisable(base.gameObject)
			where base.isActiveAndEnabled
			where this.Active
			where this.AnimalActive
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x002597B2 File Offset: 0x00257BB2
		public void Initialize(bool _enabled)
		{
			this.Clear();
			this.TableSetting();
			this.SetRandomParam();
			this.Active = _enabled;
		}

		// Token: 0x060056DD RID: 22237 RVA: 0x002597CD File Offset: 0x00257BCD
		private void TableSetting()
		{
			this.DesireTableSetting();
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x002597D8 File Offset: 0x00257BD8
		private void DesireTableSetting()
		{
			AnimalTypes animalType = this.AnimalType;
			BreedingTypes breedingType = this.BreedingType;
			this.ClearTable();
			foreach (DesireType key in AnimalDesireController.DesireTypeElements)
			{
				this.ParamTable[key] = 0f;
			}
			Dictionary<BreedingTypes, Dictionary<int, DesireType>> dictionary;
			Dictionary<int, DesireType> dictionary2;
			if (this.AnimalTable.DesirePriorityTable.TryGetValue(animalType, out dictionary) && dictionary.TryGetValue(breedingType, out dictionary2))
			{
				foreach (KeyValuePair<int, DesireType> keyValuePair in dictionary2)
				{
					this.PriorityTable[keyValuePair.Key] = keyValuePair.Value;
					this.PriorityList.Add(keyValuePair.Key);
				}
				this.PriorityList.Sort();
			}
			Dictionary<BreedingTypes, Dictionary<DesireType, int>> dictionary3;
			Dictionary<DesireType, int> dictionary4;
			if (this.AnimalTable.DesireSpanTable.TryGetValue(animalType, out dictionary3) && dictionary3.TryGetValue(breedingType, out dictionary4))
			{
				foreach (KeyValuePair<DesireType, int> keyValuePair2 in dictionary4)
				{
					this.SpanTable[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			Dictionary<BreedingTypes, Dictionary<DesireType, Tuple<float, float>>> dictionary5;
			Dictionary<DesireType, Tuple<float, float>> dictionary6;
			if (this.AnimalTable.DesireBorderTable.TryGetValue(animalType, out dictionary5) && dictionary5.TryGetValue(breedingType, out dictionary6))
			{
				foreach (KeyValuePair<DesireType, Tuple<float, float>> keyValuePair3 in dictionary6)
				{
					this.BorderTable[keyValuePair3.Key] = keyValuePair3.Value;
				}
			}
			Dictionary<BreedingTypes, Dictionary<AnimalState, Dictionary<DesireType, float>>> dictionary7;
			Dictionary<AnimalState, Dictionary<DesireType, float>> dictionary8;
			if (this.AnimalTable.DesireRateTable.TryGetValue(animalType, out dictionary7) && dictionary7.TryGetValue(breedingType, out dictionary8))
			{
				foreach (KeyValuePair<AnimalState, Dictionary<DesireType, float>> keyValuePair4 in dictionary8)
				{
					this.RateTable[keyValuePair4.Key] = new Dictionary<DesireType, float>();
					foreach (KeyValuePair<DesireType, float> keyValuePair5 in keyValuePair4.Value)
					{
						this.RateTable[keyValuePair4.Key][keyValuePair5.Key] = keyValuePair5.Value;
					}
				}
			}
			Dictionary<BreedingTypes, Dictionary<DesireType, List<AnimalState>>> dictionary9;
			Dictionary<DesireType, List<AnimalState>> dictionary10;
			if (this.AnimalTable.DesireTargetStateTable.TryGetValue(animalType, out dictionary9) && dictionary9.TryGetValue(breedingType, out dictionary10))
			{
				foreach (KeyValuePair<DesireType, List<AnimalState>> keyValuePair6 in dictionary10)
				{
					List<AnimalState> list = new List<AnimalState>();
					foreach (AnimalState item in keyValuePair6.Value)
					{
						list.Add(item);
					}
					this.TargetStateTable[keyValuePair6.Key] = list;
				}
			}
			Dictionary<BreedingTypes, Dictionary<DesireType, Dictionary<bool, Dictionary<DesireType, ChangeParamState>>>> dictionary11;
			Dictionary<DesireType, Dictionary<bool, Dictionary<DesireType, ChangeParamState>>> dictionary12;
			if (this.AnimalTable.DesireResultTable.TryGetValue(animalType, out dictionary11) && dictionary11.TryGetValue(breedingType, out dictionary12))
			{
				foreach (KeyValuePair<DesireType, Dictionary<bool, Dictionary<DesireType, ChangeParamState>>> keyValuePair7 in dictionary12)
				{
					this.ResultTable[keyValuePair7.Key] = new Dictionary<bool, Dictionary<DesireType, ChangeParamState>>();
					foreach (KeyValuePair<bool, Dictionary<DesireType, ChangeParamState>> keyValuePair8 in keyValuePair7.Value)
					{
						this.ResultTable[keyValuePair7.Key][keyValuePair8.Key] = new Dictionary<DesireType, ChangeParamState>();
						foreach (KeyValuePair<DesireType, ChangeParamState> keyValuePair9 in keyValuePair8.Value)
						{
							this.ResultTable[keyValuePair7.Key][keyValuePair8.Key][keyValuePair9.Key] = keyValuePair9.Value;
						}
					}
				}
			}
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x00259D9C File Offset: 0x0025819C
		public void SetRandomParam()
		{
			foreach (KeyValuePair<DesireType, Tuple<float, float>> keyValuePair in this.BorderTable)
			{
				if (this.ParamTable.ContainsKey(keyValuePair.Key))
				{
					float item = keyValuePair.Value.Item1;
					float value = UnityEngine.Random.Range(0f, item * 0.75f);
					this.ParamTable[keyValuePair.Key] = value;
				}
			}
		}

		// Token: 0x060056E0 RID: 22240 RVA: 0x00259E40 File Offset: 0x00258240
		private void OnUpdate()
		{
			if (this.animal.ParamRisePossible && !this.HasCurrentDesire)
			{
				this.SetDesireType();
			}
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x00259E64 File Offset: 0x00258264
		public void OnMinuteUpdate()
		{
			if (this.Active && !this.HasCurrentDesire)
			{
				this.DesireParamRiseMinute();
			}
			this.CheckCandidateDesire();
		}

		// Token: 0x060056E2 RID: 22242 RVA: 0x00259E88 File Offset: 0x00258288
		private void DesireParamRiseMinute()
		{
			AnimalState currentState = this.animal.CurrentState;
			Dictionary<DesireType, float> dictionary;
			if (this.RateTable.TryGetValue(currentState, out dictionary))
			{
				foreach (KeyValuePair<DesireType, float> keyValuePair in dictionary)
				{
					float num;
					if (this.ParamTable.TryGetValue(keyValuePair.Key, out num))
					{
						num += keyValuePair.Value;
						Tuple<float, float> tuple;
						this.ParamTable[keyValuePair.Key] = ((!this.BorderTable.TryGetValue(keyValuePair.Key, out tuple)) ? Mathf.Max(num, 0f) : Mathf.Clamp(num, 0f, tuple.Item2));
					}
				}
			}
		}

		// Token: 0x060056E3 RID: 22243 RVA: 0x00259F6C File Offset: 0x0025836C
		public void CheckCandidateDesire()
		{
			DesireType candidateDesire = this.CandidateDesire;
			this.CandidateDesire = DesireType.None;
			for (int i = 0; i < this.PriorityList.Count; i++)
			{
				DesireType desireType = this.PriorityTable[this.PriorityList[i]];
				float num;
				Tuple<float, float> tuple;
				if (this.ParamTable.TryGetValue(desireType, out num) && this.BorderTable.TryGetValue(desireType, out tuple))
				{
					if (tuple.Item1 <= num)
					{
						this.CandidateDesire = desireType;
						break;
					}
				}
			}
			if (this.CandidateDesire != candidateDesire)
			{
				Func<DesireType, bool> changedCandidateDesireEvent = this.ChangedCandidateDesireEvent;
				if (changedCandidateDesireEvent != null)
				{
					changedCandidateDesireEvent(this.CandidateDesire);
				}
			}
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x0025A028 File Offset: 0x00258428
		public bool CheckDesireParamFilled(DesireType _desireType)
		{
			float num = 0f;
			Tuple<float, float> tuple;
			return this.BorderTable.TryGetValue(_desireType, out tuple) && this.ParamTable.TryGetValue(_desireType, out num) && tuple.Item2 <= num;
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x0025A070 File Offset: 0x00258470
		public bool SetDesireType()
		{
			int i = 0;
			while (i < this.PriorityList.Count)
			{
				DesireType desireType = this.PriorityTable[this.PriorityList[i]];
				if (this.CheckDesireParamFilled(desireType))
				{
					if (this.CurrentDesire == desireType)
					{
						return false;
					}
					this.CurrentDesire = desireType;
					Func<DesireType, bool> desireFilledEvent = this.DesireFilledEvent;
					bool? flag = (desireFilledEvent != null) ? new bool?(desireFilledEvent(desireType)) : null;
					if (flag != null && flag.Value)
					{
						this.CheckCandidateDesire();
						return true;
					}
					this.CurrentDesire = DesireType.None;
					this.SetParam(desireType, 0f);
					this.CheckCandidateDesire();
					return false;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x060056E6 RID: 22246 RVA: 0x0025A13C File Offset: 0x0025853C
		public void ReduceParameter(bool _success, DesireType _changeDesire)
		{
			AnimalTypes animalType = this.AnimalType;
			Dictionary<bool, Dictionary<DesireType, ChangeParamState>> dictionary;
			if (this.ResultTable.TryGetValue(_changeDesire, out dictionary))
			{
				foreach (KeyValuePair<bool, Dictionary<DesireType, ChangeParamState>> keyValuePair in dictionary)
				{
					bool key = keyValuePair.Key;
					if (_success == key)
					{
						foreach (KeyValuePair<DesireType, ChangeParamState> keyValuePair2 in keyValuePair.Value)
						{
							DesireType key2 = keyValuePair2.Key;
							ChangeParamState value = keyValuePair2.Value;
							if (this.ParamTable.ContainsKey(key2))
							{
								float num = this.ParamTable[key2];
								float randomValue = value.RandomValue;
								switch (value.changeType)
								{
								case ChangeType.Add:
									num += randomValue;
									break;
								case ChangeType.Sub:
									num -= randomValue;
									break;
								case ChangeType.Cng:
									num = randomValue;
									break;
								default:
									continue;
								}
								Tuple<float, float> tuple;
								this.ParamTable[key2] = ((!this.BorderTable.TryGetValue(key2, out tuple)) ? Mathf.Max(num, 0f) : Mathf.Clamp(num, 0f, tuple.Item2));
							}
						}
					}
				}
			}
			else
			{
				this.ParamTable[_changeDesire] = 0f;
			}
			if (this.CurrentDesire == _changeDesire)
			{
				this.CurrentDesire = DesireType.None;
			}
			this.CheckCandidateDesire();
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x0025A318 File Offset: 0x00258718
		public void ReduceParameter(bool _success)
		{
			this.ReduceParameter(_success, this.CurrentDesire);
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x0025A327 File Offset: 0x00258727
		public void SuccessActionPoint()
		{
			this.ReduceParameter(true);
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x0025A330 File Offset: 0x00258730
		public void FailureActionPoint(AnimalState _nextState = AnimalState.Idle)
		{
			this.ReduceParameter(false);
			this.animal.CancelActionPoint();
			if (this.animal.CurrentState != _nextState)
			{
				this.animal.CurrentState = _nextState;
			}
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x0025A364 File Offset: 0x00258764
		private void ClearTable()
		{
			this.ParamTable.Clear();
			this.PriorityList.Clear();
			this.PriorityTable.Clear();
			this.SpanTable.Clear();
			this.BorderTable.Clear();
			this.RateTable.Clear();
			this.TargetStateTable.Clear();
			this.ResultTable.Clear();
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x0025A3CC File Offset: 0x002587CC
		public void Clear()
		{
			this.Active = false;
			DesireType desireType = DesireType.None;
			this.CandidateDesire = desireType;
			this.CurrentDesire = desireType;
			this.ClearTable();
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x0025A3F6 File Offset: 0x002587F6
		private void OnDestroy()
		{
			this.Clear();
		}

		// Token: 0x04004FFA RID: 20474
		[SerializeField]
		private AnimalBase animal;

		// Token: 0x04004FFB RID: 20475
		private static List<DesireType> DesireTypeElements = new List<DesireType>
		{
			DesireType.Sleepiness,
			DesireType.Loneliness,
			DesireType.Action
		};

		// Token: 0x04004FFD RID: 20477
		private Dictionary<DesireType, float> ParamTable = new Dictionary<DesireType, float>();

		// Token: 0x04004FFE RID: 20478
		private List<int> PriorityList = new List<int>();

		// Token: 0x04004FFF RID: 20479
		private Dictionary<int, DesireType> PriorityTable = new Dictionary<int, DesireType>();

		// Token: 0x04005000 RID: 20480
		private Dictionary<DesireType, int> SpanTable = new Dictionary<DesireType, int>();

		// Token: 0x04005001 RID: 20481
		private Dictionary<DesireType, Tuple<float, float>> BorderTable = new Dictionary<DesireType, Tuple<float, float>>();

		// Token: 0x04005002 RID: 20482
		private Dictionary<AnimalState, Dictionary<DesireType, float>> RateTable = new Dictionary<AnimalState, Dictionary<DesireType, float>>();

		// Token: 0x04005004 RID: 20484
		private Dictionary<DesireType, Dictionary<bool, Dictionary<DesireType, ChangeParamState>>> ResultTable = new Dictionary<DesireType, Dictionary<bool, Dictionary<DesireType, ChangeParamState>>>();
	}
}
