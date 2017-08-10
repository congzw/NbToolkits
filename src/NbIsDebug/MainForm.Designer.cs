using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NbIsDebug
{
    partial class MainForm
    {

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.lv = new ListView();
            this.columnHeader1 = ((ColumnHeader)(new ColumnHeader()));
            this.columnHeader3 = ((ColumnHeader)(new ColumnHeader()));
            this.columnHeader2 = ((ColumnHeader)(new ColumnHeader()));
            this.mainMenu1 = new MainMenu(this.components);
            this.menuItem3 = new MenuItem();
            this.menuItem4 = new MenuItem();
            this.menuItem7 = new MenuItem();
            this.menuItem8 = new MenuItem();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.AllowDrop = true;
            this.lv.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            this.lv.Columns.AddRange(new ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2});
            this.lv.Location = new Point(8, 8);
            this.lv.Name = "lv";
            this.lv.Size = new Size(360, 107);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = View.Details;
            this.lv.DragDrop += new DragEventHandler(this.lv_DragDrop);
            this.lv.DragOver += new DragEventHandler(this.lv_DragOver);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Assembly";
            this.columnHeader1.Width = 133;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Version";
            this.columnHeader3.Width = 115;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Build Type";
            this.columnHeader2.Width = 108;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new MenuItem[] {
            this.menuItem3,
            this.menuItem7});
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.MenuItems.AddRange(new MenuItem[] {
            this.menuItem4});
            this.menuItem3.Text = "&File";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "E&xit";
            this.menuItem4.Click += new EventHandler(this.MenuHandler);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.MenuItems.AddRange(new MenuItem[] {
            this.menuItem8});
            this.menuItem7.Text = "&View";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 0;
            this.menuItem8.Text = "&Clear";
            this.menuItem8.Click += new EventHandler(this.MenuHandler);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new Size(5, 14);
            this.ClientSize = new Size(376, 125);
            this.Controls.Add(this.lv);
            this.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "IsDebug";
            this.Load += new EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        public ListView lv;
        private MainMenu mainMenu1;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        private MenuItem menuItem7;
        private MenuItem menuItem8;

        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private IContainer components;
    }
}

