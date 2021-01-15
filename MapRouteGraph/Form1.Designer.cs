
namespace MapRouteGraph
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpenImage = new System.Windows.Forms.Button();
            this.buttonPickColor = new System.Windows.Forms.Button();
            this.buttonGenerateRooms = new System.Windows.Forms.Button();
            this.buttonExportJSON = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureWallColor = new System.Windows.Forms.PictureBox();
            this.buttonMarkRoom = new System.Windows.Forms.Button();
            this.textBoxRoomName = new System.Windows.Forms.TextBox();
            this.labelRoomName = new System.Windows.Forms.Label();
            this.buttonGenerateGraph = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWallColor)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Location = new System.Drawing.Point(12, 12);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(100, 43);
            this.buttonOpenImage.TabIndex = 0;
            this.buttonOpenImage.Text = "Open image";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonPickColor
            // 
            this.buttonPickColor.Enabled = false;
            this.buttonPickColor.Location = new System.Drawing.Point(127, 12);
            this.buttonPickColor.Name = "buttonPickColor";
            this.buttonPickColor.Size = new System.Drawing.Size(100, 43);
            this.buttonPickColor.TabIndex = 1;
            this.buttonPickColor.Text = "Pick wall color";
            this.buttonPickColor.UseVisualStyleBackColor = true;
            this.buttonPickColor.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // buttonGenerateRooms
            // 
            this.buttonGenerateRooms.Enabled = false;
            this.buttonGenerateRooms.Location = new System.Drawing.Point(324, 12);
            this.buttonGenerateRooms.Name = "buttonGenerateRooms";
            this.buttonGenerateRooms.Size = new System.Drawing.Size(120, 43);
            this.buttonGenerateRooms.TabIndex = 2;
            this.buttonGenerateRooms.Text = "Generate rooms";
            this.buttonGenerateRooms.UseVisualStyleBackColor = true;
            this.buttonGenerateRooms.Click += new System.EventHandler(this.buttonGenerateGraph_Click);
            // 
            // buttonExportJSON
            // 
            this.buttonExportJSON.Enabled = false;
            this.buttonExportJSON.Location = new System.Drawing.Point(1048, 12);
            this.buttonExportJSON.Name = "buttonExportJSON";
            this.buttonExportJSON.Size = new System.Drawing.Size(100, 43);
            this.buttonExportJSON.TabIndex = 3;
            this.buttonExportJSON.Text = "Export JSON";
            this.buttonExportJSON.UseVisualStyleBackColor = true;
            this.buttonExportJSON.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(12, 75);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1300, 547);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureWallColor
            // 
            this.pictureWallColor.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureWallColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureWallColor.Location = new System.Drawing.Point(242, 12);
            this.pictureWallColor.Name = "pictureWallColor";
            this.pictureWallColor.Size = new System.Drawing.Size(43, 43);
            this.pictureWallColor.TabIndex = 6;
            this.pictureWallColor.TabStop = false;
            // 
            // buttonMarkRoom
            // 
            this.buttonMarkRoom.Enabled = false;
            this.buttonMarkRoom.Location = new System.Drawing.Point(750, 12);
            this.buttonMarkRoom.Name = "buttonMarkRoom";
            this.buttonMarkRoom.Size = new System.Drawing.Size(100, 43);
            this.buttonMarkRoom.TabIndex = 7;
            this.buttonMarkRoom.Text = "Mark room";
            this.buttonMarkRoom.UseVisualStyleBackColor = true;
            this.buttonMarkRoom.Click += new System.EventHandler(this.buttonMarkRoom_Click);
            // 
            // textBoxRoomName
            // 
            this.textBoxRoomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxRoomName.Location = new System.Drawing.Point(619, 22);
            this.textBoxRoomName.Name = "textBoxRoomName";
            this.textBoxRoomName.Size = new System.Drawing.Size(113, 30);
            this.textBoxRoomName.TabIndex = 8;
            // 
            // labelRoomName
            // 
            this.labelRoomName.AutoSize = true;
            this.labelRoomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRoomName.Location = new System.Drawing.Point(469, 22);
            this.labelRoomName.Name = "labelRoomName";
            this.labelRoomName.Size = new System.Drawing.Size(133, 25);
            this.labelRoomName.TabIndex = 9;
            this.labelRoomName.Text = "Room name:";
            // 
            // buttonGenerateGraph
            // 
            this.buttonGenerateGraph.Enabled = false;
            this.buttonGenerateGraph.Location = new System.Drawing.Point(897, 12);
            this.buttonGenerateGraph.Name = "buttonGenerateGraph";
            this.buttonGenerateGraph.Size = new System.Drawing.Size(105, 43);
            this.buttonGenerateGraph.TabIndex = 10;
            this.buttonGenerateGraph.Text = "Generate graph";
            this.buttonGenerateGraph.UseVisualStyleBackColor = true;
            this.buttonGenerateGraph.Click += new System.EventHandler(this.buttonGenerateGraph_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(1334, 682);
            this.Controls.Add(this.buttonGenerateGraph);
            this.Controls.Add(this.labelRoomName);
            this.Controls.Add(this.textBoxRoomName);
            this.Controls.Add(this.buttonMarkRoom);
            this.Controls.Add(this.pictureWallColor);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonExportJSON);
            this.Controls.Add(this.buttonGenerateRooms);
            this.Controls.Add(this.buttonPickColor);
            this.Controls.Add(this.buttonOpenImage);
            this.Name = "Form1";
            this.Text = "Map Route Graph";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWallColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenImage;
        private System.Windows.Forms.Button buttonPickColor;
        private System.Windows.Forms.Button buttonGenerateRooms;
        private System.Windows.Forms.Button buttonExportJSON;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureWallColor;
        private System.Windows.Forms.Button buttonMarkRoom;
        private System.Windows.Forms.TextBox textBoxRoomName;
        private System.Windows.Forms.Label labelRoomName;
        private System.Windows.Forms.Button buttonGenerateGraph;
    }
}

