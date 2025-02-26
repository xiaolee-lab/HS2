using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace CharaUtils
{
	// Token: 0x02000820 RID: 2080
	public class Expression : MonoBehaviour
	{
		// Token: 0x060034FC RID: 13564 RVA: 0x001380D9 File Offset: 0x001364D9
		public void SetCharaTransform(Transform trf)
		{
			this.trfChara = trf;
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x001380E4 File Offset: 0x001364E4
		public void Initialize()
		{
			if (null == this.trfChara)
			{
				return;
			}
			this.FindObjectAll(this.dictTrf, this.trfChara);
			Transform transform = null;
			foreach (Expression.ScriptInfo scriptInfo in this.info)
			{
				if (scriptInfo.enableLookAt && scriptInfo.lookAt != null)
				{
					if (this.dictTrf.TryGetValue(scriptInfo.lookAt.lookAtName, out transform))
					{
						scriptInfo.lookAt.SetLookAtTransform(transform);
						if (this.dictTrf.TryGetValue(scriptInfo.lookAt.targetName, out transform))
						{
							scriptInfo.lookAt.SetTargetTransform(transform);
							this.dictTrf.TryGetValue(scriptInfo.lookAt.upAxisName, out transform);
							scriptInfo.lookAt.SetUpAxisTransform(transform);
						}
					}
				}
				if (scriptInfo.enableCorrect && scriptInfo.correct != null)
				{
					if (this.dictTrf.TryGetValue(scriptInfo.correct.correctName, out transform))
					{
						scriptInfo.correct.SetCorrectTransform(transform);
						this.dictTrf.TryGetValue(scriptInfo.correct.referenceName, out transform);
						scriptInfo.correct.SetReferenceTransform(transform);
					}
				}
			}
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x00138238 File Offset: 0x00136638
		public void FindObjectAll(Dictionary<string, Transform> _dictTrf, Transform _trf)
		{
			if (!_dictTrf.ContainsKey(_trf.name))
			{
				_dictTrf[_trf.name] = _trf;
			}
			for (int i = 0; i < _trf.childCount; i++)
			{
				this.FindObjectAll(_dictTrf, _trf.GetChild(i));
			}
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x00138288 File Offset: 0x00136688
		public void EnableCategory(int categoryNo, bool _enable)
		{
			for (int i = 0; i < this.info.Length; i++)
			{
				if (this.info[i].categoryNo == categoryNo)
				{
					this.info[i].enable = _enable;
				}
			}
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x001382CF File Offset: 0x001366CF
		public void EnableIndex(int indexNo, bool _enable)
		{
			if (0 <= indexNo && indexNo < this.info.Length)
			{
				this.info[indexNo].enable = _enable;
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x001382F4 File Offset: 0x001366F4
		private void Start()
		{
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x001382F8 File Offset: 0x001366F8
		private void LateUpdate()
		{
			if (this.info == null)
			{
				return;
			}
			if (this.enable)
			{
				foreach (Expression.ScriptInfo scriptInfo in this.info)
				{
					scriptInfo.Update();
				}
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x00138344 File Offset: 0x00136744
		private void OnDestroy()
		{
			if (this.info == null)
			{
				return;
			}
			foreach (Expression.ScriptInfo scriptInfo in this.info)
			{
				scriptInfo.Destroy();
			}
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x00138384 File Offset: 0x00136784
		public bool LoadSetting(string assetBundleName, string assetName)
		{
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(assetBundleName, assetName, false, string.Empty);
			if (null == textAsset)
			{
				return false;
			}
			string text = textAsset.text.Replace("\r", string.Empty);
			string[] collection = text.Split(new char[]
			{
				'\n'
			});
			List<string> list = new List<string>();
			list.AddRange(collection);
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, true);
			return this.LoadSettingSub(list);
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x001383F4 File Offset: 0x001367F4
		public bool LoadSetting(string path)
		{
			List<string> list = new List<string>();
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
				{
					while (streamReader.Peek() > -1)
					{
						list.Add(streamReader.ReadLine());
					}
				}
			}
			return this.LoadSettingSub(list);
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x00138484 File Offset: 0x00136884
		public bool LoadSettingSub(List<string> slist)
		{
			if (slist.Count == 0)
			{
				return false;
			}
			string[] array = slist[0].Split(new char[]
			{
				'\t'
			});
			int num = int.Parse(array[0]);
			if (num > slist.Count - 1)
			{
				return false;
			}
			this.info = new Expression.ScriptInfo[num];
			for (int i = 0; i < num; i++)
			{
				array = slist[i + 1].Split(new char[]
				{
					'\t'
				});
				this.info[i] = new Expression.ScriptInfo();
				this.info[i].index = i;
				int num2 = 0;
				this.info[i].categoryNo = int.Parse(array[num2++]);
				this.info[i].enableLookAt = (array[num2++] == "○");
				if (this.info[i].enableLookAt)
				{
					this.info[i].lookAt.lookAtName = array[num2++];
					if ("0" == this.info[i].lookAt.lookAtName)
					{
						this.info[i].lookAt.lookAtName = string.Empty;
					}
					else
					{
						this.info[i].elementName = this.info[i].lookAt.lookAtName;
					}
					this.info[i].lookAt.targetName = array[num2++];
					if ("0" == this.info[i].lookAt.targetName)
					{
						this.info[i].lookAt.targetName = string.Empty;
					}
					this.info[i].lookAt.targetAxisType = (Expression.LookAt.AxisType)Enum.Parse(typeof(Expression.LookAt.AxisType), array[num2++]);
					this.info[i].lookAt.upAxisName = array[num2++];
					if ("0" == this.info[i].lookAt.upAxisName)
					{
						this.info[i].lookAt.upAxisName = string.Empty;
					}
					this.info[i].lookAt.upAxisType = (Expression.LookAt.AxisType)Enum.Parse(typeof(Expression.LookAt.AxisType), array[num2++]);
					this.info[i].lookAt.sourceAxisType = (Expression.LookAt.AxisType)Enum.Parse(typeof(Expression.LookAt.AxisType), array[num2++]);
					this.info[i].lookAt.limitAxisType = (Expression.LookAt.AxisType)Enum.Parse(typeof(Expression.LookAt.AxisType), array[num2++]);
					this.info[i].lookAt.rotOrder = (Expression.LookAt.RotationOrder)Enum.Parse(typeof(Expression.LookAt.RotationOrder), array[num2++]);
					this.info[i].lookAt.limitMin = float.Parse(array[num2++]);
					this.info[i].lookAt.limitMax = float.Parse(array[num2++]);
				}
				else
				{
					num2 += 10;
				}
				this.info[i].enableCorrect = (array[num2++] == "○");
				if (this.info[i].enableCorrect)
				{
					this.info[i].correct.correctName = array[num2++];
					if ("0" == this.info[i].correct.correctName)
					{
						this.info[i].correct.correctName = string.Empty;
					}
					else
					{
						this.info[i].elementName = this.info[i].correct.correctName;
					}
					this.info[i].correct.referenceName = array[num2++];
					if ("0" == this.info[i].correct.referenceName)
					{
						this.info[i].correct.referenceName = string.Empty;
					}
					this.info[i].correct.calcType = (Expression.Correct.CalcType)Enum.Parse(typeof(Expression.Correct.CalcType), array[num2++]);
					this.info[i].correct.rotOrder = (Expression.Correct.RotationOrder)Enum.Parse(typeof(Expression.Correct.RotationOrder), array[num2++]);
					this.info[i].correct.charmRate = float.Parse(array[num2++]);
					this.info[i].correct.useRX = (array[num2++] == "○");
					this.info[i].correct.valRXMin = float.Parse(array[num2++]);
					this.info[i].correct.valRXMax = float.Parse(array[num2++]);
					this.info[i].correct.useRY = (array[num2++] == "○");
					this.info[i].correct.valRYMin = float.Parse(array[num2++]);
					this.info[i].correct.valRYMax = float.Parse(array[num2++]);
					this.info[i].correct.useRZ = (array[num2++] == "○");
					this.info[i].correct.valRZMin = float.Parse(array[num2++]);
					this.info[i].correct.valRZMax = float.Parse(array[num2++]);
				}
			}
			return true;
		}

		// Token: 0x0400357D RID: 13693
		public Transform trfChara;

		// Token: 0x0400357E RID: 13694
		public Expression.ScriptInfo[] info;

		// Token: 0x0400357F RID: 13695
		public bool enable = true;

		// Token: 0x04003580 RID: 13696
		private Dictionary<string, Transform> dictTrf = new Dictionary<string, Transform>();

		// Token: 0x02000821 RID: 2081
		[Serializable]
		public class LookAt
		{
			// Token: 0x06003507 RID: 13575 RVA: 0x00138A78 File Offset: 0x00136E78
			public LookAt()
			{
				this.trfLookAt = null;
				this.trfTarget = null;
				this.trfUpAxis = null;
			}

			// Token: 0x17000995 RID: 2453
			// (get) Token: 0x06003508 RID: 13576 RVA: 0x00138AE4 File Offset: 0x00136EE4
			// (set) Token: 0x06003509 RID: 13577 RVA: 0x00138AEC File Offset: 0x00136EEC
			public Transform trfLookAt { get; private set; }

			// Token: 0x17000996 RID: 2454
			// (get) Token: 0x0600350A RID: 13578 RVA: 0x00138AF5 File Offset: 0x00136EF5
			// (set) Token: 0x0600350B RID: 13579 RVA: 0x00138AFD File Offset: 0x00136EFD
			public Transform trfTarget { get; private set; }

			// Token: 0x17000997 RID: 2455
			// (get) Token: 0x0600350C RID: 13580 RVA: 0x00138B06 File Offset: 0x00136F06
			// (set) Token: 0x0600350D RID: 13581 RVA: 0x00138B0E File Offset: 0x00136F0E
			public Transform trfUpAxis { get; private set; }

			// Token: 0x0600350E RID: 13582 RVA: 0x00138B17 File Offset: 0x00136F17
			public void SetLookAtTransform(Transform trf)
			{
				this.trfLookAt = trf;
			}

			// Token: 0x0600350F RID: 13583 RVA: 0x00138B20 File Offset: 0x00136F20
			public void SetTargetTransform(Transform trf)
			{
				this.trfTarget = trf;
			}

			// Token: 0x06003510 RID: 13584 RVA: 0x00138B29 File Offset: 0x00136F29
			public void SetUpAxisTransform(Transform trf)
			{
				this.trfUpAxis = trf;
			}

			// Token: 0x06003511 RID: 13585 RVA: 0x00138B34 File Offset: 0x00136F34
			public void Update()
			{
				if (null == this.trfTarget || null == this.trfLookAt)
				{
					return;
				}
				Vector3 upVector = this.GetUpVector();
				Vector3 vector = Vector3.Normalize(this.trfTarget.position - this.trfLookAt.position);
				Vector3 vector2 = Vector3.Normalize(Vector3.Cross(upVector, vector));
				Vector3 vector3 = Vector3.Cross(vector, vector2);
				if (this.targetAxisType == Expression.LookAt.AxisType.RevX || this.targetAxisType == Expression.LookAt.AxisType.RevY || this.targetAxisType == Expression.LookAt.AxisType.RevZ)
				{
					vector = -vector;
					vector2 = -vector2;
				}
				Vector3 xvec = Vector3.zero;
				Vector3 yvec = Vector3.zero;
				Vector3 zvec = Vector3.zero;
				switch (this.targetAxisType)
				{
				case Expression.LookAt.AxisType.X:
				case Expression.LookAt.AxisType.RevX:
					xvec = vector;
					if (this.sourceAxisType == Expression.LookAt.AxisType.Y)
					{
						yvec = vector3;
						zvec = -vector2;
					}
					else if (this.sourceAxisType == Expression.LookAt.AxisType.RevY)
					{
						yvec = -vector3;
						zvec = vector2;
					}
					else if (this.sourceAxisType == Expression.LookAt.AxisType.Z)
					{
						yvec = vector2;
						zvec = vector3;
					}
					else if (this.sourceAxisType == Expression.LookAt.AxisType.RevZ)
					{
						yvec = -vector2;
						zvec = -vector3;
					}
					break;
				case Expression.LookAt.AxisType.Y:
				case Expression.LookAt.AxisType.RevY:
					yvec = vector;
					if (this.sourceAxisType == Expression.LookAt.AxisType.X)
					{
						xvec = vector3;
						zvec = vector2;
					}
					else if (this.sourceAxisType == Expression.LookAt.AxisType.RevX)
					{
						xvec = -vector3;
						zvec = -vector2;
					}
					else if (this.sourceAxisType == Expression.LookAt.AxisType.Z)
					{
						xvec = -vector2;
						zvec = vector3;
					}
					else if (this.sourceAxisType == Expression.LookAt.AxisType.RevZ)
					{
						xvec = vector2;
						zvec = -vector3;
					}
					break;
				case Expression.LookAt.AxisType.Z:
				case Expression.LookAt.AxisType.RevZ:
					zvec = vector;
					if (this.sourceAxisType == Expression.LookAt.AxisType.X)
					{
						xvec = vector3;
						yvec = -vector2;
					}
					else if (this.sourceAxisType == Expression.LookAt.AxisType.RevX)
					{
						xvec = -vector3;
						yvec = vector2;
					}
					else if (this.sourceAxisType == Expression.LookAt.AxisType.Y)
					{
						xvec = vector2;
						yvec = vector3;
					}
					else if (this.sourceAxisType == Expression.LookAt.AxisType.RevY)
					{
						xvec = -vector2;
						yvec = -vector3;
					}
					break;
				}
				if (this.limitAxisType == Expression.LookAt.AxisType.None)
				{
					this.trfLookAt.rotation = this.LookAtQuat(xvec, yvec, zvec);
				}
				else
				{
					this.trfLookAt.rotation = this.LookAtQuat(xvec, yvec, zvec);
					ConvertRotation.RotationOrder order = (ConvertRotation.RotationOrder)this.rotOrder;
					Quaternion localRotation = this.trfLookAt.localRotation;
					Vector3 vector4 = ConvertRotation.ConvertDegreeFromQuaternion(order, localRotation);
					Quaternion q = Quaternion.Slerp(localRotation, Quaternion.identity, 0.5f);
					Vector3 vector5 = ConvertRotation.ConvertDegreeFromQuaternion(order, q);
					if (this.limitAxisType == Expression.LookAt.AxisType.X)
					{
						if ((vector4.x < 0f && vector5.x > 0f) || (vector4.x > 0f && vector5.x < 0f))
						{
							vector4.x *= -1f;
						}
						vector4.x = Mathf.Clamp(vector4.x, this.limitMin, this.limitMax);
					}
					else if (this.limitAxisType == Expression.LookAt.AxisType.Y)
					{
						if ((vector4.y < 0f && vector5.y > 0f) || (vector4.y > 0f && vector5.y < 0f))
						{
							vector4.y *= -1f;
						}
						vector4.y = Mathf.Clamp(vector4.y, this.limitMin, this.limitMax);
					}
					else if (this.limitAxisType == Expression.LookAt.AxisType.Z)
					{
						if ((vector4.z < 0f && vector5.z > 0f) || (vector4.z > 0f && vector5.z < 0f))
						{
							vector4.z *= -1f;
						}
						vector4.z = Mathf.Clamp(vector4.z, this.limitMin, this.limitMax);
					}
					this.trfLookAt.localRotation = ConvertRotation.ConvertDegreeToQuaternion(order, vector4.x, vector4.y, vector4.z);
				}
			}

			// Token: 0x06003512 RID: 13586 RVA: 0x00138FA0 File Offset: 0x001373A0
			private Vector3 GetUpVector()
			{
				Vector3 result = Vector3.up;
				if (null != this.trfUpAxis)
				{
					Expression.LookAt.AxisType axisType = this.upAxisType;
					if (axisType != Expression.LookAt.AxisType.X)
					{
						if (axisType != Expression.LookAt.AxisType.Y)
						{
							if (axisType == Expression.LookAt.AxisType.Z)
							{
								result = this.trfUpAxis.forward;
							}
						}
						else
						{
							result = this.trfUpAxis.up;
						}
					}
					else
					{
						result = this.trfUpAxis.right;
					}
				}
				return result;
			}

			// Token: 0x06003513 RID: 13587 RVA: 0x00139018 File Offset: 0x00137418
			private Quaternion LookAtQuat(Vector3 xvec, Vector3 yvec, Vector3 zvec)
			{
				float num = 1f + xvec.x + yvec.y + zvec.z;
				if (num == 0f)
				{
					return Quaternion.identity;
				}
				float num2 = Mathf.Sqrt(num) / 2f;
				if (float.IsNaN(num2))
				{
					return Quaternion.identity;
				}
				float num3 = 4f * num2;
				if (num3 == 0f)
				{
					return Quaternion.identity;
				}
				float x = (yvec.z - zvec.y) / num3;
				float y = (zvec.x - xvec.z) / num3;
				float z = (xvec.y - yvec.x) / num3;
				return new Quaternion(x, y, z, num2);
			}

			// Token: 0x04003581 RID: 13697
			public string lookAtName = string.Empty;

			// Token: 0x04003583 RID: 13699
			public string targetName = string.Empty;

			// Token: 0x04003585 RID: 13701
			public Expression.LookAt.AxisType targetAxisType = Expression.LookAt.AxisType.Z;

			// Token: 0x04003586 RID: 13702
			public string upAxisName = string.Empty;

			// Token: 0x04003588 RID: 13704
			public Expression.LookAt.AxisType upAxisType = Expression.LookAt.AxisType.Y;

			// Token: 0x04003589 RID: 13705
			public Expression.LookAt.AxisType sourceAxisType = Expression.LookAt.AxisType.Y;

			// Token: 0x0400358A RID: 13706
			public Expression.LookAt.AxisType limitAxisType = Expression.LookAt.AxisType.None;

			// Token: 0x0400358B RID: 13707
			public Expression.LookAt.RotationOrder rotOrder = Expression.LookAt.RotationOrder.ZXY;

			// Token: 0x0400358C RID: 13708
			[Range(-180f, 180f)]
			public float limitMin;

			// Token: 0x0400358D RID: 13709
			[Range(-180f, 180f)]
			public float limitMax;

			// Token: 0x02000822 RID: 2082
			public enum AxisType
			{
				// Token: 0x0400358F RID: 13711
				X,
				// Token: 0x04003590 RID: 13712
				Y,
				// Token: 0x04003591 RID: 13713
				Z,
				// Token: 0x04003592 RID: 13714
				RevX,
				// Token: 0x04003593 RID: 13715
				RevY,
				// Token: 0x04003594 RID: 13716
				RevZ,
				// Token: 0x04003595 RID: 13717
				None
			}

			// Token: 0x02000823 RID: 2083
			public enum RotationOrder
			{
				// Token: 0x04003597 RID: 13719
				XYZ,
				// Token: 0x04003598 RID: 13720
				XZY,
				// Token: 0x04003599 RID: 13721
				YXZ,
				// Token: 0x0400359A RID: 13722
				YZX,
				// Token: 0x0400359B RID: 13723
				ZXY,
				// Token: 0x0400359C RID: 13724
				ZYX
			}
		}

		// Token: 0x02000824 RID: 2084
		[Serializable]
		public class Correct
		{
			// Token: 0x06003514 RID: 13588 RVA: 0x001390CE File Offset: 0x001374CE
			public Correct()
			{
				this.trfCorrect = null;
				this.trfReference = null;
			}

			// Token: 0x17000998 RID: 2456
			// (get) Token: 0x06003515 RID: 13589 RVA: 0x00139101 File Offset: 0x00137501
			// (set) Token: 0x06003516 RID: 13590 RVA: 0x00139109 File Offset: 0x00137509
			public Transform trfCorrect { get; private set; }

			// Token: 0x17000999 RID: 2457
			// (get) Token: 0x06003517 RID: 13591 RVA: 0x00139112 File Offset: 0x00137512
			// (set) Token: 0x06003518 RID: 13592 RVA: 0x0013911A File Offset: 0x0013751A
			public Transform trfReference { get; private set; }

			// Token: 0x06003519 RID: 13593 RVA: 0x00139123 File Offset: 0x00137523
			public void SetCorrectTransform(Transform trf)
			{
				this.trfCorrect = trf;
			}

			// Token: 0x0600351A RID: 13594 RVA: 0x0013912C File Offset: 0x0013752C
			public void SetReferenceTransform(Transform trf)
			{
				this.trfReference = trf;
			}

			// Token: 0x0600351B RID: 13595 RVA: 0x00139138 File Offset: 0x00137538
			public void Update()
			{
				if (null == this.trfCorrect || null == this.trfReference)
				{
					return;
				}
				if (this.calcType == Expression.Correct.CalcType.Euler)
				{
					ConvertRotation.RotationOrder order = (ConvertRotation.RotationOrder)this.rotOrder;
					Vector3 vector = ConvertRotation.ConvertDegreeFromQuaternion(order, this.trfCorrect.localRotation);
					Vector3 vector2 = ConvertRotation.ConvertDegreeFromQuaternion(order, this.trfReference.localRotation);
					Quaternion q = Quaternion.identity;
					Vector3 vector3 = Vector3.zero;
					if (this.charmRate != 0f)
					{
						q = Quaternion.Slerp(this.trfReference.localRotation, Quaternion.identity, this.charmRate);
						vector3 = ConvertRotation.ConvertDegreeFromQuaternion(order, q);
					}
					if (this.useRX)
					{
						float num = Mathf.InverseLerp(0f, 90f, Mathf.Clamp(Mathf.Abs(vector2.x), 0f, 90f));
						num = Mathf.Lerp(this.valRXMin, this.valRXMax, num);
						vector.x = vector2.x * num;
						if (this.charmRate != 0f && ((vector2.x < 0f && vector3.x > 0f) || (vector2.x > 0f && vector3.x < 0f)))
						{
							vector.x *= -1f;
						}
					}
					if (this.useRY)
					{
						float num = Mathf.InverseLerp(0f, 90f, Mathf.Clamp(Mathf.Abs(vector2.y), 0f, 90f));
						num = Mathf.Lerp(this.valRYMin, this.valRYMax, num);
						vector.y = vector2.y * num;
						if (this.charmRate != 0f && ((vector2.y < 0f && vector3.y > 0f) || (vector2.y > 0f && vector3.y < 0f)))
						{
							vector.y *= -1f;
						}
					}
					if (this.useRZ)
					{
						float num = Mathf.InverseLerp(0f, 90f, Mathf.Clamp(Mathf.Abs(vector2.z), 0f, 90f));
						num = Mathf.Lerp(this.valRZMin, this.valRZMax, num);
						vector.z = vector2.z * num;
						if (this.charmRate != 0f && ((vector2.z < 0f && vector3.z > 0f) || (vector2.z > 0f && vector3.z < 0f)))
						{
							vector.z *= -1f;
						}
					}
					this.trfCorrect.localRotation = ConvertRotation.ConvertDegreeToQuaternion(order, vector.x, vector.y, vector.z);
				}
				else if (this.calcType == Expression.Correct.CalcType.Quaternion)
				{
					Quaternion localRotation = this.trfCorrect.localRotation;
					if (this.useRX)
					{
						localRotation.x = this.trfReference.localRotation.x * (this.valRXMin + this.valRXMax) * 0.5f;
					}
					if (this.useRY)
					{
						localRotation.y = this.trfReference.localRotation.y * (this.valRYMin + this.valRYMax) * 0.5f;
					}
					if (this.useRZ)
					{
						localRotation.z = this.trfReference.localRotation.z * (this.valRZMin + this.valRZMax) * 0.5f;
					}
					this.trfCorrect.localRotation = localRotation;
				}
			}

			// Token: 0x0400359D RID: 13725
			public string correctName = string.Empty;

			// Token: 0x0400359F RID: 13727
			public string referenceName = string.Empty;

			// Token: 0x040035A1 RID: 13729
			public Expression.Correct.CalcType calcType;

			// Token: 0x040035A2 RID: 13730
			public Expression.Correct.RotationOrder rotOrder = Expression.Correct.RotationOrder.ZXY;

			// Token: 0x040035A3 RID: 13731
			[Range(0f, 1f)]
			public float charmRate;

			// Token: 0x040035A4 RID: 13732
			public bool useRX;

			// Token: 0x040035A5 RID: 13733
			[Range(-1f, 1f)]
			public float valRXMin;

			// Token: 0x040035A6 RID: 13734
			[Range(-1f, 1f)]
			public float valRXMax;

			// Token: 0x040035A7 RID: 13735
			public bool useRY;

			// Token: 0x040035A8 RID: 13736
			[Range(-1f, 1f)]
			public float valRYMin;

			// Token: 0x040035A9 RID: 13737
			[Range(-1f, 1f)]
			public float valRYMax;

			// Token: 0x040035AA RID: 13738
			public bool useRZ;

			// Token: 0x040035AB RID: 13739
			[Range(-1f, 1f)]
			public float valRZMin;

			// Token: 0x040035AC RID: 13740
			[Range(-1f, 1f)]
			public float valRZMax;

			// Token: 0x02000825 RID: 2085
			public enum CalcType
			{
				// Token: 0x040035AE RID: 13742
				Euler,
				// Token: 0x040035AF RID: 13743
				Quaternion
			}

			// Token: 0x02000826 RID: 2086
			public enum RotationOrder
			{
				// Token: 0x040035B1 RID: 13745
				XYZ,
				// Token: 0x040035B2 RID: 13746
				XZY,
				// Token: 0x040035B3 RID: 13747
				YXZ,
				// Token: 0x040035B4 RID: 13748
				YZX,
				// Token: 0x040035B5 RID: 13749
				ZXY,
				// Token: 0x040035B6 RID: 13750
				ZYX
			}
		}

		// Token: 0x02000827 RID: 2087
		[Serializable]
		public class ScriptInfo
		{
			// Token: 0x0600351D RID: 13597 RVA: 0x00139558 File Offset: 0x00137958
			public void Update()
			{
				if (!this.enable)
				{
					return;
				}
				if (this.enableLookAt && this.lookAt != null)
				{
					this.lookAt.Update();
				}
				if (this.enableCorrect && this.correct != null)
				{
					this.correct.Update();
				}
			}

			// Token: 0x0600351E RID: 13598 RVA: 0x001395B3 File Offset: 0x001379B3
			public void UpdateArrow()
			{
			}

			// Token: 0x0600351F RID: 13599 RVA: 0x001395B5 File Offset: 0x001379B5
			public void Destroy()
			{
			}

			// Token: 0x06003520 RID: 13600 RVA: 0x001395B7 File Offset: 0x001379B7
			public void DestroyArrow()
			{
			}

			// Token: 0x040035B7 RID: 13751
			public string elementName = string.Empty;

			// Token: 0x040035B8 RID: 13752
			public bool enable = true;

			// Token: 0x040035B9 RID: 13753
			public bool enableLookAt;

			// Token: 0x040035BA RID: 13754
			public Expression.LookAt lookAt = new Expression.LookAt();

			// Token: 0x040035BB RID: 13755
			public bool enableCorrect;

			// Token: 0x040035BC RID: 13756
			public Expression.Correct correct = new Expression.Correct();

			// Token: 0x040035BD RID: 13757
			public int index;

			// Token: 0x040035BE RID: 13758
			public int categoryNo;
		}
	}
}
