using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

// Token: 0x02000413 RID: 1043
[TestFixture]
public class NavMeshSurfaceTests
{
	// Token: 0x060012F7 RID: 4855 RVA: 0x00074C6C File Offset: 0x0007306C
	[SetUp]
	public void CreatePlaneWithSurface()
	{
		this.plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		this.surface = new GameObject().AddComponent<NavMeshSurface>();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x00074C96 File Offset: 0x00073096
	[TearDown]
	public void DestroyPlaneWithSurface()
	{
		UnityEngine.Object.DestroyImmediate(this.plane);
		UnityEngine.Object.DestroyImmediate(this.surface.gameObject);
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x00074CBF File Offset: 0x000730BF
	[Test]
	public void NavMeshIsAvailableAfterBuild()
	{
		this.surface.BuildNavMesh();
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x00074CD8 File Offset: 0x000730D8
	[Test]
	public void NavMeshCanBeRemovedAndAdded()
	{
		this.surface.BuildNavMesh();
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		this.surface.RemoveData();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		this.surface.AddData();
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x00074D2A File Offset: 0x0007312A
	[Test]
	public void NavMeshIsNotAvailableWhenDisabled()
	{
		this.surface.BuildNavMesh();
		this.surface.enabled = false;
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		this.surface.enabled = true;
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x00074D68 File Offset: 0x00073168
	[Test]
	public void CanBuildWithCustomArea()
	{
		this.surface.defaultArea = 4;
		int areaMask = 16;
		this.surface.BuildNavMesh();
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(areaMask, 0));
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x00074D9B File Offset: 0x0007319B
	[Test]
	public void CanBuildWithCustomAgentTypeID()
	{
		this.surface.agentTypeID = 1234;
		this.surface.BuildNavMesh();
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 1234));
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x00074DC8 File Offset: 0x000731C8
	[Test]
	public void CanBuildCollidersAndIgnoreRenderMeshes()
	{
		this.plane.GetComponent<MeshRenderer>().enabled = false;
		this.surface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
		this.surface.BuildNavMesh();
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		this.surface.useGeometry = NavMeshCollectGeometry.RenderMeshes;
		this.surface.BuildNavMesh();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x00074E2C File Offset: 0x0007322C
	[Test]
	public void CanBuildRenderMeshesAndIgnoreColliders()
	{
		this.plane.GetComponent<Collider>().enabled = false;
		this.surface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
		this.surface.BuildNavMesh();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		this.surface.useGeometry = NavMeshCollectGeometry.RenderMeshes;
		this.surface.BuildNavMesh();
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x00074E90 File Offset: 0x00073290
	[Test]
	public void BuildIgnoresGeometryOutsideBounds()
	{
		this.surface.collectObjects = CollectObjects.Volume;
		this.surface.center = new Vector3(20f, 0f, 0f);
		this.surface.size = new Vector3(10f, 10f, 10f);
		this.surface.BuildNavMesh();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x00074EFE File Offset: 0x000732FE
	[Test]
	public void BuildIgnoresGeometrySiblings()
	{
		this.surface.collectObjects = CollectObjects.Children;
		this.surface.BuildNavMesh();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x00074F23 File Offset: 0x00073323
	[Test]
	public void BuildUsesOnlyIncludedLayers()
	{
		this.plane.layer = 4;
		this.surface.layerMask = -17;
		this.surface.BuildNavMesh();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x00074F5C File Offset: 0x0007335C
	[Test]
	public void DefaultSettingsMatchBuiltinSettings()
	{
		NavMeshBuildSettings buildSettings = this.surface.GetBuildSettings();
		Assert.AreEqual(NavMesh.GetSettingsByIndex(0), buildSettings);
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x00074F8C File Offset: 0x0007338C
	[Test]
	public void ActiveSurfacesContainsOnlyActiveAndEnabledSurface()
	{
		Assert.IsTrue(NavMeshSurface.activeSurfaces.Contains(this.surface));
		Assert.AreEqual(1, NavMeshSurface.activeSurfaces.Count);
		this.surface.enabled = false;
		Assert.IsFalse(NavMeshSurface.activeSurfaces.Contains(this.surface));
		Assert.AreEqual(0, NavMeshSurface.activeSurfaces.Count);
		this.surface.enabled = true;
		this.surface.gameObject.SetActive(false);
		Assert.IsFalse(NavMeshSurface.activeSurfaces.Contains(this.surface));
		Assert.AreEqual(0, NavMeshSurface.activeSurfaces.Count);
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x00075050 File Offset: 0x00073450
	[UnityTest]
	public IEnumerator NavMeshMovesToSurfacePositionNextFrame()
	{
		this.plane.transform.position = new Vector3(100f, 0f, 0f);
		this.surface.transform.position = new Vector3(100f, 0f, 0f);
		this.surface.BuildNavMesh();
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		this.surface.transform.position = Vector3.zero;
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		yield return null;
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		yield break;
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x0007506C File Offset: 0x0007346C
	[UnityTest]
	public IEnumerator UpdatingAndAddingNavMesh()
	{
		NavMeshData navmeshData = new NavMeshData();
		AsyncOperation oper = this.surface.UpdateNavMesh(navmeshData);
		Assert.IsFalse(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		do
		{
			yield return null;
		}
		while (!oper.isDone);
		this.surface.RemoveData();
		this.surface.navMeshData = navmeshData;
		this.surface.AddData();
		Assert.IsTrue(NavMeshSurfaceTests.HasNavMeshAtOrigin(-1, 0));
		yield break;
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x00075088 File Offset: 0x00073488
	public static bool HasNavMeshAtOrigin(int areaMask = -1, int agentTypeID = 0)
	{
		NavMeshHit navMeshHit = default(NavMeshHit);
		NavMeshQueryFilter filter = default(NavMeshQueryFilter);
		filter.areaMask = areaMask;
		filter.agentTypeID = agentTypeID;
		return NavMesh.SamplePosition(Vector3.zero, out navMeshHit, 0.1f, filter);
	}

	// Token: 0x04001534 RID: 5428
	private GameObject plane;

	// Token: 0x04001535 RID: 5429
	private NavMeshSurface surface;
}
