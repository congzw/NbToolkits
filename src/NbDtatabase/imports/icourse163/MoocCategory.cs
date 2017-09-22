using System.Collections.Generic;
using System.Linq;
using System.Text;
using NbDtatabase.Common;

namespace NbDtatabase.imports.icourse163
{
    public class icourse163Category
    {
        //"id": 1001043131,
        //"name": "计算机",
        //"type": 4,
        //"parentId": -1,
        //"linkName": "computer",
        //"webIcon": "u-icon-computer",
        //"mobIcon": "http://nos.netease.com/edu-image/9EF3C16225145BB17BF1A365E8F35861.png?imageView0026thumbnail003d52y520026quality003d100",
        //"weight": 130,
        //"checked": true,
        public icourse163Category()
        {
            children = new List<icourse163Category>();
        }
        public string id { get; set; }
        public string parentId { get; set; }
        public string name { get; set; }
        public IList<icourse163Category> children { get; set; }
    }

    public class MoocCategoryImport
    {
        public MoocCategoryImport()
        {
            Children = new List<MoocCategoryImport>();
        }

        public string Name { get; set; }
        public IList<MoocCategoryImport> Children { get; set; }

        public static void ProcessJson()
        {
            var exeDirPath = MyPathHelper.MakeExeDirPath();
            var jsonFilePath = MyPathHelper.JoinPath(exeDirPath, @"imports\icourse163\moocCategory.json");


            var moocCategoryOutputPath = MyPathHelper.JoinPath(exeDirPath, @"imports\icourse163\moocCategoryOutput.json");

            var icourse163Categories = MyJsonHelper.DeserializeFile<IList<icourse163Category>>(jsonFilePath).OrderBy(x => x.id).ToList();
            //MyJsonHelper.SerializeFile(jsonFilePath, icourse163Categories);

            var moocCategoryImports = new List<MoocCategoryImport>();
            foreach (var icourse163Category in icourse163Categories)
            {
                var moocCategoryImport = ProcessItem(icourse163Category);
                moocCategoryImports.Add(moocCategoryImport);
            }

            MyJsonHelper.SerializeFile(moocCategoryOutputPath, moocCategoryImports, Encoding.Unicode);
        }

        private static MoocCategoryImport ProcessItem(icourse163Category item)
        {
            var moocCategoryImport = new MoocCategoryImport() { Name = item.name };
            if (item.children == null)
            {
                return moocCategoryImport;
            }

            foreach (var child in item.children.OrderBy(x => x.id))
            {
                var childImport = ProcessItem(child);
                moocCategoryImport.Children.Add(childImport);
            }
            return moocCategoryImport;
        }
    }
}
