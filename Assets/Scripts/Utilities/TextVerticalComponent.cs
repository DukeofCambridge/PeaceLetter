using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/TextVerticalComponent", 10)]
public class TextVerticalComponent : Text
{
    public bool m_Virtical = true;
    private float lineSpace = 1;
    private float textSpace = 1;
    private float xOffset = 0;
    private float yOffset = 0;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        //关闭自动裁切文本
        //解决有时裁切文本会出错的问题
        verticalOverflow = VerticalWrapMode.Overflow;
    }
#endif

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        if (m_Virtical)
        {
            VirticalText(toFill);
        }
    }

    private void VirticalText(VertexHelper toFill)
    {
        if (!IsActive())
            return;

        lineSpace = fontSize * lineSpacing;
        textSpace = fontSize;

        //排除掉锚点变化造成的影响
        xOffset = rectTransform.rect.width + rectTransform.rect.x - fontSize / 2;
        yOffset = rectTransform.rect.height + rectTransform.rect.y - fontSize / 2;

        //重新计算一版排版
        int row = 0;
        int col = 0;
        float height = fontSize / 2;

        int minCount = toFill.currentVertCount / 4;

        if (rectTransform.rect.height < fontSize)
        {
            return;
        }

        for (int i = 0; i < minCount; i++)
        {
            if (height > rectTransform.rect.height || text[i] == '.')
            {
                col++;
                row = 0;
                height = fontSize / 2;
            }
            ModifyText(toFill, i, row, col);
            if (text[i] != '.')
            {
                row++;
                height += fontSize;
            }
        }
    }

    void ModifyText(VertexHelper helper, int i, int charYPos, int charXPos)
    {
        //Text 的绘制是每4个顶点绘制一个字符
        //按字符顺序取出顶点，则可以获得字符的位置
        //并对其进行修改

        //取出原来顶点的位置
        UIVertex lb = new UIVertex();
        helper.PopulateUIVertex(ref lb, i * 4);

        UIVertex lt = new UIVertex();
        helper.PopulateUIVertex(ref lt, i * 4 + 1);

        UIVertex rt = new UIVertex();
        helper.PopulateUIVertex(ref rt, i * 4 + 2);

        UIVertex rb = new UIVertex();
        helper.PopulateUIVertex(ref rb, i * 4 + 3);

        //计算文本的中心点
        Vector3 center = Vector3.Lerp(lb.position, rt.position, 0.5f);

        float x = -charXPos * lineSpace + xOffset;
        float y = -charYPos * textSpace + yOffset;

        //计算字符新位置
        Vector3 pos = new Vector3(x, y, 0);

        lb.position = lb.position - center + new Vector3(x, y, 0);
        lt.position = lt.position - center + new Vector3(x, y, 0);
        rt.position = rt.position - center + new Vector3(x, y, 0);
        rb.position = rb.position - center + new Vector3(x, y, 0);

        helper.SetUIVertex(lb, i * 4);
        helper.SetUIVertex(lt, i * 4 + 1);
        helper.SetUIVertex(rt, i * 4 + 2);
        helper.SetUIVertex(rb, i * 4 + 3);
    }
}