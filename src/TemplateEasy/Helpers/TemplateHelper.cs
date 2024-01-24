using Newtonsoft.Json;

namespace TemplateEasy
{
    public class TemplateHelper
    {
        public static string ReplaceWithDoubleQuotes(string values)
        {
            //var demoResult = @"
            //<div class=""form-group"" ng-class=""{'has-success':courseFrom.Title.$valid,'has-error':courseFrom.Title.$dirty && courseFrom.Title.$invalid}"">
            //    <label class=""col-xs-2 control-label"" ng-class="""">标题：</label>
            //    <div class=""col-xs-9"">
            //        <input type=""text"" class=""form-control"" name=""Title"" ng-model=""uploadModel.Title"" ng-minlength=""1"" ng-maxlength=""100"" required/>
            //        <div class=""help-block"" ng-if=""courseFrom.Title.$dirty && courseFrom.Title.$invalid"">请输入1-100位字符。</div>
            //    </div>
            //    <div class=""help-block inline"">必填</div>
            //</div>";
            //var result = Replace(values, "\"", "\"\"");
            var result = Replace(values, "\"", "\"\"");
            return result;
        }

        public static string Replace(string values, string oldValue, string newValue)
        {
            if (string.IsNullOrWhiteSpace(values))
            {
                return string.Empty;
            }
            return values.Replace(oldValue, newValue);
        }

        public static string FormatJson(string json, int mode)
        {
            if (mode == 0)
            {
                return json;
            }
            if (mode == 1)
            {
                return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.None);
            }
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented);
        }
    }
}
