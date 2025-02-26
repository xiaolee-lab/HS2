using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MessagePack;
using UnityEngine;

namespace OutputLogControl
{
	// Token: 0x0200083D RID: 2109
	public sealed class OutputLog
	{
		// Token: 0x060035DF RID: 13791 RVA: 0x0013D674 File Offset: 0x0013BA74
		[Conditional("OUTPUT_LOG")]
		public static void Log(string msg, bool unityLog = false, string filename = "Log")
		{
			OutputLog.AddMessage(filename, msg, 0, unityLog);
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x0013D67F File Offset: 0x0013BA7F
		[Conditional("OUTPUT_LOG")]
		public static void Warning(string msg, bool unityLog = false, string filename = "Log")
		{
			OutputLog.AddMessage(filename, msg, 1, unityLog);
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x0013D68A File Offset: 0x0013BA8A
		[Conditional("OUTPUT_LOG")]
		public static void Error(string msg, bool unityLog = false, string filename = "Log")
		{
			OutputLog.AddMessage(filename, msg, 2, unityLog);
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x0013D698 File Offset: 0x0013BA98
		private static void AddMessage(string filename, string msg, byte type, bool unityLog = false)
		{
			if (unityLog)
			{
				switch (type)
				{
				case 0:
					UnityEngine.Debug.Log(msg);
					break;
				case 1:
					UnityEngine.Debug.LogWarning(msg);
					break;
				case 2:
					UnityEngine.Debug.LogError(msg);
					break;
				}
			}
			if (!Directory.Exists(OutputLog.outputDir))
			{
				Directory.CreateDirectory(OutputLog.outputDir);
			}
			string key = DateTime.Now.ToString("yyyy年MM月dd日");
			string time = DateTime.Now.ToString("HH:mm:ss");
			string path = OutputLog.outputDir + filename + ".mpt";
			LogInfo logInfo = new LogInfo();
			try
			{
				if (File.Exists(path))
				{
					byte[] array = File.ReadAllBytes(path);
					if (array != null)
					{
						logInfo = MessagePackSerializer.Deserialize<LogInfo>(array);
					}
				}
			}
			catch (Exception)
			{
				UnityEngine.Debug.LogWarning(string.Format("{0}:ファイルが読み込めない", filename));
			}
			List<LogData> list;
			if (!logInfo.dictLog.TryGetValue(key, out list))
			{
				list = new List<LogData>();
				logInfo.dictLog[key] = list;
			}
			LogData logData = new LogData();
			logData.time = time;
			logData.type = (int)type;
			logData.msg = msg;
			list.Add(logData);
			byte[] bytes = MessagePackSerializer.Serialize<LogInfo>(logInfo);
			File.WriteAllBytes(path, bytes);
		}

		// Token: 0x04003637 RID: 13879
		public static readonly string outputDir = Application.dataPath + "/";
	}
}
