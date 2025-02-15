using UnityEngine;

namespace Core.Extensions
{
    public static class SpriteRendererExtensions
    {
        public static void FillAlpha(this SpriteRenderer renderer, float alpha)
        {
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
}