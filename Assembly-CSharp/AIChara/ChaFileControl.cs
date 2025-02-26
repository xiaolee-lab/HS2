using System;
using System.Collections.Generic;
using System.IO;
using Manager;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007BE RID: 1982
	public class ChaFileControl : ChaFile
	{
		// Token: 0x06003061 RID: 12385 RVA: 0x00121CFC File Offset: 0x001200FC
		public static bool CheckDataRangeFace(ChaFile chafile, List<string> checkInfo = null)
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			ChaFileFace face = chafile.custom.face;
			byte sex = chafile.parameter.sex;
			bool result = false;
			for (int i = 0; i < face.shapeValueFace.Length; i++)
			{
				if (!MathfEx.RangeEqualOn<float>(0f, face.shapeValueFace[i], 1f))
				{
					if (checkInfo != null)
					{
						checkInfo.Add(string.Format("【{0}】値:{1}", ChaFileDefine.cf_headshapename[i], face.shapeValueFace[i]));
					}
					result = true;
					face.shapeValueFace[i] = Mathf.Clamp(face.shapeValueFace[i], 0f, 1f);
				}
			}
			if (face.shapeValueFace.Length > ChaFileDefine.cf_headshapename.Length)
			{
				float[] array = new float[ChaFileDefine.cf_headshapename.Length];
				Array.Copy(face.shapeValueFace, array, array.Length);
				face.shapeValueFace = new float[ChaFileDefine.cf_headshapename.Length];
				Array.Copy(array, face.shapeValueFace, array.Length);
			}
			Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo((sex != 0) ? ChaListDefine.CategoryNo.fo_head : ChaListDefine.CategoryNo.mo_head);
			if (!categoryInfo.ContainsKey(face.headId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【頭の種類】値:{0}", face.headId));
				}
				result = true;
				face.headId = ((sex != 0) ? 0 : 0);
			}
			categoryInfo = chaListCtrl.GetCategoryInfo((sex != 0) ? ChaListDefine.CategoryNo.ft_skin_f : ChaListDefine.CategoryNo.mt_skin_f);
			if (!categoryInfo.ContainsKey(face.skinId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【肌の種類】値:{0}", face.skinId));
				}
				result = true;
				face.skinId = 0;
			}
			categoryInfo = chaListCtrl.GetCategoryInfo((sex != 0) ? ChaListDefine.CategoryNo.ft_detail_f : ChaListDefine.CategoryNo.mt_detail_f);
			if (!categoryInfo.ContainsKey(face.detailId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【彫りの種類】値:{0}", face.detailId));
				}
				result = true;
				face.detailId = 0;
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eyebrow);
			if (!categoryInfo.ContainsKey(face.eyebrowId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【眉毛の種類】値:{0}", face.eyebrowId));
				}
				result = true;
				face.eyebrowId = 0;
			}
			for (int j = 0; j < 2; j++)
			{
				categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eye);
				if (!categoryInfo.ContainsKey(face.pupil[j].pupilId))
				{
					if (checkInfo != null)
					{
						checkInfo.Add(string.Format("【瞳の種類】値:{0}", face.pupil[j].pupilId));
					}
					result = true;
					face.pupil[j].pupilId = 0;
				}
				categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eyeblack);
				if (!categoryInfo.ContainsKey(face.pupil[j].blackId))
				{
					if (checkInfo != null)
					{
						checkInfo.Add(string.Format("【黒目の種類】値:{0}", face.pupil[j].blackId));
					}
					result = true;
					face.pupil[j].blackId = 0;
				}
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eye_hl);
			if (!categoryInfo.ContainsKey(face.hlId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【ハイライト種類】値:{0}", face.hlId));
				}
				result = true;
				face.hlId = 0;
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eyelash);
			if (!categoryInfo.ContainsKey(face.eyelashesId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【睫毛の種類】値:{0}", face.eyelashesId));
				}
				result = true;
				face.eyelashesId = 0;
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_mole);
			if (!categoryInfo.ContainsKey(face.moleId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【ホクロの種類】値:{0}", face.moleId));
				}
				result = true;
				face.moleId = 0;
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_eyeshadow);
			if (!categoryInfo.ContainsKey(face.makeup.eyeshadowId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【アイシャドウ種類】値:{0}", face.makeup.eyeshadowId));
				}
				result = true;
				face.makeup.eyeshadowId = 0;
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_cheek);
			if (!categoryInfo.ContainsKey(face.makeup.cheekId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【チークの種類】値:{0}", face.makeup.cheekId));
				}
				result = true;
				face.makeup.cheekId = 0;
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_lip);
			if (!categoryInfo.ContainsKey(face.makeup.lipId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【リップの種類】値:{0}", face.makeup.lipId));
				}
				result = true;
				face.makeup.lipId = 0;
			}
			for (int k = 0; k < 2; k++)
			{
				categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_paint);
				if (!categoryInfo.ContainsKey(face.makeup.paintInfo[k].id))
				{
					if (checkInfo != null)
					{
						checkInfo.Add(string.Format("【ペイント種類】値:{0}", face.makeup.paintInfo[k].id));
					}
					result = true;
					face.makeup.paintInfo[k].id = 0;
				}
			}
			if (sex == 0)
			{
				categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.mt_beard);
				if (!categoryInfo.ContainsKey(face.beardId))
				{
					if (checkInfo != null)
					{
						checkInfo.Add(string.Format("【ヒゲの種類】値:{0}", face.beardId));
					}
					result = true;
					face.beardId = 0;
				}
			}
			return result;
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x001222F4 File Offset: 0x001206F4
		public static bool CheckDataRangeBody(ChaFile chafile, List<string> checkInfo = null)
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			ChaFileBody body = chafile.custom.body;
			byte sex = chafile.parameter.sex;
			bool result = false;
			for (int i = 0; i < body.shapeValueBody.Length; i++)
			{
				if (!MathfEx.RangeEqualOn<float>(0f, body.shapeValueBody[i], 1f))
				{
					if (checkInfo != null)
					{
						checkInfo.Add(string.Format("【{0}】値:{1}", ChaFileDefine.cf_bodyshapename[i], body.shapeValueBody[i]));
					}
					result = true;
					body.shapeValueBody[i] = Mathf.Clamp(body.shapeValueBody[i], 0f, 1f);
				}
			}
			if (!MathfEx.RangeEqualOn<float>(0f, body.bustSoftness, 1f))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【胸の柔らかさ】値:{0}", body.bustSoftness));
				}
				result = true;
				body.bustSoftness = Mathf.Clamp(body.bustSoftness, 0f, 1f);
			}
			if (!MathfEx.RangeEqualOn<float>(0f, body.bustWeight, 1f))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【胸の重さ】値:{0}", body.bustWeight));
				}
				result = true;
				body.bustWeight = Mathf.Clamp(body.bustWeight, 0f, 1f);
			}
			Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo((sex != 0) ? ChaListDefine.CategoryNo.ft_skin_b : ChaListDefine.CategoryNo.mt_skin_b);
			if (!categoryInfo.ContainsKey(body.skinId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【肌の種類】値:{0}", body.skinId));
				}
				result = true;
				body.skinId = 0;
			}
			categoryInfo = chaListCtrl.GetCategoryInfo((sex != 0) ? ChaListDefine.CategoryNo.ft_detail_b : ChaListDefine.CategoryNo.mt_detail_b);
			if (!categoryInfo.ContainsKey(body.detailId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【筋肉の種類】値:{0}", body.detailId));
				}
				result = true;
				body.detailId = 0;
			}
			categoryInfo = chaListCtrl.GetCategoryInfo((sex != 0) ? ChaListDefine.CategoryNo.ft_sunburn : ChaListDefine.CategoryNo.mt_sunburn);
			if (!categoryInfo.ContainsKey(body.sunburnId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【日焼けの種類】値:{0}", body.sunburnId));
				}
				result = true;
				body.sunburnId = 0;
			}
			for (int j = 0; j < 2; j++)
			{
				categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_paint);
				if (!categoryInfo.ContainsKey(body.paintInfo[j].id))
				{
					if (checkInfo != null)
					{
						checkInfo.Add(string.Format("【ペイントの種類】値:{0}", body.paintInfo[j].id));
					}
					result = true;
					body.paintInfo[j].id = 0;
				}
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_nip);
			if (!categoryInfo.ContainsKey(body.nipId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【乳首の種類】値:{0}", body.nipId));
				}
				result = true;
				body.nipId = 0;
			}
			if (!MathfEx.RangeEqualOn<float>(0f, body.areolaSize, 1f))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【乳輪のサイズ】値:{0}", body.areolaSize));
				}
				result = true;
				body.areolaSize = Mathf.Clamp(body.areolaSize, 0f, 1f);
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_underhair);
			if (!categoryInfo.ContainsKey(body.underhairId))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【アンダーヘア種類】値:{0}", body.underhairId));
				}
				result = true;
				body.underhairId = 0;
			}
			return result;
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x001226CC File Offset: 0x00120ACC
		public static bool CheckDataRangeHair(ChaFile chafile, List<string> checkInfo = null)
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			ChaFileHair hair = chafile.custom.hair;
			byte sex = chafile.parameter.sex;
			bool result = false;
			Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.so_hair_b);
			if (!categoryInfo.ContainsKey(hair.parts[0].id))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【後ろ髪】値:{0}", hair.parts[0].id));
				}
				result = true;
				hair.parts[0].id = ((sex != 0) ? 0 : 0);
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.so_hair_f);
			if (!categoryInfo.ContainsKey(hair.parts[1].id))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【前髪】値:{0}", hair.parts[1].id));
				}
				result = true;
				hair.parts[1].id = ((sex != 0) ? 1 : 2);
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.so_hair_s);
			if (!categoryInfo.ContainsKey(hair.parts[2].id))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【横髪】値:{0}", hair.parts[2].id));
				}
				result = true;
				hair.parts[2].id = ((sex != 0) ? 0 : 0);
			}
			categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.so_hair_o);
			if (!categoryInfo.ContainsKey(hair.parts[3].id))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【エクステ】値:{0}", hair.parts[3].id));
				}
				result = true;
				hair.parts[3].id = ((sex != 0) ? 0 : 0);
			}
			return result;
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x001228A8 File Offset: 0x00120CA8
		public static bool CheckDataRangeCoordinate(ChaFile chafile, List<string> checkInfo = null)
		{
			ChaFileCoordinate coordinate = chafile.coordinate;
			return ChaFileControl.CheckDataRangeCoordinate(coordinate, (int)chafile.parameter.sex, checkInfo);
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x001228D0 File Offset: 0x00120CD0
		public static bool CheckDataRangeCoordinate(ChaFileCoordinate coorde, int sex, List<string> checkInfo = null)
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			bool result = false;
			string[] array = new string[]
			{
				"トップス",
				"ボトムス",
				"インナー上",
				"インナー下",
				"手袋",
				"パンスト",
				"靴下",
				"靴"
			};
			ChaListDefine.CategoryNo[] array2 = new ChaListDefine.CategoryNo[]
			{
				(sex != 0) ? ChaListDefine.CategoryNo.fo_top : ChaListDefine.CategoryNo.mo_top,
				(sex != 0) ? ChaListDefine.CategoryNo.fo_bot : ChaListDefine.CategoryNo.mo_bot,
				(sex != 0) ? ChaListDefine.CategoryNo.fo_inner_t : ChaListDefine.CategoryNo.unknown,
				(sex != 0) ? ChaListDefine.CategoryNo.fo_inner_b : ChaListDefine.CategoryNo.unknown,
				(sex != 0) ? ChaListDefine.CategoryNo.fo_gloves : ChaListDefine.CategoryNo.mo_gloves,
				(sex != 0) ? ChaListDefine.CategoryNo.fo_panst : ChaListDefine.CategoryNo.unknown,
				(sex != 0) ? ChaListDefine.CategoryNo.fo_socks : ChaListDefine.CategoryNo.unknown,
				(sex != 0) ? ChaListDefine.CategoryNo.fo_shoes : ChaListDefine.CategoryNo.mo_shoes
			};
			int[] array3 = new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7
			};
			int[] array4 = new int[]
			{
				(sex != 0) ? 0 : 0,
				(sex != 0) ? 0 : 0,
				(sex != 0) ? 0 : -1,
				(sex != 0) ? 0 : -1,
				(sex != 0) ? 0 : 0,
				(sex != 0) ? 0 : -1,
				(sex != 0) ? 0 : -1,
				(sex != 0) ? 0 : 0
			};
			ChaFileClothes clothes = coorde.clothes;
			for (int i = 0; i < array3.Length; i++)
			{
				if (array2[i] != ChaListDefine.CategoryNo.unknown)
				{
					Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo(array2[i]);
					if (!categoryInfo.ContainsKey(clothes.parts[array3[i]].id))
					{
						if (checkInfo != null)
						{
							checkInfo.Add(string.Format("【{0}】値:{1}", array[i], clothes.parts[array3[i]].id));
						}
						result = true;
						clothes.parts[array3[i]].id = array4[i];
					}
					categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.st_pattern);
					for (int j = 0; j < clothes.parts[array3[i]].colorInfo.Length; j++)
					{
						if (!categoryInfo.ContainsKey(clothes.parts[array3[i]].colorInfo[j].pattern))
						{
							if (checkInfo != null)
							{
								checkInfo.Add(string.Format("【柄】値:{0}", clothes.parts[array3[i]].colorInfo[j].pattern));
							}
							result = true;
							clothes.parts[array3[i]].colorInfo[j].pattern = 0;
						}
					}
				}
			}
			ChaFileAccessory accessory = coorde.accessory;
			for (int k = 0; k < accessory.parts.Length; k++)
			{
				int type = accessory.parts[k].type;
				int id = accessory.parts[k].id;
				Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo((ChaListDefine.CategoryNo)type);
				if (categoryInfo != null)
				{
					if (!categoryInfo.ContainsKey(accessory.parts[k].id))
					{
						if (checkInfo != null)
						{
							checkInfo.Add(string.Format("【{0}】値:{1}", ChaAccessoryDefine.GetAccessoryTypeName((ChaListDefine.CategoryNo)type), accessory.parts[k].id));
						}
						result = true;
						accessory.parts[k].MemberInit();
					}
				}
			}
			return result;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x00122C94 File Offset: 0x00121094
		public static bool CheckDataRangeParameter(ChaFile chafile, List<string> checkInfo = null)
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			ChaFileParameter parameter = chafile.parameter;
			bool result = false;
			if (parameter.sex == 0)
			{
				return false;
			}
			if (!Singleton<Voice>.Instance.voiceInfoDic.ContainsKey(parameter.personality))
			{
				if (checkInfo != null)
				{
					checkInfo.Add(string.Format("【性格】値:{0}", parameter.personality));
				}
				result = true;
				parameter.personality = 0;
			}
			return result;
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x00122D08 File Offset: 0x00121108
		public static bool CheckDataRange(ChaFile chafile, bool chk_custom, bool chk_clothes, bool chk_parameter, List<string> checkInfo = null)
		{
			bool flag = false;
			if (chk_custom)
			{
				flag |= ChaFileControl.CheckDataRangeFace(chafile, checkInfo);
				flag |= ChaFileControl.CheckDataRangeBody(chafile, checkInfo);
				flag |= ChaFileControl.CheckDataRangeHair(chafile, checkInfo);
			}
			if (chk_clothes)
			{
				flag |= ChaFileControl.CheckDataRangeCoordinate(chafile, checkInfo);
			}
			if (chk_parameter)
			{
				flag |= ChaFileControl.CheckDataRangeParameter(chafile, checkInfo);
			}
			if (flag)
			{
			}
			return flag;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x00122D68 File Offset: 0x00121168
		public static bool InitializeCharaFile(string filename, int sex)
		{
			ChaFileControl chaFileControl = new ChaFileControl();
			if (!chaFileControl.LoadCharaFile(filename, (byte)sex, false, true))
			{
				return false;
			}
			if (chaFileControl.gameinfo.gameRegistration)
			{
				return true;
			}
			chaFileControl.InitGameInfoParam();
			chaFileControl.gameinfo.gameRegistration = true;
			chaFileControl.SaveCharaFile(filename, (byte)sex, false);
			return true;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x00122DC0 File Offset: 0x001211C0
		public bool InitGameInfoParam()
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			ListInfoBase listInfo = chaListCtrl.GetListInfo(ChaListDefine.CategoryNo.init_type_param, this.parameter.personality);
			this.gameinfo.moodBound.lower = (float)listInfo.GetInfoInt(ChaListDefine.KeyType.MoodLow);
			this.gameinfo.moodBound.upper = (float)listInfo.GetInfoInt(ChaListDefine.KeyType.MoodUp);
			this.gameinfo.flavorState = new Dictionary<int, int>();
			for (int i = 0; i < 8; i++)
			{
				this.gameinfo.flavorState[i] = 0;
			}
			this.gameinfo.desireDefVal = new Dictionary<int, float>();
			this.gameinfo.desireBuffVal = new Dictionary<int, float>();
			for (int j = 0; j < 16; j++)
			{
				this.gameinfo.desireDefVal[j] = 0f;
				this.gameinfo.desireBuffVal[j] = 0f;
			}
			this.gameinfo.motivation = 0;
			this.gameinfo.immoral = 0;
			this.gameinfo.morality = 50;
			for (int k = 0; k < 8; k++)
			{
				Dictionary<int, int> flavorState;
				int key;
				(flavorState = this.gameinfo.flavorState)[key = k] = flavorState[key] + listInfo.GetInfoInt(ChaListDefine.KeyType.FS_00 + k);
			}
			for (int l = 0; l < 16; l++)
			{
				Dictionary<int, float> dictionary;
				int key2;
				(dictionary = this.gameinfo.desireDefVal)[key2 = l] = dictionary[key2] + (float)listInfo.GetInfoInt(ChaListDefine.KeyType.DD_00 + l);
				int key3;
				(dictionary = this.gameinfo.desireBuffVal)[key3 = l] = dictionary[key3] + (float)listInfo.GetInfoInt(ChaListDefine.KeyType.DB_00 + l);
			}
			this.gameinfo.motivation += listInfo.GetInfoInt(ChaListDefine.KeyType.Motivation);
			this.gameinfo.immoral += listInfo.GetInfoInt(ChaListDefine.KeyType.Immoral);
			Dictionary<int, ListInfoBase> categoryInfo = chaListCtrl.GetCategoryInfo(ChaListDefine.CategoryNo.init_wish_param);
			if (this.parameter.hsWish != null && this.parameter.hsWish.Count != 0)
			{
				foreach (int key4 in this.parameter.hsWish)
				{
					ListInfoBase listInfoBase;
					if (categoryInfo.TryGetValue(key4, out listInfoBase))
					{
						this.gameinfo.moodBound.lower += (float)listInfoBase.GetInfoInt(ChaListDefine.KeyType.MoodLow);
						this.gameinfo.moodBound.upper += (float)listInfoBase.GetInfoInt(ChaListDefine.KeyType.MoodUp);
						for (int m = 0; m < 8; m++)
						{
							Dictionary<int, int> flavorState;
							int key5;
							(flavorState = this.gameinfo.flavorState)[key5 = m] = flavorState[key5] + listInfoBase.GetInfoInt(ChaListDefine.KeyType.FS_00 + m);
						}
						for (int n = 0; n < 16; n++)
						{
							Dictionary<int, float> dictionary;
							int key6;
							(dictionary = this.gameinfo.desireDefVal)[key6 = n] = dictionary[key6] + (float)listInfoBase.GetInfoInt(ChaListDefine.KeyType.DD_00 + n);
							int key7;
							(dictionary = this.gameinfo.desireBuffVal)[key7 = n] = dictionary[key7] + (float)listInfoBase.GetInfoInt(ChaListDefine.KeyType.DB_00 + n);
						}
						this.gameinfo.motivation += listInfoBase.GetInfoInt(ChaListDefine.KeyType.Motivation);
						this.gameinfo.immoral += listInfoBase.GetInfoInt(ChaListDefine.KeyType.Immoral);
					}
				}
			}
			for (int num = 0; num < 8; num++)
			{
				this.gameinfo.flavorState[num] = Mathf.Max(this.gameinfo.flavorState[num], 0);
			}
			return true;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x001231DC File Offset: 0x001215DC
		public bool SaveCharaFile(string filename, byte sex = 255, bool newFile = false)
		{
			string path = this.ConvertCharaFilePath(filename, sex, newFile);
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			base.charaFileName = Path.GetFileName(path);
			string userID = this.userID;
			string dataID = this.dataID;
			if (this.userID != Singleton<GameSystem>.Instance.UserUUID)
			{
				this.dataID = YS_Assist.CreateUUID();
			}
			else if (!File.Exists(path))
			{
				this.dataID = YS_Assist.CreateUUID();
			}
			this.userID = Singleton<GameSystem>.Instance.UserUUID;
			bool result;
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				bool flag = this.SaveCharaFile(fileStream, true);
				this.userID = userID;
				this.dataID = dataID;
				result = flag;
			}
			return result;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x001232C4 File Offset: 0x001216C4
		public bool SaveCharaFile(Stream st, bool savePng)
		{
			bool result;
			using (BinaryWriter binaryWriter = new BinaryWriter(st))
			{
				result = this.SaveCharaFile(binaryWriter, savePng);
			}
			return result;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x00123304 File Offset: 0x00121704
		public bool SaveCharaFile(BinaryWriter bw, bool savePng)
		{
			return base.SaveFile(bw, savePng, (int)Singleton<GameSystem>.Instance.language);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x00123318 File Offset: 0x00121718
		public bool LoadCharaFile(string filename, byte sex = 255, bool noLoadPng = false, bool noLoadStatus = true)
		{
			if (filename.IsNullOrEmpty())
			{
				return false;
			}
			base.charaFileName = Path.GetFileName(filename);
			string path = this.ConvertCharaFilePath(filename, sex, false);
			if (!File.Exists(path))
			{
				return false;
			}
			bool result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				result = this.LoadCharaFile(fileStream, noLoadPng, noLoadStatus);
			}
			return result;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0012338C File Offset: 0x0012178C
		public bool LoadCharaFile(Stream st, bool noLoadPng = false, bool noLoadStatus = true)
		{
			bool result;
			using (BinaryReader binaryReader = new BinaryReader(st))
			{
				result = this.LoadCharaFile(binaryReader, noLoadPng, noLoadStatus);
			}
			return result;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x001233D0 File Offset: 0x001217D0
		public bool LoadCharaFile(BinaryReader br, bool noLoadPng = false, bool noLoadStatus = true)
		{
			bool result = base.LoadFile(br, (int)Singleton<GameSystem>.Instance.language, noLoadPng, noLoadStatus);
			if (!this.skipRangeCheck)
			{
				Singleton<Character>.Instance.isMod = ChaFileControl.CheckDataRange(this, true, true, true, null);
			}
			return result;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x00123414 File Offset: 0x00121814
		public bool LoadFileLimited(string filename, byte sex = 255, bool face = true, bool body = true, bool hair = true, bool parameter = true, bool coordinate = true)
		{
			string path = this.ConvertCharaFilePath(filename, sex, false);
			ChaFileControl chaFileControl = new ChaFileControl();
			if (chaFileControl.LoadFile(path, (int)Singleton<GameSystem>.Instance.language, false, true))
			{
				if (!this.skipRangeCheck)
				{
					Singleton<Character>.Instance.isMod = ChaFileControl.CheckDataRange(chaFileControl, true, true, true, null);
				}
				if (face)
				{
					byte[] bytes = MessagePackSerializer.Serialize<ChaFileFace>(chaFileControl.custom.face);
					this.custom.face = MessagePackSerializer.Deserialize<ChaFileFace>(bytes);
				}
				if (body)
				{
					byte[] bytes = MessagePackSerializer.Serialize<ChaFileBody>(chaFileControl.custom.body);
					this.custom.body = MessagePackSerializer.Deserialize<ChaFileBody>(bytes);
				}
				if (hair)
				{
					byte[] bytes = MessagePackSerializer.Serialize<ChaFileHair>(chaFileControl.custom.hair);
					this.custom.hair = MessagePackSerializer.Deserialize<ChaFileHair>(bytes);
				}
				if (parameter)
				{
					this.parameter.Copy(chaFileControl.parameter);
					this.gameinfo.Copy(chaFileControl.gameinfo);
					this.parameter2.Copy(chaFileControl.parameter2);
					this.gameinfo2.Copy(chaFileControl.gameinfo2);
				}
				if (coordinate)
				{
					base.CopyCoordinate(chaFileControl.coordinate);
				}
				if (face && body && hair && parameter && coordinate)
				{
					this.userID = chaFileControl.userID;
					this.dataID = chaFileControl.dataID;
				}
			}
			return false;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x00123580 File Offset: 0x00121980
		public bool LoadMannequinFile(string assetBundleName, string assetName, bool face = true, bool body = true, bool hair = true, bool parameter = true, bool coordinate = true)
		{
			ChaFileControl chaFileControl = new ChaFileControl();
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(assetBundleName, assetName, false, string.Empty);
			if (null == textAsset)
			{
				return false;
			}
			if (chaFileControl.LoadFromTextAsset(textAsset, true, true))
			{
				if (face)
				{
					byte[] bytes = MessagePackSerializer.Serialize<ChaFileFace>(chaFileControl.custom.face);
					this.custom.face = MessagePackSerializer.Deserialize<ChaFileFace>(bytes);
				}
				if (body)
				{
					byte[] bytes = MessagePackSerializer.Serialize<ChaFileBody>(chaFileControl.custom.body);
					this.custom.body = MessagePackSerializer.Deserialize<ChaFileBody>(bytes);
				}
				if (hair)
				{
					byte[] bytes = MessagePackSerializer.Serialize<ChaFileHair>(chaFileControl.custom.hair);
					this.custom.hair = MessagePackSerializer.Deserialize<ChaFileHair>(bytes);
				}
				if (parameter)
				{
					this.parameter.Copy(chaFileControl.parameter);
				}
				if (coordinate)
				{
					base.CopyCoordinate(chaFileControl.coordinate);
				}
			}
			return false;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x00123668 File Offset: 0x00121A68
		public bool LoadFromAssetBundle(string assetBundleName, string assetName, bool noSetPNG = false, bool noLoadStatus = true)
		{
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(assetBundleName, assetName, false, string.Empty);
			if (null == textAsset)
			{
				return false;
			}
			bool result = this.LoadFromTextAsset(textAsset, noSetPNG, noLoadStatus);
			AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, true);
			return result;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x001236A8 File Offset: 0x00121AA8
		public bool LoadFromTextAsset(TextAsset ta, bool noSetPNG = false, bool noLoadStatus = true)
		{
			if (null == ta)
			{
				return false;
			}
			bool result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				memoryStream.Write(ta.bytes, 0, ta.bytes.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = this.LoadCharaFile(memoryStream, noSetPNG, noLoadStatus);
			}
			return result;
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x00123718 File Offset: 0x00121B18
		public bool LoadFromBytes(byte[] bytes, bool noSetPNG = false, bool noLoadStatus = true)
		{
			bool result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				memoryStream.Write(bytes, 0, bytes.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				bool flag = this.LoadCharaFile(memoryStream, noSetPNG, noLoadStatus);
				if (!this.skipRangeCheck)
				{
					Singleton<Character>.Instance.isMod = ChaFileControl.CheckDataRange(this, true, true, true, null);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x00123790 File Offset: 0x00121B90
		public void SaveFace(string path)
		{
			if (this.custom != null)
			{
				this.custom.SaveFace(path);
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x001237A9 File Offset: 0x00121BA9
		public void LoadFace(string path)
		{
			if (this.custom != null)
			{
				this.custom.LoadFace(path);
			}
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x001237C4 File Offset: 0x00121BC4
		public void LoadFacePreset()
		{
			ChaListControl chaListCtrl = Singleton<Character>.Instance.chaListCtrl;
			ListInfoBase listInfo = chaListCtrl.GetListInfo((this.parameter.sex != 0) ? ChaListDefine.CategoryNo.fo_head : ChaListDefine.CategoryNo.mo_head, this.custom.face.headId);
			string info = listInfo.GetInfo(ChaListDefine.KeyType.MainManifest);
			string info2 = listInfo.GetInfo(ChaListDefine.KeyType.MainAB);
			string info3 = listInfo.GetInfo(ChaListDefine.KeyType.Preset);
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(info2, info3, false, info);
			if (null == textAsset)
			{
				return;
			}
			this.custom.LoadFace(textAsset.bytes);
			AssetBundleManager.UnloadAssetBundle(info2, true, null, false);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x00123860 File Offset: 0x00121C60
		public string ConvertCharaFilePath(string path, byte _sex, bool newFile = false)
		{
			byte b = (_sex != byte.MaxValue) ? _sex : this.parameter.sex;
			string text = string.Empty;
			string text2 = string.Empty;
			if (!(path != string.Empty))
			{
				return string.Empty;
			}
			text = Path.GetDirectoryName(path);
			text2 = Path.GetFileName(path);
			if (text == string.Empty)
			{
				text = UserData.Path + ((b != 0) ? "chara/female/" : "chara/male/");
			}
			else
			{
				text += "/";
			}
			if (text2 == string.Empty)
			{
				if (newFile || base.charaFileName == string.Empty)
				{
					if (b == 0)
					{
						text2 = "AISChaM_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
					}
					else
					{
						text2 = "AISChaF_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
					}
				}
				else
				{
					text2 = base.charaFileName;
				}
			}
			if (Path.GetExtension(text2).IsNullOrEmpty())
			{
				return text + Path.GetFileNameWithoutExtension(text2) + ".png";
			}
			return text + text2;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x001239A8 File Offset: 0x00121DA8
		public static string ConvertCoordinateFilePath(string path, byte sex)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			if (!(path != string.Empty))
			{
				return string.Empty;
			}
			text = Path.GetDirectoryName(path);
			text2 = Path.GetFileName(path);
			if (text == string.Empty)
			{
				text = UserData.Path + ((sex != 0) ? "coordinate/female/" : "coordinate/male/");
			}
			else
			{
				text += "/";
			}
			if (text2 == string.Empty)
			{
				if (sex == 0)
				{
					text2 = "AISCoordeM_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
				}
				else
				{
					text2 = "AISCoordeF_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
				}
			}
			if (Path.GetExtension(text2).IsNullOrEmpty())
			{
				return text + Path.GetFileNameWithoutExtension(text2) + ".png";
			}
			return text + text2;
		}

		// Token: 0x04002E2A RID: 11818
		public bool skipRangeCheck;
	}
}
