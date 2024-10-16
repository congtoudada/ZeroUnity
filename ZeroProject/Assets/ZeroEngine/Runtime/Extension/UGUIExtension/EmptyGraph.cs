/****************************************************
  文件：EmptyGraph.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年10月16日 18:36:34
  功能：
*****************************************************/

using UnityEngine.UI;

public class EmptyGraph : Graphic
{
    public bool m_debug = false;

    protected override void OnPopulateMesh(VertexHelper vbo)
    {
        vbo.Clear();

#if UNITY_EDITOR
        if (m_debug)
        {
            base.OnPopulateMesh(vbo);
        }
#endif
    }
}
