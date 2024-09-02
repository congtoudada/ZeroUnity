/****************************************************
  文件：Utility_Excel.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月31日 16:38:30
  功能：
*****************************************************/

namespace ZeroEngine
{
    public static partial class Utility
    {
        /// <summary>
        /// Excel相关的实用函数。
        /// * 只提供同时对一个Excel文件的基础操作，先调用Load加载，操作完后记得调用Save或DontSave
        /// * 如果需要同时操作多个Excel文件，请自行new DefaultExcelPackageHelper
        /// </summary>
        public static partial class Excel
        {
            private static IExcelPackageHelper _excelPackageHelper = new DefaultExcelPackageHelper();
            private static IExcelSheetHelper _excelSheetHelper = null;

            #region Excel文件相关
            /// <summary>
            /// 设置 Excel 辅助器。
            /// </summary>
            /// <param name="excelHelper">要设置的 Excel 辅助器。</param>
            public static void SetExcelHelper(IExcelPackageHelper excelHelper)
            {
                _excelPackageHelper = excelHelper;
            }
            
            /// <summary>
            /// 获取 Excel 辅助器。
            /// </summary>
            /// <returns></returns>
            public static IExcelPackageHelper GetExcelHelper()
            {
                return _excelPackageHelper;
            }

            /// <summary>
            /// 加载Excel文件指定表
            /// </summary>
            /// <param name="path"></param>
            /// <param name="sheetIdx">默认第1张</param>
            /// <returns></returns>
            public static IExcelSheetHelper Load(string path, int sheetIdx=1)
            {
                _excelSheetHelper = _excelPackageHelper.LoadAndOpen(path, sheetIdx);
                return _excelSheetHelper;
            }
            
            /// <summary>
            /// 加载Excel文件指定表
            /// </summary>
            /// <param name="path"></param>
            /// <param name="sheetName"></param>
            /// <returns></returns>
            public static IExcelSheetHelper Load(string path, string sheetName)
            {
                _excelSheetHelper = _excelPackageHelper.LoadAndOpen(path, sheetName);
                return _excelSheetHelper;
            }

            /// <summary>
            /// 保存Excel文件并关闭
            /// </summary>
            /// <returns></returns>
            public static bool Save()
            {
                return _excelPackageHelper.Save();
            }

            /// <summary>
            /// 不保存关闭
            /// </summary>
            /// <returns></returns>
            public static bool DontSave()
            {
                return _excelPackageHelper.DontSave();
            }
            #endregion
            
            #region Excel表相关

            /// <summary>
            /// 表格当前最大有效行数
            /// </summary>
            public static int MaxRow
            {
                get
                {
                    if (_excelSheetHelper == null) return 0;
                    return _excelSheetHelper.MaxRow;
                }
            }

            /// <summary>
            /// 表格当前最大有效列数
            /// </summary>
            public static int MaxColumn
            {
                get
                {
                    if (_excelSheetHelper == null) return 0;
                    return _excelSheetHelper.MaxColumn;
                }
            }

            /// <summary>
            /// 根据行列获取值
            /// </summary>
            /// <param name="row"></param>
            /// <param name="column"></param>
            /// <returns></returns>
            public static object ReadValue(int row, int column)
            {
                if (_excelSheetHelper == null)
                {
                    Log.Warning("Excel表格未加载，请先调用Load!");
                    return null;
                }
                return _excelSheetHelper.ReadValue(row, column);
            }

            /// <summary>
            /// 根据行列获取值
            /// </summary>
            /// <param name="row"></param>
            /// <param name="column"></param>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static T ReadValue<T>(int row, int column)
            {
                if (_excelSheetHelper == null)
                {
                    Log.Warning("Excel表格未加载，请先调用Load!");
                    return default(T);
                }
                return _excelSheetHelper.ReadValue<T>(row, column);
            }

            /// <summary>
            /// 根据行列写入值
            /// </summary>
            /// <param name="row"></param>
            /// <param name="column"></param>
            /// <param name="val"></param>
            /// <returns></returns>
            public static IExcelSheetHelper WriteValue(int row, int column, object val)
            {
                if (_excelSheetHelper == null)
                {
                    Log.Warning("Excel表格未加载，请先调用Load!");
                    return _excelSheetHelper;
                }
                return _excelSheetHelper.WriteValue(row, column, val);
            }
            #endregion
            
        }
    }
}