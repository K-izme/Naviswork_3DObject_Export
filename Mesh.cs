using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Interop.ComApi;
using System;
using System.Collections.Generic;
using System.IO;

namespace Geommeetryy
{
    public class Mesh
    {
        public List<Point3D> Points;
        public List<int> Faces;
        public List<int> Lines;

        public Mesh()
        {
            Points = new List<Point3D>();
            Faces = new List<int>();
            Lines = new List<int>(); 
        }

        internal int GetOrAddPointIndex(Point3D point)
        {
            if (Points.Contains(point))
            {
                int index = Points.IndexOf(point) + 1;
                return index;
            }
            else
            {
                Points.Add(point);
                return Points.Count;
            }
        }

        internal void AddTriangle(InwSimpleVertex v1, InwSimpleVertex v2, InwSimpleVertex v3)
        {
            int index1 = GetOrAddPointIndex(v1.ToPoint3D());
            int index2 = GetOrAddPointIndex(v2.ToPoint3D());
            int index3 = GetOrAddPointIndex(v3.ToPoint3D());

            Faces.Add(index1);
            Faces.Add(index2);
            Faces.Add(index3);
        }

        internal void AddLine(InwSimpleVertex v1, InwSimpleVertex v2)
        {
            int index1 = GetOrAddPointIndex(v1.ToPoint3D());
            int index2 = GetOrAddPointIndex(v2.ToPoint3D());

            Lines.Add(index1);
            Lines.Add(index2);
        }

        internal void AddPoint(InwSimpleVertex v1)
        {
            
        }

        internal void WriteObj(string filePath)
        {
            if (Points.Count == 0 && Faces.Count == 0 && Lines.Count == 0)
            {
                Console.WriteLine("Nothing");
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (Point3D point in Points)
                    {
                        writer.WriteLine($"v {point.X} {point.Y} {point.Z}");
                    }

                    for (int i = 0; i < Faces.Count; i += 3)
                    {
                        int v1 = Faces[i];
                        int v2 = Faces[i + 1];
                        int v3 = Faces[i + 2];
                        writer.WriteLine($"f {v1} {v2} {v3}");
                    }

                    //for (int i = 0; i < Lines.Count; i += 2)
                    //{
                    //    int v1 = Lines[i];
                    //    int v2 = Lines[i + 1];
                    //    writer.WriteLine($"l {v1} {v2}");
                    //}
                }
                Console.WriteLine($"Wrote {filePath}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
