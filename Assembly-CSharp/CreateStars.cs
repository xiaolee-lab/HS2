using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200066D RID: 1645
public class CreateStars : MonoBehaviour
{
	// Token: 0x060026BB RID: 9915 RVA: 0x000DFC3C File Offset: 0x000DE03C
	private void Start()
	{
		Vector3[] array = new Vector3[this.numberOfStars];
		for (int i = 0; i < this.numberOfStars; i++)
		{
			array[i] = UnityEngine.Random.onUnitSphere * 100f;
		}
		float[] array2 = new float[this.numberOfStars];
		for (int j = 0; j < this.numberOfStars; j++)
		{
			array2[j] = UnityEngine.Random.Range(1.5f, 2.5f);
		}
		Color32[] array3 = new Color32[this.numberOfStars];
		for (int k = 0; k < this.numberOfStars; k++)
		{
			float num = UnityEngine.Random.value * 0.75f + 0.25f;
			array3[k] = new Color(num, num, num);
		}
		this.stars = new VectorLine("Stars", new List<Vector3>(array), 1f, LineType.Points);
		this.stars.SetColors(new List<Color32>(array3));
		this.stars.SetWidths(new List<float>(array2));
		this.stars.Draw();
		VectorLine.SetCanvasCamera(Camera.main);
		VectorLine.canvas.planeDistance = Camera.main.farClipPlane - 1f;
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x000DFD83 File Offset: 0x000DE183
	private void LateUpdate()
	{
		this.stars.Draw();
	}

	// Token: 0x040026E8 RID: 9960
	public int numberOfStars = 2000;

	// Token: 0x040026E9 RID: 9961
	private VectorLine stars;
}
