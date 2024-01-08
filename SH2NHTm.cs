using Memory.Win64;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Silent_Hill_2_NHT_minimalist
{
    public partial class SH2NHTm : Form
    {
        public SH2NHTm()
        {
            InitializeComponent();
        }

        ulong hpAddr;
        ulong IGTAddr;
        ulong damageAddr;
        ulong savesAddr;
        ulong enemiesshootingAddr;
        ulong enemiesfightingAddr;
        ulong itemsAddr;
        ulong actionlevelAddr;
        ulong riddlelevelAddr;
        ulong boatAddr;
        ulong FPSAddr;
        ulong handgunAddr;
        ulong shotgunAddr;
        ulong rifleAddr;
        MemoryHelper64 helper;

        private void SH2NHTm_Load(object sender, EventArgs e)
        {
            Process p = Process.GetProcessesByName("sh2pc").FirstOrDefault();

            if (p == null) return;

            helper = new MemoryHelper64(p);

            hpAddr = helper.GetBaseAddress(0x1BB113C);
            IGTAddr = helper.GetBaseAddress(0x19BBF94);
            damageAddr = helper.GetBaseAddress(0x19BBFA8);
            savesAddr = helper.GetBaseAddress(0x19BBF8A);
            enemiesshootingAddr = helper.GetBaseAddress(0x19BBF90);
            enemiesfightingAddr = helper.GetBaseAddress(0x19BBF92);
            itemsAddr = helper.GetBaseAddress(0x19BBF8E);
            actionlevelAddr = helper.GetBaseAddress(0x19BBFF4);
            riddlelevelAddr = helper.GetBaseAddress(0x19BBFF5);
            boatAddr = helper.GetBaseAddress(0x19BBFA0);
            FPSAddr = helper.GetBaseAddress(0x633364);
            handgunAddr = helper.GetBaseAddress(0x1B7A7F4);
            shotgunAddr = helper.GetBaseAddress(0x1B7A7F8);
            rifleAddr = helper.GetBaseAddress(0x1B7A7FC);

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HP.Text = helper.ReadMemory<float>(hpAddr).ToString("F0");
            
            float totalTime = helper.ReadMemory<float>(IGTAddr);
            TimeSpan time = TimeSpan.FromSeconds(totalTime);
            IGT.Text = time.ToString("hh' : 'mm' : 'ss");
            
            damagetaken.Text = helper.ReadMemory<float>(damageAddr).ToString("F0");
            saves.Text = helper.ReadMemory<byte>(savesAddr).ToString();
            enemiesshooting.Text = helper.ReadMemory<short>(enemiesshootingAddr).ToString();
            enemiesmelee.Text = helper.ReadMemory<short>(enemiesfightingAddr).ToString();
            items.Text = helper.ReadMemory<short>(itemsAddr).ToString();

            byte actionLevelValue = helper.ReadMemory<byte>(actionlevelAddr);

            string actionLevelText;

            switch (actionLevelValue)
            {
                case 0:
                    actionLevelText = "Beginner";
                    break;
                case 1:
                    actionLevelText = "Easy";
                    break;
                case 2:
                    actionLevelText = "Normal";
                    break;
                case 3:
                    actionLevelText = "Hard";
                    break;
                default:
                    actionLevelText = "Unknown"; // Add a default case if needed
                    break;
            }
            actionlevel.Text = actionLevelText;


            byte riddleLevelValue = helper.ReadMemory<byte>(riddlelevelAddr);

            string riddleLevelText;

            switch (riddleLevelValue)
            {
                case 0:
                    riddleLevelText = "Easy";
                    break;
                case 1:
                    riddleLevelText = "Normal";
                    break;
                case 2:
                    riddleLevelText = "Hard";
                    break;
                default:
                    riddleLevelText = "Unknown"; // Add a default case if needed
                    break;
            }
            riddlelevel.Text = riddleLevelText;

            float phTimer = helper.ReadMemory<float>(boatAddr);
            TimeSpan timer = TimeSpan.FromSeconds(phTimer);
            boattime.Text = $"{(int)timer.TotalMinutes:D2}:{timer.Seconds:D2}";

            FPS.Text = helper.ReadMemory<float>(FPSAddr).ToString("F0");
            handgun.Text = helper.ReadMemory<short>(handgunAddr).ToString();
            shotgun.Text = helper.ReadMemory<short>(shotgunAddr).ToString();
            rifle.Text = helper.ReadMemory<short>(rifleAddr).ToString();
        }
    }
}
