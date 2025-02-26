using System;
using UnityEngine;

// Token: 0x02000A60 RID: 2656
public class SphereGun : Weapon
{
	// Token: 0x06004EBB RID: 20155 RVA: 0x001E3424 File Offset: 0x001E1824
	protected override void DoShoot(DungeonControl2 dungeon, Vector3 from, Vector3 to)
	{
		this.weaponBody.transform.position = to;
		dungeon.Attack(this.brush);
		UnityEngine.Object.Instantiate<GameObject>(this.hitPS.gameObject, to, Quaternion.identity);
	}

	// Token: 0x040047AD RID: 18349
	public ParticleSystem hitPS;
}
