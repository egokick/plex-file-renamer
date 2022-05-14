using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using plex_file_renamerv2.Models;
using System.Text.RegularExpressions;

namespace plex_file_renamerv2
{
    public partial class Form1 : Form
    {
        private string FlexFolderPath;
        private List<string> FilePaths;
        List<FilePreview> FileRenames;
        RenamerConfig Config;

        public Form1()
        {
            InitializeComponent();

            if (File.Exists("config.json"))
            {
                var configJson = File.ReadAllText("config.json");
                Config = JsonConvert.DeserializeObject<RenamerConfig>(configJson);
            }
            else
            {
                Config = new RenamerConfig();
            }

            if (!string.IsNullOrEmpty(Config.FolderPath))
            {
                FlexFolderPath = Config.FolderPath;
                lblFlexFolder.Text = Config.FolderPath;
            }
            
            // todo: Only show files created/added n days ago or less

            // todo: Colour code input fields when text is entered

            // todo: Show in UI example file name changing at each stage

            // todo: Save config by folder

            // todo: UI select file types to limit replacement to 

            // todo: Add new replacements through UI
            if (!string.IsNullOrEmpty(Config.TextToRemove1)) txtRemove1.Text = Config.TextToRemove1;            
            if (!string.IsNullOrEmpty(Config.TextToRemove2)) txtRemove2.Text = Config.TextToRemove2;
            if (!string.IsNullOrEmpty(Config.TextToRemove3)) txtRemove3.Text = Config.TextToRemove3;            
            if (!string.IsNullOrEmpty(Config.TextToRemove4)) txtRemove4.Text = Config.TextToRemove4;
            if (!string.IsNullOrEmpty(Config.TextToRemove5)) txtRemove5.Text = Config.TextToRemove5;
            if (!string.IsNullOrEmpty(Config.TextToRemove6)) txtRemove6.Text = Config.TextToRemove6;
            if (!string.IsNullOrEmpty(Config.TextToRemove7)) txtRemove7.Text = Config.TextToRemove7;
            if (!string.IsNullOrEmpty(Config.TextToRemove8)) txtRemove8.Text = Config.TextToRemove8;
            if (!string.IsNullOrEmpty(Config.TextToRemove9)) txtRemove9.Text = Config.TextToRemove9;
            if (!string.IsNullOrEmpty(Config.TextToRemove10)) txtRemove10.Text = Config.TextToRemove10;
            if (!string.IsNullOrEmpty(Config.TextToRemove11)) txtRemove11.Text = Config.TextToRemove11;
            if (!string.IsNullOrEmpty(Config.TextToRemove12)) txtRemove12.Text = Config.TextToRemove12;            
            if (!string.IsNullOrEmpty(Config.TextToReplace1)) txtReplace1.Text = Config.TextToReplace1;
            if (!string.IsNullOrEmpty(Config.TextToReplace2)) txtReplace2.Text = Config.TextToReplace2;
            if (!string.IsNullOrEmpty(Config.TextToReplace3)) txtReplace3.Text = Config.TextToReplace3;
            if (!string.IsNullOrEmpty(Config.TextToReplace4)) txtReplace4.Text = Config.TextToReplace4;
            if (!string.IsNullOrEmpty(Config.TextToReplace5)) txtReplace5.Text = Config.TextToReplace5;
            if (!string.IsNullOrEmpty(Config.TextToReplace6)) txtReplace6.Text = Config.TextToReplace6;
            if (!string.IsNullOrEmpty(Config.TextToReplace7)) txtReplace7.Text = Config.TextToReplace7;
            if (!string.IsNullOrEmpty(Config.TextToReplace8)) txtReplace8.Text = Config.TextToReplace8;
            if (!string.IsNullOrEmpty(Config.TextToReplace9)) txtReplace9.Text = Config.TextToReplace9;
            if (!string.IsNullOrEmpty(Config.TextToReplace10)) txtReplace10.Text = Config.TextToReplace10;
            if (!string.IsNullOrEmpty(Config.TextToReplace11)) txtReplace11.Text = Config.TextToReplace11;
            if (!string.IsNullOrEmpty(Config.TextToReplace12)) txtReplace12.Text = Config.TextToReplace12;

            if (Config.IsRegex1) chkRegex1.Checked = Config.IsRegex1;
            if (Config.IsRegex2) chkRegex2.Checked = Config.IsRegex2;
            if (Config.IsRegex3) chkRegex3.Checked = Config.IsRegex3;
            if (Config.IsRegex4) chkRegex4.Checked = Config.IsRegex4;
            if (Config.IsRegex5) chkRegex5.Checked = Config.IsRegex5;
            if (Config.IsRegex6) chkRegex6.Checked = Config.IsRegex6;
            if (Config.IsRegex7) chkRegex7.Checked = Config.IsRegex7;
            if (Config.IsRegex8) chkRegex8.Checked = Config.IsRegex8;
            if (Config.IsRegex9) chkRegex9.Checked = Config.IsRegex9;
            if (Config.IsRegex10) chkRegex10.Checked = Config.IsRegex10;
            if (Config.IsRegex11) chkRegex11.Checked = Config.IsRegex11;
            if (Config.IsRegex12) chkRegex12.Checked = Config.IsRegex12;

            if (Config.Capitalize1) chkCapital1.Checked = Config.Capitalize1;
            if (Config.Capitalize2) chkCapital2.Checked = Config.Capitalize2;
            if (Config.Capitalize3) chkCapital3.Checked = Config.Capitalize3;
            if (Config.Capitalize4) chkCapital4.Checked = Config.Capitalize4;
            if (Config.Capitalize5) chkCapital5.Checked = Config.Capitalize5;
            if (Config.Capitalize6) chkCapital6.Checked = Config.Capitalize6;
            if (Config.Capitalize7) chkCapital7.Checked = Config.Capitalize7;
            if (Config.Capitalize8) chkCapital8.Checked = Config.Capitalize8;
            if (Config.Capitalize9) chkCapital9.Checked = Config.Capitalize9;
            if (Config.Capitalize10) chkCapital10.Checked = Config.Capitalize10;
            if (Config.Capitalize11) chkCapital11.Checked = Config.Capitalize11;
            if (Config.Capitalize12) chkCapital12.Checked = Config.Capitalize12;

            if (Config.LowerCase1) chkLower1.Checked = Config.LowerCase1;
            if (Config.LowerCase2) chkLower2.Checked = Config.LowerCase2;
            if (Config.LowerCase3) chkLower3.Checked = Config.LowerCase3;
            if (Config.LowerCase4) chkLower4.Checked = Config.LowerCase4;
            if (Config.LowerCase5) chkLower5.Checked = Config.LowerCase5;
            if (Config.LowerCase6) chkLower6.Checked = Config.LowerCase6;
            if (Config.LowerCase7) chkLower7.Checked = Config.LowerCase7;
            if (Config.LowerCase8) chkLower8.Checked = Config.LowerCase8;
            if (Config.LowerCase9) chkLower9_2.Checked = Config.LowerCase9;
            if (Config.LowerCase10) chkLower10.Checked = Config.LowerCase10;
            if (Config.LowerCase11) chkLower11.Checked = Config.LowerCase11;
            if (Config.LowerCase12) chkLower12.Checked = Config.LowerCase12;            
        }

