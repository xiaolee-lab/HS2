using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000411 RID: 1041
[TestFixture]
public class NavMeshSurfaceModifierTests
{
	// Token: 0x060012E8 RID: 4840 RVA: 0x000748B8 File Offset: 0x00072CB8
	[SetUp]
	public void CreatePlaneWithModifier()
	{
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
		this.surface = gameObject.AddComponent<NavMeshSurface>();
		this.modifier = gameObject.AddComponent<NavMeshModifier>();
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x000748E4 File Offset: 0x00072CE4
	[TearDown]
	public void DestroyPlaneWithModifier()
	{
		UnityEngine.Object.DestroyImmediate(this.modifier.gameObject);
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x000748F6 File Offset: 0x00072CF6
	[Test]
	public void ModifierIgnoreAffectsSelf()
	{
		this.modifier.ignoreFromBuild = true;
		this.surface.BuildNavMesh();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x0007491C File Offset: 0x00072D1C
	[Test]
	public void ModifierIgnoreAffectsChild()
	{
		this.modifier.ignoreFromBuild = true;
		this.modifier.GetComponent<MeshRenderer>().enabled = false;
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
		gameObject.transform.SetParent(this.modifier.transform);
		this.surface.BuildNavMesh();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		UnityEngine.Object.DestroyImmediate(gameObject);
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x00074980 File Offset: 0x00072D80
	[Test]
	public void ModifierIgnoreDoesNotAffectSibling()
	{
		this.modifier.ignoreFromBuild = true;
		this.modifier.GetComponent<MeshRenderer>().enabled = false;
		GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
		this.surface.BuildNavMesh();
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		UnityEngine.Object.DestroyImmediate(obj);
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x000749D0 File Offset: 0x00072DD0
	[Test]
	public void ModifierOverrideAreaAffectsSelf()
	{
		this.modifier.area = 4;
		this.modifier.overrideArea = true;
		this.surface.BuildNavMesh();
		int areaMask = 16;
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(areaMask, 0));
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x00074A10 File Offset: 0x00072E10
	[Test]
	public void ModifierOverrideAreaAffectsChild()
	{
		this.modifier.area = 4;
		this.modifier.overrideArea = true;
		this.modifier.GetComponent<MeshRenderer>().enabled = false;
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
		gameObject.transform.SetParent(this.modifier.transform);
		this.surface.BuildNavMesh();
		int areaMask = 16;
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(areaMask, 0));
		UnityEngine.Object.DestroyImmediate(gameObject);
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x00074A84 File Offset: 0x00072E84
	[Test]
	public void ModifierOverrideAreaDoesNotAffectSibling()
	{
		this.modifier.area = 4;
		this.modifier.overrideArea = true;
		this.modifier.GetComponent<MeshRenderer>().enabled = false;
		GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
		this.surface.BuildNavMesh();
		int areaMask = 1;
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(areaMask, 0));
		UnityEngine.Object.DestroyImmediate(obj);
	}

	// Token: 0x04001530 RID: 5424
	private NavMeshSurface surface;

	// Token: 0x04001531 RID: 5425
	private NavMeshModifier modifier;
}
