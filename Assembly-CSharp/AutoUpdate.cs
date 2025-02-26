using System;
using UnityEngine;

// Token: 0x02000A3B RID: 2619
[RequireComponent(typeof(ImplicitSurfaceMeshCreaterBase))]
public class AutoUpdate : MonoBehaviour
{
	// Token: 0x06004DFF RID: 19967 RVA: 0x001DDC54 File Offset: 0x001DC054
	private void Awake()
	{
		this._surface = base.GetComponent<ImplicitSurfaceMeshCreaterBase>();
	}

	// Token: 0x06004E00 RID: 19968 RVA: 0x001DDC62 File Offset: 0x001DC062
	private void Update()
	{
		this._surface.CreateMesh();
	}

	// Token: 0x04004738 RID: 18232
	private ImplicitSurfaceMeshCreaterBase _surface;
}
