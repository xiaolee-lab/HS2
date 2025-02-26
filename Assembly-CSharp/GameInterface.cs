using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200049D RID: 1181
public class GameInterface
{
	// Token: 0x060015D3 RID: 5587 RVA: 0x000868EC File Offset: 0x00084CEC
	public bool Init()
	{
		foreach (KeyValuePair<string, string> keyValuePair in GameInterface.ObjectNames)
		{
			GameObject gameObject = GameObject.Find(keyValuePair.Value);
			if (gameObject != null)
			{
				Log.Info("KeyNode {0} added as {1}", new object[]
				{
					keyValuePair.Value,
					keyValuePair.Key
				});
				this.KeyNodes[keyValuePair.Key] = gameObject;
			}
		}
		return true;
	}

	// Token: 0x060015D4 RID: 5588 RVA: 0x00086994 File Offset: 0x00084D94
	public void ToggleSwitch(string name, bool on)
	{
		if (this.KeyNodes.ContainsKey(name))
		{
			Log.Info("{0} toggled as {1}", new object[]
			{
				name,
				on
			});
			this.KeyNodes[name].SetActive(on);
		}
	}

	// Token: 0x060015D5 RID: 5589 RVA: 0x000869E4 File Offset: 0x00084DE4
	public void ChangePercentage(string name, double val)
	{
		if (GameInterface.VisiblePercentages.ContainsKey(name))
		{
			GameInterface.VisiblePercentages[name] = val;
			Log.Info("{0} slided to {1:0.00}", new object[]
			{
				name,
				val
			});
			this.FilterVisibleObjects((float)GameInterface.VisiblePercentages["Objects"], (float)GameInterface.VisiblePercentages["Effects"]);
		}
	}

	// Token: 0x060015D6 RID: 5590 RVA: 0x00086A50 File Offset: 0x00084E50
	private void DoFilter<T>(List<T> visible, List<T> disabled, float percentage) where T : Renderer
	{
		int num = (int)((float)(visible.Count + disabled.Count) * percentage);
		if (visible.Count > num)
		{
			for (int i = 0; i < visible.Count - num; i++)
			{
				T item = visible[i];
				item.enabled = false;
				disabled.Add(item);
				GameUtil.Log("{0} is hidden.", new object[]
				{
					item.gameObject.name
				});
			}
		}
		else if (num > visible.Count)
		{
			for (int j = 0; j < num - visible.Count; j++)
			{
				T item2 = disabled[j];
				item2.enabled = true;
				disabled.Remove(item2);
				GameUtil.Log("{0} is shown.", new object[]
				{
					item2.gameObject.name
				});
			}
		}
	}

	// Token: 0x060015D7 RID: 5591 RVA: 0x00086B45 File Offset: 0x00084F45
	private bool IsEnvironmentObject(GameObject go)
	{
		return go.transform.root.gameObject.name == GameInterface.EnvironmentRootName;
	}

	// Token: 0x060015D8 RID: 5592 RVA: 0x00086B68 File Offset: 0x00084F68
	public void FilterVisibleObjects(float percentage, float psysPercent)
	{
		List<Renderer> list = new List<Renderer>();
		List<ParticleSystemRenderer> list2 = new List<ParticleSystemRenderer>();
		foreach (Renderer renderer in UnityEngine.Object.FindObjectsOfType(typeof(Renderer)) as Renderer[])
		{
			if (renderer.isVisible && renderer.enabled && this.IsEnvironmentObject(renderer.gameObject))
			{
				if (renderer is ParticleSystemRenderer)
				{
					list2.Add((ParticleSystemRenderer)renderer);
				}
				else
				{
					list.Add(renderer);
				}
			}
		}
		this.DoFilter<Renderer>(list, this.DisabledRenderers, GameUtil.Clamp(percentage, 0f, 1f));
		this.DoFilter<ParticleSystemRenderer>(list2, this.DisabledParticleSystems, GameUtil.Clamp(psysPercent, 0f, 1f));
	}

	// Token: 0x040018BA RID: 6330
	public static GameInterface Instance = new GameInterface();

	// Token: 0x040018BB RID: 6331
	public static string EnvironmentRootName = "Environment";

	// Token: 0x040018BC RID: 6332
	public static Dictionary<string, string> ObjectNames = new Dictionary<string, string>
	{
		{
			"Scene_Objects",
			"Environment/Models"
		},
		{
			"Scene_Objects_(Lv1)",
			"Environment/Models/level_1"
		},
		{
			"Scene_Objects_(Lv2)",
			"Environment/Models/level_2"
		},
		{
			"Scene_Objects_(Lv3)",
			"Environment/Models/level_3"
		},
		{
			"Scene_Effects",
			"Environment/SceneEffect"
		},
		{
			"Scene_Terrain",
			"Environment/Terrain"
		},
		{
			"UI",
			"Main/UIMgr"
		},
		{
			"Players",
			"Main/_Players"
		},
		{
			"Effects",
			"Main/AnimationEffects"
		}
	};

	// Token: 0x040018BD RID: 6333
	public static Dictionary<string, double> VisiblePercentages = new Dictionary<string, double>
	{
		{
			"Objects",
			100.0
		},
		{
			"Effects",
			100.0
		}
	};

	// Token: 0x040018BE RID: 6334
	public Dictionary<string, GameObject> KeyNodes = new Dictionary<string, GameObject>();

	// Token: 0x040018BF RID: 6335
	public List<Renderer> DisabledRenderers = new List<Renderer>();

	// Token: 0x040018C0 RID: 6336
	public List<ParticleSystemRenderer> DisabledParticleSystems = new List<ParticleSystemRenderer>();
}
