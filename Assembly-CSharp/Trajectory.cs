using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000FC5 RID: 4037
public class Trajectory : MonoBehaviour
{
	// Token: 0x0600862A RID: 34346 RVA: 0x00381D31 File Offset: 0x00380131
	private void Start()
	{
	}

	// Token: 0x0600862B RID: 34347 RVA: 0x00381D33 File Offset: 0x00380133
	public void Init(float fTime = 0f)
	{
		this.fAlpha = 1f;
		this.fTimer = 0f;
		if (Trajectory.fExistTime == 0f)
		{
			Trajectory.fExistTime = fTime;
		}
	}

	// Token: 0x0600862C RID: 34348 RVA: 0x00381D60 File Offset: 0x00380160
	private void Update()
	{
		if (this.image == null)
		{
			this.image = base.gameObject.GetComponentInChildren<Image>();
		}
		if (this.image == null)
		{
			return;
		}
		if (base.gameObject.activeSelf)
		{
			this.fTimer += Time.unscaledDeltaTime;
			this.fAlpha -= 1f / Trajectory.fExistTime * Time.unscaledDeltaTime;
			Color color = this.image.color;
			color.a = this.fAlpha;
			this.image.color = color;
			if (this.fTimer > Trajectory.fExistTime)
			{
				this.fTimer = 0f;
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600862D RID: 34349 RVA: 0x00381E2D File Offset: 0x0038022D
	public void Set(Vector3 vSetPos, Quaternion qRot)
	{
		base.transform.position = vSetPos;
		base.transform.rotation = qRot;
		this.fTimer = 0f;
		this.fAlpha = 1f;
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600862E RID: 34350 RVA: 0x00381E69 File Offset: 0x00380269
	public void Dead()
	{
		this.fTimer = 0f;
	}

	// Token: 0x04006D42 RID: 27970
	private float fTimer;

	// Token: 0x04006D43 RID: 27971
	private float fAlpha;

	// Token: 0x04006D44 RID: 27972
	private static float fExistTime;

	// Token: 0x04006D45 RID: 27973
	private Image image;
}
