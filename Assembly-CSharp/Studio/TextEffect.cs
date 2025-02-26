using System;
using System.Collections.Generic;
using GUITree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001357 RID: 4951
	public abstract class TextEffect : UIBehaviour, IMeshModifier
	{
		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x0600A5FC RID: 42492 RVA: 0x004392BE File Offset: 0x004376BE
		public Graphic graphic
		{
			get
			{
				if (this.m_Graphic == null)
				{
					this.m_Graphic = base.GetComponent<Graphic>();
				}
				return this.m_Graphic;
			}
		}

		// Token: 0x0600A5FD RID: 42493 RVA: 0x004392E3 File Offset: 0x004376E3
		public void ModifyMesh(Mesh mesh)
		{
		}

		// Token: 0x0600A5FE RID: 42494 RVA: 0x004392E8 File Offset: 0x004376E8
		public void ModifyMesh(VertexHelper verts)
		{
			List<UIVertex> list = ListPool<UIVertex>.Get();
			verts.GetUIVertexStream(list);
			this.Modify(ref list);
			verts.Clear();
			verts.AddUIVertexTriangleStream(list);
			ListPool<UIVertex>.Release(list);
		}

		// Token: 0x0600A5FF RID: 42495
		protected abstract void Modify(ref List<UIVertex> _stream);

		// Token: 0x04008273 RID: 33395
		private Graphic m_Graphic;
	}
}
