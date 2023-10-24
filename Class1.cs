using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Navisworks.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComBridge = Autodesk.Navisworks.Api.ComApi.ComApiBridge;
using COMApi = Autodesk.Navisworks.Api.Interop.ComApi;
using wf = System.Windows.Forms;
using System.IO;

namespace Geommeetryy
{
    #region InwSimplePrimitivesCB Class
    class CallbackGeomListener : COMApi.InwSimplePrimitivesCB
    {

        public Mesh Geometry { get; private set; }
        public CallbackGeomListener()
        {
            Geometry = new Mesh();
        }
        public void Line(COMApi.InwSimpleVertex v1, COMApi.InwSimpleVertex v2)
        {
            Geometry.AddLine(v1, v2);
        }
        public void Point(COMApi.InwSimpleVertex v1)
        {
            Geometry.AddPoint(v1);
        }

        public void SnapPoint(COMApi.InwSimpleVertex v1)
        {
            Geometry.AddPoint(v1);
        }

        public void Triangle(COMApi.InwSimpleVertex v1, COMApi.InwSimpleVertex v2, COMApi.InwSimpleVertex v3)
        {
            Geometry.AddTriangle(v1, v2, v3);
        }
    }
    #endregion

    #region Naviswork
    [Plugin("API",
        "7c580887-66f8-5629-ab42-457c647b9b46",
        ExtendedToolTip = "Help",
        DisplayName = "AppInfo2",
        Options = PluginOptions.None)]
    public class MainClass : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            ModelItemCollection oModelColl = Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentSelection.SelectedItems;
            COMApi.InwOpState oState = ComBridge.State;
            COMApi.InwOpSelection oSel = ComBridge.ToInwOpSelection(oModelColl);
            int count = 0;

            foreach (COMApi.InwOaPath3 path in oSel.Paths())
            {
                foreach (COMApi.InwOaFragment3 frag in path.Fragments())
                {
                    count++;
                    CallbackGeomListener callbkListener = new CallbackGeomListener();
                    frag.GenerateSimplePrimitives(COMApi.nwEVertexProperty.eNORMAL, callbkListener);
                    callbkListener.Geometry.WriteObj($"C:\\Users\\gohan\\OneDrive\\Documents\\GitHub\\testCsharp\\Geommeetryy\\Output\\output_{count}.obj");
                }
            }
            return 0;
        }
    }
    #endregion

}
