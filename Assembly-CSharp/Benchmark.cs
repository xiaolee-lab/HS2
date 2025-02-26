using System;
using System.Collections.Generic;
using System.IO;
using Exploder;
using Exploder.Utils;
using UnityEngine;

// Token: 0x02000379 RID: 889
public class Benchmark : MonoBehaviour
{
	// Token: 0x06000FC0 RID: 4032 RVA: 0x0005873C File Offset: 0x00056B3C
	private void AddReport(Benchmark.Report r)
	{
		if (this.report.ContainsKey(r.name))
		{
			this.report[r.name].ms += r.ms;
			this.report[r.name].count++;
			this.report[r.name].frames += r.frames;
		}
		else
		{
			this.report.Add(r.name, r);
		}
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x000587DC File Offset: 0x00056BDC
	private void PrintReport()
	{
		UnityEngine.Debug.Log("PrintReportTotal");
		string text = "Report:\n\n";
		foreach (Benchmark.Report report in this.report.Values)
		{
			string str = string.Format("{0}: {1}[ms] {2}[frames]", report.name, report.ms / (float)report.count, report.frames / report.count);
			text = text + str + "\n";
		}
		File.WriteAllText("c:\\Development\\Unity\\AssetStore\\exploder-git\\benchmark.txt", text);
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x00058894 File Offset: 0x00056C94
	private void Start()
	{
		this.objects = this.testObjects.GetComponentsInChildren<MeshRenderer>(true);
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x000588A8 File Offset: 0x00056CA8
	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 100f, 50f), "Start"))
		{
			this.ExplodeObject();
		}
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x000588D8 File Offset: 0x00056CD8
	private void ExplodeObject()
	{
		if (this.rounds == 0)
		{
			this.batchIndex++;
			this.rounds = 5;
			if (this.batchIndex >= this.batches.Length)
			{
				this.PrintReport();
				return;
			}
		}
		int targetFragments = this.batches[this.batchIndex];
		ExploderSingleton.Instance.TargetFragments = targetFragments;
		if (this.index >= this.objects.Length)
		{
			FragmentPool.Instance.DeactivateFragments();
			this.rounds--;
			this.index = 0;
		}
		this.objects[this.index].gameObject.SetActive(true);
		ExploderSingleton.Instance.ExplodeObject(this.objects[this.index].gameObject, delegate(float ms, ExploderObject.ExplosionState state)
		{
			if (state == ExploderObject.ExplosionState.ExplosionFinished)
			{
				int processingFrames = ExploderSingleton.Instance.ProcessingFrames;
				this.AddReport(new Benchmark.Report
				{
					name = string.Concat(new object[]
					{
						this.objects[this.index].gameObject.name,
						"[",
						targetFragments,
						"]"
					}),
					ms = ms,
					frames = processingFrames,
					count = 1
				});
				this.index++;
				this.ExplodeObject();
			}
		});
	}

	// Token: 0x04001169 RID: 4457
	public GameObject testObjects;

	// Token: 0x0400116A RID: 4458
	private MeshRenderer[] objects;

	// Token: 0x0400116B RID: 4459
	private int index;

	// Token: 0x0400116C RID: 4460
	private int rounds = 5;

	// Token: 0x0400116D RID: 4461
	private int batchIndex;

	// Token: 0x0400116E RID: 4462
	private int[] batches = new int[]
	{
		30,
		60,
		100
	};

	// Token: 0x0400116F RID: 4463
	private readonly Dictionary<string, Benchmark.Report> report = new Dictionary<string, Benchmark.Report>();

	// Token: 0x0200037A RID: 890
	private class Report
	{
		// Token: 0x04001170 RID: 4464
		public string name;

		// Token: 0x04001171 RID: 4465
		public float ms;

		// Token: 0x04001172 RID: 4466
		public int frames;

		// Token: 0x04001173 RID: 4467
		public int count;
	}
}
