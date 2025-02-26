using System;
using UnityEngine;

// Token: 0x02000407 RID: 1031
public class Oscillator : MonoBehaviour
{
	// Token: 0x06001263 RID: 4707 RVA: 0x00072CEC File Offset: 0x000710EC
	private void Start()
	{
		this.m_StartPosition = base.transform.position;
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00072D00 File Offset: 0x00071100
	private void Update()
	{
		Vector3 position = this.m_StartPosition + this.m_Direction * this.m_Amplitude * Mathf.Sin(6.2831855f * Time.time / this.m_Period);
		base.transform.position = position;
	}

	// Token: 0x040014E4 RID: 5348
	public float m_Amplitude = 1f;

	// Token: 0x040014E5 RID: 5349
	public float m_Period = 1f;

	// Token: 0x040014E6 RID: 5350
	public Vector3 m_Direction = Vector3.up;

	// Token: 0x040014E7 RID: 5351
	private Vector3 m_StartPosition;
}
