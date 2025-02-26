using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using AIChara;
using AIProject;
using AIProject.Animal;
using AIProject.Animal.Resources;
using AIProject.Definitions;
using AIProject.MiniGames.Fishing;
using AIProject.Player;
using AIProject.SaveData;
using AIProject.Scene;
using AIProject.UI.Popup;
using Illusion;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace Manager
{
	// Token: 0x020008EB RID: 2283
	public class Resources : Singleton<Resources>
	{
		// Token: 0x06003E2C RID: 15916 RVA: 0x00173508 File Offset: 0x00171908
		private static void LoadActionAnimationInfo(ExcelData actionExcel, Dictionary<int, Dictionary<int, PlayState>> dic, bool awaitable)
		{
			if (actionExcel == null)
			{
				return;
			}
			for (int i = 1; i < actionExcel.MaxCell; i++)
			{
				ExcelData.Param param = actionExcel.list[i];
				if (!param.list.IsNullOrEmpty<string>())
				{
					int j = 2;
					string element = param.list.GetElement(j++);
					string element2 = param.list.GetElement(j++);
					if (!element.IsNullOrEmpty() && !element2.IsNullOrEmpty())
					{
						int num;
						int key;
						if (int.TryParse(element, out num) && int.TryParse(element2, out key))
						{
							int key2 = num;
							string element3 = param.list.GetElement(j++);
							int directionType;
							if (!int.TryParse(element3, out directionType))
							{
								directionType = 0;
							}
							string element4 = param.list.GetElement(j++);
							string element5 = param.list.GetElement(j++);
							string element6 = param.list.GetElement(j++);
							string[] array = element6.Split(Resources._separators, StringSplitOptions.RemoveEmptyEntries);
							bool flag;
							bool isLoop = bool.TryParse(param.list.GetElement(j++), out flag) && flag;
							int num2;
							int loopMin = (!int.TryParse(param.list.GetElement(j++), out num2)) ? 0 : num2;
							int num3;
							int loopMax = (!int.TryParse(param.list.GetElement(j++), out num3)) ? 0 : num3;
							bool flag2;
							bool enableFade = bool.TryParse(param.list.GetElement(j++), out flag2) && flag2;
							float num4;
							float fadeSecond = (!float.TryParse(param.list.GetElement(j++), out num4)) ? 0f : num4;
							string element7 = param.list.GetElement(j++);
							string[] array2 = (element7 != null) ? element7.Split(Resources._separators, StringSplitOptions.RemoveEmptyEntries) : null;
							float num5;
							float fadeOutTime = (!float.TryParse(param.list.GetElement(j++), out num5)) ? 1f : num5;
							bool flag3;
							bool enableFade2 = bool.TryParse(param.list.GetElement(j++), out flag3) && flag3;
							float num6;
							float fadeSecond2 = (!float.TryParse(param.list.GetElement(j++), out num6)) ? 0f : num6;
							int num7;
							int randCount = (!int.TryParse(param.list.GetElement(j++), out num7)) ? 0 : num7;
							int num9;
							int num8 = (!int.TryParse(param.list.GetElement(j++), out num9)) ? 0 : num9;
							bool flag4;
							bool endEnableBlend = bool.TryParse(param.list.GetElement(j++), out flag4) && flag4;
							float num10;
							float endBlendRate = (!float.TryParse(param.list.GetElement(j++), out num10)) ? 0f : num10;
							Dictionary<int, PlayState> dictionary;
							if (!dic.TryGetValue(key2, out dictionary))
							{
								dictionary = (dic[key2] = new Dictionary<int, PlayState>());
							}
							string element8 = param.list.GetElement(j++);
							List<UnityEx.ValueTuple<string, bool, int, bool>> list = ListPool<UnityEx.ValueTuple<string, bool, int, bool>>.Get();
							MatchCollection matchCollection = Resources._regex.Matches(element8 ?? string.Empty);
							if (matchCollection.Count > 0)
							{
								for (int k = 0; k < matchCollection.Count; k++)
								{
									Match match = matchCollection[k];
									for (int l = 0; l < match.Groups[1].Captures.Count; l++)
									{
										string value = match.Groups[1].Captures[l].Value;
										string[] source = value.Split(Resources._separationKeywords, StringSplitOptions.None);
										int num11 = 0;
										string element9 = source.GetElement(num11++);
										bool i2;
										if (!bool.TryParse(source.GetElement(num11++), out i2))
										{
											i2 = false;
										}
										int i3;
										if (!int.TryParse(source.GetElement(num11++), out i3))
										{
											i3 = -1;
										}
										bool i4;
										if (!bool.TryParse(source.GetElement(num11++), out i4))
										{
											i4 = false;
										}
										list.Add(new UnityEx.ValueTuple<string, bool, int, bool>(element9, i2, i3, i4));
									}
								}
							}
							List<PlayState.PlayStateInfo> list2 = ListPool<PlayState.PlayStateInfo>.Get();
							while (j < param.list.Count)
							{
								string element10 = param.list.GetElement(j++);
								if (!element10.IsNullOrEmpty())
								{
									string element11 = param.list.GetElement(j++);
									string element12 = param.list.GetElement(j++);
									string[] array3 = (element12 != null) ? element12.Split(Resources._separators, StringSplitOptions.RemoveEmptyEntries) : null;
									bool flag5;
									bool isLoop2 = bool.TryParse(param.list.GetElement(j++), out flag5) && flag5;
									int num12;
									int loopMin2 = (!int.TryParse(param.list.GetElement(j++), out num12)) ? 0 : num12;
									int num13;
									int loopMax2 = (!int.TryParse(param.list.GetElement(j++), out num13)) ? 0 : num13;
									bool flag6;
									bool enableFade3 = bool.TryParse(param.list.GetElement(j++), out flag6) && flag6;
									float num14;
									float fadeSecond3 = (!float.TryParse(param.list.GetElement(j++), out num14)) ? 0f : num14;
									string element13 = param.list.GetElement(j++);
									string[] array4 = (element13 != null) ? element13.Split(Resources._separators, StringSplitOptions.RemoveEmptyEntries) : null;
									float num15;
									float fadeOutTime2 = (!float.TryParse(param.list.GetElement(j++), out num15)) ? 1f : num15;
									bool flag7;
									bool enableFade4 = bool.TryParse(param.list.GetElement(j++), out flag7);
									float num16;
									float fadeSecond4 = (!float.TryParse(param.list.GetElement(j++), out num16)) ? 0f : num16;
									PlayState.PlayStateInfo playStateInfo = new PlayState.PlayStateInfo
									{
										AssetBundleInfo = new AssetBundleInfo(string.Empty, element10, element11, string.Empty),
										FadeOutTime = fadeOutTime2
									};
									playStateInfo.InStateInfo = new PlayState.AnimStateInfo();
									if (!array3.IsNullOrEmpty<string>())
									{
										PlayState.Info[] array5 = new PlayState.Info[array3.Length];
										playStateInfo.InStateInfo.StateInfos = array5;
										PlayState.Info[] array6 = array5;
										for (int m = 0; m < array6.Length; m++)
										{
											array6[m] = new PlayState.Info(array3[m], num8);
										}
									}
									playStateInfo.OutStateInfo = new PlayState.AnimStateInfo();
									if (!array4.IsNullOrEmpty<string>())
									{
										PlayState.Info[] array5 = new PlayState.Info[array4.Length];
										playStateInfo.OutStateInfo.StateInfos = array5;
										PlayState.Info[] array7 = array5;
										for (int n = 0; n < array7.Length; n++)
										{
											array7[n] = new PlayState.Info(array4[n], num8);
										}
									}
									playStateInfo.FadeOutTime = fadeOutTime2;
									playStateInfo.InStateInfo.EnableFade = enableFade3;
									playStateInfo.InStateInfo.FadeSecond = fadeSecond3;
									playStateInfo.OutStateInfo.EnableFade = enableFade4;
									playStateInfo.OutStateInfo.FadeSecond = fadeSecond4;
									playStateInfo.IsLoop = isLoop2;
									playStateInfo.LoopMin = loopMin2;
									playStateInfo.LoopMax = loopMax2;
									list2.Add(playStateInfo);
								}
							}
							PlayState playState = new PlayState
							{
								Layer = num8,
								DirectionType = directionType,
								EndEnableBlend = endEnableBlend,
								EndBlendRate = endBlendRate
							};
							dictionary[key] = playState;
							PlayState playState2 = playState;
							playState2.MainStateInfo.AssetBundleInfo = new AssetBundleInfo(string.Empty, element4, element5, string.Empty);
							playState2.MainStateInfo.InStateInfo = new PlayState.AnimStateInfo();
							if (!array.IsNullOrEmpty<string>())
							{
								PlayState.Info[] array5 = new PlayState.Info[array.Length];
								playState2.MainStateInfo.InStateInfo.StateInfos = array5;
								PlayState.Info[] array8 = array5;
								for (int num17 = 0; num17 < array8.Length; num17++)
								{
									array8[num17] = new PlayState.Info(array[num17], num8);
								}
							}
							playState2.MainStateInfo.OutStateInfo = new PlayState.AnimStateInfo();
							if (!array2.IsNullOrEmpty<string>())
							{
								PlayState.Info[] array5 = new PlayState.Info[array2.Length];
								playState2.MainStateInfo.OutStateInfo.StateInfos = array5;
								PlayState.Info[] array9 = array5;
								for (int num18 = 0; num18 < array9.Length; num18++)
								{
									array9[num18] = new PlayState.Info(array2[num18], num8);
								}
							}
							playState2.MainStateInfo.FadeOutTime = fadeOutTime;
							playState2.MainStateInfo.InStateInfo.EnableFade = enableFade;
							playState2.MainStateInfo.InStateInfo.FadeSecond = fadeSecond;
							playState2.MainStateInfo.OutStateInfo.EnableFade = enableFade2;
							playState2.MainStateInfo.OutStateInfo.FadeSecond = fadeSecond2;
							playState2.MainStateInfo.IsLoop = isLoop;
							playState2.MainStateInfo.LoopMin = loopMin;
							playState2.MainStateInfo.LoopMax = loopMax;
							foreach (PlayState.PlayStateInfo item in list2)
							{
								playState2.SubStateInfos.Add(item);
							}
							foreach (UnityEx.ValueTuple<string, bool, int, bool> valueTuple in list)
							{
								playState2.AddItemInfo(new PlayState.ItemInfo
								{
									parentName = valueTuple.Item1,
									fromEquipedItem = valueTuple.Item2,
									itemID = valueTuple.Item3,
									isSync = valueTuple.Item4
								});
							}
							playState2.ActionInfo = new ActionInfo(!list2.IsNullOrEmpty<PlayState.PlayStateInfo>(), randCount);
							ListPool<UnityEx.ValueTuple<string, bool, int, bool>>.Release(list);
							ListPool<PlayState.PlayStateInfo>.Release(list2);
						}
					}
				}
			}
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x00173F90 File Offset: 0x00172390
		public void LoadMapIK(DefinePack definePack)
		{
			List<UnityEx.ValueTuple<string, string>> list = new List<UnityEx.ValueTuple<string, string>>();
			List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.MapIKList, false);
			assetBundleNameListFromPath.Sort();
			for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
			{
				string assetbundleName = assetBundleNameListFromPath[i];
				string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(assetBundleNameListFromPath[i]);
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, fileNameWithoutExtension, string.Empty);
				if (!(excelData == null))
				{
					int j = 1;
					while (j < excelData.MaxCell)
					{
						ExcelData.Param param = excelData.list[j++];
						int num = 0;
						list.Add(new UnityEx.ValueTuple<string, string>(param.list[num++], param.list[num++]));
					}
				}
			}
			this.LoadMapIK(list);
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x00174070 File Offset: 0x00172470
		public void LoadMapIK(List<UnityEx.ValueTuple<string, string>> AnimatorList)
		{
			int num = ChaFileDefine.cf_bodyshapename.Length;
			float num2 = 0f;
			string text = "f_t_";
			int i = 1;
			foreach (UnityEx.ValueTuple<string, string> valueTuple in AnimatorList)
			{
				if (GlobalMethod.AssetFileExist(valueTuple.Item1, valueTuple.Item2, string.Empty))
				{
					ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(valueTuple.Item1, valueTuple.Item2, string.Empty);
					if (!(excelData == null))
					{
						this.MapIKData.Add(valueTuple.Item2, new MotionIKData());
						List<MotionIKData.State> list = new List<MotionIKData.State>();
						int num3 = 0;
						i = 1;
						bool flag = false;
						int index = -1;
						int pattern = 0;
						float num4 = -1f;
						float num5 = -1f;
						while (i < excelData.MaxCell)
						{
							ExcelData.Param param = excelData.list[i++];
							int num6 = 0;
							if (param.list == null || param.list.Count <= 0)
							{
								if (num3 == 8)
								{
									num3 = 0;
								}
							}
							else if (param.list[num6] == string.Empty)
							{
								if (this.CheckMapIKBlankLine(param) && num3 == 8)
								{
									num3 = 0;
								}
							}
							else
							{
								bool flag2 = 0 == string.Compare(text, 0, param.list[num6], 0, text.Length);
								if (i == 1 || (num3 == 0 && !flag2))
								{
									index = list.Count;
									list.Add(new MotionIKData.State());
									flag = false;
									list[index].name = param.list[num6];
									list[index].frames = new MotionIKData.Frame[8];
								}
								else
								{
									num6 = 1;
									if (!flag)
									{
										if (num3 % 2 == 0)
										{
											list[index].parts[num3 / 2].param2.target = param.list[num6++];
										}
										else
										{
											list[index].parts[num3 / 2].param3.chein = param.list[num6++];
										}
										list[index].frames[num3].editNo = 0;
										list[index].frames[num3].shapes = new MotionIKData.Shape[num];
										for (int j = 0; j < num; j++)
										{
											int shapeNo = j;
											list[index].frames[num3].shapes[j].shapeNo = shapeNo;
										}
									}
									else
									{
										num6++;
									}
									int shapeNo2 = 0;
									if (!int.TryParse(param.list[num6++], out shapeNo2))
									{
										if (!flag)
										{
											if (num3 % 2 == 0)
											{
												if (!float.TryParse(param.list[15], out num2))
												{
													num2 = 1f;
												}
												list[index].parts[num3 / 2].param2.weightPos = num2;
												if (!float.TryParse(param.list[16], out num2))
												{
													num2 = 1f;
												}
												list[index].parts[num3 / 2].param2.weightAng = num2;
											}
											else
											{
												if (!float.TryParse(param.list[15], out num2))
												{
													num2 = 1f;
												}
												list[index].parts[num3 / 2].param3.weight = num2;
											}
											pattern = 0;
											num4 = -1f;
											num5 = -1f;
											num6 = 17;
											if (num3 % 2 == 0)
											{
												if (!int.TryParse(param.list[num6++], out pattern))
												{
													pattern = 0;
												}
												MotionIKData.BlendWeightInfo item;
												item.pattern = pattern;
												if (!float.TryParse(param.list[num6++], out num4))
												{
													num4 = 0f;
												}
												num4 = Mathf.Clamp01(num4);
												item.StartKey = num4;
												if (!float.TryParse(param.list[num6++], out num5))
												{
													num5 = 1f;
												}
												num5 = Mathf.Clamp01(num5);
												item.EndKey = num5;
												item.shape = default(MotionIKData.Shape);
												list[index].parts[num3 / 2].param2.blendInfos[0].Add(item);
												if (!int.TryParse(param.list[num6++], out pattern))
												{
													pattern = 0;
												}
												item.pattern = pattern;
												if (!float.TryParse(param.list[num6++], out num4))
												{
													num4 = 0f;
												}
												num4 = Mathf.Clamp01(num4);
												item.StartKey = num4;
												if (!float.TryParse(param.list[num6++], out num5))
												{
													num5 = 1f;
												}
												num5 = Mathf.Clamp01(num5);
												item.EndKey = num5;
												list[index].parts[num3 / 2].param2.blendInfos[1].Add(item);
											}
											else
											{
												if (!int.TryParse(param.list[num6++], out pattern))
												{
													pattern = 0;
												}
												MotionIKData.BlendWeightInfo item;
												item.pattern = pattern;
												if (!float.TryParse(param.list[num6++], out num4))
												{
													num4 = 0f;
												}
												num4 = Mathf.Clamp01(num4);
												item.StartKey = num4;
												if (!float.TryParse(param.list[num6++], out num5))
												{
													num5 = 1f;
												}
												num5 = Mathf.Clamp01(num5);
												item.EndKey = num5;
												item.shape = default(MotionIKData.Shape);
												list[index].parts[num3 / 2].param3.blendInfos.Add(item);
												num6 += 3;
											}
											list[index].frames[num3].frameNo = num3++;
											flag = (num3 == 8);
										}
									}
									else
									{
										float[,,] array = new float[2, 2, 3];
										if (!float.TryParse(param.list[num6++], out array[0, 0, 0]))
										{
											array[0, 0, 0] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[0, 0, 1]))
										{
											array[0, 0, 1] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[0, 0, 2]))
										{
											array[0, 0, 2] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[0, 1, 0]))
										{
											array[0, 1, 0] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[0, 1, 1]))
										{
											array[0, 1, 1] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[0, 1, 2]))
										{
											array[0, 1, 2] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[1, 0, 0]))
										{
											array[1, 0, 0] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[1, 0, 1]))
										{
											array[1, 0, 1] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[1, 0, 2]))
										{
											array[1, 0, 2] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[1, 1, 0]))
										{
											array[1, 1, 0] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[1, 1, 1]))
										{
											array[1, 1, 1] = 0f;
										}
										if (!float.TryParse(param.list[num6++], out array[1, 1, 2]))
										{
											array[1, 1, 2] = 0f;
										}
										if (!flag)
										{
											if (num3 % 2 == 0)
											{
												if (!float.TryParse(param.list[num6++], out num2))
												{
													num2 = 1f;
												}
												list[index].parts[num3 / 2].param2.weightPos = num2;
												if (!float.TryParse(param.list[num6++], out num2))
												{
													num2 = 1f;
												}
												list[index].parts[num3 / 2].param2.weightAng = num2;
											}
											else
											{
												if (!float.TryParse(param.list[num6++], out num2))
												{
													num2 = 1f;
												}
												list[index].parts[num3 / 2].param3.weight = num2;
												num6++;
											}
										}
										else
										{
											num6 += 2;
										}
										pattern = 0;
										num4 = -1f;
										num5 = -1f;
										int num7;
										if (!flag)
										{
											num7 = num3;
										}
										else
										{
											num7 = Resources.DicMapIKFrameID[param.list[0]];
										}
										if (num7 % 2 == 0)
										{
											if (!int.TryParse(param.list[num6++], out pattern))
											{
												pattern = 0;
											}
											MotionIKData.BlendWeightInfo item;
											item.pattern = pattern;
											if (!float.TryParse(param.list[num6++], out num4))
											{
												num4 = 0f;
											}
											num4 = Mathf.Clamp01(num4);
											item.StartKey = num4;
											if (!float.TryParse(param.list[num6++], out num5))
											{
												num5 = 1f;
											}
											num5 = Mathf.Clamp01(num5);
											item.EndKey = num5;
											item.shape = default(MotionIKData.Shape);
											item.shape.shapeNo = shapeNo2;
											item.shape.small.pos.x = array[0, 0, 0];
											item.shape.small.pos.y = array[0, 0, 1];
											item.shape.small.pos.z = array[0, 0, 2];
											item.shape.large.pos.x = array[1, 0, 0];
											item.shape.large.pos.y = array[1, 0, 1];
											item.shape.large.pos.z = array[1, 0, 2];
											list[index].parts[num7 / 2].param2.blendInfos[0].Add(item);
											if (!int.TryParse(param.list[num6++], out pattern))
											{
												pattern = 0;
											}
											item.pattern = pattern;
											if (!float.TryParse(param.list[num6++], out num4))
											{
												num4 = 0f;
											}
											num4 = Mathf.Clamp01(num4);
											item.StartKey = num4;
											if (!float.TryParse(param.list[num6++], out num5))
											{
												num5 = 1f;
											}
											num5 = Mathf.Clamp01(num5);
											item.EndKey = num5;
											item.shape = default(MotionIKData.Shape);
											item.shape.shapeNo = shapeNo2;
											item.shape.small.ang.x = array[0, 1, 0];
											item.shape.small.ang.y = array[0, 1, 1];
											item.shape.small.ang.z = array[0, 1, 2];
											item.shape.large.ang.x = array[1, 1, 0];
											item.shape.large.ang.y = array[1, 1, 1];
											item.shape.large.ang.z = array[1, 1, 2];
											list[index].parts[num7 / 2].param2.blendInfos[1].Add(item);
										}
										else
										{
											if (!int.TryParse(param.list[num6++], out pattern))
											{
												pattern = 0;
											}
											MotionIKData.BlendWeightInfo item;
											item.pattern = pattern;
											if (!float.TryParse(param.list[num6++], out num4))
											{
												num4 = 0f;
											}
											num4 = Mathf.Clamp01(num4);
											item.StartKey = num4;
											if (!float.TryParse(param.list[num6++], out num5))
											{
												num5 = 1f;
											}
											num5 = Mathf.Clamp01(num5);
											item.EndKey = num5;
											item.shape = default(MotionIKData.Shape);
											item.shape.shapeNo = shapeNo2;
											item.shape.small.pos.x = array[0, 0, 0];
											item.shape.small.pos.y = array[0, 0, 1];
											item.shape.small.pos.z = array[0, 0, 2];
											item.shape.small.ang.x = array[0, 1, 0];
											item.shape.small.ang.y = array[0, 1, 1];
											item.shape.small.ang.z = array[0, 1, 2];
											item.shape.large.pos.x = array[1, 0, 0];
											item.shape.large.pos.y = array[1, 0, 1];
											item.shape.large.pos.z = array[1, 0, 2];
											item.shape.large.ang.x = array[1, 1, 0];
											item.shape.large.ang.y = array[1, 1, 1];
											item.shape.large.ang.z = array[1, 1, 2];
											list[index].parts[num7 / 2].param3.blendInfos.Add(item);
											num6 += 3;
										}
										if (!flag)
										{
											list[index].frames[num3].frameNo = num3++;
											flag = (num3 == 8);
										}
									}
								}
							}
						}
						this.MapIKData[valueTuple.Item2].states = new MotionIKData.State[list.Count];
						for (int k = 0; k < list.Count; k++)
						{
							this.MapIKData[valueTuple.Item2].states[k] = list[k];
						}
					}
				}
			}
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00175134 File Offset: 0x00173534
		private bool CheckMapIKBlankLine(ExcelData.Param param)
		{
			for (int i = 1; i < param.list.Count; i++)
			{
				if (param.list[i] != string.Empty)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x0017517B File Offset: 0x0017357B
		public static Dictionary<string, int> StatusTagTable { get; }

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x00175182 File Offset: 0x00173582
		public DefinePack DefinePack
		{
			[CompilerGenerated]
			get
			{
				return this._definePack;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003E32 RID: 15922 RVA: 0x0017518A File Offset: 0x0017358A
		public SoundPack SoundPack
		{
			[CompilerGenerated]
			get
			{
				return this._soundPack;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x00175192 File Offset: 0x00173592
		public LocomotionProfile LocomotionProfile
		{
			[CompilerGenerated]
			get
			{
				return this._locomotionProfile;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x0017519A File Offset: 0x0017359A
		public PlayerProfile PlayerProfile
		{
			[CompilerGenerated]
			get
			{
				return this._playerProfile;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x001751A2 File Offset: 0x001735A2
		public AgentProfile AgentProfile
		{
			[CompilerGenerated]
			get
			{
				return this._agentProfile;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x001751AA File Offset: 0x001735AA
		public StatusProfile StatusProfile
		{
			[CompilerGenerated]
			get
			{
				return this._statusProfile;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x001751B2 File Offset: 0x001735B2
		public CommonDefine CommonDefine
		{
			[CompilerGenerated]
			get
			{
				return this._commonDefine;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x001751BA File Offset: 0x001735BA
		public MerchantProfile MerchantProfile
		{
			[CompilerGenerated]
			get
			{
				return this._merchantProfile;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06003E39 RID: 15929 RVA: 0x001751C2 File Offset: 0x001735C2
		public FishingDefinePack FishingDefinePack
		{
			[CompilerGenerated]
			get
			{
				return this._fishingDefinePack;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x001751CA File Offset: 0x001735CA
		public AnimalDefinePack AnimalDefinePack
		{
			[CompilerGenerated]
			get
			{
				return this._animalDefinePack;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x001751D2 File Offset: 0x001735D2
		// (set) Token: 0x06003E3C RID: 15932 RVA: 0x001751DA File Offset: 0x001735DA
		public ChaFileCoordinate BathDefaultCoord { get; private set; }

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06003E3D RID: 15933 RVA: 0x001751E3 File Offset: 0x001735E3
		// (set) Token: 0x06003E3E RID: 15934 RVA: 0x001751EB File Offset: 0x001735EB
		public bool LoadAssetBundle { get; set; }

		// Token: 0x06003E3F RID: 15935 RVA: 0x001751F4 File Offset: 0x001735F4
		public void BeginLoadAssetBundle()
		{
			this._loadedAssetBundles.Clear();
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x00175204 File Offset: 0x00173604
		public void AddLoadAssetBundle(string assetBundleName, string manifestName)
		{
			if (manifestName.IsNullOrEmpty())
			{
				manifestName = this._mainManifestName;
			}
			if (!this._loadedAssetBundles.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == assetBundleName && x.Item2 == manifestName))
			{
				this._loadedAssetBundles.Add(new UnityEx.ValueTuple<string, string>(assetBundleName, manifestName));
			}
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x0017527C File Offset: 0x0017367C
		public void EndLoadAssetBundle(bool forceRemove = false)
		{
			foreach (UnityEx.ValueTuple<string, string> valueTuple in this._loadedAssetBundles)
			{
				AssetBundleManager.UnloadAssetBundle(valueTuple.Item1, true, null, forceRemove);
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			this._loadedAssetBundles.Clear();
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x001752F8 File Offset: 0x001736F8
		// (set) Token: 0x06003E43 RID: 15939 RVA: 0x00175300 File Offset: 0x00173700
		public Resources.AnimationTables Animation { get; private set; } = new Resources.AnimationTables();

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x00175309 File Offset: 0x00173709
		// (set) Token: 0x06003E45 RID: 15941 RVA: 0x00175311 File Offset: 0x00173711
		public Resources.ActionTable Action { get; private set; } = new Resources.ActionTable();

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06003E46 RID: 15942 RVA: 0x0017531A File Offset: 0x0017371A
		// (set) Token: 0x06003E47 RID: 15943 RVA: 0x00175322 File Offset: 0x00173722
		public Resources.BehaviorTreeTables BehaviorTree { get; private set; } = new Resources.BehaviorTreeTables();

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x0017532B File Offset: 0x0017372B
		// (set) Token: 0x06003E49 RID: 15945 RVA: 0x00175333 File Offset: 0x00173733
		public Resources.HSceneTables HSceneTable { get; private set; } = new Resources.HSceneTables();

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x0017533C File Offset: 0x0017373C
		// (set) Token: 0x06003E4B RID: 15947 RVA: 0x00175344 File Offset: 0x00173744
		public Resources.ItemIconTables itemIconTables { get; private set; } = new Resources.ItemIconTables();

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06003E4C RID: 15948 RVA: 0x0017534D File Offset: 0x0017374D
		// (set) Token: 0x06003E4D RID: 15949 RVA: 0x00175355 File Offset: 0x00173755
		public Dictionary<int, int> DefaultRequiredExpTablePrimal { get; private set; } = new Dictionary<int, int>();

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x0017535E File Offset: 0x0017375E
		// (set) Token: 0x06003E4F RID: 15951 RVA: 0x00175366 File Offset: 0x00173766
		public Resources.FishingTable Fishing { get; private set; } = new Resources.FishingTable();

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06003E50 RID: 15952 RVA: 0x0017536F File Offset: 0x0017376F
		// (set) Token: 0x06003E51 RID: 15953 RVA: 0x00175377 File Offset: 0x00173777
		public Resources.AnimalTables AnimalTable { get; private set; } = new Resources.AnimalTables();

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x00175380 File Offset: 0x00173780
		// (set) Token: 0x06003E53 RID: 15955 RVA: 0x00175388 File Offset: 0x00173788
		public Resources.PopupInfoTable PopupInfo { get; private set; } = new Resources.PopupInfoTable();

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003E54 RID: 15956 RVA: 0x00175391 File Offset: 0x00173791
		// (set) Token: 0x06003E55 RID: 15957 RVA: 0x00175399 File Offset: 0x00173799
		public Resources.SoundTable Sound { get; private set; } = new Resources.SoundTable();

		// Token: 0x06003E56 RID: 15958 RVA: 0x001753A4 File Offset: 0x001737A4
		public bool IsRecognizable(int lv, Rarelity rarelity)
		{
			Rarelity source = this._recognizableShapeFilterTable[lv];
			return source.Contains(rarelity);
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003E57 RID: 15959 RVA: 0x001753C5 File Offset: 0x001737C5
		// (set) Token: 0x06003E58 RID: 15960 RVA: 0x001753CD File Offset: 0x001737CD
		public Dictionary<int, Dictionary<int, List<Vector3>>> WaypointDataList { get; private set; } = new Dictionary<int, Dictionary<int, List<Vector3>>>();

		// Token: 0x06003E59 RID: 15961 RVA: 0x001753D8 File Offset: 0x001737D8
		public Dictionary<int, float> GetDesireAddRateTable(int personalID, AIProject.TimeZone zone)
		{
			int key = AIProject.Definitions.Environment.TimeZoneIDTable[zone];
			Dictionary<int, Dictionary<int, float>> dictionary;
			if (!this._desireAddRateMultiTable.TryGetValue(personalID, out dictionary))
			{
				return null;
			}
			Dictionary<int, float> result;
			if (!dictionary.TryGetValue(key, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00175418 File Offset: 0x00173818
		public UnityEx.ValueTuple<int, int> GetDesireBorder(int key)
		{
			UnityEx.ValueTuple<int, int> result;
			if (!this._desireBorderTable.TryGetValue(key, out result))
			{
				return default(UnityEx.ValueTuple<int, int>);
			}
			return result;
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06003E5B RID: 15963 RVA: 0x00175443 File Offset: 0x00173843
		// (set) Token: 0x06003E5C RID: 15964 RVA: 0x0017544B File Offset: 0x0017384B
		public Dictionary<int, string> FeatureNameTable { get; private set; }

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003E5D RID: 15965 RVA: 0x00175454 File Offset: 0x00173854
		// (set) Token: 0x06003E5E RID: 15966 RVA: 0x0017545C File Offset: 0x0017385C
		public Dictionary<int, UnityEx.ValueTuple<int, int>> FeatureParameterTable { get; private set; }

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06003E5F RID: 15967 RVA: 0x00175465 File Offset: 0x00173865
		// (set) Token: 0x06003E60 RID: 15968 RVA: 0x0017546D File Offset: 0x0017386D
		public Resources.GameInfoTables GameInfo { get; private set; } = new Resources.GameInfoTables();

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06003E61 RID: 15969 RVA: 0x00175476 File Offset: 0x00173876
		// (set) Token: 0x06003E62 RID: 15970 RVA: 0x0017547E File Offset: 0x0017387E
		public Resources.MapTables Map { get; private set; } = new Resources.MapTables();

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003E63 RID: 15971 RVA: 0x00175487 File Offset: 0x00173887
		public IObservable<Unit> LoadMapResourceStream
		{
			[CompilerGenerated]
			get
			{
				return this._loadMapResourceStream;
			}
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x0017548F File Offset: 0x0017388F
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x0017549D File Offset: 0x0017389D
		public void PreSetup()
		{
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x0017549F File Offset: 0x0017389F
		public void SetupMap()
		{
			this._loadMapResourceStream = Observable.FromCoroutine(() => this.LoadMapResources(), false).PublishLast<Unit>();
			this._loadMapResourceStream.Connect();
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x001754CC File Offset: 0x001738CC
		public IEnumerator LoadMapResources()
		{
			this.BathDefaultCoord = new ChaFileCoordinate();
			TextAsset ta = CommonLib.LoadAsset<TextAsset>(this._definePack.ABPaths.ActorPrefab, "bath_coord_def", false, string.Empty);
			if (ta != null)
			{
				this.BathDefaultCoord.LoadFile(ta);
			}
			this.Animation.Load(this._definePack, true);
			this.Action.Load(this._definePack);
			this.BehaviorTree.Load(this._definePack);
			this.Fishing.Load(this._fishingDefinePack);
			this.AnimalTable.Load(this._animalDefinePack);
			this.PopupInfo.Load(this._definePack);
			this.Map.Load(this._definePack);
			this.Sound.Load(this._definePack);
			yield return this.itemIconTables.LoadIcon(Resources.ItemIconTables.IconCategory.System);
			yield return this.itemIconTables.LoadIcon(Resources.ItemIconTables.IconCategory.Menu);
			yield return this.itemIconTables.LoadIcon(Resources.ItemIconTables.IconCategory.Category);
			yield return this.itemIconTables.LoadIcon(Resources.ItemIconTables.IconCategory.Item);
			yield return this.itemIconTables.LoadMinimapActionIconList();
			yield return this.itemIconTables.LoadMinimapActionIconNameList(this._definePack);
			this.GameInfo.Load(this._definePack);
			this.itemIconTables.Load(this._definePack);
			this.LoadWaypointData();
			this.LoadDesireInfo();
			yield return this.HSceneTable.LoadH();
			GC.Collect();
			yield break;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x001754E8 File Offset: 0x001738E8
		public void ReleaseMapResources()
		{
			this.Animation.Release();
			this.Action.Release();
			this.BehaviorTree.Release();
			this.Fishing.Release();
			this.AnimalTable.Release();
			this.PopupInfo.Release();
			this.Map.Release();
			this.Sound.Release();
			this.itemIconTables.Release();
			this.GameInfo.Release();
			this.WaypointDataList.Clear();
			this._desireAddRateMultiTable.Clear();
			this._desireBorderTable.Clear();
			this.HSceneTable.Release();
			GC.Collect();
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00175594 File Offset: 0x00173994
		private void LoadWaypointData()
		{
			List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(this._definePack.ABDirectories.WaypointList, false);
			assetBundleNameListFromPath.Sort();
			foreach (string assetBundleName in assetBundleNameListFromPath)
			{
				foreach (NavMeshWayPointData navMeshWayPointData in AssetBundleManager.LoadAllAsset(assetBundleName, typeof(NavMeshWayPointData), null).GetAllAssets<NavMeshWayPointData>())
				{
					Dictionary<int, List<Vector3>> dictionary;
					if (!this.WaypointDataList.TryGetValue(navMeshWayPointData.MapID, out dictionary))
					{
						Dictionary<int, List<Vector3>> dictionary2 = new Dictionary<int, List<Vector3>>();
						this.WaypointDataList[navMeshWayPointData.MapID] = dictionary2;
						dictionary = dictionary2;
					}
					List<Vector3> list;
					if (!dictionary.TryGetValue(navMeshWayPointData.AreaID, out list))
					{
						List<Vector3> list2 = new List<Vector3>();
						dictionary[navMeshWayPointData.AreaID] = list2;
						list = list2;
					}
					list.AddRange(navMeshWayPointData.Points);
				}
			}
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x001756AC File Offset: 0x00173AAC
		private IEnumerator LoadExperience()
		{
			yield return null;
			yield break;
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x001756C0 File Offset: 0x00173AC0
		private void LoadDesireInfo()
		{
			List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(this._definePack.ABDirectories.AgentDesire, false);
			assetBundleNameListFromPath.Sort();
			foreach (string text in assetBundleNameListFromPath)
			{
				if (!Game.IsRestrictedOver50(text))
				{
					foreach (DesireRateData desireRateData in AssetBundleManager.LoadAllAsset(text, typeof(DesireRateData), null).GetAllAssets<DesireRateData>())
					{
						foreach (DesireRateData.Param param in desireRateData.param)
						{
							Dictionary<int, Dictionary<int, float>> dictionary;
							if (!this._desireAddRateMultiTable.TryGetValue(param.PersonalID, out dictionary))
							{
								Dictionary<int, Dictionary<int, float>> dictionary2 = new Dictionary<int, Dictionary<int, float>>();
								this._desireAddRateMultiTable[param.PersonalID] = dictionary2;
								dictionary = dictionary2;
							}
							Dictionary<int, float> dictionary3;
							if (!dictionary.TryGetValue(0, out dictionary3))
							{
								Dictionary<int, float> dictionary4 = new Dictionary<int, float>();
								dictionary[0] = dictionary4;
								dictionary3 = dictionary4;
							}
							Dictionary<int, float> dictionary5;
							if (!dictionary.TryGetValue(1, out dictionary5))
							{
								Dictionary<int, float> dictionary4 = new Dictionary<int, float>();
								dictionary[1] = dictionary4;
								dictionary5 = dictionary4;
							}
							Dictionary<int, float> dictionary6;
							if (!dictionary.TryGetValue(2, out dictionary6))
							{
								Dictionary<int, float> dictionary4 = new Dictionary<int, float>();
								dictionary[2] = dictionary4;
								dictionary6 = dictionary4;
							}
							dictionary3[param.ID] = param.Morning;
							dictionary5[param.ID] = param.Day;
							dictionary6[param.ID] = param.Night;
						}
					}
					foreach (DesireBorderData desireBorderData in AssetBundleManager.LoadAllAsset(text, typeof(DesireBorderData), null).GetAllAssets<DesireBorderData>())
					{
						foreach (DesireBorderData.Param param2 in desireBorderData.param)
						{
							this._desireBorderTable[param2.ID] = new UnityEx.ValueTuple<int, int>(param2.Border, param2.Limit);
						}
					}
					AssetBundleManager.UnloadAssetBundle(text, false, null, false);
				}
			}
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x00175970 File Offset: 0x00173D70
		// Note: this type is marked as 'beforefieldinit'.
		static Resources()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary["体温"] = 0;
			dictionary["機嫌"] = 1;
			dictionary["満腹"] = 2;
			dictionary["体調"] = 3;
			dictionary["生命"] = 4;
			dictionary["やる気"] = 5;
			dictionary["H"] = 6;
			dictionary["善悪"] = 7;
			dictionary["女子力"] = 10;
			dictionary["信頼"] = 11;
			dictionary["人間性"] = 12;
			dictionary["本能"] = 13;
			dictionary["変態"] = 14;
			dictionary["警戒"] = 15;
			dictionary["闇"] = 16;
			dictionary["社交"] = 17;
			dictionary["トイレ"] = 100;
			dictionary["風呂"] = 101;
			dictionary["睡眠"] = 102;
			dictionary["食事"] = 103;
			dictionary["休憩"] = 104;
			dictionary["ギフト"] = 105;
			dictionary["おねだり"] = 106;
			dictionary["寂しい"] = 107;
			dictionary["H欲"] = 108;
			dictionary["採取"] = 110;
			dictionary["遊び"] = 111;
			dictionary["料理"] = 112;
			dictionary["動物"] = 113;
			dictionary["ロケ"] = 114;
			dictionary["飲み物"] = 115;
			Resources.StatusTagTable = dictionary;
		}

		// Token: 0x04003B69 RID: 15209
		private static readonly string[] _separators = new string[]
		{
			"/",
			"／"
		};

		// Token: 0x04003B6A RID: 15210
		private static readonly char[] _separationKeywords = new char[]
		{
			',',
			'<',
			'>'
		};

		// Token: 0x04003B6B RID: 15211
		private static Regex _regex = new Regex("<((?:[\\w/.]*[,]*)+)>");

		// Token: 0x04003B6C RID: 15212
		public readonly Dictionary<string, MotionIKData> MapIKData = new Dictionary<string, MotionIKData>();

		// Token: 0x04003B6D RID: 15213
		private static readonly Dictionary<string, int> DicMapIKFrameID = new Dictionary<string, int>
		{
			{
				"f_t_arm_L",
				0
			},
			{
				"f_t_elbo_L",
				1
			},
			{
				"f_t_arm_R",
				2
			},
			{
				"f_t_elbo_R",
				3
			},
			{
				"f_t_leg_L",
				4
			},
			{
				"f_t_knee_L",
				5
			},
			{
				"f_t_leg_R",
				6
			},
			{
				"f_t_knee_R",
				7
			}
		};

		// Token: 0x04003B6F RID: 15215
		[SerializeField]
		private DefinePack _definePack;

		// Token: 0x04003B70 RID: 15216
		[SerializeField]
		private SoundPack _soundPack;

		// Token: 0x04003B71 RID: 15217
		[SerializeField]
		private LocomotionProfile _locomotionProfile;

		// Token: 0x04003B72 RID: 15218
		[SerializeField]
		private PlayerProfile _playerProfile;

		// Token: 0x04003B73 RID: 15219
		[SerializeField]
		private AgentProfile _agentProfile;

		// Token: 0x04003B74 RID: 15220
		[SerializeField]
		private StatusProfile _statusProfile;

		// Token: 0x04003B75 RID: 15221
		[SerializeField]
		private CommonDefine _commonDefine;

		// Token: 0x04003B76 RID: 15222
		[SerializeField]
		private MerchantProfile _merchantProfile;

		// Token: 0x04003B77 RID: 15223
		[SerializeField]
		private FishingDefinePack _fishingDefinePack;

		// Token: 0x04003B78 RID: 15224
		[SerializeField]
		private AnimalDefinePack _animalDefinePack;

		// Token: 0x04003B7A RID: 15226
		private List<UnityEx.ValueTuple<string, string>> _loadedAssetBundles = new List<UnityEx.ValueTuple<string, string>>();

		// Token: 0x04003B7B RID: 15227
		private readonly string _mainManifestName = "abdata";

		// Token: 0x04003B87 RID: 15239
		private Dictionary<int, Rarelity> _recognizableShapeFilterTable = new Dictionary<int, Rarelity>();

		// Token: 0x04003B89 RID: 15241
		private Dictionary<int, Dictionary<int, Dictionary<int, float>>> _desireAddRateMultiTable = new Dictionary<int, Dictionary<int, Dictionary<int, float>>>();

		// Token: 0x04003B8A RID: 15242
		private Dictionary<int, UnityEx.ValueTuple<int, int>> _desireBorderTable = new Dictionary<int, UnityEx.ValueTuple<int, int>>();

		// Token: 0x04003B8F RID: 15247
		private IConnectableObservable<Unit> _loadMapResourceStream;

		// Token: 0x020008EC RID: 2284
		public class ActionTable
		{
			// Token: 0x17000B80 RID: 2944
			// (get) Token: 0x06003E6F RID: 15983 RVA: 0x00175C75 File Offset: 0x00174075
			// (set) Token: 0x06003E70 RID: 15984 RVA: 0x00175C7D File Offset: 0x0017407D
			public Dictionary<int, Dictionary<int, int>> PhaseExp { get; private set; } = new Dictionary<int, Dictionary<int, int>>();

			// Token: 0x17000B81 RID: 2945
			// (get) Token: 0x06003E71 RID: 15985 RVA: 0x00175C86 File Offset: 0x00174086
			// (set) Token: 0x06003E72 RID: 15986 RVA: 0x00175C8E File Offset: 0x0017408E
			public Dictionary<int, Dictionary<int, Threshold>> PersonalityMotivation { get; private set; } = new Dictionary<int, Dictionary<int, Threshold>>();

			// Token: 0x17000B82 RID: 2946
			// (get) Token: 0x06003E73 RID: 15987 RVA: 0x00175C97 File Offset: 0x00174097
			// (set) Token: 0x06003E74 RID: 15988 RVA: 0x00175C9F File Offset: 0x0017409F
			public Dictionary<int, Dictionary<int, int>> LifestyleTable { get; private set; } = new Dictionary<int, Dictionary<int, int>>();

			// Token: 0x17000B83 RID: 2947
			// (get) Token: 0x06003E75 RID: 15989 RVA: 0x00175CA8 File Offset: 0x001740A8
			// (set) Token: 0x06003E76 RID: 15990 RVA: 0x00175CB0 File Offset: 0x001740B0
			public Dictionary<int, Dictionary<int, ObtainItemInfo>> FlavorPickSkillTable { get; private set; } = new Dictionary<int, Dictionary<int, ObtainItemInfo>>();

			// Token: 0x17000B84 RID: 2948
			// (get) Token: 0x06003E77 RID: 15991 RVA: 0x00175CB9 File Offset: 0x001740B9
			// (set) Token: 0x06003E78 RID: 15992 RVA: 0x00175CC1 File Offset: 0x001740C1
			public Dictionary<int, Dictionary<int, ObtainItemInfo>> FlavorPickHSkillTable { get; private set; } = new Dictionary<int, Dictionary<int, ObtainItemInfo>>();

			// Token: 0x17000B85 RID: 2949
			// (get) Token: 0x06003E79 RID: 15993 RVA: 0x00175CCA File Offset: 0x001740CA
			// (set) Token: 0x06003E7A RID: 15994 RVA: 0x00175CD2 File Offset: 0x001740D2
			public Dictionary<int, Dictionary<int, ActAnimFlagData>> AgentActionFlagTable { get; private set; } = new Dictionary<int, Dictionary<int, ActAnimFlagData>>();

			// Token: 0x17000B86 RID: 2950
			// (get) Token: 0x06003E7B RID: 15995 RVA: 0x00175CDB File Offset: 0x001740DB
			// (set) Token: 0x06003E7C RID: 15996 RVA: 0x00175CE3 File Offset: 0x001740E3
			public Dictionary<int, Dictionary<int, Dictionary<int, ParameterPacket>>> ActionStatusResultTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, ParameterPacket>>>();

			// Token: 0x17000B87 RID: 2951
			// (get) Token: 0x06003E7D RID: 15997 RVA: 0x00175CEC File Offset: 0x001740EC
			// (set) Token: 0x06003E7E RID: 15998 RVA: 0x00175CF4 File Offset: 0x001740F4
			public Dictionary<int, Dictionary<int, ParameterPacket>> SituationStatusResultTable { get; private set; } = new Dictionary<int, Dictionary<int, ParameterPacket>>();

			// Token: 0x17000B88 RID: 2952
			// (get) Token: 0x06003E7F RID: 15999 RVA: 0x00175CFD File Offset: 0x001740FD
			// (set) Token: 0x06003E80 RID: 16000 RVA: 0x00175D05 File Offset: 0x00174105
			public Dictionary<int, Dictionary<int, string>> ActionExpressionTable { get; private set; } = new Dictionary<int, Dictionary<int, string>>();

			// Token: 0x17000B89 RID: 2953
			// (get) Token: 0x06003E81 RID: 16001 RVA: 0x00175D0E File Offset: 0x0017410E
			// (set) Token: 0x06003E82 RID: 16002 RVA: 0x00175D16 File Offset: 0x00174116
			public Dictionary<int, Dictionary<int, List<ExpressionKeyframe>>> ActionExpressionKeyframeTable { get; private set; } = new Dictionary<int, Dictionary<int, List<ExpressionKeyframe>>>();

			// Token: 0x17000B8A RID: 2954
			// (get) Token: 0x06003E83 RID: 16003 RVA: 0x00175D1F File Offset: 0x0017411F
			// (set) Token: 0x06003E84 RID: 16004 RVA: 0x00175D27 File Offset: 0x00174127
			public Dictionary<string, Dictionary<int, YureCtrl.Info>> ActionYureTable { get; private set; } = new Dictionary<string, Dictionary<int, YureCtrl.Info>>();

			// Token: 0x17000B8B RID: 2955
			// (get) Token: 0x06003E85 RID: 16005 RVA: 0x00175D30 File Offset: 0x00174130
			// (set) Token: 0x06003E86 RID: 16006 RVA: 0x00175D38 File Offset: 0x00174138
			public Dictionary<int, int> AgentLocomotionBreathTable { get; private set; } = new Dictionary<int, int>();

			// Token: 0x17000B8C RID: 2956
			// (get) Token: 0x06003E87 RID: 16007 RVA: 0x00175D41 File Offset: 0x00174141
			// (set) Token: 0x06003E88 RID: 16008 RVA: 0x00175D49 File Offset: 0x00174149
			public Dictionary<int, ABInfoData.Param> ComCameraList { get; private set; } = new Dictionary<int, ABInfoData.Param>();

			// Token: 0x17000B8D RID: 2957
			// (get) Token: 0x06003E89 RID: 16009 RVA: 0x00175D52 File Offset: 0x00174152
			// (set) Token: 0x06003E8A RID: 16010 RVA: 0x00175D5A File Offset: 0x0017415A
			public Dictionary<int, Dictionary<int, ByproductInfo>> ByproductList { get; private set; } = new Dictionary<int, Dictionary<int, ByproductInfo>>();

			// Token: 0x06003E8B RID: 16011 RVA: 0x00175D64 File Offset: 0x00174164
			public void Load(DefinePack definePack)
			{
				this.LoadPhaseExp(definePack);
				this.LoadPersonalityMotivation(definePack);
				this.LoadLifestyleTable(definePack);
				this.LoadFlavorPickSkillTable(definePack);
				this.LoadFlavorPickHSkillTable(definePack);
				this.LoadActionTalkFlags(definePack);
				this.LoadLocmotionBreathTable(definePack);
				this.LoadActionExpressionTable(definePack);
				this.LoadActionExpressionKeyFrameTable(definePack);
				this.LoadActionBustCtrlTable(definePack);
				this.LoadActionResultTable(definePack);
				this.LoadSituationResultTable(definePack);
				this.LoadComCemra(definePack);
				this.LoadByproductList(definePack);
			}

			// Token: 0x06003E8C RID: 16012 RVA: 0x00175DD4 File Offset: 0x001741D4
			public void Release()
			{
				this.PhaseExp.Clear();
				this.PersonalityMotivation.Clear();
				this.LifestyleTable.Clear();
				this.FlavorPickSkillTable.Clear();
				this.FlavorPickHSkillTable.Clear();
				this.AgentActionFlagTable.Clear();
				this.ActionStatusResultTable.Clear();
				this.SituationStatusResultTable.Clear();
				this.ActionExpressionTable.Clear();
				this.ActionExpressionKeyframeTable.Clear();
				this.ActionYureTable.Clear();
				this.AgentLocomotionBreathTable.Clear();
				this.ComCameraList.Clear();
				this.ByproductList.Clear();
			}

			// Token: 0x06003E8D RID: 16013 RVA: 0x00175E7C File Offset: 0x0017427C
			private void LoadPhaseExp(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentPhase, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (PhaseExpData phaseExpData in AssetBundleManager.LoadAllAsset(text, typeof(PhaseExpData), null).GetAllAssets<PhaseExpData>())
						{
							foreach (PhaseExpData.Param param in phaseExpData.param)
							{
								Dictionary<int, int> dictionary;
								if (!this.PhaseExp.TryGetValue(param.Personality, out dictionary))
								{
									Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
									this.PhaseExp[param.Personality] = dictionary2;
									dictionary = dictionary2;
								}
								for (int j = 0; j < param.ExpArray.Count; j++)
								{
									string text2 = param.ExpArray[j];
									if (!text2.IsNullOrEmpty())
									{
										int num;
										int value = (!int.TryParse(text2, out num)) ? 0 : num;
										dictionary[j] = value;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003E8E RID: 16014 RVA: 0x00176028 File Offset: 0x00174428
			private void LoadPersonalityMotivation(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentPersonalityMotivation, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ExcelData excelData in AssetBundleManager.LoadAllAsset(text, typeof(ExcelData), null).GetAllAssets<ExcelData>())
						{
							int key;
							if (int.TryParse(excelData.name, out key))
							{
								Dictionary<int, Threshold> dictionary;
								if (!this.PersonalityMotivation.TryGetValue(key, out dictionary))
								{
									Dictionary<int, Threshold> dictionary2 = new Dictionary<int, Threshold>();
									this.PersonalityMotivation[key] = dictionary2;
									dictionary = dictionary2;
								}
								foreach (ExcelData.Param param in excelData.list)
								{
									int num = 0;
									int key2;
									if (int.TryParse(param.list.GetElement(num++), out key2))
									{
										float minValue;
										if (float.TryParse(param.list.GetElement(num++), out minValue))
										{
											float maxValue;
											if (float.TryParse(param.list.GetElement(num++), out maxValue))
											{
												dictionary[key2] = new Threshold(minValue, maxValue);
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003E8F RID: 16015 RVA: 0x001761FC File Offset: 0x001745FC
			private void LoadLifestyleTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.LifestyleTable, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ExcelData excelData in AssetBundleManager.LoadAllAsset(text, typeof(ExcelData), null).GetAllAssets<ExcelData>())
						{
							foreach (ExcelData.Param param in excelData.list)
							{
								int j = 0;
								int key;
								if (int.TryParse(param.list.GetElement(j++), out key))
								{
									Dictionary<int, int> dictionary;
									if (!this.LifestyleTable.TryGetValue(key, out dictionary))
									{
										Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
										this.LifestyleTable[key] = dictionary2;
										dictionary = dictionary2;
									}
									j++;
									int num = 0;
									while (j < param.list.Count)
									{
										int value;
										if (!int.TryParse(param.list.GetElement(j++), out value))
										{
											num++;
										}
										else
										{
											dictionary[num++] = value;
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003E90 RID: 16016 RVA: 0x001763BC File Offset: 0x001747BC
			private void LoadFlavorPickSkillTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.FlavorPickSkillTable, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (SkillObtainData skillObtainData in AssetBundleManager.LoadAllAsset(text, typeof(SkillObtainData), null).GetAllAssets<SkillObtainData>())
						{
							int key;
							if (int.TryParse(skillObtainData.name, out key))
							{
								Dictionary<int, ObtainItemInfo> dictionary;
								if (!this.FlavorPickSkillTable.TryGetValue(key, out dictionary))
								{
									Dictionary<int, ObtainItemInfo> dictionary2 = new Dictionary<int, ObtainItemInfo>();
									this.FlavorPickSkillTable[key] = dictionary2;
									dictionary = dictionary2;
								}
								foreach (SkillObtainData.Param param in skillObtainData.param)
								{
									dictionary[param.ID] = new ObtainItemInfo
									{
										Name = param.Name,
										Rate = param.Rate,
										Info = new ItemInfo
										{
											CategoryID = param.Category,
											ItemID = param.ItemID
										}
									};
								}
							}
						}
					}
				}
			}

			// Token: 0x06003E91 RID: 16017 RVA: 0x00176570 File Offset: 0x00174970
			private void LoadFlavorPickHSkillTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.FlavorPickHSkillTable, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (SkillObtainData skillObtainData in AssetBundleManager.LoadAllAsset(text, typeof(SkillObtainData), null).GetAllAssets<SkillObtainData>())
						{
							int key;
							if (int.TryParse(skillObtainData.name, out key))
							{
								Dictionary<int, ObtainItemInfo> dictionary;
								if (!this.FlavorPickHSkillTable.TryGetValue(key, out dictionary))
								{
									Dictionary<int, ObtainItemInfo> dictionary2 = new Dictionary<int, ObtainItemInfo>();
									this.FlavorPickHSkillTable[key] = dictionary2;
									dictionary = dictionary2;
								}
								foreach (SkillObtainData.Param param in skillObtainData.param)
								{
									dictionary[param.ID] = new ObtainItemInfo
									{
										Name = param.Name,
										Rate = param.Rate,
										Info = new ItemInfo
										{
											CategoryID = param.Category,
											ItemID = param.ItemID
										}
									};
								}
							}
						}
					}
				}
			}

			// Token: 0x06003E92 RID: 16018 RVA: 0x00176724 File Offset: 0x00174B24
			public void LoadActionTalkFlags(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentCommunicationFlags, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ActionTalkData actionTalkData in AssetBundleManager.LoadAllAsset(text, typeof(ActionTalkData), null).GetAllAssets<ActionTalkData>())
						{
							foreach (ActionTalkData.Param param in actionTalkData.param)
							{
								Dictionary<int, ActAnimFlagData> dictionary;
								if (!this.AgentActionFlagTable.TryGetValue(param.ActionID, out dictionary))
								{
									Dictionary<int, ActAnimFlagData> dictionary2 = new Dictionary<int, ActAnimFlagData>();
									this.AgentActionFlagTable[param.ActionID] = dictionary2;
									dictionary = dictionary2;
								}
								dictionary[param.PoseID] = new ActAnimFlagData
								{
									obstacleRadius = param.ObstacleRadius,
									useNeckLook = param.useNeckLook,
									canTalk = param.CanTalk,
									attitudeID = param.TalkAttitudeID,
									canHCommand = param.CanHCommand,
									isBadMood = param.IsBadMood,
									isSpecial = param.IsSpecial,
									hPositionID1 = param.HPositionID,
									hPositionID2 = param.HPositionSubID
								};
							}
							Singleton<Resources>.Instance.AddLoadAssetBundle(text, string.Empty);
						}
					}
				}
			}

			// Token: 0x06003E93 RID: 16019 RVA: 0x00176910 File Offset: 0x00174D10
			public void LoadActionResultTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentActionResult, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = CommonLib.LoadAsset<ExcelData>(text, fileNameWithoutExtension, false, string.Empty);
						if (!(excelData == null))
						{
							Singleton<Resources>.Instance.AddLoadAssetBundle(text, string.Empty);
							int j = 0;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								int k = 2;
								string element = param.list.GetElement(k++);
								int key;
								if (int.TryParse(element, out key))
								{
									string element2 = param.list.GetElement(k++);
									int key2;
									if (int.TryParse(element2, out key2))
									{
										string element3 = param.list.GetElement(k++);
										int key3;
										if (int.TryParse(element3, out key3))
										{
											int num2;
											int num = (!int.TryParse(param.list.GetElement(k++), out num2)) ? 0 : num2;
											ParameterPacket parameterPacket = new ParameterPacket
											{
												Probability = (float)num
											};
											while (k < param.list.Count)
											{
												string element4 = param.list.GetElement(k++);
												string element5 = param.list.GetElement(k++);
												string element6 = param.list.GetElement(k++);
												string element7 = param.list.GetElement(k++);
												if (!element4.IsNullOrEmpty())
												{
													int key4;
													if (Resources.StatusTagTable.TryGetValue(element4, out key4))
													{
														int s = (!int.TryParse(element5, out num2)) ? 0 : num2;
														int m = (!int.TryParse(element6, out num2)) ? 0 : num2;
														int l = (!int.TryParse(element7, out num2)) ? 0 : num2;
														parameterPacket.Parameters[key4] = new TriThreshold(s, m, l);
													}
												}
											}
											Dictionary<int, Dictionary<int, ParameterPacket>> dictionary;
											if (!this.ActionStatusResultTable.TryGetValue(key, out dictionary))
											{
												Dictionary<int, Dictionary<int, ParameterPacket>> dictionary2 = new Dictionary<int, Dictionary<int, ParameterPacket>>();
												this.ActionStatusResultTable[key] = dictionary2;
												dictionary = dictionary2;
											}
											Dictionary<int, ParameterPacket> dictionary3;
											if (!dictionary.TryGetValue(key2, out dictionary3))
											{
												Dictionary<int, ParameterPacket> dictionary4 = new Dictionary<int, ParameterPacket>();
												dictionary[key2] = dictionary4;
												dictionary3 = dictionary4;
											}
											dictionary3[key3] = parameterPacket;
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003E94 RID: 16020 RVA: 0x00176BD8 File Offset: 0x00174FD8
			private void LoadLocmotionBreathTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentLocomotionBreath, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (LocomotionBreathData locomotionBreathData in AssetBundleManager.LoadAllAsset(text, typeof(LocomotionBreathData), null).GetAllAssets<LocomotionBreathData>())
						{
							foreach (LocomotionBreathData.Param param in locomotionBreathData.param)
							{
								if (!param.State.IsNullOrEmpty())
								{
									int key = Animator.StringToHash(param.State);
									this.AgentLocomotionBreathTable[key] = param.VoiceID;
								}
							}
						}
					}
				}
			}

			// Token: 0x06003E95 RID: 16021 RVA: 0x00176D08 File Offset: 0x00175108
			public void LoadActionExpressionTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActionExpList, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (MotionExpressionData motionExpressionData in AssetBundleManager.LoadAllAsset(text, typeof(MotionExpressionData), null).GetAllAssets<MotionExpressionData>())
						{
							int key = int.Parse(motionExpressionData.name.Replace("c", string.Empty));
							Dictionary<int, string> dictionary;
							if (!this.ActionExpressionTable.TryGetValue(key, out dictionary))
							{
								Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
								this.ActionExpressionTable[key] = dictionary2;
								dictionary = dictionary2;
							}
							foreach (MotionExpressionData.Param param in motionExpressionData.param)
							{
								if (!param.State.IsNullOrEmpty())
								{
									int key2 = Animator.StringToHash(param.State);
									dictionary[key2] = param.ExpressionName;
								}
							}
						}
						Singleton<Resources>.Instance.AddLoadAssetBundle(text, string.Empty);
					}
				}
			}

			// Token: 0x06003E96 RID: 16022 RVA: 0x00176EA4 File Offset: 0x001752A4
			public void LoadActionExpressionKeyFrameTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActionExpKeyFrameList, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ExcelData excelData in AssetBundleManager.LoadAllAsset(text, typeof(ExcelData), null).GetAllAssets<ExcelData>())
						{
							int key = int.Parse(excelData.name.Replace("c", string.Empty));
							Dictionary<int, List<ExpressionKeyframe>> dictionary;
							if (!this.ActionExpressionKeyframeTable.TryGetValue(key, out dictionary))
							{
								Dictionary<int, List<ExpressionKeyframe>> dictionary2 = new Dictionary<int, List<ExpressionKeyframe>>();
								this.ActionExpressionKeyframeTable[key] = dictionary2;
								dictionary = dictionary2;
							}
							foreach (ExcelData.Param param in excelData.list)
							{
								int j = 1;
								string element = param.list.GetElement(j++);
								if (!element.IsNullOrEmpty())
								{
									int key2 = Animator.StringToHash(element);
									List<ExpressionKeyframe> list;
									if (!dictionary.TryGetValue(key2, out list))
									{
										List<ExpressionKeyframe> list2 = new List<ExpressionKeyframe>();
										dictionary[key2] = list2;
										list = list2;
									}
									while (j < param.list.Count)
									{
										string element2 = param.list.GetElement(j++);
										string element3 = param.list.GetElement(j++);
										float normalizedTime;
										if (float.TryParse(element2, out normalizedTime))
										{
											list.Add(new ExpressionKeyframe
											{
												normalizedTime = normalizedTime,
												expressionName = element3
											});
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003E97 RID: 16023 RVA: 0x001770C8 File Offset: 0x001754C8
			public void LoadActionBustCtrlTable(DefinePack definePack)
			{
				if (definePack.ABDirectories.ActionBustCtrlList.IsNullOrEmpty())
				{
					return;
				}
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActionBustCtrlList, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (MotionBustCtrlData motionBustCtrlData in AssetBundleManager.LoadAllAsset(text, typeof(MotionBustCtrlData), null).GetAllAssets<MotionBustCtrlData>())
						{
							Dictionary<int, YureCtrl.Info> dictionary;
							if (!this.ActionYureTable.TryGetValue(motionBustCtrlData.name, out dictionary))
							{
								Dictionary<int, YureCtrl.Info> dictionary2 = new Dictionary<int, YureCtrl.Info>();
								this.ActionYureTable[motionBustCtrlData.name] = dictionary2;
								dictionary = dictionary2;
							}
							foreach (MotionBustCtrlData.Param param in motionBustCtrlData.param)
							{
								if (!param.State.IsNullOrEmpty())
								{
									int key = Animator.StringToHash(param.State);
									YureCtrl.Info info = new YureCtrl.Info();
									dictionary[key] = info;
									YureCtrl.Info info2 = info;
									int num = 0;
									info2.aIsActive[0] = (param.Parameters.GetElement(num++) == "1");
									info2.aBreastShape[0].MemberInit();
									for (int j = 0; j < Resources.ActionTable._bustIndexes.Length; j++)
									{
										info2.aBreastShape[0].breast[j] = (param.Parameters.GetElement(num++) == "1");
									}
									info2.aBreastShape[0].nip = (param.Parameters.GetElement(num++) == "1");
									info2.aIsActive[1] = (param.Parameters.GetElement(num++) == "1");
									info2.aBreastShape[1].MemberInit();
									for (int k = 0; k < Resources.ActionTable._bustIndexes.Length; k++)
									{
										info2.aBreastShape[1].breast[k] = (param.Parameters.GetElement(num++) == "1");
									}
									info2.aBreastShape[1].nip = (param.Parameters.GetElement(num++) == "1");
									info2.aIsActive[2] = (param.Parameters.GetElement(num++) == "1");
									info2.aIsActive[3] = (param.Parameters.GetElement(num++) == "1");
								}
							}
						}
						Singleton<Resources>.Instance.AddLoadAssetBundle(text, string.Empty);
					}
				}
			}

			// Token: 0x06003E98 RID: 16024 RVA: 0x00177424 File Offset: 0x00175824
			public void LoadSituationResultTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentSituationResult, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							Singleton<Resources>.Instance.AddLoadAssetBundle(text, string.Empty);
							int j = 0;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								int k = 1;
								string element = param.list.GetElement(k++);
								int key;
								if (int.TryParse(element, out key))
								{
									string element2 = param.list.GetElement(k++);
									int key2;
									if (int.TryParse(element2, out key2))
									{
										int num2;
										int num = (!int.TryParse(param.list.GetElement(k++), out num2)) ? 0 : num2;
										ParameterPacket parameterPacket = new ParameterPacket
										{
											Probability = (float)num
										};
										while (k < param.list.Count)
										{
											string element3 = param.list.GetElement(k++);
											string element4 = param.list.GetElement(k++);
											string element5 = param.list.GetElement(k++);
											string element6 = param.list.GetElement(k++);
											if (!element3.IsNullOrEmpty())
											{
												int key3;
												if (Resources.StatusTagTable.TryGetValue(element3, out key3))
												{
													int s = (!int.TryParse(element4, out num2)) ? 0 : num2;
													int m = (!int.TryParse(element5, out num2)) ? 0 : num2;
													int l = (!int.TryParse(element6, out num2)) ? 0 : num2;
													parameterPacket.Parameters[key3] = new TriThreshold(s, m, l);
												}
											}
										}
										Dictionary<int, ParameterPacket> dictionary;
										if (!this.SituationStatusResultTable.TryGetValue(key, out dictionary))
										{
											Dictionary<int, ParameterPacket> dictionary2 = new Dictionary<int, ParameterPacket>();
											this.SituationStatusResultTable[key] = dictionary2;
											dictionary = dictionary2;
										}
										dictionary[key2] = parameterPacket;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003E99 RID: 16025 RVA: 0x001776A0 File Offset: 0x00175AA0
			private void LoadComCemra(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ComCamera, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ABInfoData abinfoData in AssetBundleManager.LoadAllAsset(text, typeof(ABInfoData), null).GetAllAssets<ABInfoData>())
						{
							foreach (ABInfoData.Param param in abinfoData.param)
							{
								this.ComCameraList[param.ID] = param;
							}
						}
					}
				}
			}

			// Token: 0x06003E9A RID: 16026 RVA: 0x001777AC File Offset: 0x00175BAC
			private void LoadByproductList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActionByproductList, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ActByproductData actByproductData in AssetBundleManager.LoadAllAsset(text, typeof(ActByproductData), null).GetAllAssets<ActByproductData>())
						{
							foreach (ActByproductData.Param param in actByproductData.param)
							{
								Dictionary<int, ByproductInfo> dictionary;
								if (!this.ByproductList.TryGetValue(param.ActionID, out dictionary))
								{
									Dictionary<int, ByproductInfo> dictionary2 = new Dictionary<int, ByproductInfo>();
									this.ByproductList[param.ActionID] = dictionary2;
									dictionary = dictionary2;
								}
								if (!param.ItemList.IsNullOrEmpty<string>())
								{
									List<List<int>> list = new List<List<int>>();
									foreach (string text2 in param.ItemList)
									{
										if (!text2.IsNullOrEmpty())
										{
											string[] array = text2.Split(Resources.ActionTable._splitChars, StringSplitOptions.RemoveEmptyEntries);
											List<int> list2 = new List<int>();
											foreach (string s in array)
											{
												int item;
												if (int.TryParse(s, out item))
												{
													list2.Add(item);
												}
											}
											list.Add(list2);
										}
									}
									dictionary[param.PoseID] = new ByproductInfo
									{
										ItemList = list
									};
								}
							}
						}
					}
				}
			}

			// Token: 0x04003B90 RID: 15248
			private static readonly string[] _separators = new string[]
			{
				"/",
				"／"
			};

			// Token: 0x04003B9F RID: 15263
			private static readonly int[] _bustIndexes = new int[]
			{
				2,
				3,
				4,
				5,
				6,
				7,
				8
			};

			// Token: 0x04003BA0 RID: 15264
			private static readonly char[] _splitChars = new char[]
			{
				','
			};
		}

		// Token: 0x020008F1 RID: 2289
		public class AnimalTables
		{
			// Token: 0x06003EB0 RID: 16048 RVA: 0x00177BEF File Offset: 0x00175FEF
			public void Load(AnimalDefinePack _animalDefinePack)
			{
				this.LoadInfo(_animalDefinePack);
				this.LoadAction(_animalDefinePack);
				this.LoadLook(_animalDefinePack);
				this.LoadState(_animalDefinePack);
				this.LoadDesire(_animalDefinePack);
				this.LoadWithActor(_animalDefinePack);
				this.LoadPlayerInfo(_animalDefinePack);
				this.LoadAnimalPoint(_animalDefinePack);
			}

			// Token: 0x06003EB1 RID: 16049 RVA: 0x00177C2C File Offset: 0x0017602C
			public void Release()
			{
				this.PetItemInfoTable.Clear();
				this.PetHomeUIInfoTable.Clear();
				this.ExpressionTable.Clear();
				this.TextureTable.Clear();
				this.CommonAnimeTable.Clear();
				this.WithAgentAnimeTable.Clear();
				this.AnimatorTable.Clear();
				this.TempAnimatorTable.Clear();
				this.ActionInfoTable.Clear();
				this.ModelInfoTable.Clear();
				this.DesirePriorityTable.Clear();
				this.DesireSpanTable.Clear();
				this.DesireBorderTable.Clear();
				this.DesireRateTable.Clear();
				this.DesireTargetStateTable.Clear();
				this.DesireResultTable.Clear();
				this.StateConditionTable.Clear();
				this.StateTargetActionTable.Clear();
				this.LookStateTable.Clear();
				this.PlayerCatchAnimalAnimationTable.Clear();
				this.PlayerCatchAnimalPoseTable.Clear();
				this.AnimalPointAssetTable.Clear();
				this.AnimalBaseObjInfoTable.Clear();
			}

			// Token: 0x17000B94 RID: 2964
			// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x00177D36 File Offset: 0x00176136
			// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x00177D3E File Offset: 0x0017613E
			public Dictionary<int, List<UnityEx.ValueTuple<ItemIDKeyPair, int>>> PetItemInfoTable { get; set; } = new Dictionary<int, List<UnityEx.ValueTuple<ItemIDKeyPair, int>>>();

			// Token: 0x17000B95 RID: 2965
			// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x00177D47 File Offset: 0x00176147
			// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x00177D4F File Offset: 0x0017614F
			public Dictionary<int, UnityEx.ValueTuple<int, List<string>>> PetHomeUIInfoTable { get; private set; } = new Dictionary<int, UnityEx.ValueTuple<int, List<string>>>();

			// Token: 0x17000B96 RID: 2966
			// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x00177D58 File Offset: 0x00176158
			// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x00177D60 File Offset: 0x00176160
			public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>>> ExpressionTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>>>();

			// Token: 0x17000B97 RID: 2967
			// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x00177D69 File Offset: 0x00176169
			// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x00177D71 File Offset: 0x00176171
			public Dictionary<int, Dictionary<int, UnityEx.ValueTuple<Texture2D, Color[]>>> TextureTable { get; private set; } = new Dictionary<int, Dictionary<int, UnityEx.ValueTuple<Texture2D, Color[]>>>();

			// Token: 0x17000B98 RID: 2968
			// (get) Token: 0x06003EBA RID: 16058 RVA: 0x00177D7A File Offset: 0x0017617A
			// (set) Token: 0x06003EBB RID: 16059 RVA: 0x00177D82 File Offset: 0x00176182
			public Dictionary<int, Dictionary<int, Dictionary<int, AnimalPlayState>>> CommonAnimeTable { get; set; } = new Dictionary<int, Dictionary<int, Dictionary<int, AnimalPlayState>>>();

			// Token: 0x17000B99 RID: 2969
			// (get) Token: 0x06003EBC RID: 16060 RVA: 0x00177D8B File Offset: 0x0017618B
			// (set) Token: 0x06003EBD RID: 16061 RVA: 0x00177D93 File Offset: 0x00176193
			public Dictionary<int, Dictionary<int, AnimalPlayState>> WithAgentAnimeTable { get; set; } = new Dictionary<int, Dictionary<int, AnimalPlayState>>();

			// Token: 0x17000B9A RID: 2970
			// (get) Token: 0x06003EBE RID: 16062 RVA: 0x00177D9C File Offset: 0x0017619C
			// (set) Token: 0x06003EBF RID: 16063 RVA: 0x00177DA4 File Offset: 0x001761A4
			public Dictionary<int, Dictionary<int, RuntimeAnimatorController>> AnimatorTable { get; set; } = new Dictionary<int, Dictionary<int, RuntimeAnimatorController>>();

			// Token: 0x17000B9B RID: 2971
			// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x00177DAD File Offset: 0x001761AD
			// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x00177DB5 File Offset: 0x001761B5
			protected Dictionary<string, Dictionary<string, RuntimeAnimatorController>> TempAnimatorTable { get; set; } = new Dictionary<string, Dictionary<string, RuntimeAnimatorController>>();

			// Token: 0x17000B9C RID: 2972
			// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x00177DBE File Offset: 0x001761BE
			// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x00177DC6 File Offset: 0x001761C6
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, AnimalActionInfo>>> ActionInfoTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, AnimalActionInfo>>>();

			// Token: 0x17000B9D RID: 2973
			// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x00177DCF File Offset: 0x001761CF
			// (set) Token: 0x06003EC5 RID: 16069 RVA: 0x00177DD7 File Offset: 0x001761D7
			public Dictionary<int, Dictionary<int, AnimalModelInfo>> ModelInfoTable { get; set; } = new Dictionary<int, Dictionary<int, AnimalModelInfo>>();

			// Token: 0x17000B9E RID: 2974
			// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x00177DE0 File Offset: 0x001761E0
			// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x00177DE8 File Offset: 0x001761E8
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<int, DesireType>>> DesirePriorityTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<int, DesireType>>>();

			// Token: 0x17000B9F RID: 2975
			// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x00177DF1 File Offset: 0x001761F1
			// (set) Token: 0x06003EC9 RID: 16073 RVA: 0x00177DF9 File Offset: 0x001761F9
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<DesireType, int>>> DesireSpanTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<DesireType, int>>>();

			// Token: 0x17000BA0 RID: 2976
			// (get) Token: 0x06003ECA RID: 16074 RVA: 0x00177E02 File Offset: 0x00176202
			// (set) Token: 0x06003ECB RID: 16075 RVA: 0x00177E0A File Offset: 0x0017620A
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<DesireType, System.Tuple<float, float>>>> DesireBorderTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<DesireType, System.Tuple<float, float>>>>();

			// Token: 0x17000BA1 RID: 2977
			// (get) Token: 0x06003ECC RID: 16076 RVA: 0x00177E13 File Offset: 0x00176213
			// (set) Token: 0x06003ECD RID: 16077 RVA: 0x00177E1B File Offset: 0x0017621B
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, Dictionary<DesireType, float>>>> DesireRateTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, Dictionary<DesireType, float>>>>();

			// Token: 0x17000BA2 RID: 2978
			// (get) Token: 0x06003ECE RID: 16078 RVA: 0x00177E24 File Offset: 0x00176224
			// (set) Token: 0x06003ECF RID: 16079 RVA: 0x00177E2C File Offset: 0x0017622C
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<DesireType, List<AnimalState>>>> DesireTargetStateTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<DesireType, List<AnimalState>>>>();

			// Token: 0x17000BA3 RID: 2979
			// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x00177E35 File Offset: 0x00176235
			// (set) Token: 0x06003ED1 RID: 16081 RVA: 0x00177E3D File Offset: 0x0017623D
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<DesireType, Dictionary<bool, Dictionary<DesireType, ChangeParamState>>>>> DesireResultTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<DesireType, Dictionary<bool, Dictionary<DesireType, ChangeParamState>>>>>();

			// Token: 0x17000BA4 RID: 2980
			// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x00177E46 File Offset: 0x00176246
			// (set) Token: 0x06003ED3 RID: 16083 RVA: 0x00177E4E File Offset: 0x0017624E
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, StateCondition>>> StateConditionTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, StateCondition>>>();

			// Token: 0x17000BA5 RID: 2981
			// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x00177E57 File Offset: 0x00176257
			// (set) Token: 0x06003ED5 RID: 16085 RVA: 0x00177E5F File Offset: 0x0017625F
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, List<ActionTypes>>>> StateTargetActionTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, List<ActionTypes>>>>();

			// Token: 0x17000BA6 RID: 2982
			// (get) Token: 0x06003ED6 RID: 16086 RVA: 0x00177E68 File Offset: 0x00176268
			// (set) Token: 0x06003ED7 RID: 16087 RVA: 0x00177E70 File Offset: 0x00176270
			public Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, LookState>>> LookStateTable { get; set; } = new Dictionary<AnimalTypes, Dictionary<BreedingTypes, Dictionary<AnimalState, LookState>>>();

			// Token: 0x17000BA7 RID: 2983
			// (get) Token: 0x06003ED8 RID: 16088 RVA: 0x00177E79 File Offset: 0x00176279
			// (set) Token: 0x06003ED9 RID: 16089 RVA: 0x00177E81 File Offset: 0x00176281
			public Dictionary<int, Dictionary<int, PlayState>> PlayerCatchAnimalAnimationTable { get; set; } = new Dictionary<int, Dictionary<int, PlayState>>();

			// Token: 0x17000BA8 RID: 2984
			// (get) Token: 0x06003EDA RID: 16090 RVA: 0x00177E8A File Offset: 0x0017628A
			// (set) Token: 0x06003EDB RID: 16091 RVA: 0x00177E92 File Offset: 0x00176292
			public Dictionary<int, Dictionary<int, PoseKeyPair>> PlayerCatchAnimalPoseTable { get; set; } = new Dictionary<int, Dictionary<int, PoseKeyPair>>();

			// Token: 0x17000BA9 RID: 2985
			// (get) Token: 0x06003EDC RID: 16092 RVA: 0x00177E9B File Offset: 0x0017629B
			// (set) Token: 0x06003EDD RID: 16093 RVA: 0x00177EA3 File Offset: 0x001762A3
			public Dictionary<int, Dictionary<int, AssetBundleInfo>> AnimalPointAssetTable { get; set; } = new Dictionary<int, Dictionary<int, AssetBundleInfo>>();

			// Token: 0x17000BAA RID: 2986
			// (get) Token: 0x06003EDE RID: 16094 RVA: 0x00177EAC File Offset: 0x001762AC
			// (set) Token: 0x06003EDF RID: 16095 RVA: 0x00177EB4 File Offset: 0x001762B4
			public Dictionary<int, Dictionary<int, AssetBundleInfo>> AnimalBaseObjInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, AssetBundleInfo>>();

			// Token: 0x06003EE0 RID: 16096 RVA: 0x00177EBD File Offset: 0x001762BD
			private string AssetStr(AssetBundleInfo _info)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}]", _info.assetbundle, _info.asset, _info.manifest);
			}

			// Token: 0x06003EE1 RID: 16097 RVA: 0x00177EDE File Offset: 0x001762DE
			private string AssetStrWithName(AssetBundleInfo _info)
			{
				return string.Format("Name[{0}] AssetBundleName[{1}] AssetName[{2}] ManifestName[{3}]", new object[]
				{
					_info.name,
					_info.assetbundle,
					_info.asset,
					_info.manifest
				});
			}

			// Token: 0x06003EE2 RID: 16098 RVA: 0x00177F18 File Offset: 0x00176318
			private RuntimeAnimatorController LoadAnimator(string _assetBundleName, string _assetName, string _manifestName = "")
			{
				RuntimeAnimatorController runtimeAnimatorController = null;
				if (!this.TempAnimatorTable.ContainsKey(_assetBundleName))
				{
					this.TempAnimatorTable[_assetBundleName] = new Dictionary<string, RuntimeAnimatorController>();
				}
				if (!this.TempAnimatorTable[_assetBundleName].TryGetValue(_assetName, out runtimeAnimatorController))
				{
					runtimeAnimatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(_assetBundleName, _assetName, _manifestName);
					this.TempAnimatorTable[_assetBundleName][_assetName] = runtimeAnimatorController;
				}
				return runtimeAnimatorController;
			}

			// Token: 0x06003EE3 RID: 16099 RVA: 0x00177F7F File Offset: 0x0017637F
			private RuntimeAnimatorController LoadAnimator(AssetBundleInfo _info)
			{
				return this.LoadAnimator(_info.assetbundle, _info.asset, _info.manifest);
			}

			// Token: 0x06003EE4 RID: 16100 RVA: 0x00177F9C File Offset: 0x0017639C
			private string GetDirectory(string _directory, string _animalName)
			{
				return _directory + _animalName + "/";
			}

			// Token: 0x06003EE5 RID: 16101 RVA: 0x00177FAA File Offset: 0x001763AA
			private string GetDirectory(string _directory, string _animalName, string _breedingName)
			{
				return string.Format("{0}{1}/{2}/", _directory, _animalName, _breedingName);
			}

			// Token: 0x06003EE6 RID: 16102 RVA: 0x00177FBC File Offset: 0x001763BC
			private AssetBundleInfo GetAssetInfo(List<string> _address, ref int _idx, bool _addSummary)
			{
				string name_ = (!_addSummary) ? string.Empty : (_address.GetElement(_idx++) ?? string.Empty);
				return new AssetBundleInfo(name_, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty);
			}

			// Token: 0x06003EE7 RID: 16103 RVA: 0x0017804C File Offset: 0x0017644C
			private void LoadInfo(AnimalDefinePack _animalDefinePack)
			{
				string animalInfoDirectory = _animalDefinePack.AssetBundleNames.AnimalInfoDirectory;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(animalInfoDirectory, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
											switch (num2)
											{
											case 0:
												this.LoadPetItemInfoList(assetInfo);
												break;
											case 1:
												this.LoadPetHomeUIInfoList(assetInfo);
												break;
											case 2:
												this.LoadAnimatorInfo(assetInfo);
												break;
											case 3:
												this.LoadAnimStateInfo(assetInfo);
												break;
											case 4:
												this.LoadModelInfo(assetInfo);
												break;
											case 5:
												this.LoadExpressionList(assetInfo);
												break;
											case 6:
												this.LoadAnimalTextureList(assetInfo);
												break;
											case 7:
												this.LoadAnimalBaseObjList(assetInfo);
												break;
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EE8 RID: 16104 RVA: 0x00178200 File Offset: 0x00176600
			private void LoadPetItemInfoList(AssetBundleInfo _excelAdress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAdress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
						this.LoadPetItemInfo(assetInfo);
					}
				}
			}

			// Token: 0x06003EE9 RID: 16105 RVA: 0x00178270 File Offset: 0x00176670
			private void LoadPetItemInfo(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int _categoryID;
							if (int.TryParse(s2, out _categoryID))
							{
								string s3 = list.GetElement(num++) ?? string.Empty;
								int _itemID;
								if (int.TryParse(s3, out _itemID))
								{
									string s4 = list.GetElement(num++) ?? string.Empty;
									int num2;
									if (int.TryParse(s4, out num2))
									{
										List<UnityEx.ValueTuple<ItemIDKeyPair, int>> list2;
										if (!this.PetItemInfoTable.TryGetValue(key, out list2) || list2 == null)
										{
											List<UnityEx.ValueTuple<ItemIDKeyPair, int>> list3 = new List<UnityEx.ValueTuple<ItemIDKeyPair, int>>();
											this.PetItemInfoTable[key] = list3;
											list2 = list3;
										}
										if (0 <= num2)
										{
											UnityEx.ValueTuple<ItemIDKeyPair, int> valueTuple = new UnityEx.ValueTuple<ItemIDKeyPair, int>(new ItemIDKeyPair
											{
												categoryID = _categoryID,
												itemID = _itemID
											}, num2);
											int index;
											if (0 <= (index = list2.FindIndex((UnityEx.ValueTuple<ItemIDKeyPair, int> x) => x.Item1.categoryID == _categoryID && x.Item1.itemID == _itemID)))
											{
												list2[index] = valueTuple;
											}
											else
											{
												list2.Add(valueTuple);
											}
										}
										else
										{
											list2.RemoveAll((UnityEx.ValueTuple<ItemIDKeyPair, int> x) => x.Item1.categoryID == _categoryID && x.Item1.itemID == _itemID);
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EEA RID: 16106 RVA: 0x00178450 File Offset: 0x00176850
			private void LoadPetHomeUIInfoList(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 1;
						string s = list.GetElement(j++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(j++) ?? string.Empty;
							int i2;
							if (int.TryParse(s2, out i2))
							{
								UnityEx.ValueTuple<int, List<string>> value;
								if (!this.PetHomeUIInfoTable.TryGetValue(key, out value))
								{
									value = new UnityEx.ValueTuple<int, List<string>>(i2, new List<string>());
								}
								else
								{
									value.Item2.Clear();
								}
								List<string> item = value.Item2;
								while (j < list.Count)
								{
									item.Add(list.GetElement(j++));
								}
								value.Item2 = item;
								this.PetHomeUIInfoTable[key] = value;
							}
						}
					}
				}
			}

			// Token: 0x06003EEB RID: 16107 RVA: 0x0017857C File Offset: 0x0017697C
			private void LoadAnimatorInfo(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 1;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, false);
								RuntimeAnimatorController runtimeAnimatorController = this.LoadAnimator(assetInfo);
								if (!(runtimeAnimatorController == null))
								{
									Dictionary<int, RuntimeAnimatorController> dictionary;
									if (!this.AnimatorTable.TryGetValue(key, out dictionary) || dictionary == null)
									{
										Dictionary<int, RuntimeAnimatorController> dictionary2 = new Dictionary<int, RuntimeAnimatorController>();
										this.AnimatorTable[key] = dictionary2;
										dictionary = dictionary2;
									}
									dictionary[key2] = runtimeAnimatorController;
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EEC RID: 16108 RVA: 0x00178698 File Offset: 0x00176A98
			private void LoadAnimStateInfo(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
						this.LoadAnimStateInfoElement(assetInfo);
					}
				}
			}

			// Token: 0x06003EED RID: 16109 RVA: 0x00178708 File Offset: 0x00176B08
			private void LoadAnimStateInfoElement(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 2;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string s3 = list.GetElement(num++) ?? string.Empty;
								int num2;
								if (int.TryParse(s3, out num2))
								{
									string text = list.GetElement(num++) ?? string.Empty;
									string text2 = list.GetElement(num++) ?? string.Empty;
									if (!text.IsNullOrEmpty() && !text2.IsNullOrEmpty())
									{
										string text3 = list.GetElement(num++) ?? string.Empty;
										string[] inStateNames = text3.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
										bool inFadeEnable;
										if (!bool.TryParse(list.GetElement(num++) ?? string.Empty, out inFadeEnable))
										{
											inFadeEnable = false;
										}
										float inFadeSecond;
										if (!float.TryParse(list.GetElement(num++) ?? string.Empty, out inFadeSecond))
										{
											inFadeSecond = 0f;
										}
										bool isLoop;
										if (!bool.TryParse(list.GetElement(num++) ?? string.Empty, out isLoop))
										{
											isLoop = false;
										}
										int loopMin;
										if (!int.TryParse(list.GetElement(num++) ?? string.Empty, out loopMin))
										{
											loopMin = 0;
										}
										int loopMax;
										if (!int.TryParse(list.GetElement(num++) ?? string.Empty, out loopMax))
										{
											loopMax = 0;
										}
										string text4 = list.GetElement(num++) ?? string.Empty;
										string[] outStateNames = text4.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
										bool outFadeEnable;
										if (!bool.TryParse(list.GetElement(num++) ?? string.Empty, out outFadeEnable))
										{
											outFadeEnable = false;
										}
										float outFadeSecond;
										if (!float.TryParse(list.GetElement(num++) ?? string.Empty, out outFadeSecond))
										{
											outFadeSecond = 0f;
										}
										int layer;
										if (!int.TryParse(list.GetElement(num++) ?? string.Empty, out layer))
										{
											layer = 0;
										}
										AnimalPlayState animalPlayState = new AnimalPlayState(layer, num2, inStateNames, outStateNames);
										AnimalPlayState.PlayStateInfo mainStateInfo = animalPlayState.MainStateInfo;
										mainStateInfo.AssetBundleInfo = new AssetBundleInfo(string.Empty, text, text2, string.Empty);
										mainStateInfo.InFadeEnable = inFadeEnable;
										mainStateInfo.InFadeSecond = inFadeSecond;
										mainStateInfo.OutFadeEnable = outFadeEnable;
										mainStateInfo.OutFadeSecond = outFadeSecond;
										mainStateInfo.IsLoop = isLoop;
										mainStateInfo.LoopMin = loopMin;
										mainStateInfo.LoopMax = loopMax;
										this.LoadAnimalAnimator(animalPlayState);
										Dictionary<int, Dictionary<int, AnimalPlayState>> dictionary;
										if (!this.CommonAnimeTable.TryGetValue(key, out dictionary))
										{
											dictionary = (this.CommonAnimeTable[key] = new Dictionary<int, Dictionary<int, AnimalPlayState>>());
										}
										Dictionary<int, AnimalPlayState> dictionary2;
										if (!dictionary.TryGetValue(key2, out dictionary2))
										{
											dictionary2 = (dictionary[key2] = new Dictionary<int, AnimalPlayState>());
										}
										dictionary2[num2] = animalPlayState;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EEE RID: 16110 RVA: 0x00178A90 File Offset: 0x00176E90
			private void LoadModelInfo(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 1;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, false);
								string value = list.GetElement(num++) ?? string.Empty;
								bool flag;
								if (!bool.TryParse(value, out flag))
								{
									flag = false;
								}
								string text = string.Empty;
								int num2 = -1;
								if (flag)
								{
									text = (list.GetElement(num++) ?? string.Empty);
									string s3 = list.GetElement(num++) ?? string.Empty;
									num2 = ((!int.TryParse(s3, out num2)) ? -1 : num2);
									flag = (!text.IsNullOrEmpty() && 0 <= num2);
								}
								else
								{
									num += 2;
								}
								string value2 = list.GetElement(num++) ?? string.Empty;
								bool flag2;
								if (!bool.TryParse(value2, out flag2))
								{
									flag2 = false;
								}
								string text2 = string.Empty;
								int num3 = -1;
								if (flag2)
								{
									text2 = (list.GetElement(num++) ?? string.Empty);
									string s4 = list.GetElement(num++) ?? string.Empty;
									num3 = ((!int.TryParse(s4, out num3)) ? -1 : num3);
									flag2 = (!text2.IsNullOrEmpty() && 0 <= num3);
								}
								else
								{
									num += 2;
								}
								AnimalShapeInfo eyesShapeInfo = new AnimalShapeInfo(flag, text, num2);
								AnimalShapeInfo mouthShapeInfo = new AnimalShapeInfo(flag2, text2, num3);
								AnimalModelInfo value3 = new AnimalModelInfo(assetInfo, eyesShapeInfo, mouthShapeInfo);
								Dictionary<int, AnimalModelInfo> dictionary;
								if (!this.ModelInfoTable.TryGetValue(key, out dictionary))
								{
									Dictionary<int, AnimalModelInfo> dictionary2 = new Dictionary<int, AnimalModelInfo>();
									this.ModelInfoTable[key] = dictionary2;
									dictionary = dictionary2;
								}
								dictionary[key2] = value3;
							}
						}
					}
				}
			}

			// Token: 0x06003EEF RID: 16111 RVA: 0x00178D00 File Offset: 0x00177100
			private void LoadExpressionList(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
						this.LoadExpressionTable(assetInfo);
					}
				}
			}

			// Token: 0x06003EF0 RID: 16112 RVA: 0x00178D70 File Offset: 0x00177170
			private void LoadExpressionTable(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 2;
						string s = list.GetElement(j++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(j++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string s3 = list.GetElement(j++) ?? string.Empty;
								int key3;
								if (int.TryParse(s3, out key3))
								{
									string element = list.GetElement(j++);
									if (!element.IsNullOrEmpty())
									{
										Dictionary<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>> dictionary;
										if (!this.ExpressionTable.TryGetValue(key, out dictionary) || dictionary == null)
										{
											dictionary = (this.ExpressionTable[key] = new Dictionary<int, Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>>());
										}
										Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>> dictionary2;
										if (!dictionary.TryGetValue(key2, out dictionary2) || dictionary2 == null)
										{
											dictionary2 = (dictionary[key2] = new Dictionary<int, Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>>());
										}
										Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>> dictionary3;
										if (!dictionary2.TryGetValue(key3, out dictionary3) || dictionary3 == null)
										{
											dictionary3 = (dictionary2[key3] = new Dictionary<string, List<UnityEx.ValueTuple<string, int, int>>>());
										}
										List<UnityEx.ValueTuple<string, int, int>> list2;
										if (!dictionary3.TryGetValue(element, out list2) || list2 == null)
										{
											list2 = (dictionary3[element] = new List<UnityEx.ValueTuple<string, int, int>>());
										}
										else
										{
											list2.Clear();
										}
										while (j < list.Count)
										{
											string element2 = list.GetElement(j++);
											string s4 = list.GetElement(j++) ?? string.Empty;
											string s5 = list.GetElement(j++) ?? string.Empty;
											int i2;
											int i3;
											if (!element2.IsNullOrEmpty() && int.TryParse(s4, out i2) && int.TryParse(s5, out i3))
											{
												list2.Add(new UnityEx.ValueTuple<string, int, int>(element2, i2, i3));
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EF1 RID: 16113 RVA: 0x00178FAC File Offset: 0x001773AC
			private void LoadAnimalTextureList(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(j++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string text = list.GetElement(j++) ?? string.Empty;
								string text2 = list.GetElement(j++) ?? string.Empty;
								string text3 = list.GetElement(j++) ?? string.Empty;
								List<Color> list2 = ListPool<Color>.Get();
								while (j < list.Count)
								{
									string s3 = list.GetElement(j++) ?? string.Empty;
									float num;
									if (!float.TryParse(s3, out num))
									{
										num = 0f;
									}
									string s4 = list.GetElement(j++) ?? string.Empty;
									float num2;
									if (!float.TryParse(s4, out num2))
									{
										num2 = 0f;
									}
									string s5 = list.GetElement(j++) ?? string.Empty;
									float num3;
									if (!float.TryParse(s5, out num3))
									{
										num3 = 0f;
									}
									list2.Add(new Color(num / 255f, num2 / 255f, num3 / 255f, 1f));
								}
								for (int k = 0; k < 4; k++)
								{
									string s6 = list.GetElement(j++) ?? string.Empty;
									float num4;
									if (!float.TryParse(s6, out num4))
									{
										num4 = 0f;
									}
									string s7 = list.GetElement(j++) ?? string.Empty;
									float num5;
									if (!float.TryParse(s7, out num5))
									{
										num5 = 0f;
									}
									string s8 = list.GetElement(j++) ?? string.Empty;
									float num6;
									if (!float.TryParse(s8, out num6))
									{
										num6 = 0f;
									}
									list2.Add(new Color(num4 / 255f, num5 / 255f, num6 / 255f, 1f));
								}
								string assetBundleName = text;
								string assetName = text2;
								string manifestName = (!text3.IsNullOrEmpty()) ? text3 : null;
								Texture2D texture2D = CommonLib.LoadAsset<Texture2D>(assetBundleName, assetName, false, manifestName);
								if (texture2D != null)
								{
									Singleton<Resources>.Instance.AddLoadAssetBundle(text, text3);
								}
								Color[] array = new Color[list2.Count];
								for (int l = 0; l < array.Length; l++)
								{
									array[l] = list2[l];
								}
								ListPool<Color>.Release(list2);
								UnityEx.ValueTuple<Texture2D, Color[]> value = new UnityEx.ValueTuple<Texture2D, Color[]>(texture2D, array);
								Dictionary<int, UnityEx.ValueTuple<Texture2D, Color[]>> dictionary;
								if (!this.TextureTable.TryGetValue(key, out dictionary) || dictionary == null)
								{
									Dictionary<int, UnityEx.ValueTuple<Texture2D, Color[]>> dictionary2 = new Dictionary<int, UnityEx.ValueTuple<Texture2D, Color[]>>();
									this.TextureTable[key] = dictionary2;
									dictionary = dictionary2;
								}
								dictionary[key2] = value;
							}
						}
					}
				}
			}

			// Token: 0x06003EF2 RID: 16114 RVA: 0x00179318 File Offset: 0x00177718
			private void LoadAnimalBaseObjList(AssetBundleInfo _excelAddress)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_excelAddress);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 1;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string element = list.GetElement(num++);
								string element2 = list.GetElement(num++);
								string element3 = list.GetElement(num++);
								if (!element.IsNullOrEmpty() && !element2.IsNullOrEmpty())
								{
									Dictionary<int, AssetBundleInfo> dictionary;
									if (!this.AnimalBaseObjInfoTable.TryGetValue(key, out dictionary) || dictionary == null)
									{
										Dictionary<int, AssetBundleInfo> dictionary2 = new Dictionary<int, AssetBundleInfo>();
										this.AnimalBaseObjInfoTable[key] = dictionary2;
										dictionary = dictionary2;
									}
									dictionary[key2] = new AssetBundleInfo(string.Empty, element, element2, element3);
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EF3 RID: 16115 RVA: 0x00179460 File Offset: 0x00177860
			private void LoadAction(AnimalDefinePack _animalDefinePack)
			{
				string actionInfoListBundleDirectory = _animalDefinePack.AssetBundleNames.ActionInfoListBundleDirectory;
				IReadOnlyList<UnityEx.ValueTuple<string, AnimalTypes>> animalNameList = AIProject.Animal.AnimalData.AnimalNameList;
				for (int i = 0; i < animalNameList.Count; i++)
				{
					string item = animalNameList[i].Item1;
					AnimalTypes item2 = animalNameList[i].Item2;
					IReadOnlyList<UnityEx.ValueTuple<string, BreedingTypes>> breedingNameList = AIProject.Animal.AnimalData.BreedingNameList;
					for (int j = 0; j < breedingNameList.Count; j++)
					{
						string item3 = breedingNameList[j].Item1;
						BreedingTypes item4 = breedingNameList[j].Item2;
						string directory = this.GetDirectory(actionInfoListBundleDirectory, item, item3);
						List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(directory, false);
						if (!assetBundleNameListFromPath.IsNullOrEmpty<string>())
						{
							assetBundleNameListFromPath.Sort();
							for (int k = 0; k < assetBundleNameListFromPath.Count; k++)
							{
								string text = assetBundleNameListFromPath[k];
								if (!Game.IsRestrictedOver50(text))
								{
									string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
									if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
									{
										ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
										if (!(excelData == null))
										{
											for (int l = 1; l < excelData.MaxCell; l++)
											{
												List<string> list = excelData.list[l].list;
												if (!list.IsNullOrEmpty<string>())
												{
													int num = 0;
													string s = list.GetElement(num++) ?? string.Empty;
													int num2;
													if (int.TryParse(s, out num2))
													{
														num++;
														AssetBundleInfo sheetABInfo = new AssetBundleInfo(string.Empty, list.GetElement(num++), list.GetElement(num++), list.GetElement(num++));
														if (!sheetABInfo.assetbundle.IsNullOrEmpty() && !sheetABInfo.asset.IsNullOrEmpty())
														{
															if (num2 == 0)
															{
																this.LoadActionEndInfo(item2, item4, sheetABInfo);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EF4 RID: 16116 RVA: 0x0017969C File Offset: 0x00177A9C
			private void LoadActionEndInfo(AnimalTypes _animalType, BreedingTypes _breedingType, AssetBundleInfo _sheetABInfo)
			{
				if (_sheetABInfo.assetbundle.IsNullOrEmpty() || _sheetABInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetABInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					int num = 0;
					int key = 0;
					int min = 0;
					int max = 0;
					AnimalState key2 = AnimalState.None;
					bool manageTimeEnable = false;
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						string s = list.GetElement(num++) ?? string.Empty;
						string text = list.GetElement(num++) ?? string.Empty;
						num++;
						string value = list.GetElement(num++) ?? string.Empty;
						string s2 = list.GetElement(num++) ?? string.Empty;
						string s3 = list.GetElement(num++) ?? string.Empty;
						if (int.TryParse(s, out key) && AIProject.Animal.AnimalData.AnimalStateIDTable.TryGetValue(key, out key2) && bool.TryParse(value, out manageTimeEnable) && int.TryParse(s2, out min) && int.TryParse(s3, out max))
						{
							if (!this.ActionInfoTable.ContainsKey(_animalType))
							{
								this.ActionInfoTable[_animalType] = new Dictionary<BreedingTypes, Dictionary<AnimalState, AnimalActionInfo>>();
							}
							if (!this.ActionInfoTable[_animalType].ContainsKey(_breedingType))
							{
								this.ActionInfoTable[_animalType][_breedingType] = new Dictionary<AnimalState, AnimalActionInfo>();
							}
							AnimalActionInfo animalActionInfo;
							if (!this.ActionInfoTable[_animalType][_breedingType].TryGetValue(key2, out animalActionInfo))
							{
								animalActionInfo = new AnimalActionInfo();
							}
							animalActionInfo.timeInfo = new AnimalActionInfo.TimeInfo(manageTimeEnable, min, max);
							this.ActionInfoTable[_animalType][_breedingType][key2] = animalActionInfo;
						}
					}
				}
			}

			// Token: 0x06003EF5 RID: 16117 RVA: 0x001798A4 File Offset: 0x00177CA4
			private void LoadAnimalAnimator(AnimalPlayState _playState)
			{
				if (_playState == null)
				{
					return;
				}
				AssetBundleInfo assetBundleInfo = _playState.MainStateInfo.AssetBundleInfo;
				assetBundleInfo.manifest = "abdata";
				_playState.MainStateInfo.Controller = this.LoadAnimator(assetBundleInfo);
				if (_playState.SubStateInfos.IsNullOrEmpty<AnimalPlayState.PlayStateInfo>())
				{
					return;
				}
				foreach (AnimalPlayState.PlayStateInfo playStateInfo in _playState.SubStateInfos)
				{
					AssetBundleInfo assetBundleInfo2 = playStateInfo.AssetBundleInfo;
					assetBundleInfo2.manifest = "abdata";
					playStateInfo.Controller = this.LoadAnimator(assetBundleInfo2);
				}
			}

			// Token: 0x06003EF6 RID: 16118 RVA: 0x0017995C File Offset: 0x00177D5C
			private void LoadLook(AnimalDefinePack _animalDefinePack)
			{
				string lookInfoListBundleDirectory = _animalDefinePack.AssetBundleNames.LookInfoListBundleDirectory;
				IReadOnlyList<UnityEx.ValueTuple<string, AnimalTypes>> animalNameList = AIProject.Animal.AnimalData.AnimalNameList;
				for (int i = 0; i < animalNameList.Count; i++)
				{
					string item = animalNameList[i].Item1;
					AnimalTypes item2 = animalNameList[i].Item2;
					string directory = this.GetDirectory(lookInfoListBundleDirectory, item);
					List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(directory, false);
					if (!assetBundleNameListFromPath.IsNullOrEmpty<string>())
					{
						assetBundleNameListFromPath.Sort();
						for (int j = 0; j < assetBundleNameListFromPath.Count; j++)
						{
							string text = assetBundleNameListFromPath[j];
							if (!Game.IsRestrictedOver50(text))
							{
								string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
								if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
								{
									ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
									if (!(excelData == null))
									{
										for (int k = 1; k < excelData.MaxCell; k++)
										{
											List<string> list = excelData.list[k].list;
											if (!list.IsNullOrEmpty<string>())
											{
												int num = 0;
												int key = 0;
												AnimalState key2 = AnimalState.None;
												string s = list.GetElement(num++) ?? string.Empty;
												string text2 = list.GetElement(num++) ?? string.Empty;
												string s2 = list.GetElement(num++) ?? string.Empty;
												string value = list.GetElement(num++) ?? string.Empty;
												string s3 = list.GetElement(num++) ?? string.Empty;
												string value2 = list.GetElement(num++) ?? string.Empty;
												if (int.TryParse(s, out key) && AIProject.Animal.AnimalData.AnimalStateIDTable.TryGetValue(key, out key2))
												{
													int ptnNo = 0;
													if (int.TryParse(s2, out ptnNo))
													{
														bool flag = false;
														flag = (bool.TryParse(value, out flag) && flag);
														if (!this.LookStateTable.ContainsKey(item2))
														{
															this.LookStateTable[item2] = new Dictionary<BreedingTypes, Dictionary<AnimalState, LookState>>();
														}
														if (!this.LookStateTable[item2].ContainsKey(BreedingTypes.Wild))
														{
															this.LookStateTable[item2][BreedingTypes.Wild] = new Dictionary<AnimalState, LookState>();
														}
														this.LookStateTable[item2][BreedingTypes.Wild][key2] = new LookState(ptnNo, flag);
													}
													int ptnNo2 = 0;
													if (int.TryParse(s3, out ptnNo2))
													{
														bool flag2 = false;
														flag2 = (bool.TryParse(value2, out flag2) && flag2);
														if (!this.LookStateTable.ContainsKey(item2))
														{
															this.LookStateTable[item2] = new Dictionary<BreedingTypes, Dictionary<AnimalState, LookState>>();
														}
														if (!this.LookStateTable[item2].ContainsKey(BreedingTypes.Pet))
														{
															this.LookStateTable[item2][BreedingTypes.Pet] = new Dictionary<AnimalState, LookState>();
														}
														this.LookStateTable[item2][BreedingTypes.Pet][key2] = new LookState(ptnNo2, flag2);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EF7 RID: 16119 RVA: 0x00179CB8 File Offset: 0x001780B8
			private void LoadState(AnimalDefinePack _animalDefinePack)
			{
				string stateInfoListBundleDirectory = _animalDefinePack.AssetBundleNames.StateInfoListBundleDirectory;
				IReadOnlyList<UnityEx.ValueTuple<string, AnimalTypes>> animalNameList = AIProject.Animal.AnimalData.AnimalNameList;
				for (int i = 0; i < animalNameList.Count; i++)
				{
					string item = animalNameList[i].Item1;
					AnimalTypes item2 = animalNameList[i].Item2;
					IReadOnlyList<UnityEx.ValueTuple<string, BreedingTypes>> breedingNameList = AIProject.Animal.AnimalData.BreedingNameList;
					for (int j = 0; j < breedingNameList.Count; j++)
					{
						string item3 = breedingNameList[j].Item1;
						BreedingTypes item4 = breedingNameList[j].Item2;
						string directory = this.GetDirectory(stateInfoListBundleDirectory, item, item3);
						List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(directory, false);
						assetBundleNameListFromPath.Sort();
						if (!assetBundleNameListFromPath.IsNullOrEmpty<string>())
						{
							for (int k = 0; k < assetBundleNameListFromPath.Count; k++)
							{
								string text = assetBundleNameListFromPath[k];
								if (!Game.IsRestrictedOver50(text))
								{
									string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
									if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
									{
										ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
										if (!(excelData == null))
										{
											for (int l = 1; l < excelData.MaxCell; l++)
											{
												List<string> list = excelData.list[l].list;
												if (!list.IsNullOrEmpty<string>())
												{
													int num = 0;
													string s = list.GetElement(num++) ?? string.Empty;
													int num2;
													if (int.TryParse(s, out num2))
													{
														num++;
														AssetBundleInfo sheetABInfo = new AssetBundleInfo(string.Empty, list.GetElement(num++), list.GetElement(num++), list.GetElement(num));
														if (!sheetABInfo.assetbundle.IsNullOrEmpty() && !sheetABInfo.asset.IsNullOrEmpty())
														{
															if (num2 != 0)
															{
																if (num2 == 1)
																{
																	this.LoadStateTargetActionType(item2, item4, sheetABInfo);
																}
															}
															else
															{
																this.LoadChangeState(item2, item4, sheetABInfo);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EF8 RID: 16120 RVA: 0x00179F08 File Offset: 0x00178308
			private void LoadChangeState(AnimalTypes _animalType, BreedingTypes _breedingType, AssetBundleInfo _sheetABInfo)
			{
				if (_sheetABInfo.assetbundle.IsNullOrEmpty() || _sheetABInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetABInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int dx = 0;
						int key = 0;
						AnimalState animalState = AnimalState.None;
						ConditionType conditionType = ConditionType.None;
						string s = list.GetElement(dx++) ?? string.Empty;
						string text = list.GetElement(dx++) ?? string.Empty;
						string name = list.GetElement(dx++) ?? string.Empty;
						if (int.TryParse(s, out key) && AIProject.Animal.AnimalData.AnimalStateIDTable.TryGetValue(key, out animalState) && this.TryStringToEnum<ConditionType>(name, out conditionType))
						{
							this.SetConditionState(_animalType, _breedingType, list, dx, animalState, conditionType);
						}
					}
				}
			}

			// Token: 0x06003EF9 RID: 16121 RVA: 0x0017A020 File Offset: 0x00178420
			private void SetConditionState(AnimalTypes _animalType, BreedingTypes _breedingType, List<string> _row, int _dx, AnimalState _animalState, ConditionType _conditionType)
			{
				StateCondition stateCondition = new StateCondition(_conditionType, _animalState);
				switch (_conditionType)
				{
				case ConditionType.Forced:
				{
					AnimalState state;
					if (!this.TryStringToEnum<AnimalState>(_row.GetElement(_dx++) ?? string.Empty, out state))
					{
						return;
					}
					stateCondition.AddNextState(state, 0f);
					break;
				}
				case ConditionType.Proportion:
					while (_dx < _row.Count)
					{
						AnimalState state2 = AnimalState.None;
						float proportion = 0f;
						string name = _row.GetElement(_dx++) ?? string.Empty;
						string s = _row.GetElement(_dx++) ?? string.Empty;
						if (!this.TryStringToEnum<AnimalState>(name, out state2) || !float.TryParse(s, out proportion))
						{
							break;
						}
						stateCondition.AddNextState(state2, proportion);
					}
					break;
				case ConditionType.Random:
				case ConditionType.NonOverlap:
					while (_dx < _row.Count)
					{
						AnimalState state3 = AnimalState.None;
						string name2 = _row.GetElement(_dx++) ?? string.Empty;
						if (this.TryStringToEnum<AnimalState>(name2, out state3))
						{
							stateCondition.AddNextState(state3, 0f);
						}
					}
					break;
				default:
					return;
				}
				if (stateCondition.Count <= 0)
				{
					return;
				}
				if (!this.StateConditionTable.ContainsKey(_animalType))
				{
					this.StateConditionTable[_animalType] = new Dictionary<BreedingTypes, Dictionary<AnimalState, StateCondition>>();
				}
				if (!this.StateConditionTable[_animalType].ContainsKey(_breedingType))
				{
					this.StateConditionTable[_animalType][_breedingType] = new Dictionary<AnimalState, StateCondition>();
				}
				this.StateConditionTable[_animalType][_breedingType][_animalState] = stateCondition;
			}

			// Token: 0x06003EFA RID: 16122 RVA: 0x0017A1DC File Offset: 0x001785DC
			private void LoadStateTargetActionType(AnimalTypes _animalType, BreedingTypes _breedingType, AssetBundleInfo _sheetABInfo)
			{
				if (_sheetABInfo.assetbundle.IsNullOrEmpty() || _sheetABInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetABInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						int key = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						num++;
						string text = list.GetElement(num++) ?? string.Empty;
						string[] array = text.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
						AnimalState key2;
						if (int.TryParse(s, out key) && AIProject.Animal.AnimalData.AnimalStateIDTable.TryGetValue(key, out key2))
						{
							List<ActionTypes> list2 = new List<ActionTypes>();
							if (!array.IsNullOrEmpty<string>())
							{
								foreach (string s2 in array)
								{
									int key3;
									ActionTypes item;
									if (int.TryParse(s2, out key3) && this.AnimalActionIDTable.TryGetValue(key3, out item))
									{
										list2.Add(item);
									}
								}
							}
							if (!this.StateTargetActionTable.ContainsKey(_animalType))
							{
								this.StateTargetActionTable[_animalType] = new Dictionary<BreedingTypes, Dictionary<AnimalState, List<ActionTypes>>>();
							}
							if (!this.StateTargetActionTable[_animalType].ContainsKey(_breedingType))
							{
								this.StateTargetActionTable[_animalType][_breedingType] = new Dictionary<AnimalState, List<ActionTypes>>();
							}
							this.StateTargetActionTable[_animalType][_breedingType][key2] = list2;
						}
					}
				}
			}

			// Token: 0x06003EFB RID: 16123 RVA: 0x0017A398 File Offset: 0x00178798
			private void LoadDesire(AnimalDefinePack _animalDefinePack)
			{
				string desireInfoListBundleDirectory = _animalDefinePack.AssetBundleNames.DesireInfoListBundleDirectory;
				IReadOnlyList<UnityEx.ValueTuple<string, AnimalTypes>> animalNameList = AIProject.Animal.AnimalData.AnimalNameList;
				for (int i = 0; i < animalNameList.Count; i++)
				{
					string item = animalNameList[i].Item1;
					AnimalTypes item2 = animalNameList[i].Item2;
					IReadOnlyList<UnityEx.ValueTuple<string, BreedingTypes>> breedingNameList = AIProject.Animal.AnimalData.BreedingNameList;
					for (int j = 0; j < breedingNameList.Count; j++)
					{
						string item3 = breedingNameList[j].Item1;
						BreedingTypes item4 = breedingNameList[j].Item2;
						string directory = this.GetDirectory(desireInfoListBundleDirectory, item, item3);
						List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(directory, false);
						if (!assetBundleNameListFromPath.IsNullOrEmpty<string>())
						{
							assetBundleNameListFromPath.Sort();
							for (int k = 0; k < assetBundleNameListFromPath.Count; k++)
							{
								string text = assetBundleNameListFromPath[k];
								if (!Game.IsRestrictedOver50(text))
								{
									string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
									if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
									{
										ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
										if (!(excelData == null))
										{
											for (int l = 1; l < excelData.MaxCell; l++)
											{
												List<string> list = excelData.list[l].list;
												if (!list.IsNullOrEmpty<string>())
												{
													int num = 0;
													string s = list.GetElement(num++) ?? string.Empty;
													int num2;
													if (int.TryParse(s, out num2))
													{
														num++;
														AssetBundleInfo sheetInfo = new AssetBundleInfo(string.Empty, list.GetElement(num++), list.GetElement(num++), list.GetElement(num++));
														if (!sheetInfo.assetbundle.IsNullOrEmpty() && !sheetInfo.asset.IsNullOrEmpty())
														{
															switch (num2)
															{
															case 0:
																this.LoadDesirePriority(item2, item4, sheetInfo);
																break;
															case 1:
																this.LoadDesireSpan(item2, item4, sheetInfo);
																break;
															case 2:
																this.LoadDesireBorder(item2, item4, sheetInfo);
																break;
															case 3:
																this.LoadDesireRate(item2, item4, sheetInfo);
																break;
															case 4:
																this.LoadDesireTargetState(item2, item4, sheetInfo);
																break;
															case 5:
																this.LoadDesireResult(item2, item4, sheetInfo);
																break;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003EFC RID: 16124 RVA: 0x0017A644 File Offset: 0x00178A44
			private void LoadDesirePriority(AnimalTypes _animalType, BreedingTypes _breedingType, AssetBundleInfo _sheetInfo)
			{
				if (_sheetInfo.assetbundle.IsNullOrEmpty() || _sheetInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						int key = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						string s2 = list.GetElement(num++) ?? string.Empty;
						int key2;
						if (int.TryParse(s, out key) && int.TryParse(s2, out key2))
						{
							DesireType value;
							if (this.AnimalDesireIDTable.TryGetValue(key2, out value))
							{
								if (!this.DesirePriorityTable.ContainsKey(_animalType))
								{
									this.DesirePriorityTable[_animalType] = new Dictionary<BreedingTypes, Dictionary<int, DesireType>>();
								}
								if (!this.DesirePriorityTable[_animalType].ContainsKey(_breedingType))
								{
									this.DesirePriorityTable[_animalType][_breedingType] = new Dictionary<int, DesireType>();
								}
								this.DesirePriorityTable[_animalType][_breedingType][key] = value;
							}
						}
					}
				}
			}

			// Token: 0x06003EFD RID: 16125 RVA: 0x0017A7A0 File Offset: 0x00178BA0
			private void LoadDesireSpan(AnimalTypes _animalType, BreedingTypes _breedingType, AssetBundleInfo _sheetInfo)
			{
				if (_sheetInfo.assetbundle.IsNullOrEmpty() || _sheetInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						int key = 0;
						int value = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						num++;
						string s2 = list.GetElement(num++) ?? string.Empty;
						if (int.TryParse(s, out key) && int.TryParse(s2, out value))
						{
							DesireType key2;
							if (this.AnimalDesireIDTable.TryGetValue(key, out key2))
							{
								if (!this.DesireSpanTable.ContainsKey(_animalType))
								{
									this.DesireSpanTable[_animalType] = new Dictionary<BreedingTypes, Dictionary<DesireType, int>>();
								}
								if (!this.DesireSpanTable[_animalType].ContainsKey(_breedingType))
								{
									this.DesireSpanTable[_animalType][_breedingType] = new Dictionary<DesireType, int>();
								}
								this.DesireSpanTable[_animalType][_breedingType][key2] = value;
							}
						}
					}
				}
			}

			// Token: 0x06003EFE RID: 16126 RVA: 0x0017A900 File Offset: 0x00178D00
			private void LoadDesireBorder(AnimalTypes _animalType, BreedingTypes _breedingType, AssetBundleInfo _sheetInfo)
			{
				if (_sheetInfo.assetbundle.IsNullOrEmpty() || _sheetInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						float item = 0f;
						float item2 = 0f;
						string s = list.GetElement(num++) ?? string.Empty;
						num++;
						string s2 = list.GetElement(num++) ?? string.Empty;
						string s3 = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key) && float.TryParse(s2, out item) && float.TryParse(s3, out item2))
						{
							DesireType key2;
							if (this.AnimalDesireIDTable.TryGetValue(key, out key2))
							{
								if (!this.DesireBorderTable.ContainsKey(_animalType))
								{
									this.DesireBorderTable[_animalType] = new Dictionary<BreedingTypes, Dictionary<DesireType, System.Tuple<float, float>>>();
								}
								if (!this.DesireBorderTable[_animalType].ContainsKey(_breedingType))
								{
									this.DesireBorderTable[_animalType][_breedingType] = new Dictionary<DesireType, System.Tuple<float, float>>();
								}
								this.DesireBorderTable[_animalType][_breedingType][key2] = new System.Tuple<float, float>(item, item2);
							}
						}
					}
				}
			}

			// Token: 0x06003EFF RID: 16127 RVA: 0x0017AA98 File Offset: 0x00178E98
			private void LoadDesireRate(AnimalTypes _animalType, BreedingTypes _breedingType, AssetBundleInfo _sheetInfo)
			{
				if (_sheetInfo.assetbundle.IsNullOrEmpty() || _sheetInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						int key = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						AnimalState key2;
						if (int.TryParse(s, out key) && AIProject.Animal.AnimalData.AnimalStateIDTable.TryGetValue(key, out key2))
						{
							j++;
							int num = 0;
							while (j < list.Count)
							{
								DesireType key3;
								if (this.AnimalDesireIDTable.TryGetValue(num, out key3))
								{
									float value = 0f;
									string s2 = list.GetElement(j) ?? string.Empty;
									if (float.TryParse(s2, out value))
									{
										if (!this.DesireRateTable.ContainsKey(_animalType))
										{
											this.DesireRateTable[_animalType] = new Dictionary<BreedingTypes, Dictionary<AnimalState, Dictionary<DesireType, float>>>();
										}
										if (!this.DesireRateTable[_animalType].ContainsKey(_breedingType))
										{
											this.DesireRateTable[_animalType][_breedingType] = new Dictionary<AnimalState, Dictionary<DesireType, float>>();
										}
										if (!this.DesireRateTable[_animalType][_breedingType].ContainsKey(key2))
										{
											this.DesireRateTable[_animalType][_breedingType][key2] = new Dictionary<DesireType, float>();
										}
										this.DesireRateTable[_animalType][_breedingType][key2][key3] = value;
									}
								}
								j++;
								num++;
							}
						}
					}
				}
			}

			// Token: 0x06003F00 RID: 16128 RVA: 0x0017AC74 File Offset: 0x00179074
			private void LoadDesireTargetState(AnimalTypes _animalType, BreedingTypes _breedingType, AssetBundleInfo _sheetInfo)
			{
				if (_sheetInfo.assetbundle.IsNullOrEmpty() || _sheetInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						int key = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						num++;
						string text = list.GetElement(num++) ?? string.Empty;
						string[] array = text.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
						DesireType key2;
						if (int.TryParse(s, out key) && this.AnimalDesireIDTable.TryGetValue(key, out key2))
						{
							List<AnimalState> list2 = new List<AnimalState>();
							if (!array.IsNullOrEmpty<string>())
							{
								for (int j = 0; j < array.Length; j++)
								{
									string s2 = array[j] ?? string.Empty;
									int key3;
									AnimalState item;
									if (int.TryParse(s2, out key3) && AIProject.Animal.AnimalData.AnimalStateIDTable.TryGetValue(key3, out item))
									{
										list2.Add(item);
									}
								}
							}
							if (!this.DesireTargetStateTable.ContainsKey(_animalType))
							{
								this.DesireTargetStateTable[_animalType] = new Dictionary<BreedingTypes, Dictionary<DesireType, List<AnimalState>>>();
							}
							if (!this.DesireTargetStateTable[_animalType].ContainsKey(_breedingType))
							{
								this.DesireTargetStateTable[_animalType][_breedingType] = new Dictionary<DesireType, List<AnimalState>>();
							}
							this.DesireTargetStateTable[_animalType][_breedingType][key2] = list2;
						}
					}
				}
			}

			// Token: 0x06003F01 RID: 16129 RVA: 0x0017AE3C File Offset: 0x0017923C
			private void LoadDesireResult(AnimalTypes _animalType, BreedingTypes _breedingType, AssetBundleInfo _sheetInfo)
			{
				if (_sheetInfo.assetbundle.IsNullOrEmpty() || _sheetInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				DesireType key = DesireType.None;
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						int key2 = 0;
						int key3 = 0;
						int key4 = 0;
						float num2 = 0f;
						float num3 = 0f;
						bool key5 = true;
						string s = list.GetElement(num++) ?? string.Empty;
						num++;
						DesireType desireType;
						if (int.TryParse(s, out key2) && this.AnimalDesireIDTable.TryGetValue(key2, out desireType))
						{
							key = desireType;
						}
						string value = list.GetElement(num++) ?? string.Empty;
						string s2 = list.GetElement(num++) ?? string.Empty;
						string s3 = list.GetElement(num++) ?? string.Empty;
						string s4 = list.GetElement(num++) ?? string.Empty;
						string s5 = list.GetElement(num++) ?? string.Empty;
						DesireType key6;
						ChangeType changeType;
						if (int.TryParse(s2, out key3) && int.TryParse(s3, out key4) && this.AnimalDesireIDTable.TryGetValue(key3, out key6) && this.ChangeTypeIDTable.TryGetValue(key4, out changeType))
						{
							if (bool.TryParse(value, out key5) && float.TryParse(s4, out num2))
							{
								num3 = ((!float.TryParse(s5, out num3)) ? num2 : num3);
								if (!this.DesireResultTable.ContainsKey(_animalType))
								{
									this.DesireResultTable[_animalType] = new Dictionary<BreedingTypes, Dictionary<DesireType, Dictionary<bool, Dictionary<DesireType, ChangeParamState>>>>();
								}
								if (!this.DesireResultTable[_animalType].ContainsKey(_breedingType))
								{
									this.DesireResultTable[_animalType][_breedingType] = new Dictionary<DesireType, Dictionary<bool, Dictionary<DesireType, ChangeParamState>>>();
								}
								if (!this.DesireResultTable[_animalType][_breedingType].ContainsKey(key))
								{
									this.DesireResultTable[_animalType][_breedingType][key] = new Dictionary<bool, Dictionary<DesireType, ChangeParamState>>();
								}
								if (!this.DesireResultTable[_animalType][_breedingType][key].ContainsKey(key5))
								{
									this.DesireResultTable[_animalType][_breedingType][key][key5] = new Dictionary<DesireType, ChangeParamState>();
								}
								this.DesireResultTable[_animalType][_breedingType][key][key5][key6] = new ChangeParamState(changeType, num2, num3);
							}
						}
					}
				}
			}

			// Token: 0x06003F02 RID: 16130 RVA: 0x0017B12C File Offset: 0x0017952C
			private void LoadWithActor(AnimalDefinePack _animalDefinePack)
			{
				string withActorAnimeInfoListBundleDirectory = _animalDefinePack.AssetBundleNames.WithActorAnimeInfoListBundleDirectory;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(withActorAnimeInfoListBundleDirectory, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											num++;
											AssetBundleInfo sheetInfo = new AssetBundleInfo(string.Empty, list.GetElement(num++), list.GetElement(num++), list.GetElement(num++));
											if (!sheetInfo.assetbundle.IsNullOrEmpty() && !sheetInfo.asset.IsNullOrEmpty())
											{
												if (num2 == 0)
												{
													this.LoadWithAgentAnimationList(sheetInfo, _animalDefinePack);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F03 RID: 16131 RVA: 0x0017B2BC File Offset: 0x001796BC
			private void LoadWithAgentAnimationList(AssetBundleInfo _sheetInfo, AnimalDefinePack _animalDefinePack)
			{
				if (_sheetInfo.assetbundle.IsNullOrEmpty() || _sheetInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 1;
						_sheetInfo = new AssetBundleInfo(string.Empty, list.GetElement(num++), list.GetElement(num++), list.GetElement(num++));
						if (!_sheetInfo.assetbundle.IsNullOrEmpty() && !_sheetInfo.asset.IsNullOrEmpty())
						{
							this.LoadWithAgentAnimationState(_sheetInfo, _animalDefinePack);
						}
					}
				}
			}

			// Token: 0x06003F04 RID: 16132 RVA: 0x0017B398 File Offset: 0x00179798
			private void LoadWithAgentAnimationState(AssetBundleInfo _sheetInfo, AnimalDefinePack _animalDefinePack)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null || excelData.MaxCell == 0)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 2;
						int num2 = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						if (int.TryParse(s, out num2) && num2 >= 0)
						{
							int num3 = 0;
							string s2 = list.GetElement(num++) ?? string.Empty;
							if (int.TryParse(s2, out num3))
							{
								string assetbundle_ = list.GetElement(num++) ?? string.Empty;
								string asset_ = list.GetElement(num++) ?? string.Empty;
								string text = list.GetElement(num++) ?? string.Empty;
								string[] inStateNames = text.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
								bool inFadeEnable;
								if (!bool.TryParse(list.GetElement(num++) ?? string.Empty, out inFadeEnable))
								{
									inFadeEnable = false;
								}
								float inFadeSecond;
								if (!float.TryParse(list.GetElement(num++) ?? string.Empty, out inFadeSecond))
								{
									inFadeSecond = 0f;
								}
								string text2 = list.GetElement(num++) ?? string.Empty;
								string[] outStateNames = text2.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
								bool outFadeEnable;
								if (!bool.TryParse(list.GetElement(num++) ?? string.Empty, out outFadeEnable))
								{
									outFadeEnable = false;
								}
								float outFadeSecond;
								if (!float.TryParse(list.GetElement(num++) ?? string.Empty, out outFadeSecond))
								{
									outFadeSecond = 0f;
								}
								int num4;
								if (!int.TryParse(list.GetElement(num++) ?? string.Empty, out num4))
								{
									num4 = 0;
								}
								int num5;
								if (!int.TryParse(list.GetElement(num++) ?? string.Empty, out num5))
								{
									num5 = 0;
								}
								int layer;
								if (!int.TryParse(list.GetElement(num++) ?? string.Empty, out layer))
								{
									layer = 0;
								}
								float actionPointDistance = _animalDefinePack.WithActorInfo.ActionPointDistance;
								float num6 = 0f;
								if (!float.TryParse(list.GetElement(num++) ?? string.Empty, out num6))
								{
									num6 = actionPointDistance;
								}
								float num7 = 0f;
								if (!float.TryParse(list.GetElement(num++) ?? string.Empty, out num7))
								{
									num7 = actionPointDistance;
								}
								float num8 = 0f;
								if (!float.TryParse(list.GetElement(num++) ?? string.Empty, out num8))
								{
									num8 = actionPointDistance;
								}
								AnimalPlayState animalPlayState = new AnimalPlayState(layer, num3, inStateNames, outStateNames)
								{
									FloatList = new float[]
									{
										num6,
										num7,
										num8
									}
								};
								AnimalPlayState.PlayStateInfo mainStateInfo = animalPlayState.MainStateInfo;
								mainStateInfo.AssetBundleInfo = new AssetBundleInfo(string.Empty, assetbundle_, asset_, string.Empty);
								mainStateInfo.InFadeEnable = inFadeEnable;
								mainStateInfo.InFadeSecond = inFadeSecond;
								mainStateInfo.OutFadeEnable = outFadeEnable;
								mainStateInfo.OutFadeSecond = outFadeSecond;
								mainStateInfo.IsLoop = (0 < num4 || 0 < num5);
								mainStateInfo.LoopMin = num4;
								mainStateInfo.LoopMax = num5;
								mainStateInfo.Controller = this.LoadAnimator(mainStateInfo.AssetBundleInfo);
								Dictionary<int, AnimalPlayState> dictionary;
								if (!this.WithAgentAnimeTable.TryGetValue(num2, out dictionary) || dictionary == null)
								{
									Dictionary<int, AnimalPlayState> dictionary2 = new Dictionary<int, AnimalPlayState>();
									this.WithAgentAnimeTable[num2] = dictionary2;
									dictionary = dictionary2;
								}
								dictionary[num3] = animalPlayState;
							}
						}
					}
				}
			}

			// Token: 0x06003F05 RID: 16133 RVA: 0x0017B788 File Offset: 0x00179B88
			private void LoadPlayerInfo(AnimalDefinePack _definePack)
			{
				string playerInfoListBundleDirectory = _definePack.AssetBundleNames.PlayerInfoListBundleDirectory;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(playerInfoListBundleDirectory, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											num++;
											AssetBundleInfo sheetInfo = new AssetBundleInfo(string.Empty, list.GetElement(num++), list.GetElement(num++), list.GetElement(num++));
											if (!sheetInfo.assetbundle.IsNullOrEmpty() && !sheetInfo.asset.IsNullOrEmpty())
											{
												if (num2 == 0)
												{
													this.LoadPlayerCatchAnimalPoseTable(sheetInfo);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F06 RID: 16134 RVA: 0x0017B918 File Offset: 0x00179D18
			private void LoadPlayerCatchAnimalPoseTable(AssetBundleInfo _sheetInfo)
			{
				if (_sheetInfo.assetbundle.IsNullOrEmpty() || _sheetInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 1;
						AssetBundleInfo sheetInfo = new AssetBundleInfo(string.Empty, list.GetElement(num++), list.GetElement(num++), list.GetElement(num++));
						if (!sheetInfo.assetbundle.IsNullOrEmpty() && !sheetInfo.asset.IsNullOrEmpty())
						{
							this.LoadPlayerCatchAnimalPoseList(sheetInfo);
						}
					}
				}
			}

			// Token: 0x06003F07 RID: 16135 RVA: 0x0017B9F4 File Offset: 0x00179DF4
			private void LoadPlayerCatchAnimalPoseList(AssetBundleInfo _sheetInfo)
			{
				if (_sheetInfo.assetbundle.IsNullOrEmpty() || _sheetInfo.asset.IsNullOrEmpty())
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 2;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string s3 = list.GetElement(num++) ?? string.Empty;
								int postureID;
								if (int.TryParse(s3, out postureID))
								{
									string s4 = list.GetElement(num++) ?? string.Empty;
									int poseID;
									if (int.TryParse(s4, out poseID))
									{
										Dictionary<int, PoseKeyPair> dictionary;
										if (!this.PlayerCatchAnimalPoseTable.TryGetValue(key, out dictionary))
										{
											dictionary = (this.PlayerCatchAnimalPoseTable[key] = new Dictionary<int, PoseKeyPair>());
										}
										dictionary[key2] = new PoseKeyPair
										{
											postureID = postureID,
											poseID = poseID
										};
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F08 RID: 16136 RVA: 0x0017BB74 File Offset: 0x00179F74
			private void LoadAnimalPoint(AnimalDefinePack _define)
			{
				string animalPointPrefabBundleDirectory = _define.AssetBundleNames.AnimalPointPrefabBundleDirectory;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(animalPointPrefabBundleDirectory, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								int num = 1;
								ExcelData.Param element = excelData.list.GetElement(num++);
								this.LoadAnimalPointPrefabInfo((element != null) ? element.list : null, fileNameWithoutExtension);
							}
						}
					}
				}
			}

			// Token: 0x06003F09 RID: 16137 RVA: 0x0017BC40 File Offset: 0x0017A040
			private void LoadAnimalPointPrefabInfo(List<string> _pathRow, string _ver)
			{
				if (_pathRow.IsNullOrEmpty<string>())
				{
					return;
				}
				int num = 1;
				AssetBundleInfo info = new AssetBundleInfo(string.Empty, _pathRow.GetElement(num++) ?? string.Empty, _pathRow.GetElement(num++) ?? string.Empty, _pathRow.GetElement(num++) ?? string.Empty);
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(info);
				if (excelData == null || excelData.MaxCell <= 1)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num2 = 0;
						string name_ = list.GetElement(num2++) ?? string.Empty;
						int key = 0;
						string s = list.GetElement(num2++) ?? string.Empty;
						if (int.TryParse(s, out key))
						{
							int key2 = 0;
							string s2 = list.GetElement(num2++) ?? string.Empty;
							if (int.TryParse(s2, out key2))
							{
								string assetbundle_ = list.GetElement(num2++) ?? string.Empty;
								string asset_ = list.GetElement(num2++) ?? string.Empty;
								string manifest_ = list.GetElement(num2++) ?? string.Empty;
								if (!this.AnimalPointAssetTable.ContainsKey(key))
								{
									this.AnimalPointAssetTable[key] = new Dictionary<int, AssetBundleInfo>();
								}
								this.AnimalPointAssetTable[key][key2] = new AssetBundleInfo(name_, assetbundle_, asset_, manifest_);
							}
						}
					}
				}
			}

			// Token: 0x06003F0A RID: 16138 RVA: 0x0017BE24 File Offset: 0x0017A224
			public AIProject.Animal.Tuple<string, string, string> GetName(List<string> _row, int _bundleIndex, int _assetIndex, int _manifestIndex)
			{
				return new AIProject.Animal.Tuple<string, string, string>(_row.GetElement(_bundleIndex) ?? string.Empty, _row.GetElement(_assetIndex) ?? string.Empty, _row.GetElement(_manifestIndex) ?? string.Empty);
			}

			// Token: 0x06003F0B RID: 16139 RVA: 0x0017BE70 File Offset: 0x0017A270
			public T LoadAsset<T>(List<string> _row, int _bundleIndex, int _assetIndex, int _manifestIndex) where T : UnityEngine.Object
			{
				AIProject.Animal.Tuple<string, string, string> name = this.GetName(_row, _bundleIndex, _assetIndex, _manifestIndex);
				return AssetUtility.LoadAsset<T>(name.Item1, name.Item2, name.Item3);
			}

			// Token: 0x06003F0C RID: 16140 RVA: 0x0017BEA0 File Offset: 0x0017A2A0
			private bool TryStringToEnum<T>(string _name, out T _value) where T : struct
			{
				return Enum.TryParse<T>(_name, true, out _value);
			}

			// Token: 0x04003BAA RID: 15274
			private const int AssetBundleNameIndex = 1;

			// Token: 0x04003BAB RID: 15275
			private const int AssetNameIndex = 2;

			// Token: 0x04003BAC RID: 15276
			private const int ManifestNameIndex = 3;

			// Token: 0x04003BAD RID: 15277
			public readonly Dictionary<int, ActionTypes> AnimalActionIDTable = new Dictionary<int, ActionTypes>
			{
				{
					0,
					ActionTypes.None
				},
				{
					1,
					ActionTypes.Rest
				}
			};

			// Token: 0x04003BAE RID: 15278
			public readonly Dictionary<int, DesireType> AnimalDesireIDTable = new Dictionary<int, DesireType>
			{
				{
					-1,
					DesireType.None
				},
				{
					0,
					DesireType.Sleepiness
				},
				{
					1,
					DesireType.Loneliness
				},
				{
					2,
					DesireType.Action
				}
			};

			// Token: 0x04003BAF RID: 15279
			public readonly Dictionary<int, ChangeType> ChangeTypeIDTable = new Dictionary<int, ChangeType>
			{
				{
					0,
					ChangeType.Add
				},
				{
					1,
					ChangeType.Sub
				},
				{
					2,
					ChangeType.Cng
				}
			};

			// Token: 0x04003BC7 RID: 15303
			private string[] separators = new string[]
			{
				"/",
				"／",
				",",
				"、"
			};
		}

		// Token: 0x020008F2 RID: 2290
		public class AnimationTables
		{
			// Token: 0x06003F0E RID: 16142 RVA: 0x0017C154 File Offset: 0x0017A554
			public RuntimeAnimatorController GetPlayerAnimator(int id, ref AssetBundleInfo outInfo)
			{
				AssetBundleInfo assetBundleInfo;
				if (!this._playerAnimatorAssetTable.TryGetValue(id, out assetBundleInfo))
				{
					return null;
				}
				outInfo = assetBundleInfo;
				return AssetUtility.LoadAsset<RuntimeAnimatorController>(assetBundleInfo);
			}

			// Token: 0x06003F0F RID: 16143 RVA: 0x0017C188 File Offset: 0x0017A588
			public RuntimeAnimatorController GetCharaAnimator(int id, ref AssetBundleInfo outInfo)
			{
				AssetBundleInfo assetBundleInfo;
				if (!this._charaAnimatorAssetTable.TryGetValue(id, out assetBundleInfo))
				{
					return null;
				}
				outInfo = assetBundleInfo;
				return AssetUtility.LoadAsset<RuntimeAnimatorController>(assetBundleInfo);
			}

			// Token: 0x06003F10 RID: 16144 RVA: 0x0017C1BC File Offset: 0x0017A5BC
			public RuntimeAnimatorController GetMerchantAnimator(int id, ref AssetBundleInfo outInfo)
			{
				AssetBundleInfo assetBundleInfo;
				if (!this._merchantAnimatorAssetTable.TryGetValue(id, out assetBundleInfo))
				{
					return null;
				}
				outInfo = assetBundleInfo;
				return AssetUtility.LoadAsset<RuntimeAnimatorController>(assetBundleInfo);
			}

			// Token: 0x06003F11 RID: 16145 RVA: 0x0017C1F0 File Offset: 0x0017A5F0
			public RuntimeAnimatorController GetItemAnimator(int id)
			{
				AssetBundleInfo info;
				if (!this._itemAnimatorAssetTable.TryGetValue(id, out info))
				{
					return null;
				}
				return AssetUtility.LoadAsset<RuntimeAnimatorController>(info);
			}

			// Token: 0x17000BAB RID: 2987
			// (get) Token: 0x06003F12 RID: 16146 RVA: 0x0017C21A File Offset: 0x0017A61A
			// (set) Token: 0x06003F13 RID: 16147 RVA: 0x0017C222 File Offset: 0x0017A622
			public Dictionary<int, Dictionary<int, List<int>>> PersonalActionListTable { get; private set; } = new Dictionary<int, Dictionary<int, List<int>>>();

			// Token: 0x17000BAC RID: 2988
			// (get) Token: 0x06003F14 RID: 16148 RVA: 0x0017C22B File Offset: 0x0017A62B
			// (set) Token: 0x06003F15 RID: 16149 RVA: 0x0017C233 File Offset: 0x0017A633
			public Dictionary<int, Dictionary<int, PlayState>> AgentActionAnimTable { get; private set; } = new Dictionary<int, Dictionary<int, PlayState>>();

			// Token: 0x17000BAD RID: 2989
			// (get) Token: 0x06003F16 RID: 16150 RVA: 0x0017C23C File Offset: 0x0017A63C
			// (set) Token: 0x06003F17 RID: 16151 RVA: 0x0017C244 File Offset: 0x0017A644
			public Dictionary<int, List<AnimeMoveInfo>> AgentMoveInfoTable { get; private set; } = new Dictionary<int, List<AnimeMoveInfo>>();

			// Token: 0x17000BAE RID: 2990
			// (get) Token: 0x06003F18 RID: 16152 RVA: 0x0017C24D File Offset: 0x0017A64D
			// (set) Token: 0x06003F19 RID: 16153 RVA: 0x0017C255 File Offset: 0x0017A655
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> AgentItemEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>();

			// Token: 0x17000BAF RID: 2991
			// (get) Token: 0x06003F1A RID: 16154 RVA: 0x0017C25E File Offset: 0x0017A65E
			// (set) Token: 0x06003F1B RID: 16155 RVA: 0x0017C266 File Offset: 0x0017A666
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> AgentAnimalEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>();

			// Token: 0x17000BB0 RID: 2992
			// (get) Token: 0x06003F1C RID: 16156 RVA: 0x0017C26F File Offset: 0x0017A66F
			// (set) Token: 0x06003F1D RID: 16157 RVA: 0x0017C277 File Offset: 0x0017A677
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>> AgentActSEEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>>();

			// Token: 0x17000BB1 RID: 2993
			// (get) Token: 0x06003F1E RID: 16158 RVA: 0x0017C280 File Offset: 0x0017A680
			// (set) Token: 0x06003F1F RID: 16159 RVA: 0x0017C288 File Offset: 0x0017A688
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>> AgentActOnceVoiceEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>>();

			// Token: 0x17000BB2 RID: 2994
			// (get) Token: 0x06003F20 RID: 16160 RVA: 0x0017C291 File Offset: 0x0017A691
			// (set) Token: 0x06003F21 RID: 16161 RVA: 0x0017C299 File Offset: 0x0017A699
			public Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>> AgentActLoopVoiceEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>();

			// Token: 0x17000BB3 RID: 2995
			// (get) Token: 0x06003F22 RID: 16162 RVA: 0x0017C2A2 File Offset: 0x0017A6A2
			// (set) Token: 0x06003F23 RID: 16163 RVA: 0x0017C2AA File Offset: 0x0017A6AA
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> AgentActParticleEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>>();

			// Token: 0x17000BB4 RID: 2996
			// (get) Token: 0x06003F24 RID: 16164 RVA: 0x0017C2B3 File Offset: 0x0017A6B3
			// (set) Token: 0x06003F25 RID: 16165 RVA: 0x0017C2BB File Offset: 0x0017A6BB
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> AgentChangeClothEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>();

			// Token: 0x17000BB5 RID: 2997
			// (get) Token: 0x06003F26 RID: 16166 RVA: 0x0017C2C4 File Offset: 0x0017A6C4
			// (set) Token: 0x06003F27 RID: 16167 RVA: 0x0017C2CC File Offset: 0x0017A6CC
			public Dictionary<string, List<PlayState.ItemInfo>> SurpriseItemList { get; private set; } = new Dictionary<string, List<PlayState.ItemInfo>>();

			// Token: 0x17000BB6 RID: 2998
			// (get) Token: 0x06003F28 RID: 16168 RVA: 0x0017C2D5 File Offset: 0x0017A6D5
			// (set) Token: 0x06003F29 RID: 16169 RVA: 0x0017C2DD File Offset: 0x0017A6DD
			public Dictionary<int, PlayState> AgentLocomotionStateTable { get; private set; } = new Dictionary<int, PlayState>();

			// Token: 0x17000BB7 RID: 2999
			// (get) Token: 0x06003F2A RID: 16170 RVA: 0x0017C2E6 File Offset: 0x0017A6E6
			// (set) Token: 0x06003F2B RID: 16171 RVA: 0x0017C2EE File Offset: 0x0017A6EE
			public Dictionary<int, PoseKeyPair> TalkSpeakerStateTable { get; private set; } = new Dictionary<int, PoseKeyPair>();

			// Token: 0x17000BB8 RID: 3000
			// (get) Token: 0x06003F2C RID: 16172 RVA: 0x0017C2F7 File Offset: 0x0017A6F7
			// (set) Token: 0x06003F2D RID: 16173 RVA: 0x0017C2FF File Offset: 0x0017A6FF
			public Dictionary<int, PoseKeyPair> TalkListenerStateTable { get; private set; } = new Dictionary<int, PoseKeyPair>();

			// Token: 0x17000BB9 RID: 3001
			// (get) Token: 0x06003F2E RID: 16174 RVA: 0x0017C308 File Offset: 0x0017A708
			// (set) Token: 0x06003F2F RID: 16175 RVA: 0x0017C310 File Offset: 0x0017A710
			public Dictionary<int, int> TalkSpeakerRelationTable { get; private set; } = new Dictionary<int, int>();

			// Token: 0x17000BBA RID: 3002
			// (get) Token: 0x06003F30 RID: 16176 RVA: 0x0017C319 File Offset: 0x0017A719
			// (set) Token: 0x06003F31 RID: 16177 RVA: 0x0017C321 File Offset: 0x0017A721
			public Dictionary<int, int> TalkListenerRelationTable { get; private set; } = new Dictionary<int, int>();

			// Token: 0x17000BBB RID: 3003
			// (get) Token: 0x06003F32 RID: 16178 RVA: 0x0017C32A File Offset: 0x0017A72A
			// (set) Token: 0x06003F33 RID: 16179 RVA: 0x0017C332 File Offset: 0x0017A732
			public Dictionary<int, Resources.TriValues> ItemScaleTable { get; private set; } = new Dictionary<int, Resources.TriValues>();

			// Token: 0x17000BBC RID: 3004
			// (get) Token: 0x06003F34 RID: 16180 RVA: 0x0017C33B File Offset: 0x0017A73B
			// (set) Token: 0x06003F35 RID: 16181 RVA: 0x0017C343 File Offset: 0x0017A743
			public Dictionary<int, Dictionary<int, PlayState>> WithAnimalStateTable { get; private set; } = new Dictionary<int, Dictionary<int, PlayState>>();

			// Token: 0x17000BBD RID: 3005
			// (get) Token: 0x06003F36 RID: 16182 RVA: 0x0017C34C File Offset: 0x0017A74C
			// (set) Token: 0x06003F37 RID: 16183 RVA: 0x0017C354 File Offset: 0x0017A754
			public Dictionary<int, FootStepInfo[]> AgentFootStepEventKeyTable { get; private set; } = new Dictionary<int, FootStepInfo[]>();

			// Token: 0x17000BBE RID: 3006
			// (get) Token: 0x06003F38 RID: 16184 RVA: 0x0017C35D File Offset: 0x0017A75D
			// (set) Token: 0x06003F39 RID: 16185 RVA: 0x0017C365 File Offset: 0x0017A765
			public Dictionary<int, Dictionary<int, UnityEx.ValueTuple<PoseKeyPair, bool>>> AgentGravurePoseTable { get; private set; } = new Dictionary<int, Dictionary<int, UnityEx.ValueTuple<PoseKeyPair, bool>>>();

			// Token: 0x17000BBF RID: 3007
			// (get) Token: 0x06003F3A RID: 16186 RVA: 0x0017C36E File Offset: 0x0017A76E
			// (set) Token: 0x06003F3B RID: 16187 RVA: 0x0017C376 File Offset: 0x0017A776
			public Dictionary<int, Dictionary<int, Dictionary<int, PlayState>>> PlayerActionAnimTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, PlayState>>>();

			// Token: 0x17000BC0 RID: 3008
			// (get) Token: 0x06003F3C RID: 16188 RVA: 0x0017C37F File Offset: 0x0017A77F
			// (set) Token: 0x06003F3D RID: 16189 RVA: 0x0017C387 File Offset: 0x0017A787
			public Dictionary<int, Dictionary<int, List<AnimeMoveInfo>>> PlayerMoveInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, List<AnimeMoveInfo>>>();

			// Token: 0x17000BC1 RID: 3009
			// (get) Token: 0x06003F3E RID: 16190 RVA: 0x0017C390 File Offset: 0x0017A790
			// (set) Token: 0x06003F3F RID: 16191 RVA: 0x0017C398 File Offset: 0x0017A798
			public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>> PlayerItemEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>>();

			// Token: 0x17000BC2 RID: 3010
			// (get) Token: 0x06003F40 RID: 16192 RVA: 0x0017C3A1 File Offset: 0x0017A7A1
			// (set) Token: 0x06003F41 RID: 16193 RVA: 0x0017C3A9 File Offset: 0x0017A7A9
			public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>> PlayerActExEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>>();

			// Token: 0x17000BC3 RID: 3011
			// (get) Token: 0x06003F42 RID: 16194 RVA: 0x0017C3B2 File Offset: 0x0017A7B2
			// (set) Token: 0x06003F43 RID: 16195 RVA: 0x0017C3BA File Offset: 0x0017A7BA
			public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>>> PlayerActSEEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>>>();

			// Token: 0x17000BC4 RID: 3012
			// (get) Token: 0x06003F44 RID: 16196 RVA: 0x0017C3C3 File Offset: 0x0017A7C3
			// (set) Token: 0x06003F45 RID: 16197 RVA: 0x0017C3CB File Offset: 0x0017A7CB
			public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>>> PlayerActParticleEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>>>();

			// Token: 0x17000BC5 RID: 3013
			// (get) Token: 0x06003F46 RID: 16198 RVA: 0x0017C3D4 File Offset: 0x0017A7D4
			// (set) Token: 0x06003F47 RID: 16199 RVA: 0x0017C3DC File Offset: 0x0017A7DC
			public Dictionary<int, Dictionary<int, PlayState>> PlayerLocomotionStateTable { get; private set; } = new Dictionary<int, Dictionary<int, PlayState>>();

			// Token: 0x17000BC6 RID: 3014
			// (get) Token: 0x06003F48 RID: 16200 RVA: 0x0017C3E5 File Offset: 0x0017A7E5
			// (set) Token: 0x06003F49 RID: 16201 RVA: 0x0017C3ED File Offset: 0x0017A7ED
			public Dictionary<int, Dictionary<int, FootStepInfo[]>> PlayerFootStepEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, FootStepInfo[]>>();

			// Token: 0x17000BC7 RID: 3015
			// (get) Token: 0x06003F4A RID: 16202 RVA: 0x0017C3F6 File Offset: 0x0017A7F6
			// (set) Token: 0x06003F4B RID: 16203 RVA: 0x0017C3FE File Offset: 0x0017A7FE
			public Dictionary<int, Dictionary<int, PlayState>> MerchantOnlyActionAnimStateTable { get; private set; } = new Dictionary<int, Dictionary<int, PlayState>>();

			// Token: 0x17000BC8 RID: 3016
			// (get) Token: 0x06003F4C RID: 16204 RVA: 0x0017C407 File Offset: 0x0017A807
			// (set) Token: 0x06003F4D RID: 16205 RVA: 0x0017C40F File Offset: 0x0017A80F
			public Dictionary<int, Dictionary<int, PlayState>> MerchantCommonActionAnimStateTable { get; private set; } = new Dictionary<int, Dictionary<int, PlayState>>();

			// Token: 0x17000BC9 RID: 3017
			// (get) Token: 0x06003F4E RID: 16206 RVA: 0x0017C418 File Offset: 0x0017A818
			// (set) Token: 0x06003F4F RID: 16207 RVA: 0x0017C420 File Offset: 0x0017A820
			public Dictionary<int, PlayState> MerchantLocomotionStateTable { get; private set; } = new Dictionary<int, PlayState>();

			// Token: 0x17000BCA RID: 3018
			// (get) Token: 0x06003F50 RID: 16208 RVA: 0x0017C429 File Offset: 0x0017A829
			// (set) Token: 0x06003F51 RID: 16209 RVA: 0x0017C431 File Offset: 0x0017A831
			public Dictionary<int, List<AnimeMoveInfo>> MerchantMoveInfoTable { get; private set; } = new Dictionary<int, List<AnimeMoveInfo>>();

			// Token: 0x17000BCB RID: 3019
			// (get) Token: 0x06003F52 RID: 16210 RVA: 0x0017C43A File Offset: 0x0017A83A
			// (set) Token: 0x06003F53 RID: 16211 RVA: 0x0017C442 File Offset: 0x0017A842
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> MerchantOnlyItemEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>();

			// Token: 0x17000BCC RID: 3020
			// (get) Token: 0x06003F54 RID: 16212 RVA: 0x0017C44B File Offset: 0x0017A84B
			// (set) Token: 0x06003F55 RID: 16213 RVA: 0x0017C453 File Offset: 0x0017A853
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> MerchantCommonItemEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>();

			// Token: 0x17000BCD RID: 3021
			// (get) Token: 0x06003F56 RID: 16214 RVA: 0x0017C45C File Offset: 0x0017A85C
			// (set) Token: 0x06003F57 RID: 16215 RVA: 0x0017C464 File Offset: 0x0017A864
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>> MerchantOnlySEEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>>();

			// Token: 0x17000BCE RID: 3022
			// (get) Token: 0x06003F58 RID: 16216 RVA: 0x0017C46D File Offset: 0x0017A86D
			// (set) Token: 0x06003F59 RID: 16217 RVA: 0x0017C475 File Offset: 0x0017A875
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>> MerchantCommonSEEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>>();

			// Token: 0x17000BCF RID: 3023
			// (get) Token: 0x06003F5A RID: 16218 RVA: 0x0017C47E File Offset: 0x0017A87E
			// (set) Token: 0x06003F5B RID: 16219 RVA: 0x0017C486 File Offset: 0x0017A886
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>> MerchantOnlyOnceVoiceEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>>();

			// Token: 0x17000BD0 RID: 3024
			// (get) Token: 0x06003F5C RID: 16220 RVA: 0x0017C48F File Offset: 0x0017A88F
			// (set) Token: 0x06003F5D RID: 16221 RVA: 0x0017C497 File Offset: 0x0017A897
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>> MerchantCommonOnceVoiceEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>>();

			// Token: 0x17000BD1 RID: 3025
			// (get) Token: 0x06003F5E RID: 16222 RVA: 0x0017C4A0 File Offset: 0x0017A8A0
			// (set) Token: 0x06003F5F RID: 16223 RVA: 0x0017C4A8 File Offset: 0x0017A8A8
			public Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>> MerchantOnlyLoopVoiceEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>();

			// Token: 0x17000BD2 RID: 3026
			// (get) Token: 0x06003F60 RID: 16224 RVA: 0x0017C4B1 File Offset: 0x0017A8B1
			// (set) Token: 0x06003F61 RID: 16225 RVA: 0x0017C4B9 File Offset: 0x0017A8B9
			public Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>> MerchantCommonLoopVoiceEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>>();

			// Token: 0x17000BD3 RID: 3027
			// (get) Token: 0x06003F62 RID: 16226 RVA: 0x0017C4C2 File Offset: 0x0017A8C2
			// (set) Token: 0x06003F63 RID: 16227 RVA: 0x0017C4CA File Offset: 0x0017A8CA
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> MerchantOnlyParticleEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>>();

			// Token: 0x17000BD4 RID: 3028
			// (get) Token: 0x06003F64 RID: 16228 RVA: 0x0017C4D3 File Offset: 0x0017A8D3
			// (set) Token: 0x06003F65 RID: 16229 RVA: 0x0017C4DB File Offset: 0x0017A8DB
			public Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> MerchantCommonParticleEventKeyTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>>();

			// Token: 0x17000BD5 RID: 3029
			// (get) Token: 0x06003F66 RID: 16230 RVA: 0x0017C4E4 File Offset: 0x0017A8E4
			// (set) Token: 0x06003F67 RID: 16231 RVA: 0x0017C4EC File Offset: 0x0017A8EC
			public Dictionary<int, PoseKeyPair> MerchantListenerStateTable { get; private set; } = new Dictionary<int, PoseKeyPair>();

			// Token: 0x17000BD6 RID: 3030
			// (get) Token: 0x06003F68 RID: 16232 RVA: 0x0017C4F5 File Offset: 0x0017A8F5
			// (set) Token: 0x06003F69 RID: 16233 RVA: 0x0017C4FD File Offset: 0x0017A8FD
			public Dictionary<int, int> MerchantListenerRelationTable { get; private set; } = new Dictionary<int, int>();

			// Token: 0x17000BD7 RID: 3031
			// (get) Token: 0x06003F6A RID: 16234 RVA: 0x0017C506 File Offset: 0x0017A906
			// (set) Token: 0x06003F6B RID: 16235 RVA: 0x0017C50E File Offset: 0x0017A90E
			public Dictionary<int, FootStepInfo[]> MerchantFootStepEventKeyTable { get; private set; } = new Dictionary<int, FootStepInfo[]>();

			// Token: 0x06003F6C RID: 16236 RVA: 0x0017C517 File Offset: 0x0017A917
			public void SyncLoad(DefinePack definePack)
			{
				if (definePack == null)
				{
					return;
				}
				this.Load(definePack, false);
			}

			// Token: 0x06003F6D RID: 16237 RVA: 0x0017C530 File Offset: 0x0017A930
			public void Load(DefinePack definePack, bool awaitable = true)
			{
				this.LoadAnimatorAssetBundles(definePack, awaitable);
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentAnimeInfo, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text, 20))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							int j = 1;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								string element = param.list.GetElement(0);
								int num;
								if (int.TryParse(element, out num))
								{
									switch (num)
									{
									case 0:
										this.LoadAgentActionPersonalIDs(param.list, i, awaitable);
										break;
									case 1:
										this.LoadAgentActionStates(param.list, i, awaitable);
										break;
									case 2:
										this.LoadAgentMoveInfoList(param.list, i, awaitable);
										break;
									case 3:
										this.LoadAgentEventKeyList(param.list, i, awaitable);
										break;
									case 4:
										this.LoadAgentLocomotionStateList(param.list, i, awaitable);
										break;
									case 7:
										this.LoadTalkSpeakerStateList(param.list, i, awaitable);
										break;
									case 8:
										this.LoadTalkListenerStateList(param.list, i, awaitable);
										break;
									case 9:
										this.LoadWithAnimalStateList(param.list, i, awaitable);
										break;
									}
								}
							}
						}
					}
				}
				List<string> assetBundleNameListFromPath2 = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.PlayerMaleAnimeInfo, false);
				assetBundleNameListFromPath2.Sort();
				for (int k = 0; k < assetBundleNameListFromPath2.Count; k++)
				{
					string text2 = assetBundleNameListFromPath2[k];
					if (!Game.IsRestrictedOver50(text2, 20))
					{
						string fileNameWithoutExtension2 = System.IO.Path.GetFileNameWithoutExtension(text2);
						ExcelData excelData2 = AssetUtility.LoadAsset<ExcelData>(text2, fileNameWithoutExtension2, string.Empty);
						if (!(excelData2 == null))
						{
							int l = 1;
							while (l < excelData2.MaxCell)
							{
								ExcelData.Param param2 = excelData2.list[l++];
								string s = param2.list[0];
								int num2;
								if (int.TryParse(s, out num2))
								{
									switch (num2)
									{
									case 0:
										this.LoadPlayerActionStates(param2.list, k, 0, awaitable);
										break;
									case 1:
										this.LoadPlayerMoveInfoList(param2.list, k, 0, awaitable);
										break;
									case 2:
										this.LoadPlayerEventKeyList(param2.list, k, 0, awaitable);
										break;
									case 3:
										this.LoadPlayerLocomotionStateList(param2.list, k, 0, awaitable);
										break;
									}
								}
							}
						}
					}
				}
				List<string> assetBundleNameListFromPath3 = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.PlayerFemaleAnimeInfo, false);
				assetBundleNameListFromPath3.Sort();
				for (int m = 0; m < assetBundleNameListFromPath3.Count; m++)
				{
					string text3 = assetBundleNameListFromPath3[m];
					if (!Game.IsRestrictedOver50(text3, 20))
					{
						string fileNameWithoutExtension3 = System.IO.Path.GetFileNameWithoutExtension(text3);
						ExcelData excelData3 = AssetUtility.LoadAsset<ExcelData>(text3, fileNameWithoutExtension3, string.Empty);
						if (!(excelData3 == null))
						{
							int n = 1;
							while (n < excelData3.MaxCell)
							{
								ExcelData.Param param3 = excelData3.list[n++];
								string element2 = param3.list.GetElement(0);
								int num3;
								if (int.TryParse(element2, out num3))
								{
									switch (num3)
									{
									case 0:
										this.LoadPlayerActionStates(param3.list, m, 1, awaitable);
										break;
									case 1:
										this.LoadPlayerMoveInfoList(param3.list, m, 1, awaitable);
										break;
									case 2:
										this.LoadPlayerEventKeyList(param3.list, m, 1, awaitable);
										break;
									case 3:
										this.LoadPlayerLocomotionStateList(param3.list, m, 1, awaitable);
										break;
									}
								}
							}
						}
					}
				}
				List<string> assetBundleNameListFromPath4 = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.MerchantAnimeInfo, false);
				assetBundleNameListFromPath4.Sort();
				for (int num4 = 0; num4 < assetBundleNameListFromPath4.Count; num4++)
				{
					string text4 = assetBundleNameListFromPath4[num4];
					if (!Game.IsRestrictedOver50(text4))
					{
						string fileNameWithoutExtension4 = System.IO.Path.GetFileNameWithoutExtension(text4);
						ExcelData excelData4 = AssetUtility.LoadAsset<ExcelData>(text4, fileNameWithoutExtension4, string.Empty);
						if (!(excelData4 == null))
						{
							int num5 = 1;
							while (num5 < excelData4.MaxCell)
							{
								ExcelData.Param param4 = excelData4.list[num5++];
								string s2 = param4.list.GetElement(0) ?? string.Empty;
								int num6;
								if (int.TryParse(s2, out num6))
								{
									List<string> list = param4.list;
									switch (num6)
									{
									case 0:
										this.LoadMerchantOnlyActionAnimState(list, fileNameWithoutExtension4, awaitable);
										break;
									case 1:
										this.LoadMerchantCommonActionState(list, fileNameWithoutExtension4, awaitable);
										break;
									case 2:
										this.LoadMerchantLocomotionStateList(list, fileNameWithoutExtension4, awaitable);
										break;
									case 3:
										this.LoadMerchantMoveInfoList(list, fileNameWithoutExtension4, awaitable);
										break;
									case 4:
										this.LoadMerchantEventKeyList(list, fileNameWithoutExtension4, awaitable);
										break;
									case 5:
										this.LoadMerchantListenerStateList(list, fileNameWithoutExtension4, awaitable);
										break;
									}
								}
							}
						}
					}
				}
				this.LoadItemScaleTable(definePack);
				this.LoadAgentGravurePoseTable(definePack, awaitable);
				this.LoadAgentEventKeyTable(definePack, awaitable);
				this.LoadChangeClothEventKeyTable(definePack, awaitable);
				this.LoadPlayerEventKeyTable(definePack, awaitable);
				this.LoadSurpriseItemList(definePack);
				Singleton<Resources>.Instance.LoadMapIK(definePack);
			}

			// Token: 0x06003F6E RID: 16238 RVA: 0x0017CB10 File Offset: 0x0017AF10
			public void Release()
			{
				this._playerAnimatorAssetTable.Clear();
				this._charaAnimatorAssetTable.Clear();
				this._merchantAnimatorAssetTable.Clear();
				this._itemAnimatorAssetTable.Clear();
				this.PersonalActionListTable.Clear();
				this.AgentActionAnimTable.Clear();
				this.AgentMoveInfoTable.Clear();
				this.AgentItemEventKeyTable.Clear();
				this.AgentAnimalEventKeyTable.Clear();
				this.AgentActSEEventKeyTable.Clear();
				this.AgentActOnceVoiceEventKeyTable.Clear();
				this.AgentActLoopVoiceEventKeyTable.Clear();
				this.AgentActParticleEventKeyTable.Clear();
				this.AgentChangeClothEventKeyTable.Clear();
				this.SurpriseItemList.Clear();
				this.AgentLocomotionStateTable.Clear();
				this.TalkSpeakerStateTable.Clear();
				this.TalkListenerStateTable.Clear();
				this.TalkSpeakerRelationTable.Clear();
				this.TalkListenerRelationTable.Clear();
				this.ItemScaleTable.Clear();
				this.WithAnimalStateTable.Clear();
				this.AgentFootStepEventKeyTable.Clear();
				this.AgentGravurePoseTable.Clear();
				this.PlayerActionAnimTable.Clear();
				this.PlayerMoveInfoTable.Clear();
				this.PlayerItemEventKeyTable.Clear();
				this.PlayerActExEventKeyTable.Clear();
				this.PlayerActSEEventKeyTable.Clear();
				this.PlayerActParticleEventKeyTable.Clear();
				this.PlayerLocomotionStateTable.Clear();
				this.PlayerFootStepEventKeyTable.Clear();
				this.MerchantOnlyActionAnimStateTable.Clear();
				this.MerchantCommonActionAnimStateTable.Clear();
				this.MerchantLocomotionStateTable.Clear();
				this.MerchantMoveInfoTable.Clear();
				this.MerchantOnlyItemEventKeyTable.Clear();
				this.MerchantCommonItemEventKeyTable.Clear();
				this.MerchantOnlySEEventKeyTable.Clear();
				this.MerchantCommonSEEventKeyTable.Clear();
				this.MerchantOnlyOnceVoiceEventKeyTable.Clear();
				this.MerchantCommonOnceVoiceEventKeyTable.Clear();
				this.MerchantOnlyLoopVoiceEventKeyTable.Clear();
				this.MerchantCommonLoopVoiceEventKeyTable.Clear();
				this.MerchantOnlyParticleEventKeyTable.Clear();
				this.MerchantCommonParticleEventKeyTable.Clear();
				this.MerchantListenerStateTable.Clear();
				this.MerchantListenerRelationTable.Clear();
				this.MerchantFootStepEventKeyTable.Clear();
				Singleton<Resources>.Instance.MapIKData.Clear();
			}

			// Token: 0x06003F6F RID: 16239 RVA: 0x0017CD48 File Offset: 0x0017B148
			private void LoadAnimatorAssetBundles(DefinePack definePack, bool awaitable)
			{
				if (global::Debug.isDebugBuild)
				{
				}
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActorAnimatorList, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text, 20))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							int j = 1;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								string element = param.list.GetElement(0);
								int num;
								if (int.TryParse(element, out num))
								{
									switch (num)
									{
									case 0:
										if (global::Debug.isDebugBuild)
										{
										}
										this.LoadAnimator(param.list, this._charaAnimatorAssetTable);
										break;
									case 1:
										if (global::Debug.isDebugBuild)
										{
										}
										this.LoadAnimator(param.list, this._playerAnimatorAssetTable);
										break;
									case 2:
										if (global::Debug.isDebugBuild)
										{
										}
										this.LoadAnimator(param.list, this._merchantAnimatorAssetTable);
										break;
									case 3:
										if (global::Debug.isDebugBuild)
										{
										}
										this.LoadAnimator(param.list, this._itemAnimatorAssetTable);
										break;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F70 RID: 16240 RVA: 0x0017CEC0 File Offset: 0x0017B2C0
			private void LoadAnimator(List<string> row, Dictionary<int, AssetBundleInfo> dictionary)
			{
				int num = 2;
				string element = row.GetElement(num++);
				string element2 = row.GetElement(num++);
				num++;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(element, element2, string.Empty);
				if (excelData != null)
				{
					int i = 1;
					while (i < excelData.MaxCell)
					{
						ExcelData.Param param = excelData.list[i++];
						int num2 = 0;
						int key;
						if (int.TryParse(param.list.GetElement(num2++), out key))
						{
							string element3 = param.list.GetElement(num2++);
							string element4 = param.list.GetElement(num2++);
							string element5 = param.list.GetElement(num2++);
							string element6 = param.list.GetElement(num2++);
							dictionary[key] = new AssetBundleInfo
							{
								name = element3,
								assetbundle = element4,
								asset = element5,
								manifest = element6
							};
						}
					}
				}
			}

			// Token: 0x06003F71 RID: 16241 RVA: 0x0017CFE0 File Offset: 0x0017B3E0
			private void LoadPlayerActionStates(List<string> row, int id, int sex, bool awaitable)
			{
				int num = 2;
				string assetbundleName = row[num++];
				string assetName = row[num++];
				string manifestName = row[num++];
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData != null)
				{
					Dictionary<int, Dictionary<int, PlayState>> dic;
					if (!this.PlayerActionAnimTable.TryGetValue(sex, out dic))
					{
						Dictionary<int, Dictionary<int, PlayState>> dictionary = new Dictionary<int, Dictionary<int, PlayState>>();
						this.PlayerActionAnimTable[sex] = dictionary;
						dic = dictionary;
					}
					for (int i = 1; i < excelData.MaxCell; i++)
					{
						ExcelData.Param param = excelData.list[i];
						if (!param.list.IsNullOrEmpty<string>())
						{
							int num2 = 0;
							string element = param.list.GetElement(num2++);
							if (!element.IsNullOrEmpty())
							{
								string assetbundleName2 = param.list[num2++];
								string assetName2 = param.list[num2++];
								string manifestName2 = param.list[num2++];
								ExcelData actionExcel = AssetUtility.LoadAsset<ExcelData>(assetbundleName2, assetName2, manifestName2);
								Resources.LoadActionAnimationInfo(actionExcel, dic, awaitable);
							}
						}
					}
				}
			}

			// Token: 0x06003F72 RID: 16242 RVA: 0x0017D118 File Offset: 0x0017B518
			private void LoadPlayerMoveInfoList(List<string> row, int id, int sex, bool awaitable)
			{
				int num = 2;
				string element = row.GetElement(num++);
				string element2 = row.GetElement(num++);
				row.GetElement(num++);
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(element, element2, string.Empty);
				if (excelData == null)
				{
					return;
				}
				Dictionary<int, List<AnimeMoveInfo>> dictionary;
				if (!this.PlayerMoveInfoTable.TryGetValue(sex, out dictionary))
				{
					Dictionary<int, List<AnimeMoveInfo>> dictionary2 = new Dictionary<int, List<AnimeMoveInfo>>();
					this.PlayerMoveInfoTable[sex] = dictionary2;
					dictionary = dictionary2;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					ExcelData.Param param = excelData.list[i];
					int j = 0;
					string element3 = param.list.GetElement(j++);
					int key = Animator.StringToHash(element3);
					List<AnimeMoveInfo> list;
					if (!dictionary.TryGetValue(key, out list))
					{
						List<AnimeMoveInfo> list2 = new List<AnimeMoveInfo>();
						dictionary[key] = list2;
						list = list2;
					}
					while (j < param.list.Count)
					{
						string element4 = param.list.GetElement(j++);
						string element5 = param.list.GetElement(j++);
						string element6 = param.list.GetElement(j++);
						if (!element6.IsNullOrEmpty())
						{
							float start;
							float end;
							if (float.TryParse(element4, out start) && float.TryParse(element5, out end))
							{
								AnimeMoveInfo item = new AnimeMoveInfo
								{
									start = start,
									end = end,
									movePoint = element6
								};
								list.Add(item);
							}
						}
					}
				}
			}

			// Token: 0x06003F73 RID: 16243 RVA: 0x0017D2BC File Offset: 0x0017B6BC
			private void LoadPlayerEventKeyList(List<string> row, int id, int sex, bool awaitable)
			{
				int num = 2;
				string element = row.GetElement(num++);
				string element2 = row.GetElement(num++);
				row.GetElement(num++);
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(element, element2, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					ExcelData.Param param = excelData.list[i];
					int num2 = 1;
					string element3 = param.list.GetElement(num2++);
					string element4 = param.list.GetElement(num2++);
					if (!element3.IsNullOrEmpty() && !element4.IsNullOrEmpty())
					{
						if (element4.Contains("item"))
						{
							Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> eventKeyTable;
							if (!this.PlayerItemEventKeyTable.TryGetValue(sex, out eventKeyTable))
							{
								Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> dictionary = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>();
								this.PlayerItemEventKeyTable[sex] = dictionary;
								eventKeyTable = dictionary;
							}
							this.LoadEventKeyTable(param.list, eventKeyTable, awaitable);
						}
						else if (element4.Contains("actex"))
						{
							Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> eventKeyTable2;
							if (!this.PlayerActExEventKeyTable.TryGetValue(sex, out eventKeyTable2))
							{
								Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> dictionary = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>>();
								this.PlayerActExEventKeyTable[sex] = dictionary;
								eventKeyTable2 = dictionary;
							}
							this.LoadEventKeyTable(param.list, eventKeyTable2, awaitable);
						}
					}
				}
			}

			// Token: 0x06003F74 RID: 16244 RVA: 0x0017D420 File Offset: 0x0017B820
			private void LoadPlayerLocomotionStateList(List<string> row, int id, int sex, bool awaitable)
			{
				int num = 2;
				string assetbundleName = row[num++];
				string assetName = row[num++];
				string manifestName = row[num++];
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData != null)
				{
					Dictionary<int, PlayState> dictionary = new Dictionary<int, PlayState>();
					this.PlayerLocomotionStateTable[sex] = dictionary;
					Dictionary<int, PlayState> dic = dictionary;
					this.LoadLocomotionStateList(excelData, dic, awaitable);
				}
			}

			// Token: 0x06003F75 RID: 16245 RVA: 0x0017D490 File Offset: 0x0017B890
			private void LoadAgentActionPersonalIDs(List<string> row, int id, bool awaitable)
			{
				int num = 2;
				string assetbundleName = row[num++];
				string assetName = row[num++];
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, string.Empty);
				if (excelData != null)
				{
					foreach (ExcelData.Param param in excelData.list)
					{
						if (!param.list.IsNullOrEmpty<string>())
						{
							int index = 1;
							string assetbundleName2 = param.list[index++];
							string assetName2 = param.list[index++];
							ExcelData excelData2 = AssetUtility.LoadAsset<ExcelData>(assetbundleName2, assetName2, string.Empty);
							if (!(excelData2 == null))
							{
								foreach (ExcelData.Param param2 in excelData2.list)
								{
									index = 1;
									int key;
									if (int.TryParse(param2.list.GetElement(index++), out key))
									{
										Dictionary<int, List<int>> dictionary;
										if (!this.PersonalActionListTable.TryGetValue(key, out dictionary))
										{
											Dictionary<int, List<int>> dictionary2 = new Dictionary<int, List<int>>();
											this.PersonalActionListTable[key] = dictionary2;
											dictionary = dictionary2;
										}
										int key2;
										if (int.TryParse(param2.list.GetElement(index++), out key2))
										{
											List<int> list;
											if (!dictionary.TryGetValue(key2, out list))
											{
												List<int> list2 = new List<int>();
												dictionary[key2] = list2;
												list = list2;
											}
											string element = param2.list.GetElement(index);
											if (!element.IsNullOrEmpty())
											{
												string[] array = element.Split(Resources.AnimationTables._comma, StringSplitOptions.RemoveEmptyEntries);
												foreach (string s in array)
												{
													int item;
													if (int.TryParse(s, out item))
													{
														if (!list.Contains(item))
														{
															list.Add(item);
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F76 RID: 16246 RVA: 0x0017D6FC File Offset: 0x0017BAFC
			private void LoadAgentActionStates(List<string> row, int id, bool awaitable)
			{
				int num = 2;
				string assetbundleName = row[num++];
				string assetName = row[num++];
				string manifestName = row[num++];
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData != null)
				{
					Dictionary<int, Dictionary<int, PlayState>> agentActionAnimTable = this.AgentActionAnimTable;
					for (int i = 1; i < excelData.MaxCell; i++)
					{
						ExcelData.Param param = excelData.list[i];
						if (!param.list.IsNullOrEmpty<string>())
						{
							int num2 = 0;
							string element = param.list.GetElement(num2++);
							if (!element.IsNullOrEmpty())
							{
								string assetbundleName2 = param.list[num2++];
								string assetName2 = param.list[num2++];
								string manifestName2 = param.list[num2++];
								ExcelData actionExcel = AssetUtility.LoadAsset<ExcelData>(assetbundleName2, assetName2, manifestName2);
								Resources.LoadActionAnimationInfo(actionExcel, agentActionAnimTable, awaitable);
							}
						}
					}
				}
			}

			// Token: 0x06003F77 RID: 16247 RVA: 0x0017D810 File Offset: 0x0017BC10
			private void LoadAgentMoveInfoList(List<string> row, int id, bool awaitablw)
			{
				int num = 2;
				string element = row.GetElement(num++);
				string element2 = row.GetElement(num++);
				row.GetElement(num++);
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(element, element2, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					ExcelData.Param param = excelData.list[i];
					int j = 0;
					string element3 = param.list.GetElement(j++);
					int key = Animator.StringToHash(element3);
					List<AnimeMoveInfo> list;
					if (!this.AgentMoveInfoTable.TryGetValue(key, out list))
					{
						List<AnimeMoveInfo> list2 = new List<AnimeMoveInfo>();
						this.AgentMoveInfoTable[key] = list2;
						list = list2;
					}
					while (j < param.list.Count)
					{
						string element4 = param.list.GetElement(j++);
						string element5 = param.list.GetElement(j++);
						string element6 = param.list.GetElement(j++);
						if (!element6.IsNullOrEmpty())
						{
							float start;
							float end;
							if (float.TryParse(element4, out start) && float.TryParse(element5, out end))
							{
								AnimeMoveInfo item = new AnimeMoveInfo
								{
									start = start,
									end = end,
									movePoint = element6
								};
								list.Add(item);
							}
						}
					}
				}
			}

			// Token: 0x06003F78 RID: 16248 RVA: 0x0017D990 File Offset: 0x0017BD90
			private void LoadAgentEventKeyList(List<string> row, int id, bool awaitable)
			{
				int num = 2;
				string element = row.GetElement(num++);
				string element2 = row.GetElement(num++);
				row.GetElement(num++);
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(element, element2, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					ExcelData.Param param = excelData.list[i];
					int num2 = 1;
					string element3 = param.list.GetElement(num2++);
					string element4 = param.list.GetElement(num2++);
					if (!element3.IsNullOrEmpty() && !element4.IsNullOrEmpty())
					{
						if (element4.Contains("item"))
						{
							this.LoadEventKeyTable(param.list, this.AgentItemEventKeyTable, awaitable);
						}
					}
				}
			}

			// Token: 0x06003F79 RID: 16249 RVA: 0x0017DA78 File Offset: 0x0017BE78
			private void LoadAgentLocomotionStateList(List<string> row, int id, bool awaitable)
			{
				int num = 2;
				string assetbundleName = row[num++];
				string assetName = row[num++];
				string manifestName = row[num++];
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				this.LoadLocomotionStateList(excelData, this.AgentLocomotionStateTable, awaitable);
			}

			// Token: 0x06003F7A RID: 16250 RVA: 0x0017DAC4 File Offset: 0x0017BEC4
			private void LoadTalkSpeakerStateList(List<string> row, int id, bool awaitable)
			{
				int num = 2;
				string assetbundleName = row[num++];
				string assetName = row[num++];
				string manifestName = row[num++];
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData != null)
				{
					int i = 1;
					while (i < excelData.MaxCell)
					{
						ExcelData.Param param = excelData.list[i++];
						int num2 = 0;
						string element = param.list.GetElement(num2++);
						int key;
						if (int.TryParse(element, out key))
						{
							num2++;
							string element2 = param.list.GetElement(num2++);
							int postureID;
							if (int.TryParse(element2, out postureID))
							{
								string element3 = param.list.GetElement(num2++);
								int poseID;
								if (int.TryParse(element3, out poseID))
								{
									this.TalkSpeakerStateTable[key] = new PoseKeyPair
									{
										postureID = postureID,
										poseID = poseID
									};
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F7B RID: 16251 RVA: 0x0017DBE4 File Offset: 0x0017BFE4
			private void LoadTalkListenerStateList(List<string> row, int id, bool awaitable)
			{
				int num = 2;
				string assetbundleName = row[num++];
				string assetName = row[num++];
				string manifestName = row[num++];
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData != null)
				{
					int i = 1;
					while (i < excelData.MaxCell)
					{
						ExcelData.Param param = excelData.list[i++];
						int num2 = 0;
						string element = param.list.GetElement(num2++);
						int key;
						if (int.TryParse(element, out key))
						{
							num2++;
							string element2 = param.list.GetElement(num2++);
							int postureID;
							if (int.TryParse(element2, out postureID))
							{
								string element3 = param.list.GetElement(num2++);
								int poseID;
								if (int.TryParse(element3, out poseID))
								{
									this.TalkListenerStateTable[key] = new PoseKeyPair
									{
										postureID = postureID,
										poseID = poseID
									};
									string element4 = param.list.GetElement(num2++);
									int num3;
									int value = (!int.TryParse(element4, out num3)) ? 0 : num3;
									this.TalkListenerRelationTable[key] = value;
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F7C RID: 16252 RVA: 0x0017DD40 File Offset: 0x0017C140
			private void LoadAgentGravurePoseTable(DefinePack definePack, bool awaitable)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.GravurePoseInfo, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text, 20))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (!text.IsNullOrEmpty() && !fileNameWithoutExtension.IsNullOrEmpty())
						{
							AssetBundleInfo listAsset = new AssetBundleInfo(string.Empty, text, fileNameWithoutExtension, string.Empty);
							this.LoadGravurePoseList(listAsset, false);
						}
					}
				}
			}

			// Token: 0x06003F7D RID: 16253 RVA: 0x0017DDE0 File Offset: 0x0017C1E0
			private void LoadGravurePoseList(AssetBundleInfo listAsset, bool awaitable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(listAsset);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							num++;
							string s2 = list.GetElement(num++) ?? string.Empty;
							int postureID;
							if (int.TryParse(s2, out postureID))
							{
								string s3 = list.GetElement(num++) ?? string.Empty;
								int poseID;
								if (int.TryParse(s3, out poseID))
								{
									string text = list.GetElement(num++) ?? string.Empty;
									string[] array = text.Split(Resources._separationKeywords, StringSplitOptions.RemoveEmptyEntries);
									string value = list.GetElement(num++) ?? string.Empty;
									bool i2;
									if (!bool.TryParse(value, out i2))
									{
										i2 = true;
									}
									List<int> list2 = ListPool<int>.Get();
									if (!array.IsNullOrEmpty<string>())
									{
										foreach (string text2 in array)
										{
											if (!text2.IsNullOrEmpty())
											{
												int item;
												if (int.TryParse(text2, out item))
												{
													list2.Add(item);
												}
											}
										}
									}
									if (!list2.IsNullOrEmpty<int>())
									{
										PoseKeyPair i3 = new PoseKeyPair
										{
											postureID = postureID,
											poseID = poseID
										};
										foreach (int key2 in list2)
										{
											Dictionary<int, UnityEx.ValueTuple<PoseKeyPair, bool>> dictionary;
											if (!this.AgentGravurePoseTable.TryGetValue(key2, out dictionary))
											{
												dictionary = (this.AgentGravurePoseTable[key2] = new Dictionary<int, UnityEx.ValueTuple<PoseKeyPair, bool>>());
											}
											dictionary[key] = new UnityEx.ValueTuple<PoseKeyPair, bool>(i3, i2);
										}
									}
									ListPool<int>.Release(list2);
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F7E RID: 16254 RVA: 0x0017E028 File Offset: 0x0017C428
			private void LoadWithAnimalStateList(List<string> row, int id, bool awaitable)
			{
				int num = 2;
				string assetbundleName = row.GetElement(num++) ?? string.Empty;
				string assetName = row.GetElement(num++) ?? string.Empty;
				string manifestName = row.GetElement(num++) ?? string.Empty;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData == null || excelData.MaxCell == 0)
				{
					return;
				}
				int num2 = 1;
				ExcelData.Param element = excelData.list.GetElement(num2++);
				this.LoadWithAnimalAnimationList((element != null) ? element.list : null, awaitable);
				ExcelData.Param element2 = excelData.list.GetElement(num2++);
				this.LoadEventKeyTable((element2 != null) ? element2.list : null, this.AgentAnimalEventKeyTable, awaitable);
			}

			// Token: 0x06003F7F RID: 16255 RVA: 0x0017E100 File Offset: 0x0017C500
			private void LoadWithAnimalAnimationList(List<string> row, bool awaitable)
			{
				if (row.IsNullOrEmpty<string>())
				{
					return;
				}
				int num = 1;
				string assetbundleName = row.GetElement(num++) ?? string.Empty;
				string assetName = row.GetElement(num++) ?? string.Empty;
				string manifestName = row.GetElement(num++) ?? string.Empty;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData == null || excelData.MaxCell == 0)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					ExcelData.Param element = excelData.list.GetElement(i);
					List<string> list = (element != null) ? element.list : null;
					if (!list.IsNullOrEmpty<string>())
					{
						num = 1;
						string assetbundleName2 = list.GetElement(num++) ?? string.Empty;
						string assetName2 = list.GetElement(num++) ?? string.Empty;
						string manifestName2 = list.GetElement(num++) ?? string.Empty;
						ExcelData withAnimalExcel = AssetUtility.LoadAsset<ExcelData>(assetbundleName2, assetName2, manifestName2);
						this.LoadWithAnimalAnimationState(withAnimalExcel, awaitable);
					}
				}
			}

			// Token: 0x06003F80 RID: 16256 RVA: 0x0017E23C File Offset: 0x0017C63C
			private void LoadWithAnimalAnimationState(ExcelData withAnimalExcel, bool awaitable)
			{
				if (withAnimalExcel == null || withAnimalExcel.MaxCell == 0)
				{
					return;
				}
				int i = 1;
				while (i < withAnimalExcel.MaxCell)
				{
					ExcelData.Param element = withAnimalExcel.list.GetElement(i++);
					List<string> list = (element != null) ? element.list : null;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 2;
						int key = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						if (int.TryParse(s, out key))
						{
							int key2 = 0;
							string s2 = list.GetElement(num++) ?? string.Empty;
							if (int.TryParse(s2, out key2))
							{
								string assetbundle_ = list.GetElement(num++) ?? string.Empty;
								string asset_ = list.GetElement(num++) ?? string.Empty;
								string text = list.GetElement(num++) ?? string.Empty;
								string[] inStateNames = text.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
								bool enableFade;
								if (!bool.TryParse(list.GetElement(num++) ?? string.Empty, out enableFade))
								{
									enableFade = false;
								}
								float fadeSecond;
								if (!float.TryParse(list.GetElement(num++) ?? string.Empty, out fadeSecond))
								{
									fadeSecond = 0f;
								}
								string text2 = list.GetElement(num++) ?? string.Empty;
								string[] outStateNames = text2.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
								bool enableFade2;
								if (!bool.TryParse(list.GetElement(num++) ?? string.Empty, out enableFade2))
								{
									enableFade2 = false;
								}
								float fadeSecond2;
								if (!float.TryParse(list.GetElement(num++) ?? string.Empty, out fadeSecond2))
								{
									fadeSecond2 = 0f;
								}
								int num2;
								if (!int.TryParse(list.GetElement(num++) ?? string.Empty, out num2))
								{
									num2 = 0;
								}
								int num3;
								if (!int.TryParse(list.GetElement(num++) ?? string.Empty, out num3))
								{
									num3 = 0;
								}
								int layer;
								if (!int.TryParse(list.GetElement(num++) ?? string.Empty, out layer))
								{
									layer = 0;
								}
								string input = list.GetElement(num++) ?? string.Empty;
								List<UnityEx.ValueTuple<string, bool, int, bool>> list2 = ListPool<UnityEx.ValueTuple<string, bool, int, bool>>.Get();
								MatchCollection matchCollection = this._regex.Matches(input);
								if (0 < matchCollection.Count)
								{
									for (int j = 0; j < matchCollection.Count; j++)
									{
										Match match = matchCollection[j];
										for (int k = 0; k < match.Groups[1].Captures.Count; k++)
										{
											string value = match.Groups[1].Captures[k].Value;
											string[] source = value.Split(Resources._separationKeywords, StringSplitOptions.RemoveEmptyEntries);
											int num4 = 0;
											string i2 = source.GetElement(num4++) ?? string.Empty;
											bool i3;
											if (!bool.TryParse(source.GetElement(num4++) ?? string.Empty, out i3))
											{
												i3 = false;
											}
											int i4;
											if (!int.TryParse(source.GetElement(num4++) ?? string.Empty, out i4))
											{
												i4 = -1;
											}
											bool i5;
											if (!bool.TryParse(source.GetElement(num4++) ?? string.Empty, out i5))
											{
												i5 = false;
											}
											list2.Add(new UnityEx.ValueTuple<string, bool, int, bool>(i2, i3, i4, i5));
										}
									}
								}
								PlayState playState = new PlayState(layer, inStateNames, outStateNames);
								playState.MainStateInfo.AssetBundleInfo = new AssetBundleInfo(string.Empty, assetbundle_, asset_, string.Empty);
								PlayState.PlayStateInfo mainStateInfo = playState.MainStateInfo;
								mainStateInfo.InStateInfo.EnableFade = enableFade;
								mainStateInfo.InStateInfo.FadeSecond = fadeSecond;
								mainStateInfo.OutStateInfo.EnableFade = enableFade2;
								mainStateInfo.OutStateInfo.FadeSecond = fadeSecond2;
								mainStateInfo.IsLoop = (0 < num2 || 0 < num3);
								mainStateInfo.LoopMin = num2;
								mainStateInfo.LoopMax = num3;
								playState.ActionInfo = new ActionInfo(false, 0);
								foreach (UnityEx.ValueTuple<string, bool, int, bool> valueTuple in list2)
								{
									playState.AddItemInfo(new PlayState.ItemInfo
									{
										parentName = valueTuple.Item1,
										fromEquipedItem = valueTuple.Item2,
										itemID = valueTuple.Item3,
										isSync = valueTuple.Item4
									});
								}
								ListPool<UnityEx.ValueTuple<string, bool, int, bool>>.Release(list2);
								if (!this.WithAnimalStateTable.ContainsKey(key))
								{
									this.WithAnimalStateTable[key] = new Dictionary<int, PlayState>();
								}
								this.WithAnimalStateTable[key][key2] = playState;
							}
						}
					}
				}
			}

			// Token: 0x06003F81 RID: 16257 RVA: 0x0017E774 File Offset: 0x0017CB74
			private void LoadEventKeyTable(List<string> row, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> eventKeyTable, bool awaitable)
			{
				int num = 1;
				string element = row.GetElement(num++);
				string element2 = row.GetElement(num++);
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(element, element2, string.Empty);
				for (int i = 2; i < excelData.MaxCell; i++)
				{
					ExcelData.Param param = excelData.list[i];
					int num2 = 2;
					string element3 = param.list.GetElement(num2++);
					string element4 = param.list.GetElement(num2++);
					int key;
					if (int.TryParse(element3, out key))
					{
						int key2;
						if (int.TryParse(element4, out key2))
						{
							string element5 = param.list.GetElement(num2++);
							int key3 = Animator.StringToHash(element5);
							Dictionary<int, Dictionary<int, List<AnimeEventInfo>>> dictionary;
							if (!eventKeyTable.TryGetValue(key, out dictionary))
							{
								Dictionary<int, Dictionary<int, List<AnimeEventInfo>>> dictionary2 = new Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>();
								eventKeyTable[key] = dictionary2;
								dictionary = dictionary2;
							}
							Dictionary<int, List<AnimeEventInfo>> dictionary3;
							if (!dictionary.TryGetValue(key2, out dictionary3))
							{
								Dictionary<int, List<AnimeEventInfo>> dictionary4 = new Dictionary<int, List<AnimeEventInfo>>();
								dictionary[key2] = dictionary4;
								dictionary3 = dictionary4;
							}
							List<AnimeEventInfo> list;
							if (!dictionary3.TryGetValue(key3, out list))
							{
								List<AnimeEventInfo> list2 = new List<AnimeEventInfo>();
								dictionary3[key3] = list2;
								list = list2;
							}
							string element6 = param.list.GetElement(num2++);
							if (!element6.IsNullOrEmpty())
							{
								MatchCollection matchCollection = this._regex.Matches(element6);
								if (matchCollection.Count > 0)
								{
									for (int j = 0; j < matchCollection.Count; j++)
									{
										Match match = matchCollection[j];
										for (int k = 0; k < match.Groups[1].Captures.Count; k++)
										{
											string value = match.Groups[1].Captures[k].Value;
											string[] source = value.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
											int num3 = 0;
											float maxValue;
											if (!float.TryParse(source.GetElement(num3++), out maxValue))
											{
												maxValue = float.MaxValue;
											}
											int eventID;
											if (!int.TryParse(source.GetElement(num3++), out eventID))
											{
												eventID = -1;
											}
											list.Add(new AnimeEventInfo
											{
												normalizedTime = maxValue,
												eventID = eventID
											});
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F82 RID: 16258 RVA: 0x0017E9DC File Offset: 0x0017CDDC
			private void LoadSEEventKeyTable(string sheetBundleName, string sheetAssetName, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>> table, bool awaitable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetBundleName, sheetAssetName, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 2; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						if (list.GetElement(0) == "end")
						{
							break;
						}
						int num = 2;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string element = list.GetElement(num++);
								if (!element.IsNullOrEmpty())
								{
									int key3 = Animator.StringToHash(element);
									Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>> dictionary;
									if (!table.TryGetValue(key, out dictionary))
									{
										dictionary = (table[key] = new Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>());
									}
									Dictionary<int, List<AnimeSEEventInfo>> dictionary2;
									if (!dictionary.TryGetValue(key2, out dictionary2))
									{
										dictionary2 = (dictionary[key2] = new Dictionary<int, List<AnimeSEEventInfo>>());
									}
									List<AnimeSEEventInfo> list2;
									if (!dictionary2.TryGetValue(key3, out list2) || list2 == null)
									{
										list2 = (dictionary2[key3] = new List<AnimeSEEventInfo>());
									}
									else if (!list2.IsNullOrEmpty<AnimeSEEventInfo>())
									{
										list2.Clear();
									}
									string element2 = list.GetElement(num++);
									if (!element2.IsNullOrEmpty())
									{
										string[] array = element2.Split(Resources.AnimationTables._seEventSepa, StringSplitOptions.RemoveEmptyEntries);
										if (!array.IsNullOrEmpty<string>())
										{
											foreach (string text in array)
											{
												if (!text.IsNullOrEmpty())
												{
													string text2 = text.Replace(string.Empty, Resources.AnimationTables._seEventRemoveStr);
													if (!text2.IsNullOrEmpty())
													{
														string[] source = text2.Split(Resources.AnimationTables._separators, StringSplitOptions.None);
														int num2 = 0;
														float num3;
														if (float.TryParse(source.GetElement(num2++) ?? string.Empty, out num3))
														{
															if (num3 < 0f || 1f < num3)
															{
															}
															int clipID;
															if (int.TryParse(source.GetElement(num2++) ?? string.Empty, out clipID))
															{
																int eventID;
																if (!int.TryParse(source.GetElement(num2++) ?? string.Empty, out eventID))
																{
																	eventID = -1;
																}
																string root = source.GetElement(num2++) ?? string.Empty;
																list2.Add(new AnimeSEEventInfo
																{
																	NormalizedTime = num3,
																	ClipID = clipID,
																	EventID = eventID,
																	Root = root
																});
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F83 RID: 16259 RVA: 0x0017ECE8 File Offset: 0x0017D0E8
			private void LoadFootStepEventKeyTable(string sheetAssetBundle, string sheetAsset, Dictionary<int, FootStepInfo[]> eventKeyTable, bool awaitable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetAssetBundle, sheetAsset, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 1;
						string name = list.GetElement(j++) ?? string.Empty;
						List<FootStepInfo> list2 = ListPool<FootStepInfo>.Get();
						while (j < list.Count)
						{
							string s = list.GetElement(j++) ?? string.Empty;
							float min;
							if (!float.TryParse(s, out min))
							{
								break;
							}
							string s2 = list.GetElement(j++) ?? string.Empty;
							float max;
							if (!float.TryParse(s2, out max))
							{
								break;
							}
							string text = list.GetElement(j++) ?? string.Empty;
							string[] array = text.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
							if (array.IsNullOrEmpty<string>())
							{
								break;
							}
							List<float> list3 = ListPool<float>.Get();
							foreach (string text2 in array)
							{
								float item;
								if (float.TryParse(text2 ?? string.Empty, out item))
								{
									list3.Add(item);
								}
							}
							if (!list3.IsNullOrEmpty<float>())
							{
								list2.Add(new FootStepInfo(min, max, list3));
							}
							ListPool<float>.Release(list3);
						}
						if (!list2.IsNullOrEmpty<FootStepInfo>())
						{
							int key = Animator.StringToHash(name);
							FootStepInfo[] array3 = new FootStepInfo[list2.Count];
							for (int l = 0; l < list2.Count; l++)
							{
								array3[l] = list2[l];
							}
							eventKeyTable[key] = array3;
						}
						ListPool<FootStepInfo>.Release(list2);
					}
				}
			}

			// Token: 0x06003F84 RID: 16260 RVA: 0x0017EEE4 File Offset: 0x0017D2E4
			private void LoadParticleEventKeyTable(string sheetAssetBundleName, string sheetAssetName, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> eventKeyTable, bool awaitable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetAssetBundleName, sheetAssetName, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 2; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 2;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string element = list.GetElement(num++);
								if (!element.IsNullOrEmpty())
								{
									int key3 = Animator.StringToHash(element);
									Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>> dictionary;
									if (!eventKeyTable.TryGetValue(key, out dictionary))
									{
										dictionary = (eventKeyTable[key] = new Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>());
									}
									Dictionary<int, List<AnimeParticleEventInfo>> dictionary2;
									if (!dictionary.TryGetValue(key2, out dictionary2))
									{
										dictionary2 = (dictionary[key2] = new Dictionary<int, List<AnimeParticleEventInfo>>());
									}
									List<AnimeParticleEventInfo> list2;
									if (!dictionary2.TryGetValue(key3, out list2) || list2 == null)
									{
										list2 = (dictionary2[key3] = new List<AnimeParticleEventInfo>());
									}
									else if (!list2.IsNullOrEmpty<AnimeParticleEventInfo>())
									{
										list2.Clear();
									}
									string element2 = list.GetElement(num++);
									if (!element2.IsNullOrEmpty())
									{
										MatchCollection matchCollection = this._regex.Matches(element2);
										if (0 < matchCollection.Count)
										{
											for (int j = 0; j < matchCollection.Count; j++)
											{
												Match match = matchCollection[j];
												for (int k = 0; k < match.Groups[1].Captures.Count; k++)
												{
													string value = match.Groups[1].Captures[k].Value;
													string[] source = value.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
													int num2 = 0;
													string s3 = source.GetElement(num2++) ?? string.Empty;
													float normalizedTime;
													if (float.TryParse(s3, out normalizedTime))
													{
														string s4 = source.GetElement(num2++) ?? string.Empty;
														int particleID;
														if (int.TryParse(s4, out particleID))
														{
															string s5 = source.GetElement(num2++) ?? string.Empty;
															int eventID;
															if (int.TryParse(s5, out eventID))
															{
																string root = source.GetElement(num2++) ?? string.Empty;
																list2.Add(new AnimeParticleEventInfo
																{
																	NormalizedTime = normalizedTime,
																	ParticleID = particleID,
																	EventID = eventID,
																	Root = root
																});
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F85 RID: 16261 RVA: 0x0017F1E4 File Offset: 0x0017D5E4
			private void LoadLoopVoiceEventKeyTable(string assetBundleName, string assetName, Dictionary<int, Dictionary<int, Dictionary<int, List<int>>>> table, bool awaitable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetBundleName, assetName, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 2;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string element = list.GetElement(num++);
								if (!element.IsNullOrEmpty())
								{
									string text = list.GetElement(num++) ?? string.Empty;
									string[] array = text.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
									List<int> list2 = ListPool<int>.Get();
									if (!array.IsNullOrEmpty<string>())
									{
										foreach (string text2 in array)
										{
											if (!text2.IsNullOrEmpty())
											{
												int item;
												if (int.TryParse(text2, out item))
												{
													list2.Add(item);
												}
											}
										}
									}
									if (list2.IsNullOrEmpty<int>())
									{
										ListPool<int>.Release(list2);
									}
									else
									{
										int key3 = Animator.StringToHash(element);
										Dictionary<int, Dictionary<int, List<int>>> dictionary;
										if (!table.TryGetValue(key, out dictionary))
										{
											dictionary = (table[key] = new Dictionary<int, Dictionary<int, List<int>>>());
										}
										Dictionary<int, List<int>> dictionary2;
										if (!dictionary.TryGetValue(key2, out dictionary2))
										{
											dictionary2 = (dictionary[key2] = new Dictionary<int, List<int>>());
										}
										List<int> list3;
										if (!dictionary2.TryGetValue(key3, out list3) || list3 == null)
										{
											list3 = (dictionary2[key3] = new List<int>());
										}
										else if (!list3.IsNullOrEmpty<int>())
										{
											list3.Clear();
										}
										list3.AddRange(list2);
										ListPool<int>.Release(list2);
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F86 RID: 16262 RVA: 0x0017F3F4 File Offset: 0x0017D7F4
			private void LoadEventKeyTable(string sheetAssetBundleName, string sheetAssetName, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>> eventKeyTable, bool awaitable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetAssetBundleName, sheetAssetName, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 2; i < excelData.MaxCell; i++)
				{
					ExcelData.Param param = excelData.list[i];
					int num = 2;
					string element = param.list.GetElement(num++);
					string element2 = param.list.GetElement(num++);
					int key;
					if (int.TryParse(element, out key))
					{
						int key2;
						if (int.TryParse(element2, out key2))
						{
							string element3 = param.list.GetElement(num++);
							int key3 = Animator.StringToHash(element3);
							Dictionary<int, Dictionary<int, List<AnimeEventInfo>>> dictionary;
							if (!eventKeyTable.TryGetValue(key, out dictionary))
							{
								Dictionary<int, Dictionary<int, List<AnimeEventInfo>>> dictionary2 = new Dictionary<int, Dictionary<int, List<AnimeEventInfo>>>();
								eventKeyTable[key] = dictionary2;
								dictionary = dictionary2;
							}
							Dictionary<int, List<AnimeEventInfo>> dictionary3;
							if (!dictionary.TryGetValue(key2, out dictionary3))
							{
								Dictionary<int, List<AnimeEventInfo>> dictionary4 = new Dictionary<int, List<AnimeEventInfo>>();
								dictionary[key2] = dictionary4;
								dictionary3 = dictionary4;
							}
							List<AnimeEventInfo> list;
							if (!dictionary3.TryGetValue(key3, out list) || list == null)
							{
								List<AnimeEventInfo> list2 = new List<AnimeEventInfo>();
								dictionary3[key3] = list2;
								list = list2;
							}
							else if (!list.IsNullOrEmpty<AnimeEventInfo>())
							{
								list.Clear();
							}
							string element4 = param.list.GetElement(num++);
							if (!element4.IsNullOrEmpty())
							{
								MatchCollection matchCollection = this._regex.Matches(element4);
								if (matchCollection.Count > 0)
								{
									for (int j = 0; j < matchCollection.Count; j++)
									{
										Match match = matchCollection[j];
										for (int k = 0; k < match.Groups[1].Captures.Count; k++)
										{
											string value = match.Groups[1].Captures[k].Value;
											string[] source = value.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
											int num2 = 0;
											float maxValue;
											if (!float.TryParse(source.GetElement(num2++), out maxValue))
											{
												maxValue = float.MaxValue;
											}
											int eventID;
											if (!int.TryParse(source.GetElement(num2++), out eventID))
											{
												eventID = -1;
											}
											list.Add(new AnimeEventInfo
											{
												normalizedTime = maxValue,
												eventID = eventID
											});
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F87 RID: 16263 RVA: 0x0017F65C File Offset: 0x0017DA5C
			public void LoadOnceVoiceEventKeyTable(string sheetAssetBundleName, string sheetAssetName, Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>> eventKeyTable, bool awaitable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetAssetBundleName, sheetAssetName, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 2; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 2;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string text = list.GetElement(num++) ?? string.Empty;
								if (!text.IsNullOrEmpty())
								{
									string text2 = list.GetElement(num++) ?? string.Empty;
									if (!text2.IsNullOrEmpty())
									{
										int key3 = Animator.StringToHash(text);
										Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>> dictionary;
										if (!eventKeyTable.TryGetValue(key, out dictionary))
										{
											dictionary = (eventKeyTable[key] = new Dictionary<int, Dictionary<int, List<AnimeOnceVoiceEventInfo>>>());
										}
										Dictionary<int, List<AnimeOnceVoiceEventInfo>> dictionary2;
										if (!dictionary.TryGetValue(key2, out dictionary2))
										{
											dictionary2 = (dictionary[key2] = new Dictionary<int, List<AnimeOnceVoiceEventInfo>>());
										}
										List<AnimeOnceVoiceEventInfo> list2;
										if (!dictionary2.TryGetValue(key3, out list2) || list2 == null)
										{
											list2 = (dictionary2[key3] = new List<AnimeOnceVoiceEventInfo>());
										}
										else if (!list2.IsNullOrEmpty<AnimeOnceVoiceEventInfo>())
										{
											list2.Clear();
										}
										MatchCollection matchCollection = this._regex.Matches(text2);
										if (0 < matchCollection.Count)
										{
											for (int j = 0; j < matchCollection.Count; j++)
											{
												Match match = matchCollection[j];
												for (int k = 0; k < match.Groups[1].Captures.Count; k++)
												{
													string value = match.Groups[1].Captures[k].Value;
													string[] array = value.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
													int l = 0;
													string s3 = array.GetElement(l++) ?? string.Empty;
													float maxValue;
													if (!float.TryParse(s3, out maxValue))
													{
														maxValue = float.MaxValue;
													}
													List<int> list3 = ListPool<int>.Get();
													while (l < array.Length)
													{
														string s4 = array.GetElement(l++) ?? string.Empty;
														int item;
														if (int.TryParse(s4, out item))
														{
															list3.Add(item);
														}
													}
													AnimeOnceVoiceEventInfo item2 = new AnimeOnceVoiceEventInfo
													{
														NormalizedTime = maxValue,
														EventIDs = list3.ToArray()
													};
													ListPool<int>.Release(list3);
													list2.Add(item2);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F88 RID: 16264 RVA: 0x0017F944 File Offset: 0x0017DD44
			private void LoadLoopVoiceEventKeyTable(string sheetAssetBundleName, string sheetAssetName, Dictionary<int, Dictionary<int, Dictionary<int, int>>> eventKeyTable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetAssetBundleName, sheetAssetName, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 2;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string element = list.GetElement(num++);
								if (!element.IsNullOrEmpty())
								{
									string s3 = list.GetElement(num++) ?? string.Empty;
									int value;
									if (int.TryParse(s3, out value))
									{
										int key3 = Animator.StringToHash(element);
										Dictionary<int, Dictionary<int, int>> dictionary;
										if (!eventKeyTable.TryGetValue(key, out dictionary))
										{
											dictionary = (eventKeyTable[key] = new Dictionary<int, Dictionary<int, int>>());
										}
										Dictionary<int, int> dictionary2;
										if (!dictionary.TryGetValue(key2, out dictionary2))
										{
											dictionary2 = (dictionary[key2] = new Dictionary<int, int>());
										}
										dictionary2[key3] = value;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F89 RID: 16265 RVA: 0x0017FA9C File Offset: 0x0017DE9C
			private void LoadAgentEventKeyTable(DefinePack definePack, bool awaitable)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentAnimeInfo, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text, 20))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (!text.IsNullOrEmpty() && !fileNameWithoutExtension.IsNullOrEmpty())
						{
							string sheetAsset = string.Format("footstepevent_{0}", fileNameWithoutExtension);
							this.LoadFootStepEventKeyTable(text, sheetAsset, this.AgentFootStepEventKeyTable, awaitable);
							string sheetAssetName = string.Format("event_se_{0}", fileNameWithoutExtension);
							this.LoadSEEventKeyTable(text, sheetAssetName, this.AgentActSEEventKeyTable, awaitable);
							string sheetAssetName2 = string.Format("event_particle_{0}", fileNameWithoutExtension);
							this.LoadParticleEventKeyTable(text, sheetAssetName2, this.AgentActParticleEventKeyTable, awaitable);
							string sheetAssetName3 = string.Format("event_voice_{0}", fileNameWithoutExtension);
							this.LoadOnceVoiceEventKeyTable(text, sheetAssetName3, this.AgentActOnceVoiceEventKeyTable, awaitable);
							string assetName = string.Format("event_voice_loop_{0}", fileNameWithoutExtension);
							this.LoadLoopVoiceEventKeyTable(text, assetName, this.AgentActLoopVoiceEventKeyTable, awaitable);
						}
					}
				}
			}

			// Token: 0x06003F8A RID: 16266 RVA: 0x0017FBB1 File Offset: 0x0017DFB1
			private void LoadPlayerEventKeyTable(DefinePack definePack, bool awaitable)
			{
				this.LoadPlayerEventKeyTable(definePack.ABDirectories.PlayerMaleAnimeInfo, 0, awaitable);
				this.LoadPlayerEventKeyTable(definePack.ABDirectories.PlayerFemaleAnimeInfo, 1, awaitable);
			}

			// Token: 0x06003F8B RID: 16267 RVA: 0x0017FBDC File Offset: 0x0017DFDC
			private void LoadPlayerEventKeyTable(string directory, int sex, bool awaitable)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(directory, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text, 20))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (!text.IsNullOrEmpty() && !fileNameWithoutExtension.IsNullOrEmpty())
						{
							string sheetAsset = string.Format("footstepevent_{0}", fileNameWithoutExtension);
							Dictionary<int, FootStepInfo[]> eventKeyTable;
							if (!this.PlayerFootStepEventKeyTable.TryGetValue(sex, out eventKeyTable))
							{
								eventKeyTable = (this.PlayerFootStepEventKeyTable[sex] = new Dictionary<int, FootStepInfo[]>());
							}
							this.LoadFootStepEventKeyTable(text, sheetAsset, eventKeyTable, awaitable);
							string sheetAssetName = string.Format("event_se_{0}", fileNameWithoutExtension);
							Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>> table;
							if (!this.PlayerActSEEventKeyTable.TryGetValue(sex, out table))
							{
								table = (this.PlayerActSEEventKeyTable[sex] = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeSEEventInfo>>>>());
							}
							this.LoadSEEventKeyTable(text, sheetAssetName, table, awaitable);
							string sheetAssetName2 = string.Format("event_particle_{0}", fileNameWithoutExtension);
							Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>> eventKeyTable2;
							if (!this.PlayerActParticleEventKeyTable.TryGetValue(sex, out eventKeyTable2))
							{
								eventKeyTable2 = (this.PlayerActParticleEventKeyTable[sex] = new Dictionary<int, Dictionary<int, Dictionary<int, List<AnimeParticleEventInfo>>>>());
							}
							this.LoadParticleEventKeyTable(text, sheetAssetName2, eventKeyTable2, awaitable);
						}
					}
				}
			}

			// Token: 0x06003F8C RID: 16268 RVA: 0x0017FD18 File Offset: 0x0017E118
			private void LoadChangeClothEventKeyTable(DefinePack definePack, bool awaitable)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentAnimeInfo, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text, 20))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (!text.IsNullOrEmpty() && !fileNameWithoutExtension.IsNullOrEmpty())
						{
							string sheetAssetName = string.Format("cloth_{0}", fileNameWithoutExtension);
							this.LoadEventKeyTable(text, sheetAssetName, this.AgentChangeClothEventKeyTable, false);
						}
					}
				}
			}

			// Token: 0x06003F8D RID: 16269 RVA: 0x0017FDE0 File Offset: 0x0017E1E0
			private void LoadSurpriseItemList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.SurpriseItemInfo, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (SurpriseItemData surpriseItemData in AssetBundleManager.LoadAllAsset(text, typeof(SurpriseItemData), null).GetAllAssets<SurpriseItemData>())
						{
							foreach (SurpriseItemData.Param param in surpriseItemData.param)
							{
								List<PlayState.ItemInfo> list;
								if (!this.SurpriseItemList.TryGetValue(param.Animator, out list))
								{
									List<PlayState.ItemInfo> list2 = new List<PlayState.ItemInfo>();
									this.SurpriseItemList[param.Animator] = list2;
									list = list2;
								}
								if (!param.ItemList.IsNullOrEmpty<string>())
								{
									foreach (string text2 in param.ItemList)
									{
										if (!text2.IsNullOrEmpty())
										{
											string[] source = text2.Split(Resources.AnimationTables._comma, StringSplitOptions.None);
											int num = 0;
											int num2;
											int itemID = (!int.TryParse(source.GetElement(num++), out num2)) ? -1 : num2;
											string element = source.GetElement(num++);
											bool flag;
											bool isSync = bool.TryParse(source.GetElement(num++), out flag) && flag;
											list.Add(new PlayState.ItemInfo
											{
												itemID = itemID,
												parentName = element,
												isSync = isSync
											});
										}
									}
								}
							}
						}
						Singleton<Resources>.Instance.AddLoadAssetBundle(text, string.Empty);
					}
				}
			}

			// Token: 0x06003F8E RID: 16270 RVA: 0x00180048 File Offset: 0x0017E448
			private void LoadMerchantActionState(List<string> address, string id, Dictionary<int, Dictionary<int, PlayState>> dic, bool awaitable)
			{
				int num = 2;
				string assetbundleName = address.GetElement(num++) ?? string.Empty;
				string assetName = address.GetElement(num++) ?? string.Empty;
				string manifestName = address.GetElement(num++) ?? string.Empty;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num2 = 0;
						string element = list.GetElement(num2++);
						if (!element.IsNullOrEmpty())
						{
							string assetbundleName2 = list.GetElement(num2++) ?? string.Empty;
							string assetName2 = list.GetElement(num2++) ?? string.Empty;
							string manifestName2 = list.GetElement(num2++) ?? string.Empty;
							ExcelData actionExcel = AssetUtility.LoadAsset<ExcelData>(assetbundleName2, assetName2, manifestName2);
							Resources.LoadActionAnimationInfo(actionExcel, dic, awaitable);
						}
					}
				}
			}

			// Token: 0x06003F8F RID: 16271 RVA: 0x00180188 File Offset: 0x0017E588
			private void LoadMerchantOnlyActionAnimState(List<string> address, string id, bool awaitable)
			{
				this.LoadMerchantActionState(address, id, this.MerchantOnlyActionAnimStateTable, awaitable);
			}

			// Token: 0x06003F90 RID: 16272 RVA: 0x00180199 File Offset: 0x0017E599
			private void LoadMerchantCommonActionState(List<string> address, string id, bool awaitable)
			{
				this.LoadMerchantActionState(address, id, this.MerchantCommonActionAnimStateTable, awaitable);
			}

			// Token: 0x06003F91 RID: 16273 RVA: 0x001801AC File Offset: 0x0017E5AC
			private void LoadMerchantLocomotionStateList(List<string> address, string id, bool awaitable)
			{
				int num = 2;
				string assetbundleName = address.GetElement(num++) ?? string.Empty;
				string assetName = address.GetElement(num++) ?? string.Empty;
				string manifestName = address.GetElement(num++) ?? string.Empty;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				this.LoadLocomotionStateList(excelData, this.MerchantLocomotionStateTable, awaitable);
			}

			// Token: 0x06003F92 RID: 16274 RVA: 0x0018021C File Offset: 0x0017E61C
			private void LoadMerchantMoveInfoList(List<string> address, string id, bool awaitable)
			{
				int num = 2;
				string text = address.GetElement(num++) ?? string.Empty;
				string text2 = address.GetElement(num++) ?? string.Empty;
				if (!AssetBundleCheck.IsFile(text, text2))
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, text2, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string element = list.GetElement(j++);
						int key = Animator.StringToHash(element);
						List<AnimeMoveInfo> list2;
						if (!this.MerchantMoveInfoTable.TryGetValue(key, out list2))
						{
							List<AnimeMoveInfo> list3 = new List<AnimeMoveInfo>();
							this.MerchantMoveInfoTable[key] = list3;
							list2 = list3;
						}
						while (j < list.Count)
						{
							string element2 = list.GetElement(j++);
							string element3 = list.GetElement(j++);
							string element4 = list.GetElement(j++);
							if (!element4.IsNullOrEmpty())
							{
								float start;
								float end;
								if (float.TryParse(element2, out start) && float.TryParse(element3, out end))
								{
									AnimeMoveInfo item = new AnimeMoveInfo
									{
										start = start,
										end = end,
										movePoint = element4
									};
									list2.Add(item);
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F93 RID: 16275 RVA: 0x001803B4 File Offset: 0x0017E7B4
			private void LoadMerchantEventKeyList(List<string> address, string id, bool awaitable)
			{
				int num = 2;
				string element = address.GetElement(num++);
				string element2 = address.GetElement(num++);
				if (!AssetBundleCheck.IsFile(element, element2))
				{
					return;
				}
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(element, element2, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						string s = list.GetElement(0) ?? string.Empty;
						int num2;
						if (int.TryParse(s, out num2))
						{
							int num3 = 2;
							string element3 = list.GetElement(num3++);
							string element4 = list.GetElement(num3++);
							if (!element3.IsNullOrEmpty() && !element4.IsNullOrEmpty())
							{
								switch (num2)
								{
								case 0:
									this.LoadEventKeyTable(element3, element4, this.MerchantOnlyItemEventKeyTable, awaitable);
									break;
								case 1:
									this.LoadEventKeyTable(element3, element4, this.MerchantCommonItemEventKeyTable, awaitable);
									break;
								case 2:
									this.LoadSEEventKeyTable(element3, element4, this.MerchantOnlySEEventKeyTable, awaitable);
									break;
								case 3:
									this.LoadSEEventKeyTable(element3, element4, this.MerchantCommonSEEventKeyTable, awaitable);
									break;
								case 4:
									this.LoadParticleEventKeyTable(element3, element4, this.MerchantOnlyParticleEventKeyTable, awaitable);
									break;
								case 5:
									this.LoadParticleEventKeyTable(element3, element4, this.MerchantCommonParticleEventKeyTable, awaitable);
									break;
								case 6:
									this.LoadOnceVoiceEventKeyTable(element3, element4, this.MerchantOnlyOnceVoiceEventKeyTable, awaitable);
									break;
								case 7:
									this.LoadOnceVoiceEventKeyTable(element3, element4, this.MerchantCommonOnceVoiceEventKeyTable, awaitable);
									break;
								case 8:
									this.LoadLoopVoiceEventKeyTable(element3, element4, this.MerchantOnlyLoopVoiceEventKeyTable, awaitable);
									break;
								case 9:
									this.LoadLoopVoiceEventKeyTable(element3, element4, this.MerchantCommonLoopVoiceEventKeyTable, awaitable);
									break;
								case 10:
									this.LoadFootStepEventKeyTable(element3, element4, this.MerchantFootStepEventKeyTable, awaitable);
									break;
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F94 RID: 16276 RVA: 0x001805D8 File Offset: 0x0017E9D8
			private void LoadMerchantListenerStateList(List<string> address, string id, bool awaitable)
			{
				int num = 2;
				string assetbundleName = address.GetElement(num++) ?? string.Empty;
				string assetName = address.GetElement(num++) ?? string.Empty;
				string manifestName = address.GetElement(num++) ?? string.Empty;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num2 = 0;
						string s = list.GetElement(num2++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							num2++;
							string s2 = list.GetElement(num2++) ?? string.Empty;
							int postureID;
							if (int.TryParse(s2, out postureID))
							{
								string s3 = list.GetElement(num2++) ?? string.Empty;
								int poseID;
								if (int.TryParse(s3, out poseID))
								{
									string s4 = list.GetElement(num2++) ?? string.Empty;
									int num3;
									int value = (!int.TryParse(s4, out num3)) ? 0 : num3;
									this.MerchantListenerRelationTable[key] = value;
									PoseKeyPair value2 = new PoseKeyPair
									{
										postureID = postureID,
										poseID = poseID
									};
									this.MerchantListenerStateTable[key] = value2;
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F95 RID: 16277 RVA: 0x00180790 File Offset: 0x0017EB90
			private void LoadItemScaleTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentAnimeInfo, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text, 20))
					{
						string assetName = string.Format("itemscale_{0}", System.IO.Path.GetFileNameWithoutExtension(text));
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, assetName, string.Empty);
						if (!(excelData == null))
						{
							int j = 1;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								int num = 1;
								string element = param.list.GetElement(num++);
								int key;
								if (int.TryParse(element, out key))
								{
									string element2 = param.list.GetElement(num++);
									int scaleType;
									if (int.TryParse(element2, out scaleType))
									{
										string element3 = param.list.GetElement(num++);
										string element4 = param.list.GetElement(num++);
										string element5 = param.list.GetElement(num++);
										float sthreshold;
										float mthreshold;
										float lthreshold;
										if (float.TryParse(element3, out sthreshold) && float.TryParse(element4, out mthreshold) && float.TryParse(element5, out lthreshold))
										{
											Resources.TriValues value = new Resources.TriValues
											{
												ScaleType = scaleType,
												SThreshold = sthreshold,
												MThreshold = mthreshold,
												LThreshold = lthreshold
											};
											this.ItemScaleTable[key] = value;
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003F96 RID: 16278 RVA: 0x00180944 File Offset: 0x0017ED44
			private void LoadLocomotionStateList(ExcelData excelData, Dictionary<int, PlayState> dic, bool awaitable)
			{
				if (excelData == null)
				{
					return;
				}
				int i = 1;
				while (i < excelData.MaxCell)
				{
					ExcelData.Param param = excelData.list[i++];
					if (!param.list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string element = param.list.GetElement(num++);
						int key;
						if (int.TryParse(element, out key))
						{
							num++;
							string element2 = param.list.GetElement(num++);
							string element3 = param.list.GetElement(num++);
							string element4 = param.list.GetElement(num++);
							string[] array = element4.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
							bool flag;
							bool enableFade = bool.TryParse(param.list.GetElement(num++), out flag) && flag;
							float num2;
							float fadeSecond = (!float.TryParse(param.list.GetElement(num++), out num2)) ? 0f : num2;
							string element5 = param.list.GetElement(num++);
							string[] array2 = element5.Split(Resources.AnimationTables._separators, StringSplitOptions.RemoveEmptyEntries);
							bool flag2;
							bool enableFade2 = bool.TryParse(param.list.GetElement(num++), out flag2) && flag2;
							float num3;
							float fadeSecond2 = (!float.TryParse(param.list.GetElement(num++), out num3)) ? 0f : num3;
							int num5;
							int num4 = (!int.TryParse(param.list.GetElement(num++), out num5)) ? 0 : num5;
							string element6 = param.list.GetElement(num++);
							int layer_ = (!int.TryParse(param.list.GetElement(num++), out num5)) ? 0 : num5;
							PlayState playState = new PlayState
							{
								Layer = num4
							};
							dic[key] = playState;
							PlayState playState2 = playState;
							playState2.MainStateInfo.AssetBundleInfo = new AssetBundleInfo(string.Empty, element2, element3, string.Empty);
							playState2.MainStateInfo.InStateInfo = new PlayState.AnimStateInfo();
							if (!array.IsNullOrEmpty<string>())
							{
								PlayState.Info[] array3 = new PlayState.Info[array.Length];
								playState2.MainStateInfo.InStateInfo.StateInfos = array3;
								PlayState.Info[] array4 = array3;
								for (int j = 0; j < array4.Length; j++)
								{
									array4[j] = new PlayState.Info(array[j], num4);
								}
							}
							playState2.MainStateInfo.OutStateInfo = new PlayState.AnimStateInfo();
							if (!array2.IsNullOrEmpty<string>())
							{
								PlayState.Info[] array3 = new PlayState.Info[array2.Length];
								playState2.MainStateInfo.OutStateInfo.StateInfos = array3;
								PlayState.Info[] array5 = array3;
								for (int k = 0; k < array5.Length; k++)
								{
									array5[k] = new PlayState.Info(array2[k], num4);
								}
							}
							playState2.MainStateInfo.InStateInfo.EnableFade = enableFade;
							playState2.MainStateInfo.InStateInfo.FadeSecond = fadeSecond;
							playState2.MainStateInfo.OutStateInfo.EnableFade = enableFade2;
							playState2.MainStateInfo.OutStateInfo.FadeSecond = fadeSecond2;
							playState2.MaskStateInfo = new PlayState.Info(element6, layer_);
						}
					}
				}
			}

			// Token: 0x04003BC8 RID: 15304
			private const int _appendBundleID = 20;

			// Token: 0x04003BC9 RID: 15305
			private Dictionary<int, AssetBundleInfo> _playerAnimatorAssetTable = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x04003BCA RID: 15306
			private Dictionary<int, AssetBundleInfo> _charaAnimatorAssetTable = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x04003BCB RID: 15307
			private Dictionary<int, AssetBundleInfo> _merchantAnimatorAssetTable = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x04003BCC RID: 15308
			private Dictionary<int, AssetBundleInfo> _itemAnimatorAssetTable = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x04003BFA RID: 15354
			private Regex _regex = new Regex("<((?:[\\w/.-]*[,]*)+)>");

			// Token: 0x04003BFB RID: 15355
			private static readonly string[] _separators = new string[]
			{
				"/",
				"／"
			};

			// Token: 0x04003BFC RID: 15356
			private static readonly char[] _comma = new char[]
			{
				','
			};

			// Token: 0x04003BFD RID: 15357
			private static readonly string[] _seEventSepa = new string[]
			{
				">"
			};

			// Token: 0x04003BFE RID: 15358
			private static readonly string[] _seEventRemoveStr = new string[]
			{
				"<",
				">"
			};

			// Token: 0x020008F3 RID: 2291
			private enum AnimatorLoadMap
			{
				// Token: 0x04003C00 RID: 15360
				Chara,
				// Token: 0x04003C01 RID: 15361
				Player,
				// Token: 0x04003C02 RID: 15362
				Merchant,
				// Token: 0x04003C03 RID: 15363
				Item
			}

			// Token: 0x020008F4 RID: 2292
			private enum AgentLoadingMap
			{
				// Token: 0x04003C05 RID: 15365
				Personal,
				// Token: 0x04003C06 RID: 15366
				Action,
				// Token: 0x04003C07 RID: 15367
				Move,
				// Token: 0x04003C08 RID: 15368
				EventKey,
				// Token: 0x04003C09 RID: 15369
				Locomotion,
				// Token: 0x04003C0A RID: 15370
				Common,
				// Token: 0x04003C0B RID: 15371
				Greet,
				// Token: 0x04003C0C RID: 15372
				TalkSpeaker,
				// Token: 0x04003C0D RID: 15373
				TalkListener,
				// Token: 0x04003C0E RID: 15374
				Animal
			}

			// Token: 0x020008F5 RID: 2293
			private enum PlayerLoadingMap
			{
				// Token: 0x04003C10 RID: 15376
				Action,
				// Token: 0x04003C11 RID: 15377
				Move,
				// Token: 0x04003C12 RID: 15378
				EventKey,
				// Token: 0x04003C13 RID: 15379
				Locomotion,
				// Token: 0x04003C14 RID: 15380
				Common
			}

			// Token: 0x020008F6 RID: 2294
			public enum CommonLoadMap
			{
				// Token: 0x04003C16 RID: 15382
				Stand,
				// Token: 0x04003C17 RID: 15383
				Chair,
				// Token: 0x04003C18 RID: 15384
				Sit,
				// Token: 0x04003C19 RID: 15385
				Sleep
			}
		}

		// Token: 0x020008F7 RID: 2295
		public class TriValues
		{
			// Token: 0x17000BD8 RID: 3032
			// (get) Token: 0x06003F99 RID: 16281 RVA: 0x00180D16 File Offset: 0x0017F116
			// (set) Token: 0x06003F9A RID: 16282 RVA: 0x00180D1E File Offset: 0x0017F11E
			public int ScaleType { get; set; }

			// Token: 0x17000BD9 RID: 3033
			// (get) Token: 0x06003F9B RID: 16283 RVA: 0x00180D27 File Offset: 0x0017F127
			// (set) Token: 0x06003F9C RID: 16284 RVA: 0x00180D2F File Offset: 0x0017F12F
			public float SThreshold { get; set; }

			// Token: 0x17000BDA RID: 3034
			// (get) Token: 0x06003F9D RID: 16285 RVA: 0x00180D38 File Offset: 0x0017F138
			// (set) Token: 0x06003F9E RID: 16286 RVA: 0x00180D40 File Offset: 0x0017F140
			public float MThreshold { get; set; }

			// Token: 0x17000BDB RID: 3035
			// (get) Token: 0x06003F9F RID: 16287 RVA: 0x00180D49 File Offset: 0x0017F149
			// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x00180D51 File Offset: 0x0017F151
			public float LThreshold { get; set; }
		}

		// Token: 0x020008F8 RID: 2296
		public class BehaviorTreeTables
		{
			// Token: 0x06003FA2 RID: 16290 RVA: 0x00180D84 File Offset: 0x0017F184
			public AgentBehaviorTree GetBehavior(Desire.ActionType actionType)
			{
				AgentBehaviorTree result;
				if (!this._behaviorTree.TryGetValue(actionType, out result))
				{
					return null;
				}
				return result;
			}

			// Token: 0x06003FA3 RID: 16291 RVA: 0x00180DA8 File Offset: 0x0017F1A8
			public MerchantBehaviorTree GetMerchantBehavior(Merchant.ActionType actionType)
			{
				MerchantBehaviorTree result;
				if (!this._merchantBehaviorTree.TryGetValue(actionType, out result))
				{
					return null;
				}
				return result;
			}

			// Token: 0x06003FA4 RID: 16292 RVA: 0x00180DCC File Offset: 0x0017F1CC
			public AgentBehaviorTree GetTutorialBehavior(AIProject.Definitions.Tutorial.ActionType actionType)
			{
				AgentBehaviorTree result;
				this._tutrialBehaviorTree.TryGetValue(actionType, out result);
				return result;
			}

			// Token: 0x06003FA5 RID: 16293 RVA: 0x00180DE9 File Offset: 0x0017F1E9
			public void Load(DefinePack definePack)
			{
				this.LoadAgentBehaviorTree(definePack);
				this.LoadMerchantBehaviorTree(definePack);
				this.LoadTutorialBehaviorTree(definePack);
			}

			// Token: 0x06003FA6 RID: 16294 RVA: 0x00180E00 File Offset: 0x0017F200
			public void Release()
			{
				this._behaviorTree.Clear();
				this._merchantBehaviorTree.Clear();
				this._tutrialBehaviorTree.Clear();
			}

			// Token: 0x06003FA7 RID: 16295 RVA: 0x00180E24 File Offset: 0x0017F224
			private void LoadAgentBehaviorTree(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.BehaviorTree, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
					ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
					if (!(excelData == null))
					{
						for (int j = 1; j < excelData.MaxCell; j++)
						{
							ExcelData.Param element = excelData.list.GetElement(j);
							if (element != null && !element.list.IsNullOrEmpty<string>())
							{
								int num = 0;
								Desire.ActionType key;
								if (Enum.TryParse<Desire.ActionType>(element.list[num++], out key))
								{
									num++;
									string assetbundleName = element.list[num++];
									string assetName = element.list[num++];
									GameObject gameObject = AssetUtility.LoadAsset<GameObject>(assetbundleName, assetName, string.Empty);
									if (!(gameObject == null))
									{
										this._behaviorTree[key] = gameObject.GetComponent<AgentBehaviorTree>();
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003FA8 RID: 16296 RVA: 0x00180F68 File Offset: 0x0017F368
			private void LoadMerchantBehaviorTree(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.MerchantBehaviorTree, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
					ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
					if (!(excelData == null))
					{
						for (int j = 1; j < excelData.MaxCell; j++)
						{
							List<string> list = excelData.list[j].list;
							if (!list.IsNullOrEmpty<string>())
							{
								Merchant.ActionType key;
								if (Enum.TryParse<Merchant.ActionType>(list.GetElement(0) ?? string.Empty, out key))
								{
									int num = 2;
									string assetbundleName = list.GetElement(num++) ?? string.Empty;
									string assetName = list.GetElement(num++) ?? string.Empty;
									string manifestName = list.GetElement(num++) ?? string.Empty;
									GameObject gameObject = AssetUtility.LoadAsset<GameObject>(assetbundleName, assetName, manifestName);
									MerchantBehaviorTree component = gameObject.GetComponent<MerchantBehaviorTree>();
									if (!(component == null))
									{
										this._merchantBehaviorTree[key] = component;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06003FA9 RID: 16297 RVA: 0x001810D4 File Offset: 0x0017F4D4
			private void LoadTutorialBehaviorTree(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.TutorialBehaviorTree, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
					ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
					if (!(excelData == null))
					{
						for (int j = 1; j < excelData.MaxCell; j++)
						{
							List<string> list = excelData.list[j].list;
							if (!list.IsNullOrEmpty<string>())
							{
								int num = 0;
								string value = list.GetElement(num++) ?? string.Empty;
								AIProject.Definitions.Tutorial.ActionType key;
								if (Enum.TryParse<AIProject.Definitions.Tutorial.ActionType>(value, out key))
								{
									num++;
									string assetbundleName = list.GetElement(num++) ?? string.Empty;
									string assetName = list.GetElement(num++) ?? string.Empty;
									GameObject gameObject = AssetUtility.LoadAsset<GameObject>(assetbundleName, assetName, string.Empty);
									AgentBehaviorTree agentBehaviorTree = (gameObject != null) ? gameObject.GetComponent<AgentBehaviorTree>() : null;
									if (!(agentBehaviorTree == null))
									{
										this._tutrialBehaviorTree[key] = agentBehaviorTree;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x04003C1E RID: 15390
			private Dictionary<Desire.ActionType, AgentBehaviorTree> _behaviorTree = new Dictionary<Desire.ActionType, AgentBehaviorTree>();

			// Token: 0x04003C1F RID: 15391
			private Dictionary<Merchant.ActionType, MerchantBehaviorTree> _merchantBehaviorTree = new Dictionary<Merchant.ActionType, MerchantBehaviorTree>();

			// Token: 0x04003C20 RID: 15392
			private Dictionary<AIProject.Definitions.Tutorial.ActionType, AgentBehaviorTree> _tutrialBehaviorTree = new Dictionary<AIProject.Definitions.Tutorial.ActionType, AgentBehaviorTree>();
		}

		// Token: 0x0200090D RID: 2317
		public class FishingTable
		{
			// Token: 0x17000C00 RID: 3072
			// (get) Token: 0x06004006 RID: 16390 RVA: 0x00181311 File Offset: 0x0017F711
			// (set) Token: 0x06004007 RID: 16391 RVA: 0x00181319 File Offset: 0x0017F719
			public Dictionary<int, RuntimeAnimatorController> PlayerAnimatorTable { get; private set; } = new Dictionary<int, RuntimeAnimatorController>();

			// Token: 0x17000C01 RID: 3073
			// (get) Token: 0x06004008 RID: 16392 RVA: 0x00181322 File Offset: 0x0017F722
			// (set) Token: 0x06004009 RID: 16393 RVA: 0x0018132A File Offset: 0x0017F72A
			public Dictionary<int, PlayState> PlayerAnimStateTable { get; private set; } = new Dictionary<int, PlayState>();

			// Token: 0x17000C02 RID: 3074
			// (get) Token: 0x0600400A RID: 16394 RVA: 0x00181333 File Offset: 0x0017F733
			// (set) Token: 0x0600400B RID: 16395 RVA: 0x0018133B File Offset: 0x0017F73B
			public Dictionary<string, List<Fishing.Schedule>> AnimEventScheduler { get; private set; } = new Dictionary<string, List<Fishing.Schedule>>();

			// Token: 0x17000C03 RID: 3075
			// (get) Token: 0x0600400C RID: 16396 RVA: 0x00181344 File Offset: 0x0017F744
			// (set) Token: 0x0600400D RID: 16397 RVA: 0x0018134C File Offset: 0x0017F74C
			public Dictionary<int, FishingRodInfo> RodInfos { get; private set; } = new Dictionary<int, FishingRodInfo>();

			// Token: 0x17000C04 RID: 3076
			// (get) Token: 0x0600400E RID: 16398 RVA: 0x00181355 File Offset: 0x0017F755
			// (set) Token: 0x0600400F RID: 16399 RVA: 0x0018135D File Offset: 0x0017F75D
			public Dictionary<int, System.Tuple<AssetBundleInfo, RuntimeAnimatorController, string>> FishBodyTable { get; private set; } = new Dictionary<int, System.Tuple<AssetBundleInfo, RuntimeAnimatorController, string>>();

			// Token: 0x17000C05 RID: 3077
			// (get) Token: 0x06004010 RID: 16400 RVA: 0x00181366 File Offset: 0x0017F766
			// (set) Token: 0x06004011 RID: 16401 RVA: 0x0018136E File Offset: 0x0017F76E
			public Dictionary<int, AssetBundleInfo> EffectTable { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C06 RID: 3078
			// (get) Token: 0x06004012 RID: 16402 RVA: 0x00181377 File Offset: 0x0017F777
			// (set) Token: 0x06004013 RID: 16403 RVA: 0x0018137F File Offset: 0x0017F77F
			public Dictionary<int, FishFoodInfo> FishFoodInfoTable { get; private set; } = new Dictionary<int, FishFoodInfo>();

			// Token: 0x17000C07 RID: 3079
			// (get) Token: 0x06004014 RID: 16404 RVA: 0x00181388 File Offset: 0x0017F788
			// (set) Token: 0x06004015 RID: 16405 RVA: 0x00181390 File Offset: 0x0017F790
			public Dictionary<int, Dictionary<int, float>> FishHitBaseRangeTable { get; private set; } = new Dictionary<int, Dictionary<int, float>>();

			// Token: 0x17000C08 RID: 3080
			// (get) Token: 0x06004016 RID: 16406 RVA: 0x00181399 File Offset: 0x0017F799
			// (set) Token: 0x06004017 RID: 16407 RVA: 0x001813A1 File Offset: 0x0017F7A1
			public Dictionary<int, Resources.FishBaitHitInfo> FishBaitHitInfoTable { get; private set; } = new Dictionary<int, Resources.FishBaitHitInfo>();

			// Token: 0x17000C09 RID: 3081
			// (get) Token: 0x06004018 RID: 16408 RVA: 0x001813AA File Offset: 0x0017F7AA
			// (set) Token: 0x06004019 RID: 16409 RVA: 0x001813B2 File Offset: 0x0017F7B2
			public Dictionary<int, Dictionary<int, FishInfo>> FishInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, FishInfo>>();

			// Token: 0x17000C0A RID: 3082
			// (get) Token: 0x0600401A RID: 16410 RVA: 0x001813BB File Offset: 0x0017F7BB
			// (set) Token: 0x0600401B RID: 16411 RVA: 0x001813C3 File Offset: 0x0017F7C3
			public Dictionary<int, Dictionary<int, FishInfo>> FishInfoSizeGroupTable { get; private set; } = new Dictionary<int, Dictionary<int, FishInfo>>();

			// Token: 0x17000C0B RID: 3083
			// (get) Token: 0x0600401C RID: 16412 RVA: 0x001813CC File Offset: 0x0017F7CC
			// (set) Token: 0x0600401D RID: 16413 RVA: 0x001813D4 File Offset: 0x0017F7D4
			public Dictionary<int, System.Tuple<GameObject, RuntimeAnimatorController>> FishModelTable { get; private set; } = new Dictionary<int, System.Tuple<GameObject, RuntimeAnimatorController>>();

			// Token: 0x17000C0C RID: 3084
			// (get) Token: 0x0600401E RID: 16414 RVA: 0x001813DD File Offset: 0x0017F7DD
			// (set) Token: 0x0600401F RID: 16415 RVA: 0x001813E5 File Offset: 0x0017F7E5
			public Dictionary<int, Dictionary<int, int>> FishSizeTable { get; private set; } = new Dictionary<int, Dictionary<int, int>>();

			// Token: 0x17000C0D RID: 3085
			// (get) Token: 0x06004020 RID: 16416 RVA: 0x001813EE File Offset: 0x0017F7EE
			// (set) Token: 0x06004021 RID: 16417 RVA: 0x001813F6 File Offset: 0x0017F7F6
			public float ResultFishReferenceExtent { get; private set; } = 1f;

			// Token: 0x06004022 RID: 16418 RVA: 0x00181400 File Offset: 0x0017F800
			public UnityEx.ValueTuple<int, int>[] GetItemIDInSizeTalbe(int _sizeID)
			{
				List<UnityEx.ValueTuple<int, int>> list = ListPool<UnityEx.ValueTuple<int, int>>.Get();
				foreach (KeyValuePair<int, Dictionary<int, int>> keyValuePair in this.FishSizeTable)
				{
					foreach (KeyValuePair<int, int> keyValuePair2 in keyValuePair.Value)
					{
						if (keyValuePair2.Value == _sizeID)
						{
							list.Add(new UnityEx.ValueTuple<int, int>(keyValuePair.Key, keyValuePair2.Key));
						}
					}
				}
				UnityEx.ValueTuple<int, int>[] array = new UnityEx.ValueTuple<int, int>[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = list[i];
				}
				ListPool<UnityEx.ValueTuple<int, int>>.Release(list);
				return array;
			}

			// Token: 0x06004023 RID: 16419 RVA: 0x00181510 File Offset: 0x0017F910
			public bool TryGetFishSize(int _categoryID, int _itemID, out int _sizeID)
			{
				Dictionary<int, int> dictionary;
				int num;
				if (this.FishSizeTable.TryGetValue(_categoryID, out dictionary) && dictionary.TryGetValue(_itemID, out num))
				{
					_sizeID = num;
					return true;
				}
				_sizeID = 0;
				return false;
			}

			// Token: 0x06004024 RID: 16420 RVA: 0x00181547 File Offset: 0x0017F947
			private string LogAssetBundleInfo(AssetBundleInfo _info)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}]", _info.assetbundle, _info.asset, _info.manifest);
			}

			// Token: 0x06004025 RID: 16421 RVA: 0x00181568 File Offset: 0x0017F968
			private string LogAssetBundleInfo(AssetBundleInfo _info, string _ver)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}] Ver[{3}]", new object[]
				{
					_info.assetbundle,
					_info.asset,
					_info.manifest,
					_ver
				});
			}

			// Token: 0x06004026 RID: 16422 RVA: 0x0018159C File Offset: 0x0017F99C
			private string LogAssetBundleInfo(AssetBundleInfo _info, string _ver, int _row, int _clm)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}] Ver[{3}] Row[{4}] Clm[{5}]", new object[]
				{
					_info.assetbundle,
					_info.asset,
					_info.manifest,
					_ver,
					_row,
					_clm
				});
			}

			// Token: 0x06004027 RID: 16423 RVA: 0x001815F0 File Offset: 0x0017F9F0
			private AssetBundleInfo GetAssetInfo(List<string> _address, ref int _idx, bool _addSummary)
			{
				string name_ = (!_addSummary) ? string.Empty : (_address.GetElement(_idx++) ?? string.Empty);
				return new AssetBundleInfo(name_, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty);
			}

			// Token: 0x06004028 RID: 16424 RVA: 0x00181680 File Offset: 0x0017FA80
			private void LoadExcelData(List<string> _address, ref int _idx, out AssetBundleInfo _info, out ExcelData _data, bool _addSummary)
			{
				string name_ = (!_addSummary) ? string.Empty : (_address.GetElement(_idx++) ?? string.Empty);
				_info = new AssetBundleInfo(name_, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty);
				_data = AssetUtility.LoadAsset<ExcelData>(_info);
			}

			// Token: 0x06004029 RID: 16425 RVA: 0x00181720 File Offset: 0x0017FB20
			public void Load(FishingDefinePack _fishingDefinePack)
			{
				this.mSizeFishCount = 0;
				this.ResultFishReferenceExtent = 1f;
				string fishingInfoListBundleDirectory = _fishingDefinePack.AssetBundleNames.FishingInfoListBundleDirectory;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(fishingInfoListBundleDirectory, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
											if (num2 != 0)
											{
												if (num2 != 1)
												{
													if (num2 == 2)
													{
														this.LoadFishingInfoList(assetInfo, fileNameWithoutExtension);
													}
												}
												else
												{
													this.LoadFishingAnimInfo(assetInfo, fileNameWithoutExtension);
												}
											}
											else
											{
												this.LoadFishingData(assetInfo, fileNameWithoutExtension);
											}
										}
									}
								}
							}
						}
					}
				}
				if (0 < this.mSizeFishCount)
				{
					Dictionary<int, FishInfo> dictionary;
					if (this.FishInfoSizeGroupTable.TryGetValue(1, out dictionary) && !dictionary.IsNullOrEmpty<int, FishInfo>())
					{
						List<int> list2 = ListPool<int>.Get();
						foreach (KeyValuePair<int, FishInfo> keyValuePair in dictionary)
						{
							if (!list2.Contains(keyValuePair.Value.ModelID))
							{
								list2.Add(keyValuePair.Value.ModelID);
							}
						}
						float num3 = 1f;
						int num4 = 0;
						foreach (int key in list2)
						{
							System.Tuple<GameObject, RuntimeAnimatorController> tuple;
							if (this.FishModelTable.TryGetValue(key, out tuple) && ((tuple != null) ? tuple.Item1 : null) != null)
							{
								Renderer componentInChildren = tuple.Item1.GetComponentInChildren<Renderer>(true);
								if (componentInChildren != null)
								{
									num3 += Mathf.Max(componentInChildren.bounds.extents.y, componentInChildren.bounds.extents.z);
									num4++;
								}
							}
						}
						if (0 < num4)
						{
							this.ResultFishReferenceExtent = num3 / (float)list2.Count;
						}
						ListPool<int>.Release(list2);
					}
				}
				else if (!this.FishModelTable.IsNullOrEmpty<int, System.Tuple<GameObject, RuntimeAnimatorController>>())
				{
					float num5 = 1f;
					int num6 = 0;
					foreach (KeyValuePair<int, System.Tuple<GameObject, RuntimeAnimatorController>> keyValuePair2 in this.FishModelTable)
					{
						System.Tuple<GameObject, RuntimeAnimatorController> value = keyValuePair2.Value;
						GameObject gameObject = (value != null) ? value.Item1 : null;
						Renderer renderer = (!(gameObject != null)) ? null : gameObject.GetComponentInChildren<Renderer>(true);
						if (renderer != null)
						{
							num5 += Mathf.Max(renderer.bounds.extents.y, renderer.bounds.extents.z);
							num6++;
						}
					}
					if (0 < num6)
					{
						this.ResultFishReferenceExtent = num5 / (float)num6;
					}
				}
			}

			// Token: 0x0600402A RID: 16426 RVA: 0x00181B3C File Offset: 0x0017FF3C
			public void Release()
			{
				this.PlayerAnimatorTable.Clear();
				this.PlayerAnimStateTable.Clear();
				this.AnimEventScheduler.Clear();
				this.RodInfos.Clear();
				this.FishBodyTable.Clear();
				this.EffectTable.Clear();
				this.FishFoodInfoTable.Clear();
				this.FishHitBaseRangeTable.Clear();
				this.FishBaitHitInfoTable.Clear();
				this.FishInfoTable.Clear();
				this.FishInfoSizeGroupTable.Clear();
				this.FishModelTable.Clear();
				this.FishSizeTable.Clear();
			}

			// Token: 0x0600402B RID: 16427 RVA: 0x00181BD8 File Offset: 0x0017FFD8
			private void LoadFishingAnimInfo(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int num2;
						if (int.TryParse(s, out num2))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
							if (num2 != 0)
							{
								if (num2 != 1)
								{
									if (num2 == 2)
									{
										this.LoadFishingAnimEvent(assetInfo, _ver);
									}
								}
								else
								{
									this.LoadFishingAnimState(assetInfo, _ver);
								}
							}
							else
							{
								this.LoadPlayerFishingAnimator(assetInfo, _ver);
							}
						}
					}
				}
			}

			// Token: 0x0600402C RID: 16428 RVA: 0x00181CB4 File Offset: 0x001800B4
			private void LoadPlayerFishingAnimator(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
							RuntimeAnimatorController runtimeAnimatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(assetInfo);
							if (!(runtimeAnimatorController == null))
							{
								this.PlayerAnimatorTable[key] = runtimeAnimatorController;
							}
						}
					}
				}
			}

			// Token: 0x0600402D RID: 16429 RVA: 0x00181D74 File Offset: 0x00180174
			private void LoadFishingAnimState(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				int index = 0;
				int index2 = 3;
				int index3 = 4;
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						string s = list.GetElement(index) ?? string.Empty;
						string text = list.GetElement(index2) ?? string.Empty;
						string[] array = text.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
						string s2 = list.GetElement(index3) ?? string.Empty;
						int key = 0;
						int layer = 0;
						if (!array.IsNullOrEmpty<string>() && int.TryParse(s, out key) && int.TryParse(s2, out layer))
						{
							PlayState value = new PlayState(layer, array, null);
							this.PlayerAnimStateTable[key] = value;
						}
					}
				}
			}

			// Token: 0x0600402E RID: 16430 RVA: 0x00181E7C File Offset: 0x0018027C
			private void LoadFishingAnimEvent(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				int index = 0;
				int index2 = 1;
				int index3 = 2;
				int index4 = 3;
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						string element = list.GetElement(index);
						string element2 = list.GetElement(index2);
						string element3 = list.GetElement(index3);
						string element4 = list.GetElement(index4);
						int eventID = 0;
						float startTime = 0f;
						if (!element.IsNullOrEmpty() && int.TryParse(element2, out eventID) && float.TryParse(element3, out startTime))
						{
							Fishing.Schedule item = new Fishing.Schedule(element, eventID, startTime, element4);
							List<Fishing.Schedule> list2 = null;
							if (!this.AnimEventScheduler.TryGetValue(element, out list2))
							{
								list2 = (this.AnimEventScheduler[element] = new List<Fishing.Schedule>());
							}
							list2.Add(item);
							this.AnimEventScheduler[element] = list2;
						}
					}
				}
			}

			// Token: 0x0600402F RID: 16431 RVA: 0x00181F9C File Offset: 0x0018039C
			private void LoadFishingData(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int num2;
						if (int.TryParse(s, out num2))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
							switch (num2)
							{
							case 0:
								this.LoadFishingParticle(assetInfo, _ver);
								break;
							case 1:
								this.LoadFishShadow(assetInfo, _ver);
								break;
							case 2:
								this.LoadFishingRod(assetInfo, _ver);
								break;
							case 3:
								this.LoadFishModel(assetInfo, _ver);
								break;
							}
						}
					}
				}
			}

			// Token: 0x06004030 RID: 16432 RVA: 0x00182084 File Offset: 0x00180484
			private void LoadFishingParticle(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
							if (!assetInfo.assetbundle.IsNullOrEmpty() && !assetInfo.asset.IsNullOrEmpty())
							{
								string value = list.GetElement(num++) ?? string.Empty;
								bool flag;
								if (bool.TryParse(value, out flag))
								{
									this.EffectTable[key] = assetInfo;
								}
							}
						}
					}
				}
			}

			// Token: 0x06004031 RID: 16433 RVA: 0x0018217C File Offset: 0x0018057C
			private void LoadFishShadow(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
							if (!assetInfo.assetbundle.IsNullOrEmpty() && !assetInfo.asset.IsNullOrEmpty())
							{
								AssetBundleInfo assetInfo2 = this.GetAssetInfo(list, ref num, false);
								if (!assetInfo2.assetbundle.IsNullOrEmpty() && !assetInfo2.asset.IsNullOrEmpty())
								{
									RuntimeAnimatorController runtimeAnimatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(assetInfo2);
									if (!(runtimeAnimatorController == null))
									{
										string item = list.GetElement(num++) ?? string.Empty;
										this.FishBodyTable[key] = new System.Tuple<AssetBundleInfo, RuntimeAnimatorController, string>(assetInfo, runtimeAnimatorController, item);
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004032 RID: 16434 RVA: 0x001822B8 File Offset: 0x001806B8
			private void LoadFishingRod(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, false);
							GameObject gameObject = CommonLib.LoadAsset<GameObject>(assetInfo.assetbundle, assetInfo.asset, false, assetInfo.manifest);
							if (!(gameObject == null))
							{
								Singleton<Resources>.Instance.AddLoadAssetBundle(assetInfo.assetbundle, assetInfo.manifest);
								AssetBundleInfo assetInfo2 = this.GetAssetInfo(list, ref num, false);
								RuntimeAnimatorController runtimeAnimatorController = AssetUtility.LoadAsset<RuntimeAnimatorController>(assetInfo2);
								if (!(runtimeAnimatorController == null))
								{
									string text = list.GetElement(num++) ?? string.Empty;
									if (!text.IsNullOrEmpty())
									{
										this.RodInfos[key] = new FishingRodInfo(gameObject, runtimeAnimatorController, text);
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004033 RID: 16435 RVA: 0x001823FC File Offset: 0x001807FC
			private void LoadFishModel(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, false);
							GameObject gameObject = CommonLib.LoadAsset<GameObject>(assetInfo.assetbundle, assetInfo.asset, false, assetInfo.manifest);
							if (!(gameObject == null))
							{
								Singleton<Resources>.Instance.AddLoadAssetBundle(assetInfo.assetbundle, assetInfo.manifest);
								AssetBundleInfo assetInfo2 = this.GetAssetInfo(list, ref num, false);
								RuntimeAnimatorController item = null;
								if (!assetInfo2.assetbundle.IsNullOrEmpty() && !assetInfo2.asset.IsNullOrEmpty())
								{
									item = AssetUtility.LoadAsset<RuntimeAnimatorController>(assetInfo2);
								}
								this.FishModelTable[key] = new System.Tuple<GameObject, RuntimeAnimatorController>(gameObject, item);
							}
						}
					}
				}
			}

			// Token: 0x06004034 RID: 16436 RVA: 0x00182528 File Offset: 0x00180928
			private void LoadFishingInfoList(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int num2;
						if (int.TryParse(s, out num2))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
							switch (num2)
							{
							case 0:
								this.LoadFishFoodInfo(assetInfo, _ver);
								break;
							case 1:
								this.LoadFishInfo(assetInfo, _ver);
								break;
							case 2:
								this.LoadFishHitBaseRange(assetInfo, _ver);
								break;
							case 3:
								this.LoadFishBaitHitInfoList(assetInfo);
								break;
							}
						}
					}
				}
			}

			// Token: 0x06004035 RID: 16437 RVA: 0x00182610 File Offset: 0x00180A10
			private void LoadFishFoodInfo(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int num2;
						if (int.TryParse(s, out num2))
						{
							string text = list.GetElement(num++) ?? string.Empty;
							if (!text.IsNullOrEmpty())
							{
								string s2 = list.GetElement(num++) ?? string.Empty;
								string s3 = list.GetElement(num++) ?? string.Empty;
								string s4 = list.GetElement(num++) ?? string.Empty;
								int num3 = (!int.TryParse(s2, out num3)) ? 0 : num3;
								int num4 = (!int.TryParse(s3, out num4)) ? 0 : num4;
								int num5 = (!int.TryParse(s4, out num5)) ? 0 : num5;
								this.FishFoodInfoTable[num2] = new FishFoodInfo(num2, text, num3, num4, num5);
							}
						}
					}
				}
			}

			// Token: 0x06004036 RID: 16438 RVA: 0x00182774 File Offset: 0x00180B74
			private void LoadFishInfo(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int num2;
						if (int.TryParse(s, out num2))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int num3;
							if (int.TryParse(s2, out num3))
							{
								string itemName = list.GetElement(num++) ?? string.Empty;
								string s3 = list.GetElement(num++) ?? string.Empty;
								int num4;
								if (int.TryParse(s3, out num4))
								{
									string s4 = list.GetElement(num++) ?? string.Empty;
									int modelID;
									if (int.TryParse(s4, out modelID))
									{
										string s5 = list.GetElement(num++) ?? string.Empty;
										int heartPoint;
										if (int.TryParse(s5, out heartPoint))
										{
											string s6 = list.GetElement(num++) ?? string.Empty;
											string s7 = list.GetElement(num++) ?? string.Empty;
											int num5;
											bool flag = int.TryParse(s6, out num5);
											int num6;
											bool flag2 = int.TryParse(s7, out num6);
											if (flag || flag2)
											{
												if (!flag)
												{
													num5 = num6;
												}
												else if (!flag2)
												{
													num6 = num5;
												}
												else
												{
													int num7 = Mathf.Min(num5, num6);
													int num8 = Mathf.Max(num5, num6);
													num5 = num7;
													num6 = num8;
												}
												string s8 = list.GetElement(num++) ?? string.Empty;
												int tankPointID;
												if (!int.TryParse(s8, out tankPointID))
												{
													tankPointID = 0;
												}
												string s9 = list.GetElement(num++) ?? string.Empty;
												float nicknameHeightOffset;
												if (!float.TryParse(s9, out nicknameHeightOffset))
												{
													nicknameHeightOffset = 0f;
												}
												FishInfo value = new FishInfo(num2, num3, itemName, num4, modelID, heartPoint, num5, num6, tankPointID, nicknameHeightOffset);
												Dictionary<int, FishInfo> dictionary;
												if (!this.FishInfoSizeGroupTable.TryGetValue(num4, out dictionary) || dictionary == null)
												{
													Dictionary<int, FishInfo> dictionary2 = new Dictionary<int, FishInfo>();
													this.FishInfoSizeGroupTable[num4] = dictionary2;
													dictionary = dictionary2;
												}
												dictionary[num3] = value;
												Dictionary<int, FishInfo> dictionary3;
												if (!this.FishInfoTable.TryGetValue(num2, out dictionary3) || dictionary3 == null)
												{
													dictionary3 = (this.FishInfoTable[num2] = new Dictionary<int, FishInfo>());
												}
												dictionary3[num3] = value;
												if (num4 == 1)
												{
													this.mSizeFishCount++;
												}
												Dictionary<int, int> dictionary4;
												if (!this.FishSizeTable.TryGetValue(num2, out dictionary4))
												{
													Dictionary<int, int> dictionary5 = new Dictionary<int, int>();
													this.FishSizeTable[num2] = dictionary5;
													dictionary4 = dictionary5;
												}
												dictionary4[num3] = num4;
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004037 RID: 16439 RVA: 0x00182A9C File Offset: 0x00180E9C
			private void LoadFishHitBaseRange(AssetBundleInfo _info, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string s3 = list.GetElement(num++) ?? string.Empty;
								int num2;
								if (int.TryParse(s3, out num2))
								{
									Dictionary<int, float> dictionary;
									if (!this.FishHitBaseRangeTable.TryGetValue(key, out dictionary))
									{
										Dictionary<int, float> dictionary2 = new Dictionary<int, float>();
										this.FishHitBaseRangeTable[key] = dictionary2;
										dictionary = dictionary2;
									}
									dictionary[key2] = (float)num2;
								}
							}
						}
					}
				}
			}

			// Token: 0x06004038 RID: 16440 RVA: 0x00182BB8 File Offset: 0x00180FB8
			private void LoadFishBaitHitInfoList(AssetBundleInfo _sheetInfo)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int baitID;
						if (int.TryParse(s, out baitID))
						{
							num++;
							string element = list.GetElement(num++);
							string element2 = list.GetElement(num++);
							string element3 = list.GetElement(num++);
							if (!element.IsNullOrEmpty() && !element2.IsNullOrEmpty())
							{
								AssetBundleInfo sheetInfo = new AssetBundleInfo(string.Empty, element, element2, element3);
								this.LoadBaitHitInfo(sheetInfo, baitID);
							}
						}
					}
				}
			}

			// Token: 0x06004039 RID: 16441 RVA: 0x00182CA4 File Offset: 0x001810A4
			private void LoadBaitHitInfo(AssetBundleInfo _sheetInfo, int _baitID)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				Resources.FishBaitHitInfo fishBaitHitInfo;
				if (!this.FishBaitHitInfoTable.TryGetValue(_baitID, out fishBaitHitInfo) || fishBaitHitInfo == null)
				{
					fishBaitHitInfo = (this.FishBaitHitInfoTable[_baitID] = new Resources.FishBaitHitInfo());
				}
				else
				{
					fishBaitHitInfo.HitList.Clear();
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int range;
						if (int.TryParse(s, out range))
						{
							List<UnityEx.ValueTuple<int, int, int>> list2 = ListPool<UnityEx.ValueTuple<int, int, int>>.Get();
							while (j < list.Count)
							{
								string s2 = list.GetElement(j++) ?? string.Empty;
								j++;
								string s3 = list.GetElement(j++) ?? string.Empty;
								string s4 = list.GetElement(j++) ?? string.Empty;
								int i2;
								int i3;
								int i4;
								if (int.TryParse(s2, out i2) && int.TryParse(s3, out i3) && int.TryParse(s4, out i4))
								{
									list2.Add(new UnityEx.ValueTuple<int, int, int>(i2, i3, i4));
								}
							}
							if (!list2.IsNullOrEmpty<UnityEx.ValueTuple<int, int, int>>())
							{
								Resources.FishBaitHitInfo.HitInfo item = default(Resources.FishBaitHitInfo.HitInfo);
								item.Range = range;
								item.FishList = new List<UnityEx.ValueTuple<int, int, int>>(list2);
								fishBaitHitInfo.HitList.Add(item);
							}
							ListPool<UnityEx.ValueTuple<int, int, int>>.Release(list2);
						}
					}
				}
			}

			// Token: 0x0600403A RID: 16442 RVA: 0x00182E58 File Offset: 0x00181258
			public bool TryGetBaitHitInfo(int baitID, out UnityEx.ValueTuple<int, int, int> hitInfo)
			{
				Resources.FishBaitHitInfo fishBaitHitInfo;
				if (!this.FishBaitHitInfoTable.TryGetValue(baitID, out fishBaitHitInfo) || fishBaitHitInfo == null || fishBaitHitInfo.HitList.IsNullOrEmpty<Resources.FishBaitHitInfo.HitInfo>())
				{
					hitInfo = new UnityEx.ValueTuple<int, int, int>(0, -1, -1);
					return false;
				}
				return fishBaitHitInfo.GetFishInfo(out hitInfo);
			}

			// Token: 0x04003C7B RID: 15483
			private int mSizeFishCount;

			// Token: 0x04003C7C RID: 15484
			private string[] separators = new string[]
			{
				"/",
				"／",
				","
			};
		}

		// Token: 0x0200090E RID: 2318
		public class FishBaitHitInfo
		{
			// Token: 0x17000C0E RID: 3086
			// (get) Token: 0x0600403C RID: 16444 RVA: 0x00182EB3 File Offset: 0x001812B3
			// (set) Token: 0x0600403D RID: 16445 RVA: 0x00182EBB File Offset: 0x001812BB
			public List<Resources.FishBaitHitInfo.HitInfo> HitList { get; private set; } = new List<Resources.FishBaitHitInfo.HitInfo>();

			// Token: 0x0600403E RID: 16446 RVA: 0x00182EC4 File Offset: 0x001812C4
			public bool GetFishInfo(out UnityEx.ValueTuple<int, int, int> _info)
			{
				if (this.HitList.IsNullOrEmpty<Resources.FishBaitHitInfo.HitInfo>())
				{
					_info = new UnityEx.ValueTuple<int, int, int>(0, -1, -1);
					return false;
				}
				int num = 0;
				foreach (Resources.FishBaitHitInfo.HitInfo hitInfo in this.HitList)
				{
					num += hitInfo.Range;
				}
				int num2 = UnityEngine.Random.Range(0, num);
				foreach (Resources.FishBaitHitInfo.HitInfo hitInfo2 in this.HitList)
				{
					if (num2 < hitInfo2.Range)
					{
						if (hitInfo2.FishList.IsNullOrEmpty<UnityEx.ValueTuple<int, int, int>>())
						{
							_info = new UnityEx.ValueTuple<int, int, int>(0, -1, -1);
							return false;
						}
						int num3 = 0;
						foreach (UnityEx.ValueTuple<int, int, int> valueTuple in hitInfo2.FishList)
						{
							num3 += valueTuple.Item1;
						}
						int num4 = UnityEngine.Random.Range(0, num3);
						foreach (UnityEx.ValueTuple<int, int, int> valueTuple2 in hitInfo2.FishList)
						{
							if (num4 < valueTuple2.Item1)
							{
								_info = valueTuple2;
								return true;
							}
							num4 -= valueTuple2.Item1;
						}
					}
					else
					{
						num2 -= hitInfo2.Range;
					}
				}
				_info = new UnityEx.ValueTuple<int, int, int>(0, -1, -1);
				return false;
			}

			// Token: 0x0200090F RID: 2319
			public struct HitInfo
			{
				// Token: 0x17000C0F RID: 3087
				// (get) Token: 0x0600403F RID: 16447 RVA: 0x001830E0 File Offset: 0x001814E0
				// (set) Token: 0x06004040 RID: 16448 RVA: 0x001830E8 File Offset: 0x001814E8
				public int Range { get; set; }

				// Token: 0x17000C10 RID: 3088
				// (get) Token: 0x06004041 RID: 16449 RVA: 0x001830F1 File Offset: 0x001814F1
				// (set) Token: 0x06004042 RID: 16450 RVA: 0x001830F9 File Offset: 0x001814F9
				public List<UnityEx.ValueTuple<int, int, int>> FishList { get; set; }
			}
		}

		// Token: 0x02000910 RID: 2320
		public class GameInfoTables
		{
			// Token: 0x17000C11 RID: 3089
			// (get) Token: 0x06004044 RID: 16452 RVA: 0x001831A6 File Offset: 0x001815A6
			// (set) Token: 0x06004045 RID: 16453 RVA: 0x001831AE File Offset: 0x001815AE
			public bool initialized { get; private set; }

			// Token: 0x17000C12 RID: 3090
			// (get) Token: 0x06004046 RID: 16454 RVA: 0x001831B7 File Offset: 0x001815B7
			private Dictionary<int, Dictionary<int, StuffItemInfo>> _itemTables { get; } = new Dictionary<int, Dictionary<int, StuffItemInfo>>();

			// Token: 0x17000C13 RID: 3091
			// (get) Token: 0x06004047 RID: 16455 RVA: 0x001831BF File Offset: 0x001815BF
			// (set) Token: 0x06004048 RID: 16456 RVA: 0x001831C7 File Offset: 0x001815C7
			private IReadOnlyDictionary<int, StuffItemInfo> _itemNameHashTables { get; set; }

			// Token: 0x17000C14 RID: 3092
			// (get) Token: 0x06004049 RID: 16457 RVA: 0x001831D0 File Offset: 0x001815D0
			private Dictionary<int, Dictionary<int, StuffItemInfo>> _systemItemTables { get; } = new Dictionary<int, Dictionary<int, StuffItemInfo>>();

			// Token: 0x0600404A RID: 16458 RVA: 0x001831D8 File Offset: 0x001815D8
			public int[] GetItemCategories()
			{
				return this._itemTables.Keys.ToArray<int>();
			}

			// Token: 0x0600404B RID: 16459 RVA: 0x001831EC File Offset: 0x001815EC
			public Dictionary<int, StuffItemInfo> GetItemTable(int category)
			{
				Dictionary<int, StuffItemInfo> dictionary;
				return this._itemTables.TryGetValue(category, out dictionary) ? dictionary : null;
			}

			// Token: 0x0600404C RID: 16460 RVA: 0x00183214 File Offset: 0x00181614
			public StuffItemInfo FindItemInfo(int nameHash)
			{
				StuffItemInfo stuffItemInfo;
				return this._itemNameHashTables.TryGetValue(nameHash, out stuffItemInfo) ? stuffItemInfo : null;
			}

			// Token: 0x0600404D RID: 16461 RVA: 0x0018323C File Offset: 0x0018163C
			public StuffItemInfo GetItem(int category, int id)
			{
				Dictionary<int, StuffItemInfo> itemTable = this.GetItemTable(category);
				if (itemTable == null)
				{
					return null;
				}
				StuffItemInfo stuffItemInfo;
				return itemTable.TryGetValue(id, out stuffItemInfo) ? stuffItemInfo : null;
			}

			// Token: 0x0600404E RID: 16462 RVA: 0x00183270 File Offset: 0x00181670
			public StuffItemInfo GetItem_System(int category, int id)
			{
				Dictionary<int, StuffItemInfo> dictionary;
				if (!this._systemItemTables.TryGetValue(category, out dictionary))
				{
					return null;
				}
				StuffItemInfo stuffItemInfo;
				return dictionary.TryGetValue(id, out stuffItemInfo) ? stuffItemInfo : null;
			}

			// Token: 0x17000C15 RID: 3093
			// (get) Token: 0x0600404F RID: 16463 RVA: 0x001832A7 File Offset: 0x001816A7
			// (set) Token: 0x06004050 RID: 16464 RVA: 0x001832AF File Offset: 0x001816AF
			public Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>> FoodParameterTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>>();

			// Token: 0x17000C16 RID: 3094
			// (get) Token: 0x06004051 RID: 16465 RVA: 0x001832B8 File Offset: 0x001816B8
			// (set) Token: 0x06004052 RID: 16466 RVA: 0x001832C0 File Offset: 0x001816C0
			public Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>> DrinkParameterTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, FoodParameterPacket>>>();

			// Token: 0x06004053 RID: 16467 RVA: 0x001832CC File Offset: 0x001816CC
			public bool CanEat(StuffItem item)
			{
				Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
				return (this.FoodParameterTable.TryGetValue(item.CategoryID, out dictionary) && dictionary.ContainsKey(item.ID)) || (this.DrinkParameterTable.TryGetValue(item.CategoryID, out dictionary) && dictionary.ContainsKey(item.ID));
			}

			// Token: 0x06004054 RID: 16468 RVA: 0x0018332C File Offset: 0x0018172C
			public bool CanEat(StuffItemInfo item)
			{
				Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
				return (this.FoodParameterTable.TryGetValue(item.CategoryID, out dictionary) && dictionary.ContainsKey(item.ID)) || (this.DrinkParameterTable.TryGetValue(item.CategoryID, out dictionary) && dictionary.ContainsKey(item.ID));
			}

			// Token: 0x17000C17 RID: 3095
			// (get) Token: 0x06004055 RID: 16469 RVA: 0x0018338C File Offset: 0x0018178C
			// (set) Token: 0x06004056 RID: 16470 RVA: 0x00183394 File Offset: 0x00181794
			public Dictionary<int, Dictionary<int, EquipEventItemInfo>> SearchEquipEventItemTable { get; private set; } = new Dictionary<int, Dictionary<int, EquipEventItemInfo>>();

			// Token: 0x17000C18 RID: 3096
			// (get) Token: 0x06004057 RID: 16471 RVA: 0x0018339D File Offset: 0x0018179D
			// (set) Token: 0x06004058 RID: 16472 RVA: 0x001833A5 File Offset: 0x001817A5
			public Dictionary<int, Dictionary<int, EquipEventItemInfo>> CommonEquipEventItemTable { get; private set; } = new Dictionary<int, Dictionary<int, EquipEventItemInfo>>();

			// Token: 0x17000C19 RID: 3097
			// (get) Token: 0x06004059 RID: 16473 RVA: 0x001833AE File Offset: 0x001817AE
			// (set) Token: 0x0600405A RID: 16474 RVA: 0x001833B6 File Offset: 0x001817B6
			public Dictionary<int, Dictionary<int, AccessoryItemInfo>> AccessoryItem { get; private set; } = new Dictionary<int, Dictionary<int, AccessoryItemInfo>>();

			// Token: 0x17000C1A RID: 3098
			// (get) Token: 0x0600405B RID: 16475 RVA: 0x001833BF File Offset: 0x001817BF
			// (set) Token: 0x0600405C RID: 16476 RVA: 0x001833C7 File Offset: 0x001817C7
			public Dictionary<int, Dictionary<int, RecyclingItemInfo>> RecyclingCreateableItemTable { get; private set; } = new Dictionary<int, Dictionary<int, RecyclingItemInfo>>();

			// Token: 0x17000C1B RID: 3099
			// (get) Token: 0x0600405D RID: 16477 RVA: 0x001833D0 File Offset: 0x001817D0
			// (set) Token: 0x0600405E RID: 16478 RVA: 0x001833D8 File Offset: 0x001817D8
			public List<RecyclingItemInfo> RecyclingCreateableItemList { get; private set; } = new List<RecyclingItemInfo>();

			// Token: 0x0600405F RID: 16479 RVA: 0x001833E1 File Offset: 0x001817E1
			public int[] GetAreaKeys()
			{
				return this._itemPickTables.Keys.ToArray<int>();
			}

			// Token: 0x06004060 RID: 16480 RVA: 0x001833F4 File Offset: 0x001817F4
			public Dictionary<int, ItemTableElement> GetItemTableInArea(int pointID)
			{
				if (this._itemPickTables == null)
				{
					return null;
				}
				Dictionary<int, ItemTableElement> result;
				if (!this._itemPickTables.TryGetValue(pointID, out result))
				{
					return null;
				}
				return result;
			}

			// Token: 0x06004061 RID: 16481 RVA: 0x00183424 File Offset: 0x00181824
			public Dictionary<int, ItemTableElement> GetItemTableInFrogPoint(int pointID)
			{
				if (this._frogItemTable.IsNullOrEmpty<int, Dictionary<int, ItemTableElement>>())
				{
					return null;
				}
				Dictionary<int, ItemTableElement> dictionary;
				return (!this._frogItemTable.TryGetValue(pointID, out dictionary)) ? null : dictionary;
			}

			// Token: 0x17000C1C RID: 3100
			// (get) Token: 0x06004062 RID: 16482 RVA: 0x0018345D File Offset: 0x0018185D
			public Dictionary<int, Dictionary<int, List<VendItemInfo>>> VendItemInfoTable { get; } = new Dictionary<int, Dictionary<int, List<VendItemInfo>>>();

			// Token: 0x17000C1D RID: 3101
			// (get) Token: 0x06004063 RID: 16483 RVA: 0x00183465 File Offset: 0x00181865
			// (set) Token: 0x06004064 RID: 16484 RVA: 0x0018346D File Offset: 0x0018186D
			public IReadOnlyDictionary<int, VendItemInfo> VendItemInfoSpecialTable { get; private set; }

			// Token: 0x17000C1E RID: 3102
			// (get) Token: 0x06004065 RID: 16485 RVA: 0x00183476 File Offset: 0x00181876
			public Resources.GameInfoTables.RecipeInfo recipe { get; } = new Resources.GameInfoTables.RecipeInfo();

			// Token: 0x06004066 RID: 16486 RVA: 0x00183480 File Offset: 0x00181880
			public AIProject.SaveData.Environment.PlantInfo GetPlantInfo(int nameHash)
			{
				HarvestDataInfo harvestDataInfo;
				if (!this.HarvestDataInfoTable.TryGetValue(nameHash, out harvestDataInfo) || harvestDataInfo == null)
				{
					return null;
				}
				StuffItem[] result = (from x in (((harvestDataInfo != null) ? harvestDataInfo.Get() : null) ?? new HarvestData.Data[0]).Select(delegate(HarvestData.Data x)
				{
					StuffItemInfo stuffItemInfo = this.FindItemInfo(x.nameHash);
					int? num = (stuffItemInfo != null) ? new int?(stuffItemInfo.CategoryID) : null;
					int category = (num == null) ? -1 : num.Value;
					int? num2 = (stuffItemInfo != null) ? new int?(stuffItemInfo.ID) : null;
					return new StuffItem(category, (num2 == null) ? -1 : num2.Value, x.stock);
				})
				where x.CategoryID != -1
				select x).ToArray<StuffItem>();
				return new AIProject.SaveData.Environment.PlantInfo(harvestDataInfo.nameHash, harvestDataInfo.Time, result);
			}

			// Token: 0x17000C1F RID: 3103
			// (get) Token: 0x06004067 RID: 16487 RVA: 0x0018350F File Offset: 0x0018190F
			// (set) Token: 0x06004068 RID: 16488 RVA: 0x00183517 File Offset: 0x00181917
			private IReadOnlyDictionary<int, HarvestDataInfo> HarvestDataInfoTable { get; set; }

			// Token: 0x06004069 RID: 16489 RVA: 0x00183520 File Offset: 0x00181920
			public Resources.GameInfoTables.AdvPresentItemInfo GetAdvPresentInfo(ChaControl chaControl)
			{
				ChaFileControl chaFile = chaControl.chaFile;
				Dictionary<int, int> flavorState = chaFile.gameinfo.flavorState;
				int personality = chaFile.parameter.personality;
				Dictionary<int, AgentAdvPresentInfo.Param> dictionary;
				if (!this.AgentAdvPresentInfoTable.TryGetValue(personality, out dictionary) && !this.AgentAdvPresentInfoTable.TryGetValue(0, out dictionary))
				{
					return null;
				}
				KeyValuePair<int, int> keyValuePair = (from v in flavorState
				orderby v.Value descending
				select v).FirstOrDefault<KeyValuePair<int, int>>();
				AgentAdvPresentInfo.Param param;
				if (!dictionary.TryGetValue(keyValuePair.Key, out param))
				{
					return null;
				}
				Dictionary<AgentAdvPresentInfo.ItemData, int> targetDict = param.ItemData.ToDictionary((AgentAdvPresentInfo.ItemData v) => v, (AgentAdvPresentInfo.ItemData v) => 100);
				StuffItemInfo stuffItemInfo = this.FindItemInfo(Illusion.Utils.ProbabilityCalclator.DetermineFromDict<AgentAdvPresentInfo.ItemData>(targetDict).nameHash);
				return (stuffItemInfo != null) ? new Resources.GameInfoTables.AdvPresentItemInfo(param.ItemID, stuffItemInfo) : null;
			}

			// Token: 0x17000C20 RID: 3104
			// (get) Token: 0x0600406A RID: 16490 RVA: 0x0018362B File Offset: 0x00181A2B
			// (set) Token: 0x0600406B RID: 16491 RVA: 0x00183633 File Offset: 0x00181A33
			private IReadOnlyDictionary<int, Dictionary<int, AgentAdvPresentInfo.Param>> AgentAdvPresentInfoTable { get; set; }

			// Token: 0x0600406C RID: 16492 RVA: 0x0018363C File Offset: 0x00181A3C
			public Resources.GameInfoTables.AdvPresentItemInfo GetAdvPresentBirthdayInfo(ChaControl chaControl)
			{
				ChaFileControl chaFile = chaControl.chaFile;
				int personality = chaFile.parameter.personality;
				Dictionary<int, AgentAdvPresentBirthdayInfo.Param> dictionary;
				if (!this.AgentAdvPresentBirthdayInfoTable.TryGetValue(personality, out dictionary) && !this.AgentAdvPresentBirthdayInfoTable.TryGetValue(0, out dictionary))
				{
					return null;
				}
				if (!dictionary.Any<KeyValuePair<int, AgentAdvPresentBirthdayInfo.Param>>())
				{
					return null;
				}
				AgentAdvPresentBirthdayInfo.Param param = dictionary.Values.Shuffle<AgentAdvPresentBirthdayInfo.Param>().First<AgentAdvPresentBirthdayInfo.Param>();
				Dictionary<AgentAdvPresentBirthdayInfo.ItemData, int> targetDict = param.ItemData.ToDictionary((AgentAdvPresentBirthdayInfo.ItemData v) => v, (AgentAdvPresentBirthdayInfo.ItemData v) => 100);
				StuffItemInfo stuffItemInfo = this.FindItemInfo(Illusion.Utils.ProbabilityCalclator.DetermineFromDict<AgentAdvPresentBirthdayInfo.ItemData>(targetDict).nameHash);
				return (stuffItemInfo != null) ? new Resources.GameInfoTables.AdvPresentItemInfo(param.ItemID, stuffItemInfo) : null;
			}

			// Token: 0x17000C21 RID: 3105
			// (get) Token: 0x0600406D RID: 16493 RVA: 0x00183717 File Offset: 0x00181B17
			// (set) Token: 0x0600406E RID: 16494 RVA: 0x0018371F File Offset: 0x00181B1F
			private IReadOnlyDictionary<int, Dictionary<int, AgentAdvPresentBirthdayInfo.Param>> AgentAdvPresentBirthdayInfoTable { get; set; }

			// Token: 0x0600406F RID: 16495 RVA: 0x00183728 File Offset: 0x00181B28
			public System.Tuple<StuffItemInfo, int> GetAdvScroungeInfo(ChaControl chaControl)
			{
				ChaFileControl chaFile = chaControl.chaFile;
				int personality = chaFile.parameter.personality;
				Dictionary<int, AgentAdvScroungeInfo.Param> dictionary;
				if (!this.AgentAdvScroungeInfoTable.TryGetValue(personality, out dictionary) && !this.AgentAdvScroungeInfoTable.TryGetValue(0, out dictionary))
				{
					return null;
				}
				if (!dictionary.Any<KeyValuePair<int, AgentAdvScroungeInfo.Param>>())
				{
					return null;
				}
				Dictionary<AgentAdvScroungeInfo.ItemData, int> targetDict = dictionary.Values.Shuffle<AgentAdvScroungeInfo.Param>().First<AgentAdvScroungeInfo.Param>().ItemData.ToDictionary((AgentAdvScroungeInfo.ItemData v) => v, (AgentAdvScroungeInfo.ItemData v) => 100);
				AgentAdvScroungeInfo.ItemData itemData = Illusion.Utils.ProbabilityCalclator.DetermineFromDict<AgentAdvScroungeInfo.ItemData>(targetDict);
				return Tuple.Create<StuffItemInfo, int>(this.FindItemInfo(itemData.nameHash), itemData.sum);
			}

			// Token: 0x17000C22 RID: 3106
			// (get) Token: 0x06004070 RID: 16496 RVA: 0x001837F3 File Offset: 0x00181BF3
			// (set) Token: 0x06004071 RID: 16497 RVA: 0x001837FB File Offset: 0x00181BFB
			private IReadOnlyDictionary<int, Dictionary<int, AgentAdvScroungeInfo.Param>> AgentAdvScroungeInfoTable { get; set; }

			// Token: 0x06004072 RID: 16498 RVA: 0x00183804 File Offset: 0x00181C04
			public IReadOnlyDictionary<string, AgentAdvEventInfo.Param> GetAgentAdvEvents(AgentActor agent)
			{
				int personality = agent.ChaControl.fileParam.personality;
				Dictionary<string, AgentAdvEventInfo.Param> result;
				if (!this.AgentAdvEventInfoTable.TryGetValue(personality, out result))
				{
					return new Dictionary<string, AgentAdvEventInfo.Param>();
				}
				return result;
			}

			// Token: 0x17000C23 RID: 3107
			// (get) Token: 0x06004073 RID: 16499 RVA: 0x0018383C File Offset: 0x00181C3C
			// (set) Token: 0x06004074 RID: 16500 RVA: 0x00183844 File Offset: 0x00181C44
			private IReadOnlyDictionary<int, Dictionary<string, AgentAdvEventInfo.Param>> AgentAdvEventInfoTable { get; set; }

			// Token: 0x06004075 RID: 16501 RVA: 0x00183850 File Offset: 0x00181C50
			public List<string> GetAgentRandEvents(AgentActor agent)
			{
				int personality = agent.ChaControl.fileParam.personality;
				List<string> result;
				if (!this.AgentRandAdvEventTable.TryGetValue(personality, out result))
				{
					return new List<string>();
				}
				return result;
			}

			// Token: 0x17000C24 RID: 3108
			// (get) Token: 0x06004076 RID: 16502 RVA: 0x00183888 File Offset: 0x00181C88
			// (set) Token: 0x06004077 RID: 16503 RVA: 0x00183890 File Offset: 0x00181C90
			private IReadOnlyDictionary<int, List<string>> AgentRandAdvEventTable { get; set; }

			// Token: 0x06004078 RID: 16504 RVA: 0x0018389C File Offset: 0x00181C9C
			public System.Tuple<StuffItemInfo, int> GetAdvRandomEventItemInfo(int ID)
			{
				AgentRandomEventItemInfo.Param param;
				if (!this.AgentRandomEventItemInfoTable.TryGetValue(ID, out param))
				{
					return null;
				}
				if (!param.ItemData.Any<AgentRandomEventItemInfo.ItemData>())
				{
					return null;
				}
				Dictionary<AgentRandomEventItemInfo.ItemData, int> targetDict = param.ItemData.Shuffle<AgentRandomEventItemInfo.ItemData>().ToDictionary((AgentRandomEventItemInfo.ItemData v) => v, (AgentRandomEventItemInfo.ItemData v) => 100);
				AgentRandomEventItemInfo.ItemData itemData = Illusion.Utils.ProbabilityCalclator.DetermineFromDict<AgentRandomEventItemInfo.ItemData>(targetDict);
				return Tuple.Create<StuffItemInfo, int>(this.FindItemInfo(itemData.nameHash), itemData.sum);
			}

			// Token: 0x17000C25 RID: 3109
			// (get) Token: 0x06004079 RID: 16505 RVA: 0x00183939 File Offset: 0x00181D39
			// (set) Token: 0x0600407A RID: 16506 RVA: 0x00183941 File Offset: 0x00181D41
			private IReadOnlyDictionary<int, AgentRandomEventItemInfo.Param> AgentRandomEventItemInfoTable { get; set; }

			// Token: 0x17000C26 RID: 3110
			// (get) Token: 0x0600407B RID: 16507 RVA: 0x0018394A File Offset: 0x00181D4A
			// (set) Token: 0x0600407C RID: 16508 RVA: 0x00183952 File Offset: 0x00181D52
			public IReadOnlyDictionary<int, LifeStyleData.Param> AgentLifeStyleInfoTable { get; private set; }

			// Token: 0x0600407D RID: 16509 RVA: 0x0018395C File Offset: 0x00181D5C
			public void Load(DefinePack definePack)
			{
				this.LoadItemList();
				this.LoadItemList_System();
				this.LoadFoodParameterTable(definePack);
				this.LoadDrinkParameterTable(definePack);
				this.LoadItemSearchTable(definePack);
				this.LoadFrogItemTable(definePack);
				this.recipe.Load();
				this.LoadSearchEquipItemList(definePack);
				this.LoadCommonEquipItemList(definePack);
				this.LoadAccessoryItem(definePack);
				this.LoadVendItemList();
				this.LoadSpecialVendItemList();
				this.LoadHarvest();
				this.LoadAgentAdvPresentInfoTable();
				this.LoadAgentAdvPresentBirthdayInfoTable();
				this.LoadAgentAdvScroungeInfoTable();
				this.LoadAgentAdvEventInfoTable();
				this.LoadAgentLifeStyleInfoTable();
				this.LoadRecyclingInfo(definePack);
				this.LoadAgentRandomEventInfoTable();
				this.LoadAgentRandomEventItemInfoTable();
				this.initialized = true;
			}

			// Token: 0x0600407E RID: 16510 RVA: 0x001839FC File Offset: 0x00181DFC
			public void Release()
			{
				this._itemTables.Clear();
				this._systemItemTables.Clear();
				this.FoodParameterTable.Clear();
				this.DrinkParameterTable.Clear();
				this._itemPickTables.Clear();
				this._frogItemTable.Clear();
				this.SearchEquipEventItemTable.Clear();
				this.CommonEquipEventItemTable.Clear();
				this.AccessoryItem.Clear();
				this.VendItemInfoTable.Clear();
				this.VendItemInfoSpecialTable = null;
				this.recipe.Release();
				this.HarvestDataInfoTable = null;
				this.AgentAdvPresentInfoTable = null;
				this.AgentAdvPresentBirthdayInfoTable = null;
				this.AgentAdvScroungeInfoTable = null;
				this.AgentAdvEventInfoTable = null;
				this.RecyclingCreateableItemTable.Clear();
				this.RecyclingCreateableItemList.Clear();
				this.initialized = false;
			}

			// Token: 0x0600407F RID: 16511 RVA: 0x00183ACC File Offset: 0x00181ECC
			private void LoadItemList()
			{
				Dictionary<int, System.Tuple<string, Sprite>> categoryIcon = Singleton<Resources>.Instance.itemIconTables.CategoryIcon;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/gameitem/info/item/itemlist/", true);
				assetBundleNameListFromPath.Sort();
				Dictionary<int, Dictionary<int, ItemData.Param>> dictionary = new Dictionary<int, Dictionary<int, ItemData.Param>>();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ItemData itemData in AssetBundleManager.LoadAllAsset(text, typeof(ItemData), null).GetAllAssets<ItemData>())
						{
							int key;
							if (int.TryParse(itemData.name, out key))
							{
								if (categoryIcon.ContainsKey(key))
								{
									Dictionary<int, ItemData.Param> dictionary2;
									if (!dictionary.TryGetValue(key, out dictionary2))
									{
										dictionary2 = (dictionary[key] = new Dictionary<int, ItemData.Param>());
									}
									foreach (ItemData.Param param in itemData.param)
									{
										dictionary2[param.ID] = param;
									}
								}
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				foreach (KeyValuePair<int, Dictionary<int, ItemData.Param>> keyValuePair in dictionary)
				{
					int key2 = keyValuePair.Key;
					Dictionary<int, StuffItemInfo> dictionary3 = new Dictionary<int, StuffItemInfo>();
					foreach (KeyValuePair<int, ItemData.Param> keyValuePair2 in keyValuePair.Value)
					{
						StuffItemInfo value = new StuffItemInfo(key2, keyValuePair2.Value, false)
						{
							ReactionType = -1,
							IsAvailableHeroine = false,
							EnnuiAddition = new ThresholdInt(0, 1),
							TasteAdditionNormal = new ThresholdInt(0, 1),
							TasteAdditionEnnui = new ThresholdInt(0, 1),
							EquipableState = ItemEquipableState.Impossible
						};
						dictionary3[keyValuePair2.Key] = value;
					}
					this._itemTables[key2] = dictionary3;
				}
				this._itemNameHashTables = this._itemTables.Values.SelectMany((Dictionary<int, StuffItemInfo> x) => from y in x
				select y.Value).ToDictionary((StuffItemInfo v) => v.nameHash, (StuffItemInfo v) => v);
			}

			// Token: 0x06004080 RID: 16512 RVA: 0x00183DE8 File Offset: 0x001821E8
			private void LoadItemList_System()
			{
				Dictionary<int, System.Tuple<string, Sprite>> categoryIcon = Singleton<Resources>.Instance.itemIconTables.CategoryIcon;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/gameitem/info/item/itemlist_system/", true);
				assetBundleNameListFromPath.Sort();
				Dictionary<int, Dictionary<int, ItemData_System.Param>> dictionary = new Dictionary<int, Dictionary<int, ItemData_System.Param>>();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ItemData_System itemData_System in AssetBundleManager.LoadAllAsset(text, typeof(ItemData_System), null).GetAllAssets<ItemData_System>())
						{
							int key;
							if (int.TryParse(itemData_System.name, out key))
							{
								Dictionary<int, ItemData_System.Param> dictionary2;
								if (!dictionary.TryGetValue(key, out dictionary2))
								{
									dictionary2 = (dictionary[key] = new Dictionary<int, ItemData_System.Param>());
								}
								foreach (ItemData_System.Param param in itemData_System.param)
								{
									dictionary2[param.ID] = param;
								}
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				foreach (KeyValuePair<int, Dictionary<int, ItemData_System.Param>> keyValuePair in dictionary)
				{
					int key2 = keyValuePair.Key;
					Dictionary<int, StuffItemInfo> dictionary3 = new Dictionary<int, StuffItemInfo>();
					foreach (KeyValuePair<int, ItemData_System.Param> keyValuePair2 in keyValuePair.Value)
					{
						dictionary3[keyValuePair2.Key] = ItemData_System.Convert(key2, keyValuePair2.Value);
					}
					this._systemItemTables[key2] = dictionary3;
				}
			}

			// Token: 0x06004081 RID: 16513 RVA: 0x00184004 File Offset: 0x00182404
			private void LoadFoodParameterTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.FoodInfo, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ExcelData excelData in AssetBundleManager.LoadAllAsset(text, typeof(ExcelData), null).GetAllAssets<ExcelData>())
						{
							foreach (ExcelData.Param param in excelData.list)
							{
								int j = 1;
								string element = param.list.GetElement(j++);
								int key;
								if (int.TryParse(element, out key))
								{
									Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
									if (!this.FoodParameterTable.TryGetValue(key, out dictionary))
									{
										Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary2 = new Dictionary<int, Dictionary<int, FoodParameterPacket>>();
										this.FoodParameterTable[key] = dictionary2;
										dictionary = dictionary2;
									}
									string element2 = param.list.GetElement(j++);
									int key2;
									if (int.TryParse(element2, out key2))
									{
										Dictionary<int, FoodParameterPacket> dictionary3;
										if (!dictionary.TryGetValue(key2, out dictionary3))
										{
											Dictionary<int, FoodParameterPacket> dictionary4 = new Dictionary<int, FoodParameterPacket>();
											dictionary[key2] = dictionary4;
											dictionary3 = dictionary4;
										}
										string element3 = param.list.GetElement(j++);
										int key3;
										if (int.TryParse(element3, out key3))
										{
											int num2;
											int num = (!int.TryParse(param.list.GetElement(j++), out num2)) ? 0 : num2;
											float num3;
											float minValue = (!float.TryParse(param.list.GetElement(j++), out num3)) ? 0f : num3;
											float maxValue = (!float.TryParse(param.list.GetElement(j++), out num3)) ? 0f : num3;
											float minValue2 = (!float.TryParse(param.list.GetElement(j++), out num3)) ? 0f : num3;
											float maxValue2 = (!float.TryParse(param.list.GetElement(j++), out num3)) ? 0f : num3;
											int num4 = (!int.TryParse(param.list.GetElement(j++), out num2)) ? 0 : num2;
											FoodParameterPacket foodParameterPacket = new FoodParameterPacket
											{
												Probability = (float)num,
												SatiationAscentThreshold = new Threshold(minValue, maxValue),
												SatiationDescentThreshold = new Threshold(minValue2, maxValue2),
												StomachacheRate = (float)num4
											};
											while (j < param.list.Count)
											{
												string element4 = param.list.GetElement(j++);
												string element5 = param.list.GetElement(j++);
												string element6 = param.list.GetElement(j++);
												string element7 = param.list.GetElement(j++);
												if (!element4.IsNullOrEmpty())
												{
													int key4;
													if (Resources.StatusTagTable.TryGetValue(element4, out key4))
													{
														int s = (!int.TryParse(element5, out num2)) ? 0 : num2;
														int m = (!int.TryParse(element6, out num2)) ? 0 : num2;
														int l = (!int.TryParse(element7, out num2)) ? 0 : num2;
														foodParameterPacket.Parameters[key4] = new TriThreshold(s, m, l);
													}
												}
											}
											dictionary3[key3] = foodParameterPacket;
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004082 RID: 16514 RVA: 0x00184428 File Offset: 0x00182828
			private void LoadDrinkParameterTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.DrinkInfo, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (ExcelData excelData in AssetBundleManager.LoadAllAsset(text, typeof(ExcelData), null).GetAllAssets<ExcelData>())
						{
							foreach (ExcelData.Param param in excelData.list)
							{
								int j = 1;
								string element = param.list.GetElement(j++);
								int key;
								if (int.TryParse(element, out key))
								{
									Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary;
									if (!this.DrinkParameterTable.TryGetValue(key, out dictionary))
									{
										Dictionary<int, Dictionary<int, FoodParameterPacket>> dictionary2 = new Dictionary<int, Dictionary<int, FoodParameterPacket>>();
										this.DrinkParameterTable[key] = dictionary2;
										dictionary = dictionary2;
									}
									string element2 = param.list.GetElement(j++);
									int key2;
									if (int.TryParse(element2, out key2))
									{
										Dictionary<int, FoodParameterPacket> dictionary3;
										if (!dictionary.TryGetValue(key2, out dictionary3))
										{
											Dictionary<int, FoodParameterPacket> dictionary4 = new Dictionary<int, FoodParameterPacket>();
											dictionary[key2] = dictionary4;
											dictionary3 = dictionary4;
										}
										string element3 = param.list.GetElement(j++);
										int key3;
										if (int.TryParse(element3, out key3))
										{
											int num2;
											int num = (!int.TryParse(param.list.GetElement(j++), out num2)) ? 0 : num2;
											int num3 = (!int.TryParse(param.list.GetElement(j++), out num2)) ? 0 : num2;
											FoodParameterPacket foodParameterPacket = new FoodParameterPacket
											{
												Probability = (float)num,
												StomachacheRate = (float)num3
											};
											while (j < param.list.Count)
											{
												string element4 = param.list.GetElement(j++);
												string element5 = param.list.GetElement(j++);
												string element6 = param.list.GetElement(j++);
												string element7 = param.list.GetElement(j++);
												if (!element4.IsNullOrEmpty())
												{
													int key4;
													if (Resources.StatusTagTable.TryGetValue(element4, out key4))
													{
														int s = (!int.TryParse(element5, out num2)) ? 0 : num2;
														int m = (!int.TryParse(element6, out num2)) ? 0 : num2;
														int l = (!int.TryParse(element7, out num2)) ? 0 : num2;
														foodParameterPacket.Parameters[key4] = new TriThreshold(s, m, l);
													}
												}
											}
											dictionary3[key3] = foodParameterPacket;
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004083 RID: 16515 RVA: 0x00184778 File Offset: 0x00182B78
			private void LoadItemSearchTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.GatheringTable, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (GatherItemData gatherItemData in AssetBundleManager.LoadAllAsset(text, typeof(GatherItemData), null).GetAllAssets<GatherItemData>())
						{
							int key;
							if (int.TryParse(gatherItemData.name, out key))
							{
								Dictionary<int, ItemTableElement> dictionary;
								if (!this._itemPickTables.TryGetValue(key, out dictionary))
								{
									Dictionary<int, ItemTableElement> dictionary2 = new Dictionary<int, ItemTableElement>();
									this._itemPickTables[key] = dictionary2;
									dictionary = dictionary2;
								}
								foreach (GatherItemData.Param param in gatherItemData.param)
								{
									ItemTableElement itemTableElement = new ItemTableElement
									{
										Rate = param.Rate
									};
									dictionary[param.ID] = itemTableElement;
									ItemTableElement itemTableElement2 = itemTableElement;
									if (param.CategoryID_0 > 0)
									{
										string name = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary3;
										StuffItemInfo stuffItemInfo;
										if (this._itemTables.TryGetValue(param.CategoryID_0, out dictionary3) && dictionary3.TryGetValue(param.ItemID_0, out stuffItemInfo))
										{
											name = stuffItemInfo.Name;
										}
										ItemTableElement.GatherElement item = new ItemTableElement.GatherElement
										{
											name = name,
											prob = param.Probability_0 / 100f,
											categoryID = param.CategoryID_0,
											itemID = param.ItemID_0,
											minCount = param.Min_0,
											maxCount = param.Max_0
										};
										itemTableElement2.Elements.Add(item);
									}
									if (param.CategoryID_1 > 0)
									{
										string name2 = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary4;
										StuffItemInfo stuffItemInfo2;
										if (this._itemTables.TryGetValue(param.CategoryID_1, out dictionary4) && dictionary4.TryGetValue(param.ItemID_1, out stuffItemInfo2))
										{
											name2 = stuffItemInfo2.Name;
										}
										ItemTableElement.GatherElement item2 = new ItemTableElement.GatherElement
										{
											name = name2,
											prob = param.Probability_1 / 100f,
											categoryID = param.CategoryID_1,
											itemID = param.ItemID_1,
											minCount = param.Min_1,
											maxCount = param.Max_1
										};
										itemTableElement2.Elements.Add(item2);
									}
									if (param.CategoryID_2 > 0)
									{
										string name3 = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary5;
										StuffItemInfo stuffItemInfo3;
										if (this._itemTables.TryGetValue(param.CategoryID_2, out dictionary5) && dictionary5.TryGetValue(param.ItemID_2, out stuffItemInfo3))
										{
											name3 = stuffItemInfo3.Name;
										}
										ItemTableElement.GatherElement item3 = new ItemTableElement.GatherElement
										{
											name = name3,
											prob = param.Probability_2 / 100f,
											categoryID = param.CategoryID_2,
											itemID = param.ItemID_2,
											minCount = param.Min_2,
											maxCount = param.Max_2
										};
										itemTableElement2.Elements.Add(item3);
									}
									if (param.CategoryID_3 > 0)
									{
										string name4 = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary6;
										StuffItemInfo stuffItemInfo4;
										if (this._itemTables.TryGetValue(param.CategoryID_3, out dictionary6) && dictionary6.TryGetValue(param.ItemID_3, out stuffItemInfo4))
										{
											name4 = stuffItemInfo4.Name;
										}
										ItemTableElement.GatherElement item4 = new ItemTableElement.GatherElement
										{
											name = name4,
											prob = param.Probability_3 / 100f,
											categoryID = param.CategoryID_3,
											itemID = param.ItemID_3,
											minCount = param.Min_3,
											maxCount = param.Max_3
										};
										itemTableElement2.Elements.Add(item4);
									}
									if (param.CategoryID_4 > 0)
									{
										string name5 = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary7;
										StuffItemInfo stuffItemInfo5;
										if (this._itemTables.TryGetValue(param.CategoryID_4, out dictionary7) && dictionary7.TryGetValue(param.ItemID_4, out stuffItemInfo5))
										{
											name5 = stuffItemInfo5.Name;
										}
										ItemTableElement.GatherElement item5 = new ItemTableElement.GatherElement
										{
											name = name5,
											prob = param.Probability_4 / 100f,
											categoryID = param.CategoryID_4,
											itemID = param.ItemID_4,
											minCount = param.Min_4,
											maxCount = param.Max_4
										};
										itemTableElement2.Elements.Add(item5);
									}
									if (param.CategoryID_5 > 0)
									{
										string name6 = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary8;
										StuffItemInfo stuffItemInfo6;
										if (this._itemTables.TryGetValue(param.CategoryID_5, out dictionary8) && dictionary8.TryGetValue(param.ItemID_5, out stuffItemInfo6))
										{
											name6 = stuffItemInfo6.Name;
										}
										ItemTableElement.GatherElement item6 = new ItemTableElement.GatherElement
										{
											name = name6,
											prob = param.Probability_5 / 100f,
											categoryID = param.CategoryID_5,
											itemID = param.ItemID_5,
											minCount = param.Min_5,
											maxCount = param.Max_5
										};
										itemTableElement2.Elements.Add(item6);
									}
									if (param.CategoryID_6 > 0)
									{
										string name7 = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary9;
										StuffItemInfo stuffItemInfo7;
										if (this._itemTables.TryGetValue(param.CategoryID_6, out dictionary9) && dictionary9.TryGetValue(param.ItemID_6, out stuffItemInfo7))
										{
											name7 = stuffItemInfo7.Name;
										}
										ItemTableElement.GatherElement item7 = new ItemTableElement.GatherElement
										{
											name = name7,
											prob = param.Probability_6 / 100f,
											categoryID = param.CategoryID_6,
											itemID = param.ItemID_6,
											minCount = param.Min_6,
											maxCount = param.Max_6
										};
										itemTableElement2.Elements.Add(item7);
									}
									if (param.CategoryID_7 > 0)
									{
										string name8 = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary10;
										StuffItemInfo stuffItemInfo8;
										if (this._itemTables.TryGetValue(param.CategoryID_7, out dictionary10) && dictionary10.TryGetValue(param.ItemID_7, out stuffItemInfo8))
										{
											name8 = stuffItemInfo8.Name;
										}
										ItemTableElement.GatherElement item8 = new ItemTableElement.GatherElement
										{
											name = name8,
											prob = param.Probability_7 / 100f,
											categoryID = param.CategoryID_7,
											itemID = param.ItemID_7,
											minCount = param.Min_7,
											maxCount = param.Max_7
										};
										itemTableElement2.Elements.Add(item8);
									}
									if (param.CategoryID_8 > 0)
									{
										string name9 = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary11;
										StuffItemInfo stuffItemInfo9;
										if (this._itemTables.TryGetValue(param.CategoryID_8, out dictionary11) && dictionary11.TryGetValue(param.ItemID_8, out stuffItemInfo9))
										{
											name9 = stuffItemInfo9.Name;
										}
										ItemTableElement.GatherElement item9 = new ItemTableElement.GatherElement
										{
											name = name9,
											prob = param.Probability_8 / 100f,
											categoryID = param.CategoryID_8,
											itemID = param.ItemID_8,
											minCount = param.Min_8,
											maxCount = param.Max_8
										};
										itemTableElement2.Elements.Add(item9);
									}
									if (param.CategoryID_9 > 0)
									{
										string name10 = string.Empty;
										Dictionary<int, StuffItemInfo> dictionary12;
										StuffItemInfo stuffItemInfo10;
										if (this._itemTables.TryGetValue(param.CategoryID_9, out dictionary12) && dictionary12.TryGetValue(param.ItemID_9, out stuffItemInfo10))
										{
											name10 = stuffItemInfo10.Name;
										}
										ItemTableElement.GatherElement item10 = new ItemTableElement.GatherElement
										{
											name = name10,
											prob = param.Probability_9 / 100f,
											categoryID = param.CategoryID_9,
											itemID = param.ItemID_9,
											minCount = param.Min_9,
											maxCount = param.Max_9
										};
										itemTableElement2.Elements.Add(item10);
									}
								}
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
			}

			// Token: 0x06004084 RID: 16516 RVA: 0x00185044 File Offset: 0x00183444
			private void LoadFrogItemTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.FrogItemTable, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!text.IsNullOrEmpty())
					{
						if (!Game.IsRestrictedOver50(text))
						{
							foreach (GatherItemData gatherItemData in AssetBundleManager.LoadAllAsset(text, typeof(GatherItemData), null).GetAllAssets<GatherItemData>())
							{
								if (!(gatherItemData == null))
								{
									string s = gatherItemData.name ?? string.Empty;
									int key;
									if (int.TryParse(s, out key))
									{
										Dictionary<int, ItemTableElement> dictionary;
										if (!this._frogItemTable.TryGetValue(key, out dictionary))
										{
											dictionary = (this._frogItemTable[key] = new Dictionary<int, ItemTableElement>());
										}
										foreach (GatherItemData.Param param in gatherItemData.param)
										{
											ItemTableElement itemTableElement = new ItemTableElement
											{
												Rate = param.Rate
											};
											dictionary[param.ID] = itemTableElement;
											ItemTableElement itemTableElement2 = itemTableElement;
											if (0 < param.CategoryID_0)
											{
												string name = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary2;
												StuffItemInfo stuffItemInfo;
												if (this._itemTables.TryGetValue(param.CategoryID_0, out dictionary2) && dictionary2.TryGetValue(param.ItemID_0, out stuffItemInfo))
												{
													name = stuffItemInfo.Name;
												}
												ItemTableElement.GatherElement item = new ItemTableElement.GatherElement
												{
													name = name,
													prob = param.Probability_0 / 100f,
													categoryID = param.CategoryID_0,
													itemID = param.ItemID_0,
													minCount = param.Min_0,
													maxCount = param.Max_0
												};
												itemTableElement2.Elements.Add(item);
											}
											if (0 < param.CategoryID_1)
											{
												string name2 = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary3;
												StuffItemInfo stuffItemInfo2;
												if (this._itemTables.TryGetValue(param.CategoryID_1, out dictionary3) && dictionary3.TryGetValue(param.ItemID_1, out stuffItemInfo2))
												{
													name2 = stuffItemInfo2.Name;
												}
												ItemTableElement.GatherElement item2 = new ItemTableElement.GatherElement
												{
													name = name2,
													prob = param.Probability_1 / 100f,
													categoryID = param.CategoryID_1,
													itemID = param.ItemID_1,
													minCount = param.Min_1,
													maxCount = param.Max_1
												};
												itemTableElement2.Elements.Add(item2);
											}
											if (0 < param.CategoryID_2)
											{
												string name3 = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary4;
												StuffItemInfo stuffItemInfo3;
												if (this._itemTables.TryGetValue(param.CategoryID_2, out dictionary4) && dictionary4.TryGetValue(param.ItemID_2, out stuffItemInfo3))
												{
													name3 = stuffItemInfo3.Name;
												}
												ItemTableElement.GatherElement item3 = new ItemTableElement.GatherElement
												{
													name = name3,
													prob = param.Probability_2 / 100f,
													categoryID = param.CategoryID_2,
													itemID = param.ItemID_2,
													minCount = param.Min_2,
													maxCount = param.Max_2
												};
												itemTableElement2.Elements.Add(item3);
											}
											if (0 < param.CategoryID_3)
											{
												string name4 = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary5;
												StuffItemInfo stuffItemInfo4;
												if (this._itemTables.TryGetValue(param.CategoryID_3, out dictionary5) && dictionary5.TryGetValue(param.ItemID_3, out stuffItemInfo4))
												{
													name4 = stuffItemInfo4.Name;
												}
												ItemTableElement.GatherElement item4 = new ItemTableElement.GatherElement
												{
													name = name4,
													prob = param.Probability_3 / 100f,
													categoryID = param.CategoryID_3,
													itemID = param.ItemID_3,
													minCount = param.Min_3,
													maxCount = param.Max_3
												};
												itemTableElement2.Elements.Add(item4);
											}
											if (0 < param.CategoryID_4)
											{
												string name5 = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary6;
												StuffItemInfo stuffItemInfo5;
												if (this._itemTables.TryGetValue(param.CategoryID_4, out dictionary6) && dictionary6.TryGetValue(param.ItemID_4, out stuffItemInfo5))
												{
													name5 = stuffItemInfo5.Name;
												}
												ItemTableElement.GatherElement item5 = new ItemTableElement.GatherElement
												{
													name = name5,
													prob = param.Probability_4 / 100f,
													categoryID = param.CategoryID_4,
													itemID = param.ItemID_4,
													minCount = param.Min_4,
													maxCount = param.Max_4
												};
												itemTableElement2.Elements.Add(item5);
											}
											if (0 < param.CategoryID_5)
											{
												string name6 = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary7;
												StuffItemInfo stuffItemInfo6;
												if (this._itemTables.TryGetValue(param.CategoryID_5, out dictionary7) && dictionary7.TryGetValue(param.ItemID_5, out stuffItemInfo6))
												{
													name6 = stuffItemInfo6.Name;
												}
												ItemTableElement.GatherElement item6 = new ItemTableElement.GatherElement
												{
													name = name6,
													prob = param.Probability_5 / 100f,
													categoryID = param.CategoryID_5,
													itemID = param.ItemID_5,
													minCount = param.Min_5,
													maxCount = param.Max_5
												};
												itemTableElement2.Elements.Add(item6);
											}
											if (0 < param.CategoryID_6)
											{
												string name7 = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary8;
												StuffItemInfo stuffItemInfo7;
												if (this._itemTables.TryGetValue(param.CategoryID_6, out dictionary8) && dictionary8.TryGetValue(param.ItemID_6, out stuffItemInfo7))
												{
													name7 = stuffItemInfo7.Name;
												}
												ItemTableElement.GatherElement item7 = new ItemTableElement.GatherElement
												{
													name = name7,
													prob = param.Probability_6 / 100f,
													categoryID = param.CategoryID_6,
													itemID = param.ItemID_6,
													minCount = param.Min_6,
													maxCount = param.Max_6
												};
												itemTableElement2.Elements.Add(item7);
											}
											if (0 < param.CategoryID_7)
											{
												string name8 = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary9;
												StuffItemInfo stuffItemInfo8;
												if (this._itemTables.TryGetValue(param.CategoryID_7, out dictionary9) && dictionary9.TryGetValue(param.ItemID_7, out stuffItemInfo8))
												{
													name8 = stuffItemInfo8.Name;
												}
												ItemTableElement.GatherElement item8 = new ItemTableElement.GatherElement
												{
													name = name8,
													prob = param.Probability_7 / 100f,
													categoryID = param.CategoryID_7,
													itemID = param.ItemID_7,
													minCount = param.Min_7,
													maxCount = param.Max_7
												};
												itemTableElement2.Elements.Add(item8);
											}
											if (0 < param.CategoryID_8)
											{
												string name9 = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary10;
												StuffItemInfo stuffItemInfo9;
												if (this._itemTables.TryGetValue(param.CategoryID_8, out dictionary10) && dictionary10.TryGetValue(param.ItemID_8, out stuffItemInfo9))
												{
													name9 = stuffItemInfo9.Name;
												}
												ItemTableElement.GatherElement item9 = new ItemTableElement.GatherElement
												{
													name = name9,
													prob = param.Probability_8 / 100f,
													categoryID = param.CategoryID_8,
													itemID = param.ItemID_8,
													minCount = param.Min_8,
													maxCount = param.Max_8
												};
												itemTableElement2.Elements.Add(item9);
											}
											if (0 < param.CategoryID_9)
											{
												string name10 = string.Empty;
												Dictionary<int, StuffItemInfo> dictionary11;
												StuffItemInfo stuffItemInfo10;
												if (this._itemTables.TryGetValue(param.CategoryID_9, out dictionary11) && dictionary11.TryGetValue(param.ItemID_9, out stuffItemInfo10))
												{
													name10 = stuffItemInfo10.Name;
												}
												ItemTableElement.GatherElement item10 = new ItemTableElement.GatherElement
												{
													name = name10,
													prob = param.Probability_9 / 100f,
													categoryID = param.CategoryID_9,
													itemID = param.ItemID_9,
													minCount = param.Min_9,
													maxCount = param.Max_9
												};
												itemTableElement2.Elements.Add(item10);
											}
										}
									}
								}
							}
							AssetBundleManager.UnloadAssetBundle(text, false, null, false);
						}
					}
				}
			}

			// Token: 0x06004085 RID: 16517 RVA: 0x00185948 File Offset: 0x00183D48
			private void TryGet(ref bool success, List<string> row, ref int idx, out float value)
			{
				if (success)
				{
					success &= float.TryParse(row.GetElement(idx++) ?? string.Empty, out value);
				}
				else
				{
					idx++;
					value = 0f;
				}
			}

			// Token: 0x06004086 RID: 16518 RVA: 0x00185998 File Offset: 0x00183D98
			private void TryGet(ref bool success, List<string> row, ref int idx, out int value)
			{
				if (success)
				{
					success &= int.TryParse(row.GetElement(idx++) ?? string.Empty, out value);
				}
				else
				{
					idx++;
					value = 0;
				}
			}

			// Token: 0x06004087 RID: 16519 RVA: 0x001859E4 File Offset: 0x00183DE4
			private void LoadSearchEquipItemList(DefinePack definePack)
			{
				Dictionary<int, ActionItemInfo> eventItemList = Singleton<Resources>.Instance.Map.EventItemList;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.SearchEquipItemObjList, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							int j = 1;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								int num = 1;
								int key;
								if (int.TryParse(param.list.GetElement(num++), out key))
								{
									int key2;
									if (int.TryParse(param.list.GetElement(num++), out key2))
									{
										int num2;
										if (int.TryParse(param.list.GetElement(num++), out num2))
										{
											Dictionary<int, EquipEventItemInfo> dictionary;
											if (!this.SearchEquipEventItemTable.TryGetValue(key, out dictionary))
											{
												Dictionary<int, EquipEventItemInfo> dictionary2 = new Dictionary<int, EquipEventItemInfo>();
												this.SearchEquipEventItemTable[key] = dictionary2;
												dictionary = dictionary2;
											}
											ActionItemInfo actionItemInfo;
											if (eventItemList.TryGetValue(num2, out actionItemInfo))
											{
												dictionary[key2] = new EquipEventItemInfo
												{
													EventItemID = num2,
													ActionItemInfo = actionItemInfo
												};
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004088 RID: 16520 RVA: 0x00185B74 File Offset: 0x00183F74
			private void LoadCommonEquipItemList(DefinePack definePack)
			{
				Dictionary<int, ActionItemInfo> eventItemList = Singleton<Resources>.Instance.Map.EventItemList;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.CommonEquipItemObjList, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							int j = 1;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								int num = 1;
								int key;
								if (int.TryParse(param.list.GetElement(num++), out key))
								{
									int num2;
									if (int.TryParse(param.list.GetElement(num++), out num2))
									{
										int key2;
										if (int.TryParse(param.list.GetElement(num++), out key2))
										{
											Dictionary<int, EquipEventItemInfo> dictionary;
											if (!this.CommonEquipEventItemTable.TryGetValue(key, out dictionary))
											{
												Dictionary<int, EquipEventItemInfo> dictionary2 = new Dictionary<int, EquipEventItemInfo>();
												this.CommonEquipEventItemTable[key] = dictionary2;
												dictionary = dictionary2;
											}
											ActionItemInfo actionItemInfo;
											if (eventItemList.TryGetValue(key2, out actionItemInfo))
											{
												dictionary[num2] = new EquipEventItemInfo
												{
													EventItemID = num2,
													ActionItemInfo = actionItemInfo
												};
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004089 RID: 16521 RVA: 0x00185D04 File Offset: 0x00184104
			private void LoadAccessoryItem(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AccessoryItem, false);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (GameItemAccessoryData gameItemAccessoryData in AssetBundleManager.LoadAllAsset(text, typeof(GameItemAccessoryData), null).GetAllAssets<GameItemAccessoryData>())
						{
							int key;
							if (int.TryParse(gameItemAccessoryData.name, out key))
							{
								Dictionary<int, AccessoryItemInfo> dictionary;
								if (!this.AccessoryItem.TryGetValue(key, out dictionary))
								{
									Dictionary<int, AccessoryItemInfo> dictionary2 = new Dictionary<int, AccessoryItemInfo>();
									this.AccessoryItem[key] = dictionary2;
									dictionary = dictionary2;
								}
								foreach (GameItemAccessoryData.Param param in gameItemAccessoryData.param)
								{
									if (!param.AssetBundle.IsNullOrEmpty() && !param.Asset.IsNullOrEmpty() && !param.ParentName.IsNullOrEmpty())
									{
										dictionary[param.ID] = new AccessoryItemInfo
										{
											Name = param.Name,
											AssetBundle = param.AssetBundle,
											Asset = param.Asset,
											Manifest = param.Manifest,
											ParentName = param.ParentName,
											Weight = param.Weight
										};
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x0600408A RID: 16522 RVA: 0x00185EFC File Offset: 0x001842FC
			private void LoadVendItemList()
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/merchant/vend/normal/", true);
				assetBundleNameListFromPath.Sort();
				Dictionary<int, List<VendData.Param>> dictionary = new Dictionary<int, List<VendData.Param>>();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (VendData vendData in AssetBundleManager.LoadAllAsset(text, typeof(VendData), null).GetAllAssets<VendData>())
						{
							int key;
							if (int.TryParse(vendData.name, out key))
							{
								dictionary[key] = vendData.param;
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				foreach (KeyValuePair<int, List<VendData.Param>> keyValuePair in dictionary)
				{
					Dictionary<int, List<VendItemInfo>> dictionary2 = new Dictionary<int, List<VendItemInfo>>();
					foreach (IGrouping<int, VendData.Param> grouping in from p in keyValuePair.Value
					group p by p.Group)
					{
						dictionary2[grouping.Key] = grouping.Select(delegate(VendData.Param data)
						{
							StuffItemInfo stuffItemInfo = this.FindItemInfo(data.nameHash);
							if (stuffItemInfo != null)
							{
								return new VendItemInfo(stuffItemInfo, data);
							}
							return null;
						}).Where((VendItemInfo p) => p != null).ToList<VendItemInfo>();
					}
					this.VendItemInfoTable[keyValuePair.Key] = dictionary2;
				}
			}

			// Token: 0x0600408B RID: 16523 RVA: 0x001860EC File Offset: 0x001844EC
			private void LoadSpecialVendItemList()
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/merchant/vend/special/", true);
				assetBundleNameListFromPath.Sort();
				Dictionary<int, VendItemInfo> dictionary = new Dictionary<int, VendItemInfo>();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (VendSpecialData vendSpecialData in AssetBundleManager.LoadAllAsset(text, typeof(VendSpecialData), null).GetAllAssets<VendSpecialData>())
						{
							foreach (VendSpecialData.Param param in vendSpecialData.param)
							{
								StuffItemInfo stuffItemInfo = this.FindItemInfo(param.nameHash);
								if (stuffItemInfo != null)
								{
									dictionary[param.ID] = new VendItemInfo(stuffItemInfo, param);
								}
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				this.VendItemInfoSpecialTable = dictionary;
			}

			// Token: 0x0600408C RID: 16524 RVA: 0x00186224 File Offset: 0x00184624
			private void LoadHarvest()
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/gameitem/harvest/", true);
				assetBundleNameListFromPath.Sort();
				List<HarvestData.Param> list = new List<HarvestData.Param>();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (HarvestData harvestData in AssetBundleManager.LoadAllAsset(text, typeof(HarvestData), null).GetAllAssets<HarvestData>())
						{
							list.AddRange(harvestData.param);
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				this.HarvestDataInfoTable = (from item in list
				select new HarvestDataInfo(item) into p
				where p.table.Any<KeyValuePair<int, List<HarvestData.Data>>>() && this.FindItemInfo(p.nameHash) != null
				select p).ToDictionary((HarvestDataInfo v) => v.nameHash, (HarvestDataInfo v) => v);
			}

			// Token: 0x0600408D RID: 16525 RVA: 0x00186364 File Offset: 0x00184764
			private void LoadAgentAdvPresentInfoTable()
			{
				Dictionary<int, Dictionary<int, AgentAdvPresentInfo.Param>> dictionary = new Dictionary<int, Dictionary<int, AgentAdvPresentInfo.Param>>();
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/agent/present/", true);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (AgentAdvPresentInfo agentAdvPresentInfo in AssetBundleManager.LoadAllAsset(text, typeof(AgentAdvPresentInfo), null).GetAllAssets<AgentAdvPresentInfo>())
						{
							int key;
							if (int.TryParse(agentAdvPresentInfo.name, out key))
							{
								Dictionary<int, AgentAdvPresentInfo.Param> dictionary2;
								if (!dictionary.TryGetValue(key, out dictionary2))
								{
									dictionary2 = (dictionary[key] = new Dictionary<int, AgentAdvPresentInfo.Param>());
								}
								foreach (AgentAdvPresentInfo.Param param in agentAdvPresentInfo.param)
								{
									dictionary2[param.ID] = param;
								}
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				foreach (Dictionary<int, AgentAdvPresentInfo.Param> dictionary3 in dictionary.Values)
				{
					foreach (int key2 in dictionary3.Keys.ToArray<int>())
					{
						AgentAdvPresentInfo.Param param2 = new AgentAdvPresentInfo.Param(dictionary3[key2]);
						foreach (AgentAdvPresentInfo.ItemData item in (from x in param2.ItemData
						where this.FindItemInfo(x.nameHash) == null
						select x).ToArray<AgentAdvPresentInfo.ItemData>())
						{
							param2.ItemData.Remove(item);
						}
						dictionary3[key2] = param2;
					}
				}
				this.AgentAdvPresentInfoTable = dictionary;
			}

			// Token: 0x0600408E RID: 16526 RVA: 0x00186590 File Offset: 0x00184990
			private void LoadAgentAdvPresentBirthdayInfoTable()
			{
				Dictionary<int, Dictionary<int, AgentAdvPresentBirthdayInfo.Param>> dictionary = new Dictionary<int, Dictionary<int, AgentAdvPresentBirthdayInfo.Param>>();
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/agent/present_birthday/", true);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (AgentAdvPresentBirthdayInfo agentAdvPresentBirthdayInfo in AssetBundleManager.LoadAllAsset(text, typeof(AgentAdvPresentBirthdayInfo), null).GetAllAssets<AgentAdvPresentBirthdayInfo>())
						{
							int key;
							if (int.TryParse(agentAdvPresentBirthdayInfo.name, out key))
							{
								Dictionary<int, AgentAdvPresentBirthdayInfo.Param> dictionary2;
								if (!dictionary.TryGetValue(key, out dictionary2))
								{
									dictionary2 = (dictionary[key] = new Dictionary<int, AgentAdvPresentBirthdayInfo.Param>());
								}
								foreach (AgentAdvPresentBirthdayInfo.Param param in agentAdvPresentBirthdayInfo.param)
								{
									dictionary2[param.ID] = param;
								}
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				foreach (Dictionary<int, AgentAdvPresentBirthdayInfo.Param> dictionary3 in dictionary.Values)
				{
					foreach (int key2 in dictionary3.Keys.ToArray<int>())
					{
						AgentAdvPresentBirthdayInfo.Param param2 = new AgentAdvPresentBirthdayInfo.Param(dictionary3[key2]);
						foreach (AgentAdvPresentBirthdayInfo.ItemData item in (from x in param2.ItemData
						where this.FindItemInfo(x.nameHash) == null
						select x).ToArray<AgentAdvPresentBirthdayInfo.ItemData>())
						{
							param2.ItemData.Remove(item);
						}
						dictionary3[key2] = param2;
					}
				}
				this.AgentAdvPresentBirthdayInfoTable = dictionary;
			}

			// Token: 0x0600408F RID: 16527 RVA: 0x001867BC File Offset: 0x00184BBC
			private void LoadAgentAdvScroungeInfoTable()
			{
				Dictionary<int, Dictionary<int, AgentAdvScroungeInfo.Param>> dictionary = new Dictionary<int, Dictionary<int, AgentAdvScroungeInfo.Param>>();
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/agent/scrounge/", true);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (AgentAdvScroungeInfo agentAdvScroungeInfo in AssetBundleManager.LoadAllAsset(text, typeof(AgentAdvScroungeInfo), null).GetAllAssets<AgentAdvScroungeInfo>())
						{
							int key;
							if (int.TryParse(agentAdvScroungeInfo.name, out key))
							{
								Dictionary<int, AgentAdvScroungeInfo.Param> dictionary2;
								if (!dictionary.TryGetValue(key, out dictionary2))
								{
									dictionary2 = (dictionary[key] = new Dictionary<int, AgentAdvScroungeInfo.Param>());
								}
								foreach (AgentAdvScroungeInfo.Param param in agentAdvScroungeInfo.param)
								{
									dictionary2[param.ID] = param;
								}
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				foreach (Dictionary<int, AgentAdvScroungeInfo.Param> dictionary3 in dictionary.Values)
				{
					foreach (int key2 in dictionary3.Keys.ToArray<int>())
					{
						AgentAdvScroungeInfo.Param param2 = new AgentAdvScroungeInfo.Param(dictionary3[key2]);
						foreach (AgentAdvScroungeInfo.ItemData item in (from x in param2.ItemData
						where this.FindItemInfo(x.nameHash) == null
						select x).ToArray<AgentAdvScroungeInfo.ItemData>())
						{
							param2.ItemData.Remove(item);
						}
						dictionary3[key2] = param2;
					}
				}
				this.AgentAdvScroungeInfoTable = dictionary;
			}

			// Token: 0x06004090 RID: 16528 RVA: 0x001869E8 File Offset: 0x00184DE8
			private void LoadAgentAdvEventInfoTable()
			{
				Dictionary<int, Dictionary<string, AgentAdvEventInfo.Param>> dictionary = new Dictionary<int, Dictionary<string, AgentAdvEventInfo.Param>>();
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/agent/event/", true);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (AgentAdvEventInfo agentAdvEventInfo in AssetBundleManager.LoadAllAsset(text, typeof(AgentAdvEventInfo), null).GetAllAssets<AgentAdvEventInfo>())
						{
							int key;
							if (int.TryParse(agentAdvEventInfo.name, out key))
							{
								Dictionary<string, AgentAdvEventInfo.Param> dictionary2;
								if (!dictionary.TryGetValue(key, out dictionary2))
								{
									dictionary2 = (dictionary[key] = new Dictionary<string, AgentAdvEventInfo.Param>());
								}
								foreach (AgentAdvEventInfo.Param param in agentAdvEventInfo.param)
								{
									dictionary2[param.FileName] = param;
								}
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				this.AgentAdvEventInfoTable = dictionary;
			}

			// Token: 0x06004091 RID: 16529 RVA: 0x00186B38 File Offset: 0x00184F38
			private void LoadAgentRandomEventInfoTable()
			{
				Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/agent/randevent/", true);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (AgentRandomEventInfo agentRandomEventInfo in AssetBundleManager.LoadAllAsset(text, typeof(AgentRandomEventInfo), null).GetAllAssets<AgentRandomEventInfo>())
						{
							int key;
							if (int.TryParse(agentRandomEventInfo.name, out key))
							{
								List<string> list;
								if (!dictionary.TryGetValue(key, out list))
								{
									list = (dictionary[key] = new List<string>());
								}
								foreach (AgentRandomEventInfo.Param param in agentRandomEventInfo.param)
								{
									foreach (string item in param.FileNames)
									{
										if (!list.Contains(item))
										{
											list.Add(item);
										}
									}
								}
							}
						}
					}
				}
				this.AgentRandAdvEventTable = dictionary;
			}

			// Token: 0x06004092 RID: 16530 RVA: 0x00186CF0 File Offset: 0x001850F0
			private void LoadAgentRandomEventItemInfoTable()
			{
				Dictionary<int, AgentRandomEventItemInfo.Param> dictionary = new Dictionary<int, AgentRandomEventItemInfo.Param>();
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/agent/randevent_item/", true);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (AgentRandomEventItemInfo agentRandomEventItemInfo in AssetBundleManager.LoadAllAsset(text, typeof(AgentRandomEventItemInfo), null).GetAllAssets<AgentRandomEventItemInfo>())
						{
							foreach (AgentRandomEventItemInfo.Param param in agentRandomEventItemInfo.param)
							{
								dictionary[param.ID] = param;
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				foreach (int key in dictionary.Keys.ToArray<int>())
				{
					AgentRandomEventItemInfo.Param param2 = new AgentRandomEventItemInfo.Param(dictionary[key]);
					foreach (AgentRandomEventItemInfo.ItemData item in (from x in param2.ItemData
					where this.FindItemInfo(x.nameHash) == null
					select x).ToArray<AgentRandomEventItemInfo.ItemData>())
					{
						param2.ItemData.Remove(item);
					}
					dictionary[key] = param2;
				}
				this.AgentRandomEventItemInfoTable = dictionary;
			}

			// Token: 0x06004093 RID: 16531 RVA: 0x00186E9C File Offset: 0x0018529C
			private void LoadAgentLifeStyleInfoTable()
			{
				Dictionary<int, LifeStyleData.Param> dictionary = new Dictionary<int, LifeStyleData.Param>();
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/agent/lifestyle/", true);
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (LifeStyleData lifeStyleData in AssetBundleManager.LoadAllAsset(text, typeof(LifeStyleData), null).GetAllAssets<LifeStyleData>())
						{
							foreach (LifeStyleData.Param param in lifeStyleData.param)
							{
								dictionary[param.ID] = param;
							}
						}
						AssetBundleManager.UnloadAssetBundle(text, false, null, false);
					}
				}
				this.AgentLifeStyleInfoTable = dictionary;
			}

			// Token: 0x06004094 RID: 16532 RVA: 0x00186FB4 File Offset: 0x001853B4
			private void LoadRecyclingInfo(DefinePack definePack)
			{
				if (definePack == null)
				{
					return;
				}
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.RecyclingInfoList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string element = assetBundleNameListFromPath.GetElement(i);
					if (!element.IsNullOrEmpty())
					{
						if (!Game.IsRestrictedOver50(element))
						{
							string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(element);
							if (!fileNameWithoutExtension.IsNullOrEmpty())
							{
								if (AssetBundleCheck.IsFile(element, fileNameWithoutExtension))
								{
									ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(element, fileNameWithoutExtension, string.Empty);
									if (!(excelData == null) && excelData.MaxCell > 0)
									{
										for (int j = 1; j < excelData.MaxCell; j++)
										{
											List<string> list = excelData.list[j].list;
											if (!list.IsNullOrEmpty<string>())
											{
												int num = 0;
												string s = list.GetElement(num++) ?? string.Empty;
												int num2;
												if (int.TryParse(s, out num2))
												{
													AssetBundleInfo listABInfo = new AssetBundleInfo
													{
														name = list.GetElement(num++),
														assetbundle = list.GetElement(num++),
														asset = list.GetElement(num++),
														manifest = list.GetElement(num++)
													};
													if (!listABInfo.asset.IsNullOrEmpty() && !listABInfo.assetbundle.IsNullOrEmpty())
													{
														if (num2 == 0)
														{
															this.LoadRecyclingCreateableItemInfoList(listABInfo);
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				if (!this.RecyclingCreateableItemTable.IsNullOrEmpty<int, Dictionary<int, RecyclingItemInfo>>())
				{
					List<UnityEx.ValueTuple<int, int>> list2 = ListPool<UnityEx.ValueTuple<int, int>>.Get();
					List<int> list3 = ListPool<int>.Get();
					list3.AddRange(this.RecyclingCreateableItemTable.Keys);
					for (int k = 0; k < list3.Count; k++)
					{
						int key = list3[k];
						if (this.RecyclingCreateableItemTable.ContainsKey(key))
						{
							Dictionary<int, RecyclingItemInfo> dictionary = this.RecyclingCreateableItemTable[key];
							if (!dictionary.IsNullOrEmpty<int, RecyclingItemInfo>())
							{
								List<int> list4 = ListPool<int>.Get();
								list4.AddRange(dictionary.Keys);
								for (int l = 0; l < list4.Count; l++)
								{
									int key2 = list4[l];
									if (dictionary.ContainsKey(key2))
									{
										RecyclingItemInfo info = dictionary[key2];
										info.ItemInfo = this.GetItem(info.CategoryID, info.ItemID);
										if (info.ItemInfo == null)
										{
											if (!list2.Exists((UnityEx.ValueTuple<int, int> x) => x.Item1 == info.CategoryID && x.Item2 == info.ItemID))
											{
												list2.Add(new UnityEx.ValueTuple<int, int>(info.CategoryID, info.ItemID));
											}
										}
										else if (info.Adult && !Game.isAdd01 && !list2.Exists((UnityEx.ValueTuple<int, int> x) => x.Item1 == info.CategoryID && x.Item2 == info.ItemID))
										{
											list2.Add(new UnityEx.ValueTuple<int, int>(info.CategoryID, info.ItemID));
										}
										dictionary[key2] = info;
									}
								}
								ListPool<int>.Release(list4);
							}
						}
					}
					ListPool<int>.Release(list3);
					if (!list2.IsNullOrEmpty<UnityEx.ValueTuple<int, int>>())
					{
						foreach (UnityEx.ValueTuple<int, int> valueTuple in list2)
						{
							Dictionary<int, RecyclingItemInfo> dictionary2;
							if (this.RecyclingCreateableItemTable.TryGetValue(valueTuple.Item1, out dictionary2))
							{
								if (dictionary2.IsNullOrEmpty<int, RecyclingItemInfo>())
								{
									this.RecyclingCreateableItemTable.Remove(valueTuple.Item1);
								}
								else if (dictionary2.ContainsKey(valueTuple.Item2))
								{
									dictionary2.Remove(valueTuple.Item2);
									if (dictionary2.IsNullOrEmpty<int, RecyclingItemInfo>())
									{
										this.RecyclingCreateableItemTable.Remove(valueTuple.Item1);
									}
								}
							}
						}
					}
					ListPool<UnityEx.ValueTuple<int, int>>.Release(list2);
					if (!this.RecyclingCreateableItemTable.IsNullOrEmpty<int, Dictionary<int, RecyclingItemInfo>>())
					{
						foreach (KeyValuePair<int, Dictionary<int, RecyclingItemInfo>> keyValuePair in this.RecyclingCreateableItemTable)
						{
							if (!keyValuePair.Value.IsNullOrEmpty<int, RecyclingItemInfo>())
							{
								foreach (KeyValuePair<int, RecyclingItemInfo> keyValuePair2 in keyValuePair.Value)
								{
									this.RecyclingCreateableItemList.Add(keyValuePair2.Value);
								}
							}
						}
						this.RecyclingCreateableItemList.Sort();
					}
				}
			}

			// Token: 0x06004095 RID: 16533 RVA: 0x00187528 File Offset: 0x00185928
			private void LoadRecyclingCreateableItemInfoList(AssetBundleInfo listABInfo)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(listABInfo);
				if (excelData == null || excelData.MaxCell <= 0)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int num;
						if (int.TryParse(s, out num))
						{
							string s2 = list.GetElement(j++) ?? string.Empty;
							int num2;
							if (int.TryParse(s2, out num2))
							{
								string s3 = list.GetElement(j++) ?? string.Empty;
								int iconID;
								if (int.TryParse(s3, out iconID))
								{
									string value = list.GetElement(j++) ?? string.Empty;
									bool adult;
									if (bool.TryParse(value, out adult))
									{
										string value2 = list.GetElement(j++) ?? string.Empty;
										bool flag;
										Dictionary<int, RecyclingItemInfo> dictionary;
										if (bool.TryParse(value2, out flag) && !flag && this.RecyclingCreateableItemTable.TryGetValue(num, out dictionary) && !dictionary.IsNullOrEmpty<int, RecyclingItemInfo>() && dictionary.ContainsKey(num2))
										{
											dictionary.Remove(num2);
											if (dictionary.Count <= 0)
											{
												this.RecyclingCreateableItemTable.Remove(num);
											}
										}
										Dictionary<int, RecyclingItemInfo> dictionary2;
										if (!this.RecyclingCreateableItemTable.TryGetValue(num, out dictionary2) || dictionary2 == null)
										{
											Dictionary<int, RecyclingItemInfo> dictionary3 = new Dictionary<int, RecyclingItemInfo>();
											this.RecyclingCreateableItemTable[num] = dictionary3;
											dictionary2 = dictionary3;
										}
										RecyclingItemInfo value3;
										if (!dictionary2.TryGetValue(num2, out value3))
										{
											RecyclingItemInfo recyclingItemInfo = default(RecyclingItemInfo);
											dictionary2[num2] = recyclingItemInfo;
											value3 = recyclingItemInfo;
										}
										value3.CategoryID = num;
										value3.ItemID = num2;
										value3.IconID = iconID;
										value3.Adult = adult;
										if (value3.ItemNameList == null)
										{
											value3.ItemNameList = new List<string>();
										}
										value3.ItemNameList.Clear();
										while (j < list.Count)
										{
											value3.ItemNameList.Add(list.GetElement(j++) ?? string.Empty);
										}
										dictionary2[num2] = value3;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x04003C86 RID: 15494
			private Dictionary<int, Dictionary<int, ItemTableElement>> _itemPickTables = new Dictionary<int, Dictionary<int, ItemTableElement>>();

			// Token: 0x04003C87 RID: 15495
			private Dictionary<int, Dictionary<int, ItemTableElement>> _frogItemTable = new Dictionary<int, Dictionary<int, ItemTableElement>>();

			// Token: 0x02000911 RID: 2321
			public class RecipeInfo
			{
				// Token: 0x17000C27 RID: 3111
				// (get) Token: 0x060040B1 RID: 16561 RVA: 0x00187965 File Offset: 0x00185D65
				// (set) Token: 0x060040B2 RID: 16562 RVA: 0x0018796D File Offset: 0x00185D6D
				public bool initialized { get; private set; }

				// Token: 0x17000C28 RID: 3112
				public IReadOnlyDictionary<int, RecipeDataInfo[]> this[int i]
				{
					get
					{
						switch (i)
						{
						case 0:
							return this.materialTable;
						case 1:
							return this.equipmentTable;
						case 2:
							return this.cookTable;
						case 3:
							return this.petTable;
						case 4:
							return this.medicineTable;
						default:
							return this.materialTable;
						}
					}
				}

				// Token: 0x17000C29 RID: 3113
				// (get) Token: 0x060040B4 RID: 16564 RVA: 0x001879D2 File Offset: 0x00185DD2
				// (set) Token: 0x060040B5 RID: 16565 RVA: 0x001879DA File Offset: 0x00185DDA
				public IReadOnlyDictionary<int, RecipeDataInfo[]> materialTable { get; private set; }

				// Token: 0x17000C2A RID: 3114
				// (get) Token: 0x060040B6 RID: 16566 RVA: 0x001879E3 File Offset: 0x00185DE3
				// (set) Token: 0x060040B7 RID: 16567 RVA: 0x001879EB File Offset: 0x00185DEB
				public IReadOnlyDictionary<int, RecipeDataInfo[]> equipmentTable { get; private set; }

				// Token: 0x17000C2B RID: 3115
				// (get) Token: 0x060040B8 RID: 16568 RVA: 0x001879F4 File Offset: 0x00185DF4
				// (set) Token: 0x060040B9 RID: 16569 RVA: 0x001879FC File Offset: 0x00185DFC
				public IReadOnlyDictionary<int, RecipeDataInfo[]> cookTable { get; private set; }

				// Token: 0x17000C2C RID: 3116
				// (get) Token: 0x060040BA RID: 16570 RVA: 0x00187A05 File Offset: 0x00185E05
				// (set) Token: 0x060040BB RID: 16571 RVA: 0x00187A0D File Offset: 0x00185E0D
				public IReadOnlyDictionary<int, RecipeDataInfo[]> petTable { get; private set; }

				// Token: 0x17000C2D RID: 3117
				// (get) Token: 0x060040BC RID: 16572 RVA: 0x00187A16 File Offset: 0x00185E16
				// (set) Token: 0x060040BD RID: 16573 RVA: 0x00187A1E File Offset: 0x00185E1E
				public IReadOnlyDictionary<int, RecipeDataInfo[]> medicineTable { get; private set; }

				// Token: 0x060040BE RID: 16574 RVA: 0x00187A28 File Offset: 0x00185E28
				public void Load()
				{
					this.Load("material", delegate(Dictionary<int, RecipeDataInfo[]> data)
					{
						this.materialTable = data;
					});
					this.Load("equipment", delegate(Dictionary<int, RecipeDataInfo[]> data)
					{
						this.equipmentTable = data;
					});
					this.Load("cook", delegate(Dictionary<int, RecipeDataInfo[]> data)
					{
						this.cookTable = data;
					});
					this.Load("pet", delegate(Dictionary<int, RecipeDataInfo[]> data)
					{
						this.petTable = data;
					});
					this.Load("medicine", delegate(Dictionary<int, RecipeDataInfo[]> data)
					{
						this.medicineTable = data;
					});
					this.initialized = true;
				}

				// Token: 0x060040BF RID: 16575 RVA: 0x00187AB0 File Offset: 0x00185EB0
				private void Load(string name, Action<Dictionary<int, RecipeDataInfo[]>> action)
				{
					List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath("list/actor/gameitem/recipe/" + string.Format("{0}/", name), true);
					assetBundleNameListFromPath.Sort();
					List<RecipeData.Param> list = new List<RecipeData.Param>();
					foreach (string text in assetBundleNameListFromPath)
					{
						if (!Game.IsRestrictedOver50(text))
						{
							foreach (RecipeData recipeData in AssetBundleManager.LoadAllAsset(text, typeof(RecipeData), null).GetAllAssets<RecipeData>())
							{
								list.AddRange(recipeData.param);
							}
							AssetBundleManager.UnloadAssetBundle(text, false, null, false);
						}
					}
					IEnumerable<IGrouping<int, RecipeDataInfo>> source = (from item in list
					select new RecipeDataInfo(item) into p
					where p.NeedList.Any<RecipeDataInfo.NeedData>()
					select p).ToLookup((RecipeDataInfo v) => v.nameHash, (RecipeDataInfo v) => v);
					Func<IGrouping<int, RecipeDataInfo>, int> keySelector = (IGrouping<int, RecipeDataInfo> v) => v.Key;
					if (Resources.GameInfoTables.RecipeInfo.<>f__mg$cache0 == null)
					{
						Resources.GameInfoTables.RecipeInfo.<>f__mg$cache0 = new Func<IGrouping<int, RecipeDataInfo>, RecipeDataInfo[]>(Enumerable.ToArray<RecipeDataInfo>);
					}
					Dictionary<int, RecipeDataInfo[]> obj = source.ToDictionary(keySelector, Resources.GameInfoTables.RecipeInfo.<>f__mg$cache0);
					action(obj);
				}

				// Token: 0x060040C0 RID: 16576 RVA: 0x00187C54 File Offset: 0x00186054
				public void Release()
				{
					this.materialTable = null;
					this.equipmentTable = null;
					this.cookTable = null;
					this.petTable = null;
					this.medicineTable = null;
					this.initialized = false;
				}

				// Token: 0x04003CAB RID: 15531
				private const string LOAD_PATH = "list/actor/gameitem/recipe/";

				// Token: 0x04003CB2 RID: 15538
				[CompilerGenerated]
				private static Func<IGrouping<int, RecipeDataInfo>, RecipeDataInfo[]> <>f__mg$cache0;
			}

			// Token: 0x02000912 RID: 2322
			public class AdvPresentItemInfo
			{
				// Token: 0x060040CB RID: 16587 RVA: 0x00187CD5 File Offset: 0x001860D5
				public AdvPresentItemInfo(int eventItemID, StuffItemInfo itemInfo)
				{
					this.eventItemID = eventItemID;
					this.itemInfo = itemInfo;
				}

				// Token: 0x17000C2E RID: 3118
				// (get) Token: 0x060040CC RID: 16588 RVA: 0x00187CEB File Offset: 0x001860EB
				public int eventItemID { get; }

				// Token: 0x17000C2F RID: 3119
				// (get) Token: 0x060040CD RID: 16589 RVA: 0x00187CF3 File Offset: 0x001860F3
				public StuffItemInfo itemInfo { get; }
			}
		}

		// Token: 0x02000914 RID: 2324
		public class HSceneTables
		{
			// Token: 0x060040CF RID: 16591 RVA: 0x00187D64 File Offset: 0x00186164
			public HSceneTables()
			{
				string[,] array = new string[2, 3];
				array[0, 0] = "animator/h/male/01/aibu.unity3d";
				array[0, 1] = "animator/h/male/01/houshi.unity3d";
				array[0, 2] = "animator/h/male/01/sonyu.unity3d";
				array[1, 0] = "animator/h/female/01/aibu.unity3d";
				array[1, 1] = "animator/h/female/01/houshi.unity3d";
				array[1, 2] = "animator/h/female/01/sonyu.unity3d";
				this.strAssetAnimatorBase = array;
				string[,] array2 = new string[2, 3];
				array2[0, 0] = "aia_m_base";
				array2[0, 1] = "aih_m_base";
				array2[0, 2] = "ais_m_base";
				array2[1, 0] = "aia_f_base";
				array2[1, 1] = "aih_f_base";
				array2[1, 2] = "ais_f_base";
				this.racBaseNames = array2;
				this.hashUseAssetBundle = new HashSet<Resources.HSceneTables.HAssetBundle>[]
				{
					new HashSet<Resources.HSceneTables.HAssetBundle>(),
					new HashSet<Resources.HSceneTables.HAssetBundle>()
				};
				base..ctor();
			}

			// Token: 0x17000C30 RID: 3120
			// (get) Token: 0x060040D0 RID: 16592 RVA: 0x0018802F File Offset: 0x0018642F
			// (set) Token: 0x060040D1 RID: 16593 RVA: 0x00188037 File Offset: 0x00186437
			public Dictionary<int, ParameterPacket> HBaseParamTable { get; private set; } = new Dictionary<int, ParameterPacket>();

			// Token: 0x17000C31 RID: 3121
			// (get) Token: 0x060040D2 RID: 16594 RVA: 0x00188040 File Offset: 0x00186440
			// (set) Token: 0x060040D3 RID: 16595 RVA: 0x00188048 File Offset: 0x00186448
			public Dictionary<int, ParameterPacket> HactionParamTable { get; private set; } = new Dictionary<int, ParameterPacket>();

			// Token: 0x17000C32 RID: 3122
			// (get) Token: 0x060040D4 RID: 16596 RVA: 0x00188051 File Offset: 0x00186451
			// (set) Token: 0x060040D5 RID: 16597 RVA: 0x00188059 File Offset: 0x00186459
			public Dictionary<int, Dictionary<int, float>> HSkileParamTable { get; private set; } = new Dictionary<int, Dictionary<int, float>>();

			// Token: 0x17000C33 RID: 3123
			// (get) Token: 0x060040D6 RID: 16598 RVA: 0x00188062 File Offset: 0x00186462
			public static Dictionary<string, int> HTagTable { get; }

			// Token: 0x060040D7 RID: 16599 RVA: 0x0018806C File Offset: 0x0018646C
			public IEnumerator LoadH()
			{
				this.hashUseAssetBundle[0] = new HashSet<Resources.HSceneTables.HAssetBundle>();
				this.commonSpace = GameObject.Find("CommonSpace");
				yield return this.LoadHmesh();
				yield return this.LoadAnimationFileName();
				yield return this.LoadStartAnimationList(false);
				yield return this.LoadStartAnimationList(true);
				yield return this.LoadStartWaitAnim();
				yield return this.LoadEndAnimationList();
				this.LoadHitObject();
				this.LoadHitObjectAdd();
				yield return this.LoadHsceneParticle();
				yield return this.LoadHItemObjInfo();
				yield return this.LoadHYure();
				yield return this.LoadHYureMale();
				yield return this.LoadHPointInfo();
				yield return this.LoadAutoHPointPath();
				yield return this.LoadHItemBaseAnim();
				yield return this.LoadHAutoInfo();
				yield return this.LoadAutoLeaveItToYou();
				yield return this.LoadAutoLeaveItToYouPersonality();
				yield return this.LoadAutoLeaveItToYouAttribute();
				yield return this.LoadFeelHit();
				yield return this.LoadDankonList();
				this.CollisionLoadExcel();
				this.HitObjLoadExcel();
				this.HLayerLoadExcel();
				this.LoadHParamTable(0);
				this.LoadHParamTable(1);
				this.LoadHSkilParm();
				yield return this.LoadHPointList();
				foreach (Resources.HSceneTables.HAssetBundle hassetBundle in this.hashUseAssetBundle[0])
				{
					AssetBundleManager.UnloadAssetBundle(hassetBundle.path, false, hassetBundle.manifest, false);
				}
				this.endHLoad = true;
				yield break;
			}

			// Token: 0x060040D8 RID: 16600 RVA: 0x00188088 File Offset: 0x00186488
			public void Release()
			{
				for (int i = 0; i < this.lstAnimInfo.Length; i++)
				{
					this.lstAnimInfo[i] = null;
				}
				this.lstStartAnimInfo.Clear();
				this.lstStartAnimInfoM.Clear();
				this.startWaitAnims.Clear();
				this.lstEndAnimInfo.Clear();
				this.lstHitObject.Clear();
				this.HitObjAtariName.Clear();
				this.DicLstHitObjInfo.Clear();
				foreach (KeyValuePair<int, Dictionary<int, Dictionary<string, GameObject>>> keyValuePair in this.DicHitObject)
				{
					foreach (KeyValuePair<int, Dictionary<string, GameObject>> keyValuePair2 in keyValuePair.Value)
					{
						foreach (KeyValuePair<string, GameObject> keyValuePair3 in keyValuePair2.Value)
						{
							UnityEngine.Object.Destroy(keyValuePair3.Value);
						}
					}
				}
				this.DicHitObject.Clear();
				for (int j = 0; j < this.lstHItemObjInfo.Length; j++)
				{
					this.lstHItemObjInfo[j] = null;
				}
				this.lstHItemBase.Clear();
				this.HitemPathList.Clear();
				if (this.HSceneSet != null)
				{
					HScene componentInChildren = this.HSceneSet.GetComponentInChildren<HScene>(true);
					if (componentInChildren != null)
					{
						componentInChildren.HParticleSetNull();
					}
				}
				this.hPointLists.Clear();
				foreach (Dictionary<string, GameObject> dictionary in this.HPointPrefabs.Values)
				{
					foreach (GameObject obj in dictionary.Values)
					{
						UnityEngine.Object.Destroy(obj);
					}
					dictionary.Clear();
				}
				this.HPointPrefabs.Clear();
				UnityEngine.Object.Destroy(this.AutoHPointPrefabs);
				this.AutoHPointPrefabs = null;
				this.autoHPointDatas = null;
				this.loadHPointDatas.Clear();
				this.AutoHpointData.Clear();
				if (this.HPointObj != null)
				{
					UnityEngine.Object.Destroy(this.HPointObj);
				}
				this.HPointObj = null;
				this.HAutoPathList.Clear();
				this.HAutoInfo = null;
				this.HAutoLeaveItToYou = null;
				this.autoLeavePersonalityRate.Clear();
				this.autoLeaveAttributeRate.Clear();
				this.aHsceneBGM = null;
				this.lstHParticleCtrl.Clear();
				this.hParticle.ReleaseObject();
				this.hParticle = null;
				for (int k = 0; k < this.HBaseRuntimeAnimatorControllers.GetLength(0); k++)
				{
					for (int l = 0; l < this.HBaseRuntimeAnimatorControllers.GetLength(1); l++)
					{
						this.HBaseRuntimeAnimatorControllers[k, l] = null;
					}
				}
				this.HmeshDictionary.Clear();
				this.HMeshObjDic.Clear();
				this.DicDicYure.Clear();
				this.DicDicYureMale.Clear();
				this.DicLstHitInfo.Clear();
				this.DicLstLookAtDan.Clear();
				this.DicLstCollisionInfo.Clear();
				this.LayerInfos.Clear();
				this.HBaseParamTable.Clear();
				this.HactionParamTable.Clear();
				this.HSkileParamTable.Clear();
				this.endHLoad = false;
			}

			// Token: 0x060040D9 RID: 16601 RVA: 0x00188490 File Offset: 0x00186890
			public IEnumerator LoadHObj()
			{
				this.hashUseAssetBundle[1] = new HashSet<Resources.HSceneTables.HAssetBundle>();
				yield return this.LoadHScene();
				yield return this.LoadHsceneBaseRAC();
				this.HparticleInit();
				this.HitObjListInit();
				int mapID = Singleton<Manager.Map>.Instance.MapID;
				this.ChangeMapHpoint(mapID);
				GC.Collect();
				foreach (Resources.HSceneTables.HAssetBundle hassetBundle in this.hashUseAssetBundle[1])
				{
					AssetBundleManager.UnloadAssetBundle(hassetBundle.path, false, hassetBundle.manifest, false);
				}
				yield return null;
				yield break;
			}

			// Token: 0x060040DA RID: 16602 RVA: 0x001884AC File Offset: 0x001868AC
			private IEnumerator LoadAnimationFileName()
			{
				for (int i = 0; i < this.lstAnimInfo.Length; i++)
				{
					this.lstAnimInfo[i] = new List<HScene.AnimationListInfo>();
				}
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetAnimationInfoListFolder, false);
				this.pathList.Sort();
				this.sbAbName.Clear();
				this.excelData = null;
				bool bTokushu = false;
				bool bfemaleMulti = false;
				for (int nLoopCnt = 0; nLoopCnt < this.pathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[nLoopCnt]);
					int abNum = -1;
					string p = System.IO.Path.GetFileNameWithoutExtension(this.pathList[nLoopCnt]);
					if (!int.TryParse(p, out abNum))
					{
						abNum = -1;
					}
					if (abNum < 50 || Game.isAdd50)
					{
						for (int nAssetNameLoopCnt = 0; nAssetNameLoopCnt < this.assetNames.Length; nAssetNameLoopCnt++)
						{
							this.sbAssetName.Clear();
							this.sbAssetName.AppendFormat("{0}_{1}", this.assetNames[nAssetNameLoopCnt], p);
							bool exist = false;
							exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
							if (!exist)
							{
								if (nAssetNameLoopCnt < 3)
								{
									this.LoadAnimationFileNameSP(nAssetNameLoopCnt, this.assetNames[nAssetNameLoopCnt]);
								}
								yield return null;
							}
							else
							{
								this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
								this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
								if (this.excelData == null)
								{
									if (nAssetNameLoopCnt < 3)
									{
										this.LoadAnimationFileNameSP(nAssetNameLoopCnt, this.assetNames[nAssetNameLoopCnt]);
									}
									yield return null;
								}
								else
								{
									bTokushu = (nAssetNameLoopCnt > 2);
									bfemaleMulti = (nAssetNameLoopCnt > 3);
									int excelRowIdx = 1;
									while (excelRowIdx < this.excelData.MaxCell)
									{
										List<ExcelData.Param> list = this.excelData.list;
										int index;
										excelRowIdx = (index = excelRowIdx) + 1;
										this.row = list[index].list;
										int num = 0;
										HScene.AnimationListInfo info = new HScene.AnimationListInfo();
										info.nameAnimation = this.row.GetElement(num++);
										if (int.TryParse(this.row.GetElement(num++), out info.id))
										{
											if (bTokushu)
											{
												info.assetpathBaseM = this.row.GetElement(num++);
												info.assetBaseM = this.row.GetElement(num++);
											}
											info.assetpathMale = this.row.GetElement(num++);
											info.fileMale = this.row.GetElement(num++);
											info.isMaleHitObject = (this.row.GetElement(num++) == "1");
											info.fileMotionNeckMale = this.row.GetElement(num++);
											if (bTokushu)
											{
												info.assetpathBaseF = this.row.GetElement(num++);
												info.assetBaseF = this.row.GetElement(num++);
											}
											info.assetpathFemale = this.row.GetElement(num++);
											info.fileFemale = this.row.GetElement(num++);
											info.isFemaleHitObject = (this.row.GetElement(num++) == "1");
											info.fileMotionNeckFemale = this.row.GetElement(num++);
											if (nAssetNameLoopCnt == 4)
											{
												info.fileMotionNeckFemalePlayer = this.row.GetElement(num++);
											}
											if (bfemaleMulti)
											{
												info.assetpathBaseF2 = this.row.GetElement(num++);
												info.assetBaseF2 = this.row.GetElement(num++);
												info.assetpathFemale2 = this.row.GetElement(num++);
												info.fileFemale2 = this.row.GetElement(num++);
												info.isFemaleHitObject2 = (this.row.GetElement(num++) == "1");
												info.fileMotionNeckFemale2 = this.row.GetElement(num++);
											}
											int.TryParse(this.row.GetElement(num++), out info.ActionCtrl.Item1);
											int.TryParse(this.row.GetElement(num++), out info.ActionCtrl.Item2);
											string tmpbuf = this.row.GetElement(num++);
											if (tmpbuf != string.Empty)
											{
												string[] abuf = tmpbuf.Split(new char[]
												{
													','
												});
												foreach (string s in abuf)
												{
													int item = -1;
													if (int.TryParse(s, out item))
													{
														info.nPositons.Add(item);
													}
												}
											}
											tmpbuf = this.row.GetElement(num++);
											if (!tmpbuf.IsNullOrEmpty())
											{
												string[] abuf = tmpbuf.Split(new char[]
												{
													'/'
												});
												foreach (string item2 in abuf)
												{
													info.lstOffset.Add(item2);
												}
											}
											info.isNeedItem = (this.row.GetElement(num++) == "1");
											int.TryParse(this.row.GetElement(num++), out info.nDownPtn);
											int.TryParse(this.row.GetElement(num++), out info.nFaintnessLimit);
											if (!int.TryParse(this.row.GetElement(num++), out info.nIyaAction))
											{
												info.nIyaAction = 0;
											}
											info.bSleep = (this.row.GetElement(num++) == "1");
											info.bMerchantMotion = (this.row.GetElement(num++) == "1");
											int.TryParse(this.row.GetElement(num++), out info.nHentai);
											int.TryParse(this.row.GetElement(num++), out info.nPhase);
											int.TryParse(this.row.GetElement(num++), out info.nInitiativeFemale);
											int.TryParse(this.row.GetElement(num++), out info.nFemaleProclivity);
											int.TryParse(this.row.GetElement(num++), out info.nBackInitiativeID);
											tmpbuf = this.row.GetElement(num++);
											if (tmpbuf != string.Empty)
											{
												string[] abuf = tmpbuf.Split(new char[]
												{
													','
												});
												foreach (string text in abuf)
												{
													if (text != string.Empty)
													{
														info.lstSystem.Add(int.Parse(text));
													}
												}
											}
											int.TryParse(this.row.GetElement(num++), out info.nMaleSon);
											int.TryParse(this.row.GetElement(num++), out info.nFemaleUpperCloths[0]);
											int.TryParse(this.row.GetElement(num++), out info.nFemaleLowerCloths[0]);
											int.TryParse(this.row.GetElement(num++), out info.nFemaleUpperCloths[1]);
											int.TryParse(this.row.GetElement(num++), out info.nFemaleLowerCloths[1]);
											int.TryParse(this.row.GetElement(num++), out info.nFeelHit);
											info.nameCamera = this.row.GetElement(num++);
											info.fileSiruPaste = this.row.GetElement(num++);
											if (nAssetNameLoopCnt == 5)
											{
												info.fileSiruPasteSecond = this.row.GetElement(num++);
											}
											info.fileSe = this.row.GetElement(num++);
											string element = this.row.GetElement(num++);
											if (element != string.Empty)
											{
												int.TryParse(element, out info.nShortBreahtPlay);
											}
											tmpbuf = this.row.GetElement(num++);
											if (tmpbuf != string.Empty)
											{
												string[] abuf = tmpbuf.Split(new char[]
												{
													','
												});
												foreach (string text2 in abuf)
												{
													if (!(text2 == string.Empty))
													{
														info.hasVoiceCategory.Add(int.Parse(text2));
													}
												}
											}
											element = this.row.GetElement(num++);
											if (element != string.Empty)
											{
												int.TryParse(element, out info.nPromiscuity);
											}
											tmpbuf = this.row.GetElement(num++);
											if (tmpbuf != string.Empty)
											{
												string[] abuf = tmpbuf.Split(new char[]
												{
													','
												});
												if (abuf.Length == 2)
												{
													int.TryParse(abuf[0], out info.nAnimListInfoID);
													int.TryParse(abuf[1], out info.nBreathID);
												}
											}
											int num2 = this.CheckAnimationInfoList(this.lstAnimInfo[nAssetNameLoopCnt], info.id);
											if (num2 < 0)
											{
												this.lstAnimInfo[nAssetNameLoopCnt].Add(info);
											}
											else
											{
												this.lstAnimInfo[nAssetNameLoopCnt][num2] = info;
											}
										}
									}
									if (nAssetNameLoopCnt < 3)
									{
										this.LoadAnimationFileNameSP(nAssetNameLoopCnt, this.assetNames[nAssetNameLoopCnt]);
									}
									yield return null;
								}
							}
						}
						yield return null;
					}
				}
				yield break;
			}

			// Token: 0x060040DB RID: 16603 RVA: 0x001884C8 File Offset: 0x001868C8
			private void LoadAnimationFileNameSP(int taiiKind, string assetName)
			{
				this.sbAssetName.Clear();
				this.sbAssetName.AppendFormat("{0}_{1}_sp", assetName, System.IO.Path.GetFileNameWithoutExtension(this.sbAbName.ToString()));
				if (!GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty))
				{
					return;
				}
				this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
				this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
				if (this.excelData == null)
				{
					return;
				}
				int i = 1;
				while (i < this.excelData.MaxCell)
				{
					this.row = this.excelData.list[i++].list;
					int num = 0;
					HScene.AnimationListInfo animationListInfo = new HScene.AnimationListInfo();
					animationListInfo.nameAnimation = this.row.GetElement(num++);
					if (int.TryParse(this.row.GetElement(num++), out animationListInfo.id))
					{
						animationListInfo.assetpathBaseM = this.row.GetElement(num++);
						animationListInfo.assetBaseM = this.row.GetElement(num++);
						animationListInfo.assetpathMale = this.row.GetElement(num++);
						animationListInfo.fileMale = this.row.GetElement(num++);
						animationListInfo.isMaleHitObject = (this.row.GetElement(num++) == "1");
						animationListInfo.fileMotionNeckMale = this.row.GetElement(num++);
						animationListInfo.assetpathBaseF = this.row.GetElement(num++);
						animationListInfo.assetBaseF = this.row.GetElement(num++);
						animationListInfo.assetpathFemale = this.row.GetElement(num++);
						animationListInfo.fileFemale = this.row.GetElement(num++);
						animationListInfo.isFemaleHitObject = (this.row.GetElement(num++) == "1");
						animationListInfo.fileMotionNeckFemale = this.row.GetElement(num++);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.ActionCtrl.Item1);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.ActionCtrl.Item2);
						string element = this.row.GetElement(num++);
						if (element != string.Empty)
						{
							string[] array = element.Split(new char[]
							{
								','
							});
							foreach (string s in array)
							{
								int item = -1;
								if (int.TryParse(s, out item))
								{
									animationListInfo.nPositons.Add(item);
								}
							}
						}
						element = this.row.GetElement(num++);
						if (!element.IsNullOrEmpty())
						{
							string[] array = element.Split(new char[]
							{
								'/'
							});
							foreach (string item2 in array)
							{
								animationListInfo.lstOffset.Add(item2);
							}
						}
						animationListInfo.isNeedItem = (this.row.GetElement(num++) == "1");
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nDownPtn);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nFaintnessLimit);
						if (!int.TryParse(this.row.GetElement(num++), out animationListInfo.nIyaAction))
						{
							animationListInfo.nIyaAction = 0;
						}
						animationListInfo.bSleep = (this.row.GetElement(num++) == "1");
						animationListInfo.bMerchantMotion = (this.row.GetElement(num++) == "1");
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nHentai);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nPhase);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nInitiativeFemale);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nFemaleProclivity);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nBackInitiativeID);
						element = this.row.GetElement(num++);
						if (element != string.Empty)
						{
							string[] array = element.Split(new char[]
							{
								','
							});
							foreach (string text in array)
							{
								if (text != string.Empty)
								{
									animationListInfo.lstSystem.Add(int.Parse(text));
								}
							}
						}
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nMaleSon);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nFemaleUpperCloths[0]);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nFemaleLowerCloths[0]);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nFemaleUpperCloths[1]);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nFemaleLowerCloths[1]);
						int.TryParse(this.row.GetElement(num++), out animationListInfo.nFeelHit);
						animationListInfo.nameCamera = this.row.GetElement(num++);
						animationListInfo.fileSiruPaste = this.row.GetElement(num++);
						animationListInfo.fileSe = this.row.GetElement(num++);
						string element2 = this.row.GetElement(num++);
						if (element2 != string.Empty)
						{
							int.TryParse(element2, out animationListInfo.nShortBreahtPlay);
						}
						element = this.row.GetElement(num++);
						if (element != string.Empty)
						{
							string[] array = element.Split(new char[]
							{
								','
							});
							foreach (string text2 in array)
							{
								if (!(text2 == string.Empty))
								{
									animationListInfo.hasVoiceCategory.Add(int.Parse(text2));
								}
							}
						}
						element2 = this.row.GetElement(num++);
						if (element2 != string.Empty)
						{
							int.TryParse(element2, out animationListInfo.nPromiscuity);
						}
						element = this.row.GetElement(num++);
						if (element != string.Empty)
						{
							string[] array = element.Split(new char[]
							{
								','
							});
							if (array.Length == 2)
							{
								int.TryParse(array[0], out animationListInfo.nAnimListInfoID);
								int.TryParse(array[1], out animationListInfo.nBreathID);
							}
						}
						int num2 = this.CheckAnimationInfoList(this.lstAnimInfo[taiiKind], animationListInfo.id);
						if (num2 < 0)
						{
							this.lstAnimInfo[taiiKind].Add(animationListInfo);
						}
						else
						{
							this.lstAnimInfo[taiiKind][num2] = animationListInfo;
						}
					}
				}
			}

			// Token: 0x060040DC RID: 16604 RVA: 0x00188CD8 File Offset: 0x001870D8
			private IEnumerator LoadStartAnimationList(bool merchant = false)
			{
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetStartAnimationListFolder, false);
				this.pathList.Sort();
				this.sbAbName.Clear();
				this.excelData = null;
				for (int nLoopCnt = 0; nLoopCnt < this.pathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[nLoopCnt]);
					this.sbAssetName.Clear();
					if (!merchant)
					{
						this.sbAssetName.Append(System.IO.Path.GetFileNameWithoutExtension(this.pathList[nLoopCnt]));
					}
					else
					{
						this.sbAssetName.Append("merchant");
					}
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							int excelRowIdx = 1;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								int num = 0;
								int i = -1;
								if (int.TryParse(this.row.GetElement(num++), out i))
								{
									int i2 = -1;
									int.TryParse(this.row.GetElement(num++), out i2);
									int mode = -1;
									int.TryParse(this.row.GetElement(num++), out mode);
									int id = -1;
									int.TryParse(this.row.GetElement(num++), out id);
									UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion> info = new UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>((HSceneManager.HEvent)i, i2, new HScene.StartMotion(mode, id));
									if (!merchant)
									{
										this.lstStartAnimInfo.Add(info);
									}
									else
									{
										this.lstStartAnimInfoM.Add(info);
									}
								}
							}
							yield return null;
						}
					}
				}
				yield break;
			}

			// Token: 0x060040DD RID: 16605 RVA: 0x00188CFC File Offset: 0x001870FC
			private IEnumerator LoadEndAnimationList()
			{
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetEndAnimationInfoFolder, false);
				this.pathList.Sort();
				this.sbAbName.Clear();
				string[] abNames = new string[5];
				string[] assetNames = new string[5];
				string[] stateNames = new string[5];
				this.excelData = null;
				for (int nLoopCnt = 0; nLoopCnt < this.pathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[nLoopCnt]);
					this.sbAssetName.Clear();
					this.sbAssetName.Append(System.IO.Path.GetFileNameWithoutExtension(this.pathList[nLoopCnt]));
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							int excelRowIdx = 3;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								int num = 0;
								int height = 0;
								int.TryParse(this.row.GetElement(num++), out height);
								for (int i = 0; i < 5; i++)
								{
									abNames[i] = this.row.GetElement(num++);
									assetNames[i] = this.row.GetElement(num++);
									stateNames[i] = this.row.GetElement(num++);
								}
								HScene.EndMotion info = new HScene.EndMotion(height, abNames, assetNames, stateNames);
								this.lstEndAnimInfo.Add(info);
							}
							yield return null;
						}
					}
				}
				yield break;
			}

			// Token: 0x060040DE RID: 16606 RVA: 0x00188D18 File Offset: 0x00187118
			private IEnumerator LoadHItemObjInfo()
			{
				for (int i = 0; i < this.lstAnimInfo.Length; i++)
				{
					this.lstHItemObjInfo[i] = new List<Dictionary<int, List<HItemCtrl.ListItem>>>();
				}
				this.HitemPathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetHItemObjInfoListFolder, false);
				this.HitemPathList.Sort();
				this.sbAbName.Clear();
				this.excelData = null;
				for (int nLoopCnt = 0; nLoopCnt < this.HitemPathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.HitemPathList[nLoopCnt]);
					for (int nAssetNameLoopCnt = 0; nAssetNameLoopCnt < this.assetNames.Length; nAssetNameLoopCnt++)
					{
						Dictionary<int, List<HItemCtrl.ListItem>> info = new Dictionary<int, List<HItemCtrl.ListItem>>();
						this.sbAssetName.Clear();
						this.sbAssetName.AppendFormat("{00}_{01}", this.assetNames[nAssetNameLoopCnt], System.IO.Path.GetFileNameWithoutExtension(this.HitemPathList[nLoopCnt]));
						bool exist = false;
						exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
						if (!exist)
						{
							yield return null;
						}
						else
						{
							this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
							this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
							if (this.excelData == null)
							{
								yield return null;
							}
							else
							{
								int excelRowIdx = 1;
								while (excelRowIdx < this.excelData.MaxCell)
								{
									List<ExcelData.Param> list = this.excelData.list;
									int index;
									excelRowIdx = (index = excelRowIdx) + 1;
									this.row = list[index].list;
									int num = 1;
									int key = 0;
									if (int.TryParse(this.row[num++], out key))
									{
										if (!info.ContainsKey(key))
										{
											info.Add(key, new List<HItemCtrl.ListItem>());
										}
										HItemCtrl.ListItem listItem = new HItemCtrl.ListItem();
										if (!int.TryParse(this.row.GetElement(num++), out listItem.itemkind))
										{
											listItem.itemkind = -1;
										}
										if (!int.TryParse(this.row.GetElement(num++), out listItem.itemID))
										{
											listItem.itemID = -1;
										}
										listItem.nameManifest = this.row.GetElement(num++);
										string element = this.row.GetElement(num++);
										if (element == null || element == string.Empty)
										{
											break;
										}
										listItem.pathAssetObject = element;
										listItem.nameObject = this.row.GetElement(num++);
										listItem.pathAssetAnimatorBase = this.row.GetElement(num++);
										listItem.nameAnimatorBase = this.row.GetElement(num++);
										listItem.pathAssetAnimator = this.row.GetElement(num++);
										listItem.nameAnimator = this.row.GetElement(num++);
										do
										{
											HItemCtrl.ParentInfo parentInfo = new HItemCtrl.ParentInfo();
											int num2 = num;
											element = this.row.GetElement(num2 + 1);
											if (element == string.Empty)
											{
												break;
											}
											parentInfo.isParentMode = (this.row.GetElement(num++) == "0");
											parentInfo.numToWhomParent = int.Parse(element);
											num++;
											parentInfo.nameParent = this.row.GetElement(num++);
											parentInfo.nameSelf = this.row.GetElement(num++);
											parentInfo.isParentScale = (this.row.GetElement(num++) == "1");
											listItem.lstParent.Add(parentInfo);
										}
										while (num < this.row.Count);
										IL_521:
										info[key].Add(listItem);
										continue;
										goto IL_521;
									}
								}
								this.lstHItemObjInfo[nAssetNameLoopCnt].Add(info);
							}
						}
					}
				}
				yield return null;
				yield break;
			}

			// Token: 0x060040DF RID: 16607 RVA: 0x00188D34 File Offset: 0x00187134
			private IEnumerator LoadHItemBaseAnim()
			{
				this.sbAbName.Clear();
				List<UnityEx.ValueTuple<string, string>> assetNames = new List<UnityEx.ValueTuple<string, string>>();
				foreach (List<Dictionary<int, List<HItemCtrl.ListItem>>> categoryinfo in this.lstHItemObjInfo)
				{
					foreach (Dictionary<int, List<HItemCtrl.ListItem>> sheat in categoryinfo)
					{
						foreach (List<HItemCtrl.ListItem> list in sheat.Values)
						{
							foreach (HItemCtrl.ListItem listItem in list)
							{
								UnityEx.ValueTuple<string, string> tmp;
								tmp.Item1 = listItem.pathAssetAnimatorBase;
								tmp.Item2 = listItem.nameAnimatorBase;
								if (!assetNames.Contains(tmp))
								{
									assetNames.Add(tmp);
								}
							}
						}
						yield return null;
					}
					yield return null;
				}
				foreach (UnityEx.ValueTuple<string, string> asset in assetNames)
				{
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(asset.Item1, asset.Item2, string.Empty);
					if (exist)
					{
						this.tmpHItemRuntimeAnimator = CommonLib.LoadAsset<RuntimeAnimatorController>(asset.Item1, asset.Item2, false, string.Empty);
						this.lstHItemBase.Add(new UnityEx.ValueTuple<string, RuntimeAnimatorController>(asset.Item2, this.tmpHItemRuntimeAnimator));
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(asset.Item1, string.Empty));
						yield return null;
					}
				}
				yield break;
			}

			// Token: 0x060040E0 RID: 16608 RVA: 0x00188D50 File Offset: 0x00187150
			private IEnumerator LoadHPointList()
			{
				this.sbAbName.Clear();
				this.sbAbName.Append(Singleton<HSceneManager>.Instance.strAssetHpointPrefabListFolder);
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(this.sbAbName.ToString(), false);
				this.pathList.Sort();
				this.sbAbName.Clear();
				this.excelData = null;
				StringBuilder[] array = new StringBuilder[]
				{
					new StringBuilder(),
					new StringBuilder()
				};
				for (int nLoopCnt = 0; nLoopCnt < this.pathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[nLoopCnt]);
					this.sbAssetName.Clear();
					this.sbAssetName.AppendFormat("HPointPrefab_{00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[nLoopCnt]));
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							int excelRowIdx = 1;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								int num = 0;
								int key = 0;
								if (int.TryParse(this.row[num++], out key))
								{
									HPointList.LoadInfo info = new HPointList.LoadInfo();
									info.Path = this.row[num++];
									info.Name = this.row[num++];
									info.Manifest = this.row[num++];
									if (!this.hPointListInfos.ContainsKey(key))
									{
										this.hPointListInfos.Add(key, info);
									}
									else
									{
										this.hPointListInfos[key] = info;
									}
								}
							}
							yield return null;
						}
					}
				}
				yield break;
			}

			// Token: 0x060040E1 RID: 16609 RVA: 0x00188D6C File Offset: 0x0018716C
			public void LoadHpointPrefabs(int mapID)
			{
				if (this.hPointListInfos.Count == 0)
				{
					return;
				}
				HPointList.LoadInfo loadInfo;
				if (!this.hPointListInfos.TryGetValue(mapID, out loadInfo))
				{
					return;
				}
				if (!this.hPointLists.ContainsKey(mapID))
				{
					if (this.HPointPrefabs == null)
					{
						this.HPointPrefabs = new Dictionary<int, Dictionary<string, GameObject>>();
					}
					if (!this.HPointPrefabs.ContainsKey(mapID))
					{
						this.HPointPrefabs.Add(mapID, new Dictionary<string, GameObject>());
					}
					if (!this.HPointPrefabs[mapID].ContainsKey(loadInfo.Path))
					{
						this.HPointPrefabs[mapID].Add(loadInfo.Name, null);
					}
					this.HPointPrefabs[mapID][loadInfo.Name] = CommonLib.LoadAsset<GameObject>(loadInfo.Path, loadInfo.Name, true, loadInfo.Manifest);
					if (this.HPointPrefabs[mapID][loadInfo.Name] == null)
					{
						return;
					}
					GameObject gameObject = this.HPointPrefabs[mapID][loadInfo.Name];
					Transform parent = (!(this.commonSpace == null)) ? this.commonSpace.transform : GameObject.Find("CommonSpace").transform;
					gameObject.transform.SetParent(parent, false);
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.identity;
					this.hPointLists.Add(mapID, null);
					HPointList component = gameObject.GetComponent<HPointList>();
					if (component != null)
					{
						component.Init();
						if (this.hPointLists[mapID] == null)
						{
							this.hPointLists[mapID] = component;
						}
						else
						{
							if (this.hPointLists[mapID].lst == null)
							{
								this.hPointLists[mapID].lst = new Dictionary<int, List<HPoint>>();
							}
							foreach (KeyValuePair<int, List<HPoint>> keyValuePair in component.lst)
							{
								if (!this.hPointLists[mapID].lst.ContainsKey(keyValuePair.Key))
								{
									this.hPointLists[mapID].lst.Add(keyValuePair.Key, keyValuePair.Value);
								}
								else
								{
									this.hPointLists[mapID].lst[keyValuePair.Key] = keyValuePair.Value;
								}
							}
						}
					}
				}
				AssetBundleManager.UnloadAssetBundle(loadInfo.Path, false, loadInfo.Manifest, false);
			}

			// Token: 0x060040E2 RID: 16610 RVA: 0x0018902C File Offset: 0x0018742C
			private IEnumerator LoadHPointInfo()
			{
				this.sbAbName.Clear();
				this.sbAbName.Append(Singleton<HSceneManager>.Instance.strAssetHpointListFolder);
				this.sbAssetName.Clear();
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(this.sbAbName.ToString(), false);
				this.pathList.Sort();
				this.excelData = null;
				List<int>[] notList = new List<int>[6];
				for (int i = 0; i < this.pathList.Count; i++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[i]);
					this.sbAssetName.Clear();
					this.sbAssetName.Append("Hpoint_");
					this.sbAssetName.Append(System.IO.Path.GetFileNameWithoutExtension(this.pathList[i]));
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							int excelRowIdx = 1;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								int num = 0;
								int key = -1;
								if (int.TryParse(this.row[num++], out key))
								{
									num++;
									if (!this.loadHPointDatas.ContainsKey(key))
									{
										this.loadHPointDatas.Add(key, new HPoint.HpointData());
									}
									int item = 0;
									int key2 = 0;
									int item2 = 0;
									int.TryParse(this.row[num++], out item);
									int.TryParse(this.row[num++], out key2);
									int.TryParse(this.row[num++], out item2);
									UnityEx.ValueTuple<int, int> tmp;
									tmp.Item1 = item;
									tmp.Item2 = item2;
									for (int j = 0; j < 6; j++)
									{
										int num2 = j;
										notList[num2] = new List<int>();
										if (num < this.row.Count)
										{
											string[] buf = this.row[num++].Split(new char[]
											{
												','
											});
											for (int k = 0; k < buf.Length; k++)
											{
												int item3 = -1;
												if (int.TryParse(buf[k], out item3))
												{
													notList[num2].Add(item3);
												}
											}
										}
									}
									if (!this.loadHPointDatas[key].place.ContainsKey(key2))
									{
										this.loadHPointDatas[key].place.Add(key2, tmp);
									}
									else
									{
										this.loadHPointDatas[key].place[key2] = tmp;
									}
									for (int l = 0; l < this.loadHPointDatas[key].notMotion.Length; l++)
									{
										if (this.loadHPointDatas[key].notMotion[l].motionID == null || this.loadHPointDatas[key].notMotion[l].motionID.Count == 0)
										{
											this.loadHPointDatas[key].notMotion[l].motionID = new List<int>(notList[l]);
										}
										else
										{
											this.loadHPointDatas[key].notMotion[l].motionID.AddRange(notList[l]);
										}
									}
								}
							}
							GC.Collect();
							AssetBundleManager.UnloadAssetBundle(this.sbAbName.ToString(), true, null, false);
							yield return null;
						}
					}
				}
				yield break;
			}

			// Token: 0x060040E3 RID: 16611 RVA: 0x00189048 File Offset: 0x00187448
			public IEnumerator LoadAutoHPointPath()
			{
				this.sbAbName.Clear();
				this.sbAbName.Append(Singleton<HSceneManager>.Instance.strAssetHpointListFolder);
				this.sbAssetName.Clear();
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(this.sbAbName.ToString(), false);
				this.pathList.Sort();
				this.excelData = null;
				for (int i = 0; i < this.pathList.Count; i++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[i]);
					this.sbAssetName.Clear();
					this.sbAssetName.Append("AutoHpoint_");
					this.sbAssetName.Append(System.IO.Path.GetFileNameWithoutExtension(this.pathList[i]));
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							int excelRowIdx = 1;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								int num = 0;
								UnityEx.ValueTuple<string, string> tmp;
								tmp.Item1 = this.row[num++];
								tmp.Item2 = this.row[num++];
								int key = 0;
								if (this.row.Count >= num || !int.TryParse(this.row[num++], out key))
								{
									key = 0;
								}
								if (!this.AutoHpointData.ContainsKey(key))
								{
									this.AutoHpointData.Add(key, new List<UnityEx.ValueTuple<string, string>>());
								}
								this.AutoHpointData[key].Add(tmp);
							}
						}
					}
				}
				yield return null;
				yield break;
			}

			// Token: 0x060040E4 RID: 16612 RVA: 0x00189064 File Offset: 0x00187464
			public void LoadAutoHPoint(int mapID)
			{
				this.pathList.Clear();
				this.pathList = CommonLib.GetAssetBundleNameListFromPath("list/h/hpoint/prefab/", false);
				this.autoHPointDatas = new Dictionary<int, AutoHPointData>();
				GameObject gameObject = null;
				for (int i = 0; i < this.pathList.Count; i++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[i]);
					if (GlobalMethod.AssetFileExist(this.sbAbName.ToString(), "Hpointobj", string.Empty))
					{
						if (this.HPointObj != null)
						{
							UnityEngine.Object.Destroy(this.HPointObj);
							this.HPointObj = null;
						}
						gameObject = CommonLib.LoadAsset<GameObject>(this.sbAbName.ToString(), "Hpointobj", false, string.Empty);
						this.hashUseAssetBundle[1].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
					}
				}
				Transform parent = (!(this.commonSpace == null)) ? this.commonSpace.transform : GameObject.Find("CommonSpace").transform;
				if (this.HPointObj == null && gameObject != null)
				{
					this.HPointObj = UnityEngine.Object.Instantiate<GameObject>(gameObject, parent);
					this.HPointObj.GetComponent<HPoint>().Init();
				}
				if (gameObject == null)
				{
					return;
				}
				if (!this.AutoHpointData.ContainsKey(mapID))
				{
					return;
				}
				if (this.AutoHPointPrefabs == null)
				{
					this.AutoHPointPrefabs = new GameObject("AutoHpoints");
				}
				GameObject autoHPointPrefabs = this.AutoHPointPrefabs;
				autoHPointPrefabs.transform.SetParent(parent, false);
				autoHPointPrefabs.transform.localPosition = Vector3.zero;
				autoHPointPrefabs.transform.localRotation = Quaternion.identity;
				for (int j = 0; j < this.AutoHpointData[mapID].Count; j++)
				{
					if (GlobalMethod.AssetFileExist(this.AutoHpointData[mapID][j].Item1, this.AutoHpointData[mapID][j].Item2, string.Empty))
					{
						AutoHPointData autoHPointData = CommonLib.LoadAsset<AutoHPointData>(this.AutoHpointData[mapID][j].Item1, this.AutoHpointData[mapID][j].Item2, false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.AutoHpointData[mapID][j].Item1, string.Empty));
						if (!(autoHPointData == null))
						{
							if (!this.autoHPointDatas.ContainsKey(mapID))
							{
								this.autoHPointDatas.Add(mapID, autoHPointData);
							}
							else
							{
								this.autoHPointDatas[mapID] = autoHPointData;
							}
							if (autoHPointData.Points != null)
							{
								foreach (KeyValuePair<string, List<UnityEx.ValueTuple<int, Vector3>>> keyValuePair in autoHPointData.Points)
								{
									int num = this.AutoHpointID(keyValuePair.Key);
									if (num >= 0)
									{
										foreach (UnityEx.ValueTuple<int, Vector3> valueTuple in keyValuePair.Value)
										{
											GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
											gameObject2.transform.SetParent(autoHPointPrefabs.transform, false);
											gameObject2.transform.position = valueTuple.Item2;
											gameObject2.transform.rotation = Quaternion.identity;
											gameObject2.GetComponent<HPoint>().id = num;
											if (!this.hPointLists.ContainsKey(mapID))
											{
												this.hPointLists.Add(mapID, new GameObject("HPointLists").AddComponent<HPointList>());
												this.hPointLists[mapID].lst = new Dictionary<int, List<HPoint>>();
											}
											if (!this.hPointLists[mapID].lst.ContainsKey(valueTuple.Item1))
											{
												this.hPointLists[mapID].lst.Add(valueTuple.Item1, new List<HPoint>());
											}
											this.hPointLists[mapID].lst[valueTuple.Item1].Add(gameObject2.GetComponent<HPoint>());
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060040E5 RID: 16613 RVA: 0x00189554 File Offset: 0x00187954
			private void HPointInitData(int mapID)
			{
				foreach (KeyValuePair<int, List<HPoint>> keyValuePair in this.hPointLists[mapID].lst)
				{
					foreach (HPoint hpoint in keyValuePair.Value)
					{
						hpoint.Init();
					}
				}
			}

			// Token: 0x060040E6 RID: 16614 RVA: 0x00189600 File Offset: 0x00187A00
			private IEnumerator LoadHsceneParticle()
			{
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetHParticleListFolder, false);
				this.pathList.Sort();
				this.sbAbName.Clear();
				this.excelData = null;
				for (int nLoopCnt = 0; nLoopCnt < this.pathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[nLoopCnt]);
					this.sbAssetName.Clear();
					this.sbAssetName.Append(System.IO.Path.GetFileNameWithoutExtension(this.pathList[nLoopCnt]));
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							int excelRowIdx = 1;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								int num = 1;
								HParticleCtrl.ParticleInfo info = new HParticleCtrl.ParticleInfo();
								info.assetPath = this.row.GetElement(num++);
								info.file = this.row.GetElement(num++);
								info.manifest = this.row.GetElement(num++);
								info.numParent = int.Parse(this.row.GetElement(num++));
								info.nameParent = this.row.GetElement(num++);
								info.pos = new Vector3(float.Parse(this.row.GetElement(num++)), float.Parse(this.row.GetElement(num++)), float.Parse(this.row.GetElement(num++)));
								info.rot = new Vector3(float.Parse(this.row.GetElement(num++)), float.Parse(this.row.GetElement(num++)), float.Parse(this.row.GetElement(num++)));
								this.lstHParticleCtrl.Add(info);
							}
						}
					}
				}
				yield break;
			}

			// Token: 0x060040E7 RID: 16615 RVA: 0x0018961C File Offset: 0x00187A1C
			private IEnumerator LoadHsceneBaseRAC()
			{
				for (int nSexLoopCnt = 0; nSexLoopCnt < this.HBaseRuntimeAnimatorControllers.GetLength(0); nSexLoopCnt++)
				{
					for (int nCategoryLoopCnt = 0; nCategoryLoopCnt < this.HBaseRuntimeAnimatorControllers.GetLength(1); nCategoryLoopCnt++)
					{
						bool exist = false;
						exist = GlobalMethod.AssetFileExist(this.strAssetAnimatorBase[nSexLoopCnt, nCategoryLoopCnt], this.racBaseNames[nSexLoopCnt, nCategoryLoopCnt], string.Empty);
						if (!exist)
						{
							yield return null;
						}
						else
						{
							this.HBaseRuntimeAnimatorControllers[nSexLoopCnt, nCategoryLoopCnt] = CommonLib.LoadAsset<RuntimeAnimatorController>(this.strAssetAnimatorBase[nSexLoopCnt, nCategoryLoopCnt], this.racBaseNames[nSexLoopCnt, nCategoryLoopCnt], false, string.Empty);
							yield return null;
							AssetBundleManager.UnloadAssetBundle(this.strAssetAnimatorBase[nSexLoopCnt, nCategoryLoopCnt], false, null, false);
						}
					}
				}
				yield break;
			}

			// Token: 0x060040E8 RID: 16616 RVA: 0x00189638 File Offset: 0x00187A38
			private IEnumerator LoadHAutoInfo()
			{
				this.sbAssetName.Clear();
				this.sbAssetName.Append("hAuto");
				this.excelData = null;
				bool exist = false;
				exist = GlobalMethod.AssetFileExist(Singleton<HSceneManager>.Instance.strAssetHAutoListFolder, this.sbAssetName.ToString(), string.Empty);
				if (!exist)
				{
					yield break;
				}
				this.excelData = CommonLib.LoadAsset<ExcelData>(Singleton<HSceneManager>.Instance.strAssetHAutoListFolder, this.sbAssetName.ToString(), false, string.Empty);
				this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(Singleton<HSceneManager>.Instance.strAssetHAutoListFolder, string.Empty));
				if (this.excelData == null)
				{
					yield break;
				}
				this.HAutoInfo = new HAutoCtrl.HAutoInfo();
				int excelRowIdx = 2;
				while (excelRowIdx < this.excelData.MaxCell)
				{
					List<ExcelData.Param> list = this.excelData.list;
					int index;
					excelRowIdx = (index = excelRowIdx) + 1;
					this.row = list[index].list;
					int c = 0;
					HAutoCtrl.AutoTime start = this.HAutoInfo.start;
					List<string> source = this.row;
					c = (index = c) + 1;
					float x = float.Parse(source.GetElement(index));
					List<string> source2 = this.row;
					c = (index = c) + 1;
					start.minmax = new Vector2(x, float.Parse(source2.GetElement(index)));
					this.HAutoInfo.start.time = UnityEngine.Random.Range(this.HAutoInfo.start.minmax.x, this.HAutoInfo.start.minmax.y);
					HAutoCtrl.AutoTime reStart = this.HAutoInfo.reStart;
					List<string> source3 = this.row;
					c = (index = c) + 1;
					float x2 = float.Parse(source3.GetElement(index));
					List<string> source4 = this.row;
					c = (index = c) + 1;
					reStart.minmax = new Vector2(x2, float.Parse(source4.GetElement(index)));
					this.HAutoInfo.reStart.time = UnityEngine.Random.Range(this.HAutoInfo.reStart.minmax.x, this.HAutoInfo.reStart.minmax.y);
					HAutoCtrl.AutoTime speed = this.HAutoInfo.speed;
					List<string> source5 = this.row;
					c = (index = c) + 1;
					float x3 = float.Parse(source5.GetElement(index));
					List<string> source6 = this.row;
					c = (index = c) + 1;
					speed.minmax = new Vector2(x3, float.Parse(source6.GetElement(index)));
					this.HAutoInfo.speed.time = UnityEngine.Random.Range(this.HAutoInfo.speed.minmax.x, this.HAutoInfo.speed.minmax.y);
					HAutoCtrl.HAutoInfo hautoInfo = this.HAutoInfo;
					List<string> source7 = this.row;
					c = (index = c) + 1;
					hautoInfo.lerpTimeSpeed = float.Parse(source7.GetElement(index));
					HAutoCtrl.AutoTime loopChange = this.HAutoInfo.loopChange;
					List<string> source8 = this.row;
					c = (index = c) + 1;
					float x4 = float.Parse(source8.GetElement(index));
					List<string> source9 = this.row;
					c = (index = c) + 1;
					loopChange.minmax = new Vector2(x4, float.Parse(source9.GetElement(index)));
					this.HAutoInfo.loopChange.time = UnityEngine.Random.Range(this.HAutoInfo.loopChange.minmax.x, this.HAutoInfo.loopChange.minmax.y);
					HAutoCtrl.AutoTime motionChange = this.HAutoInfo.motionChange;
					List<string> source10 = this.row;
					c = (index = c) + 1;
					float x5 = float.Parse(source10.GetElement(index));
					List<string> source11 = this.row;
					c = (index = c) + 1;
					motionChange.minmax = new Vector2(x5, float.Parse(source11.GetElement(index)));
					this.HAutoInfo.motionChange.time = UnityEngine.Random.Range(this.HAutoInfo.motionChange.minmax.x, this.HAutoInfo.motionChange.minmax.y);
					HAutoCtrl.HAutoInfo hautoInfo2 = this.HAutoInfo;
					List<string> source12 = this.row;
					c = (index = c) + 1;
					hautoInfo2.rateWeakLoop = int.Parse(source12.GetElement(index));
					HAutoCtrl.HAutoInfo hautoInfo3 = this.HAutoInfo;
					List<string> source13 = this.row;
					c = (index = c) + 1;
					hautoInfo3.rateHit = int.Parse(source13.GetElement(index));
					HAutoCtrl.HAutoInfo hautoInfo4 = this.HAutoInfo;
					List<string> source14 = this.row;
					c = (index = c) + 1;
					hautoInfo4.rateAddMotionChange = float.Parse(source14.GetElement(index));
					HAutoCtrl.HAutoInfo hautoInfo5 = this.HAutoInfo;
					List<string> source15 = this.row;
					c = (index = c) + 1;
					hautoInfo5.rateRestartMotionChange = int.Parse(source15.GetElement(index));
					List<string> source16 = this.row;
					c = (index = c) + 1;
					float time = float.Parse(source16.GetElement(index));
					this.HAutoInfo.pull.minmax = new Vector2(time, time);
					this.HAutoInfo.pull.time = UnityEngine.Random.Range(this.HAutoInfo.motionChange.minmax.x, this.HAutoInfo.motionChange.minmax.y);
					HAutoCtrl.HAutoInfo hautoInfo6 = this.HAutoInfo;
					List<string> source17 = this.row;
					c = (index = c) + 1;
					hautoInfo6.rateInsertPull = float.Parse(source17.GetElement(index));
					HAutoCtrl.HAutoInfo hautoInfo7 = this.HAutoInfo;
					List<string> source18 = this.row;
					c = (index = c) + 1;
					hautoInfo7.rateNotInsertPull = float.Parse(source18.GetElement(index));
					yield return null;
				}
				GC.Collect();
				yield break;
			}

			// Token: 0x060040E9 RID: 16617 RVA: 0x00189654 File Offset: 0x00187A54
			private IEnumerator LoadAutoLeaveItToYou()
			{
				this.HAutoPathList = new List<string>();
				this.HAutoPathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetLeaveItToYouFolder, false);
				this.HAutoPathList.Sort();
				this.sbAbName.Clear();
				this.sbAssetName.Clear();
				this.sbAssetName.Append("LeaveItToYou");
				this.excelData = null;
				for (int nLoopCnt = 0; nLoopCnt < this.HAutoPathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.HAutoPathList[nLoopCnt]);
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							this.HAutoLeaveItToYou = new HAutoCtrl.AutoLeaveItToYou();
							int excelRowIdx = 2;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								int num = 0;
								this.HAutoLeaveItToYou.time.minmax = new Vector2(float.Parse(this.row.GetElement(num++)), float.Parse(this.row.GetElement(num++)));
								this.HAutoLeaveItToYou.time.Reset();
								this.HAutoLeaveItToYou.baseTime = this.HAutoLeaveItToYou.time.minmax;
								this.HAutoLeaveItToYou.rate = int.Parse(this.row.GetElement(num++));
							}
							yield return null;
						}
					}
				}
				GC.Collect();
				yield break;
			}

			// Token: 0x060040EA RID: 16618 RVA: 0x00189670 File Offset: 0x00187A70
			private IEnumerator LoadAutoLeaveItToYouPersonality()
			{
				this.excelData = null;
				for (int nLoopCnt = 0; nLoopCnt < this.HAutoPathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.HAutoPathList[nLoopCnt]);
					this.sbAssetName.Clear();
					this.sbAssetName.AppendFormat("LeaveItToYou_personality_{00}", System.IO.Path.GetFileNameWithoutExtension(this.HAutoPathList[nLoopCnt]));
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (exist)
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(this.sbAbName.ToString());
						if (!(this.excelData == null))
						{
							int excelRowIdx = 1;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								int num = 1;
								int key = 0;
								if (int.TryParse(this.row.GetElement(num++), out key))
								{
									float value = 1f;
									if (!float.TryParse(this.row.GetElement(num++), out value))
									{
										value = 1f;
									}
									if (!this.autoLeavePersonalityRate.ContainsKey(key))
									{
										this.autoLeavePersonalityRate.Add(key, value);
									}
									else
									{
										this.autoLeavePersonalityRate[key] = value;
									}
								}
							}
							yield return null;
						}
					}
				}
				yield break;
			}

			// Token: 0x060040EB RID: 16619 RVA: 0x0018968C File Offset: 0x00187A8C
			private IEnumerator LoadAutoLeaveItToYouAttribute()
			{
				this.sbAbName.Clear();
				this.sbAssetName.Clear();
				this.sbAssetName.Append("LeaveItToYou_attribute");
				this.excelData = null;
				for (int i = 0; i < this.HAutoPathList.Count; i++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.HAutoPathList[i]);
					if (GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty))
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(this.sbAbName.ToString());
						if (!(this.excelData == null))
						{
							int j = 1;
							while (j < this.excelData.MaxCell)
							{
								this.row = this.excelData.list[j++].list;
								int num = 1;
								int key;
								if (int.TryParse(this.row.GetElement(num++), out key))
								{
									float value = 1f;
									if (!float.TryParse(this.row.GetElement(num++), out value))
									{
										value = 1f;
									}
									if (!this.autoLeaveAttributeRate.ContainsKey(key))
									{
										this.autoLeaveAttributeRate.Add(key, value);
									}
									else
									{
										this.autoLeaveAttributeRate[key] = value;
									}
								}
							}
						}
					}
				}
				yield return null;
				yield break;
			}

			// Token: 0x060040EC RID: 16620 RVA: 0x001896A8 File Offset: 0x00187AA8
			private IEnumerator LoadHmesh()
			{
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.HmeshListFolder, false);
				this.pathList.Sort();
				this.sbAbName.Clear();
				this.excelData = null;
				for (int nLoopCnt = 0; nLoopCnt < this.pathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[nLoopCnt]);
					this.sbAssetName.Clear();
					this.sbAssetName.Append("hmesh_");
					this.sbAssetName.Append(System.IO.Path.GetFileNameWithoutExtension(this.pathList[nLoopCnt]));
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							int excelRowIdx = 1;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								int c = 0;
								int mapID = 0;
								List<string> list2 = this.row;
								c = (index = c) + 1;
								if (!int.TryParse(list2[index], out mapID))
								{
									yield return null;
								}
								else
								{
									this.hMeshInfo.mapID = mapID;
									List<string> list3 = this.row;
									c = (index = c) + 1;
									this.hMeshInfo.abName = list3[index];
									List<string> list4 = this.row;
									c = (index = c) + 1;
									this.hMeshInfo.assetName = list4[index];
									List<string> list5 = this.row;
									c = (index = c) + 1;
									this.hMeshInfo.manifest = list5[index];
									if (!this.HmeshDictionary.ContainsKey(nLoopCnt))
									{
										this.HmeshDictionary.Add(nLoopCnt, new Dictionary<int, Resources.HSceneTables.HmeshInfo>());
									}
									if (this.HmeshDictionary[nLoopCnt].ContainsKey(mapID))
									{
										this.HmeshDictionary[nLoopCnt].Add(mapID, this.hMeshInfo);
									}
									else
									{
										this.HmeshDictionary[nLoopCnt][mapID] = this.hMeshInfo;
									}
								}
							}
							yield return null;
						}
					}
				}
				yield break;
			}

			// Token: 0x060040ED RID: 16621 RVA: 0x001896C4 File Offset: 0x00187AC4
			private IEnumerator LoadStartWaitAnim()
			{
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetStartWaitAnimListFolder, false);
				this.pathList.Sort();
				this.sbAbName.Clear();
				this.excelData = null;
				for (int nLoopCnt = 0; nLoopCnt < this.pathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[nLoopCnt]);
					this.sbAssetName.Clear();
					this.sbAssetName.Append(System.IO.Path.GetFileNameWithoutExtension(this.pathList[nLoopCnt]));
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							int excelRowIdx = 2;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								List<ExcelData.Param> list = this.excelData.list;
								int index;
								excelRowIdx = (index = excelRowIdx) + 1;
								this.row = list[index].list;
								StartWaitAnim info = new StartWaitAnim();
								int c = 0;
								int ID = -1;
								List<string> list2 = this.row;
								c = (index = c) + 1;
								if (!int.TryParse(list2[index], out ID))
								{
									yield return null;
								}
								else
								{
									info.ID = ID;
									if (this.row.Count != c)
									{
										StartWaitAnim startWaitAnim = info;
										List<string> list3 = this.row;
										c = (index = c) + 1;
										startWaitAnim.CameraFile = list3[index];
										int visibleMode = 0;
										if (this.row.Count == c)
										{
											goto IL_33F;
										}
										List<string> list4 = this.row;
										c = (index = c) + 1;
										if (!int.TryParse(list4[index], out visibleMode))
										{
											goto IL_33F;
										}
										IL_341:
										info.VisibleMode = visibleMode;
										StartWaitAnim.Info[] player = info.Player;
										int num = 0;
										List<string> list5 = this.row;
										c = (index = c) + 1;
										player[num].abName = list5[index];
										StartWaitAnim.Info[] player2 = info.Player;
										int num2 = 0;
										List<string> list6 = this.row;
										c = (index = c) + 1;
										player2[num2].assetName = list6[index];
										StartWaitAnim.Info[] player3 = info.Player;
										int num3 = 0;
										List<string> list7 = this.row;
										c = (index = c) + 1;
										player3[num3].State = list7[index];
										StartWaitAnim.Info[] player4 = info.Player;
										int num4 = 1;
										List<string> list8 = this.row;
										c = (index = c) + 1;
										player4[num4].abName = list8[index];
										StartWaitAnim.Info[] player5 = info.Player;
										int num5 = 1;
										List<string> list9 = this.row;
										c = (index = c) + 1;
										player5[num5].assetName = list9[index];
										StartWaitAnim.Info[] player6 = info.Player;
										int num6 = 1;
										List<string> list10 = this.row;
										c = (index = c) + 1;
										player6[num6].State = list10[index];
										StartWaitAnim.Info[] agent = info.Agent;
										int num7 = 0;
										List<string> list11 = this.row;
										c = (index = c) + 1;
										agent[num7].abName = list11[index];
										StartWaitAnim.Info[] agent2 = info.Agent;
										int num8 = 0;
										List<string> list12 = this.row;
										c = (index = c) + 1;
										agent2[num8].assetName = list12[index];
										StartWaitAnim.Info[] agent3 = info.Agent;
										int num9 = 0;
										List<string> list13 = this.row;
										c = (index = c) + 1;
										agent3[num9].State = list13[index];
										if (this.row.Count > c)
										{
											StartWaitAnim.Info[] agent4 = info.Agent;
											int num10 = 1;
											List<string> list14 = this.row;
											c = (index = c) + 1;
											agent4[num10].abName = list14[index];
											StartWaitAnim.Info[] agent5 = info.Agent;
											int num11 = 1;
											List<string> list15 = this.row;
											c = (index = c) + 1;
											agent5[num11].assetName = list15[index];
											StartWaitAnim.Info[] agent6 = info.Agent;
											int num12 = 1;
											List<string> list16 = this.row;
											c = (index = c) + 1;
											agent6[num12].State = list16[index];
										}
										goto IL_761;
										IL_33F:
										visibleMode = 0;
										goto IL_341;
									}
									info.Player[0].abName = string.Empty;
									info.Player[0].assetName = string.Empty;
									info.Player[0].State = string.Empty;
									info.Player[1].abName = string.Empty;
									info.Player[1].assetName = string.Empty;
									info.Player[1].State = string.Empty;
									info.Agent[0].abName = string.Empty;
									info.Agent[0].assetName = string.Empty;
									info.Agent[0].State = string.Empty;
									info.Agent[1].abName = string.Empty;
									info.Agent[1].assetName = string.Empty;
									info.Agent[1].State = string.Empty;
									info.CameraFile = string.Empty;
									info.VisibleMode = 0;
									IL_761:
									this.startWaitAnims.Add(info);
								}
							}
							yield return null;
						}
					}
				}
				yield break;
			}

			// Token: 0x060040EE RID: 16622 RVA: 0x001896E0 File Offset: 0x00187AE0
			public IEnumerator LoadHYure()
			{
				int[] idxBust = new int[]
				{
					2,
					3,
					4,
					5,
					6,
					7,
					8
				};
				this.pathList.Clear();
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetYureListFolder, false);
				this.pathList.Sort();
				this.excelData = null;
				for (int i = 0; i < 6; i++)
				{
					int key = i;
					Dictionary<int, List<YureCtrl.Info>> dictionary = new Dictionary<int, List<YureCtrl.Info>>();
					for (int j = 0; j < this.pathList.Count; j++)
					{
						this.sbAbName.Clear();
						this.sbAssetName.Clear();
						this.sbAbName.Append(this.pathList[j]);
						switch (i)
						{
						case 0:
							this.sbAssetName.AppendFormat("aia_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
							break;
						case 1:
							this.sbAssetName.AppendFormat("aih_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
							break;
						case 2:
							this.sbAssetName.AppendFormat("ais_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
							break;
						case 3:
							this.sbAssetName.AppendFormat("ait_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
							break;
						case 4:
							this.sbAssetName.AppendFormat("ail_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
							break;
						case 5:
							this.sbAssetName.AppendFormat("ai3p_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
							break;
						}
						if (GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty))
						{
							this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
							this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
							if (!(this.excelData == null))
							{
								int k = 1;
								while (k < this.excelData.MaxCell)
								{
									this.row = this.excelData.list[k++].list;
									int num = 0;
									int key2 = -1;
									if (int.TryParse(this.row.GetElement(num++), out key2))
									{
										YureCtrl.Info info = new YureCtrl.Info();
										int nFemale = 0;
										if (i > 3 && !int.TryParse(this.row.GetElement(num++), out nFemale))
										{
											nFemale = 0;
										}
										info.nFemale = nFemale;
										info.nameAnimation = this.row.GetElement(num++);
										info.aIsActive[0] = (this.row.GetElement(num++) == "1");
										info.aBreastShape[0].MemberInit();
										for (int l = 0; l < idxBust.Length; l++)
										{
											info.aBreastShape[0].breast[l] = (this.row.GetElement(num++) == "1");
										}
										info.aBreastShape[0].nip = (this.row.GetElement(num++) == "1");
										info.aIsActive[1] = (this.row.GetElement(num++) == "1");
										info.aBreastShape[1].MemberInit();
										for (int m = 0; m < idxBust.Length; m++)
										{
											info.aBreastShape[1].breast[m] = (this.row.GetElement(num++) == "1");
										}
										info.aBreastShape[1].nip = (this.row.GetElement(num++) == "1");
										info.aIsActive[2] = (this.row.GetElement(num++) == "1");
										info.aIsActive[3] = (this.row.GetElement(num++) == "1");
										if (!dictionary.ContainsKey(key2))
										{
											dictionary.Add(key2, new List<YureCtrl.Info>());
										}
										dictionary[key2].Add(info);
									}
								}
							}
						}
					}
					if (!this.DicDicYure.ContainsKey(key))
					{
						this.DicDicYure.Add(key, dictionary);
					}
					this.DicDicYure[key] = dictionary;
				}
				yield return null;
				yield break;
			}

			// Token: 0x060040EF RID: 16623 RVA: 0x001896FC File Offset: 0x00187AFC
			public IEnumerator LoadHYureMale()
			{
				int[] idxBust = new int[]
				{
					2,
					3,
					4,
					5,
					6,
					7,
					8
				};
				this.pathList.Clear();
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetYureListFolder, false);
				this.pathList.Sort();
				this.excelData = null;
				for (int i = 0; i < 6; i++)
				{
					int key = i;
					Dictionary<int, List<YureCtrlMale.Info>> dictionary = new Dictionary<int, List<YureCtrlMale.Info>>();
					for (int j = 0; j < this.pathList.Count; j++)
					{
						if (i != 4)
						{
							this.sbAbName.Clear();
							this.sbAssetName.Clear();
							this.sbAbName.Append(this.pathList[j]);
							switch (i)
							{
							case 0:
								this.sbAssetName.AppendFormat("aia_m_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
								break;
							case 1:
								this.sbAssetName.AppendFormat("aih_m_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
								break;
							case 2:
								this.sbAssetName.AppendFormat("ais_m_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
								break;
							case 3:
								this.sbAssetName.AppendFormat("ait_m_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
								break;
							case 5:
								this.sbAssetName.AppendFormat("ai3p_m_{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.pathList[j]));
								break;
							}
							if (GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty))
							{
								this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
								this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
								if (!(this.excelData == null))
								{
									int k = 1;
									while (k < this.excelData.MaxCell)
									{
										this.row = this.excelData.list[k++].list;
										int num = 0;
										int key2 = -1;
										if (int.TryParse(this.row.GetElement(num++), out key2))
										{
											YureCtrlMale.Info info = new YureCtrlMale.Info();
											info.nMale = 0;
											info.nameAnimation = this.row.GetElement(num++);
											info.aIsActive[0] = (this.row.GetElement(num++) == "1");
											info.aBreastShape[0].MemberInit();
											for (int l = 0; l < idxBust.Length; l++)
											{
												info.aBreastShape[0].breast[l] = (this.row.GetElement(num++) == "1");
											}
											info.aBreastShape[0].nip = (this.row.GetElement(num++) == "1");
											info.aIsActive[1] = (this.row.GetElement(num++) == "1");
											info.aBreastShape[1].MemberInit();
											for (int m = 0; m < idxBust.Length; m++)
											{
												info.aBreastShape[1].breast[m] = (this.row.GetElement(num++) == "1");
											}
											info.aBreastShape[1].nip = (this.row.GetElement(num++) == "1");
											info.aIsActive[2] = (this.row.GetElement(num++) == "1");
											info.aIsActive[3] = (this.row.GetElement(num++) == "1");
											if (!dictionary.ContainsKey(key2))
											{
												dictionary.Add(key2, new List<YureCtrlMale.Info>());
											}
											dictionary[key2].Add(info);
										}
									}
								}
							}
						}
					}
					if (!this.DicDicYure.ContainsKey(key))
					{
						this.DicDicYureMale.Add(key, dictionary);
					}
					this.DicDicYureMale[key] = dictionary;
				}
				yield return null;
				yield break;
			}

			// Token: 0x060040F0 RID: 16624 RVA: 0x00189718 File Offset: 0x00187B18
			private IEnumerator LoadHScene()
			{
				this.LoadHSceneSet();
				yield return null;
				this.LoadHSceneUISet();
				yield return null;
				yield break;
			}

			// Token: 0x060040F1 RID: 16625 RVA: 0x00189734 File Offset: 0x00187B34
			private IEnumerator LoadFeelHit()
			{
				List<string> pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetFeelHitListFolder, false);
				pathList.Sort();
				int[] c = new int[]
				{
					1,
					2
				};
				string abName = string.Empty;
				this.excelData = null;
				List<string>[] rowFeel = new List<string>[]
				{
					new List<string>(),
					new List<string>()
				};
				string[][] Initloop = new string[3][];
				for (int nLoopCnt = 0; nLoopCnt < pathList.Count; nLoopCnt++)
				{
					abName = pathList[nLoopCnt];
					this.sbAssetName.Clear();
					this.sbAssetName.Append("FeelHit_");
					this.sbAssetName.Append(System.IO.Path.GetFileNameWithoutExtension(pathList[nLoopCnt]));
					bool exist = false;
					exist = GlobalMethod.AssetFileExist(abName, this.sbAssetName.ToString(), string.Empty);
					if (!exist)
					{
						yield return null;
					}
					else
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(abName, this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(abName, string.Empty));
						if (this.excelData == null)
						{
							yield return null;
						}
						else
						{
							int excelRowIdx = 2;
							while (excelRowIdx < this.excelData.MaxCell)
							{
								for (int i = 0; i < rowFeel.Length; i++)
								{
									List<string>[] array = rowFeel;
									int num = i;
									List<ExcelData.Param> list = this.excelData.list;
									int index;
									excelRowIdx = (index = excelRowIdx) + 1;
									array[num] = list[index].list;
								}
								c[0] = 1;
								c[1] = 2;
								int personality = -1;
								if (int.TryParse(rowFeel[0].GetElement(c[0]++), out personality))
								{
									if (!this.DicLstHitInfo.ContainsKey(personality))
									{
										this.DicLstHitInfo.Add(personality, new List<FeelHit.FeelInfo>());
									}
									else
									{
										this.DicLstHitInfo[personality] = new List<FeelHit.FeelInfo>();
									}
									FeelHit.FeelInfo info = new FeelHit.FeelInfo();
									int nX = rowFeel[0].Count;
									for (int x = 2; x < nX; x += 3)
									{
										for (int j = 0; j < 3; j++)
										{
											Initloop[j] = rowFeel[0].GetElement(c[0]++).Split(new char[]
											{
												'/'
											});
											FeelHit.FeelHitInfo hitInfo;
											hitInfo.area = new Vector2(float.Parse(Initloop[j][0]), float.Parse(Initloop[j][1]));
											hitInfo.rate = float.Parse(rowFeel[1].GetElement(c[1]++));
											info.lstHitArea.Add(hitInfo);
										}
										this.DicLstHitInfo[personality].Add(info);
										yield return null;
									}
								}
							}
						}
					}
				}
				yield break;
			}

			// Token: 0x060040F2 RID: 16626 RVA: 0x00189750 File Offset: 0x00187B50
			private IEnumerator LoadDankonList()
			{
				List<string> pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetDankonListFolder, false);
				pathList.Sort();
				this.sbAbName.Clear();
				this.sbAssetName.Clear();
				this.excelData = null;
				H_Lookat_dan.ShapeInfo info = default(H_Lookat_dan.ShapeInfo);
				for (int nLoopCnt = 0; nLoopCnt < pathList.Count; nLoopCnt++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(pathList[nLoopCnt]);
					for (int nCategory = 0; nCategory < this.lstAnimInfo.Length; nCategory++)
					{
						for (int i = 0; i < this.lstAnimInfo[nCategory].Count; i++)
						{
							if (!this.lstAnimInfo[nCategory][i].fileMale.IsNullOrEmpty())
							{
								this.sbAssetName.Clear();
								this.sbAssetName.Append(this.lstAnimInfo[nCategory][i].fileMale);
								this.sbAssetName.Replace("_m_", "_");
								if (GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty))
								{
									this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
									this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
									if (!(this.excelData == null))
									{
										if (!this.DicLstLookAtDan.ContainsKey(this.sbAssetName.ToString()))
										{
											this.DicLstLookAtDan.Add(this.sbAssetName.ToString(), new List<H_Lookat_dan.MotionLookAtList>());
										}
										int j = 3;
										while (j < this.excelData.MaxCell)
										{
											this.row = this.excelData.list[j++].list;
											H_Lookat_dan.MotionLookAtList look = new H_Lookat_dan.MotionLookAtList();
											int num = 0;
											look.strMotion = this.row.GetElement(num++);
											int numFemale = 0;
											if (int.TryParse(this.row.GetElement(num++), out numFemale))
											{
												look.numFemale = numFemale;
											}
											look.strLookAtNull = this.row.GetElement(num++);
											look.bTopStick = (this.row.GetElement(num++) == "1");
											look.bManual = (this.row.GetElement(num++) == "1");
											int num2 = 0;
											for (int k = num; k < this.row.Count; k += 10)
											{
												int num3 = 0;
												int shape = 0;
												if (!int.TryParse(this.row.GetElement(k + num3++), out shape))
												{
													break;
												}
												info.shape = shape;
												info.minPos = new Vector3(float.Parse(this.row.GetElement(k + num3++)), float.Parse(this.row.GetElement(k + num3++)), float.Parse(this.row.GetElement(k + num3++)));
												info.middlePos = new Vector3(float.Parse(this.row.GetElement(k + num3++)), float.Parse(this.row.GetElement(k + num3++)), float.Parse(this.row.GetElement(k + num3++)));
												info.maxPos = new Vector3(float.Parse(this.row.GetElement(k + num3++)), float.Parse(this.row.GetElement(k + num3++)), float.Parse(this.row.GetElement(k + num3++)));
												info.bUse = true;
												look.lstShape[num2++] = info;
											}
											if (this.DicLstLookAtDan[this.sbAssetName.ToString()] == null)
											{
												this.DicLstLookAtDan[this.sbAssetName.ToString()] = new List<H_Lookat_dan.MotionLookAtList>();
											}
											this.DicLstLookAtDan[this.sbAssetName.ToString()].Add(look);
										}
									}
								}
							}
						}
						yield return null;
					}
				}
				yield break;
			}

			// Token: 0x060040F3 RID: 16627 RVA: 0x0018976C File Offset: 0x00187B6C
			public void LoadHParamTable(int mode)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetParam, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					string text2 = string.Empty;
					if (mode == 0)
					{
						text2 = "base_" + System.IO.Path.GetFileNameWithoutExtension(text);
					}
					else if (mode == 1)
					{
						text2 = "hresult_" + System.IO.Path.GetFileNameWithoutExtension(text);
					}
					if (GlobalMethod.AssetFileExist(text, text2, string.Empty))
					{
						ExcelData excelData = CommonLib.LoadAsset<ExcelData>(text, text2, false, string.Empty);
						if (!(excelData == null))
						{
							Singleton<Resources>.Instance.AddLoadAssetBundle(text, string.Empty);
							int j = 1;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								int k = 1;
								string element = param.list.GetElement(k++);
								int key;
								if (int.TryParse(element, out key))
								{
									ParameterPacket parameterPacket = new ParameterPacket();
									while (k < param.list.Count)
									{
										string element2 = param.list.GetElement(k++);
										string element3 = param.list.GetElement(k++);
										string element4 = param.list.GetElement(k++);
										string element5 = param.list.GetElement(k++);
										if (!element2.IsNullOrEmpty())
										{
											int key2;
											if (Resources.HSceneTables.HTagTable.TryGetValue(element2, out key2))
											{
												int num;
												int s = (!int.TryParse(element3, out num)) ? 0 : num;
												int m = (!int.TryParse(element4, out num)) ? 0 : num;
												int l = (!int.TryParse(element5, out num)) ? 0 : num;
												parameterPacket.Parameters[key2] = new TriThreshold(s, m, l);
											}
										}
									}
									if (mode == 0)
									{
										this.HBaseParamTable[key] = parameterPacket;
									}
									else if (mode == 1)
									{
										this.HactionParamTable[key] = parameterPacket;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060040F4 RID: 16628 RVA: 0x001899C0 File Offset: 0x00187DC0
			public void LoadHSkilParm()
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetParam, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					string text2 = "hskil_" + System.IO.Path.GetFileNameWithoutExtension(text);
					if (GlobalMethod.AssetFileExist(text, text2, string.Empty))
					{
						ExcelData excelData = CommonLib.LoadAsset<ExcelData>(text, text2, false, string.Empty);
						if (!(excelData == null))
						{
							Singleton<Resources>.Instance.AddLoadAssetBundle(text, string.Empty);
							int j = 1;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								int k = 0;
								string element = param.list.GetElement(k++);
								int key;
								if (int.TryParse(element, out key))
								{
									k++;
									Dictionary<int, float> dictionary = new Dictionary<int, float>();
									while (k < param.list.Count)
									{
										string element2 = param.list.GetElement(k++);
										string element3 = param.list.GetElement(k++);
										if (!element2.IsNullOrEmpty())
										{
											int key2;
											if (Resources.HSceneTables.HTagTable.TryGetValue(element2, out key2))
											{
												float value;
												if (!float.TryParse(element3, out value))
												{
													value = 0f;
												}
												if (!dictionary.ContainsKey(key2))
												{
													dictionary.Add(key2, 0f);
												}
												dictionary[key2] = value;
											}
										}
									}
									this.HSkileParamTable[key] = dictionary;
								}
							}
						}
					}
				}
			}

			// Token: 0x060040F5 RID: 16629 RVA: 0x00189B80 File Offset: 0x00187F80
			private void LoadHSceneSet()
			{
				if (this.HSceneSet != null)
				{
					return;
				}
				this.sbAbName.Clear();
				StringBuilder stringBuilder = new StringBuilder();
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(this.hscenePrefabPath, false);
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					stringBuilder.Clear();
					stringBuilder.AppendFormat("add{0:00}", System.IO.Path.GetFileNameWithoutExtension(assetBundleNameListFromPath[i]));
					if (GlobalMethod.AssetFileExist(assetBundleNameListFromPath[i], "HSceneSet", stringBuilder.ToString()))
					{
						this.sbAbName.Clear();
						this.sbAbName.Append(assetBundleNameListFromPath[i]);
					}
				}
				stringBuilder.Clear();
				stringBuilder.AppendFormat("add{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.sbAbName.ToString()));
				this.HSceneSet = CommonLib.LoadAsset<GameObject>(this.sbAbName.ToString(), "HSceneSet", true, stringBuilder.ToString());
				this.hashUseAssetBundle[1].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), stringBuilder.ToString()));
				Transform parent = (!(this.commonSpace == null)) ? this.commonSpace.transform : GameObject.Find("CommonSpace").transform;
				this.HSceneSet.transform.SetParent(parent, false);
				this.HSceneSet.transform.localPosition = Vector3.zero;
				this.HSceneSet.transform.localRotation = Quaternion.identity;
				this.HSceneSet.GetComponentInChildren<HSceneFlagCtrl>().MapHVoiceInit();
			}

			// Token: 0x060040F6 RID: 16630 RVA: 0x00189D1C File Offset: 0x0018811C
			private void LoadHSceneUISet()
			{
				if (this.HSceneUISet != null)
				{
					return;
				}
				this.sbAbName.Clear();
				StringBuilder stringBuilder = new StringBuilder();
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(this.hscenePrefabPath, false);
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					stringBuilder.Clear();
					stringBuilder.AppendFormat("add{0:00}", System.IO.Path.GetFileNameWithoutExtension(assetBundleNameListFromPath[i]));
					if (GlobalMethod.AssetFileExist(assetBundleNameListFromPath[i], "HSceneUISet", stringBuilder.ToString()))
					{
						this.sbAbName.Clear();
						this.sbAbName.Append(assetBundleNameListFromPath[i]);
					}
				}
				stringBuilder.Clear();
				stringBuilder.AppendFormat("add{0:00}", System.IO.Path.GetFileNameWithoutExtension(this.sbAbName.ToString()));
				this.HSceneUISet = CommonLib.LoadAsset<GameObject>(this.sbAbName.ToString(), "HSceneUISet", true, stringBuilder.ToString());
				this.hashUseAssetBundle[1].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), stringBuilder.ToString()));
				Transform parent = (!(this.commonSpace == null)) ? this.commonSpace.transform : GameObject.Find("CommonSpace").transform;
				this.HSceneUISet.transform.SetParent(parent, false);
				this.HSceneUISet.transform.localPosition = Vector3.zero;
				this.HSceneUISet.transform.localRotation = Quaternion.identity;
			}

			// Token: 0x060040F7 RID: 16631 RVA: 0x00189EA8 File Offset: 0x001882A8
			private bool GetIntMember(string[,] _str, int _y, ref int _line, ref int _member)
			{
				int length = _str.GetLength(1);
				if (length <= _line)
				{
					return false;
				}
				string text = _str[_y, _line++];
				if (!text.IsNullOrEmpty())
				{
					_member = int.Parse(text);
				}
				return true;
			}

			// Token: 0x060040F8 RID: 16632 RVA: 0x00189EF0 File Offset: 0x001882F0
			public void SetHmesh(Transform parent)
			{
				this.HMeshObjDic.Clear();
				GameObject mesh;
				foreach (KeyValuePair<int, Dictionary<int, Resources.HSceneTables.HmeshInfo>> keyValuePair in this.HmeshDictionary)
				{
					int mapID = Singleton<Manager.Map>.Instance.MapID;
					Resources.HSceneTables.HmeshInfo hmeshInfo;
					if (keyValuePair.Value.TryGetValue(mapID, out hmeshInfo))
					{
						mesh = CommonLib.LoadAsset<GameObject>(hmeshInfo.abName, hmeshInfo.assetName, true, hmeshInfo.manifest);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(hmeshInfo.abName, hmeshInfo.manifest));
						mesh.transform.SetParent(parent, false);
						mesh.transform.localPosition = Vector3.zero;
						mesh.transform.localRotation = Quaternion.identity;
						this.HMeshObjDic.Add(mapID, mesh);
						Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
						{
							HMeshData component = mesh.GetComponent<HMeshData>();
							if (component == null)
							{
								return;
							}
							component.SetColliderAreaMap();
						});
					}
				}
			}

			// Token: 0x060040F9 RID: 16633 RVA: 0x0018A020 File Offset: 0x00188420
			private void LoadHitObject()
			{
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetHitObjListFolder, false);
				this.pathList.Sort();
				this.sbAbName.Clear();
				this.sbAssetName.Clear();
				this.sbAssetName.Append("base");
				this.excelData = null;
				List<string> list = new List<string>();
				for (int i = 0; i < this.pathList.Count; i++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(this.pathList[i]);
					if (GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty))
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
						if (!(this.excelData == null))
						{
							int j = 1;
							while (j < this.excelData.MaxCell)
							{
								this.row = this.excelData.list[j++].list;
								int k = 0;
								list = new List<string>();
								int key = -1;
								if (int.TryParse(this.row.GetElement(k++), out key))
								{
									while (k < this.row.Count)
									{
										list.Add(this.row.GetElement(k++));
									}
									if (!this.lstHitObject.ContainsKey(key))
									{
										this.lstHitObject.Add(key, list);
									}
									else
									{
										this.lstHitObject[key] = list;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060040FA RID: 16634 RVA: 0x0018A21C File Offset: 0x0018861C
			private void LoadHitObjectAdd()
			{
				this.pathList = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetHitObjListFolder, false);
				this.pathList.Sort();
				List<string> list = new List<string>();
				ValueDictionary<string, int, List<string>> valueDictionary = new ValueDictionary<string, int, List<string>>();
				this.HitObjectNames(this.pathList, ref list);
				for (int i = 0; i < this.pathList.Count; i++)
				{
					int num = -1;
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(this.pathList[i]);
					if (!int.TryParse(fileNameWithoutExtension, out num))
					{
						num = -1;
					}
					if (num < 50 || Game.isAdd50)
					{
						this.LoadHitObjectAdd(this.pathList[i], list, ref valueDictionary);
					}
				}
				foreach (ValueDictionary<int, List<string>> valueDictionary2 in valueDictionary.Values)
				{
					foreach (KeyValuePair<int, List<string>> keyValuePair in valueDictionary2)
					{
						if (!this.lstHitObject.ContainsKey(keyValuePair.Key))
						{
							this.lstHitObject.Add(keyValuePair.Key, keyValuePair.Value);
						}
						else
						{
							list = this.lstHitObject[keyValuePair.Key];
							this.CheckHitObjectListContain(ref list, keyValuePair.Value);
						}
					}
				}
			}

			// Token: 0x060040FB RID: 16635 RVA: 0x0018A3B4 File Offset: 0x001887B4
			private void LoadHitObjectAdd(string path, List<string> assetNames, ref ValueDictionary<string, int, List<string>> loaded)
			{
				this.excelData = null;
				List<string> list = new List<string>();
				for (int i = 0; i < assetNames.Count; i++)
				{
					if (GlobalMethod.AssetFileExist(path, assetNames[i], string.Empty))
					{
						this.excelData = CommonLib.LoadAsset<ExcelData>(path, assetNames[i], false, string.Empty);
						this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(path, string.Empty));
						if (!(this.excelData == null))
						{
							if (!loaded.ContainsKey(assetNames[i]))
							{
								loaded[assetNames[i]] = loaded.New<string, int, List<string>>();
							}
							int j = 1;
							while (j < this.excelData.MaxCell)
							{
								this.row = this.excelData.list[j++].list;
								int k = 0;
								list = new List<string>();
								int key = -1;
								if (int.TryParse(this.row.GetElement(k++), out key))
								{
									while (k < this.row.Count)
									{
										list.Add(this.row.GetElement(k++));
									}
									if (!loaded[assetNames[i]].ContainsKey(key))
									{
										loaded[assetNames[i]].Add(key, list);
									}
									else
									{
										loaded[assetNames[i]][key] = list;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060040FC RID: 16636 RVA: 0x0018A558 File Offset: 0x00188958
			private void HitObjectNames(List<string> pathList, ref List<string> names)
			{
				foreach (string text in pathList)
				{
					int num = -1;
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
					if (!int.TryParse(fileNameWithoutExtension, out num))
					{
						num = -1;
					}
					if (num < 50 || Game.isAdd50)
					{
						string[] allAssetName = AssetBundleCheck.GetAllAssetName(text, false, null, false);
						foreach (string text2 in allAssetName)
						{
							if (!names.Contains(text2))
							{
								if (GlobalMethod.StartsWith2(text2, "base_"))
								{
									names.Add(text2);
								}
							}
						}
					}
				}
			}

			// Token: 0x060040FD RID: 16637 RVA: 0x0018A638 File Offset: 0x00188A38
			private void CheckHitObjectListContain(ref List<string> check, List<string> values)
			{
				int i = 0;
				while (i < values.Count)
				{
					bool flag = false;
					for (int j = 0; j < check.Count; j += 3)
					{
						if (!(check[j] != values[i]) && !(check[j + 1] != values[i + 1]) && !(check[j + 2] != values[i + 2]))
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						i += 3;
					}
					else
					{
						check.Add(values[i++]);
						check.Add(values[i++]);
						check.Add(values[i++]);
					}
				}
			}

			// Token: 0x060040FE RID: 16638 RVA: 0x0018A718 File Offset: 0x00188B18
			private void CollisionLoadExcel()
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetCollisionListFolder, false);
				assetBundleNameListFromPath.Sort();
				CollisionCtrl.CollisionInfo collisionInfo = default(CollisionCtrl.CollisionInfo);
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(assetBundleNameListFromPath[i]);
					int num = -1;
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(assetBundleNameListFromPath[i]);
					if (!int.TryParse(fileNameWithoutExtension, out num))
					{
						num = -1;
					}
					if (num < 50 || Game.isAdd50)
					{
						for (int j = 0; j < this.lstAnimInfo.Length; j++)
						{
							for (int k = 0; k < this.lstAnimInfo[j].Count; k++)
							{
								this.CollisionLoadExcel(this.lstAnimInfo[j][k], 0, ref collisionInfo);
								this.CollisionLoadExcel(this.lstAnimInfo[j][k], 1, ref collisionInfo);
								this.CollisionLoadExcel(this.lstAnimInfo[j][k], 2, ref collisionInfo);
							}
						}
					}
				}
			}

			// Token: 0x060040FF RID: 16639 RVA: 0x0018A83C File Offset: 0x00188C3C
			private void CollisionLoadExcel(HScene.AnimationListInfo ainfo, int kind, ref CollisionCtrl.CollisionInfo info)
			{
				if (kind != 0)
				{
					if (kind != 1)
					{
						if (kind == 2)
						{
							if (ainfo.fileFemale2.IsNullOrEmpty())
							{
								return;
							}
							this.sbAssetName.Clear();
							this.sbAssetName.Append(ainfo.fileFemale2);
						}
					}
					else
					{
						if (ainfo.fileFemale.IsNullOrEmpty())
						{
							return;
						}
						this.sbAssetName.Clear();
						this.sbAssetName.Append(ainfo.fileFemale);
					}
				}
				else
				{
					if (ainfo.fileMale.IsNullOrEmpty())
					{
						return;
					}
					this.sbAssetName.Clear();
					this.sbAssetName.Append(ainfo.fileMale);
				}
				if (!GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty))
				{
					return;
				}
				this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
				this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
				if (this.excelData == null)
				{
					return;
				}
				if (!this.DicLstCollisionInfo.ContainsKey(this.sbAssetName.ToString()))
				{
					this.DicLstCollisionInfo.Add(this.sbAssetName.ToString(), new List<CollisionCtrl.CollisionInfo>());
				}
				int i = 1;
				while (i < this.excelData.MaxCell)
				{
					this.row = this.excelData.list[i++].list;
					int j = 0;
					info.nameAnimation = this.row.GetElement(j++);
					info.lstIsActive = new List<bool>();
					while (j < this.row.Count)
					{
						info.lstIsActive.Add(this.row.GetElement(j++) == "1");
					}
					if (this.DicLstCollisionInfo[this.sbAssetName.ToString()] == null)
					{
						this.DicLstCollisionInfo[this.sbAssetName.ToString()] = new List<CollisionCtrl.CollisionInfo>();
					}
					this.DicLstCollisionInfo[this.sbAssetName.ToString()].Add(info);
				}
			}

			// Token: 0x06004100 RID: 16640 RVA: 0x0018AAA8 File Offset: 0x00188EA8
			private void HitObjLoadExcel()
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetHitObjListFolder, false);
				assetBundleNameListFromPath.Sort();
				HitObjectCtrl.CollisionInfo collisionInfo = default(HitObjectCtrl.CollisionInfo);
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(assetBundleNameListFromPath[i]);
					int num = -1;
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(assetBundleNameListFromPath[i]);
					if (!int.TryParse(fileNameWithoutExtension, out num))
					{
						num = -1;
					}
					if (num < 50 || Game.isAdd50)
					{
						for (int j = 0; j < this.lstAnimInfo.Length; j++)
						{
							for (int k = 0; k < this.lstAnimInfo[j].Count; k++)
							{
								this.HitObjLoadExcel(this.lstAnimInfo[j][k], 0, ref collisionInfo);
								this.HitObjLoadExcel(this.lstAnimInfo[j][k], 1, ref collisionInfo);
								this.HitObjLoadExcel(this.lstAnimInfo[j][k], 2, ref collisionInfo);
							}
						}
					}
				}
			}

			// Token: 0x06004101 RID: 16641 RVA: 0x0018ABCC File Offset: 0x00188FCC
			private void HitObjLoadExcel(HScene.AnimationListInfo ai, int kind, ref HitObjectCtrl.CollisionInfo info)
			{
				if (kind != 0)
				{
					if (kind != 1)
					{
						if (kind == 2)
						{
							if (ai.fileFemale2.IsNullOrEmpty())
							{
								return;
							}
							this.sbAssetName.Clear();
							this.sbAssetName.Append(ai.fileFemale2);
						}
					}
					else
					{
						if (ai.fileFemale.IsNullOrEmpty())
						{
							return;
						}
						this.sbAssetName.Clear();
						this.sbAssetName.Append(ai.fileFemale);
					}
				}
				else
				{
					if (ai.fileMale.IsNullOrEmpty())
					{
						return;
					}
					this.sbAssetName.Clear();
					this.sbAssetName.Append(ai.fileMale);
				}
				this.excelData = null;
				if (!GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty))
				{
					return;
				}
				this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
				this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
				if (this.excelData == null)
				{
					return;
				}
				int i = 0;
				if (!this.HitObjAtariName.ContainsKey(this.sbAssetName.ToString()))
				{
					this.HitObjAtariName.Add(this.sbAssetName.ToString(), new List<string>());
				}
				this.row = this.excelData.list[i++].list;
				for (int j = 1; j < this.row.Count; j++)
				{
					int index = j;
					this.HitObjAtariName[this.sbAssetName.ToString()].Add(this.row.GetElement(index));
				}
				if (!this.DicLstHitObjInfo.ContainsKey(this.sbAssetName.ToString()))
				{
					this.DicLstHitObjInfo.Add(this.sbAssetName.ToString(), new List<HitObjectCtrl.CollisionInfo>());
				}
				while (i < this.excelData.MaxCell)
				{
					int num = 0;
					this.row = this.excelData.list[i++].list;
					info.nameAnimation = this.row.GetElement(num++);
					info.lstIsActive = new List<bool>();
					for (int k = 1; k < this.row.Count; k++)
					{
						info.lstIsActive.Add(this.row.GetElement(num++) == "1");
					}
					this.DicLstHitObjInfo[this.sbAssetName.ToString()].Add(info);
				}
			}

			// Token: 0x06004102 RID: 16642 RVA: 0x0018AEB4 File Offset: 0x001892B4
			private void HLayerLoadExcel()
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(Singleton<HSceneManager>.Instance.strAssetLayerCtrlListFolder, false);
				assetBundleNameListFromPath.Sort();
				this.excelData = null;
				HLayerCtrl.HLayerInfo hlayerInfo = default(HLayerCtrl.HLayerInfo);
				StringBuilder stateName = new StringBuilder();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					this.sbAbName.Clear();
					this.sbAbName.Append(assetBundleNameListFromPath[i]);
					int num = -1;
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(assetBundleNameListFromPath[i]);
					if (!int.TryParse(fileNameWithoutExtension, out num))
					{
						num = -1;
					}
					if (num < 50 || Game.isAdd50)
					{
						for (int j = 0; j < this.lstAnimInfo.Length; j++)
						{
							for (int k = 0; k < this.lstAnimInfo[j].Count; k++)
							{
								this.HLayerLoadExcel(this.lstAnimInfo[j][k], 0, stateName, ref hlayerInfo);
								this.HLayerLoadExcel(this.lstAnimInfo[j][k], 1, stateName, ref hlayerInfo);
								this.HLayerLoadExcel(this.lstAnimInfo[j][k], 2, stateName, ref hlayerInfo);
							}
						}
					}
				}
			}

			// Token: 0x06004103 RID: 16643 RVA: 0x0018AFEC File Offset: 0x001893EC
			private void HLayerLoadExcel(HScene.AnimationListInfo ai, int kind, StringBuilder stateName, ref HLayerCtrl.HLayerInfo info)
			{
				if (kind != 0)
				{
					if (kind != 1)
					{
						if (kind == 2)
						{
							if (ai.fileFemale2.IsNullOrEmpty())
							{
								return;
							}
							this.sbAssetName.Clear();
							this.sbAssetName.Append(ai.fileFemale2);
						}
					}
					else
					{
						if (ai.fileFemale.IsNullOrEmpty())
						{
							return;
						}
						this.sbAssetName.Clear();
						this.sbAssetName.Append(ai.fileFemale);
					}
				}
				else
				{
					if (ai.fileMale.IsNullOrEmpty())
					{
						return;
					}
					this.sbAssetName.Clear();
					this.sbAssetName.Append(ai.fileMale);
				}
				if (!GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.sbAssetName.ToString(), string.Empty))
				{
					return;
				}
				this.excelData = CommonLib.LoadAsset<ExcelData>(this.sbAbName.ToString(), this.sbAssetName.ToString(), false, string.Empty);
				this.hashUseAssetBundle[0].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
				if (this.excelData == null)
				{
					return;
				}
				if (!this.LayerInfos.ContainsKey(this.sbAssetName.ToString()))
				{
					this.LayerInfos.Add(this.sbAssetName.ToString(), new Dictionary<string, HLayerCtrl.HLayerInfo>());
				}
				int i = 1;
				while (i < this.excelData.MaxCell)
				{
					this.row = this.excelData.list[i++].list;
					int num = 0;
					stateName.Clear();
					stateName.Append(this.row[num++]);
					if (!(stateName.ToString() == string.Empty))
					{
						if (!this.LayerInfos[this.sbAssetName.ToString()].ContainsKey(stateName.ToString()))
						{
							this.LayerInfos[this.sbAssetName.ToString()].Add(stateName.ToString(), default(HLayerCtrl.HLayerInfo));
						}
						int layerID = 0;
						float weight = 0f;
						if (int.TryParse(this.row[num++], out layerID))
						{
							float.TryParse(this.row[num++], out weight);
						}
						else
						{
							layerID = 0;
						}
						info.LayerID = layerID;
						info.weight = weight;
						this.LayerInfos[this.sbAssetName.ToString()][stateName.ToString()] = info;
					}
				}
			}

			// Token: 0x06004104 RID: 16644 RVA: 0x0018B2AC File Offset: 0x001896AC
			private int AutoHpointID(string tag)
			{
				int result = -1;
				foreach (KeyValuePair<int, HPoint.HpointData> keyValuePair in this.loadHPointDatas)
				{
					if (HSceneManager.HmeshTag[tag] != -1)
					{
						if (keyValuePair.Value.place.Count == 1)
						{
							UnityEx.ValueTuple<int, int> valueTuple;
							if (keyValuePair.Value.place.TryGetValue(0, out valueTuple))
							{
								if (valueTuple.Item1 == HSceneManager.HmeshTag[tag])
								{
									result = keyValuePair.Key;
									break;
								}
							}
						}
					}
					else if (keyValuePair.Value.place.Count == 2)
					{
						bool flag = true;
						foreach (KeyValuePair<int, UnityEx.ValueTuple<int, int>> keyValuePair2 in keyValuePair.Value.place)
						{
							if (keyValuePair2.Value.Item1 != 0 && keyValuePair2.Value.Item1 != 1)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							result = keyValuePair.Key;
							break;
						}
					}
				}
				return result;
			}

			// Token: 0x06004105 RID: 16645 RVA: 0x0018B448 File Offset: 0x00189848
			private void HparticleInit()
			{
				HScene componentInChildren = this.HSceneSet.GetComponentInChildren<HScene>(true);
				this.hParticle = new HParticleCtrl();
				this.hParticle.particlePlace = componentInChildren.hParticlePlace;
				this.hParticle.Init();
			}

			// Token: 0x06004106 RID: 16646 RVA: 0x0018B48C File Offset: 0x0018988C
			private void HitObjListInit()
			{
				HScene componentInChildren = this.HSceneSet.GetComponentInChildren<HScene>(true);
				for (int i = 0; i < 2; i++)
				{
					componentInChildren.ctrlHitObjectFemales[i] = new HitObjectCtrl();
					componentInChildren.ctrlHitObjectFemales[i].Place = componentInChildren.hitobjPlace;
					if (!this.DicHitObject.ContainsKey(1))
					{
						this.DicHitObject.Add(1, new Dictionary<int, Dictionary<string, GameObject>>());
					}
					if (!this.DicHitObject[1].ContainsKey(i))
					{
						this.DicHitObject[1].Add(i, new Dictionary<string, GameObject>());
					}
					int count = this.lstHitObject[1].Count;
					for (int j = 0; j < count; j += 3)
					{
						this.sbAbName.Clear();
						this.sbAbName.Append(this.lstHitObject[1][j + 1]);
						if (GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.lstHitObject[1][j + 2], string.Empty))
						{
							GameObject gameObject = CommonLib.LoadAsset<GameObject>(this.sbAbName.ToString(), this.lstHitObject[1][j + 2], true, string.Empty);
							this.hashUseAssetBundle[1].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
							if (!(gameObject == null))
							{
								gameObject.SetActive(false);
								gameObject.transform.SetParent(componentInChildren.hitobjPlace, false);
								if (!this.DicHitObject[1][i].ContainsKey(this.lstHitObject[1][j + 2]))
								{
									this.DicHitObject[1][i].Add(this.lstHitObject[1][j + 2], gameObject);
								}
								else
								{
									this.DicHitObject[1][i][this.lstHitObject[1][j + 2]] = gameObject;
								}
							}
						}
					}
				}
				for (int k = 0; k < 2; k++)
				{
					componentInChildren.ctrlHitObjectMales[k] = new HitObjectCtrl();
					componentInChildren.ctrlHitObjectMales[k].Place = componentInChildren.hitobjPlace;
					if (!this.DicHitObject.ContainsKey(0))
					{
						this.DicHitObject.Add(0, new Dictionary<int, Dictionary<string, GameObject>>());
					}
					if (!this.DicHitObject[0].ContainsKey(k))
					{
						this.DicHitObject[0].Add(k, new Dictionary<string, GameObject>());
					}
					int count2 = this.lstHitObject[0].Count;
					for (int l = 0; l < count2; l += 3)
					{
						this.sbAbName.Clear();
						this.sbAbName.Append(this.lstHitObject[0][l + 1]);
						if (GlobalMethod.AssetFileExist(this.sbAbName.ToString(), this.lstHitObject[0][l + 2], string.Empty))
						{
							GameObject gameObject2 = CommonLib.LoadAsset<GameObject>(this.sbAbName.ToString(), this.lstHitObject[0][l + 2], true, string.Empty);
							this.hashUseAssetBundle[1].Add(new Resources.HSceneTables.HAssetBundle(this.sbAbName.ToString(), string.Empty));
							if (!(gameObject2 == null))
							{
								gameObject2.SetActive(false);
								gameObject2.transform.SetParent(componentInChildren.hitobjPlace, false);
								if (!this.DicHitObject[0][k].ContainsKey(this.lstHitObject[0][l + 2]))
								{
									this.DicHitObject[0][k].Add(this.lstHitObject[0][l + 2], gameObject2);
								}
								else
								{
									this.DicHitObject[0][k][this.lstHitObject[0][l + 2]] = gameObject2;
								}
							}
						}
					}
				}
			}

			// Token: 0x06004107 RID: 16647 RVA: 0x0018B8EC File Offset: 0x00189CEC
			public void ChangeMapHpoint(int mapID)
			{
				if (this.HPointPrefabs != null && this.HPointPrefabs.Count > 0)
				{
					foreach (KeyValuePair<int, Dictionary<string, GameObject>> keyValuePair in this.HPointPrefabs)
					{
						if (keyValuePair.Key != mapID)
						{
							foreach (GameObject obj in keyValuePair.Value.Values)
							{
								UnityEngine.Object.Destroy(obj);
							}
							keyValuePair.Value.Clear();
						}
					}
				}
				if (this.AutoHPointPrefabs != null)
				{
					foreach (HPoint hpoint in this.AutoHPointPrefabs.GetComponentsInChildren<HPoint>(true))
					{
						if (!(hpoint == null) && !(hpoint.gameObject == null))
						{
							UnityEngine.Object.Destroy(hpoint.gameObject);
						}
					}
				}
				if (this.hPointLists != null && this.hPointLists.Count > 0)
				{
					List<int> list = new List<int>();
					foreach (KeyValuePair<int, HPointList> keyValuePair2 in this.hPointLists)
					{
						if (keyValuePair2.Key != mapID)
						{
							foreach (KeyValuePair<int, List<HPoint>> keyValuePair3 in keyValuePair2.Value.lst)
							{
								foreach (HPoint hpoint2 in keyValuePair3.Value)
								{
									if (!(hpoint2 == null) && !(hpoint2.gameObject == null))
									{
										UnityEngine.Object.Destroy(hpoint2.gameObject);
									}
								}
								keyValuePair3.Value.Clear();
							}
							list.Add(keyValuePair2.Key);
						}
					}
					for (int j = 0; j < list.Count; j++)
					{
						this.hPointLists.Remove(list[j]);
					}
				}
				this.LoadHpointPrefabs(mapID);
				this.LoadAutoHPoint(mapID);
				this.HPointInitData(mapID);
			}

			// Token: 0x06004108 RID: 16648 RVA: 0x0018BBD0 File Offset: 0x00189FD0
			private int CheckAnimationInfoList(List<HScene.AnimationListInfo> list, int id)
			{
				for (int i = 0; i < list.Count; i++)
				{
					int num = i;
					if (list[num].id == id)
					{
						return num;
					}
				}
				return -1;
			}

			// Token: 0x06004109 RID: 16649 RVA: 0x0018BC0C File Offset: 0x0018A00C
			// Note: this type is marked as 'beforefieldinit'.
			static HSceneTables()
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				dictionary["体温"] = 0;
				dictionary["機嫌"] = 1;
				dictionary["満腹"] = 2;
				dictionary["体調"] = 3;
				dictionary["生命"] = 4;
				dictionary["やる気"] = 5;
				dictionary["H"] = 6;
				dictionary["善悪"] = 7;
				dictionary["女子力"] = 10;
				dictionary["信頼"] = 11;
				dictionary["人間性"] = 12;
				dictionary["本能"] = 13;
				dictionary["変態"] = 14;
				dictionary["警戒"] = 15;
				dictionary["闇"] = 16;
				dictionary["社交"] = 17;
				dictionary["トイレ"] = 100;
				dictionary["風呂"] = 101;
				dictionary["睡眠"] = 102;
				dictionary["食事"] = 103;
				dictionary["休憩"] = 104;
				dictionary["ギフト"] = 105;
				dictionary["おねだり"] = 106;
				dictionary["寂しい"] = 107;
				dictionary["H欲"] = 108;
				dictionary["採取"] = 110;
				dictionary["遊び"] = 111;
				dictionary["料理"] = 112;
				dictionary["動物"] = 113;
				dictionary["ロケ"] = 114;
				dictionary["飲み物"] = 115;
				dictionary["ゲージ"] = -1;
				Resources.HSceneTables.HTagTable = dictionary;
			}

			// Token: 0x04003CC0 RID: 15552
			private ExcelData excelData;

			// Token: 0x04003CC1 RID: 15553
			private List<string> row = new List<string>();

			// Token: 0x04003CC2 RID: 15554
			private List<string> pathList = new List<string>();

			// Token: 0x04003CC3 RID: 15555
			private GameObject commonSpace;

			// Token: 0x04003CC4 RID: 15556
			public List<HScene.AnimationListInfo>[] lstAnimInfo = new List<HScene.AnimationListInfo>[6];

			// Token: 0x04003CC5 RID: 15557
			public List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> lstStartAnimInfo = new List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>>();

			// Token: 0x04003CC6 RID: 15558
			public List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>> lstStartAnimInfoM = new List<UnityEx.ValueTuple<HSceneManager.HEvent, int, HScene.StartMotion>>();

			// Token: 0x04003CC7 RID: 15559
			public List<StartWaitAnim> startWaitAnims = new List<StartWaitAnim>();

			// Token: 0x04003CC8 RID: 15560
			public List<HScene.EndMotion> lstEndAnimInfo = new List<HScene.EndMotion>();

			// Token: 0x04003CC9 RID: 15561
			public Dictionary<int, List<string>> lstHitObject = new Dictionary<int, List<string>>();

			// Token: 0x04003CCA RID: 15562
			public Dictionary<string, List<string>> HitObjAtariName = new Dictionary<string, List<string>>();

			// Token: 0x04003CCB RID: 15563
			public Dictionary<string, List<HitObjectCtrl.CollisionInfo>> DicLstHitObjInfo = new Dictionary<string, List<HitObjectCtrl.CollisionInfo>>();

			// Token: 0x04003CCC RID: 15564
			public Dictionary<int, Dictionary<int, Dictionary<string, GameObject>>> DicHitObject = new Dictionary<int, Dictionary<int, Dictionary<string, GameObject>>>();

			// Token: 0x04003CCD RID: 15565
			public List<Dictionary<int, List<HItemCtrl.ListItem>>>[] lstHItemObjInfo = new List<Dictionary<int, List<HItemCtrl.ListItem>>>[6];

			// Token: 0x04003CCE RID: 15566
			public List<UnityEx.ValueTuple<string, RuntimeAnimatorController>> lstHItemBase = new List<UnityEx.ValueTuple<string, RuntimeAnimatorController>>();

			// Token: 0x04003CCF RID: 15567
			private RuntimeAnimatorController tmpHItemRuntimeAnimator;

			// Token: 0x04003CD0 RID: 15568
			private List<string> HitemPathList = new List<string>();

			// Token: 0x04003CD1 RID: 15569
			public Dictionary<int, HPointList> hPointLists = new Dictionary<int, HPointList>();

			// Token: 0x04003CD2 RID: 15570
			public Dictionary<int, AutoHPointData> autoHPointDatas;

			// Token: 0x04003CD3 RID: 15571
			public Dictionary<int, HPointList.LoadInfo> hPointListInfos = new Dictionary<int, HPointList.LoadInfo>();

			// Token: 0x04003CD4 RID: 15572
			public Dictionary<int, HPoint.HpointData> loadHPointDatas = new Dictionary<int, HPoint.HpointData>();

			// Token: 0x04003CD5 RID: 15573
			public Dictionary<int, List<UnityEx.ValueTuple<string, string>>> AutoHpointData = new Dictionary<int, List<UnityEx.ValueTuple<string, string>>>();

			// Token: 0x04003CD6 RID: 15574
			public GameObject HPointObj;

			// Token: 0x04003CD7 RID: 15575
			public List<string> HAutoPathList;

			// Token: 0x04003CD8 RID: 15576
			public HAutoCtrl.HAutoInfo HAutoInfo;

			// Token: 0x04003CD9 RID: 15577
			public HAutoCtrl.AutoLeaveItToYou HAutoLeaveItToYou;

			// Token: 0x04003CDA RID: 15578
			public Dictionary<int, float> autoLeavePersonalityRate = new Dictionary<int, float>();

			// Token: 0x04003CDB RID: 15579
			public Dictionary<int, float> autoLeaveAttributeRate = new Dictionary<int, float>();

			// Token: 0x04003CDC RID: 15580
			public string[,] aHsceneBGM;

			// Token: 0x04003CDD RID: 15581
			public List<HParticleCtrl.ParticleInfo> lstHParticleCtrl = new List<HParticleCtrl.ParticleInfo>();

			// Token: 0x04003CDE RID: 15582
			public HParticleCtrl hParticle;

			// Token: 0x04003CDF RID: 15583
			public RuntimeAnimatorController[,] HBaseRuntimeAnimatorControllers = new RuntimeAnimatorController[2, 3];

			// Token: 0x04003CE0 RID: 15584
			private Dictionary<int, Dictionary<int, Resources.HSceneTables.HmeshInfo>> HmeshDictionary = new Dictionary<int, Dictionary<int, Resources.HSceneTables.HmeshInfo>>();

			// Token: 0x04003CE1 RID: 15585
			private Resources.HSceneTables.HmeshInfo hMeshInfo = default(Resources.HSceneTables.HmeshInfo);

			// Token: 0x04003CE2 RID: 15586
			public Dictionary<int, GameObject> HMeshObjDic = new Dictionary<int, GameObject>();

			// Token: 0x04003CE3 RID: 15587
			public Dictionary<int, Dictionary<int, List<YureCtrl.Info>>> DicDicYure = new Dictionary<int, Dictionary<int, List<YureCtrl.Info>>>();

			// Token: 0x04003CE4 RID: 15588
			public Dictionary<int, Dictionary<int, List<YureCtrlMale.Info>>> DicDicYureMale = new Dictionary<int, Dictionary<int, List<YureCtrlMale.Info>>>();

			// Token: 0x04003CE5 RID: 15589
			public Dictionary<int, List<FeelHit.FeelInfo>> DicLstHitInfo = new Dictionary<int, List<FeelHit.FeelInfo>>();

			// Token: 0x04003CE6 RID: 15590
			public Dictionary<string, List<H_Lookat_dan.MotionLookAtList>> DicLstLookAtDan = new Dictionary<string, List<H_Lookat_dan.MotionLookAtList>>();

			// Token: 0x04003CE7 RID: 15591
			public Dictionary<string, List<CollisionCtrl.CollisionInfo>> DicLstCollisionInfo = new Dictionary<string, List<CollisionCtrl.CollisionInfo>>();

			// Token: 0x04003CE8 RID: 15592
			public Dictionary<string, Dictionary<string, HLayerCtrl.HLayerInfo>> LayerInfos = new Dictionary<string, Dictionary<string, HLayerCtrl.HLayerInfo>>();

			// Token: 0x04003CEC RID: 15596
			public bool endHLoad;

			// Token: 0x04003CED RID: 15597
			public GameObject HSceneSet;

			// Token: 0x04003CEE RID: 15598
			public GameObject HSceneUISet;

			// Token: 0x04003CEF RID: 15599
			public Dictionary<int, Dictionary<string, GameObject>> HPointPrefabs = new Dictionary<int, Dictionary<string, GameObject>>();

			// Token: 0x04003CF0 RID: 15600
			public GameObject AutoHPointPrefabs;

			// Token: 0x04003CF1 RID: 15601
			private StringBuilder sbAssetName = new StringBuilder();

			// Token: 0x04003CF2 RID: 15602
			private StringBuilder sbAbName = new StringBuilder();

			// Token: 0x04003CF3 RID: 15603
			private string hscenePrefabPath = "h/scene/";

			// Token: 0x04003CF4 RID: 15604
			private readonly string[] assetNames = new string[]
			{
				"aibu",
				"houshi",
				"sonyu",
				"tokushu",
				"les",
				"3P_F2M1"
			};

			// Token: 0x04003CF5 RID: 15605
			private readonly string[,] strAssetAnimatorBase;

			// Token: 0x04003CF6 RID: 15606
			private readonly string[,] racBaseNames;

			// Token: 0x04003CF8 RID: 15608
			public HashSet<Resources.HSceneTables.HAssetBundle>[] hashUseAssetBundle;

			// Token: 0x02000915 RID: 2325
			public class HAssetBundle
			{
				// Token: 0x0600410A RID: 16650 RVA: 0x0018BDBC File Offset: 0x0018A1BC
				public HAssetBundle(string _path, string _manifest = "")
				{
					this.path = _path;
					if (_manifest != string.Empty)
					{
						this.manifest = _manifest;
					}
					else
					{
						this.manifest = "abdata";
					}
				}

				// Token: 0x04003CF9 RID: 15609
				public string path;

				// Token: 0x04003CFA RID: 15610
				public string manifest;
			}

			// Token: 0x02000916 RID: 2326
			private struct HmeshInfo
			{
				// Token: 0x04003CFB RID: 15611
				public int mapID;

				// Token: 0x04003CFC RID: 15612
				public string abName;

				// Token: 0x04003CFD RID: 15613
				public string assetName;

				// Token: 0x04003CFE RID: 15614
				public string manifest;
			}
		}

		// Token: 0x02000917 RID: 2327
		public class ItemIconTables
		{
			// Token: 0x17000C34 RID: 3124
			private Dictionary<int, System.Tuple<string, Sprite>> this[Resources.ItemIconTables.IconCategory index]
			{
				get
				{
					switch (index)
					{
					case Resources.ItemIconTables.IconCategory.System:
						return this.SystemIcon;
					case Resources.ItemIconTables.IconCategory.Menu:
						return this.MenuIcon;
					case Resources.ItemIconTables.IconCategory.Category:
						return this.CategoryIcon;
					case Resources.ItemIconTables.IconCategory.Item:
						return this.ItemIcon;
					default:
						return null;
					}
				}
			}

			// Token: 0x17000C35 RID: 3125
			// (get) Token: 0x0600410D RID: 16653 RVA: 0x00193140 File Offset: 0x00191540
			// (set) Token: 0x0600410E RID: 16654 RVA: 0x00193148 File Offset: 0x00191548
			public Dictionary<int, Sprite> InputIconTable { get; private set; } = new Dictionary<int, Sprite>();

			// Token: 0x17000C36 RID: 3126
			// (get) Token: 0x0600410F RID: 16655 RVA: 0x00193151 File Offset: 0x00191551
			// (set) Token: 0x06004110 RID: 16656 RVA: 0x00193159 File Offset: 0x00191559
			public Dictionary<int, Sprite> ActionIconTable { get; private set; } = new Dictionary<int, Sprite>();

			// Token: 0x17000C37 RID: 3127
			// (get) Token: 0x06004111 RID: 16657 RVA: 0x00193162 File Offset: 0x00191562
			// (set) Token: 0x06004112 RID: 16658 RVA: 0x0019316A File Offset: 0x0019156A
			public Dictionary<int, Sprite> ActorIconTable { get; private set; } = new Dictionary<int, Sprite>();

			// Token: 0x17000C38 RID: 3128
			// (get) Token: 0x06004113 RID: 16659 RVA: 0x00193173 File Offset: 0x00191573
			// (set) Token: 0x06004114 RID: 16660 RVA: 0x0019317B File Offset: 0x0019157B
			public Dictionary<int, Dictionary<int, int>> EquipmentIconTable { get; private set; } = new Dictionary<int, Dictionary<int, int>>();

			// Token: 0x17000C39 RID: 3129
			// (get) Token: 0x06004115 RID: 16661 RVA: 0x00193184 File Offset: 0x00191584
			// (set) Token: 0x06004116 RID: 16662 RVA: 0x0019318C File Offset: 0x0019158C
			public Dictionary<int, Dictionary<int, Sprite>> StatusIconTable { get; private set; } = new Dictionary<int, Dictionary<int, Sprite>>();

			// Token: 0x17000C3A RID: 3130
			// (get) Token: 0x06004117 RID: 16663 RVA: 0x00193195 File Offset: 0x00191595
			// (set) Token: 0x06004118 RID: 16664 RVA: 0x0019319D File Offset: 0x0019159D
			public Dictionary<int, Sprite> SickIconTable { get; private set; } = new Dictionary<int, Sprite>();

			// Token: 0x17000C3B RID: 3131
			// (get) Token: 0x06004119 RID: 16665 RVA: 0x001931A6 File Offset: 0x001915A6
			// (set) Token: 0x0600411A RID: 16666 RVA: 0x001931AE File Offset: 0x001915AE
			public Dictionary<int, Sprite> WeatherIconTable { get; private set; } = new Dictionary<int, Sprite>();

			// Token: 0x0600411B RID: 16667 RVA: 0x001931B8 File Offset: 0x001915B8
			public IEnumerator LoadIcon(Resources.ItemIconTables.IconCategory iconCategory)
			{
				List<string> pathList = CommonLib.GetAssetBundleNameListFromPath(string.Format("list/icon/{0}/", this.BundleNameListPath[iconCategory]), false);
				pathList.Sort();
				List<string> unloadFiles = new List<string>();
				List<System.Tuple<string, string>> unloadSprites = new List<System.Tuple<string, string>>();
				foreach (string file in pathList)
				{
					foreach (GameIconData gameIconData in AssetBundleManager.LoadAllAsset(file, typeof(GameIconData), null).GetAllAssets<GameIconData>())
					{
						foreach (GameIconData.Param param in gameIconData.param)
						{
							Sprite sprite = Resources.ItemIconTables.LoadSpriteAsset(param.Bundle, param.Asset, param.Manifest);
							if (!(sprite == null))
							{
								unloadSprites.Add(new System.Tuple<string, string>(param.Bundle, param.Manifest));
								int id = param.ID;
								System.Tuple<string, Sprite> value = new System.Tuple<string, Sprite>(param.Name, sprite);
								switch (iconCategory)
								{
								case Resources.ItemIconTables.IconCategory.System:
									this.SystemIcon[id] = value;
									break;
								case Resources.ItemIconTables.IconCategory.Menu:
									this.MenuIcon[id] = value;
									break;
								case Resources.ItemIconTables.IconCategory.Category:
									this.CategoryIcon[id] = value;
									break;
								case Resources.ItemIconTables.IconCategory.Item:
									this.ItemIcon[id] = value;
									break;
								}
							}
						}
					}
					unloadFiles.Add(file);
					yield return null;
				}
				foreach (System.Tuple<string, string> tuple in unloadSprites)
				{
					AssetBundleManager.UnloadAssetBundle(tuple.Item1, false, tuple.Item2, false);
				}
				foreach (string assetBundleName in unloadFiles)
				{
					AssetBundleManager.UnloadAssetBundle(assetBundleName, false, null, false);
				}
				yield break;
			}

			// Token: 0x0600411C RID: 16668 RVA: 0x001931DC File Offset: 0x001915DC
			public static void SetIcon(Resources.ItemIconTables.IconCategory category, int iconID, Image icon, bool imageName = true)
			{
				Dictionary<int, System.Tuple<string, Sprite>> dictionary = Singleton<Resources>.Instance.itemIconTables[category];
				if (!dictionary.ContainsKey(iconID))
				{
					return;
				}
				icon.sprite = dictionary[iconID].Item2;
				if (!imageName)
				{
					return;
				}
				Text componentInChildren = icon.GetComponentInChildren<Text>();
				if (componentInChildren == null)
				{
					return;
				}
				componentInChildren.text = dictionary[iconID].Item1;
				componentInChildren.enabled = (icon.sprite == null);
				icon.enabled = !componentInChildren.enabled;
			}

			// Token: 0x0600411D RID: 16669 RVA: 0x00193267 File Offset: 0x00191667
			public void Load(DefinePack definePack)
			{
				this.LoadInputIcon(definePack);
				this.LoadActionIcon(definePack);
				this.LoadActorIcon(definePack);
				this.LoadWeatherIcon(definePack);
				this.LoadEquipItemIcon(definePack);
				this.LoadStatusIcon(definePack);
				this.LoadSickIcon(definePack);
			}

			// Token: 0x0600411E RID: 16670 RVA: 0x0019329C File Offset: 0x0019169C
			public void Release()
			{
				this.CategoryIcon.Clear();
				this.ItemIcon.Clear();
				this.MenuIcon.Clear();
				this.SystemIcon.Clear();
				this.MiniMapIcon.Clear();
				this.MiniMapIconName.Clear();
				this.BaseName.Clear();
				this.InputIconTable.Clear();
				this.ActionIconTable.Clear();
				this.ActorIconTable.Clear();
				this.EquipmentIconTable.Clear();
				this.StatusIconTable.Clear();
				this.SickIconTable.Clear();
				this.WeatherIconTable.Clear();
			}

			// Token: 0x0600411F RID: 16671 RVA: 0x00193344 File Offset: 0x00191744
			private void LoadInputIcon(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.InputIconList, false);
				assetBundleNameListFromPath.Sort();
				this.LoadIcon(assetBundleNameListFromPath, this.InputIconTable);
			}

			// Token: 0x06004120 RID: 16672 RVA: 0x00193378 File Offset: 0x00191778
			private void LoadActionIcon(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActionIconList, false);
				assetBundleNameListFromPath.Sort();
				this.LoadIcon(assetBundleNameListFromPath, this.ActionIconTable);
			}

			// Token: 0x06004121 RID: 16673 RVA: 0x001933AC File Offset: 0x001917AC
			private void LoadActorIcon(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActorIconList, false);
				assetBundleNameListFromPath.Sort();
				this.LoadIcon(assetBundleNameListFromPath, this.ActorIconTable);
			}

			// Token: 0x06004122 RID: 16674 RVA: 0x001933E0 File Offset: 0x001917E0
			private void LoadWeatherIcon(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.WeatherIconList, false);
				assetBundleNameListFromPath.Sort();
				this.LoadIcon(assetBundleNameListFromPath, this.WeatherIconTable);
			}

			// Token: 0x06004123 RID: 16675 RVA: 0x00193414 File Offset: 0x00191814
			public IEnumerator LoadMinimapActionIconList()
			{
				List<string> pathList = CommonLib.GetAssetBundleNameListFromPath("list/icon/minimap/", false);
				pathList.Sort();
				yield return this.LoadMinimapActionIconList(pathList, this.MiniMapIcon);
				yield break;
			}

			// Token: 0x06004124 RID: 16676 RVA: 0x00193430 File Offset: 0x00191830
			public IEnumerator LoadMinimapActionIconNameList(DefinePack definePack)
			{
				List<string> pathList = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.MinimapIconNameList, false);
				pathList.Sort();
				yield return this.LoadMinimapActionIconNameList(pathList, this.MiniMapIconName, 0);
				yield return this.LoadMinimapActionIconNameList(pathList, this.BaseName, 1);
				yield break;
			}

			// Token: 0x06004125 RID: 16677 RVA: 0x00193454 File Offset: 0x00191854
			private void LoadIcon(List<string> pathList, Dictionary<int, Sprite> table)
			{
				foreach (string assetBundleName in pathList)
				{
					foreach (GameIconData gameIconData in AssetBundleManager.LoadAllAsset(assetBundleName, typeof(GameIconData), null).GetAllAssets<GameIconData>())
					{
						foreach (GameIconData.Param param in gameIconData.param)
						{
							Sprite sprite = Resources.ItemIconTables.LoadSpriteAsset(param.Bundle, param.Asset, param.Manifest);
							if (!(sprite == null))
							{
								Singleton<Resources>.Instance.AddLoadAssetBundle(param.Bundle, param.Manifest);
								table[param.ID] = sprite;
							}
						}
					}
					Singleton<Resources>.Instance.AddLoadAssetBundle(assetBundleName, string.Empty);
				}
			}

			// Token: 0x06004126 RID: 16678 RVA: 0x00193584 File Offset: 0x00191984
			private IEnumerator LoadIconAsync(List<string> pathList, Dictionary<int, Sprite> table)
			{
				foreach (string file in pathList)
				{
					foreach (GameIconData gameIconData in AssetBundleManager.LoadAllAsset(file, typeof(GameIconData), null).GetAllAssets<GameIconData>())
					{
						foreach (GameIconData.Param param in gameIconData.param)
						{
							Sprite sprite = Resources.ItemIconTables.LoadSpriteAsset(param.Bundle, param.Asset, param.Manifest);
							if (!(sprite == null))
							{
								Singleton<Resources>.Instance.AddLoadAssetBundle(param.Bundle, param.Manifest);
								table[param.ID] = sprite;
							}
						}
					}
					Singleton<Resources>.Instance.AddLoadAssetBundle(file, string.Empty);
					yield return null;
				}
				yield break;
			}

			// Token: 0x06004127 RID: 16679 RVA: 0x001935A8 File Offset: 0x001919A8
			private void LoadEquipItemIcon(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.EquipItemIconList, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
					ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
					if (!(excelData == null))
					{
						for (int j = 1; j < excelData.MaxCell; j++)
						{
							ExcelData.Param param = excelData.list[j];
							int num = 1;
							int key;
							if (int.TryParse(param.list.GetElement(num++), out key))
							{
								int key2;
								if (int.TryParse(param.list.GetElement(num++), out key2))
								{
									int value;
									if (int.TryParse(param.list.GetElement(num++), out value))
									{
										Dictionary<int, int> dictionary;
										if (!this.EquipmentIconTable.TryGetValue(key, out dictionary))
										{
											Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
											this.EquipmentIconTable[key] = dictionary2;
											dictionary = dictionary2;
										}
										dictionary[key2] = value;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004128 RID: 16680 RVA: 0x001936E8 File Offset: 0x00191AE8
			private void LoadStatusIcon(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.StatusIconList, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
					ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
					if (!(excelData == null))
					{
						for (int j = 1; j < excelData.MaxCell; j++)
						{
							ExcelData.Param param = excelData.list[j];
							int num = 2;
							int key;
							int key2;
							if (int.TryParse(param.list.GetElement(num++), out key) && int.TryParse(param.list.GetElement(num++), out key2))
							{
								string element = param.list.GetElement(num++);
								string element2 = param.list.GetElement(num++);
								string element3 = param.list.GetElement(num++);
								Dictionary<int, Sprite> dictionary;
								if (!this.StatusIconTable.TryGetValue(key, out dictionary))
								{
									Dictionary<int, Sprite> dictionary2 = new Dictionary<int, Sprite>();
									this.StatusIconTable[key] = dictionary2;
									dictionary = dictionary2;
								}
								Sprite value = Resources.ItemIconTables.LoadSpriteAsset(element, element2, element3);
								dictionary[key2] = value;
								Singleton<Resources>.Instance.AddLoadAssetBundle(element, element3);
							}
						}
					}
				}
			}

			// Token: 0x06004129 RID: 16681 RVA: 0x00193858 File Offset: 0x00191C58
			private void LoadSickIcon(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.SickIconList, false);
				assetBundleNameListFromPath.Sort();
				this.LoadIcon(assetBundleNameListFromPath, this.SickIconTable);
			}

			// Token: 0x0600412A RID: 16682 RVA: 0x0019388C File Offset: 0x00191C8C
			public static Sprite LoadSpriteAsset(string assetBundleName, string assetName, string manifestName)
			{
				manifestName = ((!manifestName.IsNullOrEmpty()) ? manifestName : null);
				if (AssetBundleCheck.IsSimulation)
				{
					manifestName = string.Empty;
				}
				if (!AssetBundleCheck.IsFile(assetBundleName, assetName))
				{
					string text = string.Format("読み込みエラー\r\nassetBundleName：{0}\tassetName：{1}", assetBundleName, assetName);
					return null;
				}
				AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(assetBundleName, assetName, typeof(Sprite), (!manifestName.IsNullOrEmpty()) ? manifestName : null);
				Sprite sprite = assetBundleLoadAssetOperation.GetAsset<Sprite>();
				if (sprite == null)
				{
					Texture2D asset = assetBundleLoadAssetOperation.GetAsset<Texture2D>();
					if (asset == null)
					{
						return null;
					}
					sprite = Sprite.Create(asset, new Rect(0f, 0f, (float)asset.width, (float)asset.height), Vector2.zero);
				}
				return sprite;
			}

			// Token: 0x0600412B RID: 16683 RVA: 0x00193950 File Offset: 0x00191D50
			public IEnumerator LoadMinimapActionIconList(List<string> pathList, Dictionary<int, int> table)
			{
				foreach (string file in pathList)
				{
					foreach (MinimapIconIDData minimapIconIDData in AssetBundleManager.LoadAllAsset(file, typeof(MinimapIconIDData), null).GetAllAssets<MinimapIconIDData>())
					{
						foreach (MinimapIconIDData.Param param in minimapIconIDData.param)
						{
							if (param.ActionID != -1)
							{
								table[param.ActionID] = param.ID;
							}
						}
					}
					Singleton<Resources>.Instance.AddLoadAssetBundle(file, string.Empty);
					yield return null;
				}
				yield break;
			}

			// Token: 0x0600412C RID: 16684 RVA: 0x00193974 File Offset: 0x00191D74
			public IEnumerator LoadMinimapActionIconNameList(List<string> pathList, Dictionary<int, string> table, int mode)
			{
				string asset = string.Empty;
				foreach (string file in pathList)
				{
					if (mode == 0)
					{
						asset = string.Format("Names_{0:00}", System.IO.Path.GetFileNameWithoutExtension(file));
					}
					else if (mode == 1)
					{
						asset = string.Format("BaseNames_{0:00}", System.IO.Path.GetFileNameWithoutExtension(file));
					}
					ExcelData excelData = CommonLib.LoadAsset<ExcelData>(file, asset, false, string.Empty);
					if (excelData == null)
					{
						yield return null;
					}
					else
					{
						foreach (ExcelData.Param param in excelData.list)
						{
							int key;
							if (int.TryParse(param.list[0], out key))
							{
								if (mode == 0)
								{
									if (param.list.Count >= 3)
									{
										table.Add(key, param.list[2]);
									}
								}
								else if (mode == 1)
								{
									table.Add(key, param.list[1]);
								}
							}
						}
					}
				}
				yield return null;
				yield break;
			}

			// Token: 0x04003CFF RID: 15615
			public Dictionary<int, System.Tuple<string, Sprite>> CategoryIcon = new Dictionary<int, System.Tuple<string, Sprite>>();

			// Token: 0x04003D00 RID: 15616
			public Dictionary<int, System.Tuple<string, Sprite>> ItemIcon = new Dictionary<int, System.Tuple<string, Sprite>>();

			// Token: 0x04003D01 RID: 15617
			public Dictionary<int, System.Tuple<string, Sprite>> MenuIcon = new Dictionary<int, System.Tuple<string, Sprite>>();

			// Token: 0x04003D02 RID: 15618
			public Dictionary<int, System.Tuple<string, Sprite>> SystemIcon = new Dictionary<int, System.Tuple<string, Sprite>>();

			// Token: 0x04003D03 RID: 15619
			public Dictionary<int, int> MiniMapIcon = new Dictionary<int, int>();

			// Token: 0x04003D04 RID: 15620
			public Dictionary<int, string> MiniMapIconName = new Dictionary<int, string>();

			// Token: 0x04003D05 RID: 15621
			public Dictionary<int, string> BaseName = new Dictionary<int, string>();

			// Token: 0x04003D06 RID: 15622
			private readonly Dictionary<Resources.ItemIconTables.IconCategory, string> BundleNameListPath = new Dictionary<Resources.ItemIconTables.IconCategory, string>
			{
				{
					Resources.ItemIconTables.IconCategory.System,
					"system"
				},
				{
					Resources.ItemIconTables.IconCategory.Menu,
					"menu"
				},
				{
					Resources.ItemIconTables.IconCategory.Category,
					"category"
				},
				{
					Resources.ItemIconTables.IconCategory.Item,
					"item"
				}
			};

			// Token: 0x02000918 RID: 2328
			public enum IconCategory
			{
				// Token: 0x04003D0F RID: 15631
				System,
				// Token: 0x04003D10 RID: 15632
				Menu,
				// Token: 0x04003D11 RID: 15633
				Category,
				// Token: 0x04003D12 RID: 15634
				Item
			}
		}

		// Token: 0x02000919 RID: 2329
		public class MapTables
		{
			// Token: 0x17000C3C RID: 3132
			// (get) Token: 0x0600412E RID: 16686 RVA: 0x001948F5 File Offset: 0x00192CF5
			// (set) Token: 0x0600412F RID: 16687 RVA: 0x001948FD File Offset: 0x00192CFD
			public Dictionary<int, AssetBundleInfo> MapList { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C3D RID: 3133
			// (get) Token: 0x06004130 RID: 16688 RVA: 0x00194906 File Offset: 0x00192D06
			// (set) Token: 0x06004131 RID: 16689 RVA: 0x0019490E File Offset: 0x00192D0E
			public Dictionary<int, AssetBundleInfo> ChunkList { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C3E RID: 3134
			// (get) Token: 0x06004132 RID: 16690 RVA: 0x00194917 File Offset: 0x00192D17
			// (set) Token: 0x06004133 RID: 16691 RVA: 0x0019491F File Offset: 0x00192D1F
			public Dictionary<int, AssetBundleInfo> SEMeshList { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C3F RID: 3135
			// (get) Token: 0x06004134 RID: 16692 RVA: 0x00194928 File Offset: 0x00192D28
			// (set) Token: 0x06004135 RID: 16693 RVA: 0x00194930 File Offset: 0x00192D30
			public Dictionary<int, AssetBundleInfo> CameraColliderList { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C40 RID: 3136
			// (get) Token: 0x06004136 RID: 16694 RVA: 0x00194939 File Offset: 0x00192D39
			// (set) Token: 0x06004137 RID: 16695 RVA: 0x00194941 File Offset: 0x00192D41
			public Dictionary<int, Dictionary<int, string>> MapGroupNameList { get; set; } = new Dictionary<int, Dictionary<int, string>>();

			// Token: 0x17000C41 RID: 3137
			// (get) Token: 0x06004138 RID: 16696 RVA: 0x0019494A File Offset: 0x00192D4A
			// (set) Token: 0x06004139 RID: 16697 RVA: 0x00194952 File Offset: 0x00192D52
			public Dictionary<int, Dictionary<int, List<int>>> MapGroupHiddenAreaList { get; set; } = new Dictionary<int, Dictionary<int, List<int>>>();

			// Token: 0x17000C42 RID: 3138
			// (get) Token: 0x0600413A RID: 16698 RVA: 0x0019495B File Offset: 0x00192D5B
			// (set) Token: 0x0600413B RID: 16699 RVA: 0x00194963 File Offset: 0x00192D63
			public Dictionary<int, Dictionary<int, List<int>>> AgentHiddenAreaList { get; set; } = new Dictionary<int, Dictionary<int, List<int>>>();

			// Token: 0x17000C43 RID: 3139
			// (get) Token: 0x0600413C RID: 16700 RVA: 0x0019496C File Offset: 0x00192D6C
			// (set) Token: 0x0600413D RID: 16701 RVA: 0x00194974 File Offset: 0x00192D74
			public Dictionary<int, AssetBundleInfo> PlantItemList { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C44 RID: 3140
			// (get) Token: 0x0600413E RID: 16702 RVA: 0x0019497D File Offset: 0x00192D7D
			// (set) Token: 0x0600413F RID: 16703 RVA: 0x00194985 File Offset: 0x00192D85
			public List<ItemInfo> PlantIvyFilterList { get; private set; } = new List<ItemInfo>();

			// Token: 0x17000C45 RID: 3141
			// (get) Token: 0x06004140 RID: 16704 RVA: 0x0019498E File Offset: 0x00192D8E
			// (set) Token: 0x06004141 RID: 16705 RVA: 0x00194996 File Offset: 0x00192D96
			public Dictionary<int, ActionItemInfo> EventItemList { get; private set; } = new Dictionary<int, ActionItemInfo>();

			// Token: 0x17000C46 RID: 3142
			// (get) Token: 0x06004142 RID: 16706 RVA: 0x0019499F File Offset: 0x00192D9F
			// (set) Token: 0x06004143 RID: 16707 RVA: 0x001949A7 File Offset: 0x00192DA7
			public Dictionary<int, Dictionary<int, int>> FoodEventItemList { get; private set; } = new Dictionary<int, Dictionary<int, int>>();

			// Token: 0x17000C47 RID: 3143
			// (get) Token: 0x06004144 RID: 16708 RVA: 0x001949B0 File Offset: 0x00192DB0
			// (set) Token: 0x06004145 RID: 16709 RVA: 0x001949B8 File Offset: 0x00192DB8
			public Dictionary<int, Dictionary<int, int>> FoodDateEventItemList { get; private set; } = new Dictionary<int, Dictionary<int, int>>();

			// Token: 0x17000C48 RID: 3144
			// (get) Token: 0x06004146 RID: 16710 RVA: 0x001949C1 File Offset: 0x00192DC1
			// (set) Token: 0x06004147 RID: 16711 RVA: 0x001949C9 File Offset: 0x00192DC9
			public Dictionary<int, AssetBundleInfo> NavMeshSourceList { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C49 RID: 3145
			// (get) Token: 0x06004148 RID: 16712 RVA: 0x001949D2 File Offset: 0x00192DD2
			// (set) Token: 0x06004149 RID: 16713 RVA: 0x001949DA File Offset: 0x00192DDA
			public Dictionary<int, AssetBundleInfo> BasePointGroupTable { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C4A RID: 3146
			// (get) Token: 0x0600414A RID: 16714 RVA: 0x001949E3 File Offset: 0x00192DE3
			// (set) Token: 0x0600414B RID: 16715 RVA: 0x001949EB File Offset: 0x00192DEB
			public Dictionary<int, AssetBundleInfo> DevicePointGroupTable { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C4B RID: 3147
			// (get) Token: 0x0600414C RID: 16716 RVA: 0x001949F4 File Offset: 0x00192DF4
			// (set) Token: 0x0600414D RID: 16717 RVA: 0x001949FC File Offset: 0x00192DFC
			public Dictionary<int, AssetBundleInfo> HarvestPointGroupTable { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C4C RID: 3148
			// (get) Token: 0x0600414E RID: 16718 RVA: 0x00194A05 File Offset: 0x00192E05
			// (set) Token: 0x0600414F RID: 16719 RVA: 0x00194A0D File Offset: 0x00192E0D
			public Dictionary<int, AssetBundleInfo> ShipPointGroupTable { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C4D RID: 3149
			// (get) Token: 0x06004150 RID: 16720 RVA: 0x00194A16 File Offset: 0x00192E16
			// (set) Token: 0x06004151 RID: 16721 RVA: 0x00194A1E File Offset: 0x00192E1E
			public Dictionary<int, AssetBundleInfo> ActionPointGroupTable { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C4E RID: 3150
			// (get) Token: 0x06004152 RID: 16722 RVA: 0x00194A27 File Offset: 0x00192E27
			// (set) Token: 0x06004153 RID: 16723 RVA: 0x00194A2F File Offset: 0x00192E2F
			public Dictionary<int, Dictionary<int, AssetBundleInfo>> MerchantPointGroupTable { get; private set; } = new Dictionary<int, Dictionary<int, AssetBundleInfo>>();

			// Token: 0x17000C4F RID: 3151
			// (get) Token: 0x06004154 RID: 16724 RVA: 0x00194A38 File Offset: 0x00192E38
			// (set) Token: 0x06004155 RID: 16725 RVA: 0x00194A40 File Offset: 0x00192E40
			public Dictionary<int, Dictionary<int, AssetBundleInfo>> EventPointGroupTable { get; private set; } = new Dictionary<int, Dictionary<int, AssetBundleInfo>>();

			// Token: 0x17000C50 RID: 3152
			// (get) Token: 0x06004156 RID: 16726 RVA: 0x00194A49 File Offset: 0x00192E49
			// (set) Token: 0x06004157 RID: 16727 RVA: 0x00194A51 File Offset: 0x00192E51
			public Dictionary<int, Dictionary<int, AssetBundleInfo>> StoryPointGroupTable { get; private set; } = new Dictionary<int, Dictionary<int, AssetBundleInfo>>();

			// Token: 0x17000C51 RID: 3153
			// (get) Token: 0x06004158 RID: 16728 RVA: 0x00194A5A File Offset: 0x00192E5A
			// (set) Token: 0x06004159 RID: 16729 RVA: 0x00194A62 File Offset: 0x00192E62
			public Dictionary<int, Dictionary<int, AssetBundleInfo>> LightSwitchPointGroupTable { get; private set; } = new Dictionary<int, Dictionary<int, AssetBundleInfo>>();

			// Token: 0x17000C52 RID: 3154
			// (get) Token: 0x0600415A RID: 16730 RVA: 0x00194A6B File Offset: 0x00192E6B
			// (set) Token: 0x0600415B RID: 16731 RVA: 0x00194A73 File Offset: 0x00192E73
			public Dictionary<int, Dictionary<int, List<ActionPointInfo>>> PlayerActionPointInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, List<ActionPointInfo>>>();

			// Token: 0x17000C53 RID: 3155
			// (get) Token: 0x0600415C RID: 16732 RVA: 0x00194A7C File Offset: 0x00192E7C
			// (set) Token: 0x0600415D RID: 16733 RVA: 0x00194A84 File Offset: 0x00192E84
			public Dictionary<int, Dictionary<int, List<ActionPointInfo>>> AgentActionPointInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, List<ActionPointInfo>>>();

			// Token: 0x17000C54 RID: 3156
			// (get) Token: 0x0600415E RID: 16734 RVA: 0x00194A8D File Offset: 0x00192E8D
			// (set) Token: 0x0600415F RID: 16735 RVA: 0x00194A95 File Offset: 0x00192E95
			public Dictionary<int, Dictionary<int, Dictionary<int, List<DateActionPointInfo>>>> PlayerDateActionPointInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<DateActionPointInfo>>>>();

			// Token: 0x17000C55 RID: 3157
			// (get) Token: 0x06004160 RID: 16736 RVA: 0x00194A9E File Offset: 0x00192E9E
			// (set) Token: 0x06004161 RID: 16737 RVA: 0x00194AA6 File Offset: 0x00192EA6
			public Dictionary<int, Dictionary<int, List<DateActionPointInfo>>> AgentDateActionPointInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, List<DateActionPointInfo>>>();

			// Token: 0x17000C56 RID: 3158
			// (get) Token: 0x06004162 RID: 16738 RVA: 0x00194AAF File Offset: 0x00192EAF
			// (set) Token: 0x06004163 RID: 16739 RVA: 0x00194AB7 File Offset: 0x00192EB7
			public Dictionary<int, Dictionary<int, List<ActionPointInfo>>> MerchantActionPointInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, List<ActionPointInfo>>>();

			// Token: 0x17000C57 RID: 3159
			// (get) Token: 0x06004164 RID: 16740 RVA: 0x00194AC0 File Offset: 0x00192EC0
			// (set) Token: 0x06004165 RID: 16741 RVA: 0x00194AC8 File Offset: 0x00192EC8
			public Dictionary<int, Dictionary<int, List<MerchantPointInfo>>> MerchantPointInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, List<MerchantPointInfo>>>();

			// Token: 0x17000C58 RID: 3160
			// (get) Token: 0x06004166 RID: 16742 RVA: 0x00194AD1 File Offset: 0x00192ED1
			// (set) Token: 0x06004167 RID: 16743 RVA: 0x00194AD9 File Offset: 0x00192ED9
			public Dictionary<int, AssetBundleInfo> EventParticleTable { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C59 RID: 3161
			// (get) Token: 0x06004168 RID: 16744 RVA: 0x00194AE2 File Offset: 0x00192EE2
			// (set) Token: 0x06004169 RID: 16745 RVA: 0x00194AEA File Offset: 0x00192EEA
			public Dictionary<int, Dictionary<bool, string[]>> AreaOpenStateObjectNameTable { get; private set; } = new Dictionary<int, Dictionary<bool, string[]>>();

			// Token: 0x17000C5A RID: 3162
			// (get) Token: 0x0600416A RID: 16746 RVA: 0x00194AF3 File Offset: 0x00192EF3
			// (set) Token: 0x0600416B RID: 16747 RVA: 0x00194AFB File Offset: 0x00192EFB
			public Dictionary<int, List<string>> EventPointCommandLabelTextTable { get; private set; } = new Dictionary<int, List<string>>();

			// Token: 0x17000C5B RID: 3163
			// (get) Token: 0x0600416C RID: 16748 RVA: 0x00194B04 File Offset: 0x00192F04
			// (set) Token: 0x0600416D RID: 16749 RVA: 0x00194B0C File Offset: 0x00192F0C
			public Dictionary<int, UnityEx.ValueTuple<int, List<string>>> EventDialogInfoTable { get; private set; } = new Dictionary<int, UnityEx.ValueTuple<int, List<string>>>();

			// Token: 0x17000C5C RID: 3164
			// (get) Token: 0x0600416E RID: 16750 RVA: 0x00194B15 File Offset: 0x00192F15
			// (set) Token: 0x0600416F RID: 16751 RVA: 0x00194B1D File Offset: 0x00192F1D
			public Dictionary<int, string> AreaOpenIDTable { get; private set; } = new Dictionary<int, string>();

			// Token: 0x17000C5D RID: 3165
			// (get) Token: 0x06004170 RID: 16752 RVA: 0x00194B26 File Offset: 0x00192F26
			// (set) Token: 0x06004171 RID: 16753 RVA: 0x00194B2E File Offset: 0x00192F2E
			public Dictionary<int, int[]> AreaOpenStateMapAreaLinkerTable { get; private set; } = new Dictionary<int, int[]>();

			// Token: 0x17000C5E RID: 3166
			// (get) Token: 0x06004172 RID: 16754 RVA: 0x00194B37 File Offset: 0x00192F37
			// (set) Token: 0x06004173 RID: 16755 RVA: 0x00194B3F File Offset: 0x00192F3F
			public Dictionary<int, Dictionary<int, Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>>> TimeRelationObjectStateTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>>>();

			// Token: 0x17000C5F RID: 3167
			// (get) Token: 0x06004174 RID: 16756 RVA: 0x00194B48 File Offset: 0x00192F48
			// (set) Token: 0x06004175 RID: 16757 RVA: 0x00194B50 File Offset: 0x00192F50
			public Dictionary<int, string> TimeRelationObjectIDTable { get; private set; } = new Dictionary<int, string>();

			// Token: 0x17000C60 RID: 3168
			// (get) Token: 0x06004176 RID: 16758 RVA: 0x00194B59 File Offset: 0x00192F59
			// (set) Token: 0x06004177 RID: 16759 RVA: 0x00194B61 File Offset: 0x00192F61
			public Dictionary<int, Dictionary<int, ActionCameraData>> ActionCameraDataTable { get; private set; } = new Dictionary<int, Dictionary<int, ActionCameraData>>();

			// Token: 0x17000C61 RID: 3169
			// (get) Token: 0x06004178 RID: 16760 RVA: 0x00194B6A File Offset: 0x00192F6A
			// (set) Token: 0x06004179 RID: 16761 RVA: 0x00194B72 File Offset: 0x00192F72
			public Dictionary<int, List<List<Resources.MapTables.VisibleObjectInfo>>> VanishList { get; private set; } = new Dictionary<int, List<List<Resources.MapTables.VisibleObjectInfo>>>();

			// Token: 0x17000C62 RID: 3170
			// (get) Token: 0x0600417A RID: 16762 RVA: 0x00194B7B File Offset: 0x00192F7B
			// (set) Token: 0x0600417B RID: 16763 RVA: 0x00194B83 File Offset: 0x00192F83
			public Dictionary<int, Dictionary<int, List<int>>> VanishHousingAreaGroup { get; private set; } = new Dictionary<int, Dictionary<int, List<int>>>();

			// Token: 0x17000C63 RID: 3171
			// (get) Token: 0x0600417C RID: 16764 RVA: 0x00194B8C File Offset: 0x00192F8C
			// (set) Token: 0x0600417D RID: 16765 RVA: 0x00194B94 File Offset: 0x00192F94
			public Dictionary<int, Dictionary<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>>> TempRangeTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>>>();

			// Token: 0x17000C64 RID: 3172
			// (get) Token: 0x0600417E RID: 16766 RVA: 0x00194B9D File Offset: 0x00192F9D
			// (set) Token: 0x0600417F RID: 16767 RVA: 0x00194BA5 File Offset: 0x00192FA5
			public Dictionary<int, MiniMapControler.MinimapInfo> MinimapInfoTable { get; private set; } = new Dictionary<int, MiniMapControler.MinimapInfo>();

			// Token: 0x06004180 RID: 16768 RVA: 0x00194BB0 File Offset: 0x00192FB0
			public void Load(DefinePack definePack)
			{
				this.LoadMapList(definePack);
				this.LoadChunkList(definePack);
				this.LoadSEMeshList(definePack);
				this.LoadCameraColliderList(definePack);
				this.LoadMapGroupList(definePack);
				this.LoadMapHiddenAreaList(definePack);
				this.LoadAgentVanishAreaList(definePack);
				this.LoadPlantItemList(definePack);
				this.LoadIvyFilterList(definePack);
				this.LoadNavMeshSourceTable(definePack);
				this.LoadEventItemList(definePack);
				this.LoadFoodEventItemList(definePack);
				this.LoadEventParticleList(definePack);
				this.LoadBasePointListTable(definePack);
				this.LoadDevicePointListTable(definePack);
				this.LoadFarmPointListTable(definePack);
				this.LoadShipPointListTable(definePack);
				this.LoadActionPointListTable(definePack);
				this.LoadLightSwitchPointListTable(definePack);
				this.LoadPlayerActionPointInfo(definePack);
				this.LoadAgentActionPointInfo(definePack);
				this.LoadPlayerDateActionPointInfo(definePack);
				this.LoadAgentDateActionPointInfo(definePack);
				this.LoadMerchantPoint(definePack);
				this.LoadEventPointListTable(definePack);
				this.LoadStoryPointListTable(definePack);
				this.LoadAreaOpenStateList(definePack);
				this.LoadTimeRelationInfoList(definePack);
				this.LoadAreaGroup(definePack);
				this.LoadActionCameraData(definePack);
				this.LoadVanish(definePack);
				this.LoadVanishHousingGroup(definePack);
				this.LoadEnviroInfoList(definePack);
				this.LoadMiniMapInfo();
			}

			// Token: 0x06004181 RID: 16769 RVA: 0x00194CAC File Offset: 0x001930AC
			public void Release()
			{
				this.MapList.Clear();
				this.ChunkList.Clear();
				this.SEMeshList.Clear();
				this.CameraColliderList.Clear();
				this.MapGroupNameList.Clear();
				this.MapGroupHiddenAreaList.Clear();
				this.AgentHiddenAreaList.Clear();
				this.PlantItemList.Clear();
				this.PlantIvyFilterList.Clear();
				this.EventItemList.Clear();
				this.FoodEventItemList.Clear();
				this.FoodDateEventItemList.Clear();
				this.NavMeshSourceList.Clear();
				this.BasePointGroupTable.Clear();
				this.DevicePointGroupTable.Clear();
				this.HarvestPointGroupTable.Clear();
				this.ShipPointGroupTable.Clear();
				this.ActionPointGroupTable.Clear();
				this.MerchantPointGroupTable.Clear();
				this.EventPointGroupTable.Clear();
				this.StoryPointGroupTable.Clear();
				this.LightSwitchPointGroupTable.Clear();
				this.PlayerActionPointInfoTable.Clear();
				this.AgentActionPointInfoTable.Clear();
				this.PlayerDateActionPointInfoTable.Clear();
				this.AgentDateActionPointInfoTable.Clear();
				this.MerchantActionPointInfoTable.Clear();
				this.MerchantPointInfoTable.Clear();
				this.EventParticleTable.Clear();
				this.AreaOpenStateObjectNameTable.Clear();
				this.EventPointCommandLabelTextTable.Clear();
				this.EventDialogInfoTable.Clear();
				this.AreaOpenIDTable.Clear();
				this.AreaOpenStateMapAreaLinkerTable.Clear();
				this.AreaGroupTable.Clear();
				this.TimeRelationObjectStateTable.Clear();
				this.TimeRelationObjectIDTable.Clear();
				this.ActionCameraDataTable.Clear();
				this.VanishList.Clear();
				this.TempRangeTable.Clear();
				this.MinimapInfoTable.Clear();
			}

			// Token: 0x06004182 RID: 16770 RVA: 0x00194E7C File Offset: 0x0019327C
			private void LoadMapList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.MapList, false);
				assetBundleNameListFromPath.Sort();
				if (assetBundleNameListFromPath.Count == 0)
				{
					return;
				}
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							int j = 0;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								int num = 0;
								int key;
								if (int.TryParse(param.list.GetElement(num++), out key))
								{
									string element = param.list.GetElement(num++);
									string element2 = param.list.GetElement(num++);
									string element3 = param.list.GetElement(num++);
									string element4 = param.list.GetElement(num++);
									AssetBundleInfo value = new AssetBundleInfo(element, element2, element3, element4);
									this.MapList[key] = value;
								}
							}
						}
					}
				}
			}

			// Token: 0x06004183 RID: 16771 RVA: 0x00194FC8 File Offset: 0x001933C8
			private void LoadChunkList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ChunkList, false);
				assetBundleNameListFromPath.Sort();
				if (assetBundleNameListFromPath.Count == 0)
				{
					return;
				}
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							int j = 0;
							while (j < excelData.MaxCell)
							{
								ExcelData.Param param = excelData.list[j++];
								int num = 0;
								int key;
								if (int.TryParse(param.list.GetElement(num++), out key))
								{
									string element = param.list.GetElement(num++);
									string element2 = param.list.GetElement(num++);
									string element3 = param.list.GetElement(num++);
									AssetBundleInfo value = new AssetBundleInfo(string.Empty, element, element2, element3);
									this.ChunkList[key] = value;
								}
							}
						}
					}
				}
			}

			// Token: 0x06004184 RID: 16772 RVA: 0x00195104 File Offset: 0x00193504
			private void LoadSEMeshList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ChunkList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, string.Format("semesh_{0}", fileNameWithoutExtension), string.Empty);
						if (!(excelData == null))
						{
							for (int j = 1; j < excelData.MaxCell; j++)
							{
								List<string> list = excelData.list[j].list;
								if (!list.IsNullOrEmpty<string>())
								{
									int num = 0;
									int key;
									if (int.TryParse(list.GetElement(num++) ?? string.Empty, out key))
									{
										string assetbundle_ = list.GetElement(num++) ?? string.Empty;
										string asset_ = list.GetElement(num++) ?? string.Empty;
										string manifest_ = list.GetElement(num++) ?? string.Empty;
										this.SEMeshList[key] = new AssetBundleInfo(string.Empty, assetbundle_, asset_, manifest_);
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004185 RID: 16773 RVA: 0x00195278 File Offset: 0x00193678
			private void LoadCameraColliderList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ChunkList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = CommonLib.LoadAsset<ExcelData>(text, string.Format("cam_col_{0}", fileNameWithoutExtension), false, string.Empty);
						if (!(excelData == null))
						{
							for (int j = 1; j < excelData.MaxCell; j++)
							{
								List<string> list = excelData.list[j].list;
								if (!list.IsNullOrEmpty<string>())
								{
									int num = 0;
									int key;
									if (int.TryParse(list.GetElement(num++), out key))
									{
										num++;
										string element = list.GetElement(num++);
										string element2 = list.GetElement(num++);
										string element3 = list.GetElement(num++);
										this.CameraColliderList[key] = new AssetBundleInfo(string.Empty, element, element2, element3);
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004186 RID: 16774 RVA: 0x001953C4 File Offset: 0x001937C4
			private void LoadMapGroupList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ChunkList, false);
				assetBundleNameListFromPath.Sort();
				List<string> list = ListPool<string>.Get();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (MapHiddenGroupData mapHiddenGroupData in AssetBundleManager.LoadAllAsset(text, typeof(MapHiddenGroupData), null).GetAllAssets<MapHiddenGroupData>())
						{
							foreach (MapHiddenGroupData.Param param in mapHiddenGroupData.param)
							{
								Dictionary<int, string> dictionary;
								if (!this.MapGroupNameList.TryGetValue(param.MapID, out dictionary))
								{
									Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
									this.MapGroupNameList[param.MapID] = dictionary2;
									dictionary = dictionary2;
								}
								dictionary[param.ID] = param.GroupName;
							}
						}
					}
				}
				foreach (string assetBundleName in list)
				{
					AssetBundleManager.UnloadAssetBundle(assetBundleName, false, null, false);
				}
				ListPool<string>.Release(list);
			}

			// Token: 0x06004187 RID: 16775 RVA: 0x00195560 File Offset: 0x00193960
			private void LoadMapHiddenAreaList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ChunkList, false);
				assetBundleNameListFromPath.Sort();
				List<string> list = ListPool<string>.Get();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, string.Format("area_vanish_{0}", fileNameWithoutExtension), string.Empty);
						if (!(excelData == null))
						{
							foreach (ExcelData.Param param in excelData.list)
							{
								int num = 1;
								string element = param.list.GetElement(num++);
								int key;
								if (int.TryParse(element, out key))
								{
									string element2 = param.list.GetElement(num++);
									int key2;
									if (int.TryParse(element2, out key2))
									{
										string[] array = param.list.GetElement(num++).Split(Resources.MapTables._separators, StringSplitOptions.RemoveEmptyEntries);
										if (!array.IsNullOrEmpty<string>())
										{
											Dictionary<int, List<int>> dictionary;
											if (!this.MapGroupHiddenAreaList.TryGetValue(key, out dictionary))
											{
												Dictionary<int, List<int>> dictionary2 = new Dictionary<int, List<int>>();
												this.MapGroupHiddenAreaList[key] = dictionary2;
												dictionary = dictionary2;
											}
											List<int> list2;
											if (!dictionary.TryGetValue(key2, out list2))
											{
												List<int> list3 = new List<int>();
												dictionary[key2] = list3;
												list2 = list3;
											}
											foreach (string s in array)
											{
												int item;
												if (int.TryParse(s, out item))
												{
													if (!list2.Contains(item))
													{
														list2.Add(item);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				foreach (string assetBundleName in list)
				{
					AssetBundleManager.UnloadAssetBundle(assetBundleName, false, null, false);
				}
				ListPool<string>.Release(list);
			}

			// Token: 0x06004188 RID: 16776 RVA: 0x001957F4 File Offset: 0x00193BF4
			public void LoadAgentVanishAreaList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActorVanishList, false);
				assetBundleNameListFromPath.Sort();
				List<string> list = ListPool<string>.Get();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (AgentHiddenAreaData agentHiddenAreaData in AssetBundleManager.LoadAllAsset(text, typeof(AgentHiddenAreaData), null).GetAllAssets<AgentHiddenAreaData>())
						{
							foreach (AgentHiddenAreaData.Param param in agentHiddenAreaData.param)
							{
								Dictionary<int, List<int>> dictionary;
								if (!this.AgentHiddenAreaList.TryGetValue(param.MapID, out dictionary))
								{
									Dictionary<int, List<int>> dictionary2 = new Dictionary<int, List<int>>();
									this.AgentHiddenAreaList[param.MapID] = dictionary2;
									dictionary = dictionary2;
								}
								List<int> list2;
								if (!dictionary.TryGetValue(param.AreaID, out list2))
								{
									List<int> list3 = new List<int>();
									dictionary[param.AreaID] = list3;
									list2 = list3;
								}
								string[] array = param.HiddenAreaIDMulti.Split(Resources.MapTables._separators, StringSplitOptions.RemoveEmptyEntries);
								if (!array.IsNullOrEmpty<string>())
								{
									foreach (string s in array)
									{
										int item;
										if (int.TryParse(s, out item))
										{
											if (!list2.Contains(item))
											{
												list2.Add(item);
											}
										}
									}
								}
							}
						}
					}
				}
				foreach (string assetBundleName in list)
				{
					AssetBundleManager.UnloadAssetBundle(assetBundleName, false, null, false);
				}
				ListPool<string>.Release(list);
			}

			// Token: 0x06004189 RID: 16777 RVA: 0x00195A44 File Offset: 0x00193E44
			private void LoadPlantItemList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.PlantItemList, false);
				assetBundleNameListFromPath.Sort();
				if (assetBundleNameListFromPath.Count == 0)
				{
					return;
				}
				foreach (string text in assetBundleNameListFromPath)
				{
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
					ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
					if (!(excelData == null))
					{
						int i = 0;
						while (i < excelData.MaxCell)
						{
							ExcelData.Param param = excelData.list[i++];
							int num = 0;
							int key;
							if (int.TryParse(param.list.GetElement(num++), out key))
							{
								string element = param.list.GetElement(num++);
								string element2 = param.list.GetElement(num++);
								string element3 = param.list.GetElement(num++);
								string element4 = param.list.GetElement(num++);
								this.PlantItemList[key] = new AssetBundleInfo
								{
									name = element,
									assetbundle = element2,
									asset = element3,
									manifest = element4
								};
							}
						}
					}
				}
			}

			// Token: 0x0600418A RID: 16778 RVA: 0x00195BCC File Offset: 0x00193FCC
			private void LoadIvyFilterList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.IvyFilterList, false);
				assetBundleNameListFromPath.Sort();
				if (assetBundleNameListFromPath.Count == 0)
				{
					return;
				}
				List<string> toRelease = ListPool<string>.Get();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!Game.IsRestrictedOver50(text))
					{
						foreach (PlantIvyFilterData plantIvyFilterData in AssetBundleManager.LoadAllAsset(text, typeof(PlantIvyFilterData), null).GetAllAssets<PlantIvyFilterData>())
						{
							using (List<PlantIvyFilterData.Param>.Enumerator enumerator2 = plantIvyFilterData.param.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									PlantIvyFilterData.Param param = enumerator2.Current;
									if (!this.PlantIvyFilterList.Exists((ItemInfo x) => x.CategoryID == param.CategoryID && x.ItemID == param.ItemID))
									{
										this.PlantIvyFilterList.Add(new ItemInfo
										{
											CategoryID = param.CategoryID,
											ItemID = param.ItemID,
											ObjID = param.ObjID
										});
									}
								}
							}
						}
					}
				}
				ListPool<string>.Release(toRelease);
			}

			// Token: 0x0600418B RID: 16779 RVA: 0x00195D6C File Offset: 0x0019416C
			private void LoadEventItemList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.EventItemList, false);
				assetBundleNameListFromPath.Sort();
				if (assetBundleNameListFromPath.Count == 0)
				{
					return;
				}
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
					ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
					if (!(excelData == null))
					{
						int j = 0;
						while (j < excelData.MaxCell)
						{
							ExcelData.Param param = excelData.list[j++];
							int num = 0;
							int key;
							if (int.TryParse(param.list.GetElement(num++), out key))
							{
								string element = param.list.GetElement(num++);
								string element2 = param.list.GetElement(num++);
								string element3 = param.list.GetElement(num++);
								string element4 = param.list.GetElement(num++);
								bool flag;
								bool existsAnimation = bool.TryParse(param.list.GetElement(num++), out flag) && flag;
								string element5 = param.list.GetElement(num++);
								string element6 = param.list.GetElement(num++);
								this.EventItemList[key] = new ActionItemInfo
								{
									assetbundleInfo = new AssetBundleInfo(element, element2, element3, element4),
									existsAnimation = existsAnimation,
									animeAssetBundle = new AssetBundleInfo
									{
										assetbundle = element5,
										asset = element6
									}
								};
							}
						}
					}
				}
			}

			// Token: 0x0600418C RID: 16780 RVA: 0x00195F34 File Offset: 0x00194334
			private void LoadFoodEventItemList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.EventItemList, false);
				assetBundleNameListFromPath.Sort();
				if (assetBundleNameListFromPath.Count == 0)
				{
					return;
				}
				foreach (string assetBundleName in assetBundleNameListFromPath)
				{
					foreach (FoodEventItemData foodEventItemData in AssetBundleManager.LoadAllAsset(assetBundleName, typeof(FoodEventItemData), null).GetAllAssets<FoodEventItemData>())
					{
						foreach (FoodEventItemData.Param param in foodEventItemData.param)
						{
							Dictionary<int, int> dictionary;
							if (!this.FoodEventItemList.TryGetValue(param.CategoryID, out dictionary))
							{
								Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
								this.FoodEventItemList[param.CategoryID] = dictionary2;
								dictionary = dictionary2;
							}
							dictionary[param.ItemID] = param.EventItemID;
							Dictionary<int, int> dictionary3;
							if (!this.FoodDateEventItemList.TryGetValue(param.CategoryID, out dictionary3))
							{
								Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
								this.FoodDateEventItemList[param.CategoryID] = dictionary2;
								dictionary3 = dictionary2;
							}
							int value;
							if (int.TryParse(param.DateEventItemID, out value))
							{
								dictionary3[param.ItemID] = value;
							}
						}
					}
				}
			}

			// Token: 0x0600418D RID: 16781 RVA: 0x001960EC File Offset: 0x001944EC
			private void LoadNavMeshSourceTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.MapList, false);
				assetBundleNameListFromPath.Sort();
				using (List<string>.Enumerator enumerator = assetBundleNameListFromPath.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string file = enumerator.Current;
						if (!Game.IsRestrictedOver50(file))
						{
							PointPrefabData[] allAssets = AssetBundleManager.LoadAllAsset(file, typeof(PointPrefabData), null).GetAllAssets<PointPrefabData>();
							foreach (PointPrefabData pointPrefabData in allAssets)
							{
								foreach (PointPrefabData.Param param in pointPrefabData.param)
								{
									if (param != null)
									{
										this.NavMeshSourceList[param.MapID] = new AssetBundleInfo(param.Name, param.AssetBundle, param.Asset, param.Manifest);
									}
								}
							}
							if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == file))
							{
								MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(file, string.Empty));
							}
						}
					}
				}
			}

			// Token: 0x0600418E RID: 16782 RVA: 0x00196288 File Offset: 0x00194688
			private void LoadActionPointListTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActionPointPrefabList, false);
				assetBundleNameListFromPath.Sort();
				using (List<string>.Enumerator enumerator = assetBundleNameListFromPath.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string file = enumerator.Current;
						if (!Game.IsRestrictedOver50(file))
						{
							PointPrefabData[] allAssets = AssetBundleManager.LoadAllAsset(file, typeof(PointPrefabData), null).GetAllAssets<PointPrefabData>();
							foreach (PointPrefabData pointPrefabData in allAssets)
							{
								foreach (PointPrefabData.Param param in pointPrefabData.param)
								{
									if (param != null)
									{
										this.ActionPointGroupTable[param.MapID] = new AssetBundleInfo(param.Name, param.AssetBundle, param.Asset, param.Manifest);
									}
								}
							}
							if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == file))
							{
								MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(file, string.Empty));
							}
						}
					}
				}
			}

			// Token: 0x0600418F RID: 16783 RVA: 0x00196424 File Offset: 0x00194824
			private void LoadBasePointListTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.BasePointPrefabList, false);
				assetBundleNameListFromPath.Sort();
				using (List<string>.Enumerator enumerator = assetBundleNameListFromPath.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string file = enumerator.Current;
						if (!Game.IsRestrictedOver50(file))
						{
							PointPrefabData[] allAssets = AssetBundleManager.LoadAllAsset(file, typeof(PointPrefabData), null).GetAllAssets<PointPrefabData>();
							foreach (PointPrefabData pointPrefabData in allAssets)
							{
								foreach (PointPrefabData.Param param in pointPrefabData.param)
								{
									if (param != null)
									{
										this.BasePointGroupTable[param.MapID] = new AssetBundleInfo(param.Name, param.AssetBundle, param.Asset, param.Manifest);
									}
								}
							}
							if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == file))
							{
								MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(file, string.Empty));
							}
						}
					}
				}
			}

			// Token: 0x06004190 RID: 16784 RVA: 0x001965C0 File Offset: 0x001949C0
			private void LoadDevicePointListTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.DevicePointPrefabList, false);
				assetBundleNameListFromPath.Sort();
				using (List<string>.Enumerator enumerator = assetBundleNameListFromPath.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string file = enumerator.Current;
						if (!Game.IsRestrictedOver50(file))
						{
							PointPrefabData[] allAssets = AssetBundleManager.LoadAllAsset(file, typeof(PointPrefabData), null).GetAllAssets<PointPrefabData>();
							foreach (PointPrefabData pointPrefabData in allAssets)
							{
								foreach (PointPrefabData.Param param in pointPrefabData.param)
								{
									if (param != null)
									{
										this.DevicePointGroupTable[param.MapID] = new AssetBundleInfo(param.Name, param.AssetBundle, param.Asset, param.Manifest);
									}
								}
							}
							if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == file))
							{
								MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(file, string.Empty));
							}
						}
					}
				}
			}

			// Token: 0x06004191 RID: 16785 RVA: 0x0019675C File Offset: 0x00194B5C
			private void LoadFarmPointListTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.FarmPointPrefabList, false);
				assetBundleNameListFromPath.Sort();
				using (List<string>.Enumerator enumerator = assetBundleNameListFromPath.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string file = enumerator.Current;
						if (!Game.IsRestrictedOver50(file))
						{
							PointPrefabData[] allAssets = AssetBundleManager.LoadAllAsset(file, typeof(PointPrefabData), null).GetAllAssets<PointPrefabData>();
							foreach (PointPrefabData pointPrefabData in allAssets)
							{
								foreach (PointPrefabData.Param param in pointPrefabData.param)
								{
									if (param != null)
									{
										this.HarvestPointGroupTable[param.MapID] = new AssetBundleInfo(param.Name, param.AssetBundle, param.Asset, param.Manifest);
									}
								}
							}
							if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == file))
							{
								MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(file, string.Empty));
							}
						}
					}
				}
			}

			// Token: 0x06004192 RID: 16786 RVA: 0x001968F8 File Offset: 0x00194CF8
			private void LoadShipPointListTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ShipPointPrefabList, false);
				assetBundleNameListFromPath.Sort();
				using (List<string>.Enumerator enumerator = assetBundleNameListFromPath.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string file = enumerator.Current;
						if (!Game.IsRestrictedOver50(file))
						{
							PointPrefabData[] allAssets = AssetBundleManager.LoadAllAsset(file, typeof(PointPrefabData), null).GetAllAssets<PointPrefabData>();
							foreach (PointPrefabData pointPrefabData in allAssets)
							{
								foreach (PointPrefabData.Param param in pointPrefabData.param)
								{
									if (param != null)
									{
										this.ShipPointGroupTable[param.MapID] = new AssetBundleInfo(param.Name, param.AssetBundle, param.Asset, param.Manifest);
									}
								}
							}
							if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == file))
							{
								MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(file, string.Empty));
							}
						}
					}
				}
			}

			// Token: 0x06004193 RID: 16787 RVA: 0x00196A94 File Offset: 0x00194E94
			private void LoadLightSwitchPointListTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.LightSwitchPointList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				foreach (string text in assetBundleNameListFromPath)
				{
					if (!text.IsNullOrEmpty())
					{
						if (!Game.IsRestrictedOver50(text))
						{
							string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								for (int i = 1; i < excelData.MaxCell; i++)
								{
									List<string> list = excelData.list[i].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											AssetBundleInfo assetInfo = new AssetBundleInfo(list.GetElement(num++), list.GetElement(num++), list.GetElement(num++), list.GetElement(num++));
											if (num2 == 0)
											{
												this.LoadLightSwitchPointPrefabInfo(assetInfo);
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004194 RID: 16788 RVA: 0x00196C28 File Offset: 0x00195028
			private void LoadLightSwitchPointPrefabInfo(AssetBundleInfo assetInfo)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								AssetBundleInfo value = new AssetBundleInfo(string.Empty, list.GetElement(num++), list.GetElement(num++), list.GetElement(num++));
								if (!value.assetbundle.IsNullOrEmpty() && !value.asset.IsNullOrEmpty() && !value.manifest.IsNullOrEmpty())
								{
									Dictionary<int, AssetBundleInfo> dictionary;
									if (!this.LightSwitchPointGroupTable.TryGetValue(key, out dictionary) || dictionary == null)
									{
										Dictionary<int, AssetBundleInfo> dictionary2 = new Dictionary<int, AssetBundleInfo>();
										this.LightSwitchPointGroupTable[key] = dictionary2;
										dictionary = dictionary2;
									}
									dictionary[key2] = value;
								}
							}
						}
					}
				}
			}

			// Token: 0x06004195 RID: 16789 RVA: 0x00196D84 File Offset: 0x00195184
			private void LoadPlayerActionPointInfo(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.PlayerActionPointList, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							for (int j = 1; j < excelData.MaxCell; j++)
							{
								ExcelData.Param param = excelData.list[j];
								int num = 0;
								string element = param.list.GetElement(num++);
								int key;
								if (int.TryParse(element, out key))
								{
									string element2 = param.list.GetElement(num++);
									string element3 = param.list.GetElement(num++);
									string element4 = param.list.GetElement(num++);
									ExcelData excelData2 = AssetUtility.LoadAsset<ExcelData>(element2, element3, element4);
									Dictionary<int, List<ActionPointInfo>> table;
									if (!this.PlayerActionPointInfoTable.TryGetValue(key, out table))
									{
										Dictionary<int, List<ActionPointInfo>> dictionary = new Dictionary<int, List<ActionPointInfo>>();
										this.PlayerActionPointInfoTable[key] = dictionary;
										table = dictionary;
									}
									this.LoadActionPointInfoListSheet(excelData2, table);
								}
							}
						}
					}
				}
			}

			// Token: 0x06004196 RID: 16790 RVA: 0x00196EDC File Offset: 0x001952DC
			private void LoadAgentActionPointInfo(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentActionPointList, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							for (int j = 1; j < excelData.MaxCell; j++)
							{
								ExcelData.Param param = excelData.list[j];
								int num = 0;
								string element = param.list.GetElement(num++);
								int key;
								if (int.TryParse(element, out key))
								{
									string element2 = param.list.GetElement(num++);
									string element3 = param.list.GetElement(num++);
									string element4 = param.list.GetElement(num++);
									ExcelData excelData2 = AssetUtility.LoadAsset<ExcelData>(element2, element3, element4);
									Dictionary<int, List<ActionPointInfo>> table;
									if (!this.AgentActionPointInfoTable.TryGetValue(key, out table))
									{
										Dictionary<int, List<ActionPointInfo>> dictionary = new Dictionary<int, List<ActionPointInfo>>();
										this.AgentActionPointInfoTable[key] = dictionary;
										table = dictionary;
									}
									this.LoadActionPointInfoListSheet(excelData2, table);
								}
							}
						}
					}
				}
			}

			// Token: 0x06004197 RID: 16791 RVA: 0x00197034 File Offset: 0x00195434
			private void LoadActionPointInfoListSheet(ExcelData excelData, Dictionary<int, List<ActionPointInfo>> table)
			{
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					ExcelData.Param param = excelData.list[i];
					int num = 0;
					int num2;
					if (int.TryParse(param.list.GetElement(num++), out num2))
					{
						string element = param.list.GetElement(num++);
						string element2 = param.list.GetElement(num++);
						int num3;
						if (int.TryParse(element2, out num3))
						{
							string element3 = param.list.GetElement(num++);
							int poseID;
							if (int.TryParse(element3, out poseID))
							{
								string element4 = param.list.GetElement(num++);
								int datePoseID;
								if (!int.TryParse(element4, out datePoseID))
								{
								}
								string element5 = param.list.GetElement(num++);
								bool isTalkable;
								if (bool.TryParse(element5, out isTalkable))
								{
									string element6 = param.list.GetElement(num++);
									int iconID;
									if (!int.TryParse(element6, out iconID))
									{
										iconID = -1;
									}
									string element7 = param.list.GetElement(num++);
									int cameraID;
									if (int.TryParse(element7, out cameraID))
									{
										string element8 = param.list.GetElement(num++);
										string element9 = param.list.GetElement(num++);
										string element10 = param.list.GetElement(num++);
										string element11 = param.list.GetElement(num++);
										int searchAreaID;
										if (!int.TryParse(element11, out searchAreaID))
										{
											searchAreaID = -1;
										}
										string element12 = param.list.GetElement(num++);
										int gradeValue;
										if (!int.TryParse(element12, out gradeValue))
										{
											gradeValue = -1;
										}
										string element13 = param.list.GetElement(num++);
										int doorOpenType;
										if (!int.TryParse(element13, out doorOpenType))
										{
											doorOpenType = -1;
										}
										AIProject.EventType eventTypeMask = AIProject.Definitions.Action.EventTypeTable[num3];
										List<ActionPointInfo> list;
										if (!table.TryGetValue(num2, out list))
										{
											List<ActionPointInfo> list2 = new List<ActionPointInfo>();
											table[num2] = list2;
											list = list2;
										}
										list.Add(new ActionPointInfo
										{
											actionName = element,
											pointID = num2,
											eventID = num3,
											eventTypeMask = eventTypeMask,
											poseID = poseID,
											datePoseID = datePoseID,
											isTalkable = isTalkable,
											iconID = iconID,
											cameraID = cameraID,
											baseNullName = element8,
											recoveryNullName = element9,
											labelNullName = element10,
											searchAreaID = searchAreaID,
											gradeValue = gradeValue,
											doorOpenType = doorOpenType
										});
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004198 RID: 16792 RVA: 0x001972D8 File Offset: 0x001956D8
			private void LoadPlayerDateActionPointInfo(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.PlayerActionPointList, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, string.Format("date_{0}", fileNameWithoutExtension), string.Empty);
						if (!(excelData == null))
						{
							for (int j = 1; j < excelData.MaxCell; j++)
							{
								ExcelData.Param param = excelData.list[j];
								int num = 0;
								string element = param.list.GetElement(num++);
								int key;
								if (int.TryParse(element, out key))
								{
									string element2 = param.list.GetElement(num++);
									int key2;
									if (int.TryParse(element2, out key2))
									{
										string element3 = param.list.GetElement(num++);
										string element4 = param.list.GetElement(num++);
										string element5 = param.list.GetElement(num++);
										ExcelData excelData2 = AssetUtility.LoadAsset<ExcelData>(element3, element4, element5);
										Dictionary<int, Dictionary<int, List<DateActionPointInfo>>> dictionary;
										if (!this.PlayerDateActionPointInfoTable.TryGetValue(key, out dictionary))
										{
											Dictionary<int, Dictionary<int, List<DateActionPointInfo>>> dictionary2 = new Dictionary<int, Dictionary<int, List<DateActionPointInfo>>>();
											this.PlayerDateActionPointInfoTable[key] = dictionary2;
											dictionary = dictionary2;
										}
										Dictionary<int, List<DateActionPointInfo>> dictionary3;
										if (!dictionary.TryGetValue(key2, out dictionary3))
										{
											Dictionary<int, List<DateActionPointInfo>> dictionary4 = new Dictionary<int, List<DateActionPointInfo>>();
											dictionary[key2] = dictionary4;
											dictionary3 = dictionary4;
										}
										for (int k = 1; k < excelData2.MaxCell; k++)
										{
											ExcelData.Param param2 = excelData2.list[k];
											int num2 = 0;
											int num3;
											if (int.TryParse(param2.list.GetElement(num2++), out num3))
											{
												string element6 = param2.list.GetElement(num2++);
												string element7 = param2.list.GetElement(num2++);
												int num4;
												if (int.TryParse(element7, out num4))
												{
													string element8 = param2.list.GetElement(num2++);
													int poseIDA;
													if (int.TryParse(element8, out poseIDA))
													{
														string element9 = param2.list.GetElement(num2++);
														int poseIDB;
														if (int.TryParse(element9, out poseIDB))
														{
															string element10 = param2.list.GetElement(num2++);
															bool isTalkable;
															if (!bool.TryParse(element10, out isTalkable))
															{
															}
															string element11 = param2.list.GetElement(num2++);
															int iconID;
															if (!int.TryParse(element11, out iconID))
															{
																iconID = -1;
															}
															string element12 = param2.list.GetElement(num2++);
															int cameraID;
															if (!int.TryParse(element12, out cameraID))
															{
																cameraID = -1;
															}
															string element13 = param2.list.GetElement(num2++);
															string element14 = param2.list.GetElement(num2++);
															string element15 = param2.list.GetElement(num2++);
															string element16 = param2.list.GetElement(num2++);
															string element17 = param2.list.GetElement(num2++);
															string element18 = param2.list.GetElement(num2++);
															int searchAreaID;
															if (!int.TryParse(element18, out searchAreaID))
															{
																searchAreaID = -1;
															}
															string element19 = param2.list.GetElement(num2++);
															int gradeValue;
															if (!int.TryParse(element19, out gradeValue))
															{
																gradeValue = -1;
															}
															string element20 = param2.list.GetElement(num2++);
															int doorOpenType;
															if (!int.TryParse(element20, out doorOpenType))
															{
																doorOpenType = -1;
															}
															AIProject.EventType eventTypeMask = AIProject.Definitions.Action.EventTypeTable[num4];
															List<DateActionPointInfo> list;
															if (!dictionary3.TryGetValue(num3, out list))
															{
																List<DateActionPointInfo> list2 = new List<DateActionPointInfo>();
																dictionary3[num3] = list2;
																list = list2;
															}
															list.Add(new DateActionPointInfo
															{
																actionName = element6,
																pointID = num3,
																eventID = num4,
																eventTypeMask = eventTypeMask,
																poseIDA = poseIDA,
																poseIDB = poseIDB,
																isTalkable = isTalkable,
																iconID = iconID,
																cameraID = cameraID,
																baseNullNameA = element13,
																baseNullNameB = element14,
																recoveryNullNameA = element15,
																recoveryNullNameB = element16,
																labelNullName = element17,
																searchAreaID = searchAreaID,
																gradeValue = gradeValue,
																doorOpenType = doorOpenType
															});
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004199 RID: 16793 RVA: 0x00197784 File Offset: 0x00195B84
			private void LoadAgentDateActionPointInfo(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AgentActionPointList, false);
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, string.Format("date_{0}", fileNameWithoutExtension), string.Empty);
						if (!(excelData == null))
						{
							for (int j = 1; j < excelData.MaxCell; j++)
							{
								ExcelData.Param param = excelData.list[j];
								int num = 0;
								string element = param.list.GetElement(num++);
								int key;
								if (int.TryParse(element, out key))
								{
									string element2 = param.list.GetElement(num++);
									string element3 = param.list.GetElement(num++);
									string element4 = param.list.GetElement(num++);
									ExcelData excelData2 = AssetUtility.LoadAsset<ExcelData>(element2, element3, element4);
									Dictionary<int, List<DateActionPointInfo>> dictionary;
									if (!this.AgentDateActionPointInfoTable.TryGetValue(key, out dictionary))
									{
										Dictionary<int, List<DateActionPointInfo>> dictionary2 = new Dictionary<int, List<DateActionPointInfo>>();
										this.AgentDateActionPointInfoTable[key] = dictionary2;
										dictionary = dictionary2;
									}
									for (int k = 1; k < excelData2.MaxCell; k++)
									{
										ExcelData.Param param2 = excelData2.list[k];
										int num2 = 0;
										int num3;
										if (int.TryParse(param2.list.GetElement(num2++), out num3))
										{
											string element5 = param2.list.GetElement(num2++);
											string element6 = param2.list.GetElement(num2++);
											int num4;
											if (int.TryParse(element6, out num4))
											{
												string element7 = param2.list.GetElement(num2++);
												int poseIDA;
												if (int.TryParse(element7, out poseIDA))
												{
													string element8 = param2.list.GetElement(num2++);
													int poseIDB;
													if (int.TryParse(element8, out poseIDB))
													{
														string element9 = param2.list.GetElement(num2++);
														bool isTalkable;
														if (!bool.TryParse(element9, out isTalkable))
														{
															isTalkable = false;
														}
														string element10 = param2.list.GetElement(num2++);
														int iconID;
														if (!int.TryParse(element10, out iconID))
														{
															iconID = -1;
														}
														string element11 = param2.list.GetElement(num2++);
														int cameraID;
														if (!int.TryParse(element11, out cameraID))
														{
															cameraID = 1;
														}
														string element12 = param2.list.GetElement(num2++);
														string element13 = param2.list.GetElement(num2++);
														string element14 = param2.list.GetElement(num2++);
														string element15 = param2.list.GetElement(num2++);
														string element16 = param2.list.GetElement(num2++);
														string element17 = param2.list.GetElement(num2++);
														int searchAreaID;
														if (!int.TryParse(element17, out searchAreaID))
														{
															searchAreaID = -1;
														}
														string element18 = param2.list.GetElement(num2++);
														int gradeValue;
														if (!int.TryParse(element18, out gradeValue))
														{
															gradeValue = -1;
														}
														string element19 = param2.list.GetElement(num2++);
														int doorOpenType;
														if (!int.TryParse(element19, out doorOpenType))
														{
															doorOpenType = -1;
														}
														AIProject.EventType eventTypeMask = AIProject.Definitions.Action.EventTypeTable[num4];
														List<DateActionPointInfo> list;
														if (!dictionary.TryGetValue(num3, out list))
														{
															List<DateActionPointInfo> list2 = new List<DateActionPointInfo>();
															dictionary[num3] = list2;
															list = list2;
														}
														list.Add(new DateActionPointInfo
														{
															actionName = element5,
															pointID = num3,
															eventID = num4,
															eventTypeMask = eventTypeMask,
															poseIDA = poseIDA,
															poseIDB = poseIDB,
															isTalkable = isTalkable,
															iconID = iconID,
															cameraID = cameraID,
															baseNullNameA = element12,
															baseNullNameB = element13,
															recoveryNullNameA = element14,
															recoveryNullNameB = element15,
															labelNullName = element16,
															searchAreaID = searchAreaID,
															gradeValue = gradeValue,
															doorOpenType = doorOpenType
														});
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x0600419A RID: 16794 RVA: 0x00197BE8 File Offset: 0x00195FE8
			private void LoadMerchantPoint(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.MerchantActionPointList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension;
						string id = fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										string s = list.GetElement(0) ?? string.Empty;
										int num;
										if (int.TryParse(s, out num))
										{
											int num2 = 2;
											string element = list.GetElement(num2++);
											string element2 = list.GetElement(num2++);
											if (!element.IsNullOrEmpty() && !element2.IsNullOrEmpty())
											{
												if (num != 0)
												{
													if (num != 1)
													{
														if (num == 2)
														{
															this.LoadMerchantActionPointInfo(list, id);
														}
													}
													else
													{
														this.LoadMerchantPointInfo(list, id);
													}
												}
												else
												{
													this.LoadMerchantPointPrefabInfo(list, id);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x0600419B RID: 16795 RVA: 0x00197D7C File Offset: 0x0019617C
			private void LoadMerchantPointPrefabInfo(List<string> address, string id)
			{
				int num = 2;
				string assetbundleName = address.GetElement(num++) ?? string.Empty;
				string assetName = address.GetElement(num++) ?? string.Empty;
				string manifestName = address.GetElement(num++) ?? string.Empty;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num2 = 0;
						string name_ = list.GetElement(num2++) ?? string.Empty;
						string s = list.GetElement(num2++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num2++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string text = list.GetElement(num2++) ?? string.Empty;
								string text2 = list.GetElement(num2++) ?? string.Empty;
								string text3 = list.GetElement(num2++) ?? string.Empty;
								if (!text.IsNullOrEmpty() && !text2.IsNullOrEmpty() && !text3.IsNullOrEmpty())
								{
									if (!this.MerchantPointGroupTable.ContainsKey(key))
									{
										this.MerchantPointGroupTable[key] = new Dictionary<int, AssetBundleInfo>();
									}
									this.MerchantPointGroupTable[key][key2] = new AssetBundleInfo(name_, text, text2, text3);
								}
							}
						}
					}
				}
			}

			// Token: 0x0600419C RID: 16796 RVA: 0x00197F6C File Offset: 0x0019636C
			private void LoadMerchantPointInfo(List<string> address, string id)
			{
				int num = 2;
				string assetbundleName = address.GetElement(num++) ?? string.Empty;
				string assetName = address.GetElement(num++) ?? string.Empty;
				string manifestName = address.GetElement(num++) ?? string.Empty;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num2 = 0;
						string s = list.GetElement(num2++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string text = list.GetElement(num2++) ?? string.Empty;
							string text2 = list.GetElement(num2++) ?? string.Empty;
							if (!text.IsNullOrEmpty() && !text2.IsNullOrEmpty())
							{
								ExcelData x = AssetUtility.LoadAsset<ExcelData>(text, text2, string.Empty);
								if (!(x == null))
								{
									Dictionary<int, List<MerchantPointInfo>> dic;
									if (!this.MerchantPointInfoTable.TryGetValue(key, out dic))
									{
										Dictionary<int, List<MerchantPointInfo>> dictionary = new Dictionary<int, List<MerchantPointInfo>>();
										this.MerchantPointInfoTable[key] = dictionary;
										dic = dictionary;
									}
									this.LoadMerchantPointInfoListSheet(list, id, dic);
								}
							}
						}
					}
				}
			}

			// Token: 0x0600419D RID: 16797 RVA: 0x00198100 File Offset: 0x00196500
			private void LoadMerchantPointInfoListSheet(List<string> address, string id, Dictionary<int, List<MerchantPointInfo>> dic)
			{
				int num = 1;
				string assetbundleName = address.GetElement(num++) ?? string.Empty;
				string assetName = address.GetElement(num++) ?? string.Empty;
				string manifestName = address.GetElement(num++) ?? string.Empty;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num2 = 0;
						string s = list.GetElement(num2++) ?? string.Empty;
						int num3;
						if (int.TryParse(s, out num3))
						{
							string actionName = list.GetElement(num2++) ?? string.Empty;
							string s2 = list.GetElement(num2++) ?? string.Empty;
							int num4;
							if (int.TryParse(s2, out num4))
							{
								string s3 = list.GetElement(num2++) ?? string.Empty;
								int poseID;
								if (int.TryParse(s3, out poseID))
								{
									string value = list.GetElement(num2++) ?? string.Empty;
									bool isTalkable;
									if (bool.TryParse(value, out isTalkable))
									{
										string value2 = list.GetElement(num2++) ?? string.Empty;
										bool isLooking;
										if (bool.TryParse(value2, out isLooking))
										{
											string baseNullName = list.GetElement(num2++) ?? string.Empty;
											string recoveryNullName = list.GetElement(num2++) ?? string.Empty;
											Merchant.EventType eventTypeMask = Merchant.EventTypeTable[num4];
											List<MerchantPointInfo> list2;
											if (!dic.TryGetValue(num3, out list2))
											{
												List<MerchantPointInfo> list3 = new List<MerchantPointInfo>();
												dic[num3] = list3;
												list2 = list3;
											}
											list2.Add(new MerchantPointInfo
											{
												actionName = actionName,
												pointID = num3,
												eventID = num4,
												eventTypeMask = eventTypeMask,
												poseID = poseID,
												isTalkable = isTalkable,
												isLooking = isLooking,
												baseNullName = baseNullName,
												recoveryNullName = recoveryNullName
											});
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x0600419E RID: 16798 RVA: 0x00198388 File Offset: 0x00196788
			private void LoadMerchantActionPointInfo(List<string> address, string id)
			{
				int num = 2;
				string assetbundleName = address.GetElement(num++) ?? string.Empty;
				string assetName = address.GetElement(num++) ?? string.Empty;
				string manifestName = address.GetElement(num++) ?? string.Empty;
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(assetbundleName, assetName, manifestName);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num2 = 0;
						string s = list.GetElement(num2++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string assetbundleName2 = list.GetElement(num2++) ?? string.Empty;
							string assetName2 = list.GetElement(num2++) ?? string.Empty;
							string manifestName2 = list.GetElement(num2++) ?? string.Empty;
							ExcelData excelData2 = AssetUtility.LoadAsset<ExcelData>(assetbundleName2, assetName2, manifestName2);
							if (!(excelData2 == null))
							{
								Dictionary<int, List<ActionPointInfo>> table;
								if (!this.MerchantActionPointInfoTable.TryGetValue(key, out table))
								{
									Dictionary<int, List<ActionPointInfo>> dictionary = new Dictionary<int, List<ActionPointInfo>>();
									this.MerchantActionPointInfoTable[key] = dictionary;
									table = dictionary;
								}
								this.LoadActionPointInfoListSheet(excelData2, table);
							}
						}
					}
				}
			}

			// Token: 0x0600419F RID: 16799 RVA: 0x00198518 File Offset: 0x00196918
			private void LoadEventPointListTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.EventPointList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											num++;
											string text2 = list.GetElement(num++) ?? string.Empty;
											string text3 = list.GetElement(num++) ?? string.Empty;
											if (!text2.IsNullOrEmpty() && !text3.IsNullOrEmpty())
											{
												if (num2 != 0)
												{
													if (num2 != 1)
													{
														if (num2 == 2)
														{
															this.LoadEventDialogInfo(text2, text3);
														}
													}
													else
													{
														this.LoadEventPointCommandText(text2, text3);
													}
												}
												else
												{
													this.LoadEventPointPrefabList(text2, text3);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041A0 RID: 16800 RVA: 0x001986C8 File Offset: 0x00196AC8
			private void LoadEventPointPrefabList(string sheetBundle, string sheetAsset)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetBundle, sheetAsset, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string name_ = list.GetElement(num++) ?? string.Empty;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string text = list.GetElement(num++) ?? string.Empty;
								string text2 = list.GetElement(num++) ?? string.Empty;
								string manifest_ = list.GetElement(num++) ?? string.Empty;
								if (!text.IsNullOrEmpty() && !text2.IsNullOrEmpty())
								{
									Dictionary<int, AssetBundleInfo> dictionary;
									if (!this.EventPointGroupTable.TryGetValue(key, out dictionary))
									{
										dictionary = (this.EventPointGroupTable[key] = new Dictionary<int, AssetBundleInfo>());
									}
									dictionary[key2] = new AssetBundleInfo(name_, text, text2, manifest_);
								}
							}
						}
					}
				}
			}

			// Token: 0x060041A1 RID: 16801 RVA: 0x00198844 File Offset: 0x00196C44
			private void LoadEventPointCommandText(string sheetBundle, string sheetAsset)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetBundle, sheetAsset, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							List<string> list2;
							if (!this.EventPointCommandLabelTextTable.TryGetValue(key, out list2) || list2 != null)
							{
								list2 = (this.EventPointCommandLabelTextTable[key] = new List<string>());
							}
							else
							{
								list2.Clear();
							}
							while (j < list.Count)
							{
								list2.Add(list.GetElement(j++) ?? string.Empty);
							}
						}
					}
				}
			}

			// Token: 0x060041A2 RID: 16802 RVA: 0x00198940 File Offset: 0x00196D40
			private void LoadEventDialogInfo(string sheetBundle, string sheetAsset)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetBundle, sheetAsset, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int num;
						if (int.TryParse(s, out num))
						{
							j++;
							string s2 = list.GetElement(j++) ?? string.Empty;
							int num2;
							if (int.TryParse(s2, out num2))
							{
								UnityEx.ValueTuple<int, List<string>> value;
								if (this.EventDialogInfoTable.TryGetValue(num, out value))
								{
									if (value.Item2 == null)
									{
										value.Item2 = new List<string>();
									}
									else
									{
										value.Item2.Clear();
									}
									value.Item1 = num2;
								}
								else
								{
									Dictionary<int, UnityEx.ValueTuple<int, List<string>>> eventDialogInfoTable = this.EventDialogInfoTable;
									int key = num;
									value = new UnityEx.ValueTuple<int, List<string>>(num2, new List<string>());
									eventDialogInfoTable[key] = value;
								}
								while (j < list.Count)
								{
									value.Item2.Add(list.GetElement(j++));
								}
								this.EventDialogInfoTable[num] = value;
							}
						}
					}
				}
			}

			// Token: 0x060041A3 RID: 16803 RVA: 0x00198AA0 File Offset: 0x00196EA0
			private void LoadStoryPointListTable(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.StoryPointList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											num++;
											string element = list.GetElement(num++);
											string element2 = list.GetElement(num++);
											if (!element.IsNullOrEmpty() && !element2.IsNullOrEmpty())
											{
												if (num2 == 0)
												{
													this.LoadStoryPointPrefabList(element, element2);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041A4 RID: 16804 RVA: 0x00198C0C File Offset: 0x0019700C
			private void LoadStoryPointPrefabList(string sheetBundle, string sheetAsset)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetBundle, sheetAsset, string.Empty);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string element = list.GetElement(num++);
								string element2 = list.GetElement(num++);
								string element3 = list.GetElement(num++);
								if (!element.IsNullOrEmpty() && !element2.IsNullOrEmpty())
								{
									Dictionary<int, AssetBundleInfo> dictionary;
									if (!this.StoryPointGroupTable.TryGetValue(key, out dictionary))
									{
										dictionary = (this.StoryPointGroupTable[key] = new Dictionary<int, AssetBundleInfo>());
									}
									dictionary[key2] = new AssetBundleInfo(string.Empty, element, element2, element3);
								}
							}
						}
					}
				}
			}

			// Token: 0x060041A5 RID: 16805 RVA: 0x00198D4C File Offset: 0x0019714C
			private void LoadEventParticleList(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.EventParticleList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
						if (!(excelData == null))
						{
							for (int j = 1; j < excelData.MaxCell; j++)
							{
								List<string> list = excelData.list[j].list;
								if (!list.IsNullOrEmpty<string>())
								{
									int num = 0;
									string s = list.GetElement(num++) ?? string.Empty;
									int key;
									if (int.TryParse(s, out key))
									{
										string name = list.GetElement(num++) ?? string.Empty;
										string assetbundle = list.GetElement(num++) ?? string.Empty;
										string asset = list.GetElement(num++) ?? string.Empty;
										string manifest = list.GetElement(num++) ?? string.Empty;
										this.EventParticleTable[key] = new AssetBundleInfo
										{
											name = name,
											assetbundle = assetbundle,
											asset = asset,
											manifest = manifest
										};
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041A6 RID: 16806 RVA: 0x00198EF4 File Offset: 0x001972F4
			private void LoadAreaOpenStateList(DefinePack definePack)
			{
				string areaOpenStateList = definePack.ABDirectories.AreaOpenStateList;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(areaOpenStateList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							AssetBundleInfo info = new AssetBundleInfo(string.Empty, text, fileNameWithoutExtension, string.Empty);
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(info);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											num++;
											string text2 = list.GetElement(num++) ?? string.Empty;
											string text3 = list.GetElement(num++) ?? string.Empty;
											string sheetManifest = list.GetElement(num++) ?? string.Empty;
											if (!text2.IsNullOrEmpty() && !text3.IsNullOrEmpty())
											{
												if (num2 != 0)
												{
													if (num2 != 1)
													{
														if (num2 == 2)
														{
															this.LoadAreaOpenStateMapAreaLinkerList(text2, text3, sheetManifest);
														}
													}
													else
													{
														this.LoadAreaOpenStateObjectName(text2, text3, sheetManifest);
													}
												}
												else
												{
													this.LoadAreaOpenIDList(text2, text3, sheetManifest);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041A7 RID: 16807 RVA: 0x001990DC File Offset: 0x001974DC
			private void LoadAreaOpenIDList(string sheetBundle, string sheetAsset, string sheetManifest)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetBundle, sheetAsset, sheetManifest);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string value = list.GetElement(num++) ?? string.Empty;
							this.AreaOpenIDTable[key] = value;
						}
					}
				}
			}

			// Token: 0x060041A8 RID: 16808 RVA: 0x00199190 File Offset: 0x00197590
			private void LoadAreaOpenStateObjectName(string sheetBundle, string sheetAsset, string sheetManifest)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetBundle, sheetAsset, sheetManifest);
				if (excelData == null)
				{
					return;
				}
				int key = 0;
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int num;
						if (int.TryParse(s, out num))
						{
							key = num;
						}
						string value = list.GetElement(j++) ?? string.Empty;
						bool key2;
						if (bool.TryParse(value, out key2))
						{
							List<string> list2 = ListPool<string>.Get();
							while (j < list.Count)
							{
								string element = list.GetElement(j);
								if (!element.IsNullOrEmpty())
								{
									list2.Add(element);
								}
								j++;
							}
							string[] array = new string[list2.Count];
							for (int k = 0; k < array.Length; k++)
							{
								array[k] = list2[k];
							}
							ListPool<string>.Release(list2);
							Dictionary<bool, string[]> dictionary;
							if (!this.AreaOpenStateObjectNameTable.TryGetValue(key, out dictionary))
							{
								dictionary = (this.AreaOpenStateObjectNameTable[key] = new Dictionary<bool, string[]>());
							}
							dictionary[key2] = array;
						}
					}
				}
			}

			// Token: 0x060041A9 RID: 16809 RVA: 0x001992F8 File Offset: 0x001976F8
			private void LoadAreaOpenStateMapAreaLinkerList(string sheetBundle, string sheetAsset, string sheetManifest)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetBundle, sheetAsset, sheetManifest);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							num++;
							string text = list.GetElement(num++) ?? string.Empty;
							string[] array = text.Split(Resources._separationKeywords, StringSplitOptions.RemoveEmptyEntries);
							List<int> list2 = ListPool<int>.Get();
							if (!array.IsNullOrEmpty<string>())
							{
								foreach (string text2 in array)
								{
									if (!text2.IsNullOrEmpty())
									{
										int item;
										if (int.TryParse(text2, out item))
										{
											list2.Add(item);
										}
									}
								}
							}
							int[] array3 = new int[list2.Count];
							if (!list2.IsNullOrEmpty<int>())
							{
								for (int k = 0; k < array3.Length; k++)
								{
									array3[k] = list2.GetElement(k);
								}
							}
							this.AreaOpenStateMapAreaLinkerTable[key] = array3;
							ListPool<int>.Release(list2);
						}
					}
				}
			}

			// Token: 0x060041AA RID: 16810 RVA: 0x00199464 File Offset: 0x00197864
			private void LoadTimeRelationInfoList(DefinePack definePack)
			{
				string timeRelationInfoList = definePack.ABDirectories.TimeRelationInfoList;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(timeRelationInfoList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							AssetBundleInfo info = new AssetBundleInfo(string.Empty, text, fileNameWithoutExtension, string.Empty);
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(info);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											num++;
											string text2 = list.GetElement(num++) ?? string.Empty;
											string text3 = list.GetElement(num++) ?? string.Empty;
											string text4 = list.GetElement(num++) ?? string.Empty;
											if (!text2.IsNullOrEmpty() && !text3.IsNullOrEmpty())
											{
												if (num2 != 0)
												{
													if (num2 == 1)
													{
														this.LoadTimeRelationObjectNameList(text2, text3, text4);
													}
												}
												else
												{
													this.LoadTimeRelationObjectIDList(text2, text3, text4);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041AB RID: 16811 RVA: 0x00199630 File Offset: 0x00197A30
			private void LoadTimeRelationObjectIDList(string sheetBundleName, string sheetAssetName, string sheetManifestName)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetBundleName, sheetAssetName, sheetManifestName);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							this.TimeRelationObjectIDTable[key] = (list.GetElement(num++) ?? string.Empty);
						}
					}
				}
			}

			// Token: 0x060041AC RID: 16812 RVA: 0x001996E0 File Offset: 0x00197AE0
			private void LoadTimeRelationObjectNameList(string sheetBundleName, string sheetAssetName, string manifestName)
			{
				AssetBundleInfo info = new AssetBundleInfo(string.Empty, sheetBundleName, sheetAssetName, manifestName);
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(info);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(j++) ?? string.Empty;
							int num;
							if (int.TryParse(s2, out num))
							{
								string s3 = list.GetElement(j++) ?? string.Empty;
								int key2;
								bool flag = int.TryParse(s3, out key2);
								if (!flag)
								{
									key2 = 0;
								}
								Dictionary<int, Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>> dictionary;
								if (!this.TimeRelationObjectStateTable.TryGetValue(key, out dictionary))
								{
									dictionary = (this.TimeRelationObjectStateTable[key] = new Dictionary<int, Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>>());
								}
								Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>> dictionary2;
								if (!dictionary.TryGetValue(num, out dictionary2))
								{
									dictionary2 = (dictionary[num] = new Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>());
								}
								Dictionary<int, List<UnityEx.ValueTuple<string, float>>> dictionary3;
								if (!dictionary2.TryGetValue(flag, out dictionary3))
								{
									dictionary3 = (dictionary2[flag] = new Dictionary<int, List<UnityEx.ValueTuple<string, float>>>());
								}
								List<UnityEx.ValueTuple<string, float>> list2;
								if (!dictionary3.TryGetValue(key2, out list2) || list2 == null)
								{
									list2 = (dictionary3[key2] = new List<UnityEx.ValueTuple<string, float>>());
								}
								else
								{
									list2.Clear();
								}
								if (num != 0)
								{
									if (num == 1)
									{
										while (j < list.Count)
										{
											string element = list.GetElement(j);
											if (!element.IsNullOrEmpty())
											{
												string[] source = element.Split(Resources._separators, StringSplitOptions.RemoveEmptyEntries);
												string element2 = source.GetElement(0);
												string element3 = source.GetElement(1);
												if (!element2.IsNullOrEmpty() && !element3.IsNullOrEmpty())
												{
													float i2;
													if (float.TryParse(element3, out i2))
													{
														list2.Add(new UnityEx.ValueTuple<string, float>(element2, i2));
													}
												}
											}
											j++;
										}
									}
								}
								else
								{
									while (j < list.Count)
									{
										string element4 = list.GetElement(j);
										if (!element4.IsNullOrEmpty())
										{
											list2.Add(new UnityEx.ValueTuple<string, float>(element4, 0f));
										}
										j++;
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041AD RID: 16813 RVA: 0x00199968 File Offset: 0x00197D68
			private void LoadAreaGroup(DefinePack definePack)
			{
				List<string> list = new List<string>();
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				List<string> list2 = new List<string>();
				List<int>[] array = new List<int>[2];
				list = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.AreaGroupList, false);
				list.Sort();
				for (int i = 0; i < list.Count; i++)
				{
					stringBuilder.Clear();
					stringBuilder.Append(list[i]);
					if (!Game.IsRestrictedOver50(list[i]))
					{
						stringBuilder2.Clear();
						stringBuilder2.AppendFormat("map{0}", System.IO.Path.GetFileNameWithoutExtension(list[i]));
						ExcelData excelData = CommonLib.LoadAsset<ExcelData>(stringBuilder.ToString(), stringBuilder2.ToString(), false, string.Empty);
						if (!(excelData == null))
						{
							int j = 1;
							while (j < excelData.MaxCell)
							{
								list2 = excelData.list[j++].list;
								int num = 0;
								int key = 0;
								if (int.TryParse(list2[num++], out key))
								{
									int key2 = 0;
									if (int.TryParse(list2[num++], out key2))
									{
										string[] array2 = list2[num++].Split(new char[]
										{
											','
										});
										array[0] = new List<int>();
										foreach (string text in array2)
										{
											if (text != string.Empty)
											{
												array[0].Add(int.Parse(text));
											}
										}
										if (list2.Count == num)
										{
											array[1] = new List<int>();
											array[1].Add(-1);
										}
										else
										{
											string text2 = list2[num];
											if (text2 != string.Empty)
											{
												array2 = text2.Split(new char[]
												{
													','
												});
												array[1] = new List<int>();
												foreach (string text3 in array2)
												{
													if (text3 != string.Empty)
													{
														array[1].Add(int.Parse(text3));
													}
												}
											}
											else
											{
												array[1].Add(-1);
											}
										}
										if (!this.AreaGroupTable.ContainsKey(key))
										{
											this.AreaGroupTable.Add(key, new Dictionary<int, MinimapNavimesh.AreaGroupInfo>());
										}
										MinimapNavimesh.AreaGroupInfo areaGroupInfo = new MinimapNavimesh.AreaGroupInfo();
										areaGroupInfo.areaID = array[0];
										areaGroupInfo.OpenStateID = array[1];
										if (!this.AreaGroupTable[key].ContainsKey(key2))
										{
											this.AreaGroupTable[key].Add(key2, areaGroupInfo);
										}
										else
										{
											this.AreaGroupTable[key][key2] = areaGroupInfo;
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041AE RID: 16814 RVA: 0x00199C6C File Offset: 0x0019806C
			private void LoadActionCameraData(DefinePack definePack)
			{
				List<string> list = new List<string>();
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				StringBuilder stringBuilder3 = new StringBuilder();
				StringBuilder stringBuilder4 = new StringBuilder();
				List<string> list2 = new List<string>();
				list = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.ActionCameraData, false);
				list.Sort();
				for (int i = 0; i < list.Count; i++)
				{
					stringBuilder.Clear();
					stringBuilder.Append(list[i]);
					if (!Game.IsRestrictedOver50(list[i]))
					{
						stringBuilder2.Clear();
						stringBuilder2.Append(System.IO.Path.GetFileNameWithoutExtension(list[i]));
						if (GlobalMethod.AssetFileExist(stringBuilder.ToString(), stringBuilder2.ToString(), string.Empty))
						{
							ExcelData excelData = CommonLib.LoadAsset<ExcelData>(stringBuilder.ToString(), stringBuilder2.ToString(), false, string.Empty);
							if (!(excelData == null))
							{
								int j = 1;
								while (j < excelData.MaxCell)
								{
									list2 = excelData.list[j++].list;
									int index = 1;
									stringBuilder3.Clear();
									stringBuilder4.Clear();
									if (!(list2[index] == string.Empty))
									{
										stringBuilder3.Append(list2[index++]);
										if (!(list2[index] == string.Empty))
										{
											stringBuilder4.Append(list2[index++]);
											this.LoadActionCameraData(stringBuilder3.ToString(), stringBuilder4.ToString(), stringBuilder2.ToString());
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041AF RID: 16815 RVA: 0x00199E34 File Offset: 0x00198234
			private void LoadActionCameraData(string abName, string asset, string SheetNumber)
			{
				List<string> list = new List<string>();
				int i = 2;
				int key = -1;
				int key2 = -1;
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				if (!GlobalMethod.AssetFileExist(abName, asset, string.Empty))
				{
					return;
				}
				ExcelData excelData = CommonLib.LoadAsset<ExcelData>(abName, asset, false, string.Empty);
				if (excelData == null)
				{
					return;
				}
				while (i < excelData.MaxCell)
				{
					ActionCameraData value = default(ActionCameraData);
					list = excelData.list[i++].list;
					int num = 2;
					if (list.Count < 8)
					{
					}
					if (int.TryParse(list[num++], out key))
					{
						if (int.TryParse(list[num++], out key2))
						{
							if (!float.TryParse(list[num++], out zero.x))
							{
								zero.x = 0f;
							}
							if (!float.TryParse(list[num++], out zero.y))
							{
								zero.y = 0f;
							}
							if (!float.TryParse(list[num++], out zero.z))
							{
								zero.z = 0f;
							}
							if (!float.TryParse(list[num++], out zero2.x))
							{
								zero2.x = 0f;
							}
							if (!float.TryParse(list[num++], out zero2.y))
							{
								zero2.y = 0f;
							}
							if (!float.TryParse(list[num++], out zero2.z))
							{
								zero2.z = 0f;
							}
							value.freePos = zero;
							value.nonMovePos = zero;
							value.nonMoveRot = zero2;
							if (!this.ActionCameraDataTable.ContainsKey(key))
							{
								this.ActionCameraDataTable.Add(key, new Dictionary<int, ActionCameraData>());
							}
							if (!this.ActionCameraDataTable[key].ContainsKey(key2))
							{
								this.ActionCameraDataTable[key].Add(key2, default(ActionCameraData));
							}
							this.ActionCameraDataTable[key][key2] = value;
						}
					}
				}
			}

			// Token: 0x060041B0 RID: 16816 RVA: 0x0019A084 File Offset: 0x00198484
			private void LoadVanish(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.VanishCameraList, false);
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						if (GlobalMethod.AssetFileExist(text, "map_col_name", string.Empty))
						{
							ExcelData excelData = CommonLib.LoadAsset<ExcelData>(text, "map_col_name", false, string.Empty);
							if (!(excelData == null))
							{
								int j = 0;
								int maxCell = excelData.MaxCell;
								while (j < maxCell)
								{
									ExcelData.Param param = excelData.list[j++];
									string abName = param.list[0];
									int mapID = 0;
									if (param.list.Count < 2 || !int.TryParse(param.list[1], out mapID))
									{
										mapID = 0;
									}
									this.LoadVanish(text, abName, mapID);
								}
							}
						}
					}
				}
			}

			// Token: 0x060041B1 RID: 16817 RVA: 0x0019A190 File Offset: 0x00198590
			private void LoadVanish(string assetbundle, string abName, int mapID)
			{
				if (!GlobalMethod.AssetFileExist(assetbundle, abName, string.Empty))
				{
					return;
				}
				ExcelData excelData = CommonLib.LoadAsset<ExcelData>(assetbundle, abName, false, string.Empty);
				if (excelData == null)
				{
					return;
				}
				List<Resources.MapTables.VisibleObjectInfo> list = new List<Resources.MapTables.VisibleObjectInfo>();
				int maxCell = excelData.MaxCell;
				int i = 2;
				while (i < maxCell)
				{
					ExcelData.Param param = excelData.list[i++];
					if (!param.list[0].IsNullOrEmpty())
					{
						Resources.MapTables.VisibleObjectInfo visibleObjectInfo = new Resources.MapTables.VisibleObjectInfo();
						string nameCollider = param.list[0];
						string vanishObjName = param.list[1];
						visibleObjectInfo.nameCollider = nameCollider;
						visibleObjectInfo.VanishObjName = vanishObjName;
						list.Add(visibleObjectInfo);
					}
				}
				if (!this.VanishList.ContainsKey(mapID))
				{
					this.VanishList.Add(mapID, new List<List<Resources.MapTables.VisibleObjectInfo>>());
				}
				this.VanishList[mapID].Add(new List<Resources.MapTables.VisibleObjectInfo>(list));
			}

			// Token: 0x060041B2 RID: 16818 RVA: 0x0019A294 File Offset: 0x00198694
			private void LoadVanishHousingGroup(DefinePack definePack)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(definePack.ABDirectories.VanishCameraList, false);
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(assetBundleNameListFromPath[i]);
						if (GlobalMethod.AssetFileExist(text, fileNameWithoutExtension, string.Empty))
						{
							ExcelData excelData = CommonLib.LoadAsset<ExcelData>(text, fileNameWithoutExtension, false, string.Empty);
							if (!(excelData == null))
							{
								int j = 1;
								int maxCell = excelData.MaxCell;
								while (j < maxCell)
								{
									ExcelData.Param param = excelData.list[j++];
									int num = 0;
									int mapID = 0;
									if (int.TryParse(param.list[num++], out mapID))
									{
										string assetbundle = param.list[num++];
										string abName = param.list[num++];
										this.LoadVanishHousingGroup(assetbundle, abName, mapID);
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041B3 RID: 16819 RVA: 0x0019A3C0 File Offset: 0x001987C0
			private void LoadVanishHousingGroup(string assetbundle, string abName, int mapID)
			{
				if (!GlobalMethod.AssetFileExist(assetbundle, abName, string.Empty))
				{
					return;
				}
				ExcelData excelData = CommonLib.LoadAsset<ExcelData>(assetbundle, abName, false, string.Empty);
				if (excelData == null)
				{
					return;
				}
				if (!this.VanishHousingAreaGroup.ContainsKey(mapID))
				{
					this.VanishHousingAreaGroup.Add(mapID, new Dictionary<int, List<int>>());
				}
				int maxCell = excelData.MaxCell;
				int i = 1;
				int key = 0;
				while (i < maxCell)
				{
					ExcelData.Param param = excelData.list[i++];
					int index = 0;
					List<int> list = new List<int>();
					if (!param.list[index].IsNullOrEmpty())
					{
						if (int.TryParse(param.list[index++], out key))
						{
							string text = param.list[index++];
							if (!text.IsNullOrEmpty())
							{
								string[] array = text.Split(new char[]
								{
									','
								});
								foreach (string s in array)
								{
									int item = 0;
									if (int.TryParse(s, out item))
									{
										list.Add(item);
									}
								}
							}
							if (!this.VanishHousingAreaGroup[mapID].ContainsKey(key))
							{
								this.VanishHousingAreaGroup[mapID].Add(key, new List<int>());
							}
							if (list != null && list.Count > 0)
							{
								if (this.VanishHousingAreaGroup[mapID][key].Count != 0)
								{
									this.VanishHousingAreaGroup[mapID][key].Clear();
								}
								this.VanishHousingAreaGroup[mapID][key].AddRange(list);
							}
						}
					}
				}
			}

			// Token: 0x060041B4 RID: 16820 RVA: 0x0019A5A0 File Offset: 0x001989A0
			private void LoadEnviroInfoList(DefinePack definePack)
			{
				if (definePack == null)
				{
					return;
				}
				string enviroInfoList = definePack.ABDirectories.EnviroInfoList;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(enviroInfoList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							AssetBundleInfo info = new AssetBundleInfo(string.Empty, text, fileNameWithoutExtension, string.Empty);
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(info);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											string name_ = list.GetElement(num++) ?? string.Empty;
											string text2 = list.GetElement(num++) ?? string.Empty;
											string text3 = list.GetElement(num++) ?? string.Empty;
											string manifest_ = list.GetElement(num++) ?? string.Empty;
											if (!text2.IsNullOrEmpty() && !text3.IsNullOrEmpty())
											{
												AssetBundleInfo sheetInfo = new AssetBundleInfo(name_, text2, text3, manifest_);
												if (num2 == 0)
												{
													this.LoadEnvTempRangeList(sheetInfo);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041B5 RID: 16821 RVA: 0x0019A784 File Offset: 0x00198B84
			private void LoadEnvTempRangeList(AssetBundleInfo sheetInfo)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int mapID;
						if (int.TryParse(s, out mapID))
						{
							string text = list.GetElement(num++) ?? string.Empty;
							string text2 = list.GetElement(num++) ?? string.Empty;
							string manifest_ = list.GetElement(num++) ?? string.Empty;
							if (!text.IsNullOrEmpty() && !text2.IsNullOrEmpty())
							{
								this.LoadEnvTempRangeTable(new AssetBundleInfo(string.Empty, text, text2, manifest_), mapID);
							}
						}
					}
				}
			}

			// Token: 0x060041B6 RID: 16822 RVA: 0x0019A88C File Offset: 0x00198C8C
			private void LoadEnvTempRangeTable(AssetBundleInfo sheetInfo, int mapID)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(j++) ?? string.Empty;
							int key2;
							if (int.TryParse(s2, out key2))
							{
								string text = list.GetElement(j++) ?? string.Empty;
								Dictionary<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>> dictionary;
								if (!this.TempRangeTable.TryGetValue(mapID, out dictionary) || dictionary == null)
								{
									Dictionary<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>> dictionary2 = new Dictionary<int, Dictionary<int, List<UnityEx.ValueTuple<int, int>>>>();
									this.TempRangeTable[mapID] = dictionary2;
									dictionary = dictionary2;
								}
								Dictionary<int, List<UnityEx.ValueTuple<int, int>>> dictionary3;
								if (!dictionary.TryGetValue(key, out dictionary3) || dictionary3 == null)
								{
									Dictionary<int, List<UnityEx.ValueTuple<int, int>>> dictionary4 = new Dictionary<int, List<UnityEx.ValueTuple<int, int>>>();
									dictionary[key] = dictionary4;
									dictionary3 = dictionary4;
								}
								List<UnityEx.ValueTuple<int, int>> list2;
								if (!dictionary3.TryGetValue(key2, out list2) || list2 == null)
								{
									List<UnityEx.ValueTuple<int, int>> list3 = new List<UnityEx.ValueTuple<int, int>>();
									dictionary3[key2] = list3;
									list2 = list3;
								}
								else
								{
									list2.Clear();
								}
								while (j < list.Count)
								{
									string s3 = list.GetElement(j++) ?? string.Empty;
									string s4 = list.GetElement(j++) ?? string.Empty;
									int i2;
									int i3;
									if (int.TryParse(s3, out i2) && int.TryParse(s4, out i3))
									{
										list2.Add(new UnityEx.ValueTuple<int, int>(i2, i3));
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041B7 RID: 16823 RVA: 0x0019AA64 File Offset: 0x00198E64
			private void LoadMiniMapInfo()
			{
				List<string> list = new List<string>();
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				List<string> list2 = new List<string>();
				list = CommonLib.GetAssetBundleNameListFromPath(MiniMapControler.RoadPath, false);
				list.Sort();
				for (int i = 0; i < list.Count; i++)
				{
					stringBuilder.Clear();
					stringBuilder.Append(list[i]);
					if (!Game.IsRestrictedOver50(list[i]))
					{
						stringBuilder2.Clear();
						stringBuilder2.Append(System.IO.Path.GetFileNameWithoutExtension(list[i]));
						if (GlobalMethod.AssetFileExist(stringBuilder.ToString(), stringBuilder2.ToString(), string.Empty))
						{
							ExcelData excelData = CommonLib.LoadAsset<ExcelData>(stringBuilder.ToString(), stringBuilder2.ToString(), false, string.Empty);
							if (!(excelData == null))
							{
								int j = 1;
								while (j < excelData.MaxCell)
								{
									list2 = excelData.list[j++].list;
									int index = 0;
									if (!(list2[index] == string.Empty))
									{
										int key = -1;
										if (int.TryParse(list2[index++], out key))
										{
											MiniMapControler.MinimapInfo minimapInfo = new MiniMapControler.MinimapInfo();
											minimapInfo.assetPath = list2[index++];
											minimapInfo.asset = list2[index++];
											minimapInfo.manifest = list2[index++];
											if (!this.MinimapInfoTable.ContainsKey(key))
											{
												this.MinimapInfoTable.Add(key, minimapInfo);
											}
											else
											{
												this.MinimapInfoTable[key] = minimapInfo;
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x04003D35 RID: 15669
			public Dictionary<int, Dictionary<int, MinimapNavimesh.AreaGroupInfo>> AreaGroupTable = new Dictionary<int, Dictionary<int, MinimapNavimesh.AreaGroupInfo>>();

			// Token: 0x04003D3D RID: 15677
			private static readonly string[] _separators = new string[]
			{
				","
			};

			// Token: 0x0200091A RID: 2330
			public class VisibleObjectInfo
			{
				// Token: 0x04003D3E RID: 15678
				public string nameCollider;

				// Token: 0x04003D3F RID: 15679
				public string VanishObjName;
			}
		}

		// Token: 0x0200091B RID: 2331
		public class PopupInfoTable
		{
			// Token: 0x060041BA RID: 16826 RVA: 0x0019AD40 File Offset: 0x00199140
			public PopupInfoTable()
			{
				this.WarningTable = new ReadOnlyDictionary<int, string[]>(this.warningTable);
				this.RequestTable = new ReadOnlyDictionary<int, RequestInfo>(this.requestTable);
				this.StorySupportTable = new ReadOnlyDictionary<int, string[]>(this.storySupportTable);
				this.TutorialImageTable = new ReadOnlyDictionary<int, Sprite[]>(this.tutorialImageTable);
				this.TutorialPrefabTable = new ReadOnlyDictionary<int, UnityEx.ValueTuple<string, GameObject[]>>(this.tutorialPrefabTable);
				this.TutorialFilterTable = new ReadOnlyDictionary<int, Resources.TutorialUIFilterInfo>(this.tutorialFilterTable);
			}

			// Token: 0x17000C65 RID: 3173
			// (get) Token: 0x060041BB RID: 16827 RVA: 0x0019AE27 File Offset: 0x00199227
			// (set) Token: 0x060041BC RID: 16828 RVA: 0x0019AE2F File Offset: 0x0019922F
			public ReadOnlyDictionary<int, string[]> WarningTable { get; private set; }

			// Token: 0x17000C66 RID: 3174
			// (get) Token: 0x060041BD RID: 16829 RVA: 0x0019AE38 File Offset: 0x00199238
			// (set) Token: 0x060041BE RID: 16830 RVA: 0x0019AE40 File Offset: 0x00199240
			public ReadOnlyDictionary<int, RequestInfo> RequestTable { get; private set; }

			// Token: 0x17000C67 RID: 3175
			// (get) Token: 0x060041BF RID: 16831 RVA: 0x0019AE49 File Offset: 0x00199249
			// (set) Token: 0x060041C0 RID: 16832 RVA: 0x0019AE51 File Offset: 0x00199251
			public ReadOnlyDictionary<int, string[]> StorySupportTable { get; private set; }

			// Token: 0x17000C68 RID: 3176
			// (get) Token: 0x060041C1 RID: 16833 RVA: 0x0019AE5A File Offset: 0x0019925A
			// (set) Token: 0x060041C2 RID: 16834 RVA: 0x0019AE62 File Offset: 0x00199262
			public ReadOnlyDictionary<int, Sprite[]> TutorialImageTable { get; private set; }

			// Token: 0x17000C69 RID: 3177
			// (get) Token: 0x060041C3 RID: 16835 RVA: 0x0019AE6B File Offset: 0x0019926B
			// (set) Token: 0x060041C4 RID: 16836 RVA: 0x0019AE73 File Offset: 0x00199273
			public ReadOnlyDictionary<int, UnityEx.ValueTuple<string, GameObject[]>> TutorialPrefabTable { get; private set; }

			// Token: 0x17000C6A RID: 3178
			// (get) Token: 0x060041C5 RID: 16837 RVA: 0x0019AE7C File Offset: 0x0019927C
			// (set) Token: 0x060041C6 RID: 16838 RVA: 0x0019AE84 File Offset: 0x00199284
			public ReadOnlyDictionary<int, Resources.TutorialUIFilterInfo> TutorialFilterTable { get; private set; }

			// Token: 0x17000C6B RID: 3179
			// (get) Token: 0x060041C7 RID: 16839 RVA: 0x0019AE8D File Offset: 0x0019928D
			// (set) Token: 0x060041C8 RID: 16840 RVA: 0x0019AE95 File Offset: 0x00199295
			public Dictionary<int, int> RequestFlavorAdditionBorderTable { get; private set; } = new Dictionary<int, int>();

			// Token: 0x060041C9 RID: 16841 RVA: 0x0019AE9E File Offset: 0x0019929E
			private string LogAssetBundleInfo(AssetBundleInfo _info)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}]", _info.assetbundle, _info.asset, _info.manifest);
			}

			// Token: 0x060041CA RID: 16842 RVA: 0x0019AEC0 File Offset: 0x001992C0
			private string LogAssetBundleInfo(AssetBundleInfo _info, int _row, int _clm)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}] Row[{3}] Clm[{4}]", new object[]
				{
					_info.assetbundle,
					_info.asset,
					_info.manifest,
					_row,
					_clm
				});
			}

			// Token: 0x060041CB RID: 16843 RVA: 0x0019AF10 File Offset: 0x00199310
			private AssetBundleInfo GetAssetInfo(List<string> _address, ref int _idx, bool _addSummary)
			{
				string name_ = (!_addSummary) ? string.Empty : (_address.GetElement(_idx++) ?? string.Empty);
				return new AssetBundleInfo(name_, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty);
			}

			// Token: 0x060041CC RID: 16844 RVA: 0x0019AFA0 File Offset: 0x001993A0
			public void Load(DefinePack _definePack)
			{
				this.RequestFlavorAdditionBorderTable.Clear();
				string popupInfoList = _definePack.ABDirectories.PopupInfoList;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(popupInfoList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				this.tutorialImageABInfo.Clear();
				this.tutorialPrefabABInfo.Clear();
				this.tutorialFilterTable.Clear();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(text, fileNameWithoutExtension, string.Empty);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
											switch (num2)
											{
											case 0:
												this.LoadWarningInfo(assetInfo);
												break;
											case 1:
												this.LoadRequestInfo(assetInfo);
												break;
											case 2:
												this.LoadRequestText(assetInfo);
												break;
											case 3:
												this.LoadStorySupportInfo(assetInfo);
												break;
											case 4:
												this.LoadTutorialUIPrefab(assetInfo);
												break;
											case 5:
												this.LoadTutorialUIFilterInfo(assetInfo);
												break;
											}
										}
									}
								}
							}
						}
					}
				}
				List<UnityEx.ValueTuple<string, string>> toRelease = ListPool<UnityEx.ValueTuple<string, string>>.Get();
				foreach (KeyValuePair<int, List<AssetBundleInfo>> keyValuePair in this.tutorialImageABInfo)
				{
					if (!keyValuePair.Value.IsNullOrEmpty<AssetBundleInfo>())
					{
						Resources.TutorialUIFilterInfo tutorialUIFilterInfo;
						if (!this.tutorialFilterTable.TryGetValue(keyValuePair.Key, out tutorialUIFilterInfo) || Game.isAdd01 || !tutorialUIFilterInfo.Adult)
						{
							List<Sprite> list2 = ListPool<Sprite>.Get();
							foreach (AssetBundleInfo assetBundleInfo in keyValuePair.Value)
							{
								Sprite sprite = Resources.ItemIconTables.LoadSpriteAsset(assetBundleInfo.assetbundle, assetBundleInfo.asset, assetBundleInfo.manifest);
								if (!(sprite == null))
								{
									Singleton<Resources>.Instance.AddLoadAssetBundle(assetBundleInfo.assetbundle, assetBundleInfo.manifest);
									list2.Add(sprite);
								}
							}
							if (!list2.IsNullOrEmpty<Sprite>())
							{
								Sprite[] array = new Sprite[list2.Count];
								for (int k = 0; k < array.Length; k++)
								{
									array[k] = list2[k];
								}
								this.tutorialImageTable[keyValuePair.Key] = array;
							}
							ListPool<Sprite>.Release(list2);
						}
					}
				}
				ListPool<UnityEx.ValueTuple<string, string>>.Release(toRelease);
				foreach (KeyValuePair<int, UnityEx.ValueTuple<string, List<AssetBundleInfo>>> keyValuePair2 in this.tutorialPrefabABInfo)
				{
					if (!keyValuePair2.Value.Item2.IsNullOrEmpty<AssetBundleInfo>())
					{
						Resources.TutorialUIFilterInfo tutorialUIFilterInfo2;
						if (!this.tutorialFilterTable.TryGetValue(keyValuePair2.Key, out tutorialUIFilterInfo2) || Game.isAdd01 || !tutorialUIFilterInfo2.Adult)
						{
							List<GameObject> list3 = ListPool<GameObject>.Get();
							foreach (AssetBundleInfo assetBundleInfo2 in keyValuePair2.Value.Item2)
							{
								GameObject gameObject = CommonLib.LoadAsset<GameObject>(assetBundleInfo2.assetbundle, assetBundleInfo2.asset, false, assetBundleInfo2.manifest);
								if (!(gameObject == null))
								{
									Singleton<Resources>.Instance.AddLoadAssetBundle(assetBundleInfo2.assetbundle, assetBundleInfo2.manifest);
									list3.Add(gameObject);
								}
							}
							if (!list3.IsNullOrEmpty<GameObject>())
							{
								GameObject[] array2 = new GameObject[list3.Count];
								for (int l = 0; l < array2.Length; l++)
								{
									array2[l] = list3[l];
								}
								this.tutorialPrefabTable[keyValuePair2.Key] = new UnityEx.ValueTuple<string, GameObject[]>(keyValuePair2.Value.Item1, array2);
							}
							ListPool<GameObject>.Release(list3);
						}
					}
				}
				foreach (KeyValuePair<int, RequestInfo> keyValuePair3 in this.requestTable)
				{
					System.Tuple<string[], string[]> tuple;
					if (!this.requestTextTable.TryGetValue(keyValuePair3.Key, out tuple))
					{
						keyValuePair3.Value.SetText(new string[]
						{
							string.Empty,
							string.Empty
						}, new string[]
						{
							string.Empty,
							string.Empty
						});
					}
					else
					{
						keyValuePair3.Value.SetText(tuple.Item1, tuple.Item2);
					}
				}
				if (!this.RequestFlavorAdditionBorderTable.IsNullOrEmpty<int, int>())
				{
					List<UnityEx.ValueTuple<int, int>> list4 = ListPool<UnityEx.ValueTuple<int, int>>.Get();
					foreach (KeyValuePair<int, int> keyValuePair4 in this.RequestFlavorAdditionBorderTable)
					{
						list4.Add(new UnityEx.ValueTuple<int, int>(keyValuePair4.Key, keyValuePair4.Value));
					}
					this.RequestFlavorAdditionBorderTable.Clear();
					list4.Sort((UnityEx.ValueTuple<int, int> x1, UnityEx.ValueTuple<int, int> x2) => x1.Item1 - x2.Item1);
					foreach (UnityEx.ValueTuple<int, int> valueTuple in list4)
					{
						this.RequestFlavorAdditionBorderTable[valueTuple.Item1] = valueTuple.Item2;
					}
					ListPool<UnityEx.ValueTuple<int, int>>.Release(list4);
				}
			}

			// Token: 0x060041CD RID: 16845 RVA: 0x0019B6BC File Offset: 0x00199ABC
			public void Release()
			{
				this.warningTable.Clear();
				this.requestTable.Clear();
				this.requestTextTable.Clear();
				this.storySupportTable.Clear();
				this.tutorialImageTable.Clear();
				this.tutorialImageABInfo.Clear();
				this.tutorialPrefabTable.Clear();
				this.tutorialPrefabABInfo.Clear();
				this.tutorialFilterTable.Clear();
				this.RequestFlavorAdditionBorderTable.Clear();
			}

			// Token: 0x060041CE RID: 16846 RVA: 0x0019B738 File Offset: 0x00199B38
			private void LoadWarningInfo(AssetBundleInfo _address)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							num++;
							string text = list.GetElement(num++) ?? string.Empty;
							string text2 = list.GetElement(num++) ?? string.Empty;
							this.warningTable[key] = new string[]
							{
								text,
								text2
							};
						}
					}
				}
			}

			// Token: 0x060041CF RID: 16847 RVA: 0x0019B814 File Offset: 0x00199C14
			private void LoadRequestInfo(AssetBundleInfo _address)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(j++) ?? string.Empty;
							int num;
							if (int.TryParse(s2, out num))
							{
								if (num != 0)
								{
									if (num != 1)
									{
										if (num == 2)
										{
											string s3 = list.GetElement(j++) ?? string.Empty;
											int item;
											if (int.TryParse(s3, out item))
											{
												System.Tuple<int, int, int>[] array = new System.Tuple<int, int, int>[]
												{
													new System.Tuple<int, int, int>(item, 0, 0)
												};
												this.requestTable[key] = new RequestInfo(num, array);
												this.RequestFlavorAdditionBorderTable[key] = array[0].Item1;
											}
										}
									}
									else
									{
										this.requestTable[key] = new RequestInfo(num, null);
									}
								}
								else
								{
									List<System.Tuple<int, int, int>> list2 = ListPool<System.Tuple<int, int, int>>.Get();
									while (j < list.Count)
									{
										string s4 = list.GetElement(j++) ?? string.Empty;
										int item2;
										if (!int.TryParse(s4, out item2))
										{
											break;
										}
										string s5 = list.GetElement(j++) ?? string.Empty;
										int item3;
										if (!int.TryParse(s5, out item3))
										{
											break;
										}
										string s6 = list.GetElement(j++) ?? string.Empty;
										int item4;
										if (!int.TryParse(s6, out item4))
										{
											break;
										}
										list2.Add(new System.Tuple<int, int, int>(item2, item3, item4));
									}
									if (list2.IsNullOrEmpty<System.Tuple<int, int, int>>())
									{
										ListPool<System.Tuple<int, int, int>>.Release(list2);
									}
									else
									{
										System.Tuple<int, int, int>[] array2 = new System.Tuple<int, int, int>[list2.Count];
										for (int k = 0; k < list2.Count; k++)
										{
											array2[k] = list2[k];
										}
										this.requestTable[key] = new RequestInfo(num, array2);
										ListPool<System.Tuple<int, int, int>>.Release(list2);
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041D0 RID: 16848 RVA: 0x0019BA90 File Offset: 0x00199E90
			private void LoadRequestText(AssetBundleInfo _address)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string s2 = list.GetElement(num++) ?? string.Empty;
							int num2;
							if (int.TryParse(s2, out num2))
							{
								if (num2 != 0 && num2 != 2)
								{
									if (num2 == 1)
									{
										string text = list.GetElement(num++) ?? string.Empty;
										string text2 = list.GetElement(num++) ?? string.Empty;
										string text3 = list.GetElement(num++) ?? string.Empty;
										string text4 = list.GetElement(num++) ?? string.Empty;
										this.SetNewLine(ref text);
										this.SetNewLine(ref text2);
										this.SetNewLine(ref text3);
										this.SetNewLine(ref text4);
										this.requestTextTable[key] = new System.Tuple<string[], string[]>(new string[]
										{
											text,
											text3
										}, new string[]
										{
											text2,
											text4
										});
									}
								}
								else
								{
									string text5 = list.GetElement(num++) ?? string.Empty;
									num++;
									string text6 = list.GetElement(num++) ?? string.Empty;
									this.SetNewLine(ref text5);
									this.SetNewLine(ref text6);
									this.requestTextTable[key] = new System.Tuple<string[], string[]>(new string[]
									{
										text5,
										text6
									}, new string[2]);
								}
							}
						}
					}
				}
			}

			// Token: 0x060041D1 RID: 16849 RVA: 0x0019BC90 File Offset: 0x0019A090
			private void LoadStorySupportInfo(AssetBundleInfo _address)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							num += 2;
							string text = list.GetElement(num++) ?? string.Empty;
							string text2 = list.GetElement(num++) ?? string.Empty;
							this.storySupportTable[key] = new string[]
							{
								text,
								text2
							};
						}
					}
				}
			}

			// Token: 0x060041D2 RID: 16850 RVA: 0x0019BD6C File Offset: 0x0019A16C
			private void LoadTutorialImage(AssetBundleInfo _address)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							j++;
							List<UnityEx.ValueTuple<string, string, string>> list2 = ListPool<UnityEx.ValueTuple<string, string, string>>.Get();
							while (j < list.Count)
							{
								string element = list.GetElement(j++);
								string element2 = list.GetElement(j++);
								string element3 = list.GetElement(j++);
								if (!element.IsNullOrEmpty() && !element2.IsNullOrEmpty())
								{
									list2.Add(new UnityEx.ValueTuple<string, string, string>(element, element2, element3));
								}
							}
							List<AssetBundleInfo> list3;
							if (!this.tutorialImageABInfo.TryGetValue(key, out list3) || list3 == null)
							{
								list3 = (this.tutorialImageABInfo[key] = new List<AssetBundleInfo>());
							}
							else
							{
								list3.Clear();
							}
							foreach (UnityEx.ValueTuple<string, string, string> valueTuple in list2)
							{
								list3.Add(new AssetBundleInfo(string.Empty, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3));
							}
							ListPool<UnityEx.ValueTuple<string, string, string>>.Release(list2);
						}
					}
				}
			}

			// Token: 0x060041D3 RID: 16851 RVA: 0x0019BF10 File Offset: 0x0019A310
			private void LoadTutorialUIPrefab(AssetBundleInfo _address)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int j = 0;
						string s = list.GetElement(j++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							string text = list.GetElement(j++) ?? string.Empty;
							List<UnityEx.ValueTuple<string, string, string>> list2 = ListPool<UnityEx.ValueTuple<string, string, string>>.Get();
							while (j < list.Count)
							{
								string element = list.GetElement(j++);
								string element2 = list.GetElement(j++);
								string element3 = list.GetElement(j++);
								if (!element.IsNullOrEmpty() && !element2.IsNullOrEmpty())
								{
									list2.Add(new UnityEx.ValueTuple<string, string, string>(element, element2, element3));
								}
							}
							UnityEx.ValueTuple<string, List<AssetBundleInfo>> value;
							if (!this.tutorialPrefabABInfo.TryGetValue(key, out value))
							{
								UnityEx.ValueTuple<string, List<AssetBundleInfo>> valueTuple = new UnityEx.ValueTuple<string, List<AssetBundleInfo>>(text, new List<AssetBundleInfo>());
								this.tutorialPrefabABInfo[key] = valueTuple;
								value = valueTuple;
							}
							else if (value.Item2 == null)
							{
								value.Item1 = text;
								value.Item2 = new List<AssetBundleInfo>();
							}
							else
							{
								value.Item1 = text;
								value.Item2.Clear();
							}
							foreach (UnityEx.ValueTuple<string, string, string> valueTuple2 in list2)
							{
								value.Item2.Add(new AssetBundleInfo(string.Empty, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3));
							}
							ListPool<UnityEx.ValueTuple<string, string, string>>.Release(list2);
							this.tutorialPrefabABInfo[key] = value;
						}
					}
				}
			}

			// Token: 0x060041D4 RID: 16852 RVA: 0x0019C118 File Offset: 0x0019A518
			private void LoadTutorialUIFilterInfo(AssetBundleInfo _address)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							num++;
							string value = list.GetElement(num) ?? string.Empty;
							bool adult;
							if (bool.TryParse(value, out adult))
							{
								this.tutorialFilterTable[key] = new Resources.TutorialUIFilterInfo
								{
									Success = true,
									Adult = adult
								};
							}
						}
					}
				}
			}

			// Token: 0x060041D5 RID: 16853 RVA: 0x0019C1F4 File Offset: 0x0019A5F4
			private void SetNewLine(ref string str)
			{
				if (str.IsNullOrEmpty())
				{
					return;
				}
				str = str.Replace("\\n", "\n");
			}

			// Token: 0x04003D41 RID: 15681
			private Dictionary<int, string[]> warningTable = new Dictionary<int, string[]>();

			// Token: 0x04003D43 RID: 15683
			private Dictionary<int, RequestInfo> requestTable = new Dictionary<int, RequestInfo>();

			// Token: 0x04003D44 RID: 15684
			private Dictionary<int, System.Tuple<string[], string[]>> requestTextTable = new Dictionary<int, System.Tuple<string[], string[]>>();

			// Token: 0x04003D46 RID: 15686
			private Dictionary<int, string[]> storySupportTable = new Dictionary<int, string[]>();

			// Token: 0x04003D48 RID: 15688
			private Dictionary<int, Sprite[]> tutorialImageTable = new Dictionary<int, Sprite[]>();

			// Token: 0x04003D49 RID: 15689
			private Dictionary<int, List<AssetBundleInfo>> tutorialImageABInfo = new Dictionary<int, List<AssetBundleInfo>>();

			// Token: 0x04003D4B RID: 15691
			private Dictionary<int, UnityEx.ValueTuple<string, GameObject[]>> tutorialPrefabTable = new Dictionary<int, UnityEx.ValueTuple<string, GameObject[]>>();

			// Token: 0x04003D4C RID: 15692
			private Dictionary<int, UnityEx.ValueTuple<string, List<AssetBundleInfo>>> tutorialPrefabABInfo = new Dictionary<int, UnityEx.ValueTuple<string, List<AssetBundleInfo>>>();

			// Token: 0x04003D4E RID: 15694
			private Dictionary<int, Resources.TutorialUIFilterInfo> tutorialFilterTable = new Dictionary<int, Resources.TutorialUIFilterInfo>();
		}

		// Token: 0x0200091C RID: 2332
		public class TutorialUIInfo
		{
			// Token: 0x04003D51 RID: 15697
			public int groupID = -1;

			// Token: 0x04003D52 RID: 15698
			public int layoutType;

			// Token: 0x04003D53 RID: 15699
			public string title = string.Empty;

			// Token: 0x04003D54 RID: 15700
			public List<Resources.TutorialUIInfo.ElemInfo> elemList = new List<Resources.TutorialUIInfo.ElemInfo>();

			// Token: 0x0200091D RID: 2333
			public class ElemInfo
			{
				// Token: 0x04003D55 RID: 15701
				public List<UnityEx.ValueTuple<AssetBundleInfo, Sprite>> spriteInfoList = new List<UnityEx.ValueTuple<AssetBundleInfo, Sprite>>();

				// Token: 0x04003D56 RID: 15702
				public string subTitle = string.Empty;

				// Token: 0x04003D57 RID: 15703
				public string flavorText = string.Empty;
			}
		}

		// Token: 0x0200091E RID: 2334
		public struct TutorialUIFilterInfo
		{
			// Token: 0x04003D58 RID: 15704
			public bool Success;

			// Token: 0x04003D59 RID: 15705
			public bool Adult;
		}

		// Token: 0x0200091F RID: 2335
		public class SoundTable
		{
			// Token: 0x17000C6C RID: 3180
			// (get) Token: 0x060041DA RID: 16858 RVA: 0x0019C320 File Offset: 0x0019A720
			// (set) Token: 0x060041DB RID: 16859 RVA: 0x0019C328 File Offset: 0x0019A728
			public Dictionary<int, Dictionary<int, Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>>>> DefaultFootStepSETable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>>>>();

			// Token: 0x17000C6D RID: 3181
			// (get) Token: 0x060041DC RID: 16860 RVA: 0x0019C331 File Offset: 0x0019A731
			// (set) Token: 0x060041DD RID: 16861 RVA: 0x0019C339 File Offset: 0x0019A739
			public Dictionary<int, AssetBundleInfo> MapBGMABTable { get; private set; } = new Dictionary<int, AssetBundleInfo>();

			// Token: 0x17000C6E RID: 3182
			// (get) Token: 0x060041DE RID: 16862 RVA: 0x0019C342 File Offset: 0x0019A742
			// (set) Token: 0x060041DF RID: 16863 RVA: 0x0019C34A File Offset: 0x0019A74A
			public Dictionary<int, Dictionary<int, SoundPlayer.MapBGMInfo>> MapBGMInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, SoundPlayer.MapBGMInfo>>();

			// Token: 0x17000C6F RID: 3183
			// (get) Token: 0x060041E0 RID: 16864 RVA: 0x0019C353 File Offset: 0x0019A753
			// (set) Token: 0x060041E1 RID: 16865 RVA: 0x0019C35B File Offset: 0x0019A75B
			public Dictionary<int, Dictionary<int, AssetBundleInfo>> EnviroSEPrefabInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, AssetBundleInfo>>();

			// Token: 0x17000C70 RID: 3184
			// (get) Token: 0x060041E2 RID: 16866 RVA: 0x0019C364 File Offset: 0x0019A764
			// (set) Token: 0x060041E3 RID: 16867 RVA: 0x0019C36C File Offset: 0x0019A76C
			public Dictionary<int, Dictionary<int, int[]>> EnviroSEAdjacentInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, int[]>>();

			// Token: 0x17000C71 RID: 3185
			// (get) Token: 0x060041E4 RID: 16868 RVA: 0x0019C375 File Offset: 0x0019A775
			// (set) Token: 0x060041E5 RID: 16869 RVA: 0x0019C37D File Offset: 0x0019A77D
			public Dictionary<int, Dictionary<int, AssetBundleInfo>> ReverbPrefabInfoTable { get; private set; } = new Dictionary<int, Dictionary<int, AssetBundleInfo>>();

			// Token: 0x17000C72 RID: 3186
			// (get) Token: 0x060041E6 RID: 16870 RVA: 0x0019C386 File Offset: 0x0019A786
			// (set) Token: 0x060041E7 RID: 16871 RVA: 0x0019C38E File Offset: 0x0019A78E
			public Dictionary<int, Dictionary<int, AssetBundleInfo>> ActorActionVoiceDataTable { get; private set; } = new Dictionary<int, Dictionary<int, AssetBundleInfo>>();

			// Token: 0x060041E8 RID: 16872 RVA: 0x0019C398 File Offset: 0x0019A798
			public bool TryGetMapActionVoiceInfo(int personalID, int voiceID, out AssetBundleInfo info)
			{
				Dictionary<int, AssetBundleInfo> dictionary;
				if (this.ActorActionVoiceDataTable.TryGetValue(personalID, out dictionary))
				{
					return dictionary.TryGetValue(voiceID, out info);
				}
				info = default(AssetBundleInfo);
				return false;
			}

			// Token: 0x060041E9 RID: 16873 RVA: 0x0019C3D1 File Offset: 0x0019A7D1
			private string LogAssetBundleInfo(AssetBundleInfo _info)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}]", _info.assetbundle, _info.asset, _info.manifest);
			}

			// Token: 0x060041EA RID: 16874 RVA: 0x0019C3F2 File Offset: 0x0019A7F2
			private string LogAssetBundleInfo(AssetBundleInfo _info, string _ver)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}] Ver[{3}]", new object[]
				{
					_info.assetbundle,
					_info.asset,
					_info.manifest,
					_ver
				});
			}

			// Token: 0x060041EB RID: 16875 RVA: 0x0019C426 File Offset: 0x0019A826
			private string LogAssetBundleInfo(AssetBundleInfo _info, string _ver, int _row)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}] Ver[{3}] 行[{4}]", new object[]
				{
					_info.assetbundle,
					_info.asset,
					_info.manifest,
					_ver,
					_row
				});
			}

			// Token: 0x060041EC RID: 16876 RVA: 0x0019C464 File Offset: 0x0019A864
			private string LogAssetBundleInfo(AssetBundleInfo _info, string _ver, int _row, int _clm)
			{
				return string.Format("AssetBundleName[{0}] AssetName[{1}] ManifestName[{2}] Ver[{3}] 行[{4}] 列[{5}]", new object[]
				{
					_info.assetbundle,
					_info.asset,
					_info.manifest,
					_ver,
					_row,
					_clm
				});
			}

			// Token: 0x060041ED RID: 16877 RVA: 0x0019C4B8 File Offset: 0x0019A8B8
			private AssetBundleInfo GetAssetInfo(List<string> _address, ref int _idx, bool _addSummary)
			{
				string name_ = (!_addSummary) ? string.Empty : (_address.GetElement(_idx++) ?? string.Empty);
				return new AssetBundleInfo(name_, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty, _address.GetElement(_idx++) ?? string.Empty);
			}

			// Token: 0x060041EE RID: 16878 RVA: 0x0019C548 File Offset: 0x0019A948
			public void Load(DefinePack _definePack)
			{
				this.LoadMapBGMInfo(_definePack);
				this.LoadDefaultFootStep(_definePack.ABDirectories.DefaultMaleFootStepSEInfoList, 0);
				this.LoadDefaultFootStep(_definePack.ABDirectories.DefaultFemaleFootStepSEInfoList, 1);
				this.LoadEnviroInfo(_definePack);
				this.LoadPersonalVoiceInfo(_definePack.ABDirectories.MapActionVoiceInfoList, this.ActorActionVoiceDataTable);
			}

			// Token: 0x060041EF RID: 16879 RVA: 0x0019C5A0 File Offset: 0x0019A9A0
			public void Release()
			{
				this.DefaultFootStepSETable.Clear();
				this.MapBGMABTable.Clear();
				this.MapBGMInfoTable.Clear();
				this.EnviroSEPrefabInfoTable.Clear();
				this.EnviroSEAdjacentInfoTable.Clear();
				this.ReverbPrefabInfoTable.Clear();
				this.ActorActionVoiceDataTable.Clear();
			}

			// Token: 0x060041F0 RID: 16880 RVA: 0x0019C5FC File Offset: 0x0019A9FC
			private void LoadMapBGMInfo(DefinePack _definePack)
			{
				string mapBGMInfoList = _definePack.ABDirectories.MapBGMInfoList;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(mapBGMInfoList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							AssetBundleInfo info = new AssetBundleInfo(string.Empty, text, fileNameWithoutExtension, string.Empty);
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(info);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
											if (num2 != 0)
											{
												if (num2 == 1)
												{
													this.LoadMapBGMChunkInfo(assetInfo, fileNameWithoutExtension);
												}
											}
											else
											{
												this.LoadMapBGMAssetInfo(assetInfo, fileNameWithoutExtension);
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041F1 RID: 16881 RVA: 0x0019C754 File Offset: 0x0019AB54
			private void LoadMapBGMAssetInfo(AssetBundleInfo _address, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							this.MapBGMABTable[key] = this.GetAssetInfo(list, ref num, true);
						}
					}
				}
			}

			// Token: 0x060041F2 RID: 16882 RVA: 0x0019C7F4 File Offset: 0x0019ABF4
			private void LoadMapBGMChunkInfo(AssetBundleInfo _address, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int chunkID;
						if (int.TryParse(s, out chunkID))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, false);
							this.LoadMapBGMAreaInfoInChunk(assetInfo, _ver, chunkID);
						}
					}
				}
			}

			// Token: 0x060041F3 RID: 16883 RVA: 0x0019C894 File Offset: 0x0019AC94
			private void LoadMapBGMAreaInfoInChunk(AssetBundleInfo _address, string _ver, int _chunkID)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_address);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							num++;
							string s2 = list.GetElement(num++) ?? string.Empty;
							int noonID;
							if (int.TryParse(s2, out noonID))
							{
								string text = list.GetElement(num++) ?? string.Empty;
								if (!text.IsNullOrEmpty())
								{
									SoundPlayer.MapBGMInfo.Period[] array = this.LoadPeriod(text);
									if (!array.IsNullOrEmpty<SoundPlayer.MapBGMInfo.Period>())
									{
										string s3 = list.GetElement(num++) ?? string.Empty;
										int nightID;
										if (int.TryParse(s3, out nightID))
										{
											string text2 = list.GetElement(num++) ?? string.Empty;
											if (!text2.IsNullOrEmpty())
											{
												SoundPlayer.MapBGMInfo.Period[] array2 = this.LoadPeriod(text2);
												if (!array2.IsNullOrEmpty<SoundPlayer.MapBGMInfo.Period>())
												{
													Dictionary<int, SoundPlayer.MapBGMInfo> dictionary;
													if (!this.MapBGMInfoTable.TryGetValue(_chunkID, out dictionary))
													{
														Dictionary<int, SoundPlayer.MapBGMInfo> dictionary2 = new Dictionary<int, SoundPlayer.MapBGMInfo>();
														this.MapBGMInfoTable[_chunkID] = dictionary2;
														dictionary = dictionary2;
													}
													dictionary[key] = new SoundPlayer.MapBGMInfo(noonID, array, nightID, array2);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041F4 RID: 16884 RVA: 0x0019CA44 File Offset: 0x0019AE44
			private SoundPlayer.MapBGMInfo.Period[] LoadPeriod(string _timePeriodStr)
			{
				MatchCollection matchCollection = this.regex.Matches(_timePeriodStr);
				if (0 < matchCollection.Count)
				{
					List<DateTime> list = ListPool<DateTime>.Get();
					for (int i = 0; i < matchCollection.Count; i++)
					{
						List<DateTime> list2 = ListPool<DateTime>.Get();
						Match match = matchCollection[i];
						string text = match.Value.Replace(string.Empty, new string[]
						{
							"<",
							">"
						});
						string[] array = text.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
						if (!array.IsNullOrEmpty<string>() && array.Length == 2)
						{
							for (int j = 0; j < array.Length; j++)
							{
								string text2 = array[j] ?? string.Empty;
								string[] array2 = text2.Split(this.timeSeparators, StringSplitOptions.RemoveEmptyEntries);
								if (array2.IsNullOrEmpty<string>() || array2.Length != 2)
								{
									break;
								}
								int num = 0;
								int hour;
								if (!int.TryParse(array2.GetElement(num++) ?? string.Empty, out hour))
								{
									break;
								}
								int minute;
								if (!int.TryParse(array2.GetElement(num++) ?? string.Empty, out minute))
								{
									break;
								}
								list2.Add(new DateTime(1, 1, 1, hour, minute, (j != 0) ? 59 : 0));
							}
							if (list2.Count == 2)
							{
								list.AddRange(list2);
							}
							ListPool<DateTime>.Release(list2);
						}
					}
					SoundPlayer.MapBGMInfo.Period[] array3 = null;
					if (!list.IsNullOrEmpty<DateTime>() && 0 < list.Count && list.Count % 2 == 0)
					{
						array3 = new SoundPlayer.MapBGMInfo.Period[list.Count / 2];
						for (int k = 0; k < list.Count; k += 2)
						{
							array3[k / 2].Start = list[k];
							array3[k / 2].End = list[k + 1];
						}
					}
					ListPool<DateTime>.Release(list);
					return array3;
				}
				return null;
			}

			// Token: 0x060041F5 RID: 16885 RVA: 0x0019CC64 File Offset: 0x0019B064
			private void LoadDefaultFootStep(string _directory, int _sexID)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(_directory, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						AssetBundleInfo info = new AssetBundleInfo(string.Empty, text, fileNameWithoutExtension, string.Empty);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(info);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
											if (num2 == 0)
											{
												Dictionary<int, Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>>> chunkTable;
												if (!this.DefaultFootStepSETable.TryGetValue(_sexID, out chunkTable))
												{
													chunkTable = (this.DefaultFootStepSETable[_sexID] = new Dictionary<int, Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>>>());
												}
												this.LoadDefaultFootStepList(assetInfo, fileNameWithoutExtension, chunkTable);
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041F6 RID: 16886 RVA: 0x0019CDC0 File Offset: 0x0019B1C0
			private void LoadDefaultFootStepList(AssetBundleInfo _sheetInfo, string _ver, Dictionary<int, Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>>> _chunkTable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, false);
							Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>> areaTable;
							if (!_chunkTable.TryGetValue(key, out areaTable))
							{
								areaTable = (_chunkTable[key] = new Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>>());
							}
							this.LoadDefaultFootStepTable(assetInfo, _ver, areaTable);
						}
					}
				}
			}

			// Token: 0x060041F7 RID: 16887 RVA: 0x0019CE80 File Offset: 0x0019B280
			private void LoadDefaultFootStepTable(AssetBundleInfo _sheetInfo, string _ver, Dictionary<int, UnityEx.ValueTuple<int[], int[], int[]>> _areaTable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							num++;
							string text = list.GetElement(num++) ?? string.Empty;
							string[] array = text.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
							string text2 = list.GetElement(num++) ?? string.Empty;
							string[] array2 = text2.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
							string text3 = list.GetElement(num++) ?? string.Empty;
							string[] array3 = text3.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
							if (!array.IsNullOrEmpty<string>())
							{
								List<int> list2 = ListPool<int>.Get();
								List<int> list3 = ListPool<int>.Get();
								List<int> list4 = ListPool<int>.Get();
								if (!array.IsNullOrEmpty<string>())
								{
									foreach (string s2 in array)
									{
										int item;
										if (int.TryParse(s2, out item))
										{
											list2.Add(item);
										}
									}
								}
								if (!array2.IsNullOrEmpty<string>())
								{
									foreach (string s3 in array2)
									{
										int item2;
										if (int.TryParse(s3, out item2))
										{
											list3.Add(item2);
										}
									}
								}
								if (!array3.IsNullOrEmpty<string>())
								{
									foreach (string s4 in array3)
									{
										int item3;
										if (int.TryParse(s4, out item3))
										{
											list4.Add(item3);
										}
									}
								}
								if (!list2.IsNullOrEmpty<int>())
								{
									int[] array7 = new int[list2.Count];
									int[] array8 = new int[list3.Count];
									int[] array9 = new int[list4.Count];
									for (int m = 0; m < array7.Length; m++)
									{
										array7[m] = list2[m];
									}
									for (int n = 0; n < array8.Length; n++)
									{
										array8[n] = list3[n];
									}
									for (int num2 = 0; num2 < array9.Length; num2++)
									{
										array9[num2] = list4[num2];
									}
									_areaTable[key] = new UnityEx.ValueTuple<int[], int[], int[]>(array7, array8, array9);
								}
								ListPool<int>.Release(list2);
								ListPool<int>.Release(list3);
								ListPool<int>.Release(list4);
							}
						}
					}
				}
			}

			// Token: 0x060041F8 RID: 16888 RVA: 0x0019D158 File Offset: 0x0019B558
			private void LoadEnviroInfo(DefinePack _definePack)
			{
				string enviroSEInfoList = _definePack.ABDirectories.EnviroSEInfoList;
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(enviroSEInfoList, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							AssetBundleInfo info = new AssetBundleInfo(string.Empty, text, fileNameWithoutExtension, string.Empty);
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(info);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int num2;
										if (int.TryParse(s, out num2))
										{
											AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
											if (num2 != 0)
											{
												if (num2 != 1)
												{
													if (num2 == 2)
													{
														this.LoadReverbInfo(assetInfo, fileNameWithoutExtension);
													}
												}
												else
												{
													this.LoadEnviroAdjacentList(assetInfo, fileNameWithoutExtension);
												}
											}
											else
											{
												this.LoadEnviroPrefabList(assetInfo, fileNameWithoutExtension);
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x060041F9 RID: 16889 RVA: 0x0019D2C8 File Offset: 0x0019B6C8
			private void LoadEnviroPrefabList(AssetBundleInfo _sheetInfo, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int chunkID;
						if (int.TryParse(s, out chunkID))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, false);
							this.LoadEnviroPrefabInfoTable(assetInfo, _ver, chunkID);
						}
					}
				}
			}

			// Token: 0x060041FA RID: 16890 RVA: 0x0019D368 File Offset: 0x0019B768
			private void LoadEnviroPrefabInfoTable(AssetBundleInfo _sheetInfo, string _ver, int _chunkID)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
							if (!assetInfo.asset.IsNullOrEmpty() && !assetInfo.assetbundle.IsNullOrEmpty() && !assetInfo.manifest.IsNullOrEmpty())
							{
								Dictionary<int, AssetBundleInfo> dictionary;
								if (!this.EnviroSEPrefabInfoTable.TryGetValue(_chunkID, out dictionary))
								{
									Dictionary<int, AssetBundleInfo> dictionary2 = new Dictionary<int, AssetBundleInfo>();
									this.EnviroSEPrefabInfoTable[_chunkID] = dictionary2;
									dictionary = dictionary2;
								}
								dictionary[key] = assetInfo;
							}
						}
					}
				}
			}

			// Token: 0x060041FB RID: 16891 RVA: 0x0019D46C File Offset: 0x0019B86C
			private void LoadEnviroAdjacentList(AssetBundleInfo _sheetInfo, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int chunkID;
						if (int.TryParse(s, out chunkID))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, false);
							this.LoadEnviroAdjacentInfoTable(assetInfo, _ver, chunkID);
						}
					}
				}
			}

			// Token: 0x060041FC RID: 16892 RVA: 0x0019D50C File Offset: 0x0019B90C
			private void LoadEnviroAdjacentInfoTable(AssetBundleInfo _sheetInfo, string _ver, int _chunkID)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							num++;
							string text = list.GetElement(num++) ?? string.Empty;
							string[] array = text.Split(this.separators, StringSplitOptions.RemoveEmptyEntries);
							if (array.IsNullOrEmpty<string>())
							{
							}
							List<int> list2 = ListPool<int>.Get();
							if (!array.IsNullOrEmpty<string>())
							{
								foreach (string s2 in array)
								{
									int item;
									if (int.TryParse(s2, out item))
									{
										list2.Add(item);
									}
								}
							}
							int[] array3 = new int[list2.Count];
							for (int k = 0; k < array3.Length; k++)
							{
								array3[k] = list2[k];
							}
							Dictionary<int, int[]> dictionary;
							if (!this.EnviroSEAdjacentInfoTable.TryGetValue(_chunkID, out dictionary))
							{
								dictionary = (this.EnviroSEAdjacentInfoTable[_chunkID] = new Dictionary<int, int[]>());
							}
							dictionary[key] = array3;
							ListPool<int>.Release(list2);
						}
					}
				}
			}

			// Token: 0x060041FD RID: 16893 RVA: 0x0019D68C File Offset: 0x0019BA8C
			private void LoadReverbInfo(AssetBundleInfo _sheetInfo, string _ver)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int chunkID;
						if (int.TryParse(s, out chunkID))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, false);
							this.LoadReverbPrefabTable(assetInfo, _ver, chunkID);
						}
					}
				}
			}

			// Token: 0x060041FE RID: 16894 RVA: 0x0019D72C File Offset: 0x0019BB2C
			private void LoadReverbPrefabTable(AssetBundleInfo _sheetInfo, string _ver, int _chunkID)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
							if (!assetInfo.assetbundle.IsNullOrEmpty() && !assetInfo.asset.IsNullOrEmpty())
							{
								Dictionary<int, AssetBundleInfo> dictionary;
								if (!this.ReverbPrefabInfoTable.TryGetValue(_chunkID, out dictionary))
								{
									dictionary = (this.ReverbPrefabInfoTable[_chunkID] = new Dictionary<int, AssetBundleInfo>());
								}
								dictionary[key] = assetInfo;
							}
						}
					}
				}
			}

			// Token: 0x060041FF RID: 16895 RVA: 0x0019D81C File Offset: 0x0019BC1C
			private void LoadPersonalVoiceInfo(string _directory, Dictionary<int, Dictionary<int, AssetBundleInfo>> _setTable)
			{
				List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(_directory, false);
				if (assetBundleNameListFromPath.IsNullOrEmpty<string>())
				{
					return;
				}
				assetBundleNameListFromPath.Sort();
				for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
				{
					string text = assetBundleNameListFromPath[i];
					if (!Game.IsRestrictedOver50(text))
					{
						string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(text);
						if (AssetBundleCheck.IsFile(text, fileNameWithoutExtension))
						{
							AssetBundleInfo info = new AssetBundleInfo(string.Empty, text, fileNameWithoutExtension, string.Empty);
							ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(info);
							if (!(excelData == null))
							{
								for (int j = 1; j < excelData.MaxCell; j++)
								{
									List<string> list = excelData.list[j].list;
									if (!list.IsNullOrEmpty<string>())
									{
										int num = 0;
										string s = list.GetElement(num++) ?? string.Empty;
										int key;
										if (int.TryParse(s, out key))
										{
											AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
											Dictionary<int, AssetBundleInfo> setTable;
											if (!_setTable.TryGetValue(key, out setTable))
											{
												setTable = (_setTable[key] = new Dictionary<int, AssetBundleInfo>());
											}
											this.LoadPersonalVoiceInfo(assetInfo, setTable);
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06004200 RID: 16896 RVA: 0x0019D964 File Offset: 0x0019BD64
			private void LoadPersonalVoiceInfo(AssetBundleInfo _sheetInfo, Dictionary<int, AssetBundleInfo> _setTable)
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(_sheetInfo);
				if (excelData == null)
				{
					return;
				}
				for (int i = 1; i < excelData.MaxCell; i++)
				{
					List<string> list = excelData.list[i].list;
					if (!list.IsNullOrEmpty<string>())
					{
						int num = 0;
						string s = list.GetElement(num++) ?? string.Empty;
						int key;
						if (int.TryParse(s, out key))
						{
							AssetBundleInfo assetInfo = this.GetAssetInfo(list, ref num, true);
							if (!assetInfo.assetbundle.IsNullOrEmpty() && !assetInfo.asset.IsNullOrEmpty())
							{
								_setTable[key] = assetInfo;
							}
						}
					}
				}
			}

			// Token: 0x04003D61 RID: 15713
			private string[] separators = new string[]
			{
				"/",
				"／"
			};

			// Token: 0x04003D62 RID: 15714
			private string[] timeSeparators = new string[]
			{
				":",
				"："
			};

			// Token: 0x04003D63 RID: 15715
			private Regex regex = new Regex("<\\d{1,2}:[0-9]{1,2}\\/[0-9]{1,2}:[0-9]{1,2}>");
		}
	}
}
