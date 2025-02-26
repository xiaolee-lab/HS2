using System;
using System.Collections.Generic;

namespace Exploder
{
	// Token: 0x020003B2 RID: 946
	internal class IsolateMeshIslands : ExploderTask
	{
		// Token: 0x060010C2 RID: 4290 RVA: 0x000619CC File Offset: 0x0005FDCC
		public IsolateMeshIslands(Core Core) : base(Core)
		{
			this.islands = new List<MeshObject>();
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x000619E0 File Offset: 0x0005FDE0
		public override TaskType Type
		{
			get
			{
				return TaskType.IsolateMeshIslands;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000619E3 File Offset: 0x0005FDE3
		public override void Init()
		{
			base.Init();
			this.islands.Clear();
			this.core.poolIdx = 0;
			this.core.postList = new List<MeshObject>(this.core.meshSet);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00061A20 File Offset: 0x0005FE20
		public override bool Run(float frameBudget)
		{
			int count = this.core.postList.Count;
			while (this.core.poolIdx < count)
			{
				MeshObject item = this.core.postList[this.core.poolIdx];
				this.core.poolIdx++;
				bool flag = false;
				if (this.core.parameters.SplitMeshIslands || (item.option && item.option.SplitMeshIslands))
				{
					List<ExploderMesh> list = MeshUtils.IsolateMeshIslands(item.mesh);
					if (list != null)
					{
						flag = true;
						foreach (ExploderMesh mesh in list)
						{
							this.islands.Add(new MeshObject
							{
								mesh = mesh,
								material = item.material,
								transform = item.transform,
								original = item.original,
								skinnedOriginal = item.skinnedOriginal,
								parent = item.transform.parent,
								position = item.transform.position,
								rotation = item.transform.rotation,
								localScale = item.transform.localScale,
								option = item.option
							});
						}
					}
				}
				if (!flag)
				{
					this.islands.Add(item);
				}
				if ((float)base.Watch.ElapsedMilliseconds > frameBudget)
				{
					return false;
				}
			}
			this.core.postList = this.islands;
			base.Watch.Stop();
			return true;
		}

		// Token: 0x040012A5 RID: 4773
		private readonly List<MeshObject> islands;
	}
}
