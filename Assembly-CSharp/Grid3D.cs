using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000667 RID: 1639
public class Grid3D : MonoBehaviour
{
	// Token: 0x060026A4 RID: 9892 RVA: 0x000DEDE4 File Offset: 0x000DD1E4
	private void Start()
	{
		this.numberOfLines = Mathf.Clamp(this.numberOfLines, 2, 8190);
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < this.numberOfLines; i++)
		{
			list.Add(new Vector3((float)i * this.distanceBetweenLines, 0f, 0f));
			list.Add(new Vector3((float)i * this.distanceBetweenLines, 0f, (float)(this.numberOfLines - 1) * this.distanceBetweenLines));
		}
		for (int j = 0; j < this.numberOfLines; j++)
		{
			list.Add(new Vector3(0f, 0f, (float)j * this.distanceBetweenLines));
			list.Add(new Vector3((float)(this.numberOfLines - 1) * this.distanceBetweenLines, 0f, (float)j * this.distanceBetweenLines));
		}
		VectorLine vectorLine = new VectorLine("Grid", list, this.lineWidth);
		vectorLine.Draw3DAuto();
		Vector3 position = base.transform.position;
		position.x = (float)(this.numberOfLines - 1) * this.distanceBetweenLines / 2f;
		base.transform.position = position;
	}

	// Token: 0x060026A5 RID: 9893 RVA: 0x000DEF1C File Offset: 0x000DD31C
	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			base.transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * this.rotateSpeed);
			base.transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * this.moveSpeed);
		}
		else
		{
			base.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * this.moveSpeed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * this.moveSpeed));
		}
	}

	// Token: 0x060026A6 RID: 9894 RVA: 0x000DEFF7 File Offset: 0x000DD3F7
	private void OnGUI()
	{
		GUILayout.Label(" Use arrow keys to move camera. Hold Shift + arrow up/down to move vertically. Hold Shift + arrow left/right to rotate.", Array.Empty<GUILayoutOption>());
	}

	// Token: 0x040026C0 RID: 9920
	public int numberOfLines = 20;

	// Token: 0x040026C1 RID: 9921
	public float distanceBetweenLines = 2f;

	// Token: 0x040026C2 RID: 9922
	public float moveSpeed = 8f;

	// Token: 0x040026C3 RID: 9923
	public float rotateSpeed = 70f;

	// Token: 0x040026C4 RID: 9924
	public float lineWidth = 2f;
}
