using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B99 RID: 2969
	public class StateCondition
	{
		// Token: 0x0600589C RID: 22684 RVA: 0x00260884 File Offset: 0x0025EC84
		public StateCondition()
		{
			this.conditionType = ConditionType.None;
			this.animalState = AnimalState.None;
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x002608B0 File Offset: 0x0025ECB0
		public StateCondition(ConditionType _type, AnimalState _state)
		{
			this.conditionType = _type;
			this.animalState = _state;
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x002608DC File Offset: 0x0025ECDC
		public StateCondition(StateCondition _c)
		{
			this.conditionType = _c.conditionType;
			this.animalState = _c.animalState;
			this.nextState.Clear();
			this.nextState.AddRange(_c.nextState);
			this.nextStateTemp.Clear();
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x0600589F RID: 22687 RVA: 0x00260944 File Offset: 0x0025ED44
		// (set) Token: 0x060058A0 RID: 22688 RVA: 0x0026094C File Offset: 0x0025ED4C
		public ConditionType conditionType { get; protected set; }

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x00260955 File Offset: 0x0025ED55
		// (set) Token: 0x060058A2 RID: 22690 RVA: 0x0026095D File Offset: 0x0025ED5D
		public AnimalState animalState { get; protected set; }

		// Token: 0x1700106E RID: 4206
		public OneState this[int index]
		{
			get
			{
				return this.nextState[index];
			}
			set
			{
				this.nextState[index] = value;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x060058A5 RID: 22693 RVA: 0x00260983 File Offset: 0x0025ED83
		public int Count
		{
			get
			{
				return this.nextState.Count;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x060058A6 RID: 22694 RVA: 0x00260990 File Offset: 0x0025ED90
		public OneState FirstElement
		{
			get
			{
				return (this.Count != 0) ? this.nextState[0] : default(OneState);
			}
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x002609C2 File Offset: 0x0025EDC2
		public void AddNextState(AnimalState _state, float _proportion = 0f)
		{
			this.AddNextState(new OneState(_state, _proportion));
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x002609D4 File Offset: 0x0025EDD4
		public void AddNextState(OneState _state)
		{
			if (_state.state == AnimalState.None)
			{
				return;
			}
			OneState item = this.nextState.Find((OneState x) => x.state == _state.state);
			if (item.state == AnimalState.None)
			{
				this.nextState.Add(_state);
			}
			else
			{
				int index = this.nextState.IndexOf(item);
				this.nextState[index] = _state;
			}
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x00260A58 File Offset: 0x0025EE58
		public void SetNextState(List<OneState> _nextState)
		{
			this.nextState.Clear();
			this.nextState.AddRange(_nextState);
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x00260A74 File Offset: 0x0025EE74
		public void RemoveNextState(AnimalState _state)
		{
			this.nextState.RemoveAll((OneState x) => x.state == _state);
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x00260AA8 File Offset: 0x0025EEA8
		public void RemoveNextState(OneState _state)
		{
			this.nextState.RemoveAll((OneState x) => x.Equal(_state));
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x00260ADC File Offset: 0x0025EEDC
		public OneState GetRandomState()
		{
			if (this.nextState.IsNullOrEmpty<OneState>())
			{
				return default(OneState);
			}
			return this.nextState[UnityEngine.Random.Range(0, this.nextState.Count)];
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x00260B20 File Offset: 0x0025EF20
		public OneState GetProportionState()
		{
			if (this.nextState.IsNullOrEmpty<OneState>())
			{
				return default(OneState);
			}
			float num = 0f;
			this.nextStateTemp.Clear();
			this.nextStateTemp.AddRange(this.nextState);
			for (int i = 0; i < this.nextStateTemp.Count; i++)
			{
				num += this.nextStateTemp[i].proportion;
				OneState value = this.nextStateTemp[i];
				value.proportion = num;
				this.nextStateTemp[i] = value;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			for (int j = 0; j < this.nextStateTemp.Count; j++)
			{
				if (num2 <= this.nextStateTemp[j].proportion)
				{
					return this.nextStateTemp[j];
				}
			}
			return default(OneState);
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x00260C24 File Offset: 0x0025F024
		public OneState GetNonOverlapState()
		{
			if (this.nextState.IsNullOrEmpty<OneState>())
			{
				return default(OneState);
			}
			int index = UnityEngine.Random.Range(0, this.Count);
			OneState result = this.nextState[index];
			this.nextState.RemoveAt(index);
			return result;
		}

		// Token: 0x060058AF RID: 22703 RVA: 0x00260C72 File Offset: 0x0025F072
		public List<OneState> AllState(bool _copy = true)
		{
			return (!_copy) ? this.nextState : new List<OneState>(this.nextState);
		}

		// Token: 0x0400513C RID: 20796
		private List<OneState> nextState = new List<OneState>();

		// Token: 0x0400513D RID: 20797
		private List<OneState> nextStateTemp = new List<OneState>();
	}
}
