using System;
using System.Collections.Generic;
using AIProject.Animal;
using AIProject.Animal.Resources;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000CD8 RID: 3288
	[TaskCategory("")]
	public class PetAnimal : AgentAction
	{
		// Token: 0x06006A4B RID: 27211 RVA: 0x002D3BF8 File Offset: 0x002D1FF8
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

		// Token: 0x06006A4C RID: 27212 RVA: 0x002D3CE2 File Offset: 0x002D20E2
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.LivesWithAnimalSequence)
			{
				return TaskStatus.Running;
			}
			this.End();
			return TaskStatus.Success;
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x002D3D00 File Offset: 0x002D2100
		private void End()
		{
			int desireKey = Desire.GetDesireKey(base.Agent.RuntimeDesire);
			if (desireKey != -1)
			{
				base.Agent.SetDesire(desireKey, 0f);
			}
			base.Agent.RuntimeDesire = Desire.Type.None;
			IPetAnimal petAnimal = this.animal as IPetAnimal;
			if (petAnimal != null)
			{
				AIProject.SaveData.AnimalData animalData = petAnimal.AnimalData;
				if (animalData != null)
				{
					animalData.AddFavorability(base.Agent.ID, 1f);
				}
			}
			this.ReleaseAnimal();
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x002D3D7F File Offset: 0x002D217F
		public override void OnEnd()
		{
			this.ReleaseAnimal();
			base.Agent.ClearItems();
			base.Agent.StopWithAnimalSequence();
			base.Agent.ChangeDynamicNavMeshAgentAvoidance();
			base.Agent.StateType = State.Type.Normal;
			base.OnEnd();
		}

		// Token: 0x06006A4F RID: 27215 RVA: 0x002D3DBA File Offset: 0x002D21BA
		public override void OnBehaviorComplete()
		{
			this.ReleaseAnimal();
			base.OnBehaviorComplete();
		}

		// Token: 0x06006A50 RID: 27216 RVA: 0x002D3DC8 File Offset: 0x002D21C8
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

		// Token: 0x040059E5 RID: 23013
		private AnimalBase animal;
	}
}
