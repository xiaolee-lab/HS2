using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200049E RID: 1182
public class utest : IDisposable
{
	// Token: 0x060015DB RID: 5595 RVA: 0x00086D47 File Offset: 0x00085147
	public void Dispose()
	{
	}

	// Token: 0x060015DC RID: 5596 RVA: 0x00086D4C File Offset: 0x0008514C
	public void OnLevelWasLoaded()
	{
		if (SceneManager.GetActiveScene().name == "Loading0")
		{
			return;
		}
		GameUtil.Log("on_level loaded.", Array.Empty<object>());
		GameInterface.Instance.Init();
	}

	// Token: 0x060015DD RID: 5597 RVA: 0x00086D90 File Offset: 0x00085190
	public void OnGUI()
	{
		using (new utest.FontSetter())
		{
			if (this.m_enable)
			{
				GUILayout.BeginArea(new Rect(200f, 50f, (float)(Screen.width - 400), (float)(Screen.height - 100)), "utest", GUI.skin.window);
				GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
				if (this.Gui_ShowCloseButton())
				{
					this.m_enable = false;
				}
				GUILayout.EndHorizontal();
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				this.Gui_ShowToggles();
				if (this.Gui_ChangeByPercentSlider(ref this.m_renderOrdinaryPercentage, GameInterface.Instance.DisabledRenderers.Count) || this.Gui_ChangeByPercentSlider(ref this.m_renderParticlePercentage, GameInterface.Instance.DisabledParticleSystems.Count))
				{
					GameInterface.Instance.FilterVisibleObjects(this.m_renderOrdinaryPercentage * 0.01f, this.m_renderParticlePercentage * 0.01f);
				}
				this.Gui_ShowLogs();
				GUILayout.EndVertical();
				GUILayout.EndArea();
			}
			else if (GUI.Button(new Rect(50f, (float)Screen.height * 0.5f - 40f, 80f, 80f), "utest"))
			{
				this.m_enable = !this.m_enable;
			}
		}
	}

	// Token: 0x060015DE RID: 5598 RVA: 0x00086F00 File Offset: 0x00085300
	private bool Gui_ShowCloseButton()
	{
		return GUILayout.Button("×", new GUILayoutOption[]
		{
			GUILayout.MinWidth(GUI.skin.label.CalcSize(new GUIContent("M")).x * 1.5f),
			GUILayout.ExpandWidth(false)
		});
	}

	// Token: 0x060015DF RID: 5599 RVA: 0x00086F58 File Offset: 0x00085358
	private void Gui_ShowToggles()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in GameInterface.Instance.KeyNodes)
		{
			if (!keyValuePair.Value.activeInHierarchy && keyValuePair.Value.activeSelf)
			{
				GUI.enabled = false;
			}
			bool flag = GUILayout.Toggle(keyValuePair.Value.activeSelf, keyValuePair.Key, Array.Empty<GUILayoutOption>());
			if (keyValuePair.Value.activeSelf != flag)
			{
				keyValuePair.Value.SetActive(flag);
			}
			GUI.enabled = true;
		}
	}

	// Token: 0x060015E0 RID: 5600 RVA: 0x0008701C File Offset: 0x0008541C
	private void Gui_ShowLogs()
	{
		GUILayout.Box("Log", Array.Empty<GUILayoutOption>());
		GameUtil._logPosition = GUILayout.BeginScrollView(GameUtil._logPosition, Array.Empty<GUILayoutOption>());
		GUILayout.Label(GameUtil._log, Array.Empty<GUILayoutOption>());
		GUILayout.EndScrollView();
	}

	// Token: 0x060015E1 RID: 5601 RVA: 0x00087058 File Offset: 0x00085458
	private bool Gui_ChangeByPercentSlider(ref float percentage, int disabledCount)
	{
		bool result = false;
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		float num = GUILayout.HorizontalSlider(percentage, 0f, 100f, Array.Empty<GUILayoutOption>());
		if (num != percentage)
		{
			percentage = num;
			result = true;
		}
		GUILayout.Label(percentage.ToString("0.00"), new GUILayoutOption[]
		{
			GUILayout.MinWidth(GUI.skin.label.CalcSize(new GUIContent("100.00")).x * 1.5f),
			GUILayout.ExpandWidth(false)
		});
		GUILayout.Label(disabledCount.ToString(), new GUILayoutOption[]
		{
			GUILayout.MinWidth(GUI.skin.label.CalcSize(new GUIContent("000")).x * 1.5f),
			GUILayout.ExpandWidth(false)
		});
		GUILayout.EndHorizontal();
		return result;
	}

	// Token: 0x040018C1 RID: 6337
	public bool m_enable;

	// Token: 0x040018C2 RID: 6338
	public float m_renderOrdinaryPercentage = 100f;

	// Token: 0x040018C3 RID: 6339
	public float m_renderParticlePercentage = 100f;

	// Token: 0x0200049F RID: 1183
	public class FontSetter : IDisposable
	{
		// Token: 0x060015E2 RID: 5602 RVA: 0x00087139 File Offset: 0x00085539
		public FontSetter()
		{
			GUI.skin.button.fontSize = GameUtil.FontSize;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0008716A File Offset: 0x0008556A
		public void Dispose()
		{
			GUI.skin.button.fontSize = this.m_oldSize;
		}

		// Token: 0x040018C4 RID: 6340
		private int m_oldSize = GUI.skin.button.fontSize;
	}
}
