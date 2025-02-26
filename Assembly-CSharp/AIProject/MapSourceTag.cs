using System;
using System.Collections.Generic;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C21 RID: 3105
	public class MapSourceTag : MonoBehaviour
	{
		// Token: 0x06005FCD RID: 24525 RVA: 0x00285EE4 File Offset: 0x002842E4
		private void Awake()
		{
			if (this._startPoint != null)
			{
				Singleton<Map>.Instance.PlayerStartPoint = this._startPoint;
			}
			AQUAS_Reflection[] componentsInChildren = base.GetComponentsInChildren<AQUAS_Reflection>(true);
			Singleton<Map>.Instance.WaterRefrections = componentsInChildren;
			MeshCollider[] componentsInChildren2 = base.GetComponentsInChildren<MeshCollider>(true);
			if (!componentsInChildren2.IsNullOrEmpty<MeshCollider>())
			{
				foreach (MeshCollider meshCollider in componentsInChildren2)
				{
					if (meshCollider.enabled)
					{
						meshCollider.enabled = false;
					}
				}
			}
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x00285F69 File Offset: 0x00284369
		private void Start()
		{
			Singleton<Manager.Resources>.Instance.LoadMapResourceStream.Subscribe(delegate(Unit _)
			{
				this.LoadMapGroups();
				this.LoadAreaOpenObjects();
				this.LoadTimeRelationObjects();
			});
		}

		// Token: 0x06005FCF RID: 24527 RVA: 0x00285F88 File Offset: 0x00284388
		private void LoadMapGroups()
		{
			Map instance = Singleton<Map>.Instance;
			instance.MapGroupObjList.Clear();
			Dictionary<int, string> dictionary;
			if (!Singleton<Manager.Resources>.Instance.Map.MapGroupNameList.TryGetValue(instance.MapID, out dictionary))
			{
				return;
			}
			foreach (KeyValuePair<int, string> keyValuePair in dictionary)
			{
				GameObject gameObject = base.transform.FindLoop(keyValuePair.Value);
				if (!(gameObject == null))
				{
					instance.MapGroupObjList[keyValuePair.Key] = gameObject;
				}
			}
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x00286048 File Offset: 0x00284448
		private void LoadAreaOpenObjects()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Dictionary<int, Dictionary<bool, ForcedHideObject[]>> areaOpenObjectTable = Singleton<Map>.Instance.AreaOpenObjectTable;
			Dictionary<int, Dictionary<bool, string[]>> areaOpenStateObjectNameTable = Singleton<Manager.Resources>.Instance.Map.AreaOpenStateObjectNameTable;
			areaOpenObjectTable.Clear();
			if (areaOpenStateObjectNameTable.IsNullOrEmpty<int, Dictionary<bool, string[]>>())
			{
				return;
			}
			foreach (KeyValuePair<int, Dictionary<bool, string[]>> keyValuePair in areaOpenStateObjectNameTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<bool, string[]>())
				{
					int key = keyValuePair.Key;
					foreach (KeyValuePair<bool, string[]> keyValuePair2 in keyValuePair.Value)
					{
						if (!keyValuePair2.Value.IsNullOrEmpty<string>())
						{
							bool key2 = keyValuePair2.Key;
							List<GameObject> list = ListPool<GameObject>.Get();
							foreach (string text in keyValuePair2.Value)
							{
								if (!text.IsNullOrEmpty())
								{
									GameObject gameObject = base.transform.FindLoop(text);
									if (!(gameObject == null))
									{
										list.Add(gameObject);
									}
								}
							}
							if (!list.IsNullOrEmpty<GameObject>())
							{
								List<ForcedHideObject> list2 = ListPool<ForcedHideObject>.Get();
								for (int j = 0; j < list.Count; j++)
								{
									GameObject gameObject2 = list[j];
									ForcedHideObject orAddComponent = gameObject2.GetOrAddComponent<ForcedHideObject>();
									if (!(orAddComponent == null) && !list2.Contains(orAddComponent))
									{
										orAddComponent.Init();
										list2.Add(orAddComponent);
									}
								}
								ForcedHideObject[] array = new ForcedHideObject[list2.Count];
								for (int k = 0; k < array.Length; k++)
								{
									array[k] = list2[k];
								}
								Dictionary<bool, ForcedHideObject[]> dictionary;
								if (!areaOpenObjectTable.TryGetValue(key, out dictionary) || dictionary == null)
								{
									dictionary = (areaOpenObjectTable[key] = new Dictionary<bool, ForcedHideObject[]>());
								}
								dictionary[key2] = array;
								ListPool<ForcedHideObject>.Release(list2);
							}
							ListPool<GameObject>.Release(list);
						}
					}
				}
			}
			Singleton<Map>.Instance.RefreshAreaOpenObject();
		}

		// Token: 0x06005FD1 RID: 24529 RVA: 0x002862D4 File Offset: 0x002846D4
		private void LoadTimeRelationObjects()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Map instance2 = Singleton<Map>.Instance;
			string name = "_EmissionColor";
			Dictionary<int, Dictionary<int, Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>>> timeRelationObjectTable = instance2.TimeRelationObjectTable;
			Dictionary<int, Dictionary<int, Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>>> timeRelationObjectStateTable = instance.Map.TimeRelationObjectStateTable;
			timeRelationObjectTable.Clear();
			if (timeRelationObjectStateTable.IsNullOrEmpty<int, Dictionary<int, Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>>>())
			{
				return;
			}
			foreach (KeyValuePair<int, Dictionary<int, Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>>> keyValuePair in timeRelationObjectStateTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<int, Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>>())
				{
					int key = keyValuePair.Key;
					foreach (KeyValuePair<int, Dictionary<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>> keyValuePair2 in keyValuePair.Value)
					{
						if (!keyValuePair2.Value.IsNullOrEmpty<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>>())
						{
							int key2 = keyValuePair2.Key;
							foreach (KeyValuePair<bool, Dictionary<int, List<UnityEx.ValueTuple<string, float>>>> keyValuePair3 in keyValuePair2.Value)
							{
								if (!keyValuePair3.Value.IsNullOrEmpty<int, List<UnityEx.ValueTuple<string, float>>>())
								{
									bool key3 = keyValuePair3.Key;
									foreach (KeyValuePair<int, List<UnityEx.ValueTuple<string, float>>> keyValuePair4 in keyValuePair3.Value)
									{
										if (!keyValuePair4.Value.IsNullOrEmpty<UnityEx.ValueTuple<string, float>>())
										{
											int key4 = keyValuePair4.Key;
											UnityEx.ValueTuple<GameObject, Material, float, Color>[] array = null;
											if (key2 != 0)
											{
												if (key2 == 1)
												{
													List<UnityEx.ValueTuple<GameObject, Material, float, Color>> list = ListPool<UnityEx.ValueTuple<GameObject, Material, float, Color>>.Get();
													foreach (UnityEx.ValueTuple<string, float> valueTuple in keyValuePair4.Value)
													{
														string item = valueTuple.Item1;
														if (!item.IsNullOrEmpty())
														{
															GameObject gameObject = base.transform.FindLoop(item);
															if (!(gameObject == null))
															{
																Renderer componentInChildren = gameObject.GetComponentInChildren<Renderer>(true);
																UnityEngine.Object x;
																if (componentInChildren == null)
																{
																	x = null;
																}
																else
																{
																	Material material = componentInChildren.material;
																	x = ((material != null) ? material.shader : null);
																}
																if (!(x == null))
																{
																	Color i = Color.white;
																	if (componentInChildren.material.HasProperty(name))
																	{
																		i = componentInChildren.material.GetColor(name);
																		if (1f < i.r)
																		{
																			i.r = Mathf.Repeat(i.r, 1f);
																		}
																		if (1f < i.g)
																		{
																			i.g = Mathf.Repeat(i.g, 1f);
																		}
																		if (1f < i.b)
																		{
																			i.b = Mathf.Repeat(i.b, 1f);
																		}
																		if (1f < i.a)
																		{
																			i.a = Mathf.Repeat(i.a, 1f);
																		}
																	}
																	list.Add(new UnityEx.ValueTuple<GameObject, Material, float, Color>(gameObject, componentInChildren.material, valueTuple.Item2, i));
																}
															}
														}
													}
													if (!list.IsNullOrEmpty<UnityEx.ValueTuple<GameObject, Material, float, Color>>())
													{
														array = new UnityEx.ValueTuple<GameObject, Material, float, Color>[list.Count];
														for (int j = 0; j < array.Length; j++)
														{
															array[j] = new UnityEx.ValueTuple<GameObject, Material, float, Color>(list[j].Item1, list[j].Item2, list[j].Item3, list[j].Item4);
														}
													}
													ListPool<UnityEx.ValueTuple<GameObject, Material, float, Color>>.Release(list);
												}
											}
											else
											{
												List<GameObject> list2 = ListPool<GameObject>.Get();
												foreach (UnityEx.ValueTuple<string, float> valueTuple2 in keyValuePair4.Value)
												{
													string item2 = valueTuple2.Item1;
													if (!item2.IsNullOrEmpty())
													{
														GameObject gameObject2 = base.transform.FindLoop(item2);
														if (!(gameObject2 == null))
														{
															list2.Add(gameObject2);
														}
													}
												}
												if (!list2.IsNullOrEmpty<GameObject>())
												{
													array = new UnityEx.ValueTuple<GameObject, Material, float, Color>[list2.Count];
													for (int k = 0; k < array.Length; k++)
													{
														array[k] = new UnityEx.ValueTuple<GameObject, Material, float, Color>(list2[k], null, 0f, Color.white);
													}
												}
												ListPool<GameObject>.Release(list2);
											}
											if (array == null)
											{
												array = new UnityEx.ValueTuple<GameObject, Material, float, Color>[0];
											}
											Dictionary<int, Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>> dictionary;
											if (!timeRelationObjectTable.TryGetValue(key, out dictionary))
											{
												dictionary = (timeRelationObjectTable[key] = new Dictionary<int, Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>>());
											}
											Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>> dictionary2;
											if (!dictionary.TryGetValue(key2, out dictionary2))
											{
												dictionary2 = (dictionary[key2] = new Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>());
											}
											Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]> dictionary3;
											if (!dictionary2.TryGetValue(key3, out dictionary3))
											{
												dictionary3 = (dictionary2[key3] = new Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>());
											}
											dictionary3[key4] = array;
										}
									}
								}
							}
						}
					}
				}
			}
			instance2.RefreshActiveTimeRelationObjects();
		}

		// Token: 0x0400554F RID: 21839
		[SerializeField]
		private Transform _startPoint;
	}
}
