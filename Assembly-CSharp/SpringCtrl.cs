using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02001154 RID: 4436
public class SpringCtrl
{
	// Token: 0x060092B0 RID: 37552 RVA: 0x003CD2FA File Offset: 0x003CB6FA
	public SpringCtrl()
	{
		this.MemberInit();
	}

	// Token: 0x17001F6B RID: 8043
	// (set) Token: 0x060092B1 RID: 37553 RVA: 0x003CD31E File Offset: 0x003CB71E
	public float setGravity
	{
		set
		{
			this.m_fGravity = value;
		}
	}

	// Token: 0x17001F6C RID: 8044
	// (set) Token: 0x060092B2 RID: 37554 RVA: 0x003CD327 File Offset: 0x003CB727
	public float setDrag
	{
		set
		{
			this.m_fDrag = value;
		}
	}

	// Token: 0x17001F6D RID: 8045
	// (set) Token: 0x060092B3 RID: 37555 RVA: 0x003CD330 File Offset: 0x003CB730
	public float setMass
	{
		set
		{
			this.m_fMass = value;
			uint num = this.m_nPointNumW * this.m_nPointNumH;
			float fMass = 0f;
			if (num != 0U)
			{
				fMass = this.m_fMass / num;
			}
			foreach (SpringCtrl.CSpringPoint cspringPoint in this.m_listPoint)
			{
				cspringPoint.fMass = fMass;
				if (cspringPoint.fMass != 0f)
				{
					cspringPoint.fInvMass = 1f / cspringPoint.fMass;
				}
				else
				{
					cspringPoint.fInvMass = 0f;
				}
			}
		}
	}

	// Token: 0x17001F6E RID: 8046
	// (set) Token: 0x060092B4 RID: 37556 RVA: 0x003CD3EC File Offset: 0x003CB7EC
	public float setTension
	{
		set
		{
			this.m_fTension = value;
			foreach (SpringCtrl.CSpring cspring in this.m_listSpring)
			{
				if (!cspring.bShear)
				{
					cspring.fConstant = this.m_fTension;
				}
			}
		}
	}

	// Token: 0x17001F6F RID: 8047
	// (set) Token: 0x060092B5 RID: 37557 RVA: 0x003CD460 File Offset: 0x003CB860
	public float setShear
	{
		set
		{
			this.m_fShear = value;
			foreach (SpringCtrl.CSpring cspring in this.m_listSpring)
			{
				if (!cspring.bShear)
				{
					cspring.fConstant = this.m_fShear;
				}
			}
		}
	}

	// Token: 0x17001F70 RID: 8048
	// (set) Token: 0x060092B6 RID: 37558 RVA: 0x003CD4D4 File Offset: 0x003CB8D4
	public float setAttenuation
	{
		set
		{
			this.m_fAttenuation = value;
			foreach (SpringCtrl.CSpring cspring in this.m_listSpring)
			{
				cspring.fAttenuation = this.m_fAttenuation;
			}
		}
	}

	// Token: 0x17001F71 RID: 8049
	// (get) Token: 0x060092B7 RID: 37559 RVA: 0x003CD53C File Offset: 0x003CB93C
	public List<SpringCtrl.CSpringPoint> listPoint
	{
		get
		{
			return this.m_listPoint;
		}
	}

	// Token: 0x17001F72 RID: 8050
	// (set) Token: 0x060092B8 RID: 37560 RVA: 0x003CD544 File Offset: 0x003CB944
	public Vector3 setForce
	{
		set
		{
			this.m_vForce = value;
		}
	}

	// Token: 0x17001F73 RID: 8051
	// (set) Token: 0x060092B9 RID: 37561 RVA: 0x003CD54D File Offset: 0x003CB94D
	public Vector3 setAutoForce
	{
		set
		{
			this.m_vAutoForce = value;
		}
	}

