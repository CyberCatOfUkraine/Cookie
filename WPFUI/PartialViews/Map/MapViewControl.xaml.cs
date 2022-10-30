using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mapsui;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.UI.Wpf;
using Brush = System.Windows.Media.Brush;
using Point = System.Windows.Point;


namespace WPFUI.PartialViews.Map
{
    /// <summary>
    /// Interaction logic for MapViewControl.xaml
    /// </summary>
    public partial class MapViewControl : UserControl
    {
        public MapViewControl()
        {
            InitializeComponent();
            LoadMapContainerGrid();
        }

        private void LoadMapContainerGrid()
        {
            MapControl mapControl = new MapControl();

            var layer = Mapsui.Utilities.OpenStreetMap.CreateTileLayer();
            mapControl.Map?.Layers.Add(layer, CreateLineStringLayer("Test",new List<Point>(){new Point(34.322,47.322),new Point(34.323,47.326)}));

            var hrenb = SphericalMercator.FromLonLat(34.014, 48.801).AsPoint();
            mapControl.Navigator.NavigateTo(hrenb,7);//6/47.983,34.321 /
            
           

            MapContainerGrid.Children.Clear();
            MapContainerGrid.Children.Add(mapControl);
            
        }

        protected ILayer CreateLineStringLayer(String name, List<Point> geoWaypoints)
        {
            var lineString = new LineString();

            List<Feature> featureList = new List<Feature>();

            IStyle pointStyle = new SymbolStyle()
            {
                SymbolScale = 0.30,
                Fill = null//new Brush(Mapsui.Styles.Color.FromString("Red"))
            };

            foreach (var wp in geoWaypoints)
            {
                var point = SphericalMercator.FromLonLat(wp.X, wp.Y);
                lineString.Vertices.Add(point);

                var p2 = SphericalMercator.FromLonLat(wp.X, wp.Y);
                var pointFeature = new Feature();
                pointFeature.Geometry = p2;
                pointFeature.Styles.Add(pointStyle);
                featureList.Add(pointFeature);
            }


            IStyle linestringStyle = new VectorStyle()
            {
                Fill = null,
                Outline = null,
                Line = { Color = Mapsui.Styles.Color.FromString("Blue"), Width = 4 }
            };

            Feature lineStringFeature = new Feature()
            {
                Geometry = lineString
            };
            lineStringFeature.Styles.Add(linestringStyle);

            featureList.Add(lineStringFeature);

            MemoryProvider memoryProvider = new MemoryProvider(featureList);

            return new MemoryLayer
            {
                DataSource = memoryProvider,
                Name = name
            };
        }

    }
}
