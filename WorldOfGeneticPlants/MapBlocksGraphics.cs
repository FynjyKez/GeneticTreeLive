using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using WorldOfGeneticPlants.EnumData;
using System.Globalization;

namespace WorldOfGeneticPlants
{
    public static class MapBlocksGraphics
    {
        private const int ReverseBitmapHeigth = BitmapHeigth - 1;
        private const int CameraBaseRadius = 25;
        private const int CameraBaseDiameter = 2 * CameraBaseRadius;
        private const int MaxCameraZoom = 16;
        private const int BitmapWidth = MaxCameraZoom * CameraBaseDiameter;
        private const int BitmapHeigth = MaxCameraZoom * CameraBaseDiameter;

        private static Point CameraCenterCoordinate = new Point(MainWorld.WorldWidth / 2, MainWorld.WorldHeight / 2);
        private static int CameraWidth;
        private static int CameraHeigth;

        public enum CameraZoom
        {
            x16 = 1,
            x8 = 2,
            x4 = 4,
            x2 = 8,
            x1 = 16
        }
        public enum CameraMode
        {
            Standart,
            Enerdy,
        }
        private static CameraZoom CameraBaseZoom = (CameraZoom)Properties.BaseWorld.Default.Camera_Zoom;
        private static CameraMode CameraBaseMode = CameraMode.Standart;

        private static Bitmap BlocksBitmap;
        private static Graphics graphics;
        private static Brush[] brushes;
        private static Brush CustomBrush1;
        private static Point[] CustomPoints;

        /// <summary>
        /// Конструктор изображения симуляции
        /// </summary>
        static MapBlocksGraphics()
        {
            
            BlocksBitmap = new Bitmap(BitmapWidth, BitmapHeigth);
            graphics = Graphics.FromImage(BlocksBitmap);
            brushes = new Brush[10];
            /*for(int i = 0; i < 10; i++)
            {
                brushes[i].
            }*/
            //
            DrawingBorder(BitmapHeigth - 1, 0, 0, BitmapWidth - 1, Color.Red);
            //
            Camera();
            Drawing();


        }
        
        private static void Camera()
        {
            CameraWidth = CameraBaseDiameter * (int)CameraBaseZoom;
            CameraHeigth = CameraBaseDiameter * (int)CameraBaseZoom;
        }

        private static void Camera(CameraMode Mode)
        {

        }
        private static void Camera(CameraZoom Zoom)
        {
            CameraBaseZoom = Zoom;
            Camera();
        }
        private static void CameraMove()
        {

        }

        /// <summary>
        /// Отрисовать блоки на карте
        /// </summary>
        private static void Drawing()
        {
            for(int X = 0; X < CameraWidth; X++)
            {
                for(int Y = 0; Y < CameraHeigth; Y++)
                {
                    DrawingBlock(X, Y, CameraCenterCoordinate.X - CameraWidth / 2 + X, CameraCenterCoordinate.Y - CameraHeigth / 2 + Y);
                }
            }
        }

        /// <summary>
        /// Нарисовать границу
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="Bottom"></param>
        /// <param name="Left"></param>
        /// <param name="Right"></param>
        /// <param name="color"></param>
        private static void DrawingBorder(int Top, int Bottom, int Left, int Right, Color color)
        {
            for (int i = Left; i < Right; i++)
            {
                BlocksBitmap.SetPixel(i, ReverseBitmapHeigth - Bottom, color);
                BlocksBitmap.SetPixel(i, ReverseBitmapHeigth - Top, color);
            }
            for (int i = Bottom; i < Top; i++)
            {
                BlocksBitmap.SetPixel(Left, ReverseBitmapHeigth - i, color);
                BlocksBitmap.SetPixel(Right, ReverseBitmapHeigth - i, color);
            }
        }

        /// <summary>
        /// Нарисовать блок на карту
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="MapX"></param>
        /// <param name="MapY"></param>
        private static void DrawingBlock(int X, int Y, int MapX, int MapY)
        {
            //Это графический параметр, он должен нахолится в граффикс и максимум дублироваться по данным из ворда
            //надо посмотреть как оптимизировать
            if (MainWorld.GetPointChangeMap(MapX, MapY))
            {
                TypeBlocks TypeBlock = MainWorld.GetTypeBlockMap(MapX, MapY);
                Color BlockColor = ColorsOfBlocks.ValueDictionary[TypeBlock];
                //Можно реализовать подгрузку всех необходимых кистей заранее
                CustomBrush1 = new SolidBrush(BlockColor);
                //
                int Modifier = MaxCameraZoom / (int)CameraBaseZoom,
                    Left = Modifier * X,
                    Right = Modifier * (X + 1),
                    Top = ReverseBitmapHeigth - Modifier * (Y + 1),
                    Bottom = ReverseBitmapHeigth - Modifier * Y;
                CustomPoints = new Point[4];
                CustomPoints[0] = new Point(Left, Bottom);
                CustomPoints[1] = new Point(Right, Bottom);
                CustomPoints[2] = new Point(Right, Top);
                CustomPoints[3] = new Point(Left, Top);


                graphics.FillPolygon(CustomBrush1, CustomPoints);

            }
        }

        /// <summary>
        /// Получить изображение симуляции
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetBlocksBitmap()
        {
            return BlocksBitmap;
        }
        /// <summary>
        /// Получить изображение симуляции после отрисовки
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetNewBlocksBitmap()
        {
            Drawing();
            return BlocksBitmap;
        }
    }

}
