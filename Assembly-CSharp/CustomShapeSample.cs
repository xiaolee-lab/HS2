using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020010CA RID: 4298
public class CustomShapeSample : MonoBehaviour
{
	// Token: 0x06008F2B RID: 36651 RVA: 0x003B7F1C File Offset: 0x003B631C
	private void Start()
	{
		if (this.cctrl != null)
		{
			this.cctrl.Initialize();
		}
		if (this.trfPanel)
		{
			for (int i = 0; i < ShapeSampleDefine.shapename.Length; i++)
			{
				Transform transform = this.trfPanel.transform.Find("Parts" + i.ToString("00"));
				if (!(null == this.trfPanel))
				{
					Transform transform2 = transform.Find("Slider");
					if (!(null == transform2))
					{
						this.sldCustom[i] = transform2.GetComponent<Slider>();
						if (this.cctrl != null && this.cctrl.CheckInitEnd())
						{
							this.sldCustom[i].value = this.cctrl.GetValue(i);
						}
					}
				}
			}
		}
		if (this.trfDemo)
		{
			this.anmDemo = this.trfDemo.GetComponent<Animator>();
			if (this.anmDemo)
			{
				this.anmDemo.Play(this.anmDemo.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0.5f);
			}
		}
	}

	// Token: 0x06008F2C RID: 36652 RVA: 0x003B805B File Offset: 0x003B645B
	public void OnWireFrameDraw(Toggle tgl)
	{
		if (this.wfr)
		{
			this.wfr.wireFrameDraw = tgl.isOn;
		}
	}

	// Token: 0x06008F2D RID: 36653 RVA: 0x003B8080 File Offset: 0x003B6480
	public void OnObjectPosition(Toggle tgl)
	{
		float[] array = new float[2];
		if (tgl.isOn)
		{
			array[0] = -0.2f;
			array[1] = 0.1f;
		}
		else
		{
			array[0] = 0f;
			array[1] = 0f;
		}
		if (this.trfSample)
		{
			this.trfSample.position = new Vector3(array[0], 0f, 0f);
		}
		if (this.trfDemo)
		{
			this.trfDemo.position = new Vector3(array[1], 0f, 0f);
		}
	}

	// Token: 0x06008F2E RID: 36654 RVA: 0x003B8120 File Offset: 0x003B6520
	public void OnPushButton(int id)
	{
		float num = 0f;
		if (id == 1)
		{
			num = 0.5f;
		}
		else if (id == 2)
		{
			num = 1f;
		}
		for (int i = 0; i < ShapeSampleDefine.shapename.Length; i++)
		{
			this.sldCustom[i].value = num;
		}
		if (this.anmDemo)
		{
			this.anmDemo.Play(this.anmDemo.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, num);
		}
	}

	// Token: 0x06008F2F RID: 36655 RVA: 0x003B81AC File Offset: 0x003B65AC
	private void Update()
	{
		if (this.cctrl != null && this.cctrl.CheckInitEnd())
		{
			for (int i = 0; i < ShapeSampleDefine.shapename.Length; i++)
			{
				if (null != this.sldCustom[i])
				{
					this.cctrl.SetValue(i, this.sldCustom[i].value);
				}
			}
			this.cctrl.Update();
		}
	}

	// Token: 0x040073A2 RID: 29602
	public CustomShapeSample.CustomCtrl cctrl;

	// Token: 0x040073A3 RID: 29603
	public Transform trfPanel;

	// Token: 0x040073A4 RID: 29604
	private Slider[] sldCustom = new Slider[ShapeSampleDefine.shapename.Length];

	// Token: 0x040073A5 RID: 29605
	public Transform trfSample;

	// Token: 0x040073A6 RID: 29606
	public Transform trfDemo;

	// Token: 0x040073A7 RID: 29607
	private Animator anmDemo;

	// Token: 0x040073A8 RID: 29608
	public WireFrameRender wfr;

	// Token: 0x020010CB RID: 4299
	[Serializable]
	public class CustomCtrl
	{
		// Token: 0x06008F31 RID: 36657 RVA: 0x003B822B File Offset: 0x003B662B
		public bool CheckInitEnd()
		{
			return this.InitEnd;
		}

		// Token: 0x06008F32 RID: 36658 RVA: 0x003B8233 File Offset: 0x003B6633
		public void Update()
		{
			if (this.sibSample != null)
			{
				this.sibSample.Update();
			}
		}

		// Token: 0x06008F33 RID: 36659 RVA: 0x003B824B File Offset: 0x003B664B
		public void SetValue(int no, float val)
		{
			this.value[no] = val;
			if (this.sibSample != null)
			{
				this.sibSample.ChangeValue(no, val);
			}
		}

		// Token: 0x06008F34 RID: 36660 RVA: 0x003B826F File Offset: 0x003B666F
		public float GetValue(int no)
		{
			return this.value[no];
		}

		// Token: 0x06008F35 RID: 36661 RVA: 0x003B827C File Offset: 0x003B667C
		public void Initialize()
		{
			this.sibSample = new ShapeInfoSample();
			int num = ShapeSampleDefine.shapename.Length;
			this.value = new float[num];
			if (this.sibSample != null && null != this.objSample)
			{
				this.sibSample.InitShapeInfo(string.Empty, "sample.unity3d", "sample.unity3d", "anmShapeSample", "customSample", this.objSample.transform);
				for (int i = 0; i < num; i++)
				{
					this.SetValue(i, 0.5f);
				}
				this.sibSample.Update();
			}
			this.InitEnd = true;
		}

		// Token: 0x040073A9 RID: 29609
		private bool InitEnd;

		// Token: 0x040073AA RID: 29610
		public GameObject objSample;

		// Token: 0x040073AB RID: 29611
		private ShapeInfoBase sibSample;

		// Token: 0x040073AC RID: 29612
		private float[] value;
	}
}
