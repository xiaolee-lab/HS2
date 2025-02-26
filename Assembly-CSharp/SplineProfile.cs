using System;
using UnityEngine;

// Token: 0x020011C9 RID: 4553
[CreateAssetMenu(fileName = "SplineProfile", menuName = "SplineProfile", order = 1)]
public class SplineProfile : ScriptableObject
{
	// Token: 0x0400783D RID: 30781
	public Material splineMaterial;

	// Token: 0x0400783E RID: 30782
	public AnimationCurve meshCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 0f)
	});

	// Token: 0x0400783F RID: 30783
	public float minVal = 0.5f;

	// Token: 0x04007840 RID: 30784
	public float maxVal = 0.5f;

	// Token: 0x04007841 RID: 30785
	public int vertsInShape = 3;

	// Token: 0x04007842 RID: 30786
	public float traingleDensity = 0.2f;

	// Token: 0x04007843 RID: 30787
	public float uvScale = 3f;

	// Token: 0x04007844 RID: 30788
	public bool uvRotation = true;

	// Token: 0x04007845 RID: 30789
	public AnimationCurve flowFlat = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0.025f),
		new Keyframe(0.5f, 0.05f),
		new Keyframe(1f, 0.025f)
	});

	// Token: 0x04007846 RID: 30790
	public AnimationCurve flowWaterfall = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0.25f),
		new Keyframe(1f, 0.25f)
	});
}
