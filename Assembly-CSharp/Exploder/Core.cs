using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Exploder
{
	// Token: 0x0200039C RID: 924
	internal class Core : Singleton<Core>
	{
		// Token: 0x06001051 RID: 4177 RVA: 0x0005B554 File Offset: 0x00059954
		public void Initialize(ExploderObject exploder)
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			this.parameters = new ExploderParams(exploder);
			FragmentPool.Instance.Reset(this.parameters);
			this.frameWatch = new Stopwatch();
			this.explosionWatch = new Stopwatch();
			this.crackManager = new CrackManager(this);
			this.bakeSkinManager = new BakeSkinManager(this);
			this.queue = new ExploderQueue(this);
			this.tasks = new ExploderTask[6];
			this.tasks[1] = new Preprocess(this);
			if (this.parameters.ThreadOptions == ExploderObject.ThreadOptions.Disabled)
			{
				this.tasks[2] = new CutterST(this);
			}
			else
			{
				this.tasks[2] = new CutterMT(this);
			}
			this.tasks[3] = new IsolateMeshIslands(this);
			this.tasks[4] = new PostprocessExplode(this);
			this.tasks[5] = new PostprocessCrack(this);
			this.PreAllocateBuffers();
			this.audioSource = base.gameObject.AddComponent<AudioSource>();
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0005B657 File Offset: 0x00059A57
		public void Enqueue(ExploderObject exploderObject, ExploderObject.OnExplosion callback, bool crack, params GameObject[] obj)
		{
			this.queue.Enqueue(exploderObject, callback, crack, obj);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0005B66C File Offset: 0x00059A6C
		public void ExplodeCracked(GameObject obj, ExploderObject.OnExplosion callback)
		{
			if (obj != null)
			{
				long num = this.crackManager.Explode(obj);
				if (callback != null)
				{
					callback((float)num, ExploderObject.ExplosionState.ExplosionFinished);
				}
			}
			else
			{
				long num2 = this.crackManager.ExplodeAll();
				if (callback != null)
				{
					callback((float)num2, ExploderObject.ExplosionState.ExplosionFinished);
				}
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0005B6C4 File Offset: 0x00059AC4
		public void ExplodePartial(GameObject obj, Vector3 shotDir, Vector3 hitPosition, float bulletSize, ExploderObject.OnExplosion callback)
		{
			if (obj != null)
			{
				long num = this.crackManager.ExplodePartial(obj, shotDir, hitPosition, bulletSize);
				if (callback != null)
				{
					callback((float)num, ExploderObject.ExplosionState.ExplosionFinished);
				}
			}
			else
			{
				long num2 = this.crackManager.ExplodeAll();
				if (callback != null)
				{
					callback((float)num2, ExploderObject.ExplosionState.ExplosionFinished);
				}
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0005B721 File Offset: 0x00059B21
		public bool IsCracked(GameObject gm)
		{
			return this.crackManager.IsCracked(gm);
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0005B730 File Offset: 0x00059B30
		public void StartExplosionFromQueue(ExploderParams p)
		{
			this.parameters = p;
			this.processingFrames = 1;
			this.explosionWatch.Reset();
			this.explosionWatch.Start();
			AudioSource component = p.ExploderGameObject.GetComponent<AudioSource>();
			if (component)
			{
				ExploderUtils.CopyAudioSource(component, this.audioSource);
			}
			this.currTaskType = TaskType.Preprocess;
			this.InitTask(this.currTaskType);
			this.RunTask(this.currTaskType, 0f);
			if (this.parameters.ThreadOptions != ExploderObject.ThreadOptions.Disabled)
			{
				this.currTaskType = this.NextTask(this.currTaskType);
				if (this.currTaskType != TaskType.None)
				{
					this.InitTask(this.currTaskType);
					this.RunTask(this.currTaskType, this.parameters.FrameBudget);
				}
				else
				{
					this.explosionWatch.Stop();
					this.queue.OnExplosionFinished(this.parameters.id, this.explosionWatch.ElapsedMilliseconds);
				}
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0005B82C File Offset: 0x00059C2C
		public void Update()
		{
			this.frameWatch.Reset();
			this.frameWatch.Start();
			if (this.currTaskType != TaskType.None)
			{
				while ((float)this.frameWatch.ElapsedMilliseconds < this.parameters.FrameBudget)
				{
					if (this.RunTask(this.currTaskType, this.parameters.FrameBudget))
					{
						this.currTaskType = this.NextTask(this.currTaskType);
						if (this.currTaskType == TaskType.None)
						{
							this.explosionWatch.Stop();
							this.bakeSkinManager.Clear();
							this.queue.OnExplosionFinished(this.parameters.id, this.explosionWatch.ElapsedMilliseconds);
							return;
						}
						this.InitTask(this.currTaskType);
					}
				}
				this.processingFrames++;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0005B908 File Offset: 0x00059D08
		public override void OnDestroy()
		{
			base.OnDestroy();
			foreach (ExploderTask exploderTask in this.tasks)
			{
				if (exploderTask != null)
				{
					exploderTask.OnDestroy();
				}
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0005B948 File Offset: 0x00059D48
		private void PreAllocateBuffers()
		{
			this.meshSet = new HashSet<MeshObject>();
			for (int i = 0; i < 64; i++)
			{
				this.meshSet.Add(default(MeshObject));
			}
			this.InitTask(TaskType.Preprocess);
			this.RunTask(TaskType.Preprocess, 0f);
			if (this.meshSet.Count > 0)
			{
				this.InitTask(TaskType.ProcessCutter);
				this.RunTask(TaskType.ProcessCutter, 0f);
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0005B9C1 File Offset: 0x00059DC1
		private bool RunTask(TaskType taskType, float budget = 0f)
		{
			return this.tasks[(int)taskType].Run(budget);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0005B9D1 File Offset: 0x00059DD1
		private void InitTask(TaskType taskType)
		{
			this.tasks[(int)taskType].Init();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0005B9E0 File Offset: 0x00059DE0
		private TaskType NextTask(TaskType taskType)
		{
			switch (taskType)
			{
			case TaskType.Preprocess:
				if (this.meshSet.Count == 0)
				{
					return TaskType.None;
				}
				return TaskType.ProcessCutter;
			case TaskType.ProcessCutter:
				if (this.splitMeshIslands)
				{
					return TaskType.IsolateMeshIslands;
				}
				if (this.parameters.Crack)
				{
					return TaskType.PostprocessCrack;
				}
				return TaskType.PostprocessExplode;
			case TaskType.IsolateMeshIslands:
				if (this.parameters.Crack)
				{
					return TaskType.PostprocessCrack;
				}
				return TaskType.PostprocessExplode;
			case TaskType.PostprocessExplode:
				return TaskType.None;
			case TaskType.PostprocessCrack:
				return TaskType.None;
			default:
				return TaskType.None;
			}
		}

		// Token: 0x04001205 RID: 4613
		[NonSerialized]
		public ExploderParams parameters;

		// Token: 0x04001206 RID: 4614
		[NonSerialized]
		public ExploderQueue queue;

		// Token: 0x04001207 RID: 4615
		[NonSerialized]
		public Stopwatch explosionWatch;

		// Token: 0x04001208 RID: 4616
		[NonSerialized]
		public Stopwatch frameWatch;

		// Token: 0x04001209 RID: 4617
		[NonSerialized]
		public HashSet<MeshObject> meshSet;

		// Token: 0x0400120A RID: 4618
		[NonSerialized]
		public Dictionary<int, int> targetFragments;

		// Token: 0x0400120B RID: 4619
		[NonSerialized]
		public List<MeshObject> postList;

		// Token: 0x0400120C RID: 4620
		[NonSerialized]
		public List<Fragment> pool;

		// Token: 0x0400120D RID: 4621
		[NonSerialized]
		public AudioSource audioSource;

		// Token: 0x0400120E RID: 4622
		[NonSerialized]
		public CrackManager crackManager;

		// Token: 0x0400120F RID: 4623
		[NonSerialized]
		public int processingFrames;

		// Token: 0x04001210 RID: 4624
		[NonSerialized]
		public int poolIdx;

		// Token: 0x04001211 RID: 4625
		[NonSerialized]
		public bool splitMeshIslands;

		// Token: 0x04001212 RID: 4626
		[NonSerialized]
		public BakeSkinManager bakeSkinManager;

		// Token: 0x04001213 RID: 4627
		private ExploderTask[] tasks;

		// Token: 0x04001214 RID: 4628
		private TaskType currTaskType;

		// Token: 0x04001215 RID: 4629
		private bool initialized;
	}
}
