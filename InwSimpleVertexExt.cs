using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Interop.ComApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geommeetryy
{
    internal static class InwSimpleVertexExt
    {
        public static Point3D ToPoint3D(this InwSimpleVertex vertex)
        {
            Array array_v1 = (Array)(object)vertex.coord;
            return new Point3D((float)array_v1.GetValue(1), (float)array_v1.GetValue(2), (float)array_v1.GetValue(3));
        }
    }
}
