using UnityEngine;
using UnityEngine.UI;

namespace Core.Extensions
{
    public static class GraphicExtensions
    {
        public static void FillAlpha(this Graphic uiElement, float alpha)
        {
            uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b,alpha);
        }
    }
}