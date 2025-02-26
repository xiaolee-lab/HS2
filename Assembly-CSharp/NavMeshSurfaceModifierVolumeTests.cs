using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000412 RID: 1042
[TestFixture]
public class NavMeshSurfaceModifierVolumeTests
{
	// Token: 0x060012F1 RID: 4849 RVA: 0x00074AE8 File Offset: 0x00072EE8
	[SetUp]
	public void CreatePlaneAndModifierVolume()
	{
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
		this.surface = gameObject.AddComponent<NavMeshSurface>();
		this.modifier = new GameObject().AddComponent<NavMeshModifierVolume>();
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x00074B18 File Offset: 0x00072F18
	[TearDown]
	public void DestroyPlaneAndModifierVolume()
	{
		UnityEngine.Object.DestroyImmediate(this.surface.gameObject);
		UnityEngine.Object.DestroyImmediate(this.modifier.gameObject);
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x00074B3C File Offset: 0x00072F3C
	[Test]
	public void AreaAffectsNavMeshOverlapping()
	{
		this.modifier.center = Vector3.zero;
		this.modifier.size = Vector3.one;
		this.modifier.area = 4;
		this.surface.BuildNavMesh();
		int areaMask = 16;
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(areaMask, 0));
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x00074B90 File Offset: 0x00072F90
	[Test]
	public void AreaDoesNotAffectsNavMeshWhenNotOverlapping()
	{
		this.modifier.center = 1.1f * Vector3.right;
		this.modifier.size = Vector3.one;
		this.modifier.area = 4;
		this.surface.BuildNavMesh();
		int areaMask = 1;
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(areaMask, 0));
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x00074BEC File Offset: 0x00072FEC
	[Test]
	public void BuildUsesOnlyIncludedModifierVolume()
	{
		this.modifier.center = Vector3.zero;
		this.modifier.size = Vector3.one;
		this.modifier.area = 4;
		this.modifier.gameObject.layer = 7;
		this.surface.layerMask = -129;
		this.surface.BuildNavMesh();
		int areaMask = 1;
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(areaMask, 0));
	}

	// Token: 0x04001532 RID: 5426
	private NavMeshSurface surface;

	// Token: 0x04001533 RID: 5427
	private NavMeshModifierVolume modifier;
}
