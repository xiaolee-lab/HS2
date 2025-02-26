using System;
using System.Collections.Generic;
using AIChara;

namespace CharaCustom
{
	// Token: 0x020009AF RID: 2479
	public class CustomClothesFileInfoAssist
	{
		// Token: 0x0600474B RID: 18251 RVA: 0x001B6EA0 File Offset: 0x001B52A0
		private static void AddList(List<CustomClothesFileInfo> _list, string path, byte sex, bool preset, ref int idx)
		{
			string[] searchPattern = new string[]
			{
				"*.png"
			};
			CoordinateCategoryKind coordinateCategoryKind = (sex != 0) ? CoordinateCategoryKind.Female : CoordinateCategoryKind.Male;
			if (preset)
			{
				coordinateCategoryKind |= CoordinateCategoryKind.Preset;
			}
			FolderAssist folderAssist = new FolderAssist();
			folderAssist.CreateFolderInfoEx(path, searchPattern, true);
			int fileCount = folderAssist.GetFileCount();
			for (int i = 0; i < fileCount; i++)
			{
				ChaFileCoordinate chaFileCoordinate = new ChaFileCoordinate();
				if (!chaFileCoordinate.LoadFile(folderAssist.lstFile[i].FullPath))
				{
					int lastErrorCode = chaFileCoordinate.GetLastErrorCode();
				}
				else
				{
					_list.Add(new CustomClothesFileInfo
					{
						index = idx++,
						name = chaFileCoordinate.coordinateName,
						FullPath = folderAssist.lstFile[i].FullPath,
						FileName = folderAssist.lstFile[i].FileName,
						time = folderAssist.lstFile[i].time,
						cateKind = coordinateCategoryKind
					});
				}
			}
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x001B6FB8 File Offset: 0x001B53B8
		public static List<CustomClothesFileInfo> CreateClothesFileInfoList(bool useMale, bool useFemale, bool useMyData = true, bool usePreset = true)
		{
			List<CustomClothesFileInfo> list = new List<CustomClothesFileInfo>();
			int num = 0;
			if (usePreset)
			{
				if (useMale)
				{
					string path = DefaultData.Path + "coordinate/male/";
					CustomClothesFileInfoAssist.AddList(list, path, 0, true, ref num);
				}
				if (useFemale)
				{
					string path2 = DefaultData.Path + "coordinate/female/";
					CustomClothesFileInfoAssist.AddList(list, path2, 1, true, ref num);
				}
			}
			if (useMyData)
			{
				if (useMale)
				{
					string path3 = UserData.Path + "coordinate/male/";
					CustomClothesFileInfoAssist.AddList(list, path3, 0, false, ref num);
				}
				if (useFemale)
				{
					string path4 = UserData.Path + "coordinate/female/";
					CustomClothesFileInfoAssist.AddList(list, path4, 1, false, ref num);
				}
			}
			return list;
		}
	}
}
