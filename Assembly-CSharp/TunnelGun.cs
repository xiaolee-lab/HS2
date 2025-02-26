using System;
using UnityEngine;

// Token: 0x02000A61 RID: 2657
public class TunnelGun : Weapon
{
	// Token: 0x06004EBD RID: 20157 RVA: 0x001E3464 File Offset: 0x001E1864
	protected override void DoShoot(DungeonControl2 dungeon, Vector3 from, Vector3 to)
	{
		this.weaponBody.transform.position = from;
		this.weaponBody.transform.LookAt(to, Vector3.up);
		dungeon.Attack(this.brush);
		UnityEngine.Object.Instantiate<GameObject>(this.hitPS.gameObject, to, Quaternion.identity);
		UnityEngine.Object.Instantiate<GameObject>(this.shootPS.gameObject, this.weaponBody.transform.position, this.weaponBody.transform.rotation);
	}

	// Token: 0x040047AE RID: 18350
	public ParticleSystem hitPS;

	// Token: 0x040047AF RID: 18351
	public ParticleSystem shootPS;
}
