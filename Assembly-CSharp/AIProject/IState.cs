using System;
using System.Collections;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000E14 RID: 3604
	public interface IState
	{
		// Token: 0x06006F81 RID: 28545
		void Awake(Actor actor);

		// Token: 0x06006F82 RID: 28546
		void Release(Actor actor, EventType type);

		// Token: 0x06006F83 RID: 28547
		void AfterUpdate(Actor actor, Actor.InputInfo info);

		// Token: 0x06006F84 RID: 28548
		void Update(Actor actor, ref Actor.InputInfo info);

		// Token: 0x06006F85 RID: 28549
		void FixedUpdate(Actor actor, Actor.InputInfo info);

		// Token: 0x06006F86 RID: 28550
		void OnAnimatorStateEnter(ActorController control, AnimatorStateInfo stateInfo);

		// Token: 0x06006F87 RID: 28551
		void OnAnimatorStateExit(ActorController control, AnimatorStateInfo stateInfo);

		// Token: 0x06006F88 RID: 28552
		IEnumerator OnEnumerate(Actor actor);

		// Token: 0x06006F89 RID: 28553
		IEnumerator End(Actor actor);
	}
}
