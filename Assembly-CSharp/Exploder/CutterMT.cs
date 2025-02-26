using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003AD RID: 941
	internal class CutterMT : CutterST
	{
		// Token: 0x060010A5 RID: 4261 RVA: 0x00060EA8 File Offset: 0x0005F2A8
		public CutterMT(Core Core) : base(Core)
		{
			this.splitIDs = new int[2];
			this.THREAD_MAX = Mathf.Clamp((int)(Core.parameters.ThreadOptions + 2), 1, 4);
			UnityEngine.Debug.LogFormat("Exploder: using {0} threads.", new object[]
			{
				this.THREAD_MAX - 1
			});
			this.workers = new CutterWorker[this.THREAD_MAX - 1];
			for (int i = 0; i < this.THREAD_MAX - 1; i++)
			{
				this.workers[i] = new CutterWorker(Core, new CuttingPlane(Core));
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00060F4D File Offset: 0x0005F34D
		public override TaskType Type
		{
			get
			{
				return TaskType.ProcessCutter;
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00060F50 File Offset: 0x0005F350
		public override void Init()
		{
			base.Init();
			this.cutInitialized = false;
			foreach (CutterWorker cutterWorker in this.workers)
			{
				cutterWorker.Init();
			}
			this.localWatch.Reset();
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00060F9C File Offset: 0x0005F39C
		public override void OnDestroy()
		{
			foreach (CutterWorker cutterWorker in this.workers)
			{
				cutterWorker.Terminate();
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00060FD0 File Offset: 0x0005F3D0
		public override bool Run(float frameBudget)
		{
			bool flag = this.Cut(frameBudget);
			if (flag)
			{
				bool flag2 = true;
				foreach (CutterWorker cutterWorker in this.workers)
				{
					flag2 &= cutterWorker.IsFinished();
				}
				if (flag2)
				{
					foreach (CutterWorker cutterWorker2 in this.workers)
					{
						this.core.meshSet.UnionWith(cutterWorker2.GetResults());
					}
					base.Watch.Stop();
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00061070 File Offset: 0x0005F470
		protected override bool Cut(float frameBudget)
		{
			if (this.cutInitialized)
			{
				return true;
			}
			this.localWatch.Start();
			this.cutInitialized = true;
			if (this.core.parameters.TargetFragments < 2 || this.core.meshSet.Count == this.core.parameters.TargetFragments)
			{
				return true;
			}
			int i = this.THREAD_MAX - 1 - this.core.meshSet.Count;
			if (i > this.core.parameters.TargetFragments - 1)
			{
				i = this.core.parameters.TargetFragments - 1;
			}
			int num = 0;
			while (i > 0)
			{
				this.newFragments.Clear();
				foreach (MeshObject meshObject in this.core.meshSet)
				{
					List<ExploderMesh> list = base.CutSingleMesh(meshObject);
					num++;
					if (num > this.core.parameters.TargetFragments)
					{
						i = 0;
						break;
					}
					if (list != null)
					{
						i--;
						int[] array = this.SplitMeshTargetFragments(meshObject.id);
						int num2 = 0;
						foreach (ExploderMesh mesh in list)
						{
							MeshObject item = new MeshObject
							{
								mesh = mesh,
								material = meshObject.material,
								transform = meshObject.transform,
								id = meshObject.id,
								original = meshObject.original,
								skinnedOriginal = meshObject.skinnedOriginal,
								bakeObject = meshObject.bakeObject,
								parent = meshObject.transform.parent,
								position = meshObject.transform.position,
								rotation = meshObject.transform.rotation,
								localScale = meshObject.transform.localScale,
								option = meshObject.option
							};
							item.id = array[num2++];
							this.newFragments.Add(item);
						}
						this.meshToRemove.Add(meshObject);
						break;
					}
				}
				this.core.meshSet.ExceptWith(this.meshToRemove);
				this.core.meshSet.UnionWith(this.newFragments);
			}
			if (this.core.meshSet.Count >= this.core.parameters.TargetFragments)
			{
				return true;
			}
			int num3 = this.core.meshSet.Count / (this.THREAD_MAX - 1);
			int num4 = 0;
			int num5 = 0;
			foreach (MeshObject meshObject2 in this.core.meshSet)
			{
				this.workers[num4].AddMesh(meshObject2);
				num5++;
				if (num5 >= num3 && num4 < this.THREAD_MAX - 2)
				{
					num5 = 0;
					num4++;
				}
			}
			this.core.meshSet.Clear();
			foreach (CutterWorker cutterWorker in this.workers)
			{
				cutterWorker.Run();
			}
			this.localWatch.Stop();
			return true;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00061468 File Offset: 0x0005F868
		private int[] SplitMeshTargetFragments(int id)
		{
			int num = this.core.targetFragments[id] / 2;
			int num2 = num;
			if (this.core.targetFragments[id] % 2 == 1)
			{
				num2++;
			}
			this.splitIDs[0] = (id + 1) * 100;
			this.splitIDs[1] = (id + 1) * 200;
			this.core.targetFragments.Add(this.splitIDs[0], num);
			this.core.targetFragments.Add(this.splitIDs[1], num2);
			return this.splitIDs;
		}

		// Token: 0x04001287 RID: 4743
		protected readonly int THREAD_MAX;

		// Token: 0x04001288 RID: 4744
		protected readonly CutterWorker[] workers;

		// Token: 0x04001289 RID: 4745
		private readonly int[] splitIDs;

		// Token: 0x0400128A RID: 4746
		private readonly Stopwatch localWatch = new Stopwatch();

		// Token: 0x0400128B RID: 4747
		private bool cutInitialized;
	}
}