	// Token: 0x060092BA RID: 37562 RVA: 0x003CD558 File Offset: 0x003CB958
	public bool Init(float _fMass, float _fTension, float _fShear, float _fAttenuation, float _fGravity, float _fDrag, uint _nPointNumW, uint _nPointNumH)
	{
		this.MemberInit();
		this.m_fGravity = _fGravity;
		this.m_fDrag = _fDrag;
		this.m_nPointNumW = _nPointNumW;
		this.m_nPointNumH = _nPointNumH;
		this.m_nNumPoint = this.m_nPointNumW * this.m_nPointNumH;
		if (this.m_nNumPoint == 0U)
		{
			this.MemberInit();
			return false;
		}
		int num = 0;
		while ((long)num < (long)((ulong)this.m_nNumPoint))
		{
			this.m_listPoint.Add(new SpringCtrl.CSpringPoint());
			num++;
		}
		uint[] array = new uint[]
		{
			this.m_nPointNumH - 1U,
			(this.m_nPointNumH - 1U) * (this.m_nPointNumW - 2U),
			this.m_nPointNumH - 1U,
			this.m_nPointNumW - 1U
		};
		this.m_nNumSpring = array[0] * 3U + array[1] * 4U + array[2] * 2U + array[3];
		if (this.m_nNumSpring == 0U)
		{
			this.MemberInit();
			return false;
		}
		int num2 = 0;
		while ((long)num2 < (long)((ulong)this.m_nNumSpring))
		{
			this.m_listSpring.Add(new SpringCtrl.CSpring());
			num2++;
		}
		return this.SetParam(_fMass, _fTension, _fShear, _fAttenuation);
	}

	// Token: 0x060092BB RID: 37563 RVA: 0x003CD678 File Offset: 0x003CBA78
	public bool UpdateEulerMethod(Transform _transfrom, float _ftime)
	{
		this.CalcForce(_transfrom);
		Vector3 b = Vector3.zero;
		foreach (SpringCtrl.CSpringPoint cspringPoint in this.m_listPoint)
		{
			cspringPoint.vAccel *= cspringPoint.fInvMass;
			b = cspringPoint.vAccel * _ftime;
			cspringPoint.vVelocity += b;
			b = cspringPoint.vVelocity * _ftime;
			cspringPoint.vPos += b;
		}
		return true;
	}

	// Token: 0x060092BC RID: 37564 RVA: 0x003CD734 File Offset: 0x003CBB34
	public bool ResetAllForce()
	{
		foreach (SpringCtrl.CSpringPoint cspringPoint in this.m_listPoint)
		{
			cspringPoint.vVelocity = Vector3.zero;
			cspringPoint.vAccel = Vector3.zero;
			cspringPoint.vForce = Vector3.zero;
		}
		return true;
	}

	// Token: 0x060092BD RID: 37565 RVA: 0x003CD7AC File Offset: 0x003CBBAC
	public void InitForce()
	{
		this.m_vAutoForce = Vector3.zero;
		foreach (SpringCtrl.CSpringPoint cspringPoint in this.m_listPoint)
		{
			cspringPoint.vVelocity = Vector3.zero;
			cspringPoint.vAccel = Vector3.zero;
			cspringPoint.vForce = Vector3.zero;
		}
	}

	// Token: 0x060092BE RID: 37566 RVA: 0x003CD830 File Offset: 0x003CBC30
	public bool SetParam(float _fMass, float _fTension, float _fShear, float _fAttenuation)
	{
		if (this.m_nPointNumW == 0U || this.m_nPointNumH == 0U)
		{
			return false;
		}
		this.m_fMass = _fMass;
		this.m_fTension = _fTension;
		this.m_fShear = _fShear;
		this.m_fAttenuation = _fAttenuation;
		int num = (int)(this.m_nPointNumW - 1U);
		int num2 = (int)(this.m_nPointNumH - 1U);
		float fMass = this.m_fMass / (this.m_nPointNumW * this.m_nPointNumH);
		int num3 = 0;
		while ((long)num3 < (long)((ulong)this.m_nPointNumH))
		{
			int num4 = 0;
			while ((long)num4 < (long)((ulong)this.m_nPointNumW))
			{
				int index = num3 * (int)this.m_nPointNumH + num4;
				this.m_listPoint[index].fMass = fMass;
				if (this.m_listPoint[index].fMass != 0f)
				{
					this.m_listPoint[index].fInvMass = 1f / this.m_listPoint[index].fMass;
				}
				else
				{
					this.m_listPoint[index].fInvMass = 0f;
				}
				this.m_listPoint[index].vVelocity = Vector3.zero;
				this.m_listPoint[index].vAccel = Vector3.zero;
				this.m_listPoint[index].vForce = Vector3.zero;
				this.m_listPoint[index].vPos = Vector3.zero;
				num4++;
			}
			num3++;
		}
		int[] array = new int[]
		{
			1,
			0,
			1,
			-1
		};
		int[] array2 = new int[]
		{
			0,
			1,
			1,
			1
		};
		byte[] array3 = new byte[4];
		int num5 = 0;
		SpringCtrl.POINTS points = new SpringCtrl.POINTS();
		SpringCtrl.POINTS points2 = new SpringCtrl.POINTS();
		num3 = 0;
		while ((long)num3 < (long)((ulong)this.m_nPointNumH))
		{
			int num4 = 0;
			while ((long)num4 < (long)((ulong)this.m_nPointNumW))
			{
				points.x = (short)num4;
				points.y = (short)num3;
				array3[0] = ((num4 >= num) ? 0 : 1);
				array3[1] = ((num3 >= num2) ? 0 : 1);
				array3[2] = ((num3 >= num2 || num4 >= num) ? 0 : 1);
				array3[3] = ((num4 <= 0 || num3 >= num2) ? 0 : 1);
				for (int i = 0; i < 4; i++)
				{
					if (array3[i] != 0)
					{
						points2.x = (short)(num4 + array[i]);
						points2.y = (short)(num3 + array2[i]);
						this.m_listSpring[num5].nIdx1 = (uint)((long)points.x + (long)points.y * (long)((ulong)this.m_nPointNumH));
						this.m_listSpring[num5].nIdx2 = (uint)((long)points2.x + (long)points2.y * (long)((ulong)this.m_nPointNumH));
						if (2 > i)
						{
							this.m_listSpring[num5].bShear = false;
							this.m_listSpring[num5].fConstant = this.m_fTension;
						}
						else
						{
							this.m_listSpring[num5].bShear = true;
							this.m_listSpring[num5].fConstant = this.m_fShear;
						}
						this.m_listSpring[num5].fAttenuation = this.m_fAttenuation;
						uint nIdx = this.m_listSpring[num5].nIdx1;
						uint nIdx2 = this.m_listSpring[num5].nIdx2;
						this.m_listSpring[num5].fLength = Vector3.Distance(this.m_listPoint[(int)nIdx].vPos, this.m_listPoint[(int)nIdx2].vPos);
						num5++;
					}
				}
				num4++;
			}
			num3++;
		}
		return true;
	}

