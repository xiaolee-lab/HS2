using System;
using UnityEngine;

// Token: 0x02000A5A RID: 2650
public class DungeonControl : MonoBehaviour
{
	// Token: 0x06004EA4 RID: 20132 RVA: 0x001E2A64 File Offset: 0x001E0E64
	private void Start()
	{
		MeshCollider component = this.metaball.GetComponent<MeshCollider>();
		if (component != null)
		{
			component.sharedMesh = this.metaball.Mesh;
		}
	}

	// Token: 0x06004EA5 RID: 20133 RVA: 0x001E2A9A File Offset: 0x001E0E9A
	private void Update()
	{
	}

	// Token: 0x06004EA6 RID: 20134 RVA: 0x001E2A9C File Offset: 0x001E0E9C
	public void AddCell(Vector3 position, float size)
	{
		this.audioSource.Play();
		MetaballNode metaballNode = new GameObject("MetaballNode")
		{
			transform = 
			{
				parent = this.metaball.sourceRoot.transform,
				position = position,
				localScale = Vector3.one,
				localRotation = Quaternion.identity
			}
		}.AddComponent<MetaballNode>();
		metaballNode.baseRadius = size;
		this.metaball.CreateMesh();
		MeshCollider component = this.metaball.GetComponent<MeshCollider>();
		if (component != null)
		{
			component.sharedMesh = this.metaball.Mesh;
		}
		UnityEngine.Object.Instantiate<GameObject>(this.hitPS.gameObject, position, Quaternion.identity);
	}

	// Token: 0x04004791 RID: 18321
	public Camera myCamera;

	// Token: 0x04004792 RID: 18322
	public StaticMetaballSeed metaball;

	// Token: 0x04004793 RID: 18323
	public ParticleSystem hitPS;

	// Token: 0x04004794 RID: 18324
	public AudioSource audioSource;
}
