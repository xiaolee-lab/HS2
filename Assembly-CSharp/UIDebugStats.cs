using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x020004C7 RID: 1223
public class UIDebugStats : MonoBehaviour
{
	// Token: 0x06001694 RID: 5780 RVA: 0x0008A91C File Offset: 0x00088D1C
	private void Start()
	{
		Texture2D texture2D = new Texture2D(1, 1);
		Color color = new Color(0.2f, 0.2f, 0.2f, 0.4f);
		texture2D.SetPixel(0, 0, color);
		texture2D.Apply();
		this.m_debugTexture = texture2D;
	}

	// Token: 0x06001695 RID: 5781 RVA: 0x0008A964 File Offset: 0x00088D64
	public string PrintDictDouble()
	{
		List<KeyValuePair<int, UIPanelData>> list = this.m_elapsedTicks.ToList<KeyValuePair<int, UIPanelData>>();
		list.Sort((KeyValuePair<int, UIPanelData> pair1, KeyValuePair<int, UIPanelData> pair2) => Math.Sign(pair2.Value.mElapsedTicks - pair1.Value.mElapsedTicks));
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<int, UIPanelData> keyValuePair in list)
		{
			stringBuilder.AppendFormat("{0, -30} \t{1:0.00} \t{2:0.00} \t{3}/{4} \t{5}\n", new object[]
			{
				UIDebugCache.GetName(keyValuePair.Key),
				keyValuePair.Value.mElapsedTicks,
				keyValuePair.Value.mElapsedTicks / (double)(1f / Time.deltaTime),
				keyValuePair.Value.mRebuildCount,
				keyValuePair.Value.mCalls,
				keyValuePair.Value.mDrawCallNum / keyValuePair.Value.mCalls
			});
			if (UIDebugVariables.ShowWidgetStatsOnScreen)
			{
				UITimingDict uitimingDict = null;
				if (this.m_widgetTicks.TryGetValue(keyValuePair.Key, out uitimingDict))
				{
					stringBuilder.AppendFormat("{0}\n", uitimingDict.PrintDict(5));
				}
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06001696 RID: 5782 RVA: 0x0008AAC8 File Offset: 0x00088EC8
	private void Update()
	{
		if (Time.time - this.m_lastUpdateTime >= 1f)
		{
			foreach (KeyValuePair<int, UIPanelData> keyValuePair in this.m_elapsedTicks)
			{
				if (this.m_accumulatedPanels.ContainsKey(keyValuePair.Key))
				{
					this.m_accumulatedPanels[keyValuePair.Key].Enlarge(keyValuePair.Value);
				}
				else
				{
					this.m_accumulatedPanels.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			this.m_debugInfo = this.PrintDictDouble();
			this.m_elapsedTicks.Clear();
			this.m_widgetTicks.Clear();
			this.m_lastUpdateTime = Time.time;
		}
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x0008ABB4 File Offset: 0x00088FB4
	private void OnGUI()
	{
		if (!string.IsNullOrEmpty(this.m_debugInfo))
		{
			Rect position = new Rect(250f, 60f, (float)(Screen.width - 640), (float)Screen.height - 120f);
			GUI.DrawTexture(position, this.m_debugTexture);
			this.guiStyle.fontSize = 20;
			this.guiStyle.normal.textColor = Color.white;
			GUI.Label(position, this.m_debugInfo, this.guiStyle);
		}
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x0008AC3C File Offset: 0x0008903C
	public void StartStats()
	{
		if (base.enabled)
		{
			return;
		}
		base.enabled = true;
		this.m_accumulated.Clear();
		this.m_accumulatedPanels.Clear();
		this.m_lastUpdateTime = Time.time;
		this.m_startFrame = Time.frameCount;
		this.m_startTime = Time.time;
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x0008AC94 File Offset: 0x00089094
	public void StopStats()
	{
		if (!base.enabled)
		{
			return;
		}
		base.enabled = false;
		float num = Time.time - this.m_startTime;
		List<string> list = new List<string>();
		int num2 = Time.frameCount - this.m_startFrame;
		list.Add(string.Format("name \ttotalMS \tperFrameMS \trebuildCount \tupdateCount \tdrawcallCount/updateCount \t --- {0} frames ---", num2));
		List<KeyValuePair<int, UIPanelData>> list2 = this.m_accumulatedPanels.ToList<KeyValuePair<int, UIPanelData>>();
		list2.Sort((KeyValuePair<int, UIPanelData> pair1, KeyValuePair<int, UIPanelData> pair2) => pair2.Value.mElapsedTicks.CompareTo(pair1.Value.mElapsedTicks));
		foreach (KeyValuePair<int, UIPanelData> keyValuePair in list2)
		{
			string name = UIDebugCache.GetName(keyValuePair.Key);
			UIPanelData value = keyValuePair.Value;
			list.Add(string.Format("{0}\t{1:0.00}\t{2:0.00}\t{3}\t{4}\t{5}", new object[]
			{
				name,
				value.mElapsedTicks,
				value.mElapsedTicks / (double)num2,
				(int)((float)value.mRebuildCount / num),
				(int)((float)value.mCalls / num),
				value.mDrawCallNum / value.mCalls
			}));
		}
		List<KeyValuePair<int, double>> list3 = this.m_accumulated.ToList<KeyValuePair<int, double>>();
		list3.Sort((KeyValuePair<int, double> pair1, KeyValuePair<int, double> pair2) => pair2.Value.CompareTo(pair1.Value));
		foreach (KeyValuePair<int, double> keyValuePair2 in list3)
		{
			string text = UIDebugCache.GetName(keyValuePair2.Key);
			string parentName = UIDebugCache.GetParentName(keyValuePair2.Key);
			if (!string.IsNullOrEmpty(parentName))
			{
				text = string.Format("{0}:{1}", parentName, text);
			}
			list.Add(string.Format("{0}\t{1:0.00}\t{2:0.00}", text, keyValuePair2.Value, keyValuePair2.Value / (double)num2));
		}
		string path = Path.Combine(Application.persistentDataPath, string.Format("TestTools/ui_stats_panels_{0}_{1}.log", SysUtil.FormatDateAsFileNameString(DateTime.Now), SysUtil.FormatTimeAsFileNameString(DateTime.Now)));
		File.WriteAllLines(path, list.ToArray());
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x0008AF00 File Offset: 0x00089300
	public void StartPanelUpdate()
	{
		if (!base.enabled)
		{
			return;
		}
		this.m_panelSW.Reset();
		this.m_panelSW.Start();
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x0008AF24 File Offset: 0x00089324
	public void StopPanelUpdate(UnityEngine.Object panel, bool bRebuild, int drawCallNum)
	{
		if (!base.enabled)
		{
			return;
		}
		int instanceID = panel.GetInstanceID();
		this.m_panelSW.Stop();
		double num = (double)this.m_panelSW.ElapsedTicks / 10000.0;
		if (this.m_elapsedTicks.ContainsKey(instanceID))
		{
			UIPanelData uipanelData = this.m_elapsedTicks[instanceID];
			uipanelData.mElapsedTicks += num;
			uipanelData.mCalls++;
			uipanelData.mRebuildCount += ((!bRebuild) ? 0 : 1);
			uipanelData.mDrawCallNum += drawCallNum;
		}
		else
		{
			this.m_elapsedTicks.Add(instanceID, new UIPanelData
			{
				mElapsedTicks = num,
				mCalls = 1,
				mRebuildCount = ((!bRebuild) ? 0 : 1),
				mDrawCallNum = drawCallNum
			});
			if (!UIDebugCache.s_nameLut.ContainsKey(instanceID))
			{
				UIDebugCache.s_nameLut.Add(instanceID, panel.name);
			}
		}
	}

	// Token: 0x0600169C RID: 5788 RVA: 0x0008B02C File Offset: 0x0008942C
	public void StartPanelWidget(UnityEngine.Object p)
	{
		if (!base.enabled)
		{
			return;
		}
		int instanceID = p.GetInstanceID();
		UITimingDict uitimingDict = null;
		if (!this.m_widgetTicks.TryGetValue(instanceID, out uitimingDict))
		{
			uitimingDict = new UITimingDict();
			this.m_widgetTicks.Add(instanceID, uitimingDict);
		}
		uitimingDict.StartTiming();
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x0008B07C File Offset: 0x0008947C
	public void StopPanelWidget(UnityEngine.Object p, UnityEngine.Object w)
	{
		if (!base.enabled)
		{
			return;
		}
		int instanceID = p.GetInstanceID();
		int instanceID2 = w.GetInstanceID();
		UITimingDict uitimingDict = null;
		if (!this.m_widgetTicks.TryGetValue(instanceID, out uitimingDict))
		{
			return;
		}
		if (!UIDebugCache.s_parentNameLut.ContainsKey(w.GetInstanceID()))
		{
			UIDebugCache.s_parentNameLut.Add(w.GetInstanceID(), p.name);
		}
		double num = uitimingDict.StopTiming(w);
		if (this.m_accumulated.ContainsKey(instanceID2))
		{
			Dictionary<int, double> accumulated;
			int key;
			(accumulated = this.m_accumulated)[key = instanceID2] = accumulated[key] + num;
		}
		else
		{
			this.m_accumulated.Add(instanceID2, num);
		}
	}

	// Token: 0x04001958 RID: 6488
	public static UIDebugStats Instance;

	// Token: 0x04001959 RID: 6489
	private Texture m_debugTexture;

	// Token: 0x0400195A RID: 6490
	private string m_debugInfo;

	// Token: 0x0400195B RID: 6491
	private float m_lastUpdateTime;

	// Token: 0x0400195C RID: 6492
	private GUIStyle guiStyle = new GUIStyle();

	// Token: 0x0400195D RID: 6493
	private float m_startTime;

	// Token: 0x0400195E RID: 6494
	private int m_startFrame = -1;

	// Token: 0x0400195F RID: 6495
	private Stopwatch m_panelSW = Stopwatch.StartNew();

	// Token: 0x04001960 RID: 6496
	private Dictionary<int, UIPanelData> m_elapsedTicks = new Dictionary<int, UIPanelData>(200);

	// Token: 0x04001961 RID: 6497
	private Dictionary<int, UITimingDict> m_widgetTicks = new Dictionary<int, UITimingDict>(200);

	// Token: 0x04001962 RID: 6498
	private Dictionary<int, double> m_accumulated = new Dictionary<int, double>(200);

	// Token: 0x04001963 RID: 6499
	private Dictionary<int, UIPanelData> m_accumulatedPanels = new Dictionary<int, UIPanelData>(200);
}
