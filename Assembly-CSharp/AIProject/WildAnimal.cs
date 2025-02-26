using System;
using System.Collections.Generic;
using AIProject.Animal;
using AIProject.Animal.Resources;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CF8 RID: 3320
	[TaskCategory("")]
	public class WildAnimal : AgentAction
	{
		// Token: 0x06006AD8 RID: 27352 RVA: 0x002DA794 File Offset: 0x002D8B94
		public override void OnStart()
		{
			base.OnStart();
			base.Agent.RuntimeDesire = Desire.Type.Animal;
			base.Agent.ChangeStaticNavMeshAgentAvoidance();
			this.animal = base.Agent.TargetInSightAnimal;
			if (this.animal == null)
			{
				return;
			}
			this.animal.SetSearchTargetEnabled(false, false);
			base.Agent.StateType = State.Type.Immobility;
			this.animal.BackupState = this.animal.CurrentState;
			Dictionary<int, Dictionary<int, AnimalPlayState>> withAgentAnimeTable = Singleton<Manager.Resources>.Instance.AnimalTable.WithAgentAnimeTable;
			Dictionary<int, AnimalPlayState> dictionary;
			if (withAgentAnimeTable.TryGetValue(this.animal.AnimalTypeID, out dictionary) && !dictionary.IsNullOrEmpty<int, AnimalPlayState>())
			{
				List<int> list = ListPool<int>.Get();
				list.AddRange(dictionary.Keys);
				int element = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				base.Agent.StartWithAnimalSequence(this.animal, element);
			}
		}

		// Token: 0x06006AD9 RID: 27353 RVA: 0x002DA87E File Offset: 0x002D8C7E
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.LivesWithAnimalSequence)
			{
				return TaskStatus.Running;
			}
			this.End();
			return TaskStatus.Success;
		}

		// Token: 0x06006ADA RID: 27354 RVA: 0x002DA89C File Offset: 0x002D8C9C
		private void End()
		{
			int desireKey = Desire.GetDesireKey(base.Agent.RuntimeDesire);
			if (desireKey != -1)
			{
				base.Agent.SetDesire(desireKey, 0f);
			}
			base.Agent.RuntimeDesire = Desire.Type.None;
			this.ReleaseAnimal();
		}

		// Token: 0x06006ADB RID: 27355 RVA: 0x002DA8E5 File Offset: 0x002D8CE5
		public override void OnEnd()
		{
			this.ReleaseAnimal();
			base.Agent.ClearItems();
			base.Agent.StopWithAnimalSequence();
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
			base.Agent.StateType = State.Type.Normal;
			base.OnEnd();
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x002DA920 File Offset: 0x002D8D20
		public override void OnBehaviorComplete()
		{
			this.ReleaseAnimal();
			base.OnBehaviorComplete();
		}

		// Token: 0x06006ADD RID: 27357 RVA: 0x002DA930 File Offset: 0x002D8D30
		private void ReleaseAnimal()
		{
			if (this.animal != null)
			{
				this.animal.SetImpossible(false, base.Agent);
				if (base.Agent.TargetInSightAnimal == this.animal)
				{
					base.Agent.TargetInSightAnimal = null;
					this.animal.SetSearchTargetEnabled(true, false);
				}
				this.animal = null;
			}
		}

		// Token: 0x04005A43 RID: 23107
		private AnimalBase animal;
	}
}
