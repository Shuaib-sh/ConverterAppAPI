using App.Application.Interfaces;
using NHapi.Base;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class DataFormatService : IDataFormatService
    {
        public async Task<object> ParseHL7(string input)
        {
            try
            {
                input = input.Replace("\r\n", "\n")
                             .Replace("\r", "\n")
                             .Replace("\n", "\r\n");
                var pipeParser = new PipeParser();

                // Parse HL7 message
                IMessage message = pipeParser.Parse(input);

                // Convert to structured dictionary
                var result = ConvertMessageToDictionary(message);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid HL7 format. Parsing failed.", ex);
            }
        }

        private Dictionary<string, object> ConvertMessageToDictionary(IMessage message)
        {
            var result = new Dictionary<string, object>();

            ProcessStructure(message, result);

            return result;
        }

        private void ProcessStructure(IStructure structure, Dictionary<string, object> result)
        {
            if (structure is AbstractSegment segment)
            {
                var segmentName = segment.GetStructureName();
                var segmentDict = new Dictionary<string, object>();

                for (int i = 1; i <= segment.NumFields(); i++)
                {
                    try
                    {
                        var repetitions = segment.GetField(i);

                        if (repetitions.Length > 0)
                        {
                            for (int r = 0; r < repetitions.Length; r++)
                            {
                                var extracted = ExtractValue(repetitions[r]);

                                if (extracted != null)
                                {
                                    var key = repetitions.Length > 1
                                        ? $"{segmentName}.{i}.{r + 1}"
                                        : $"{segmentName}.{i}";

                                    segmentDict[key] = extracted;
                                }
                            }
                        }
                    }
                    catch
                    {
                        // ignore invalid field
                    }
                }

                if (segmentDict.Count > 0)
                {
                    if (!result.ContainsKey(segmentName))
                        result[segmentName] = new List<Dictionary<string, object>>();

                    ((List<Dictionary<string, object>>)result[segmentName]).Add(segmentDict);
                }
            }
            else if (structure is AbstractGroup group)
            {
                foreach (var name in group.Names)
                {
                    var structures = group.GetAll(name);

                    foreach (var childStructure in structures)
                    {
                        ProcessStructure(childStructure, result);
                    }
                }
            }
        }

        private object ExtractValue(IType type)
        {
            if (type == null)
                return null;

            if (type.GetType().Name == "Varies")
            {
                dynamic varies = type;
                return ExtractValue(varies.Data);
            }

            if (type is IPrimitive primitive)
                return primitive.Value;

            if (type is IComposite composite)
            {
                var componentDict = new Dictionary<string, object>();

                for (int i = 0; i < composite.Components.Length; i++)
                {
                    var component = composite.Components[i];

                    var extracted = ExtractValue(component);

                    if (extracted != null && !string.IsNullOrWhiteSpace(extracted.ToString()))
                    {
                        componentDict[$"{i + 1}"] = extracted;
                    }
                }

                return componentDict.Count > 0 ? componentDict : null;
            }

            return type.ToString();
        }

        public async Task<object> FormatJsonAsync(string input, string mode)
        {
            var parsedJson = JsonDocument.Parse(input);

            return mode?.ToLower() switch
            {
                "minify" => parsedJson.RootElement,

                "pretty" => parsedJson.RootElement,

                "validate" => new { message = "JSON is valid" },

                _ => throw new Exception("Invalid mode. Use pretty | minify | validate")
            };
        }
    }
}