using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004AF RID: 1199
public class ResourceTracker : IDisposable
{
	// Token: 0x06001615 RID: 5653 RVA: 0x00087BFC File Offset: 0x00085FFC
	public ResourceTracker(bool enableTracking)
	{
		if (enableTracking)
		{
			this.Open();
			if (this._enableTracking)
			{
				if (UsNet.Instance != null && UsNet.Instance.CmdExecutor != null)
				{
					UsNet.Instance.CmdExecutor.RegisterHandler(eNetCmd.CL_RequestStackData, new UsCmdHandler(this.NetHandle_RequestStackData));
					UsNet.Instance.CmdExecutor.RegisterHandler(eNetCmd.CL_RequestStackSummary, new UsCmdHandler(this.NetHandle_RequestStackSummary));
				}
				else
				{
					UnityEngine.Debug.LogError("UsNet not available");
				}
				this.readShaderPropertyJson();
			}
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06001616 RID: 5654 RVA: 0x00087CD6 File Offset: 0x000860D6
	public bool EnableTracking
	{
		get
		{
			return this._enableTracking;
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06001617 RID: 5655 RVA: 0x00087CDE File Offset: 0x000860DE
	public Dictionary<string, string> ShaderPropertyDict
	{
		get
		{
			return this._shaderPropertyDict;
		}
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x00087CE8 File Offset: 0x000860E8
	private void readShaderPropertyJson()
	{
		if (this._shaderPropertyDict == null)
		{
			try
			{
				StreamReader streamReader = new StreamReader(new FileStream(ResourceTrackerConst.shaderPropertyNameJsonPath, FileMode.Open));
				string json = streamReader.ReadToEnd();
				streamReader.Close();
				this._shaderPropertyDict = JsonUtility.FromJson<Serialization<string, string>>(json).ToDictionary();
			}
			catch (Exception)
			{
				UnityEngine.Debug.Log("no ShaderPropertyNameRecord.json");
			}
		}
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x00087D54 File Offset: 0x00086154
	public void Open()
	{
		if (this._enableTracking)
		{
			UnityEngine.Debug.LogFormat("[ResourceTracker] info: {0} ", new object[]
			{
				"already enabled, ignored."
			});
			return;
		}
		try
		{
			string text = Path.Combine(Application.persistentDataPath, "mem_logs");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			DateTime now = DateTime.Now;
			string path = string.Format("{0}_{1}_alloc.txt", SysUtil.FormatDateAsFileNameString(now), SysUtil.FormatTimeAsFileNameString(now));
			string text2 = Path.Combine(text, path);
			this._logWriter = new FileInfo(text2).CreateText();
			this._logWriter.AutoFlush = true;
			this._enableTracking = true;
			UnityEngine.Debug.LogFormat("[ResourceTracker] tracking enabled: {0} ", new object[]
			{
				text2
			});
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogErrorFormat("[ResourceTracker] Open() failed, error: {0} ", new object[]
			{
				ex.Message
			});
			if (this._logWriter != null)
			{
				this._logWriter.Close();
				this._logWriter = null;
			}
			this._enableTracking = false;
		}
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x00087E60 File Offset: 0x00086260
	public void Close()
	{
		if (this._logWriter != null)
		{
			try
			{
				this._logWriter.WriteLine("--------- unfinished request: {0} --------- ", this.InProgressAsyncObjects.Count);
				foreach (KeyValuePair<object, ResourceRequestInfo> keyValuePair in this.InProgressAsyncObjects)
				{
					this._logWriter.WriteLine("  + {0}", keyValuePair.Value.ToString());
				}
				this._logWriter.Close();
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogErrorFormat("[ResourceTracker.ctor] error: {0} ", new object[]
				{
					ex.Message
				});
			}
			finally
			{
				this._logWriter = null;
			}
		}
		this._enableTracking = false;
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x00087F54 File Offset: 0x00086354
	public void Dispose()
	{
		if (this._enableTracking && this._logWriter != null)
		{
			this.Close();
		}
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x00087F74 File Offset: 0x00086374
	public void TrackSyncRequest(UnityEngine.Object spawned, string path)
	{
		if (!this._enableTracking)
		{
			return;
		}
		StackFrame sf = new StackFrame(2, true);
		ResourceRequestInfo resourceRequestInfo = this.NewRequest(path, sf);
		resourceRequestInfo.requestType = ResourceRequestType.Ordinary;
		this.TrackRequestWithObject(resourceRequestInfo, spawned);
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x00087FB0 File Offset: 0x000863B0
	public void TrackResourcesDotLoad(UnityEngine.Object loaded, string path)
	{
		if (!this._enableTracking)
		{
			return;
		}
		StackFrame sf = new StackFrame(1, true);
		ResourceRequestInfo resourceRequestInfo = this.NewRequest(path, sf);
		resourceRequestInfo.requestType = ResourceRequestType.Ordinary;
		this.TrackRequestWithObject(resourceRequestInfo, loaded);
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x00087FEC File Offset: 0x000863EC
	public void TrackAsyncRequest(object handle, string path)
	{
		if (!this._enableTracking)
		{
			return;
		}
		StackFrame stackFrame = new StackFrame(2, true);
		if (stackFrame.GetMethod().Name.Contains("SpawnAsyncOldVer"))
		{
			stackFrame = new StackFrame(3, true);
		}
		this.InProgressAsyncObjects[handle] = this.NewRequest(path, stackFrame);
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x00088044 File Offset: 0x00086444
	public void TrackAsyncDone(object handle, UnityEngine.Object target)
	{
		if (!this._enableTracking || target == null)
		{
			return;
		}
		ResourceRequestInfo resourceRequestInfo;
		if (!this.InProgressAsyncObjects.TryGetValue(handle, out resourceRequestInfo))
		{
			return;
		}
		resourceRequestInfo.requestType = ResourceRequestType.Async;
		this.TrackRequestWithObject(resourceRequestInfo, target);
		this.InProgressAsyncObjects.Remove(handle);
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x0008809C File Offset: 0x0008649C
	public void TrackSceneLoaded(string sceneName)
	{
		if (!this._enableTracking)
		{
			return;
		}
		GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
		for (int i = 0; i < rootGameObjects.Length; i++)
		{
			this.TrackSyncRequest(rootGameObjects[i], "[scene]: " + sceneName);
		}
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000880EC File Offset: 0x000864EC
	public void TrackObjectInstantiation(UnityEngine.Object src, UnityEngine.Object instantiated)
	{
		if (!this._enableTracking)
		{
			return;
		}
		int reqSeqID = -1;
		if (!this.TrackedGameObjects.TryGetValue(src.GetInstanceID(), out reqSeqID))
		{
			return;
		}
		this.ExtractObjectResources(instantiated, reqSeqID);
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x00088128 File Offset: 0x00086528
	public ResourceRequestInfo GetAllocInfo(int instID)
	{
		if (!this._enableTracking)
		{
			return null;
		}
		int key = -1;
		if (!this.TrackedGameObjects.TryGetValue(instID, out key) && !this.TrackedMemObjects.TryGetValue(instID, out key))
		{
			return null;
		}
		ResourceRequestInfo result = null;
		if (!this.TrackedAllocInfo.TryGetValue(key, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x00088184 File Offset: 0x00086584
	public string GetStackTrace(ResourceRequestInfo req)
	{
		string result;
		if (!this.Stacktraces.TryGetValue(req.stacktraceHash, out result))
		{
			return string.Empty;
		}
		return result;
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x000881B0 File Offset: 0x000865B0
	private ResourceRequestInfo NewRequest(string path, StackFrame sf)
	{
		ResourceRequestInfo resourceRequestInfo = new ResourceRequestInfo();
		resourceRequestInfo.resourcePath = path;
		resourceRequestInfo.srcFile = sf.GetFileName();
		resourceRequestInfo.srcLineNum = sf.GetFileLineNumber();
		resourceRequestInfo.seqID = this._reqSeq++;
		string text = StackTraceUtility.ExtractStackTrace();
		for (int i = 10; i > 0; i--)
		{
			string b;
			if (!this.Stacktraces.TryGetValue(text.GetHashCode(), out b))
			{
				this.Stacktraces[text.GetHashCode()] = text;
				break;
			}
			if (text == b)
			{
				break;
			}
			text += ((int)(UnityEngine.Random.value * 100f)).ToString();
		}
		resourceRequestInfo.stacktraceHash = text.GetHashCode();
		return resourceRequestInfo;
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x00088284 File Offset: 0x00086684
	private void ExtractObjectResources(UnityEngine.Object obj, int reqSeqID)
	{
		SceneGraphExtractor sceneGraphExtractor = new SceneGraphExtractor(obj);
		for (int i = 0; i < sceneGraphExtractor.GameObjectIDs.Count; i++)
		{
			if (!this.TrackedGameObjects.ContainsKey(sceneGraphExtractor.GameObjectIDs[i]))
			{
				this.TrackedGameObjects[sceneGraphExtractor.GameObjectIDs[i]] = reqSeqID;
			}
		}
		foreach (KeyValuePair<string, List<int>> keyValuePair in sceneGraphExtractor.MemObjectIDs)
		{
			foreach (int key in keyValuePair.Value)
			{
				if (!this.TrackedMemObjects.ContainsKey(key))
				{
					this.TrackedMemObjects[key] = reqSeqID;
				}
			}
		}
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x00088398 File Offset: 0x00086798
	public bool NetHandle_RequestStackSummary(eNetCmd cmd, UsCmd c)
	{
		string text = c.ReadString();
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		if (text.Equals("begin"))
		{
			this._stackUnavailableDict.Clear();
			return true;
		}
		if (text.Equals("end"))
		{
			UnityEngine.Debug.Log("Stack Category Statistical Information:");
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = c.ReadInt32();
			for (int i = 0; i < num5; i++)
			{
				string text2 = c.ReadString();
				List<ResourceTracker.stackParamater> list;
				this._stackUnavailableDict.TryGetValue(text2, out list);
				if (list != null)
				{
					int num6 = c.ReadInt32();
					int num7 = c.ReadInt32();
					num += num6;
					num3 += num7;
					num2 += list.Count;
					int num8 = 0;
					foreach (ResourceTracker.stackParamater stackParamater in list)
					{
						num8 += stackParamater.Size;
					}
					num4 += num8;
					UnityEngine.Debug.Log(string.Format("[{0} =({1}/{2},{3}/{4})]", new object[]
					{
						text2,
						list.Count,
						num6,
						ResourceTrackerConst.FormatBytes(num8),
						ResourceTrackerConst.FormatBytes(num7)
					}));
				}
			}
			UnityEngine.Debug.Log(string.Format("[total =({0}/{1},{2}/{3})]", new object[]
			{
				num2,
				num,
				ResourceTrackerConst.FormatBytes(num4),
				ResourceTrackerConst.FormatBytes(num3)
			}));
			return true;
		}
		string key = text;
		int num9 = c.ReadInt32();
		for (int j = 0; j < num9; j++)
		{
			int num10 = c.ReadInt32();
			int size = c.ReadInt32();
			if (ResourceTracker.Instance.GetAllocInfo(num10) == null)
			{
				if (!this._stackUnavailableDict.ContainsKey(key))
				{
					this._stackUnavailableDict.Add(key, new List<ResourceTracker.stackParamater>());
				}
				List<ResourceTracker.stackParamater> list2;
				this._stackUnavailableDict.TryGetValue(key, out list2);
				ResourceTracker.stackParamater stackParamater2 = new ResourceTracker.stackParamater();
				stackParamater2.InstanceID = num10;
				stackParamater2.Size = size;
				list2.Add(stackParamater2);
			}
		}
		return true;
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x000885D8 File Offset: 0x000869D8
	public List<Texture2D> GetTexture2DObjsFromMaterial(Material mat)
	{
		Dictionary<string, string> shaderPropertyDict = ResourceTracker.Instance.ShaderPropertyDict;
		List<Texture2D> list = new List<Texture2D>();
		if (mat != null)
		{
			Shader shader = mat.shader;
			if (shader != null && shaderPropertyDict != null && shaderPropertyDict.ContainsKey(shader.name))
			{
				string text;
				shaderPropertyDict.TryGetValue(shader.name, out text);
				char[] separator = new char[]
				{
					ResourceTrackerConst.shaderPropertyNameJsonToken
				};
				string[] array = text.Split(separator);
				foreach (string name in array)
				{
					Texture2D texture2D = mat.GetTexture(name) as Texture2D;
					if (texture2D != null)
					{
						list.Add(texture2D);
					}
				}
			}
			Texture2D texture2D2 = mat.mainTexture as Texture2D;
			if (texture2D2 != null && !list.Contains(texture2D2))
			{
				list.Add(texture2D2);
			}
		}
		return list;
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x000886D0 File Offset: 0x00086AD0
	public bool NetHandle_RequestStackData(eNetCmd cmd, UsCmd c)
	{
		int num = c.ReadInt32();
		string arg = c.ReadString();
		UnityEngine.Debug.Log(string.Format("NetHandle_RequestStackData instanceID={0} className={1}", num, arg));
		ResourceRequestInfo allocInfo = ResourceTracker.Instance.GetAllocInfo(num);
		UsCmd usCmd = new UsCmd();
		usCmd.WriteNetCmd(eNetCmd.SV_QueryStacksResponse);
		if (allocInfo == null)
		{
			usCmd.WriteString("<no_callstack_available>");
		}
		else
		{
			usCmd.WriteString(ResourceTracker.Instance.GetStackTrace(allocInfo));
		}
		UsNet.Instance.SendCommand(usCmd);
		return true;
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x00088754 File Offset: 0x00086B54
	private void TrackRequestWithObject(ResourceRequestInfo req, UnityEngine.Object obj)
	{
		if (obj == null)
		{
			return;
		}
		try
		{
			req.RecordObject(obj);
			this.TrackedAllocInfo[req.seqID] = req;
			this.ExtractObjectResources(obj, req.seqID);
			if (this._logWriter != null)
			{
				this._logWriter.WriteLine(req.ToString());
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogErrorFormat("[ResourceTracker.TrackRequestWithObject] error: {0} \n {1} \n {2}", new object[]
			{
				ex.Message,
				(req == null) ? string.Empty : req.ToString(),
				ex.StackTrace
			});
		}
	}

	// Token: 0x040018E8 RID: 6376
	public static ResourceTracker Instance;

	// Token: 0x040018E9 RID: 6377
	private bool _enableTracking;

	// Token: 0x040018EA RID: 6378
	private StreamWriter _logWriter;

	// Token: 0x040018EB RID: 6379
	private int _reqSeq;

	// Token: 0x040018EC RID: 6380
	private Dictionary<string, string> _shaderPropertyDict;

	// Token: 0x040018ED RID: 6381
	private Dictionary<string, List<ResourceTracker.stackParamater>> _stackUnavailableDict = new Dictionary<string, List<ResourceTracker.stackParamater>>();

	// Token: 0x040018EE RID: 6382
	private Dictionary<object, ResourceRequestInfo> InProgressAsyncObjects = new Dictionary<object, ResourceRequestInfo>();

	// Token: 0x040018EF RID: 6383
	private Dictionary<int, ResourceRequestInfo> TrackedAllocInfo = new Dictionary<int, ResourceRequestInfo>();

	// Token: 0x040018F0 RID: 6384
	private Dictionary<int, int> TrackedGameObjects = new Dictionary<int, int>();

	// Token: 0x040018F1 RID: 6385
	private Dictionary<int, int> TrackedMemObjects = new Dictionary<int, int>();

	// Token: 0x040018F2 RID: 6386
	private Dictionary<int, string> Stacktraces = new Dictionary<int, string>();

	// Token: 0x020004B0 RID: 1200
	private class stackParamater
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x00088810 File Offset: 0x00086C10
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x00088818 File Offset: 0x00086C18
		public int InstanceID
		{
			get
			{
				return this.instanceID;
			}
			set
			{
				this.instanceID = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x00088821 File Offset: 0x00086C21
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x00088829 File Offset: 0x00086C29
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x040018F3 RID: 6387
		private int instanceID;

		// Token: 0x040018F4 RID: 6388
		private int size;
	}
}
