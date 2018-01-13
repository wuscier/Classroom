using System.Collections.Generic;

namespace Classroom.Models
{
    public class StrokeModel
    {
        public string ColorString { get; set; }
        public byte Width { get; set; }
        public byte Height { get; set; }

        public List<PointModel> Points { get; set; }
    }

    public class PointModel
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
