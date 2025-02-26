using System;
using System.Collections;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E15 RID: 3605
	public abstract class StateBase : IState
	{
		// Token: 0x06006F8B RID: 28555
		public abstract void Awake(Actor actor);

		// Token: 0x06006F8C RID: 28556
		public abstract void Release(Actor actor, EventType type);

		// Token: 0x06006F8D RID: 28557
		public abstract void AfterUpdate(Actor actor, Actor.InputInfo info);

		// Token: 0x06006F8E RID: 28558
		public abstract void Update(Actor actor, ref Actor.InputInfo info);

		// Token: 0x06006F8F RID: 28559
		public abstract void FixedUpdate(Actor actor, Actor.InputInfo info);

		// Token: 0x06006F90 RID: 28560
		public abstract void OnAnimatorStateEnter(ActorController control, AnimatorStateInfo stateInfo);

		// Token: 0x06006F91 RID: 28561
		public abstract void OnAnimatorStateExit(ActorController control, AnimatorStateInfo stateInfo);

		// Token: 0x06006F92 RID: 28562 RVA: 0x002EB244 File Offset: 0x002E9644
		public virtual IEnumerator OnEnumerate(Actor actor)
		{
			yield return null;
			yield break;
		}

		// Token: 0x06006F93 RID: 28563
		public abstract IEnumerator End(Actor actor);

		// Token: 0x04005C17 RID: 23575
		public const string NormalAnimationStateName = "Locomotion";
	}
}
