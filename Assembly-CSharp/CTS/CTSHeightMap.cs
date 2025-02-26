using System;
using System.Text;
using UnityEngine;

namespace CTS
{
	// Token: 0x0200068F RID: 1679
	public class CTSHeightMap
	{
		// Token: 0x0600274C RID: 10060 RVA: 0x000E7A7B File Offset: 0x000E5E7B
		public CTSHeightMap()
		{
			this.Reset();
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000E7A98 File Offset: 0x000E5E98
		public CTSHeightMap(int width, int depth)
		{
			this.m_widthX = width;
			this.m_depthZ = depth;
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = (this.Math_IsPowerOf2(this.m_widthX) && this.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			this.m_isDirty = false;
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000E7B60 File Offset: 0x000E5F60
		public CTSHeightMap(float[,] source)
		{
			this.m_widthX = source.GetLength(0);
			this.m_depthZ = source.GetLength(1);
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = (this.Math_IsPowerOf2(this.m_widthX) && this.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			Buffer.BlockCopy(source, 0, this.m_heights, 0, this.m_widthX * this.m_depthZ * 4);
			this.m_isDirty = false;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000E7C50 File Offset: 0x000E6050
		public CTSHeightMap(float[,,] source, int slice)
		{
			this.m_widthX = source.GetLength(0);
			this.m_depthZ = source.GetLength(1);
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = (this.Math_IsPowerOf2(this.m_widthX) && this.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = source[i, j, slice];
				}
			}
			this.m_isDirty = false;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000E7D68 File Offset: 0x000E6168
		public CTSHeightMap(int[,] source)
		{
			this.m_widthX = source.GetLength(0);
			this.m_depthZ = source.GetLength(1);
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = (this.Math_IsPowerOf2(this.m_widthX) && this.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = (float)source[i, j];
				}
			}
			this.m_isDirty = false;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000E7E80 File Offset: 0x000E6280
		public CTSHeightMap(CTSHeightMap source)
		{
			this.Reset();
			this.m_widthX = source.m_widthX;
			this.m_depthZ = source.m_depthZ;
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_heights = new float[this.m_widthX, this.m_depthZ];
			this.m_isPowerOf2 = source.m_isPowerOf2;
			this.m_metaData = new byte[source.m_metaData.Length];
			for (int i = 0; i < source.m_metaData.Length; i++)
			{
				this.m_metaData[i] = source.m_metaData[i];
			}
			Buffer.BlockCopy(source.m_heights, 0, this.m_heights, 0, this.m_widthX * this.m_depthZ * 4);
			this.m_isDirty = false;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000E7F6B File Offset: 0x000E636B
		public int Width()
		{
			return this.m_widthX;
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000E7F73 File Offset: 0x000E6373
		public int Depth()
		{
			return this.m_depthZ;
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000E7F7B File Offset: 0x000E637B
		public float MinVal()
		{
			return this.m_statMinVal;
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000E7F83 File Offset: 0x000E6383
		public float MaxVal()
		{
			return this.m_statMaxVal;
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000E7F8B File Offset: 0x000E638B
		public double SumVal()
		{
			return this.m_statSumVals;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000E7F93 File Offset: 0x000E6393
		public int GetBufferSize()
		{
			return this.m_widthX * this.m_depthZ;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000E7FA2 File Offset: 0x000E63A2
		public byte[] GetMetaData()
		{
			return this.m_metaData;
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000E7FAA File Offset: 0x000E63AA
		public bool IsDirty()
		{
			return this.m_isDirty;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000E7FB2 File Offset: 0x000E63B2
		public void SetDirty(bool dirty = true)
		{
			this.m_isDirty = dirty;
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000E7FBB File Offset: 0x000E63BB
		public void ClearDirty()
		{
			this.m_isDirty = false;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000E7FC4 File Offset: 0x000E63C4
		public void SetMetaData(byte[] metadata)
		{
			this.m_metaData = new byte[metadata.Length];
			Buffer.BlockCopy(metadata, 0, this.m_metaData, 0, metadata.Length);
			this.m_isDirty = true;
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000E7FEC File Offset: 0x000E63EC
		public float[,] Heights()
		{
			return this.m_heights;
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000E7FF4 File Offset: 0x000E63F4
		public float[] Heights1D()
		{
			float[] array = new float[this.m_widthX * this.m_depthZ];
			Buffer.BlockCopy(this.m_heights, 0, array, 0, array.Length * 4);
			return array;
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000E8028 File Offset: 0x000E6428
		public void SetHeights(float[] heights)
		{
			int num = (int)Mathf.Sqrt((float)heights.Length);
			if (num != this.m_widthX || num != this.m_depthZ)
			{
				UnityEngine.Debug.LogError("SetHeights: Heights do not match. Aborting.");
				return;
			}
			Buffer.BlockCopy(heights, 0, this.m_heights, 0, heights.Length * 4);
			this.m_isDirty = true;
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000E8080 File Offset: 0x000E6480
		public void SetHeights(float[,] heights)
		{
			if (this.m_widthX != heights.GetLength(0) || this.m_depthZ != heights.GetLength(1))
			{
				UnityEngine.Debug.LogError("SetHeights: Sizes do not match. Aborting.");
				return;
			}
			int num = heights.GetLength(0) * heights.GetLength(1);
			Buffer.BlockCopy(heights, 0, this.m_heights, 0, num * 4);
			this.m_isDirty = true;
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000E80E4 File Offset: 0x000E64E4
		public float GetSafeHeight(int x, int z)
		{
			if (x < 0)
			{
				x = 0;
			}
			if (z < 0)
			{
				z = 0;
			}
			if (x >= this.m_widthX)
			{
				x = this.m_widthX - 1;
			}
			if (z >= this.m_depthZ)
			{
				z = this.m_depthZ - 1;
			}
			return this.m_heights[x, z];
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000E8140 File Offset: 0x000E6540
		public void SetSafeHeight(int x, int z, float height)
		{
			if (x < 0)
			{
				x = 0;
			}
			if (z < 0)
			{
				z = 0;
			}
			if (x >= this.m_widthX)
			{
				x = this.m_widthX - 1;
			}
			if (z >= this.m_depthZ)
			{
				z = this.m_depthZ - 1;
			}
			this.m_heights[x, z] = height;
			this.m_isDirty = true;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000E81A4 File Offset: 0x000E65A4
		protected float GetInterpolatedHeight(float x, float z)
		{
			x *= (float)this.m_widthX - 1f;
			z *= (float)this.m_depthZ - 1f;
			int num = (int)x;
			int num2 = (int)z;
			int num3 = num + 1;
			int num4 = num2 + 1;
			if (num3 >= this.m_widthX)
			{
				num3 = num;
			}
			if (num4 >= this.m_depthZ)
			{
				num4 = num2;
			}
			float num5 = x - (float)num;
			float num6 = z - (float)num2;
			float num7 = 1f - num5;
			float num8 = 1f - num6;
			return num7 * num8 * this.m_heights[num, num2] + num7 * num6 * this.m_heights[num, num4] + num5 * num8 * this.m_heights[num3, num2] + num5 * num6 * this.m_heights[num3, num4];
		}

		// Token: 0x1700059F RID: 1439
		public float this[int x, int z]
		{
			get
			{
				return this.m_heights[x, z];
			}
			set
			{
				this.m_heights[x, z] = value;
				this.m_isDirty = true;
			}
		}

		// Token: 0x170005A0 RID: 1440
		public float this[float x, float z]
		{
			get
			{
				return this.GetInterpolatedHeight(x, z);
			}
			set
			{
				x *= (float)this.m_widthX - 1f;
				z *= (float)this.m_depthZ - 1f;
				this.m_heights[(int)x, (int)z] = value;
				this.m_isDirty = true;
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000E82D8 File Offset: 0x000E66D8
		public CTSHeightMap SetHeight(float height)
		{
			float num = this.Math_Clamp(0f, 1f, height);
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000E833C File Offset: 0x000E673C
		public void GetHeightRange(ref float minHeight, ref float maxHeight)
		{
			maxHeight = float.MinValue;
			minHeight = float.MaxValue;
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j];
					if (num > maxHeight)
					{
						maxHeight = num;
					}
					if (num < minHeight)
					{
						minHeight = num;
					}
				}
			}
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000E83AC File Offset: 0x000E67AC
		public float GetSlope(int x, int z)
		{
			float num = this.m_heights[x, z];
			float num2 = this.m_heights[x + 1, z] - num;
			float num3 = this.m_heights[x, z + 1] - num;
			return (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000E83FC File Offset: 0x000E67FC
		public float GetSlope(float x, float z)
		{
			float num = this.GetInterpolatedHeight(x + this.m_widthInvX * 0.9f, z) - this.GetInterpolatedHeight(x - this.m_widthInvX * 0.9f, z);
			float num2 = this.GetInterpolatedHeight(x, z + this.m_depthInvZ * 0.9f) - this.GetInterpolatedHeight(x, z - this.m_depthInvZ * 0.9f);
			return this.Math_Clamp(0f, 90f, (float)(Math.Sqrt((double)(num * num + num2 * num2)) * 10000.0));
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000E848C File Offset: 0x000E688C
		public float GetSlope_a(float x, float z)
		{
			float interpolatedHeight = this.GetInterpolatedHeight(x, z);
			float num = Math.Abs(this.GetInterpolatedHeight(x - this.m_widthInvX, z) - interpolatedHeight);
			float num2 = Math.Abs(this.GetInterpolatedHeight(x + this.m_widthInvX, z) - interpolatedHeight);
			float num3 = Math.Abs(this.GetInterpolatedHeight(x, z - this.m_depthInvZ) - interpolatedHeight);
			float num4 = Math.Abs(this.GetInterpolatedHeight(x, z + this.m_depthInvZ) - interpolatedHeight);
			return (num + num2 + num3 + num4) / 4f * 400f;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000E8514 File Offset: 0x000E6914
		public float GetBaseLevel()
		{
			float num = 0f;
			for (int i = 0; i < this.m_widthX; i++)
			{
				if (this.m_heights[i, 0] > num)
				{
					num = this.m_heights[i, 0];
				}
				if (this.m_heights[i, this.m_depthZ - 1] > num)
				{
					num = this.m_heights[i, this.m_depthZ - 1];
				}
			}
			for (int j = 0; j < this.m_depthZ; j++)
			{
				if (this.m_heights[0, j] > num)
				{
					num = this.m_heights[0, j];
				}
				if (this.m_heights[this.m_widthX - 1, j] > num)
				{
					num = this.m_heights[this.m_widthX - 1, j];
				}
			}
			return num;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000E85F8 File Offset: 0x000E69F8
		public bool HasData()
		{
			return this.m_widthX > 0 && this.m_depthZ > 0 && this.m_heights != null && this.m_heights.GetLength(0) == this.m_widthX && this.m_heights.GetLength(1) == this.m_depthZ;
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000E8660 File Offset: 0x000E6A60
		public float[] GetRow(int rowX)
		{
			float[] array = new float[this.m_depthZ];
			for (int i = 0; i < this.m_depthZ; i++)
			{
				array[i] = this.m_heights[rowX, i];
			}
			return array;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000E86A4 File Offset: 0x000E6AA4
		public void SetRow(int rowX, float[] values)
		{
			for (int i = 0; i < this.m_depthZ; i++)
			{
				this.m_heights[rowX, i] = values[i];
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000E86D8 File Offset: 0x000E6AD8
		public float[] GetColumn(int columnZ)
		{
			float[] array = new float[this.m_widthX];
			for (int i = 0; i < this.m_widthX; i++)
			{
				array[i] = this.m_heights[i, columnZ];
			}
			return array;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000E871C File Offset: 0x000E6B1C
		public void SetColumn(int columnZ, float[] values)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				this.m_heights[i, columnZ] = values[i];
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000E8750 File Offset: 0x000E6B50
		public void Reset()
		{
			this.m_widthX = (this.m_depthZ = 0);
			this.m_widthInvX = (this.m_depthInvZ = 0f);
			this.m_heights = null;
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_metaData = new byte[0];
			this.m_heights = new float[0, 0];
			this.m_isDirty = false;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000E87CC File Offset: 0x000E6BCC
		public void UpdateStats()
		{
			this.m_statMinVal = 1f;
			this.m_statMaxVal = 0f;
			this.m_statSumVals = 0.0;
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j];
					if (num < this.m_statMinVal)
					{
						this.m_statMinVal = num;
					}
					if (num > this.m_statMaxVal)
					{
						this.m_statMaxVal = num;
					}
					this.m_statSumVals += (double)num;
				}
			}
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000E8878 File Offset: 0x000E6C78
		public CTSHeightMap Smooth(int iterations)
		{
			for (int i = 0; i < iterations; i++)
			{
				for (int j = 0; j < this.m_widthX; j++)
				{
					for (int k = 0; k < this.m_depthZ; k++)
					{
						this.m_heights[j, k] = this.Math_Clamp(0f, 1f, (this.GetSafeHeight(j - 1, k) + this.GetSafeHeight(j + 1, k) + this.GetSafeHeight(j, k - 1) + this.GetSafeHeight(j, k + 1)) / 4f);
					}
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000E891C File Offset: 0x000E6D1C
		public CTSHeightMap SmoothRadius(int radius)
		{
			radius = Mathf.Max(5, radius);
			CTSHeightMap ctsheightMap = new CTSHeightMap(this.m_widthX, this.m_depthZ);
			float num = 1f / (float)((2 * radius + 1) * (2 * radius + 1));
			for (int i = 0; i < this.m_depthZ; i++)
			{
				for (int j = 0; j < this.m_widthX; j++)
				{
					ctsheightMap[j, i] = num * this.m_heights[j, i];
				}
			}
			for (int k = radius; k < this.m_widthX - radius; k++)
			{
				int l = radius;
				float num2 = 0f;
				for (int m = -radius; m < radius + 1; m++)
				{
					for (int n = -radius; n < radius + 1; n++)
					{
						num2 += ctsheightMap[k + n, l + m];
					}
				}
				for (l++; l < this.m_depthZ - radius; l++)
				{
					for (int num3 = -radius; num3 < radius + 1; num3++)
					{
						num2 -= ctsheightMap[k + num3, l - radius - 1];
						num2 += ctsheightMap[k + num3, l + radius];
					}
					this.m_heights[k, l] = num2;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000E8A88 File Offset: 0x000E6E88
		public CTSHeightMap GetSlopeMap()
		{
			CTSHeightMap ctsheightMap = new CTSHeightMap(this);
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					ctsheightMap[i, j] = this.GetSlope(i, j);
				}
			}
			return ctsheightMap;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000E8ADC File Offset: 0x000E6EDC
		public CTSHeightMap Copy(CTSHeightMap CTSHeightMap)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not copy different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = CTSHeightMap.m_heights[i, j];
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000E8B68 File Offset: 0x000E6F68
		public CTSHeightMap CopyClamped(CTSHeightMap CTSHeightMap, float min, float max)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not copy different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = CTSHeightMap.m_heights[i, j];
					if (num < min)
					{
						num = min;
					}
					else if (num > max)
					{
						num = max;
					}
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000E8C0C File Offset: 0x000E700C
		public CTSHeightMap Duplicate()
		{
			return new CTSHeightMap(this);
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000E8C14 File Offset: 0x000E7014
		public CTSHeightMap Invert()
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = 1f - this.m_heights[i, j];
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000E8C78 File Offset: 0x000E7078
		public CTSHeightMap Flip()
		{
			float[,] array = new float[this.m_depthZ, this.m_widthX];
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					array[j, i] = this.m_heights[i, j];
				}
			}
			this.m_heights = array;
			this.m_widthX = array.GetLength(0);
			this.m_depthZ = array.GetLength(1);
			this.m_widthInvX = 1f / (float)this.m_widthX;
			this.m_depthInvZ = 1f / (float)this.m_depthZ;
			this.m_isPowerOf2 = (this.Math_IsPowerOf2(this.m_widthX) && this.Math_IsPowerOf2(this.m_depthZ));
			this.m_statMinVal = (this.m_statMaxVal = 0f);
			this.m_statSumVals = 0.0;
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000E8D74 File Offset: 0x000E7174
		public CTSHeightMap Normalise()
		{
			float num = float.MinValue;
			float num2 = float.MaxValue;
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num3 = this.m_heights[i, j];
					if (num3 > num)
					{
						num = num3;
					}
					if (num3 < num2)
					{
						num2 = num3;
					}
				}
			}
			float num4 = num - num2;
			if (num4 > 0f)
			{
				for (int k = 0; k < this.m_widthX; k++)
				{
					for (int l = 0; l < this.m_depthZ; l++)
					{
						this.m_heights[k, l] = (this.m_heights[k, l] - num2) / num4;
					}
				}
				this.m_isDirty = true;
			}
			return this;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000E8E54 File Offset: 0x000E7254
		public CTSHeightMap Add(float value)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] += value;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000E8EAC File Offset: 0x000E72AC
		public CTSHeightMap Add(CTSHeightMap CTSHeightMap)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not add different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] += CTSHeightMap.m_heights[i, j];
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000E8F3C File Offset: 0x000E733C
		public CTSHeightMap AddClamped(float value, float min, float max)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j] + value;
					if (num < min)
					{
						num = min;
					}
					else if (num > max)
					{
						num = max;
					}
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000E8FB4 File Offset: 0x000E73B4
		public CTSHeightMap AddClamped(CTSHeightMap CTSHeightMap, float min, float max)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not add different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j] + CTSHeightMap.m_heights[i, j];
					if (num < min)
					{
						num = min;
					}
					else if (num > max)
					{
						num = max;
					}
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000E9068 File Offset: 0x000E7468
		public CTSHeightMap Subtract(float value)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] -= value;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000E90C0 File Offset: 0x000E74C0
		public CTSHeightMap Subtract(CTSHeightMap CTSHeightMap)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not subtract different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] -= CTSHeightMap.m_heights[i, j];
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000E9150 File Offset: 0x000E7550
		public CTSHeightMap SubtractClamped(float value, float min, float max)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j] - value;
					if (num < min)
					{
						num = min;
					}
					else if (num > max)
					{
						num = max;
					}
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000E91C8 File Offset: 0x000E75C8
		public CTSHeightMap SubtractClamped(CTSHeightMap CTSHeightMap, float min, float max)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not add different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j] - CTSHeightMap.m_heights[i, j];
					if (num < min)
					{
						num = min;
					}
					else if (num > max)
					{
						num = max;
					}
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000E927C File Offset: 0x000E767C
		public CTSHeightMap Multiply(float value)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] *= value;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000E92D4 File Offset: 0x000E76D4
		public CTSHeightMap Multiply(CTSHeightMap CTSHeightMap)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not multiply different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] *= CTSHeightMap.m_heights[i, j];
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000E9364 File Offset: 0x000E7764
		public CTSHeightMap MultiplyClamped(float value, float min, float max)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j] * value;
					if (num < min)
					{
						num = min;
					}
					else if (num > max)
					{
						num = max;
					}
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000E93DC File Offset: 0x000E77DC
		public CTSHeightMap MultiplyClamped(CTSHeightMap CTSHeightMap, float min, float max)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not multiply different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j] * CTSHeightMap.m_heights[i, j];
					if (num < min)
					{
						num = min;
					}
					else if (num > max)
					{
						num = max;
					}
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000E9490 File Offset: 0x000E7890
		public CTSHeightMap Divide(float value)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] /= value;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000E94E8 File Offset: 0x000E78E8
		public CTSHeightMap Divide(CTSHeightMap CTSHeightMap)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not divide different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] /= CTSHeightMap.m_heights[i, j];
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000E9578 File Offset: 0x000E7978
		public CTSHeightMap DivideClamped(float value, float min, float max)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j] / value;
					if (num < min)
					{
						num = min;
					}
					else if (num > max)
					{
						num = max;
					}
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000E95F0 File Offset: 0x000E79F0
		public CTSHeightMap DivideClamped(CTSHeightMap CTSHeightMap, float min, float max)
		{
			if (this.m_widthX != CTSHeightMap.m_widthX || this.m_depthZ != CTSHeightMap.m_depthZ)
			{
				UnityEngine.Debug.LogError("Can not divide different sized CTSHeightMap");
				return this;
			}
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					float num = this.m_heights[i, j] / CTSHeightMap.m_heights[i, j];
					if (num < min)
					{
						num = min;
					}
					else if (num > max)
					{
						num = max;
					}
					this.m_heights[i, j] = num;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000E96A4 File Offset: 0x000E7AA4
		public float Sum()
		{
			float num = 0f;
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					num += this.m_heights[i, j];
				}
			}
			return num;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000E96F6 File Offset: 0x000E7AF6
		public float Average()
		{
			return this.Sum() / (float)(this.m_widthX * this.m_depthZ);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000E9710 File Offset: 0x000E7B10
		public CTSHeightMap Power(float exponent)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = Mathf.Pow(this.m_heights[i, j], exponent);
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000E9774 File Offset: 0x000E7B74
		public CTSHeightMap Contrast(float contrast)
		{
			for (int i = 0; i < this.m_widthX; i++)
			{
				for (int j = 0; j < this.m_depthZ; j++)
				{
					this.m_heights[i, j] = (this.m_heights[i, j] - 0.5f) * contrast + 0.5f;
				}
			}
			this.m_isDirty = true;
			return this;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000E97DF File Offset: 0x000E7BDF
		private bool Math_IsPowerOf2(int value)
		{
			return (value & value - 1) == 0;
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000E97E9 File Offset: 0x000E7BE9
		private float Math_Clamp(float min, float max, float value)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000E9800 File Offset: 0x000E7C00
		public void DumpMap(float scaleValue, int precision, string spacer, bool flip)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			if (precision == 0)
			{
				text = "{0:0}";
			}
			else
			{
				text = "{0:0.";
				for (int i = 0; i < precision; i++)
				{
					text += "0";
				}
				text += "}";
			}
			if (!string.IsNullOrEmpty(spacer))
			{
				text += spacer;
			}
			for (int j = 0; j < this.m_widthX; j++)
			{
				for (int k = 0; k < this.m_depthZ; k++)
				{
					if (!flip)
					{
						stringBuilder.AppendFormat(text, this.m_heights[j, k] * scaleValue);
					}
					else
					{
						stringBuilder.AppendFormat(text, this.m_heights[k, j] * scaleValue);
					}
				}
				stringBuilder.AppendLine();
			}
			UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000E98F8 File Offset: 0x000E7CF8
		public void DumpRow(int rowX, float scaleValue, int precision, string spacer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			if (precision == 0)
			{
				text = "{0:0}";
			}
			else
			{
				text = "{0:0.";
				for (int i = 0; i < precision; i++)
				{
					text += "0";
				}
				text += "}";
			}
			if (!string.IsNullOrEmpty(spacer))
			{
				text += spacer;
			}
			float[] row = this.GetRow(rowX);
			for (int j = 0; j < row.Length; j++)
			{
				stringBuilder.AppendFormat(text, row[j] * scaleValue);
			}
			UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000E99A8 File Offset: 0x000E7DA8
		public void DumpColumn(int columnZ, float scaleValue, int precision, string spacer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			if (precision == 0)
			{
				text = "{0:0}";
			}
			else
			{
				text = "{0:0.";
				for (int i = 0; i < precision; i++)
				{
					text += "0";
				}
				text += "}";
			}
			if (!string.IsNullOrEmpty(spacer))
			{
				text += spacer;
			}
			float[] column = this.GetColumn(columnZ);
			for (int j = 0; j < column.Length; j++)
			{
				stringBuilder.AppendFormat(text, column[j] * scaleValue);
			}
			UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x040027CC RID: 10188
		protected int m_widthX;

		// Token: 0x040027CD RID: 10189
		protected int m_depthZ;

		// Token: 0x040027CE RID: 10190
		protected float[,] m_heights;

		// Token: 0x040027CF RID: 10191
		protected bool m_isPowerOf2;

		// Token: 0x040027D0 RID: 10192
		protected float m_widthInvX;

		// Token: 0x040027D1 RID: 10193
		protected float m_depthInvZ;

		// Token: 0x040027D2 RID: 10194
		protected float m_statMinVal;

		// Token: 0x040027D3 RID: 10195
		protected float m_statMaxVal;

		// Token: 0x040027D4 RID: 10196
		protected double m_statSumVals;

		// Token: 0x040027D5 RID: 10197
		protected bool m_isDirty;

		// Token: 0x040027D6 RID: 10198
		protected byte[] m_metaData = new byte[0];
	}
}