	// Token: 0x060092BF RID: 37567 RVA: 0x003CDC0C File Offset: 0x003CC00C
	private bool CalcForce(Transform _trans)
	{
		foreach (SpringCtrl.CSpringPoint cspringPoint in this.m_listPoint)
		{
			cspringPoint.vForce = Vector3.zero;
		}
		foreach (SpringCtrl.CSpringPoint cspringPoint2 in this.m_listPoint)
		{
			if (!cspringPoint2.bLock)
			{
				Vector3 vector = new Vector3(0f, this.m_fGravity * cspringPoint2.fMass, 0f);
				vector = _trans.InverseTransformDirection(vector);
				cspringPoint2.vForce += vector;
				float magnitude = cspringPoint2.vVelocity.magnitude;
				Vector3 vector2 = cspringPoint2.vVelocity * -1f;
				vector2 = Vector3.Normalize(vector2);
				vector2 *= magnitude * this.m_fDrag;
				cspringPoint2.vForce += vector2;
			}
		}
		foreach (SpringCtrl.CSpring cspring in this.m_listSpring)
		{
			int nIdx = (int)cspring.nIdx1;
			int nIdx2 = (int)cspring.nIdx2;
			if (!this.m_listPoint[nIdx].bLock || !this.m_listPoint[nIdx2].bLock)
			{
				Vector3 rhs = this.m_listPoint[nIdx].vPos - this.m_listPoint[nIdx2].vPos;
				Vector3 lhs = this.m_listPoint[nIdx].vVelocity - this.m_listPoint[nIdx2].vVelocity;
				float num = Vector3.Dot(lhs, rhs);
				float magnitude = rhs.magnitude;
				float num2;
				if (magnitude != 0f)
				{
					num2 = 1f / magnitude;
				}
				else
				{
					num2 = 0f;
				}
				float fLength = cspring.fLength;
				float fConstant = cspring.fConstant;
				float fAttenuation = cspring.fAttenuation;
				float num3 = fConstant * (magnitude - fLength);
				float num4 = fAttenuation * num * num2;
				float d = -(num3 + num4);
				Vector3 normalized = rhs.normalized;
				Vector3 vector3 = normalized * d;
				vector3 += this.m_vForce;
				vector3 += this.m_vAutoForce;
				Vector3 b = vector3 * -1f;
				if (!this.m_listPoint[nIdx].bLock)
				{
					this.m_listPoint[nIdx].vForce = this.m_listPoint[nIdx].vForce + vector3;
				}
				if (!this.m_listPoint[nIdx2].bLock)
				{
					this.m_listPoint[nIdx2].vForce = this.m_listPoint[nIdx2].vForce + b;
				}
			}
		}
		return true;
	}

