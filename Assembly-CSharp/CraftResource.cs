using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using AIProject;
using SceneAssist;
using UnityEngine;
using UnityEx;

// Token: 0x02000EE0 RID: 3808
public class CraftResource : Singleton<CraftResource>
{
	// Token: 0x06007C79 RID: 31865 RVA: 0x003520CD File Offset: 0x003504CD
	protected override void Awake()
	{
		this.bEnd = false;
		this.Init();
	}

	// Token: 0x06007C7A RID: 31866 RVA: 0x003520DC File Offset: 0x003504DC
	public void Init()
	{
		this._AssetBandlePath = "craft/list/";
		base.StartCoroutine("LoadCraftItemList");
	}

	// Token: 0x06007C7B RID: 31867 RVA: 0x003520F5 File Offset: 0x003504F5
	public int[] GetCraftItemCategories()
	{
		return this._itemTables.Keys.ToArray<int>();
	}

	// Token: 0x06007C7C RID: 31868 RVA: 0x00352108 File Offset: 0x00350508
	public ReadOnlyDictionary<int, CraftItemInfo> GetItemTable(int id)
	{
		return this._itemTables[id];
	}

	// Token: 0x06007C7D RID: 31869 RVA: 0x00352124 File Offset: 0x00350524
	public CraftItemInfo GetItem(int category, int id)
	{
		ReadOnlyDictionary<int, ReadOnlyDictionary<int, CraftItemInfo>> itemTables = this._itemTables;
		if (itemTables == null)
		{
			return null;
		}
		CraftItemInfo result;
		if (!itemTables[category].TryGetValue(id, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06007C7E RID: 31870 RVA: 0x00352158 File Offset: 0x00350558
	private IEnumerator LoadCraftItemList()
	{
		List<string> pathList = CommonLib.GetAssetBundleNameListFromPath(this._AssetBandlePath, false);
		pathList.Sort();
		Dictionary<int, Dictionary<int, CraftItemInfo>> table = new Dictionary<int, Dictionary<int, CraftItemInfo>>();
		for (int i = 0; i < pathList.Count; i++)
		{
			string abName = pathList[i];
			string assetName = Path.GetFileNameWithoutExtension(abName);
			if (!SceneAssist.AssetBundleCheck.FindFile(abName, assetName, false))
			{
				yield return null;
			}
			else
			{
				ExcelData excelData = AssetUtility.LoadAsset<ExcelData>(abName, assetName, string.Empty);
				if (excelData == null)
				{
					yield return null;
				}
				else
				{
					List<UnityEx.ValueTuple<int, string, string, string>> listPaths = ListPool<UnityEx.ValueTuple<int, string, string, string>>.Get();
					int excelRowIdx = 0;
					while (excelRowIdx < excelData.MaxCell)
					{
						ExcelData excelData2 = excelData;
						int cell;
						excelRowIdx = (cell = excelRowIdx) + 1;
						List<string> row = excelData2.GetRow(cell);
						int num = 0;
						int i2;
						if (int.TryParse(row.GetElement(num++), out i2))
						{
							string element = row.GetElement(num++);
							string element2 = row.GetElement(num++);
							string element3 = row.GetElement(num++);
							listPaths.Add(new UnityEx.ValueTuple<int, string, string, string>(i2, element, element2, element3));
						}
					}
					foreach (UnityEx.ValueTuple<int, string, string, string> path in listPaths)
					{
						ExcelData infoExcel = AssetUtility.LoadAsset<ExcelData>(path.Item3, path.Item4, string.Empty);
						if (!(infoExcel == null))
						{
							excelRowIdx = 0;
							while (excelRowIdx < infoExcel.MaxCell)
							{
								ExcelData excelData3 = infoExcel;
								int cell;
								excelRowIdx = (cell = excelRowIdx) + 1;
								List<string> row2 = excelData3.GetRow(cell);
								int num2 = 0;
								int item = path.Item1;
								string item2 = path.Item2;
								int id;
								if (int.TryParse(row2.GetElement(num2++), out id))
								{
									int itemkind;
									if (int.TryParse(row2.GetElement(num2++), out itemkind))
									{
										int formkind;
										if (int.TryParse(row2.GetElement(num2++), out formkind))
										{
											num2++;
											string element4 = row2.GetElement(num2++);
											int horizontal;
											if (int.TryParse(row2.GetElement(num2++), out horizontal))
											{
												int vertical;
												if (int.TryParse(row2.GetElement(num2++), out vertical))
												{
													int height;
													if (int.TryParse(row2.GetElement(num2++), out height))
													{
														float moveVal;
														if (float.TryParse(row2.GetElement(num2++), out moveVal))
														{
															int[] array = new int[3];
															int[] array2 = new int[3];
															for (int j = 0; j < 3; j++)
															{
																if (!int.TryParse(row2.GetElement(num2++), out array[j]))
																{
																	array[j] = -1;
																}
																if (!int.TryParse(row2.GetElement(num2++), out array2[j]))
																{
																	array2[j] = -1;
																}
															}
															int putFlag;
															if (!int.TryParse(row2.GetElement(num2++), out putFlag))
															{
																putFlag = 0;
															}
															int[] array3 = new int[2];
															if (!int.TryParse(row2.GetElement(num2++), out array3[0]))
															{
																array3[0] = 0;
															}
															if (!int.TryParse(row2.GetElement(num2++), out array3[1]))
															{
																array3[1] = 0;
															}
															int cost;
															if (!int.TryParse(row2.GetElement(num2++), out cost))
															{
																cost = 0;
															}
															int element5;
															if (!int.TryParse(row2.GetElement(num2++), out element5))
															{
																element5 = 0;
															}
															string element6 = row2.GetElement(num2++);
															string element7 = row2.GetElement(num2++);
															Dictionary<int, CraftItemInfo> dictionary;
															if (!table.TryGetValue(item, out dictionary))
															{
																Dictionary<int, CraftItemInfo> dictionary2 = new Dictionary<int, CraftItemInfo>();
																table[item] = dictionary2;
																dictionary = dictionary2;
															}
															CraftItemInfo craftItemInfo = new CraftItemInfo();
															craftItemInfo.Id = id;
															craftItemInfo.Formkind = formkind;
															craftItemInfo.Itemkind = itemkind;
															craftItemInfo.Catkind = item + 1;
															craftItemInfo.Horizontal = horizontal;
															craftItemInfo.Vertical = vertical;
															craftItemInfo.Height = height;
															craftItemInfo.MoveVal = moveVal;
															craftItemInfo.recipe = new Tuple<int, int>[3];
															for (int k = 0; k < 3; k++)
															{
																craftItemInfo.recipe[k] = new Tuple<int, int>(array[k], array2[k]);
															}
															craftItemInfo.PutFlag = putFlag;
															craftItemInfo.JudgeCondition = new int[2];
															craftItemInfo.JudgeCondition[0] = array3[0];
															craftItemInfo.JudgeCondition[1] = array3[1];
															craftItemInfo.Cost = cost;
															craftItemInfo.Element = element5;
															craftItemInfo.Name = element4;
															craftItemInfo.CategoryName = item2;
															craftItemInfo.obj = AssetUtility.LoadAsset<GameObject>(element6, element7, string.Empty);
															dictionary[table[item].Count] = craftItemInfo;
														}
													}
												}
											}
										}
									}
								}
							}
							yield return null;
						}
					}
					ListPool<UnityEx.ValueTuple<int, string, string, string>>.Release(listPaths);
					yield return null;
				}
			}
		}
		this._itemTables = new ReadOnlyDictionary<int, ReadOnlyDictionary<int, CraftItemInfo>>(table.ToDictionary((KeyValuePair<int, Dictionary<int, CraftItemInfo>> x) => x.Key, (KeyValuePair<int, Dictionary<int, CraftItemInfo>> x) => new ReadOnlyDictionary<int, CraftItemInfo>(x.Value)));
		this.bEnd = true;
		yield return null;
		yield break;
	}

	// Token: 0x04006454 RID: 25684
	public bool bEnd;

	// Token: 0x04006455 RID: 25685
	private string _AssetBandlePath;

	// Token: 0x04006456 RID: 25686
	private ReadOnlyDictionary<int, ReadOnlyDictionary<int, CraftItemInfo>> _itemTables;
}
