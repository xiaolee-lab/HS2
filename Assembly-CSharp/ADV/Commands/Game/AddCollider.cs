using System;
using UnityEngine;

namespace ADV.Commands.Game
{
	// Token: 0x02000743 RID: 1859
	public class AddCollider : CommandBase
	{
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000FD986 File Offset: 0x000FBD86
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x000FD989 File Offset: 0x000FBD89
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000FD98C File Offset: 0x000FBD8C
		public override void Do()
		{
			base.Do();
			GameObject gameObject = base.scenario.currentChara.transform.gameObject;
			CapsuleCollider capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
			capsuleCollider.center = new Vector3(0f, 0.75f, 0f);
			capsuleCollider.radius = 0.5f;
			capsuleCollider.height = 1.5f;
			capsuleCollider.isTrigger = true;
			gameObject.AddComponent<Rigidbody>();
		}
	}
}
