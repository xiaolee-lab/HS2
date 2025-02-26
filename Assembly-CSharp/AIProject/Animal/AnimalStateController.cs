using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;

namespace AIProject.Animal
{
	// Token: 0x02000B9A RID: 2970
	public class AnimalStateController
	{
		// Token: 0x060058B0 RID: 22704 RVA: 0x00260CDE File Offset: 0x0025F0DE
		public AnimalStateController()
		{
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x00260D07 File Offset: 0x0025F107
		public AnimalStateController(AnimalBase _animal)
		{
			this.Initialize(_animal);
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x060058B2 RID: 22706 RVA: 0x00260D37 File Offset: 0x0025F137
		// (set) Token: 0x060058B3 RID: 22707 RVA: 0x00260D3F File Offset: 0x0025F13F
		public AnimalBase Animal { get; private set; }

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x00260D48 File Offset: 0x0025F148
		public AnimalTypes AnimalType
		{
			[CompilerGenerated]
			get
			{
				return (!(this.Animal != null)) ? ((AnimalTypes)0) : this.Animal.AnimalType;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x060058B5 RID: 22709 RVA: 0x00260D6C File Offset: 0x0025F16C
		public BreedingTypes BreedingType
		{
			[CompilerGenerated]
			get
			{
				return (!(this.Animal != null)) ? BreedingTypes.Wild : this.Animal.BreedingType;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x060058B6 RID: 22710 RVA: 0x00260D90 File Offset: 0x0025F190
		protected Resources.AnimalTables AnimalTable
		{
			[CompilerGenerated]
			get
			{
				return (!Singleton<Resources>.IsInstance()) ? null : Singleton<Resources>.Instance.AnimalTable;
			}
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x00260DAC File Offset: 0x0025F1AC
		public void Initialize(AnimalBase _animal)
		{
			this.Clear();
			this.Animal = _animal;
			if (this.Animal == null)
			{
				return;
			}
			this.TableSetting();
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x00260DD4 File Offset: 0x0025F1D4
		protected void TableSetting(AnimalTypes _animalType, BreedingTypes _breedingType)
		{
			this.StateConditionTable.Clear();
			this.StateNonOverlapTable.Clear();
			Dictionary<AnimalState, StateCondition> dictionary;
			if (this.AnimalTable.StateConditionTable.ContainsKey(_animalType) && this.AnimalTable.StateConditionTable[_animalType].TryGetValue(_breedingType, out dictionary))
			{
				foreach (KeyValuePair<AnimalState, StateCondition> keyValuePair in dictionary)
				{
					this.StateConditionTable[keyValuePair.Key] = new StateCondition(keyValuePair.Value);
				}
			}
			this.TargetActionTable.Clear();
			Dictionary<AnimalState, List<ActionTypes>> dictionary2;
			if (this.AnimalTable.StateTargetActionTable.ContainsKey(_animalType) && this.AnimalTable.StateTargetActionTable[_animalType].TryGetValue(_breedingType, out dictionary2))
			{
				foreach (KeyValuePair<AnimalState, List<ActionTypes>> keyValuePair2 in dictionary2)
				{
					this.TargetActionTable[keyValuePair2.Key] = new List<ActionTypes>(keyValuePair2.Value);
				}
			}
		}

		// Token: 0x060058B9 RID: 22713 RVA: 0x00260F2C File Offset: 0x0025F32C
		protected void TableSetting()
		{
			this.TableSetting(this.AnimalType, this.BreedingType);
		}

		// Token: 0x060058BA RID: 22714 RVA: 0x00260F40 File Offset: 0x0025F340
		public List<ActionTypes> GetActionList(AnimalState _state)
		{
			List<ActionTypes> list;
			return (!this.TargetActionTable.TryGetValue(_state, out list)) ? null : list;
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x00260F67 File Offset: 0x0025F367
		public void Clear()
		{
			this.Animal = null;
			this.StateConditionTable.Clear();
			this.StateNonOverlapTable.Clear();
			this.TargetActionTable.Clear();
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x00260F94 File Offset: 0x0025F394
		public bool ChangeState(AnimalState _nextState = AnimalState.None)
		{
			if (this.Animal == null)
			{
				return false;
			}
			AnimalState currentState = this.Animal.CurrentState;
			AnimalState prevState = this.Animal.PrevState;
			if (!this.StateConditionTable.ContainsKey(currentState))
			{
				if (_nextState != AnimalState.None)
				{
					this.Animal.CurrentState = _nextState;
				}
				return false;
			}
			StateCondition stateCondition = this.StateConditionTable[currentState];
			ConditionType conditionType = stateCondition.conditionType;
			if (conditionType == ConditionType.None)
			{
				if (_nextState != AnimalState.None)
				{
					this.Animal.CurrentState = _nextState;
				}
				return false;
			}
			if (conditionType == ConditionType.NonOverlap)
			{
				if (!this.StateNonOverlapTable.ContainsKey(currentState))
				{
					this.StateNonOverlapTable[currentState] = new StateCondition(stateCondition);
				}
				else if (stateCondition.Count <= 0)
				{
					stateCondition.SetNextState(this.StateNonOverlapTable[currentState].AllState(false));
				}
			}
			bool flag = false;
			switch (conditionType)
			{
			case ConditionType.Forced:
				flag = this.SetCurrentState(stateCondition.FirstElement);
				break;
			case ConditionType.Proportion:
				flag = this.SetCurrentState(stateCondition.GetProportionState());
				break;
			case ConditionType.Random:
				flag = this.SetCurrentState(stateCondition.GetRandomState());
				break;
			case ConditionType.NonOverlap:
				flag = this.SetCurrentState(stateCondition.GetNonOverlapState());
				break;
			}
			if (flag)
			{
				return true;
			}
			if (_nextState != AnimalState.None)
			{
				this.Animal.CurrentState = _nextState;
			}
			return false;
		}

		// Token: 0x060058BD RID: 22717 RVA: 0x002610FA File Offset: 0x0025F4FA
		public void SetState(AnimalState _nextState)
		{
			if (this.Animal == null)
			{
				return;
			}
			this.Animal.CurrentState = _nextState;
		}

		// Token: 0x060058BE RID: 22718 RVA: 0x0026111A File Offset: 0x0025F51A
		protected bool SetCurrentState(AnimalState _state)
		{
			if (this.Animal == null)
			{
				return false;
			}
			if (_state == AnimalState.None)
			{
				return false;
			}
			this.Animal.CurrentState = _state;
			return true;
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x00261144 File Offset: 0x0025F544
		protected bool SetCurrentState(OneState _state)
		{
			return this.SetCurrentState(_state.state);
		}

		// Token: 0x0400513F RID: 20799
		protected Dictionary<AnimalState, StateCondition> StateConditionTable = new Dictionary<AnimalState, StateCondition>();

		// Token: 0x04005140 RID: 20800
		protected Dictionary<AnimalState, StateCondition> StateNonOverlapTable = new Dictionary<AnimalState, StateCondition>();

		// Token: 0x04005141 RID: 20801
		public Dictionary<AnimalState, List<ActionTypes>> TargetActionTable = new Dictionary<AnimalState, List<ActionTypes>>();
	}
}
