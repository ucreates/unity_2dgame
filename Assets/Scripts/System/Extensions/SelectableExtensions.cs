using UnityEngine;
using UnityEngine.UI;

namespace Core.Extensions
{
    public static class SelectableExtensions
    {
        public static void FillAlpha(this Selectable uiElement, float alpha, bool fillNormal, bool fillPressed, bool filHighlightend)
        {
            var colorBlock = uiElement.colors;
            if (fillNormal)
                colorBlock.normalColor = new Color(colorBlock.normalColor.r, colorBlock.normalColor.g, colorBlock.normalColor.b, alpha);
            if (fillPressed)
                colorBlock.pressedColor = new Color(colorBlock.pressedColor.r, colorBlock.pressedColor.g, colorBlock.pressedColor.b, alpha);
            if (filHighlightend)
                colorBlock.highlightedColor = new Color(colorBlock.highlightedColor.r, colorBlock.highlightedColor.g, colorBlock.highlightedColor.b, alpha);
            uiElement.colors = colorBlock;
        }
    }
}