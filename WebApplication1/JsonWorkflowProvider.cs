using Elsa.Models;
using Elsa.Serialization;
using Elsa.Serialization.Formatters;
using Elsa.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class JsonWorkflowProvider : IWorkflowProvider
    {
        private readonly IWorkflowSerializer _serializer;
        private readonly IConfiguration _configuration;

        public JsonWorkflowProvider(IWorkflowSerializer serializer, IConfiguration configuration)
        {
            _serializer = serializer;
            _configuration = configuration;
        }

        public async Task<IEnumerable<WorkflowDefinitionVersion>> GetWorkflowDefinitionsAsync(CancellationToken cancellationToken)
        {
            var workflowsDirectoryPath = @"C:\Users\chkunt\source\repos\WebApplication1\WebApplication1";
            var workflowFiles = Directory.GetFiles(workflowsDirectoryPath, "*.json");
            var workflowDefinitionTasks = workflowFiles.Select(this.LoadWorkflowDefinition).ToList();
            await Task.WhenAll(workflowDefinitionTasks);
            return workflowDefinitionTasks.Select(x => x.Result);
        }

        private async Task<WorkflowDefinitionVersion> LoadWorkflowDefinition(string path)
        {
            var jsonText = await File.ReadAllTextAsync(path);
            return this._serializer.Deserialize<WorkflowDefinitionVersion>(jsonText, JsonTokenFormatter.FormatName);
        }
    }
}
