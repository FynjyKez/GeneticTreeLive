using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.PrivateImplementationDetails;

namespace TreeLive
{
    public partial class FormApp : Form
    {

        private static bool ZoobEnable = false;

        private static int Tick = 0;
        private static int EndTick = 10000;

        private static bool Visualised = true;

        
        /// <summary>
        /// Конструктор окна приложения
        /// </summary>
        public FormApp()
        {
            InitializeComponent();
            pictureBoxWorld.Image = WorldOfGeneticPlants.MapBlocksGraphics.GetBlocksBitmap();
            
            WorldTimer.Enabled = true;
            //World.World.StartThread();
            /**ThisWorldMap = new data.objects.WorldMap(
                WordHeigth,
                WordWidth
                );
            ThisWorldMap.Visualised = Visualised;

            if (Visualised)
            {
                pictureBoxWorld.Image = ThisWorldMap.GetBitmapWorld();
            }*/
        }

        /// <summary>
        /// Функция-Таймер запускающая расчеты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorldTimer_Tick(object sender, EventArgs e)
        {
            //Выполнить одну итерацию симуляции
            WorldOfGeneticPlants.MainWorld.GoOneWorldTick();
            //Получить изображение симуляции
            pictureBoxWorld.Image = WorldOfGeneticPlants.MapBlocksGraphics.GetNewBlocksBitmap();

            Tick++;
            this.label1.Text = Convert.ToString(Tick);



            /**if (Visualised)
            {
                ThisWorldMap.ReloadWorldMap(int.Parse(TestString));
                pictureBoxWorld.Image = ThisWorldMap.GetBitmapWorld();
                pictureBoxWorld.Refresh();
            }

            

            if (ThisWorldMap.ToBeBest)
            {
                ThisWorldMap.CutBestTree();
                if (Visualised)
                {
                    pictureBoxWorldBest.Image = ThisWorldMap.BestTree;
                }
            }*/

            this.GetStatistics();


            //Прекратить симуляцию по истечению таймера
            if (Tick > EndTick)
            {
                //World.World.AbortThread();
                WorldTimer.Enabled = false;
            }
        }

        /// <summary>
        /// Получить и вывести статистику
        /// </summary>
        private void GetStatistics()
        {
            //label2.Text = ThisWorldMap.TextTree();

            label7.Text = WorldOfGeneticPlants.Statistics.GetGenomeProgram(0);
            /*label3.Text = ThisWorldMap.TextGeneTree();

            label5.Text = Convert.ToString(ThisWorldMap.GetActualTreeEnergy());

            labelGenerationNow.Text = Convert.ToString(ThisWorldMap.GetGeneration());

            if (ThisWorldMap.BestGenes != null)
            {
                label7.Text = ThisWorldMap.BestGenes.GetStringGenes();
                label9.Text = ThisWorldMap.BestGenes.GetWorkIndex();
                
            }

            if (ThisWorldMap.ToBeBest) 
            {
                labelGenerationBest.Text = Convert.ToString(ThisWorldMap.GetGeneration());
            }

            label8.Text = Convert.ToString(ThisWorldMap.BestEnergy);*/
        }


       /* public int ThisWorldMapPlantStatus(int X, int Y)
        {
            //return ThisWorldMap.WorldPlantStatus(X, Y);
        }*/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           WorldTimer.Interval = Convert.ToInt32(comboBox1.SelectedItem);
        }

        private void buttonZoom_Click(object sender, EventArgs e)
        {
            if (ZoobEnable == false)
            {
                pictureBoxWorld.Height = 1100;
                pictureBoxWorld.Width = 1100;
                ZoobEnable = true;
            }
            else
            {
                pictureBoxWorld.Height = 600;
                pictureBoxWorld.Width = 600;
                ZoobEnable = false;
            }
        }
    }
}
