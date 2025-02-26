using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

// Token: 0x02000410 RID: 1040
[TestFixture]
public class NavMeshSurfaceLinkTests
{
	// Token: 0x060012DD RID: 4829 RVA: 0x000741E4 File Offset: 0x000725E4
	[SetUp]
	public void CreatesPlanesAndLink()
	{
		this.plane1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
		this.plane2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
		this.plane1.transform.position = 11f * Vector3.right;
		this.surface = new GameObject().AddComponent<NavMeshSurface>();
		this.surface.BuildNavMesh();
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnecting(this.plane1, this.plane2, -1, 0));
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnecting(this.plane2, this.plane1, -1, 0));
		this.link = new GameObject().AddComponent<NavMeshLink>();
		this.link.startPoint = this.plane1.transform.position;
		this.link.endPoint = this.plane2.transform.position;
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnecting(this.plane1, this.plane2, -1, 0));
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnecting(this.plane2, this.plane1, -1, 0));
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x000742E9 File Offset: 0x000726E9
	[TearDown]
	public void DestroyPlanesAndLink()
	{
		UnityEngine.Object.DestroyImmediate(this.surface.gameObject);
		UnityEngine.Object.DestroyImmediate(this.link.gameObject);
		UnityEngine.Object.DestroyImmediate(this.plane1);
		UnityEngine.Object.DestroyImmediate(this.plane2);
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x00074321 File Offset: 0x00072721
	[Test]
	public void NavMeshLinkCanConnectTwoSurfaces()
	{
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnecting(this.plane1, this.plane2, -1, 0));
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x0007433B File Offset: 0x0007273B
	[Test]
	public void DisablingBidirectionalMakesTheLinkOneWay()
	{
		this.link.bidirectional = false;
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnecting(this.plane1, this.plane2, -1, 0));
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnecting(this.plane2, this.plane1, -1, 0));
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x0007437C File Offset: 0x0007277C
	[Test]
	public void ChangingAreaTypeCanBlockPath()
	{
		int areaMask = -17;
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnecting(this.plane1, this.plane2, areaMask, 0));
		this.link.area = 4;
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnecting(this.plane1, this.plane2, areaMask, 0));
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x000743C8 File Offset: 0x000727C8
	[Test]
	public void EndpointsMoveRelativeToLinkOnUpdate()
	{
		this.link.transform.position += Vector3.forward;
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, this.plane1.transform.position + Vector3.forward, -1, 0));
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, this.plane2.transform.position + Vector3.forward, -1, 0));
		this.link.UpdateLink();
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, this.plane1.transform.position + Vector3.forward, -1, 0));
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, this.plane2.transform.position + Vector3.forward, -1, 0));
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x000744C8 File Offset: 0x000728C8
	[UnityTest]
	public IEnumerator EndpointsMoveRelativeToLinkNextFrameWhenAutoUpdating()
	{
		this.link.transform.position += Vector3.forward;
		this.link.autoUpdate = true;
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, this.plane1.transform.position + Vector3.forward, -1, 0));
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, this.plane2.transform.position + Vector3.forward, -1, 0));
		yield return null;
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, this.plane1.transform.position + Vector3.forward, -1, 0));
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, this.plane2.transform.position + Vector3.forward, -1, 0));
		yield break;
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x000744E4 File Offset: 0x000728E4
	[Test]
	public void ChangingCostModifierAffectsRoute()
	{
		NavMeshLink navMeshLink = this.link;
		navMeshLink.startPoint = this.plane1.transform.position;
		navMeshLink.endPoint = this.plane2.transform.position + Vector3.forward;
		NavMeshLink navMeshLink2 = this.link.gameObject.AddComponent<NavMeshLink>();
		navMeshLink2.startPoint = this.plane1.transform.position;
		navMeshLink2.endPoint = this.plane2.transform.position - Vector3.forward;
		navMeshLink.costModifier = -1;
		navMeshLink2.costModifier = 100;
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, navMeshLink.endPoint, -1, 0));
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, navMeshLink2.endPoint, -1, 0));
		navMeshLink.costModifier = 100;
		navMeshLink2.costModifier = -1;
		Assert.IsFalse(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, navMeshLink.endPoint, -1, 0));
		Assert.IsTrue(NavMeshSurfaceLinkTests.HasPathConnectingViaPoint(this.plane1, this.plane2, navMeshLink2.endPoint, -1, 0));
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x0007460C File Offset: 0x00072A0C
	public static bool HasPathConnecting(GameObject a, GameObject b, int areaMask = -1, int agentTypeID = 0)
	{
		NavMeshPath navMeshPath = new NavMeshPath();
		NavMeshQueryFilter filter = default(NavMeshQueryFilter);
		filter.areaMask = areaMask;
		filter.agentTypeID = agentTypeID;
		NavMesh.CalculatePath(a.transform.position, b.transform.position, filter, navMeshPath);
		return navMeshPath.status == NavMeshPathStatus.PathComplete;
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x00074660 File Offset: 0x00072A60
	public static bool HasPathConnectingViaPoint(GameObject a, GameObject b, Vector3 point, int areaMask = -1, int agentTypeID = 0)
	{
		NavMeshPath navMeshPath = new NavMeshPath();
		NavMeshQueryFilter filter = default(NavMeshQueryFilter);
		filter.areaMask = areaMask;
		filter.agentTypeID = agentTypeID;
		NavMesh.CalculatePath(a.transform.position, b.transform.position, filter, navMeshPath);
		if (navMeshPath.status != NavMeshPathStatus.PathComplete)
		{
			return false;
		}
		for (int i = 0; i < navMeshPath.corners.Length; i++)
		{
			if (Vector3.Distance(navMeshPath.corners[i], point) < 0.1f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400152C RID: 5420
	public GameObject plane1;

	// Token: 0x0400152D RID: 5421
	public GameObject plane2;

	// Token: 0x0400152E RID: 5422
	public NavMeshLink link;

	// Token: 0x0400152F RID: 5423
	public NavMeshSurface surface;
}
