global using System.Buffers;
global using System.Collections.Concurrent;
global using System.Diagnostics;
global using System.IO.Compression;
global using System.Runtime.Serialization;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;
global using Python.Runtime;
global using Serilog;
global using Spectre.Console;
global using Zilean.DmmScraper.Features.Dmm;
global using Zilean.DmmScraper.Features.LzString;
global using Zilean.Shared.Extensions;
global using Zilean.Shared.Features.Configuration;
global using Zilean.Shared.Features.Dmm;
global using Zilean.Shared.Features.ElasticSearch;
global using Zilean.Shared.Features.Otlp;
