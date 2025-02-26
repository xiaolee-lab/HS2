using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B9C RID: 2972
	public abstract class ButterflyBase : AnimalBase
	{
		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x060058C3 RID: 22723 RVA: 0x002613E7 File Offset: 0x0025F7E7
		public bool HasParticle
		{
			[CompilerGenerated]
			get
			{
				return this.particle != null;
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x060058C4 RID: 22724 RVA: 0x002613F5 File Offset: 0x0025F7F5
		public override bool IsNeutralCommand
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x002613F8 File Offset: 0x0025F7F8
		protected override void OnDestroy()
		{
			this.Active = false;
			base.SetDestroyState();
			base.OnDestroy();
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0026140D File Offset: 0x0025F80D
		public override void Clear()
		{
			this.particle = null;
			base.Clear();
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x0026141C File Offset: 0x0025F81C
		public override void ReleaseBody()
		{
			this.particle = null;
			base.ReleaseBody();
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x0026142B File Offset: 0x0025F82B
		public override void CreateBody()
		{
			base.CreateBody();
			if (!this.bodyObject)
			{
				return;
			}
			this.particle = this.bodyObject.GetComponentInChildren<ParticleSystem>(true);
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x00261456 File Offset: 0x0025F856
		public void PlayParticle()
		{
			if (this.particle != null)
			{
				this.particle.Play(true);
			}
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x00261475 File Offset: 0x0025F875
		public void StopParticle()
		{
			if (this.particle != null)
			{
				this.particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x00261495 File Offset: 0x0025F895
		public override void SetState(AnimalState _nextState, Action _changeEvent = null)
		{
			base.CurrentState = _nextState;
			if (_changeEvent != null)
			{
				_changeEvent();
			}
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x002614AC File Offset: 0x0025F8AC
		public override void ChangeState(AnimalState _nextState, Action _changeEvent = null)
		{
			base.CurrentState = _nextState;
			if (_changeEvent != null)
			{
				_changeEvent();
			}
		}

		// Token: 0x04005143 RID: 20803
		protected ParticleSystem particle;
	}
}
