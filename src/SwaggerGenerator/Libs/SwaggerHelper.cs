using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SwaggerGenerator.Libs
{
    public class SwaggerApiDoc
    {
        public static SwaggerApiInfo ParseJson(string json, string groupName)
        {
            var docMeta = (JObject)JsonConvert.DeserializeObject(json);

            var info = docMeta["info"];
            var title = info["title"]?.ToString();
            var description = info["description"]?.ToString();
            var version = info["version"]?.ToString();
            var swaggerApiInfo = SwaggerApiInfo.Create(groupName, title, description, version);
            
            var paths = docMeta["paths"];
            foreach (var token in paths.AsEnumerable())
            {
                if (token is JProperty prop)
                {
                    //post or get
                    var apiPath = prop.Name;
                    foreach (var apiItem in prop.Value)
                    {
                        if (apiItem is JProperty apiProp)
                        {
                            var apiMethod = apiProp.Name;
                            var summary = apiProp.Value["summary"]?.ToString();
                            swaggerApiInfo.Apis.Add(SwaggerApi.Create(apiPath, apiMethod, summary));
                        }
                    }
                }
            }

            return swaggerApiInfo;
        }
    }

    public class SwaggerApiInfo
    {
        public string Group { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }

        public List<SwaggerApi> Apis { get; set; } = new List<SwaggerApi>();

        public static SwaggerApiInfo Create(string group, string title, string desc, string version)
        {
            return new SwaggerApiInfo {Group = group,Title = title, Description = desc, Version = version };
        }
    }

    public class SwaggerApi
    {
        public string ApiPath { get; set; }
        public string Method { get; set; }
        public string Summary { get; set; }
        
        public static SwaggerApi Create(string apiPath, string method, string summary)
        {
            return new SwaggerApi {ApiPath = apiPath, Method = method, Summary = summary};
        }
    }
}