        private void InitializeComponent()
        {
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblFlexFolder = new System.Windows.Forms.Label();
            this.btnRenameFiles = new System.Windows.Forms.Button();
            this.txtRemove1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRemove2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRemove3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRemove4 = new System.Windows.Forms.TextBox();
            this.btnShowPreview = new System.Windows.Forms.Button();
            this.chkLower9 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReplace1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReplace4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReplace3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReplace2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtReplace5 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRemove5 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtReplace6 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRemove6 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtReplace7 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRemove7 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtReplace8 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRemove8 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtReplace9 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtRemove9 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtReplace10 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtRemove10 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtReplace11 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtRemove11 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtReplace12 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtRemove12 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.chkRegex1 = new System.Windows.Forms.CheckBox();
            this.chkRegex2 = new System.Windows.Forms.CheckBox();
            this.chkRegex3 = new System.Windows.Forms.CheckBox();
            this.chkRegex4 = new System.Windows.Forms.CheckBox();
            this.chkRegex8 = new System.Windows.Forms.CheckBox();
            this.chkRegex7 = new System.Windows.Forms.CheckBox();
            this.chkRegex6 = new System.Windows.Forms.CheckBox();
            this.chkRegex5 = new System.Windows.Forms.CheckBox();
            this.chkRegex12 = new System.Windows.Forms.CheckBox();
            this.chkRegex11 = new System.Windows.Forms.CheckBox();
            this.chkRegex10 = new System.Windows.Forms.CheckBox();
            this.chkRegex9 = new System.Windows.Forms.CheckBox();
            this.chkCapital1 = new System.Windows.Forms.CheckBox();
            this.chkCapital2 = new System.Windows.Forms.CheckBox();
            this.chkCapital3 = new System.Windows.Forms.CheckBox();
            this.chkCapital6 = new System.Windows.Forms.CheckBox();
            this.chkCapital5 = new System.Windows.Forms.CheckBox();
            this.chkCapital4 = new System.Windows.Forms.CheckBox();
            this.chkCapital12 = new System.Windows.Forms.CheckBox();
            this.chkCapital11 = new System.Windows.Forms.CheckBox();
            this.chkCapital10 = new System.Windows.Forms.CheckBox();
            this.chkCapital9 = new System.Windows.Forms.CheckBox();
            this.chkCapital8 = new System.Windows.Forms.CheckBox();
            this.chkCapital7 = new System.Windows.Forms.CheckBox();
            this.chkLower12 = new System.Windows.Forms.CheckBox();
            this.chkLower11 = new System.Windows.Forms.CheckBox();
            this.chkLower10 = new System.Windows.Forms.CheckBox();
            this.chkLower9_2 = new System.Windows.Forms.CheckBox();
            this.chkLower8 = new System.Windows.Forms.CheckBox();
            this.chkLower7 = new System.Windows.Forms.CheckBox();
            this.chkLower6 = new System.Windows.Forms.CheckBox();
            this.chkLower5 = new System.Windows.Forms.CheckBox();
            this.chkLower4 = new System.Windows.Forms.CheckBox();
            this.chkLower3 = new System.Windows.Forms.CheckBox();
            this.chkLower2 = new System.Windows.Forms.CheckBox();
            this.chkLower1 = new System.Windows.Forms.CheckBox();
            this.lblLogInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(12, 22);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(144, 61);
            this.btnSelectFolder.TabIndex = 0;
            this.btnSelectFolder.Text = "Select Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.button_SelectFolder_Click);
            // 
            // lblFlexFolder
            // 
            this.lblFlexFolder.AutoSize = true;
            this.lblFlexFolder.Location = new System.Drawing.Point(179, 46);
            this.lblFlexFolder.Name = "lblFlexFolder";
            this.lblFlexFolder.Size = new System.Drawing.Size(0, 13);
            this.lblFlexFolder.TabIndex = 1;
            // 
            // btnRenameFiles
            // 
            this.btnRenameFiles.Location = new System.Drawing.Point(16, 996);
            this.btnRenameFiles.Name = "btnRenameFiles";
            this.btnRenameFiles.Size = new System.Drawing.Size(144, 67);
            this.btnRenameFiles.TabIndex = 2;
            this.btnRenameFiles.Text = "Rename Files";
            this.btnRenameFiles.UseVisualStyleBackColor = true;
            this.btnRenameFiles.Click += new System.EventHandler(this.btnRenameFiles_Click);
            // 
            // txtRemove1
            // 
            this.txtRemove1.Location = new System.Drawing.Point(33, 122);
            this.txtRemove1.Name = "txtRemove1";
            this.txtRemove1.Size = new System.Drawing.Size(306, 20);
            this.txtRemove1.TabIndex = 4;
            this.txtRemove1.TextChanged += new System.EventHandler(this.txtRemove1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "2";
            // 
            // txtRemove2
            // 
            this.txtRemove2.Location = new System.Drawing.Point(33, 145);
            this.txtRemove2.Name = "txtRemove2";
            this.txtRemove2.Size = new System.Drawing.Size(306, 20);
            this.txtRemove2.TabIndex = 6;
            this.txtRemove2.TextChanged += new System.EventHandler(this.txtRemove2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "3";
            // 
            // txtRemove3
            // 
            this.txtRemove3.Location = new System.Drawing.Point(33, 168);
            this.txtRemove3.Name = "txtRemove3";
            this.txtRemove3.Size = new System.Drawing.Size(306, 20);
            this.txtRemove3.TabIndex = 8;
            this.txtRemove3.TextChanged += new System.EventHandler(this.txtRemove3_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "4";
            // 
            // txtRemove4
            // 
            this.txtRemove4.Location = new System.Drawing.Point(33, 191);
            this.txtRemove4.Name = "txtRemove4";
            this.txtRemove4.Size = new System.Drawing.Size(306, 20);
            this.txtRemove4.TabIndex = 10;
            this.txtRemove4.TextChanged += new System.EventHandler(this.txtRemove4_TextChanged);
            // 
            // btnShowPreview
            // 
            this.btnShowPreview.Location = new System.Drawing.Point(12, 479);
            this.btnShowPreview.Name = "btnShowPreview";
            this.btnShowPreview.Size = new System.Drawing.Size(144, 67);
            this.btnShowPreview.TabIndex = 12;
            this.btnShowPreview.Text = "Show Preview";
            this.btnShowPreview.UseVisualStyleBackColor = true;
            this.btnShowPreview.Click += new System.EventHandler(this.btnShowPreview_Click);
            // 
            // chkLower9
            // 
            this.chkLower9.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLower9.Location = new System.Drawing.Point(200, 479);
            this.chkLower9.Multiline = true;
            this.chkLower9.Name = "chkLower9";
            this.chkLower9.Size = new System.Drawing.Size(1285, 511);
            this.chkLower9.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(354, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "1";
            // 
            // txtReplace1
            // 
            this.txtReplace1.Location = new System.Drawing.Point(373, 124);
            this.txtReplace1.Name = "txtReplace1";
            this.txtReplace1.Size = new System.Drawing.Size(306, 20);
            this.txtReplace1.TabIndex = 15;
            this.txtReplace1.TextChanged += new System.EventHandler(this.txtReplace1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(354, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "4";
            // 
            // txtReplace4
            // 
            this.txtReplace4.Location = new System.Drawing.Point(373, 193);
            this.txtReplace4.Name = "txtReplace4";
            this.txtReplace4.Size = new System.Drawing.Size(306, 20);
            this.txtReplace4.TabIndex = 20;
            this.txtReplace4.TextChanged += new System.EventHandler(this.txtReplace4_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(354, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "3";
            // 
            // txtReplace3
            // 
            this.txtReplace3.Location = new System.Drawing.Point(373, 170);
            this.txtReplace3.Name = "txtReplace3";
            this.txtReplace3.Size = new System.Drawing.Size(306, 20);
            this.txtReplace3.TabIndex = 18;
            this.txtReplace3.TextChanged += new System.EventHandler(this.txtReplace3_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(354, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "2";
            // 
            // txtReplace2
            // 
            this.txtReplace2.Location = new System.Drawing.Point(373, 147);
            this.txtReplace2.Name = "txtReplace2";
            this.txtReplace2.Size = new System.Drawing.Size(306, 20);
            this.txtReplace2.TabIndex = 16;
            this.txtReplace2.TextChanged += new System.EventHandler(this.txtReplace2_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(354, 218);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "5";
            // 
            // txtReplace5
            // 
            this.txtReplace5.Location = new System.Drawing.Point(373, 216);
            this.txtReplace5.Name = "txtReplace5";
            this.txtReplace5.Size = new System.Drawing.Size(306, 20);
            this.txtReplace5.TabIndex = 24;
            this.txtReplace5.TextChanged += new System.EventHandler(this.txtReplace5_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 218);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "5";
            // 
            // txtRemove5
            // 
            this.txtRemove5.Location = new System.Drawing.Point(33, 214);
            this.txtRemove5.Name = "txtRemove5";
            this.txtRemove5.Size = new System.Drawing.Size(306, 20);
            this.txtRemove5.TabIndex = 22;
            this.txtRemove5.TextChanged += new System.EventHandler(this.txtRemove5_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(354, 241);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "6";
            // 
            // txtReplace6
            // 
            this.txtReplace6.Location = new System.Drawing.Point(373, 239);
            this.txtReplace6.Name = "txtReplace6";
            this.txtReplace6.Size = new System.Drawing.Size(306, 20);
            this.txtReplace6.TabIndex = 28;
            this.txtReplace6.TextChanged += new System.EventHandler(this.txtReplace6_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 241);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "6";
            // 
            // txtRemove6
            // 
            this.txtRemove6.Location = new System.Drawing.Point(33, 237);
            this.txtRemove6.Name = "txtRemove6";
            this.txtRemove6.Size = new System.Drawing.Size(306, 20);
            this.txtRemove6.TabIndex = 26;
            this.txtRemove6.TextChanged += new System.EventHandler(this.txtRemove6_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(354, 264);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "7";
            // 
            // txtReplace7
            // 
            this.txtReplace7.Location = new System.Drawing.Point(373, 262);
            this.txtReplace7.Name = "txtReplace7";
            this.txtReplace7.Size = new System.Drawing.Size(306, 20);
            this.txtReplace7.TabIndex = 32;
            this.txtReplace7.TextChanged += new System.EventHandler(this.txtReplace7_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 264);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(13, 13);
            this.label14.TabIndex = 31;
            this.label14.Text = "7";
            // 
            // txtRemove7
            // 
            this.txtRemove7.Location = new System.Drawing.Point(33, 260);
            this.txtRemove7.Name = "txtRemove7";
            this.txtRemove7.Size = new System.Drawing.Size(306, 20);
            this.txtRemove7.TabIndex = 30;
            this.txtRemove7.TextChanged += new System.EventHandler(this.txtRemove7_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(354, 287);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(13, 13);
            this.label15.TabIndex = 37;
            this.label15.Text = "8";
            // 
            // txtReplace8
            // 
            this.txtReplace8.Location = new System.Drawing.Point(373, 285);
            this.txtReplace8.Name = "txtReplace8";
            this.txtReplace8.Size = new System.Drawing.Size(306, 20);
            this.txtReplace8.TabIndex = 36;
            this.txtReplace8.TextChanged += new System.EventHandler(this.txtReplace8_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 287);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 13);
            this.label16.TabIndex = 35;
            this.label16.Text = "8";
            // 
            // txtRemove8
            // 
            this.txtRemove8.Location = new System.Drawing.Point(33, 283);
            this.txtRemove8.Name = "txtRemove8";
            this.txtRemove8.Size = new System.Drawing.Size(306, 20);
            this.txtRemove8.TabIndex = 34;
            this.txtRemove8.TextChanged += new System.EventHandler(this.txtRemove8_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(354, 309);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(13, 13);
            this.label17.TabIndex = 41;
            this.label17.Text = "9";
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // txtReplace9
            // 
            this.txtReplace9.Location = new System.Drawing.Point(373, 307);
            this.txtReplace9.Name = "txtReplace9";
            this.txtReplace9.Size = new System.Drawing.Size(306, 20);
            this.txtReplace9.TabIndex = 40;
            this.txtReplace9.TextChanged += new System.EventHandler(this.txtReplace9_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 309);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 13);
            this.label18.TabIndex = 39;
            this.label18.Text = "9";
            // 
            // txtRemove9
            // 
            this.txtRemove9.Location = new System.Drawing.Point(33, 305);
            this.txtRemove9.Name = "txtRemove9";
            this.txtRemove9.Size = new System.Drawing.Size(306, 20);
            this.txtRemove9.TabIndex = 38;
            this.txtRemove9.TextChanged += new System.EventHandler(this.txtRemove9_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(354, 333);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(19, 13);
            this.label19.TabIndex = 45;
            this.label19.Text = "10";
            // 
            // txtReplace10
            // 
            this.txtReplace10.Location = new System.Drawing.Point(373, 331);
            this.txtReplace10.Name = "txtReplace10";
            this.txtReplace10.Size = new System.Drawing.Size(306, 20);
            this.txtReplace10.TabIndex = 44;
            this.txtReplace10.TextChanged += new System.EventHandler(this.txtReplace10_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 333);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 13);
            this.label20.TabIndex = 43;
            this.label20.Text = "10";
            // 
            // txtRemove10
            // 
            this.txtRemove10.Location = new System.Drawing.Point(33, 329);
            this.txtRemove10.Name = "txtRemove10";
            this.txtRemove10.Size = new System.Drawing.Size(306, 20);
            this.txtRemove10.TabIndex = 42;
            this.txtRemove10.TextChanged += new System.EventHandler(this.txtRemove10_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(354, 355);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(19, 13);
            this.label21.TabIndex = 49;
            this.label21.Text = "11";
            // 
            // txtReplace11
            // 
            this.txtReplace11.Location = new System.Drawing.Point(373, 353);
            this.txtReplace11.Name = "txtReplace11";
            this.txtReplace11.Size = new System.Drawing.Size(306, 20);
            this.txtReplace11.TabIndex = 48;
            this.txtReplace11.TextChanged += new System.EventHandler(this.txtReplace11_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 355);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(19, 13);
            this.label22.TabIndex = 47;
            this.label22.Text = "11";
            // 
            // txtRemove11
            // 
            this.txtRemove11.Location = new System.Drawing.Point(33, 351);
            this.txtRemove11.Name = "txtRemove11";
            this.txtRemove11.Size = new System.Drawing.Size(306, 20);
            this.txtRemove11.TabIndex = 46;
            this.txtRemove11.TextChanged += new System.EventHandler(this.txtRemove11_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(354, 377);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(19, 13);
            this.label23.TabIndex = 53;
            this.label23.Text = "12";
            // 
            // txtReplace12
            // 
            this.txtReplace12.Location = new System.Drawing.Point(373, 375);
            this.txtReplace12.Name = "txtReplace12";
            this.txtReplace12.Size = new System.Drawing.Size(306, 20);
            this.txtReplace12.TabIndex = 52;
            this.txtReplace12.TextChanged += new System.EventHandler(this.txtReplace12_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(12, 377);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(19, 13);
            this.label24.TabIndex = 51;
            this.label24.Text = "12";
            // 
            // txtRemove12
            // 
            this.txtRemove12.Location = new System.Drawing.Point(33, 373);
            this.txtRemove12.Name = "txtRemove12";
            this.txtRemove12.Size = new System.Drawing.Size(306, 20);
            this.txtRemove12.TabIndex = 50;
            this.txtRemove12.TextChanged += new System.EventHandler(this.txtRemove12_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(74, 95);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(165, 24);
            this.label25.TabIndex = 54;
            this.label25.Text = "Text To Remove";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(462, 93);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(134, 24);
            this.label26.TabIndex = 55;
            this.label26.Text = "Replace With";
            // 
            // chkRegex1
            // 
            this.chkRegex1.AutoSize = true;
            this.chkRegex1.Location = new System.Drawing.Point(685, 124);
            this.chkRegex1.Name = "chkRegex1";
            this.chkRegex1.Size = new System.Drawing.Size(57, 17);
            this.chkRegex1.TabIndex = 56;
            this.chkRegex1.Text = "Regex";
            this.chkRegex1.UseVisualStyleBackColor = true;
            this.chkRegex1.CheckedChanged += new System.EventHandler(this.chkRegex1_CheckedChanged);
            // 
            // chkRegex2
            // 
            this.chkRegex2.AutoSize = true;
            this.chkRegex2.Location = new System.Drawing.Point(685, 147);
            this.chkRegex2.Name = "chkRegex2";
            this.chkRegex2.Size = new System.Drawing.Size(57, 17);
            this.chkRegex2.TabIndex = 57;
            this.chkRegex2.Text = "Regex";
            this.chkRegex2.UseVisualStyleBackColor = true;
            this.chkRegex2.CheckedChanged += new System.EventHandler(this.chkRegex2_CheckedChanged);
            // 
            // chkRegex3
            // 
            this.chkRegex3.AutoSize = true;
            this.chkRegex3.Location = new System.Drawing.Point(685, 171);
            this.chkRegex3.Name = "chkRegex3";
            this.chkRegex3.Size = new System.Drawing.Size(57, 17);
            this.chkRegex3.TabIndex = 58;
            this.chkRegex3.Text = "Regex";
            this.chkRegex3.UseVisualStyleBackColor = true;
            this.chkRegex3.CheckedChanged += new System.EventHandler(this.chkRegex3_CheckedChanged);
            // 
            // chkRegex4
            // 
            this.chkRegex4.AutoSize = true;
            this.chkRegex4.Location = new System.Drawing.Point(685, 195);
            this.chkRegex4.Name = "chkRegex4";
            this.chkRegex4.Size = new System.Drawing.Size(57, 17);
            this.chkRegex4.TabIndex = 59;
            this.chkRegex4.Text = "Regex";
            this.chkRegex4.UseVisualStyleBackColor = true;
            this.chkRegex4.CheckedChanged += new System.EventHandler(this.chkRegex4_CheckedChanged);
            // 
            // chkRegex8
            // 
            this.chkRegex8.AutoSize = true;
            this.chkRegex8.Location = new System.Drawing.Point(685, 288);
            this.chkRegex8.Name = "chkRegex8";
            this.chkRegex8.Size = new System.Drawing.Size(57, 17);
            this.chkRegex8.TabIndex = 63;
            this.chkRegex8.Text = "Regex";
            this.chkRegex8.UseVisualStyleBackColor = true;
            this.chkRegex8.CheckedChanged += new System.EventHandler(this.chkRegex8_CheckedChanged);
            // 
            // chkRegex7
            // 
            this.chkRegex7.AutoSize = true;
            this.chkRegex7.Location = new System.Drawing.Point(685, 264);
            this.chkRegex7.Name = "chkRegex7";
            this.chkRegex7.Size = new System.Drawing.Size(57, 17);
            this.chkRegex7.TabIndex = 62;
            this.chkRegex7.Text = "Regex";
            this.chkRegex7.UseVisualStyleBackColor = true;
            this.chkRegex7.CheckedChanged += new System.EventHandler(this.chkRegex7_CheckedChanged);
            // 
            // chkRegex6
            // 
            this.chkRegex6.AutoSize = true;
            this.chkRegex6.Location = new System.Drawing.Point(685, 239);
            this.chkRegex6.Name = "chkRegex6";
            this.chkRegex6.Size = new System.Drawing.Size(57, 17);
            this.chkRegex6.TabIndex = 61;
            this.chkRegex6.Text = "Regex";
            this.chkRegex6.UseVisualStyleBackColor = true;
            this.chkRegex6.CheckedChanged += new System.EventHandler(this.chkRegex6_CheckedChanged);
            // 
            // chkRegex5
            // 
            this.chkRegex5.AutoSize = true;
            this.chkRegex5.Location = new System.Drawing.Point(685, 217);
            this.chkRegex5.Name = "chkRegex5";
            this.chkRegex5.Size = new System.Drawing.Size(57, 17);
            this.chkRegex5.TabIndex = 60;
            this.chkRegex5.Text = "Regex";
            this.chkRegex5.UseVisualStyleBackColor = true;
            this.chkRegex5.CheckedChanged += new System.EventHandler(this.chkRegex5_CheckedChanged);
            // 
            // chkRegex12
            // 
            this.chkRegex12.AutoSize = true;
            this.chkRegex12.Location = new System.Drawing.Point(685, 379);
            this.chkRegex12.Name = "chkRegex12";
            this.chkRegex12.Size = new System.Drawing.Size(57, 17);
            this.chkRegex12.TabIndex = 67;
            this.chkRegex12.Text = "Regex";
            this.chkRegex12.UseVisualStyleBackColor = true;
            this.chkRegex12.CheckedChanged += new System.EventHandler(this.chkRegex12_CheckedChanged);
            // 
            // chkRegex11
            // 
            this.chkRegex11.AutoSize = true;
            this.chkRegex11.Location = new System.Drawing.Point(685, 356);
            this.chkRegex11.Name = "chkRegex11";
            this.chkRegex11.Size = new System.Drawing.Size(57, 17);
            this.chkRegex11.TabIndex = 66;
            this.chkRegex11.Text = "Regex";
            this.chkRegex11.UseVisualStyleBackColor = true;
            this.chkRegex11.CheckedChanged += new System.EventHandler(this.chkRegex11_CheckedChanged);
            // 
            // chkRegex10
            // 
            this.chkRegex10.AutoSize = true;
            this.chkRegex10.Location = new System.Drawing.Point(685, 333);
            this.chkRegex10.Name = "chkRegex10";
            this.chkRegex10.Size = new System.Drawing.Size(57, 17);
            this.chkRegex10.TabIndex = 65;
            this.chkRegex10.Text = "Regex";
            this.chkRegex10.UseVisualStyleBackColor = true;
            this.chkRegex10.CheckedChanged += new System.EventHandler(this.chkRegex10_CheckedChanged);
            // 
            // chkRegex9
            // 
            this.chkRegex9.AutoSize = true;
            this.chkRegex9.Location = new System.Drawing.Point(685, 310);
            this.chkRegex9.Name = "chkRegex9";
            this.chkRegex9.Size = new System.Drawing.Size(57, 17);
            this.chkRegex9.TabIndex = 64;
            this.chkRegex9.Text = "Regex";
            this.chkRegex9.UseVisualStyleBackColor = true;
            this.chkRegex9.CheckedChanged += new System.EventHandler(this.chkRegex9_CheckedChanged);
            // 
            // chkCapital1
            // 
            this.chkCapital1.AutoSize = true;
            this.chkCapital1.Location = new System.Drawing.Point(748, 125);
            this.chkCapital1.Name = "chkCapital1";
            this.chkCapital1.Size = new System.Drawing.Size(71, 17);
            this.chkCapital1.TabIndex = 68;
            this.chkCapital1.Text = "Capitalize";
            this.chkCapital1.UseVisualStyleBackColor = true;
            this.chkCapital1.CheckedChanged += new System.EventHandler(this.chkCapital1_CheckedChanged);
            // 
            // chkCapital2
            // 
            this.chkCapital2.AutoSize = true;
            this.chkCapital2.Location = new System.Drawing.Point(748, 147);
            this.chkCapital2.Name = "chkCapital2";
            this.chkCapital2.Size = new System.Drawing.Size(71, 17);
            this.chkCapital2.TabIndex = 69;
            this.chkCapital2.Text = "Capitalize";
            this.chkCapital2.UseVisualStyleBackColor = true;
            this.chkCapital2.CheckedChanged += new System.EventHandler(this.chkCapital2_CheckedChanged);
            // 
            // chkCapital3
            // 
            this.chkCapital3.AutoSize = true;
            this.chkCapital3.Location = new System.Drawing.Point(748, 170);
            this.chkCapital3.Name = "chkCapital3";
            this.chkCapital3.Size = new System.Drawing.Size(71, 17);
            this.chkCapital3.TabIndex = 70;
            this.chkCapital3.Text = "Capitalize";
            this.chkCapital3.UseVisualStyleBackColor = true;
            this.chkCapital3.CheckedChanged += new System.EventHandler(this.chkCapital3_CheckedChanged);
            // 
            // chkCapital6
            // 
            this.chkCapital6.AutoSize = true;
            this.chkCapital6.Location = new System.Drawing.Point(748, 241);
            this.chkCapital6.Name = "chkCapital6";
            this.chkCapital6.Size = new System.Drawing.Size(71, 17);
            this.chkCapital6.TabIndex = 73;
            this.chkCapital6.Text = "Capitalize";
            this.chkCapital6.UseVisualStyleBackColor = true;
            this.chkCapital6.CheckedChanged += new System.EventHandler(this.chkCapital6_CheckedChanged);
            // 
            // chkCapital5
            // 
            this.chkCapital5.AutoSize = true;
            this.chkCapital5.Location = new System.Drawing.Point(748, 218);
            this.chkCapital5.Name = "chkCapital5";
            this.chkCapital5.Size = new System.Drawing.Size(71, 17);
            this.chkCapital5.TabIndex = 72;
            this.chkCapital5.Text = "Capitalize";
            this.chkCapital5.UseVisualStyleBackColor = true;
            this.chkCapital5.CheckedChanged += new System.EventHandler(this.chkCapital5_CheckedChanged);
            // 
            // chkCapital4
            // 
            this.chkCapital4.AutoSize = true;
            this.chkCapital4.Location = new System.Drawing.Point(748, 196);
            this.chkCapital4.Name = "chkCapital4";
            this.chkCapital4.Size = new System.Drawing.Size(71, 17);
            this.chkCapital4.TabIndex = 71;
            this.chkCapital4.Text = "Capitalize";
            this.chkCapital4.UseVisualStyleBackColor = true;
            this.chkCapital4.CheckedChanged += new System.EventHandler(this.chkCapital4_CheckedChanged);
            // 
            // chkCapital12
            // 
            this.chkCapital12.AutoSize = true;
            this.chkCapital12.Location = new System.Drawing.Point(748, 380);
            this.chkCapital12.Name = "chkCapital12";
            this.chkCapital12.Size = new System.Drawing.Size(71, 17);
            this.chkCapital12.TabIndex = 79;
            this.chkCapital12.Text = "Capitalize";
            this.chkCapital12.UseVisualStyleBackColor = true;
            this.chkCapital12.CheckedChanged += new System.EventHandler(this.chkCapital12_CheckedChanged);
            // 
            // chkCapital11
            // 
            this.chkCapital11.AutoSize = true;
            this.chkCapital11.Location = new System.Drawing.Point(748, 357);
            this.chkCapital11.Name = "chkCapital11";
            this.chkCapital11.Size = new System.Drawing.Size(71, 17);
            this.chkCapital11.TabIndex = 78;
            this.chkCapital11.Text = "Capitalize";
            this.chkCapital11.UseVisualStyleBackColor = true;
            this.chkCapital11.CheckedChanged += new System.EventHandler(this.chkCapital11_CheckedChanged);
            // 
            // chkCapital10
            // 
            this.chkCapital10.AutoSize = true;
            this.chkCapital10.Location = new System.Drawing.Point(748, 335);
            this.chkCapital10.Name = "chkCapital10";
            this.chkCapital10.Size = new System.Drawing.Size(71, 17);
            this.chkCapital10.TabIndex = 77;
            this.chkCapital10.Text = "Capitalize";
            this.chkCapital10.UseVisualStyleBackColor = true;
            this.chkCapital10.CheckedChanged += new System.EventHandler(this.chkCapital10_CheckedChanged);
            // 
            // chkCapital9
            // 
            this.chkCapital9.AutoSize = true;
            this.chkCapital9.Location = new System.Drawing.Point(748, 309);
            this.chkCapital9.Name = "chkCapital9";
            this.chkCapital9.Size = new System.Drawing.Size(71, 17);
            this.chkCapital9.TabIndex = 76;
            this.chkCapital9.Text = "Capitalize";
            this.chkCapital9.UseVisualStyleBackColor = true;
            this.chkCapital9.CheckedChanged += new System.EventHandler(this.chkCapital9_CheckedChanged);
            // 
            // chkCapital8
            // 
            this.chkCapital8.AutoSize = true;
            this.chkCapital8.Location = new System.Drawing.Point(748, 286);
            this.chkCapital8.Name = "chkCapital8";
            this.chkCapital8.Size = new System.Drawing.Size(71, 17);
            this.chkCapital8.TabIndex = 75;
            this.chkCapital8.Text = "Capitalize";
            this.chkCapital8.UseVisualStyleBackColor = true;
            this.chkCapital8.CheckedChanged += new System.EventHandler(this.chkCapital8_CheckedChanged);
            // 
            // chkCapital7
            // 
            this.chkCapital7.AutoSize = true;
            this.chkCapital7.Location = new System.Drawing.Point(748, 264);
            this.chkCapital7.Name = "chkCapital7";
            this.chkCapital7.Size = new System.Drawing.Size(71, 17);
            this.chkCapital7.TabIndex = 74;
            this.chkCapital7.Text = "Capitalize";
            this.chkCapital7.UseVisualStyleBackColor = true;
            this.chkCapital7.CheckedChanged += new System.EventHandler(this.chkCapital7_CheckedChanged);
            // 
            // chkLower12
            // 
            this.chkLower12.AutoSize = true;
            this.chkLower12.Location = new System.Drawing.Point(825, 381);
            this.chkLower12.Name = "chkLower12";
            this.chkLower12.Size = new System.Drawing.Size(79, 17);
            this.chkLower12.TabIndex = 91;
            this.chkLower12.Text = "LowerCase";
            this.chkLower12.UseVisualStyleBackColor = true;
            this.chkLower12.CheckedChanged += new System.EventHandler(this.chkLower12_CheckedChanged);
            // 
            // chkLower11
            // 
            this.chkLower11.AutoSize = true;
            this.chkLower11.Location = new System.Drawing.Point(825, 358);
            this.chkLower11.Name = "chkLower11";
            this.chkLower11.Size = new System.Drawing.Size(79, 17);
            this.chkLower11.TabIndex = 90;
            this.chkLower11.Text = "LowerCase";
            this.chkLower11.UseVisualStyleBackColor = true;
            this.chkLower11.CheckedChanged += new System.EventHandler(this.chkLower11_CheckedChanged);
            // 
            // chkLower10
            // 
            this.chkLower10.AutoSize = true;
            this.chkLower10.Location = new System.Drawing.Point(825, 336);
            this.chkLower10.Name = "chkLower10";
            this.chkLower10.Size = new System.Drawing.Size(79, 17);
            this.chkLower10.TabIndex = 89;
            this.chkLower10.Text = "LowerCase";
            this.chkLower10.UseVisualStyleBackColor = true;
            this.chkLower10.CheckedChanged += new System.EventHandler(this.chkLower10_CheckedChanged);
            // 
            // chkLower9_2
            // 
            this.chkLower9_2.AutoSize = true;
            this.chkLower9_2.Location = new System.Drawing.Point(825, 310);
            this.chkLower9_2.Name = "chkLower9_2";
            this.chkLower9_2.Size = new System.Drawing.Size(79, 17);
            this.chkLower9_2.TabIndex = 88;
            this.chkLower9_2.Text = "LowerCase";
            this.chkLower9_2.UseVisualStyleBackColor = true;
            this.chkLower9_2.CheckedChanged += new System.EventHandler(this.chkLower9_2_CheckedChanged);
            // 
            // chkLower8
            // 
            this.chkLower8.AutoSize = true;
            this.chkLower8.Location = new System.Drawing.Point(825, 287);
            this.chkLower8.Name = "chkLower8";
            this.chkLower8.Size = new System.Drawing.Size(79, 17);
            this.chkLower8.TabIndex = 87;
            this.chkLower8.Text = "LowerCase";
            this.chkLower8.UseVisualStyleBackColor = true;
            this.chkLower8.CheckedChanged += new System.EventHandler(this.chkLower8_CheckedChanged);
            // 
            // chkLower7
            // 
            this.chkLower7.AutoSize = true;
            this.chkLower7.Location = new System.Drawing.Point(825, 265);
            this.chkLower7.Name = "chkLower7";
            this.chkLower7.Size = new System.Drawing.Size(79, 17);
            this.chkLower7.TabIndex = 86;
            this.chkLower7.Text = "LowerCase";
            this.chkLower7.UseVisualStyleBackColor = true;
            this.chkLower7.CheckedChanged += new System.EventHandler(this.chkLower7_CheckedChanged);
            // 
            // chkLower6
            // 
            this.chkLower6.AutoSize = true;
            this.chkLower6.Location = new System.Drawing.Point(825, 242);
            this.chkLower6.Name = "chkLower6";
            this.chkLower6.Size = new System.Drawing.Size(79, 17);
            this.chkLower6.TabIndex = 85;
            this.chkLower6.Text = "LowerCase";
            this.chkLower6.UseVisualStyleBackColor = true;
            this.chkLower6.CheckedChanged += new System.EventHandler(this.chkLower6_CheckedChanged);
            // 
            // chkLower5
            // 
            this.chkLower5.AutoSize = true;
            this.chkLower5.Location = new System.Drawing.Point(825, 219);
            this.chkLower5.Name = "chkLower5";
            this.chkLower5.Size = new System.Drawing.Size(79, 17);
            this.chkLower5.TabIndex = 84;
            this.chkLower5.Text = "LowerCase";
            this.chkLower5.UseVisualStyleBackColor = true;
            this.chkLower5.CheckedChanged += new System.EventHandler(this.chkLower5_CheckedChanged);
            // 
            // chkLower4
            // 
            this.chkLower4.AutoSize = true;
            this.chkLower4.Location = new System.Drawing.Point(825, 197);
            this.chkLower4.Name = "chkLower4";
            this.chkLower4.Size = new System.Drawing.Size(79, 17);
            this.chkLower4.TabIndex = 83;
            this.chkLower4.Text = "LowerCase";
            this.chkLower4.UseVisualStyleBackColor = true;
            this.chkLower4.CheckedChanged += new System.EventHandler(this.chkLower4_CheckedChanged);
            // 
            // chkLower3
            // 
            this.chkLower3.AutoSize = true;
            this.chkLower3.Location = new System.Drawing.Point(825, 171);
            this.chkLower3.Name = "chkLower3";
            this.chkLower3.Size = new System.Drawing.Size(79, 17);
            this.chkLower3.TabIndex = 82;
            this.chkLower3.Text = "LowerCase";
            this.chkLower3.UseVisualStyleBackColor = true;
            this.chkLower3.CheckedChanged += new System.EventHandler(this.chkLower3_CheckedChanged);
            // 
            // chkLower2
            // 
            this.chkLower2.AutoSize = true;
            this.chkLower2.Location = new System.Drawing.Point(825, 148);
            this.chkLower2.Name = "chkLower2";
            this.chkLower2.Size = new System.Drawing.Size(79, 17);
            this.chkLower2.TabIndex = 81;
            this.chkLower2.Text = "LowerCase";
            this.chkLower2.UseVisualStyleBackColor = true;
            this.chkLower2.CheckedChanged += new System.EventHandler(this.chkLower2_CheckedChanged);
            // 
            // chkLower1
            // 
            this.chkLower1.AutoSize = true;
            this.chkLower1.Location = new System.Drawing.Point(825, 126);
            this.chkLower1.Name = "chkLower1";
            this.chkLower1.Size = new System.Drawing.Size(79, 17);
            this.chkLower1.TabIndex = 80;
            this.chkLower1.Text = "LowerCase";
            this.chkLower1.UseVisualStyleBackColor = true;
            this.chkLower1.CheckedChanged += new System.EventHandler(this.chkLower1_CheckedChanged);
            // 
            // lblLogInfo
            // 
            this.lblLogInfo.AutoSize = true;
            this.lblLogInfo.Location = new System.Drawing.Point(179, 1011);
            this.lblLogInfo.Name = "lblLogInfo";
            this.lblLogInfo.Size = new System.Drawing.Size(0, 13);
            this.lblLogInfo.TabIndex = 3;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1546, 1075);
            this.Controls.Add(this.chkLower12);
            this.Controls.Add(this.chkLower11);
            this.Controls.Add(this.chkLower10);
            this.Controls.Add(this.chkLower9_2);
            this.Controls.Add(this.chkLower8);
            this.Controls.Add(this.chkLower7);
            this.Controls.Add(this.chkLower6);
            this.Controls.Add(this.chkLower5);
            this.Controls.Add(this.chkLower4);
            this.Controls.Add(this.chkLower3);
            this.Controls.Add(this.chkLower2);
            this.Controls.Add(this.chkLower1);
            this.Controls.Add(this.chkCapital12);
            this.Controls.Add(this.chkCapital11);
            this.Controls.Add(this.chkCapital10);
            this.Controls.Add(this.chkCapital9);
            this.Controls.Add(this.chkCapital8);
            this.Controls.Add(this.chkCapital7);
            this.Controls.Add(this.chkCapital6);
            this.Controls.Add(this.chkCapital5);
            this.Controls.Add(this.chkCapital4);
            this.Controls.Add(this.chkCapital3);
            this.Controls.Add(this.chkCapital2);
            this.Controls.Add(this.chkCapital1);
            this.Controls.Add(this.chkRegex12);
            this.Controls.Add(this.chkRegex11);
            this.Controls.Add(this.chkRegex10);
            this.Controls.Add(this.chkRegex9);
            this.Controls.Add(this.chkRegex8);
            this.Controls.Add(this.chkRegex7);
            this.Controls.Add(this.chkRegex6);
            this.Controls.Add(this.chkRegex5);
            this.Controls.Add(this.chkRegex4);
            this.Controls.Add(this.chkRegex3);
            this.Controls.Add(this.chkRegex2);
            this.Controls.Add(this.chkRegex1);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtReplace12);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtRemove12);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtReplace11);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtRemove11);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtReplace10);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtRemove10);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtReplace9);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtRemove9);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtReplace8);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtRemove8);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtReplace7);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtRemove7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtReplace6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtRemove6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtReplace5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtRemove5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtReplace4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtReplace3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtReplace2);
            this.Controls.Add(this.txtReplace1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkLower9);
            this.Controls.Add(this.btnShowPreview);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRemove4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRemove3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRemove2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRemove1);
            this.Controls.Add(this.lblLogInfo);
            this.Controls.Add(this.btnRenameFiles);
            this.Controls.Add(this.lblFlexFolder);
            this.Controls.Add(this.btnSelectFolder);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button_SelectFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    FlexFolderPath = fbd.SelectedPath;
                    lblFlexFolder.Text = FlexFolderPath;

                    // store config 
                    Config.FolderPath = FlexFolderPath;
                    File.WriteAllText("config.json", JsonConvert.SerializeObject(Config));
                }
            }

            GetFiles(FlexFolderPath);
        }

        public void GetFiles(string folderPath)
        {
            var files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                lblLogInfo.Text = "No files found";
                return;
            }

            FilePaths = files.ToList();
            lblLogInfo.Text = $"Found {FilePaths.Count} files";
        }

