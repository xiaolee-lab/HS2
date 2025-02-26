using System;
using System.Collections.Generic;
using AIProject;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000FC9 RID: 4041
public class ModifiedShadow : Shadow
{
	// Token: 0x0600864A RID: 34378 RVA: 0x0037426C File Offset: 0x0037266C
	public override void ModifyMesh(VertexHelper vh)
	{
		if (!this.IsActive())
		{
			return;
		}
		List<UIVertex> list = ListPool<UIVertex>.Get();
		vh.GetUIVertexStream(list);
		this.ModifyVertices(list);
		vh.Clear();
		vh.AddUIVertexTriangleStream(list);
		ListPool<UIVertex>.Release(list);
	}

	// Token: 0x0600864B RID: 34379 RVA: 0x003742AC File Offset: 0x003726AC
	public virtual void ModifyVertices(List<UIVertex> verts)
	{
	}
}
