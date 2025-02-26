using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Illusion
{
	// Token: 0x02001095 RID: 4245
	public static class Utils
	{
		// Token: 0x02001096 RID: 4246
		public static class Animator
		{
			// Token: 0x06008DFD RID: 36349 RVA: 0x003B2408 File Offset: 0x003B0808
			public static string GetControllerName(UnityEngine.Animator animator)
			{
				return Utils.Animator.GetControllerName(animator.runtimeAnimatorController);
			}

			// Token: 0x06008DFE RID: 36350 RVA: 0x003B2415 File Offset: 0x003B0815
			public static string GetControllerName(RuntimeAnimatorController runtimeAnimatorController)
			{
				return (!(runtimeAnimatorController == null)) ? runtimeAnimatorController.name : null;
			}

			// Token: 0x06008DFF RID: 36351 RVA: 0x003B242F File Offset: 0x003B082F
			public static AnimatorControllerParameter[] GetAnimeParams(UnityEngine.Animator animator)
			{
				return Enumerable.Range(0, animator.parameterCount).Select(new Func<int, AnimatorControllerParameter>(animator.GetParameter)).ToArray<AnimatorControllerParameter>();
			}

			// Token: 0x06008E00 RID: 36352 RVA: 0x003B2453 File Offset: 0x003B0853
			public static AnimatorControllerParameter GetAnimeParam(string name, UnityEngine.Animator animator)
			{
				return Utils.Animator.GetAnimeParam(name, Utils.Animator.GetAnimeParams(animator));
			}

			// Token: 0x06008E01 RID: 36353 RVA: 0x003B2464 File Offset: 0x003B0864
			public static AnimatorControllerParameter GetAnimeParam(string name, AnimatorControllerParameter[] param)
			{
				return param.FirstOrDefault((AnimatorControllerParameter item) => item.name == name);
			}

			// Token: 0x06008E02 RID: 36354 RVA: 0x003B2490 File Offset: 0x003B0890
			public static bool AnimeParamFindSet(UnityEngine.Animator animator, Tuple<string, object> nameValue)
			{
				return Utils.Animator.AnimeParamFindSet(animator, nameValue.Item1, nameValue.Item2, Utils.Animator.GetAnimeParams(animator));
			}

			// Token: 0x06008E03 RID: 36355 RVA: 0x003B24AA File Offset: 0x003B08AA
			public static bool AnimeParamFindSet(UnityEngine.Animator animator, string name, object value)
			{
				return Utils.Animator.AnimeParamFindSet(animator, name, value, Utils.Animator.GetAnimeParams(animator));
			}

			// Token: 0x06008E04 RID: 36356 RVA: 0x003B24BA File Offset: 0x003B08BA
			public static bool AnimeParamFindSet(UnityEngine.Animator animator, Tuple<string, object>[] nameValues)
			{
				return Utils.Animator.AnimeParamFindSet(animator, nameValues, Utils.Animator.GetAnimeParams(animator));
			}

			// Token: 0x06008E05 RID: 36357 RVA: 0x003B24C9 File Offset: 0x003B08C9
			public static bool AnimeParamFindSet(UnityEngine.Animator animator, Tuple<string, object> nameValue, AnimatorControllerParameter[] animParams)
			{
				return Utils.Animator.AnimeParamFindSet(animator, nameValue.Item1, nameValue.Item2, animParams);
			}

			// Token: 0x06008E06 RID: 36358 RVA: 0x003B24E0 File Offset: 0x003B08E0
			public static bool AnimeParamFindSet(UnityEngine.Animator animator, string name, object value, AnimatorControllerParameter[] animParams)
			{
				return animParams.FirstOrDefault((AnimatorControllerParameter p) => p.name == name).SafeProc(delegate(AnimatorControllerParameter param)
				{
					Utils.Animator.AnimeParamSet(animator, name, value, param.type);
				});
			}

			// Token: 0x06008E07 RID: 36359 RVA: 0x003B252C File Offset: 0x003B092C
			public static bool AnimeParamFindSet(UnityEngine.Animator animator, Tuple<string, object>[] nameValues, AnimatorControllerParameter[] animParams)
			{
				bool flag = false;
				foreach (var <>__AnonType in nameValues.Select(delegate(Tuple<string, object> v)
				{
					AnimatorControllerParameter animatorControllerParameter = animParams.FirstOrDefault((AnimatorControllerParameter p) => p.name == v.Item1);
					return (animatorControllerParameter != null) ? new
					{
						type = animatorControllerParameter.type,
						value = v
					} : null;
				}))
				{
					flag |= Utils.Animator.AnimeParamSet(animator, <>__AnonType.value, <>__AnonType.type);
				}
				return flag;
			}

			// Token: 0x06008E08 RID: 36360 RVA: 0x003B25B0 File Offset: 0x003B09B0
			public static bool AnimeParamSet(UnityEngine.Animator animator, Tuple<string, object> nameValue)
			{
				return Utils.Animator.AnimeParamSet(animator, nameValue.Item1, nameValue.Item2);
			}

			// Token: 0x06008E09 RID: 36361 RVA: 0x003B25C4 File Offset: 0x003B09C4
			public static bool AnimeParamSet(UnityEngine.Animator animator, string name, object value)
			{
				if (value is float)
				{
					animator.SetFloat(name, (float)value);
				}
				else if (value is int)
				{
					animator.SetInteger(name, (int)value);
				}
				else
				{
					if (!(value is bool))
					{
						return false;
					}
					animator.SetBool(name, (bool)value);
				}
				return true;
			}

			// Token: 0x06008E0A RID: 36362 RVA: 0x003B262B File Offset: 0x003B0A2B
			public static bool AnimeParamSet(UnityEngine.Animator animator, Tuple<string, object> nameValue, AnimatorControllerParameterType type)
			{
				return Utils.Animator.AnimeParamSet(animator, nameValue.Item1, nameValue.Item2, type);
			}

			// Token: 0x06008E0B RID: 36363 RVA: 0x003B2640 File Offset: 0x003B0A40
			public static bool AnimeParamSet(UnityEngine.Animator animator, string name, object value, AnimatorControllerParameterType type)
			{
				switch (type)
				{
				case AnimatorControllerParameterType.Float:
					animator.SetFloat(name, (float)value);
					break;
				default:
					if (type != AnimatorControllerParameterType.Trigger)
					{
						return false;
					}
					if (value != null)
					{
						if (value is bool)
						{
							if ((bool)value)
							{
								animator.SetTrigger(name);
							}
							else
							{
								animator.ResetTrigger(name);
							}
						}
						else if (value is int)
						{
							if ((int)value != 0)
							{
								animator.SetTrigger(name);
							}
							else
							{
								animator.ResetTrigger(name);
							}
						}
						else
						{
							animator.SetTrigger(name);
						}
					}
					else
					{
						animator.ResetTrigger(name);
					}
					break;
				case AnimatorControllerParameterType.Int:
					animator.SetInteger(name, (int)value);
					break;
				case AnimatorControllerParameterType.Bool:
					animator.SetBool(name, (bool)value);
					break;
				}
				return true;
			}

			// Token: 0x06008E0C RID: 36364 RVA: 0x003B2728 File Offset: 0x003B0B28
			public static AnimatorOverrideController SetupAnimatorOverrideController(RuntimeAnimatorController src, RuntimeAnimatorController over)
			{
				if (src == null || over == null)
				{
					return null;
				}
				AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(src);
				foreach (AnimationClip animationClip in new AnimatorOverrideController(over).animationClips)
				{
					animatorOverrideController[animationClip.name] = animationClip;
				}
				animatorOverrideController.name = over.name;
				return animatorOverrideController;
			}
		}

		// Token: 0x02001097 RID: 4247
		public static class Comparer
		{
			// Token: 0x06008E0D RID: 36365 RVA: 0x003B286C File Offset: 0x003B0C6C
			public static bool Check<T>(T v1, string compStr, T v2) where T : IComparable
			{
				return Utils.Comparer.Check<T>(v1, (Utils.Comparer.Type)Utils.Comparer.STR.Check((string s) => s == compStr), v2);
			}

			// Token: 0x06008E0E RID: 36366 RVA: 0x003B28A4 File Offset: 0x003B0CA4
			public static bool Check<T>(T v1, Utils.Comparer.Type compEnum, T v2) where T : IComparable
			{
				int num = v1.CompareTo(v2);
				switch (compEnum)
				{
				case Utils.Comparer.Type.Equal:
					return num == 0;
				case Utils.Comparer.Type.NotEqual:
					return num != 0;
				case Utils.Comparer.Type.Over:
					return num >= 0;
				case Utils.Comparer.Type.Under:
					return num <= 0;
				case Utils.Comparer.Type.Greater:
					return num > 0;
				case Utils.Comparer.Type.Lesser:
					return num < 0;
				default:
					return false;
				}
			}

			// Token: 0x06008E0F RID: 36367 RVA: 0x003B2910 File Offset: 0x003B0D10
			public static Utils.Comparer.Type Cast(string str, out string v)
			{
				int findIndex = -1;
				int num = Utils.Comparer.STR.Check(delegate(string s)
				{
					findIndex = str.IndexOf(s);
					return findIndex != -1;
				});
				v = str.Substring(findIndex + Utils.Comparer.STR[num].Length);
				return (Utils.Comparer.Type)num;
			}

			// Token: 0x06008E10 RID: 36368 RVA: 0x003B296C File Offset: 0x003B0D6C
			public static Tuple<Utils.Comparer.Type, string>[] Cast(params string[] strs)
			{
				string v;
				return (from i in Enumerable.Range(0, strs.Length)
				select Tuple.Create<Utils.Comparer.Type, string>(Utils.Comparer.Cast(strs[i], out v), v)).ToArray<Tuple<Utils.Comparer.Type, string>>();
			}

			// Token: 0x04007321 RID: 29473
			public static readonly string[] STR = new string[]
			{
				"==",
				"!=",
				">=",
				"<=",
				">",
				"<"
			};

			// Token: 0x04007322 RID: 29474
			public static readonly string[] LABEL = new string[]
			{
				"一致",
				"不一致",
				"以上",
				"以下",
				"より大きい",
				"より小さい"
			};

			// Token: 0x02001098 RID: 4248
			public enum Type
			{
				// Token: 0x04007324 RID: 29476
				Equal,
				// Token: 0x04007325 RID: 29477
				NotEqual,
				// Token: 0x04007326 RID: 29478
				Over,
				// Token: 0x04007327 RID: 29479
				Under,
				// Token: 0x04007328 RID: 29480
				Greater,
				// Token: 0x04007329 RID: 29481
				Lesser
			}
		}

		// Token: 0x02001099 RID: 4249
		public static class Crypto
		{
			// Token: 0x06008E12 RID: 36370 RVA: 0x003B2A98 File Offset: 0x003B0E98
			public static byte[] Encrypt(byte[] binData)
			{
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				rijndaelManaged.Padding = PaddingMode.Zeros;
				rijndaelManaged.Mode = CipherMode.CBC;
				rijndaelManaged.KeySize = 256;
				rijndaelManaged.BlockSize = 256;
				byte[] bytes = Encoding.UTF8.GetBytes("piyopiyopiyopiyopiyopiyopiyopiyo");
				byte[] bytes2 = Encoding.UTF8.GetBytes("1234567890abcdefghujklmnopqrstuv");
				ICryptoTransform transform = rijndaelManaged.CreateEncryptor(bytes, bytes2);
				MemoryStream memoryStream = new MemoryStream();
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
				{
					cryptoStream.Write(binData, 0, binData.Length);
				}
				memoryStream.Close();
				return memoryStream.ToArray();
			}

			// Token: 0x06008E13 RID: 36371 RVA: 0x003B2B50 File Offset: 0x003B0F50
			public static byte[] Decrypt(byte[] binData)
			{
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				rijndaelManaged.Padding = PaddingMode.Zeros;
				rijndaelManaged.Mode = CipherMode.CBC;
				rijndaelManaged.KeySize = 256;
				rijndaelManaged.BlockSize = 256;
				byte[] bytes = Encoding.UTF8.GetBytes("piyopiyopiyopiyopiyopiyopiyopiyo");
				byte[] bytes2 = Encoding.UTF8.GetBytes("1234567890abcdefghujklmnopqrstuv");
				ICryptoTransform transform = rijndaelManaged.CreateDecryptor(bytes, bytes2);
				byte[] array = new byte[binData.Length];
				using (MemoryStream memoryStream = new MemoryStream(binData))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
					{
						cryptoStream.Read(array, 0, array.Length);
					}
				}
				return array;
			}

			// Token: 0x0400732A RID: 29482
			private const string AesInitVector = "1234567890abcdefghujklmnopqrstuv";

			// Token: 0x0400732B RID: 29483
			private const string AesKey = "piyopiyopiyopiyopiyopiyopiyopiyo";

			// Token: 0x0400732C RID: 29484
			private const int BlockSize = 256;

			// Token: 0x0400732D RID: 29485
			private const int KeySize = 256;
		}

		// Token: 0x0200109A RID: 4250
		public static class Enum<T> where T : struct
		{
			// Token: 0x17001EE2 RID: 7906
			// (get) Token: 0x06008E14 RID: 36372 RVA: 0x003B2C28 File Offset: 0x003B1028
			public static string[] Names
			{
				get
				{
					return Enum.GetNames(typeof(T));
				}
			}

			// Token: 0x17001EE3 RID: 7907
			// (get) Token: 0x06008E15 RID: 36373 RVA: 0x003B2C39 File Offset: 0x003B1039
			public static Array Values
			{
				get
				{
					return Enum.GetValues(typeof(T));
				}
			}

			// Token: 0x17001EE4 RID: 7908
			// (get) Token: 0x06008E16 RID: 36374 RVA: 0x003B2C4A File Offset: 0x003B104A
			public static int Length
			{
				get
				{
					return Utils.Enum<T>.Values.Length;
				}
			}

			// Token: 0x06008E17 RID: 36375 RVA: 0x003B2C58 File Offset: 0x003B1058
			public static void Each(Action<T> act)
			{
				IEnumerator enumerator = Utils.Enum<T>.Values.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						T obj2 = (T)((object)obj);
						act(obj2);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}

			// Token: 0x06008E18 RID: 36376 RVA: 0x003B2CBC File Offset: 0x003B10BC
			[Conditional("UNITY_ASSERTIONS")]
			private static void Check(bool condition, string message)
			{
			}

			// Token: 0x06008E19 RID: 36377 RVA: 0x003B2CBE File Offset: 0x003B10BE
			public static bool Contains(string key, bool ignoreCase = false)
			{
				return Utils.Enum<T>.FindIndex(key, ignoreCase) != -1;
			}

			// Token: 0x06008E1A RID: 36378 RVA: 0x003B2CD0 File Offset: 0x003B10D0
			public static int FindIndex(string key, bool ignoreCase = false)
			{
				string[] names = Utils.Enum<T>.Names;
				for (int i = 0; i < names.Length; i++)
				{
					if (string.Compare(names[i], key, ignoreCase) == 0)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06008E1B RID: 36379 RVA: 0x003B2D09 File Offset: 0x003B1109
			public static T Cast(string key)
			{
				return (T)((object)Enum.Parse(typeof(T), key));
			}

			// Token: 0x06008E1C RID: 36380 RVA: 0x003B2D20 File Offset: 0x003B1120
			public static T Cast(int no)
			{
				System.Type typeFromHandle = typeof(T);
				return (T)((object)Enum.ToObject(typeFromHandle, no));
			}

			// Token: 0x06008E1D RID: 36381 RVA: 0x003B2D44 File Offset: 0x003B1144
			public static T Cast(uint sum)
			{
				return (T)((object)Enum.ToObject(typeof(T), sum));
			}

			// Token: 0x06008E1E RID: 36382 RVA: 0x003B2D5B File Offset: 0x003B115B
			public static T Cast(ulong sum)
			{
				return (T)((object)Enum.ToObject(typeof(T), sum));
			}

			// Token: 0x06008E1F RID: 36383 RVA: 0x003B2D72 File Offset: 0x003B1172
			public static Utils.Enum<T>.EnumerateParameter Enumerate()
			{
				return new Utils.Enum<T>.EnumerateParameter();
			}

			// Token: 0x17001EE5 RID: 7909
			// (get) Token: 0x06008E20 RID: 36384 RVA: 0x003B2D7C File Offset: 0x003B117C
			public static T Nothing
			{
				get
				{
					return default(T);
				}
			}

			// Token: 0x17001EE6 RID: 7910
			// (get) Token: 0x06008E21 RID: 36385 RVA: 0x003B2D94 File Offset: 0x003B1194
			public static T Everything
			{
				get
				{
					ulong sum = 0UL;
					Utils.Enum<T>.Each(delegate(T o)
					{
						sum += Convert.ToUInt64(o);
					});
					return Utils.Enum<T>.Cast(sum);
				}
			}

			// Token: 0x06008E22 RID: 36386 RVA: 0x003B2DCB File Offset: 0x003B11CB
			public static T Normalize(T value)
			{
				return Utils.Enum<T>.Cast((ulong)(Convert.ToInt64(value) & Convert.ToInt64(Utils.Enum<T>.Everything)));
			}

			// Token: 0x06008E23 RID: 36387 RVA: 0x003B2DF0 File Offset: 0x003B11F0
			public static string ToString(T value)
			{
				StringBuilder text = new StringBuilder();
				Utils.Enum<T>.Each(delegate(T e)
				{
					ulong num = Convert.ToUInt64(e);
					if ((Convert.ToUInt64(value) & num) == num)
					{
						text.AppendFormat("{0} | ", e);
					}
				});
				return text.ToString();
			}

			// Token: 0x0200109B RID: 4251
			public class EnumerateParameter
			{
				// Token: 0x06008E25 RID: 36389 RVA: 0x003B2E3C File Offset: 0x003B123C
				public IEnumerator<T> GetEnumerator()
				{
					IEnumerator enumerator = Utils.Enum<T>.Values.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							T e = (T)((object)obj);
							yield return e;
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
					yield break;
				}
			}
		}

		// Token: 0x0200109C RID: 4252
		public static class File
		{
			// Token: 0x06008E26 RID: 36390 RVA: 0x003B3040 File Offset: 0x003B1440
			public static string[] Gets(string filePath, string searchFile)
			{
				List<string> list = new List<string>();
				if (Directory.Exists(filePath))
				{
					foreach (string path in Directory.GetDirectories(filePath))
					{
						List<string> list2 = list;
						IEnumerable<string> files = Directory.GetFiles(path, searchFile);
						if (Utils.File.<>f__mg$cache0 == null)
						{
							Utils.File.<>f__mg$cache0 = new Func<string, string>(Utils.File.ConvertPath);
						}
						list2.AddRange(files.Select(Utils.File.<>f__mg$cache0));
					}
				}
				return list.ToArray();
			}

			// Token: 0x06008E27 RID: 36391 RVA: 0x003B30B4 File Offset: 0x003B14B4
			public static void GetAllFiles(string folder, string searchPattern, ref List<string> files)
			{
				if (!Directory.Exists(folder))
				{
					return;
				}
				List<string> list = files;
				IEnumerable<string> files2 = Directory.GetFiles(folder, searchPattern);
				if (Utils.File.<>f__mg$cache1 == null)
				{
					Utils.File.<>f__mg$cache1 = new Func<string, string>(Utils.File.ConvertPath);
				}
				list.AddRange(files2.Select(Utils.File.<>f__mg$cache1));
				foreach (string folder2 in Directory.GetDirectories(folder))
				{
					Utils.File.GetAllFiles(folder2, searchPattern, ref files);
				}
			}

			// Token: 0x06008E28 RID: 36392 RVA: 0x003B3124 File Offset: 0x003B1524
			public static List<string> GetPaths(string[] paths, string ext, SearchOption option)
			{
				List<string> list = new List<string>();
				if (Utils.File.<>f__mg$cache2 == null)
				{
					Utils.File.<>f__mg$cache2 = new Func<string, bool>(Directory.Exists);
				}
				foreach (IGrouping<bool, string> grouping in paths.GroupBy(Utils.File.<>f__mg$cache2))
				{
					if (grouping.Key)
					{
						foreach (string path2 in grouping)
						{
							List<string> list2 = list;
							IEnumerable<string> files = Directory.GetFiles(path2, "*" + ext, option);
							if (Utils.File.<>f__mg$cache3 == null)
							{
								Utils.File.<>f__mg$cache3 = new Func<string, string>(Utils.File.ConvertPath);
							}
							list2.AddRange(files.Select(Utils.File.<>f__mg$cache3));
						}
					}
					else
					{
						List<string> list3 = list;
						IEnumerable<string> source = from path in grouping
						where Path.GetExtension(path) == ext
						select path;
						if (Utils.File.<>f__mg$cache4 == null)
						{
							Utils.File.<>f__mg$cache4 = new Func<string, string>(Utils.File.ConvertPath);
						}
						list3.AddRange(source.Select(Utils.File.<>f__mg$cache4));
					}
				}
				return list;
			}

			// Token: 0x06008E29 RID: 36393 RVA: 0x003B3274 File Offset: 0x003B1674
			public static string ConvertPath(string path)
			{
				return path.Replace("\\", "/");
			}

			// Token: 0x06008E2A RID: 36394 RVA: 0x003B3288 File Offset: 0x003B1688
			public static object LoadFromBinaryFile(string path)
			{
				object obj = null;
				Utils.File.OpenRead(path, delegate(FileStream fs)
				{
					obj = new BinaryFormatter().Deserialize(fs);
				});
				return obj;
			}

			// Token: 0x06008E2B RID: 36395 RVA: 0x003B32BC File Offset: 0x003B16BC
			public static void SaveToBinaryFile(object obj, string path)
			{
				Utils.File.OpenWrite(path, false, delegate(FileStream fs)
				{
					new BinaryFormatter().Serialize(fs, obj);
				});
			}

			// Token: 0x06008E2C RID: 36396 RVA: 0x003B32EC File Offset: 0x003B16EC
			public static bool OpenRead(string filePath, Action<FileStream> act)
			{
				if (!System.IO.File.Exists(filePath))
				{
					return false;
				}
				using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					act(fileStream);
				}
				return true;
			}

			// Token: 0x06008E2D RID: 36397 RVA: 0x003B333C File Offset: 0x003B173C
			public static void OpenWrite(string filePath, bool isAppend, Action<FileStream> act)
			{
				if (!isAppend)
				{
					using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
					{
						act(fileStream);
					}
				}
				else
				{
					using (FileStream fileStream2 = new FileStream(filePath, FileMode.Append, FileAccess.Write))
					{
						act(fileStream2);
					}
				}
			}

			// Token: 0x06008E2E RID: 36398 RVA: 0x003B33B4 File Offset: 0x003B17B4
			public static bool Read(string filePath, Action<StreamReader> act)
			{
				return Utils.File.OpenRead(filePath, delegate(FileStream fs)
				{
					using (StreamReader streamReader = new StreamReader(fs))
					{
						act(streamReader);
					}
				});
			}

			// Token: 0x06008E2F RID: 36399 RVA: 0x003B33E0 File Offset: 0x003B17E0
			public static void Write(string filePath, bool isAppend, Action<StreamWriter> act)
			{
				Utils.File.OpenWrite(filePath, isAppend, delegate(FileStream fs)
				{
					using (StreamWriter streamWriter = new StreamWriter(fs))
					{
						act(streamWriter);
					}
				});
			}

			// Token: 0x0400732E RID: 29486
			[CompilerGenerated]
			private static Func<string, string> <>f__mg$cache0;

			// Token: 0x0400732F RID: 29487
			[CompilerGenerated]
			private static Func<string, string> <>f__mg$cache1;

			// Token: 0x04007330 RID: 29488
			[CompilerGenerated]
			private static Func<string, bool> <>f__mg$cache2;

			// Token: 0x04007331 RID: 29489
			[CompilerGenerated]
			private static Func<string, string> <>f__mg$cache3;

			// Token: 0x04007332 RID: 29490
			[CompilerGenerated]
			private static Func<string, string> <>f__mg$cache4;
		}

		// Token: 0x0200109D RID: 4253
		public static class Gizmos
		{
			// Token: 0x06008E30 RID: 36400 RVA: 0x003B34F8 File Offset: 0x003B18F8
			public static void Axis(Vector3 pos, Quaternion rot, float len = 0.25f)
			{
				UnityEngine.Gizmos.color = Color.red;
				UnityEngine.Gizmos.DrawRay(pos, rot * Vector3.right * len);
				UnityEngine.Gizmos.color = Color.green;
				UnityEngine.Gizmos.DrawRay(pos, rot * Vector3.up * len);
				UnityEngine.Gizmos.color = Color.blue;
				UnityEngine.Gizmos.DrawRay(pos, rot * Vector3.forward * len);
			}

			// Token: 0x06008E31 RID: 36401 RVA: 0x003B3568 File Offset: 0x003B1968
			public static void Axis(Transform transform, float len = 0.25f)
			{
				Utils.Gizmos.Axis(transform.position, transform.rotation, len);
			}

			// Token: 0x06008E32 RID: 36402 RVA: 0x003B357C File Offset: 0x003B197C
			public static void PointLine(Vector3[] route, bool isLink = false)
			{
				if (route.Any<Vector3>())
				{
					route.Aggregate(delegate(Vector3 prev, Vector3 current)
					{
						UnityEngine.Gizmos.DrawLine(prev, current);
						return current;
					});
					if (isLink)
					{
						UnityEngine.Gizmos.DrawLine(route.Last<Vector3>(), route.First<Vector3>());
					}
				}
			}
		}

		// Token: 0x0200109E RID: 4254
		public static class Hash
		{
			// Token: 0x06008E34 RID: 36404 RVA: 0x003B35DC File Offset: 0x003B19DC
			public static bool Equals(byte[] arg1, byte[] arg2)
			{
				if (arg1.Length != arg2.Length)
				{
					return false;
				}
				int num = -1;
				while (++num < arg1.Length && arg1[num] == arg2[num])
				{
				}
				return num == arg1.Length;
			}

			// Token: 0x06008E35 RID: 36405 RVA: 0x003B361B File Offset: 0x003B1A1B
			public static byte[] ComputeMD5(string source)
			{
				return new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(source));
			}

			// Token: 0x06008E36 RID: 36406 RVA: 0x003B3632 File Offset: 0x003B1A32
			public static byte[] ComputeSHA1(string source)
			{
				return new SHA1CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(source));
			}

			// Token: 0x06008E37 RID: 36407 RVA: 0x003B3649 File Offset: 0x003B1A49
			public static int Convert(byte[] bytes)
			{
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes);
				}
				return BitConverter.ToInt32(bytes, 0);
			}

			// Token: 0x06008E38 RID: 36408 RVA: 0x003B3664 File Offset: 0x003B1A64
			public static string Cast(byte[] source)
			{
				StringBuilder stringBuilder = new StringBuilder(source.Length);
				for (int i = 0; i < source.Length - 1; i++)
				{
					stringBuilder.Append(source[i].ToString("X2"));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0200109F RID: 4255
		public static class Math
		{
			// Token: 0x06008E39 RID: 36409 RVA: 0x003B36B0 File Offset: 0x003B1AB0
			public static Vector3 MoveSpeedPositionEnter(Vector3[] points, float moveSpeed)
			{
				for (int i = 0; i < points.Length - 1; i++)
				{
					Vector3 a = points[i];
					Vector3 b = points[i + 1];
					float num = Vector3.Distance(a, b);
					if (moveSpeed <= num)
					{
						return Vector3.Lerp(a, b, Mathf.InverseLerp(num, 0f, num - moveSpeed));
					}
					moveSpeed -= num;
				}
				return points[points.Length - 1];
			}

			// Token: 0x06008E3A RID: 36410 RVA: 0x003B3730 File Offset: 0x003B1B30
			public static int MinDistanceRouteIndex(Vector3[] route, Vector3 pos)
			{
				int result = -1;
				float num = float.MaxValue;
				for (int i = 0; i < route.Length; i++)
				{
					float sqrMagnitude = (route[i] - pos).sqrMagnitude;
					if (num > sqrMagnitude)
					{
						num = sqrMagnitude;
						result = i;
					}
				}
				return result;
			}

			// Token: 0x06008E3B RID: 36411 RVA: 0x003B3784 File Offset: 0x003B1B84
			public static void TargetFor(Transform from, Transform target, bool isHeight = false)
			{
				Vector3 position = target.position;
				if (!isHeight)
				{
					position.y = from.position.y;
				}
				from.LookAt(position);
			}

			// Token: 0x06008E3C RID: 36412 RVA: 0x003B37BC File Offset: 0x003B1BBC
			public static float NewtonMethod(Utils.Math.Func func, Utils.Math.Func derive, float initX, int maxLoop)
			{
				float num = initX;
				for (int i = 0; i < maxLoop; i++)
				{
					float num2 = func(num);
					if (num2 < 1E-05f && num2 > -1E-05f)
					{
						break;
					}
					num -= num2 / derive(num);
				}
				return num;
			}

			// Token: 0x020010A0 RID: 4256
			public static class Cast
			{
				// Token: 0x06008E3D RID: 36413 RVA: 0x003B380D File Offset: 0x003B1C0D
				public static Vector2 ToVector2(float[] f)
				{
					return new Vector2(f[0], f[1]);
				}

				// Token: 0x06008E3E RID: 36414 RVA: 0x003B381A File Offset: 0x003B1C1A
				public static Vector3 ToVector3(float[] f)
				{
					return new Vector3(f[0], f[1], f[2]);
				}

				// Token: 0x06008E3F RID: 36415 RVA: 0x003B382A File Offset: 0x003B1C2A
				public static float[] ToArray(Vector2 v2)
				{
					return new float[]
					{
						v2.x,
						v2.y
					};
				}

				// Token: 0x06008E40 RID: 36416 RVA: 0x003B3846 File Offset: 0x003B1C46
				public static float[] ToArray(Vector3 v3)
				{
					return new float[]
					{
						v3.x,
						v3.y,
						v3.z
					};
				}

				// Token: 0x06008E41 RID: 36417 RVA: 0x003B386C File Offset: 0x003B1C6C
				public static string ToString(Vector3 v3)
				{
					return string.Format("({0},{1},{2})", v3.x, v3.y, v3.z);
				}
			}

			// Token: 0x020010A1 RID: 4257
			public static class Fuzzy
			{
				// Token: 0x06008E42 RID: 36418 RVA: 0x003B389C File Offset: 0x003B1C9C
				public static float Grade(float _value, float _x0, float _x1)
				{
					float result;
					if (_value <= _x0)
					{
						result = 0f;
					}
					else if (_value >= _x1)
					{
						result = 1f;
					}
					else
					{
						result = _value / (_x1 - _x0) - _x0 / (_x1 - _x0);
					}
					return result;
				}

				// Token: 0x06008E43 RID: 36419 RVA: 0x003B38E4 File Offset: 0x003B1CE4
				public static float ReverseGrade(float _value, float _x0, float _x1)
				{
					float result;
					if (_value <= _x0)
					{
						result = 1f;
					}
					else if (_value >= _x1)
					{
						result = 0f;
					}
					else
					{
						result = -_value / (_x1 - _x0) + _x1 / (_x1 - _x0);
					}
					return result;
				}

				// Token: 0x06008E44 RID: 36420 RVA: 0x003B392C File Offset: 0x003B1D2C
				public static float Triangle(float _value, float _x0, float _x1, float _x2)
				{
					float result;
					if (_value <= _x0)
					{
						result = 0f;
					}
					else if (_value == _x1)
					{
						result = 1f;
					}
					else if (_value > _x0 && _value < _x1)
					{
						result = _value / (_x1 - _x0) - _x0 / (_x1 - _x0);
					}
					else
					{
						result = -_value / (_x2 - _x1) + _x2 / (_x2 - _x1);
					}
					return result;
				}

				// Token: 0x06008E45 RID: 36421 RVA: 0x003B3994 File Offset: 0x003B1D94
				public static float Trapezoid(float _value, float _x0, float _x1, float _x2, float _x3)
				{
					float result;
					if (_value <= _x0)
					{
						result = 0f;
					}
					else if (_value >= _x1 && _value <= _x2)
					{
						result = 1f;
					}
					else if (_value > _x0 && _value < _x1)
					{
						result = _value / (_x1 - _x0) - _x0 / (_x1 - _x0);
					}
					else
					{
						result = -_value / (_x3 - _x2) + _x3 / (_x3 - _x2);
					}
					return result;
				}

				// Token: 0x06008E46 RID: 36422 RVA: 0x003B3A04 File Offset: 0x003B1E04
				public static float AND(float _a, float _b)
				{
					return Mathf.Min(_a, _b);
				}

				// Token: 0x06008E47 RID: 36423 RVA: 0x003B3A0D File Offset: 0x003B1E0D
				public static float OR(float _a, float _b)
				{
					return Mathf.Max(_a, _b);
				}

				// Token: 0x06008E48 RID: 36424 RVA: 0x003B3A16 File Offset: 0x003B1E16
				public static float NOT(float _a)
				{
					return 1f - _a;
				}
			}

			// Token: 0x020010A2 RID: 4258
			// (Invoke) Token: 0x06008E4A RID: 36426
			public delegate float Func(float x);
		}

		// Token: 0x020010A3 RID: 4259
		public static class Mesh
		{
			// Token: 0x06008E4D RID: 36429 RVA: 0x003B3A1F File Offset: 0x003B1E1F
			private static float ToRad(float angle, int index)
			{
				return angle * (float)index * 0.017453292f;
			}

			// Token: 0x06008E4E RID: 36430 RVA: 0x003B3A2C File Offset: 0x003B1E2C
			public static IEnumerable<Vector3> CalculateVertices(int verticesNum)
			{
				if (verticesNum <= 0)
				{
					return Enumerable.Empty<Vector3>();
				}
				float angle = 360f / (float)verticesNum;
				return from i in Enumerable.Range(0, verticesNum)
				select Utils.Mesh.ToRad(angle, i) into r
				select new Vector3(Mathf.Sin(r), Mathf.Cos(r));
			}

			// Token: 0x06008E4F RID: 36431 RVA: 0x003B3A94 File Offset: 0x003B1E94
			public static void Create(GameObject go, IEnumerable<Vector3> vertices)
			{
				if (vertices == null || vertices.Count<Vector3>() < 3)
				{
					return;
				}
				MeshFilter meshFilter = go.GetComponent<MeshFilter>();
				if (meshFilter == null)
				{
					meshFilter = go.AddComponent<MeshFilter>();
				}
				UnityEngine.Mesh mesh = meshFilter.mesh;
				mesh.Clear();
				mesh.vertices = vertices.ToArray<Vector3>();
				int[] array = new int[(mesh.vertices.Length - 2) * 3];
				int i = 0;
				int num = 0;
				while (i < array.Length)
				{
					array[i] = 0;
					array[i + 1] = num + 1;
					array[i + 2] = num + 2;
					i += 3;
					num++;
				}
				mesh.triangles = array;
				mesh.RecalculateNormals();
				mesh.RecalculateBounds();
				meshFilter.sharedMesh = mesh;
			}

			// Token: 0x06008E50 RID: 36432 RVA: 0x003B3B48 File Offset: 0x003B1F48
			public static void RendererSet(GameObject go, Color color, string matName = "Sprites-Default.mat")
			{
				MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
				if (meshRenderer == null)
				{
					meshRenderer = go.AddComponent<MeshRenderer>();
				}
				meshRenderer.material = Resources.GetBuiltinResource<Material>(matName);
				meshRenderer.material.color = color;
			}
		}

		// Token: 0x020010A4 RID: 4260
		public static class NavMesh
		{
			// Token: 0x06008E52 RID: 36434 RVA: 0x003B3BB0 File Offset: 0x003B1FB0
			public static GameObject CreateDrawObject(Color? color)
			{
				NavMeshTriangulation navMeshTriangulation = UnityEngine.AI.NavMesh.CalculateTriangulation();
				UnityEngine.Mesh mesh = new UnityEngine.Mesh();
				mesh.vertices = navMeshTriangulation.vertices;
				mesh.triangles = navMeshTriangulation.indices;
				GameObject gameObject = new GameObject("NavMeshDrawObject");
				gameObject.AddComponent<MeshFilter>().mesh = mesh;
				gameObject.AddComponent<MeshRenderer>().material.color = ((color == null) ? Color.white : color.Value);
				return gameObject;
			}

			// Token: 0x06008E53 RID: 36435 RVA: 0x003B3C28 File Offset: 0x003B2028
			public static bool GetRandomPosition(Vector3 center, out Vector3 result, float range = 10f, int count = 30, float maxDistance = 1f, bool isY = true, int area = -1)
			{
				Func<Vector3> func;
				if (isY)
				{
					func = (() => UnityEngine.Random.insideUnitSphere * range);
				}
				else
				{
					func = delegate()
					{
						Vector2 vector = UnityEngine.Random.insideUnitCircle * range;
						return new Vector3(vector.x, 0f, vector.y);
					};
				}
				for (int i = 0; i < count; i++)
				{
					NavMeshHit navMeshHit;
					if (UnityEngine.AI.NavMesh.SamplePosition(center + func(), out navMeshHit, maxDistance, area))
					{
						result = navMeshHit.position;
						return true;
					}
				}
				result = Vector3.zero;
				return false;
			}
		}

		// Token: 0x020010A5 RID: 4261
		public static class Network
		{
			// Token: 0x17001EE7 RID: 7911
			// (get) Token: 0x06008E54 RID: 36436 RVA: 0x003B3D04 File Offset: 0x003B2104
			public static string MACAddress
			{
				get
				{
					string text = string.Empty;
					NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
					if (allNetworkInterfaces != null)
					{
						foreach (NetworkInterface networkInterface in allNetworkInterfaces)
						{
							PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
							if (physicalAddress != null)
							{
								if (physicalAddress.GetAddressBytes().Length == 6)
								{
									string str = physicalAddress.ToString();
									text += str;
								}
							}
						}
					}
					return text;
				}
			}
		}

		// Token: 0x020010A6 RID: 4262
		public static class ProbabilityCalclator
		{
			// Token: 0x06008E55 RID: 36437 RVA: 0x003B3D80 File Offset: 0x003B2180
			public static bool DetectFromPercent(float percent)
			{
				int num = 0;
				string text = percent.ToString();
				if (text.IndexOf(".") > 0)
				{
					num = text.Split(new char[]
					{
						'.'
					})[1].Length;
				}
				int num2 = (int)Mathf.Pow(10f, (float)num);
				int max = 100 * num2;
				int num3 = (int)((float)num2 * percent);
				return UnityEngine.Random.Range(0, max) < num3;
			}

			// Token: 0x06008E56 RID: 36438 RVA: 0x003B3DEC File Offset: 0x003B21EC
			public static T DetermineFromDict<T>(Dictionary<T, int> targetDict)
			{
				if (!targetDict.Any<KeyValuePair<T, int>>())
				{
					return default(T);
				}
				float num = UnityEngine.Random.Range(0f, (float)targetDict.Values.Sum());
				foreach (KeyValuePair<T, int> keyValuePair in targetDict)
				{
					num -= (float)keyValuePair.Value;
					if (num < 0f)
					{
						return keyValuePair.Key;
					}
				}
				return targetDict.Keys.First<T>();
			}

			// Token: 0x06008E57 RID: 36439 RVA: 0x003B3E9C File Offset: 0x003B229C
			public static T DetermineFromDict<T>(Dictionary<T, float> targetDict)
			{
				if (!targetDict.Any<KeyValuePair<T, float>>())
				{
					return default(T);
				}
				float num = UnityEngine.Random.Range(0f, targetDict.Values.Sum());
				foreach (KeyValuePair<T, float> keyValuePair in targetDict)
				{
					num -= keyValuePair.Value;
					if (num < 0f)
					{
						return keyValuePair.Key;
					}
				}
				return targetDict.Keys.First<T>();
			}
		}

		// Token: 0x020010A7 RID: 4263
		public static class String
		{
			// Token: 0x06008E58 RID: 36440 RVA: 0x003B3F48 File Offset: 0x003B2348
			public static string GetPropertyName<T>(Expression<Func<T>> e)
			{
				MemberExpression memberExpression = (MemberExpression)e.Body;
				return memberExpression.Member.Name;
			}
		}

		// Token: 0x020010A8 RID: 4264
		public static class Type
		{
			// Token: 0x06008E59 RID: 36441 RVA: 0x003B3F6C File Offset: 0x003B236C
			public static System.Type Get(string dllName, string typeName)
			{
				return Assembly.Load(dllName).GetType(typeName);
			}

			// Token: 0x06008E5A RID: 36442 RVA: 0x003B3F7C File Offset: 0x003B237C
			public static System.Type Get(string typeName)
			{
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach (System.Type type in assembly.GetTypes())
					{
						if (type.Name == typeName)
						{
							return type;
						}
					}
				}
				return null;
			}

			// Token: 0x06008E5B RID: 36443 RVA: 0x003B3FE8 File Offset: 0x003B23E8
			public static System.Type GetFull(string typeFullName)
			{
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach (System.Type type in assembly.GetTypes())
					{
						if (type.FullName == typeFullName)
						{
							return type;
						}
					}
				}
				return null;
			}

			// Token: 0x06008E5C RID: 36444 RVA: 0x003B4054 File Offset: 0x003B2454
			public static string GetAssemblyQualifiedName(string typeName)
			{
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach (System.Type type in assembly.GetTypes())
					{
						if (type.Name == typeName)
						{
							return type.AssemblyQualifiedName;
						}
					}
				}
				return null;
			}

			// Token: 0x06008E5D RID: 36445 RVA: 0x003B40C4 File Offset: 0x003B24C4
			public static string GetFullAssemblyQualifiedName(string typeFullName)
			{
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach (System.Type type in assembly.GetTypes())
					{
						if (type.FullName == typeFullName)
						{
							return type.AssemblyQualifiedName;
						}
					}
				}
				return null;
			}

			// Token: 0x06008E5E RID: 36446 RVA: 0x003B4134 File Offset: 0x003B2534
			public static FieldInfo[] GetPublicFields(System.Type type)
			{
				return type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			}

			// Token: 0x06008E5F RID: 36447 RVA: 0x003B413E File Offset: 0x003B253E
			public static PropertyInfo[] GetPublicProperties(System.Type type)
			{
				return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			}
		}

		// Token: 0x020010A9 RID: 4265
		public static class uGUI
		{
			// Token: 0x17001EE8 RID: 7912
			// (get) Token: 0x06008E60 RID: 36448 RVA: 0x003B4148 File Offset: 0x003B2548
			public static bool isMouseHit
			{
				get
				{
					return Utils.uGUI.HitList(Input.mousePosition).Count > 0;
				}
			}

			// Token: 0x06008E61 RID: 36449 RVA: 0x003B415C File Offset: 0x003B255C
			public static List<RaycastResult> HitList(Vector3 position)
			{
				List<RaycastResult> list = new List<RaycastResult>();
				EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current)
				{
					position = position
				}, list);
				return list;
			}
		}

		// Token: 0x020010AA RID: 4266
		public static class Value
		{
			// Token: 0x06008E62 RID: 36450 RVA: 0x003B4194 File Offset: 0x003B2594
			public static int Check(int len, Func<int, bool> func)
			{
				int num = -1;
				while (++num < len && !func(num))
				{
				}
				return (num < len) ? num : -1;
			}
		}
	}
}
