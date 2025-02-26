using System;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
[ExecuteInEditMode]
public class Controls : MonoBehaviour
{
	// Token: 0x060016EB RID: 5867 RVA: 0x0008E5B4 File Offset: 0x0008C9B4
	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Space(5f);
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		this.occlusion.enabled = GUILayout.Toggle(this.occlusion.enabled, " Amplify Occlusion Enabled", Array.Empty<GUILayoutOption>());
		GUILayout.Space(5f);
		this.occlusion.ApplyMethod = ((!GUILayout.Toggle(this.occlusion.ApplyMethod == AmplifyOcclusionBase.ApplicationMethod.PostEffect, " Standard Post-effect", Array.Empty<GUILayoutOption>())) ? this.occlusion.ApplyMethod : AmplifyOcclusionBase.ApplicationMethod.PostEffect);
		this.occlusion.ApplyMethod = ((!GUILayout.Toggle(this.occlusion.ApplyMethod == AmplifyOcclusionBase.ApplicationMethod.Deferred, " Deferred Injection", Array.Empty<GUILayoutOption>())) ? this.occlusion.ApplyMethod : AmplifyOcclusionBase.ApplicationMethod.Deferred);
		this.occlusion.ApplyMethod = ((!GUILayout.Toggle(this.occlusion.ApplyMethod == AmplifyOcclusionBase.ApplicationMethod.Debug, " Debug Mode", Array.Empty<GUILayoutOption>())) ? this.occlusion.ApplyMethod : AmplifyOcclusionBase.ApplicationMethod.Debug);
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(5f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(-3f);
		GUILayout.Label("Intensity     ", Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		this.occlusion.Intensity = GUILayout.HorizontalSlider(this.occlusion.Intensity, 0f, 1f, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		});
		GUILayout.Space(5f);
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(-3f);
		GUILayout.Label(" " + this.occlusion.Intensity.ToString("0.00"), Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		GUILayout.Space(5f);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(-3f);
		GUILayout.Label("Power Exp. ", Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		this.occlusion.PowerExponent = GUILayout.HorizontalSlider(this.occlusion.PowerExponent, 0.0001f, 6f, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		});
		GUILayout.Space(5f);
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(-3f);
		GUILayout.Label(" " + this.occlusion.PowerExponent.ToString("0.00"), Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		GUILayout.Space(5f);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(-3f);
		GUILayout.Label("Radius        ", Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		this.occlusion.Radius = GUILayout.HorizontalSlider(this.occlusion.Radius, 0.1f, 10f, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		});
		GUILayout.Space(5f);
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(-3f);
		GUILayout.Label(" " + this.occlusion.Radius.ToString("0.00"), Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		GUILayout.Space(5f);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(-3f);
		GUILayout.Label("Quality        ", Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		this.occlusion.SampleCount = (AmplifyOcclusionBase.SampleCountLevel)GUILayout.HorizontalSlider((float)this.occlusion.SampleCount, 0f, 3f, new GUILayoutOption[]
		{
			GUILayout.Width(100f)
		});
		GUILayout.Space(5f);
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		GUILayout.Space(-3f);
		GUILayout.Label("        ", Array.Empty<GUILayoutOption>());
		GUILayout.EndVertical();
		GUILayout.Space(5f);
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x04001A2D RID: 6701
	public AmplifyOcclusionEffect occlusion;

	// Token: 0x04001A2E RID: 6702
	private const AmplifyOcclusionBase.ApplicationMethod POST = AmplifyOcclusionBase.ApplicationMethod.PostEffect;

	// Token: 0x04001A2F RID: 6703
	private const AmplifyOcclusionBase.ApplicationMethod DEFERRED = AmplifyOcclusionBase.ApplicationMethod.Deferred;

	// Token: 0x04001A30 RID: 6704
	private const AmplifyOcclusionBase.ApplicationMethod DEBUG = AmplifyOcclusionBase.ApplicationMethod.Debug;
}
