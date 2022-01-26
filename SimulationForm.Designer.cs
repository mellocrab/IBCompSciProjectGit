
namespace IBCompSciProject
{
    partial class SimulationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_sand = new System.Windows.Forms.Button();
            this.pbox_main = new System.Windows.Forms.PictureBox();
            this.btn_water = new System.Windows.Forms.Button();
            this.btn_air = new System.Windows.Forms.Button();
            this.btn_stone = new System.Windows.Forms.Button();
            this.tbar_radius = new System.Windows.Forms.TrackBar();
            this.lbl_radius = new System.Windows.Forms.Label();
            this.btn_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_radius)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_sand
            // 
            this.btn_sand.BackColor = System.Drawing.Color.Yellow;
            this.btn_sand.ForeColor = System.Drawing.Color.SandyBrown;
            this.btn_sand.Location = new System.Drawing.Point(713, 34);
            this.btn_sand.Name = "btn_sand";
            this.btn_sand.Size = new System.Drawing.Size(75, 23);
            this.btn_sand.TabIndex = 0;
            this.btn_sand.Text = "Sand";
            this.btn_sand.UseVisualStyleBackColor = false;
            this.btn_sand.Click += new System.EventHandler(this.btn_sand_Click);
            // 
            // pbox_main
            // 
            this.pbox_main.Location = new System.Drawing.Point(1, 1);
            this.pbox_main.Name = "pbox_main";
            this.pbox_main.Size = new System.Drawing.Size(799, 452);
            this.pbox_main.TabIndex = 1;
            this.pbox_main.TabStop = false;
            this.pbox_main.Click += new System.EventHandler(this.pbox_main_Click);
            // 
            // btn_water
            // 
            this.btn_water.BackColor = System.Drawing.Color.MidnightBlue;
            this.btn_water.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.btn_water.Location = new System.Drawing.Point(713, 63);
            this.btn_water.Name = "btn_water";
            this.btn_water.Size = new System.Drawing.Size(75, 23);
            this.btn_water.TabIndex = 2;
            this.btn_water.Text = "Water";
            this.btn_water.UseVisualStyleBackColor = false;
            this.btn_water.Click += new System.EventHandler(this.btn_water_Click);
            // 
            // btn_air
            // 
            this.btn_air.BackColor = System.Drawing.Color.NavajoWhite;
            this.btn_air.ForeColor = System.Drawing.Color.Goldenrod;
            this.btn_air.Location = new System.Drawing.Point(713, 92);
            this.btn_air.Name = "btn_air";
            this.btn_air.Size = new System.Drawing.Size(75, 23);
            this.btn_air.TabIndex = 3;
            this.btn_air.Text = "Air";
            this.btn_air.UseVisualStyleBackColor = false;
            this.btn_air.Click += new System.EventHandler(this.btn_air_Click);
            // 
            // btn_stone
            // 
            this.btn_stone.BackColor = System.Drawing.Color.Gray;
            this.btn_stone.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_stone.Location = new System.Drawing.Point(713, 121);
            this.btn_stone.Name = "btn_stone";
            this.btn_stone.Size = new System.Drawing.Size(75, 23);
            this.btn_stone.TabIndex = 4;
            this.btn_stone.Text = "Stone";
            this.btn_stone.UseVisualStyleBackColor = false;
            this.btn_stone.Click += new System.EventHandler(this.btn_stone_Click);
            // 
            // tbar_radius
            // 
            this.tbar_radius.Location = new System.Drawing.Point(157, 12);
            this.tbar_radius.Maximum = 20;
            this.tbar_radius.Minimum = 5;
            this.tbar_radius.Name = "tbar_radius";
            this.tbar_radius.Size = new System.Drawing.Size(171, 45);
            this.tbar_radius.TabIndex = 5;
            this.tbar_radius.Value = 10;
            this.tbar_radius.Scroll += new System.EventHandler(this.tbar_radius_Scroll);
            // 
            // lbl_radius
            // 
            this.lbl_radius.AutoSize = true;
            this.lbl_radius.Location = new System.Drawing.Point(209, 44);
            this.lbl_radius.Name = "lbl_radius";
            this.lbl_radius.Size = new System.Drawing.Size(67, 13);
            this.lbl_radius.TabIndex = 6;
            this.lbl_radius.Text = "BrushRadius";
            this.lbl_radius.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(12, 12);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(103, 23);
            this.btn_back.TabIndex = 7;
            this.btn_back.Text = "Back To Menu";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.lbl_radius);
            this.Controls.Add(this.tbar_radius);
            this.Controls.Add(this.btn_stone);
            this.Controls.Add(this.btn_air);
            this.Controls.Add(this.btn_water);
            this.Controls.Add(this.btn_sand);
            this.Controls.Add(this.pbox_main);
            this.Name = "SimulationForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbox_main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_radius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_sand;
        private System.Windows.Forms.PictureBox pbox_main;
        private System.Windows.Forms.Button btn_water;
        private System.Windows.Forms.Button btn_air;
        private System.Windows.Forms.Button btn_stone;
        private System.Windows.Forms.TrackBar tbar_radius;
        private System.Windows.Forms.Label lbl_radius;
        private System.Windows.Forms.Button btn_back;
    }
}

