/****************************************************
  文件：Utility_Excel_IExcelHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月31日 17:11:01
  功能：
*****************************************************/

/****************************************************
  文件：Utility_Json_IJsonHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月31日 15:41:25
  功能：
*****************************************************/

using System;

namespace ZeroEngine
{
    public static partial class Utility
    {
        public static partial class Excel
        {
            /// <summary>
            /// Excel 辅助器接口。表示Excel文件
            /// </summary>
            public interface IExcelPackageHelper
            {
                /// <summary>
                /// 当前表格数
                /// </summary>
                int SheetCount { get; }
                
                /// <summary>
                /// 加载Excel文件
                /// </summary>
                /// <param name="path"></param>
                /// <returns></returns>
                IExcelPackageHelper Load(string path);
                
                /// <summary>
                /// 保存Excel文件并关闭
                /// </summary>
                /// <returns></returns>
                bool Save();
                
                /// <summary>
                /// 不保存关闭
                /// </summary>
                /// <returns></returns>
                bool DontSave();
                
                /// <summary>
                /// 加载Excel文件并打开指定索引的表格
                /// </summary>
                /// <param name="path"></param>
                /// <param name="sheetIdx"></param>
                /// <returns></returns>
                IExcelSheetHelper LoadAndOpen(string path, int sheetIdx);
                
                /// <summary>
                /// 加载Excel文件并打开指定名称的表格
                /// </summary>
                /// <param name="path"></param>
                /// <param name="sheetName"></param>
                /// <returns></returns>
                IExcelSheetHelper LoadAndOpen(string path, string sheetName);
                
                /// <summary>
                /// 打开指定索引的Excel表
                /// </summary>
                /// <param name="sheetIdx"></param>
                /// <returns></returns>
                IExcelSheetHelper OpenExcelSheet(int sheetIdx);

                /// <summary>
                /// 打开指定名称的Excel表
                /// </summary>
                /// <param name="sheetName"></param>
                /// <returns></returns>
                IExcelSheetHelper OpenExcelSheet(string sheetName);

                /// <summary>
                /// 新增Excel表并返回新表
                /// </summary>
                /// <param name="sheetName"></param>
                /// <returns></returns>
                IExcelSheetHelper AddSheet(string sheetName);
            
                /// <summary>
                /// 删除Excel表
                /// </summary>
                /// <param name="sheetIdx"></param>
                /// <returns></returns>
                bool RemoveSheet(int sheetIdx);

                /// <summary>
                /// 删除Excel表
                /// </summary>
                /// <param name="sheetName"></param>
                bool RemoveSheet(string sheetName);
            }
            
            
            /// <summary>
            /// ExcelSheet 辅助器接口。表示Excel文件的某张表
            /// </summary>
            public interface IExcelSheetHelper
            {
                /// <summary>
                /// 表格当前最大有效行数
                /// </summary>
                public int MaxRow { get; }
                
                /// <summary>
                /// 表格当前最大有效列数
                /// </summary>
                public int MaxColumn { get; }
                
                /// <summary>
                /// 表格所在Package的index
                /// </summary>
                public int SheetIdx { get; }
                
                /// <summary>
                /// 表格名称
                /// </summary>
                public string SheetName { get; }
                
                /// <summary>
                /// 表格所属的Package
                /// </summary>
                public IExcelPackageHelper ExcelPackage { get; }

                /// <summary>
                /// 根据行列获取值
                /// </summary>
                /// <param name="row"></param>
                /// <param name="column"></param>
                /// <returns></returns>
                object ReadValue(int row, int column);

                /// <summary>
                /// 根据行列获取值
                /// </summary>
                /// <param name="row"></param>
                /// <param name="column"></param>
                /// <typeparam name="T"></typeparam>
                /// <returns></returns>
                T ReadValue<T>(int row, int column);

                /// <summary>
                /// 根据行列写入值
                /// </summary>
                /// <param name="row"></param>
                /// <param name="column"></param>
                /// <param name="val"></param>
                /// <returns></returns>
                IExcelSheetHelper WriteValue(int row, int column, object val);
            }
        }
    }
}
