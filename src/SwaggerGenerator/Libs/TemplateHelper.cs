using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwaggerGenerator.Libs
{
    public class TemplateHelper
    {
        public static string TemplateProcess(List<GroupDesc> groups)
        {
            //group: App-Auth
            //"/Api/Auth/Account/LoginToken"
            var builder = new StringBuilder();
            foreach (var theGroup in groups)
            {
                //api.app_auth = {};
                builder.AppendLine($"{theGroup.Group} = {{}};");
                
                foreach (var theController in theGroup.Controllers)
                {
                    //api.app_auth.account = {};
                    builder.AppendLine($"{theGroup.Group}.{theController.Controller} = {{}};");

                    foreach (var theAction in theController.Actions)
                    {
                        //// summary
                        //api.app_auth.account.login = function(params) {
                        //    return request({
                        //    url: '/user/login',
                        //        method: 'post',
                        //    }, params);
                        //};
                        var apiFunc = ApiFunctionTemplate
                            .Replace("{{summary}}", theAction.Summary)
                            .Replace("{{group}}", theGroup.Group)
                            .Replace("{{controller}}", theController.Controller.FirstLower())
                            .Replace("{{action}}", theAction.Action.FirstLower())
                            .Replace("{{apiUrl}}", theAction.ApiUrl.ToLower())
                            .Replace("{{method}}", theAction.Method.ToLower());
                        builder.AppendLine(apiFunc);
                    }
                }
            }

            var groupTemplateValue = builder.ToString();

            return ApiTemplate.Replace("{{groupTemplateValue}}", groupTemplateValue);
        }

        public static List<GroupDesc> Convert(params SwaggerApiInfo[] apiInfos)
        {
            var groups = new List<GroupDesc>();

            foreach (var apiInfo in apiInfos)
            {
                var theGroup = groups.SingleOrDefault(x => x.Group.Equals(apiInfo.Group, StringComparison.OrdinalIgnoreCase));
                if (theGroup == null)
                {
                    theGroup = new GroupDesc();
                    theGroup.Group = apiInfo.Group.ToLower().Replace("-", "_");
                    groups.Add(theGroup);
                }

                foreach (var api in apiInfo.Apis)
                {
                    var splits = api.ApiPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                    var controller = splits[splits.Count - 2];
                    var action = splits[splits.Count - 1];

                    var theController = theGroup.Controllers.SingleOrDefault(x =>
                        x.Controller.Equals(controller, StringComparison.OrdinalIgnoreCase));
                    if (theController == null)
                    {
                        theController = new ControllerDesc();
                        theController.Controller = controller;
                        theGroup.Controllers.Add(theController);
                    }

                    var theAction = theController.Actions.SingleOrDefault(x =>
                        x.Action.Equals(action, StringComparison.OrdinalIgnoreCase));
                    if (theAction == null)
                    {
                        theAction = new ActionDesc();
                        theAction.Action = action;
                        theAction.Summary = api.Summary;
                        theAction.ApiUrl = api.ApiPath;
                        theAction.Method = api.Method;
                        theController.Actions.Add(theAction);
                    }
                }
            }
            return groups;
        }
        
        public static string ApiTemplate = @"import request from '@/utils/request';

export function api() {
    var api = {};

{{groupTemplateValue}}

    return api;
  };";

        public static string ApiFunctionTemplate = @"
    //{{summary}}
    api.{{group}}.{{controller}}.{{action}} = function(params) {
        return request({
            url: '{{apiUrl}}',
            method: '{{method}}',
        }, params);
    };";
    }

    public class GroupDesc
    {
        public string Group { get; set; }
        public List<ControllerDesc> Controllers { get; set; } = new List<ControllerDesc>();
    }

    public class ControllerDesc
    {
        public string Controller { get; set; }
        public List<ActionDesc> Actions { get; set; } = new List<ActionDesc>();
    }

    public class ActionDesc
    {
        public string Action { get; set; }
        public string Summary { get; set; }
        public string ApiUrl { get; set; }
        public string Method { get; set; }
    }
}
