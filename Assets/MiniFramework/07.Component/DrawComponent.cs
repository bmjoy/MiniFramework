using System;
using System.Collections.Generic;
using UnityEngine;
namespace MiniFramework
{
    public enum PatternType
    {
        扇形,
        圆形,
        矩形,
    }
    public enum DrawType
    {
        LineRenderer,
        Mesh,
        None,
    }
    public class DrawComponent : MonoBehaviour
    {
        public PatternAttribute Pattern;
        // Use this for initialization
        private void Start()
        {
            switch (Pattern.PatternType)
            {
                case PatternType.圆形:
                    switch (Pattern.DrawType)
                    {
                        case DrawType.LineRenderer:
                            DrawUtil.CircleOnLineRenderer(transform, Pattern.Radius, Pattern.Color);
                            break;
                        case DrawType.Mesh:
                            DrawUtil.CircleOnMesh(transform, Pattern.Radius, Pattern.Color);
                            break;
                    }
                    break;
                case PatternType.扇形:
                    switch (Pattern.DrawType)
                    {
                        case DrawType.LineRenderer:
                            DrawUtil.SectorOnLineRenderer(transform, Pattern.Angle, Pattern.Radius, Pattern.Color);
                            break;
                        case DrawType.Mesh:
                            DrawUtil.SectorOnMesh(transform, Pattern.Angle, Pattern.Radius, Pattern.Color);
                            break;
                    }
                    break;
                case PatternType.矩形:
                    switch (Pattern.DrawType)
                    {
                        case DrawType.LineRenderer:
                            DrawUtil.RectangleOnLineRenderer(transform, Pattern.Length, Pattern.Width, Pattern.Color);
                            break;
                        case DrawType.Mesh:
                            DrawUtil.RectangleOnMesh(transform, Pattern.Length, Pattern.Width, Pattern.Color);
                            break;
                    }
                    break;
            }
        }
        private void OnDrawGizmos()
        {

            switch (Pattern.PatternType)
            {
                case PatternType.圆形:
                    DrawUtil.CircleOnGizmos(transform, Pattern.Radius, Pattern.Color);
                    break;
                case PatternType.扇形:
                    DrawUtil.SectorOnGizmos(transform, Pattern.Angle, Pattern.Radius, Pattern.Color);
                    break;
                case PatternType.矩形:
                    DrawUtil.RectangleOnGizmos(transform, Pattern.Length, Pattern.Width, Pattern.Color);
                    break;
            }

        }
    }
    [Serializable]
    public class PatternAttribute
    {
        [Header("图形")]
        public PatternType PatternType;
        public DrawType DrawType;
        public Color Color = Color.black;
        [Range(0, 360)]
        public float Angle;
        public float Radius;
        public float Length;
        public float Width;
    }
}
