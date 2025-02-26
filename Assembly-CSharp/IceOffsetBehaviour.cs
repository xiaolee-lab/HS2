using System;
using UnityEngine;

// Token: 0x02000442 RID: 1090
public class IceOffsetBehaviour : MonoBehaviour
{
	// Token: 0x060013F8 RID: 5112 RVA: 0x0007CBAC File Offset: 0x0007AFAC
	private void Start()
	{
		FadeInOutShaderFloat component = base.GetComponent<FadeInOutShaderFloat>();
		if (component == null)
		{
			return;
		}
		Transform parent = base.transform.parent;
		SkinnedMeshRenderer component2 = parent.GetComponent<SkinnedMeshRenderer>();
		Mesh sharedMesh;
		if (component2 != null)
		{
			sharedMesh = component2.sharedMesh;
		}
		else
		{
			MeshFilter component3 = parent.GetComponent<MeshFilter>();
			if (component3 == null)
			{
				return;
			}
			sharedMesh = component3.sharedMesh;
		}
		if (!sharedMesh.isReadable)
		{
			component.MaxFloat = 0f;
			return;
		}
		int num = sharedMesh.triangles.Length;
		if (num < 1000)
		{
			if (num > 500)
			{
				component.MaxFloat = (float)num / 1000f - 0.5f;
			}
			else
			{
				component.MaxFloat = 0f;
			}
		}
	}

	// Token: 0x060013F9 RID: 5113 RVA: 0x0007CC74 File Offset: 0x0007B074
	private void Update()
	{
	}
}
