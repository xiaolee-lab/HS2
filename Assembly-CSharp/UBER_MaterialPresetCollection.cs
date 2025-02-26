using System;
using UnityEngine;

// Token: 0x02000617 RID: 1559
public class UBER_MaterialPresetCollection : ScriptableObject
{
	// Token: 0x040024B6 RID: 9398
	[SerializeField]
	[HideInInspector]
	public string currentPresetName;

	// Token: 0x040024B7 RID: 9399
	[SerializeField]
	[HideInInspector]
	public UBER_PresetParamSection whatToRestore;

	// Token: 0x040024B8 RID: 9400
	[SerializeField]
	[HideInInspector]
	public UBER_MaterialPreset[] matPresets;

	// Token: 0x040024B9 RID: 9401
	[SerializeField]
	[HideInInspector]
	public string[] names;
}
