using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEasy
{
    public class TemplateHolders
    {
        #region form

        public static string FormName = "<FormName>";
        public static string ModelName = "<ModelName>";
        public static string FormGroupTemplatesHolder = "<FormGroupTemplatesHolder>";
        public static string FormClass = "<FormClass>";
        public static string FormTemplate = @"
<form<FormClass> role=""form"" name=""<FormName>"">
<FormGroupTemplatesHolder>
</form>
";
        #endregion

        #region form-group

        public static string PropertyName = "<PropertyName>";
        public static string PropertyText = "<PropertyText>";
        public static string PropertyMinLength = "<PropertyMinLength>";
        public static string PropertyMaxLength = "<PropertyMaxLength>";
        public static string PropertyRequired = "<PropertyRequired>";
        public static string TipError = "<TipError>";
        public static string TipRequired = "<TipRequired>";

        public static string FormGroupTemplate_Input = @"
    <div class=""form-group"" ng-class=""{'has-success':<FormName>.<PropertyName>.$valid,'has-error':<FormName>.<PropertyName>.$dirty && <FormName>.<PropertyName>.$invalid}"">
        <label class=""col-xs-2 control-label"" ng-class=""""><PropertyText>：</label>
        <div class=""col-xs-9"">
            <input type=""text"" class=""form-control"" name=""<PropertyName>"" ng-model=""<ModelName>.<PropertyName>"" <PropertyMinLength> <PropertyMaxLength> <PropertyRequired>/>
            <div class=""help-block"" ng-if=""<FormName>.<PropertyName>.$dirty && <FormName>.<PropertyName>.$invalid""><TipError></div>
        </div>
        <div class=""help-block inline""><TipRequired></div>
    </div>
    ";
        public static string FormGroupTemplate_Select = @"
    TODO
";
        public static string FormGroupTemplate_TextArea = @"
    TODO
";
        public static Dictionary<FormGroupTemplateType, string> FormGroupTemplates = new Dictionary<FormGroupTemplateType, string>();

        #endregion

        static TemplateHolders()
        {
            FormGroupTemplates[FormGroupTemplateType.Input] = FormGroupTemplate_Input;
            FormGroupTemplates[FormGroupTemplateType.Select] = FormGroupTemplate_Select;
            FormGroupTemplates[FormGroupTemplateType.TextArea] = FormGroupTemplate_TextArea;
        }
    }

    public enum FormGroupTemplateType
    {
        Input = 0,
        Select = 1,
        TextArea = 2
    }

    public class AngularFormHelper
    {
        public static string CreateAngularTemplate(AngularFormMeta meta)
        {
            if (meta == null)
            {
                throw new ArgumentNullException("meta");
            }

            return null;
        }

    }

    public class AngularFormMeta
    {
        public AngularFormMeta()
        {
            RequiredText = "*";
            Properties = new List<AngularFormProperty>();
            FormClass = "form-horizontal";
        }

        public string FormName { get; set; }
        public string FormClass { get; set; }
        public string ModelName { get; set; }
        public string RequiredText { get; set; }

        public IList<AngularFormProperty> Properties { get; set; }


        public string GetFormClassTemplate()
        {
            if (string.IsNullOrWhiteSpace(FormClass))
            {
                return string.Empty;
            }
            return string.Format(" class=\"{0}\"", FormClass);
        }


        public string ToTemplateString()
        {
            var template = TemplateHolders.FormTemplate;
            var result = template
                .Replace(TemplateHolders.FormName, this.FormName)
                .Replace(TemplateHolders.FormClass, this.GetFormClassTemplate());

            var formGroupTemplatesHolderValue = "";
            foreach (var angularFormProperty in this.Properties)
            {
                formGroupTemplatesHolderValue += angularFormProperty.ToTemplateString();
            }

            result = result.Replace(TemplateHolders.FormGroupTemplatesHolder, formGroupTemplatesHolderValue);
            result = result.Replace(TemplateHolders.TipRequired, this.RequiredText);
            result = result.Replace(TemplateHolders.TipError, this.RequiredText);

            return result;
        }
    }

    public class AngularFormProperty
    {
        public AngularFormProperty()
        {
            TemplateType = FormGroupTemplateType.Input;
        }
        public string PropertyName { get; set; }
        public string PropertyText { get; set; }
        public int? PropertyMinLength { get; set; }
        public int? PropertyMaxLength { get; set; }
        public bool? PropertyRequired { get; set; }
        public string TipError { get; set; }
        public FormGroupTemplateType TemplateType { get; set; }
        
        public string ToTemplateString()
        {
            var template = TemplateHolders.FormGroupTemplates[this.TemplateType];

            var result = template
                .Replace(TemplateHolders.PropertyName, this.PropertyName)
                .Replace(TemplateHolders.PropertyText, this.PropertyText)
                .Replace(TemplateHolders.PropertyMinLength, this.GetMinLengthTemplate())
                .Replace(TemplateHolders.PropertyMaxLength, this.GetMaxLengthTemplate())
                .Replace(TemplateHolders.PropertyRequired, this.GetRequiredTemplate());

            return result;
        }

        public string GetMinLengthTemplate()
        {
            if (!PropertyMinLength.HasValue)
            {
                return string.Empty;
            }
            return string.Format("ng-minlength=\"{0}\"", PropertyMinLength.Value);
        }

        public string GetMaxLengthTemplate()
        {
            if (!PropertyMinLength.HasValue)
            {
                return string.Empty;
            }
            return string.Format("ng-maxlength=\"{0}\"", PropertyMinLength.Value);
        }

        public string GetRequiredTemplate()
        {
            var required = false;
            if (PropertyRequired.HasValue)
            {
                required = PropertyRequired.Value;
            }
            return required ? "required" : "";
        }

        public string GetTipErrorTemplate()
        {
            if (PropertyMinLength.HasValue && PropertyMaxLength.HasValue)
            {
                return string.Format("请输入{0}-{1}个字符", PropertyMinLength.Value, PropertyMaxLength.Value);
            }

            if (PropertyMinLength.HasValue)
            {
                return string.Format("请输入至少{0}个字符", PropertyMinLength.Value);
            }

            if (PropertyMaxLength.HasValue)
            {
                return string.Format("请输入至多{0}个字符", PropertyMaxLength.Value);
            }
            return string.Empty;
        }
    }
}
