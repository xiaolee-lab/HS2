using System;
using UnityEngine;

// Token: 0x02000A5D RID: 2653
public class DungeonControl2 : MonoBehaviour
{
	// Token: 0x06004EAF RID: 20143 RVA: 0x001E2ECC File Offset: 0x001E12CC
	private void Start()
	{
		MeshCollider component = this.metaball.GetComponent<MeshCollider>();
		if (component != null)
		{
			component.sharedMesh = this.metaball.Mesh;
		}
	}

	// Token: 0x06004EB0 RID: 20144 RVA: 0x001E2F02 File Offset: 0x001E1302
	private void Update()
	{
	}

	// Token: 0x06004EB1 RID: 20145 RVA: 0x001E2F04 File Offset: 0x001E1304
	public void Attack(IMBrush brush)
	{
		this.audioSource.Play();
		brush.Draw();
		MeshCollider component = this.metaball.GetComponent<MeshCollider>();
		if (component != null)
		{
			component.sharedMesh = this.metaball.Mesh;
		}
	}

	// Token: 0x0400479D RID: 18333
	public Camera myCamera;

	// Token: 0x0400479E RID: 18334
	public IncrementalModeling metaball;

	// Token: 0x0400479F RID: 18335
	public AudioSource audioSource;
}
