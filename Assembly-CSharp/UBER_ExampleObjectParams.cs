using System;
using UnityEngine;

// Token: 0x0200060F RID: 1551
[Serializable]
public class UBER_ExampleObjectParams
{
	// Token: 0x04002430 RID: 9264
	public GameObject target;

	// Token: 0x04002431 RID: 9265
	public string materialProperty;

	// Token: 0x04002432 RID: 9266
	public MeshRenderer renderer;

	// Token: 0x04002433 RID: 9267
	public int submeshIndex;

	// Token: 0x04002434 RID: 9268
	public Vector2 SliderRange;

	// Token: 0x04002435 RID: 9269
	public string Title;

	// Token: 0x04002436 RID: 9270
	public string MatParamName;

	// Token: 0x04002437 RID: 9271
	[TextArea(2, 5)]
	public string Description;
}
