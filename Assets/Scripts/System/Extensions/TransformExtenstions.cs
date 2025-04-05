using System;
using Frontend.Component.Vfx.Easing;
using UnityEngine;

namespace Core.Extensions
{
    public enum EaseType
    {
        QuadraticIn,
        QuadraticOut,
        QuarticIn,
        QuarticOut,
        QuintyIn,
        QuintyOut,
        CircularIn,
        CircularOut,
        CubicIn,
        CubicOut,
        ExponentialIn,
        ExponentialOut,
        SinusoidalIn,
        SinusoidalOut,
        LineIn,
        LineOut
    }

    public enum Afin
    {
        WorldPosition,
        LocalPosition,
        Scale,
        Rotation
    }

    public static class TransformExtensions
    {
        public static void Easing(this Transform source, EaseType easeType, Afin afin, float currentTime, float start, float end, float totalTime, bool fillX, bool fillY, bool fillZ, in Func<float, float, float, Vector3> cb = null)
        {
            var easeValue = 0f;
            switch (easeType)
            {
                case EaseType.QuadraticIn:
                    easeValue = Quadratic.EaseIn(currentTime, start, end, totalTime);
                    break;
                case EaseType.QuadraticOut:
                    easeValue = Quadratic.EaseOut(currentTime, start, end, totalTime);
                    break;
                case EaseType.QuarticIn:
                    easeValue = Quartic.EaseIn(currentTime, start, end, totalTime);
                    break;
                case EaseType.QuarticOut:
                    easeValue = Quartic.EaseOut(currentTime, start, end, totalTime);
                    break;
                case EaseType.QuintyIn:
                    easeValue = Quinty.EaseIn(currentTime, start, end, totalTime);
                    break;
                case EaseType.QuintyOut:
                    easeValue = Quinty.EaseOut(currentTime, start, end, totalTime);
                    break;
                case EaseType.SinusoidalIn:
                    easeValue = Sinusoidal.EaseIn(currentTime, start, end, totalTime);
                    break;
                case EaseType.SinusoidalOut:
                    easeValue = Sinusoidal.EaseOut(currentTime, start, end, totalTime);
                    break;
                case EaseType.CircularIn:
                    easeValue = Circular.EaseIn(currentTime, start, end, totalTime);
                    break;
                case EaseType.CircularOut:
                    easeValue = Circular.EaseOut(currentTime, start, end, totalTime);
                    break;
                case EaseType.CubicIn:
                    easeValue = Cubic.EaseIn(currentTime, start, end, totalTime);
                    break;
                case EaseType.CubicOut:
                    easeValue = Cubic.EaseOut(currentTime, start, end, totalTime);
                    break;
                case EaseType.LineIn:
                    easeValue = Liner.EaseIn(currentTime, start, end, totalTime);
                    break;
                case EaseType.LineOut:
                    easeValue = Liner.EaseOut(currentTime, start, end, totalTime);
                    break;
                case EaseType.ExponentialIn:
                    easeValue = Exponential.EaseIn(currentTime, start, end, totalTime);
                    break;
                case EaseType.ExponentialOut:
                    easeValue = Exponential.EaseOut(currentTime, start, end, totalTime);
                    break;
            }

            var x = fillX ? easeValue : 0;
            var y = fillY ? easeValue : 0;
            var z = fillZ ? easeValue : 0;
            var result = null == cb ? new Vector3(x, y, z) : cb.Invoke(x, y, z);
            switch (afin)
            {
                case Afin.WorldPosition:
                    source.position = result;
                    break;
                case Afin.LocalPosition:
                    source.localPosition = result;
                    break;
                case Afin.Scale:
                    source.localScale = result;
                    break;
                case Afin.Rotation:
                    source.rotation = Quaternion.Euler(result);
                    break;
            }
        }
    }
}