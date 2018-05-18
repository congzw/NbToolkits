using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NhibernateMapEasy
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var stringBuilder = new StringBuilder();

            var entityContent = this.textBox1.Lines.ToList();
            foreach (var line in entityContent)
            {
                var tryProcessLine = TryProcessLine(line);
                if (!string.IsNullOrWhiteSpace(tryProcessLine))
                {
                    stringBuilder.AppendLine(tryProcessLine);
                }
            }

            this.textBox2.Text = stringBuilder.ToString();
        }


        private string TryProcessLine(string line)
        {
            #region demos

            //public class ReadOnlyUser : NbEntity<ReadOnlyUser>, ISharedUser
            //{
            //    public virtual string LoginName { get; set; }
            //    public virtual string FullName { get; set; }
            //    public virtual string NickName { get; set; }
            //    public virtual DateTime CreateDate { get; set; }
            //    public virtual Guid OrgId { get; set; }
            //    public virtual string UserTypeCode { get; set; }
            //}

            //Table("Lib_Ds_Org");
            //Id(x => x.Id).GeneratedBy.GuidComb();

            //Map(x => x.ParentId);
            //Map(x => x.OrgTypeCode);

            //Map(x => x.string LoginName);
            //Map(x => x.string FullName);
            //Map(x => x.string NickName);
            //Map(x => x.DateTime CreateDate);
            //Map(x => x.Guid OrgId);
            //Map(x => x.string UserTypeCode);


            #endregion

            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }

            if (line.NbContains("public virtual ") && line.NbContains("{ get; set; }"))
            {
                var propertyName = line.Replace("public virtual ", "").Replace("{ get; set; }", "").Trim();
                //string LoginName
                propertyName = propertyName.Split().LastOrDefault();
                return string.Format("Map(x => x.{0});", propertyName);
            }
            return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