	// Token: 0x060092C0 RID: 37568 RVA: 0x003CDF88 File Offset: 0x003CC388
	public void MemberInit()
	{
		this.m_fGravity = 0f;
		this.m_fDrag = 0f;
		this.m_fMass = 0f;
		this.m_fTension = 0f;
		this.m_fShear = 0f;
		this.m_fAttenuation = 0f;
		this.m_nPointNumW = 0U;
		this.m_nPointNumH = 0U;
		this.m_nNumPoint = 0U;
		this.m_listPoint.Clear();
		this.m_nNumSpring = 0U;
		this.m_listSpring.Clear();
		this.m_vForce = Vector3.zero;
		this.m_vAutoForce = Vector3.zero;
	}

	// Token: 0x040076AC RID: 30380
	private float m_fGravity;

	// Token: 0x040076AD RID: 30381
	private float m_fDrag;

	// Token: 0x040076AE RID: 30382
	private float m_fMass;

	// Token: 0x040076AF RID: 30383
	private float m_fTension;

	// Token: 0x040076B0 RID: 30384
	private float m_fShear;

	// Token: 0x040076B1 RID: 30385
	private float m_fAttenuation;

	// Token: 0x040076B2 RID: 30386
	private uint m_nPointNumW;

	// Token: 0x040076B3 RID: 30387
	private uint m_nPointNumH;

	// Token: 0x040076B4 RID: 30388
	private uint m_nNumPoint;

	// Token: 0x040076B5 RID: 30389
	private List<SpringCtrl.CSpringPoint> m_listPoint = new List<SpringCtrl.CSpringPoint>();

	// Token: 0x040076B6 RID: 30390
	private uint m_nNumSpring;

	// Token: 0x040076B7 RID: 30391
	private List<SpringCtrl.CSpring> m_listSpring = new List<SpringCtrl.CSpring>();

	// Token: 0x040076B8 RID: 30392
	private Vector3 m_vForce;

	// Token: 0x040076B9 RID: 30393
	private Vector3 m_vAutoForce;

	// Token: 0x02001155 RID: 4437
	public class POINTS
	{
		// Token: 0x060092C1 RID: 37569 RVA: 0x003CE01F File Offset: 0x003CC41F
		public POINTS()
		{
			this.MemberInit();
		}

		// Token: 0x060092C2 RID: 37570 RVA: 0x003CE02D File Offset: 0x003CC42D
		public POINTS(short _x, short _y)
		{
			this.x = _x;
			this.y = _y;
		}

		// Token: 0x060092C3 RID: 37571 RVA: 0x003CE043 File Offset: 0x003CC443
		public void MemberInit()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x040076BA RID: 30394
		public short x;

		// Token: 0x040076BB RID: 30395
		public short y;
	}

	// Token: 0x02001156 RID: 4438
	public class CSpringPoint
	{
		// Token: 0x060092C5 RID: 37573 RVA: 0x003CE05C File Offset: 0x003CC45C
		public void MemberInit()
		{
			this.bLock = false;
			this.fMass = 0f;
			this.fInvMass = 0f;
			this.vPos = Vector3.zero;
			this.vVelocity = Vector3.zero;
			this.vAccel = Vector3.zero;
			this.vForce = Vector3.zero;
		}

		// Token: 0x040076BC RID: 30396
		public bool bLock;

		// Token: 0x040076BD RID: 30397
		public float fMass;

		// Token: 0x040076BE RID: 30398
		public float fInvMass;

		// Token: 0x040076BF RID: 30399
		public Vector3 vPos;

		// Token: 0x040076C0 RID: 30400
		public Vector3 vVelocity;

		// Token: 0x040076C1 RID: 30401
		public Vector3 vAccel;

		// Token: 0x040076C2 RID: 30402
		public Vector3 vForce;
	}

	// Token: 0x02001157 RID: 4439
	public class CSpring
	{
		// Token: 0x060092C7 RID: 37575 RVA: 0x003CE0BA File Offset: 0x003CC4BA
		public void MemberInit()
		{
			this.nIdx1 = 0U;
			this.nIdx2 = 0U;
			this.fLength = 0f;
			this.fConstant = 0f;
			this.fAttenuation = 0f;
			this.bShear = false;
		}

		// Token: 0x040076C3 RID: 30403
		public uint nIdx1;

		// Token: 0x040076C4 RID: 30404
		public uint nIdx2;

		// Token: 0x040076C5 RID: 30405
		public float fLength;

		// Token: 0x040076C6 RID: 30406
		public float fConstant;

		// Token: 0x040076C7 RID: 30407
		public float fAttenuation;

		// Token: 0x040076C8 RID: 30408
		public bool bShear;
	}
}
