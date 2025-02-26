using System;
using UnityEngine;

// Token: 0x02000A62 RID: 2658
public abstract class Weapon : MonoBehaviour
{
	// Token: 0x06004EBF RID: 20159
	protected abstract void DoShoot(DungeonControl2 dungeon, Vector3 from, Vector3 to);

	// Token: 0x06004EC0 RID: 20160 RVA: 0x001E3377 File Offset: 0x001E1777
	public void Shoot(DungeonControl2 dungeon, Vector3 from, Vector3 to)
	{
		if (this.audio != null && this.shotAudio != null)
		{
			this.audio.PlayOneShot(this.shotAudio);
		}
		this.DoShoot(dungeon, from, to);
	}

	// Token: 0x06004EC1 RID: 20161 RVA: 0x001E33B8 File Offset: 0x001E17B8
	public void OnEquip()
	{
		this.animator.SetBool("EQUIP", true);
		if (this.audio != null && this.equipAudio != null)
		{
			this.audio.PlayOneShot(this.equipAudio);
		}
	}

	// Token: 0x06004EC2 RID: 20162 RVA: 0x001E3409 File Offset: 0x001E1809
	public void OnRemove()
	{
		this.animator.SetBool("EQUIP", false);
	}

	// Token: 0x040047B0 RID: 18352
	public GameObject weaponBody;

	// Token: 0x040047B1 RID: 18353
	public IMBrush brush;

	// Token: 0x040047B2 RID: 18354
	public Animator animator;

	// Token: 0x040047B3 RID: 18355
	public AudioSource audio;

	// Token: 0x040047B4 RID: 18356
	public AudioClip equipAudio;

	// Token: 0x040047B5 RID: 18357
	public AudioClip shotAudio;
}