        private void btnShowPreview_Click(object sender, EventArgs e)
        {
            // save any config created in the previous steps i.e. setting text to replace
            File.WriteAllText("config.json", JsonConvert.SerializeObject(Config));

            // may want to change this for speed i.e. don't process multiple times  if file count is greater than n
            GetFiles(FlexFolderPath);

            if (FilePaths is null || FilePaths.Count == 0 && !string.IsNullOrEmpty(FlexFolderPath))
            {
                if(FilePaths is null || FilePaths.Count == 0)
                {
                    chkLower9.Text = "No files found in the selected folder, unable to show preview";
                    return;
                }
            }

            var previewFileNames = new List<FilePreview>();
            foreach (var filePath in FilePaths)
            {
                var preview = new FilePreview();
                preview.FilePath = filePath;
                preview.CurrentFileName = Path.GetFileName(filePath);
                preview.PreviewFileName = preview.CurrentFileName;

                if (!string.IsNullOrEmpty(txtRemove1.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove1.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace1.Text) ? "" : txtReplace1.Text,
                        IsRegex = chkRegex1.Checked,
                        Capitalise = chkCapital1.Checked,
                        LowerCase = chkLower1.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove2.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove2.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace2.Text) ? "" : txtReplace2.Text,
                        IsRegex = chkRegex2.Checked,
                        Capitalise = chkCapital2.Checked,
                        LowerCase = chkLower2.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove3.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove3.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace3.Text) ? "" : txtReplace3.Text,
                        IsRegex = chkRegex3.Checked,
                        Capitalise = chkCapital3.Checked,
                        LowerCase = chkLower3.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove4.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove4.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace4.Text) ? "" : txtReplace4.Text,
                        IsRegex = chkRegex4.Checked,
                        Capitalise = chkCapital4.Checked,
                        LowerCase = chkLower4.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                //
                if (!string.IsNullOrEmpty(txtRemove5.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove5.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace5.Text) ? "" : txtReplace5.Text,
                        IsRegex = chkRegex5.Checked,
                        Capitalise = chkCapital5.Checked,
                        LowerCase = chkLower5.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove6.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove6.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace6.Text) ? "" : txtReplace6.Text,
                        IsRegex = chkRegex6.Checked,
                        Capitalise = chkCapital6.Checked,
                        LowerCase = chkLower6.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove7.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove7.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace7.Text) ? "" : txtReplace7.Text,
                        IsRegex = chkRegex7.Checked,
                        Capitalise = chkCapital7.Checked,
                        LowerCase = chkLower7.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove8.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove8.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace8.Text) ? "" : txtReplace8.Text,
                        IsRegex = chkRegex8.Checked,
                        Capitalise = chkCapital8.Checked,
                        LowerCase = chkLower8.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove9.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove9.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace9.Text) ? "" : txtReplace9.Text,
                        IsRegex = chkRegex9.Checked,
                        Capitalise = chkCapital9.Checked,
                        LowerCase = chkLower9_2.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove10.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove10.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace10.Text) ? "" : txtReplace10.Text,
                        IsRegex = chkRegex10.Checked,
                        Capitalise = chkCapital10.Checked,
                        LowerCase = chkLower10.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove11.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove11.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace11.Text) ? "" : txtReplace11.Text,
                        IsRegex = chkRegex11.Checked,
                        Capitalise = chkCapital11.Checked,
                        LowerCase = chkLower11.Checked
                    };

                    preview.Replacements.Add(replacement);
                }
                if (!string.IsNullOrEmpty(txtRemove12.Text))
                {
                    var replacement = new Replacement()
                    {
                        TextToRemove = txtRemove12.Text,
                        ReplaceWith = string.IsNullOrEmpty(txtReplace12.Text) ? "" : txtReplace12.Text,
                        IsRegex = chkRegex12.Checked,
                        Capitalise = chkCapital12.Checked,
                        LowerCase = chkLower12.Checked
                    };

                    preview.Replacements.Add(replacement);
                }

                foreach (var replacement in preview.Replacements)
                {
                    var extension = Path.GetExtension(preview.PreviewFileName);
                    var nameWithoutExtension = Path.GetFileNameWithoutExtension(preview.PreviewFileName);
                    
                    if (replacement.IsRegex)
                    {
                        var segmentToReplaceRegex = Regex.Match(preview.PreviewFileName, replacement.TextToRemove);
                        var segmentToReplace = segmentToReplaceRegex.Success ? segmentToReplaceRegex.Groups[0].Value : "";
                        if (string.IsNullOrEmpty(segmentToReplace)) continue;

                        var newSegement = segmentToReplace; 
                        if (replacement.Capitalise)
                        {
                            newSegement = newSegement.ToUpper();
                        }
                        if (replacement.LowerCase)
                        {
                            newSegement = newSegement.ToLower();
                        }

                        preview.PreviewFileName = nameWithoutExtension.Replace(segmentToReplace, newSegement) + extension;
                    }
                    else
                    {
                        preview.PreviewFileName = nameWithoutExtension.Replace(replacement.TextToRemove, replacement.ReplaceWith) + extension;
                    }                    
                }

                previewFileNames.Add(preview);
            }

            // USED BY PROCESS FILES BUTTON
            FileRenames = previewFileNames;

            var fileNameLengthCurrent = previewFileNames.Select(x=>x.CurrentFileName).Max(x => x.Length);
            var fileNameLengthPreview = previewFileNames.Select(x => x.PreviewFileName).Max(x => x.Length);
            var sb = new StringBuilder(); 
            foreach(var file in previewFileNames)
            {
                sb.AppendLine($"{file.CurrentFileName.PadRight(fileNameLengthCurrent+1)} => {file.PreviewFileName.PadLeft(fileNameLengthPreview)}");
            }
            
            chkLower9.Text = sb.ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void btnRenameFiles_Click(object sender, EventArgs e)
        {
            if(FileRenames is null || FileRenames.Count == 0)
            {
                lblLogInfo.Text = "No files. You must click 'Show Preview' with at least one file...";
                return;
            }
            lblLogInfo.Text = "Renaming files";
            
            int i = 0;
            foreach(var file in FileRenames)
            {
                var renamedFilePath = Path.Combine(Path.GetDirectoryName(file.FilePath), file.PreviewFileName);
                File.Move(file.FilePath, renamedFilePath);
                i++;
                lblLogInfo.Text = $"Renamed {i} files";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtRemove1_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove1 = txtRemove1.Text;
        }

        private void txtRemove2_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove2 = txtRemove2.Text;
        }

        private void txtRemove3_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove3 = txtRemove3.Text;
        }

        private void txtRemove4_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove4 = txtRemove4.Text;
        }

        private void txtReplace1_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace1 = txtReplace1.Text;
        }

        private void txtReplace2_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace2 = txtReplace2.Text;
        }

        private void txtReplace3_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace3 = txtReplace3.Text;
        }

        private void txtReplace4_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace4 = txtReplace4.Text;
        }

        private void txtReplace5_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace5 = txtReplace5.Text;
        }

        private void txtReplace6_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace6 = txtReplace6.Text;
        }

        private void txtReplace7_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace7 = txtReplace7.Text;
        }

        private void txtReplace8_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace8 = txtReplace8.Text;
        }

        private void txtReplace9_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace9 = txtReplace9.Text;
        }

        private void txtReplace10_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace10 = txtReplace10.Text;
        }

        private void txtReplace11_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace11 = txtReplace11.Text;
        }

        private void txtReplace12_TextChanged(object sender, EventArgs e)
        {
            Config.TextToReplace12 = txtReplace12.Text;
        }

        private void txtRemove5_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove5 = txtRemove5.Text;
        }

        private void txtRemove6_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove6 = txtRemove6.Text;
        }

        private void txtRemove7_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove7 = txtRemove7.Text;
        }

        private void txtRemove8_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove8 = txtRemove8.Text;
        }

        private void txtRemove9_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove9 = txtRemove9.Text;
        }

        private void txtRemove10_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove10 = txtRemove10.Text;
        }

        private void txtRemove11_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove11 = txtRemove11.Text;
        }

        private void txtRemove12_TextChanged(object sender, EventArgs e)
        {
            Config.TextToRemove12 = txtRemove12.Text;
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void chkRegex1_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex1 = chkRegex1.Checked;
        }

        private void chkRegex9_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex9 = chkRegex9.Checked; 
        }

        private void chkRegex12_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex12 = chkRegex12.Checked;
        }

        private void chkRegex11_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex11 = chkRegex11.Checked;
        }

        private void chkRegex10_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex10 = chkRegex10.Checked;
        }

        private void chkRegex8_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex8 = chkRegex8.Checked;
        }

        private void chkRegex7_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex7 = chkRegex7.Checked;
        }

        private void chkRegex6_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex6 = chkRegex6.Checked;
        }

        private void chkRegex5_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex5 = chkRegex5.Checked;
        }

        private void chkRegex4_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex4 = chkRegex4.Checked;
        }

        private void chkRegex3_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex3 = chkRegex3.Checked;
        }

        private void chkRegex2_CheckedChanged(object sender, EventArgs e)
        {
            Config.IsRegex2 = chkRegex2.Checked;
        }

        private void chkCapital1_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize1 = chkCapital1.Checked;
        }

        private void chkCapital2_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize2 = chkCapital2.Checked;
        }

        private void chkCapital3_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize3 = chkCapital3.Checked;
        }

        private void chkCapital4_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize4 = chkCapital4.Checked;
        }

        private void chkCapital5_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize5 = chkCapital5.Checked;
        }

        private void chkCapital6_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize6 = chkCapital6.Checked;
        }

        private void chkCapital7_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize7 = chkCapital7.Checked;
        }

        private void chkCapital8_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize8 = chkCapital8.Checked;
        }

        private void chkCapital9_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize9 = chkCapital9.Checked;
        }

        private void chkCapital10_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize10 = chkCapital10.Checked;
        }

        private void chkCapital11_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize11 = chkCapital11.Checked; 
        }

        private void chkCapital12_CheckedChanged(object sender, EventArgs e)
        {
            Config.Capitalize12 = chkCapital12.Checked;
        }

        private void chkLower1_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase1 = chkLower1.Checked;
        }

        private void chkLower2_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase2 = chkLower2.Checked;
        }

        private void chkLower3_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase3 = chkLower3.Checked;
        }

        private void chkLower4_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase4 = chkLower4.Checked;
        }

        private void chkLower5_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase5 = chkLower5.Checked;
        }

        private void chkLower6_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase6 = chkLower6.Checked;
        }

        private void chkLower7_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase7 = chkLower7.Checked;
        }

        private void chkLower8_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase8 = chkLower8.Checked;
        }

        private void chkLower9_2_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase9 = chkLower9_2.Checked;
        }

        private void chkLower10_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase10 = chkLower10.Checked;
        }

        private void chkLower11_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase11 = chkLower11.Checked;
        }

        private void chkLower12_CheckedChanged(object sender, EventArgs e)
        {
            Config.LowerCase12 = chkLower12.Checked;
        }
    } 
}
