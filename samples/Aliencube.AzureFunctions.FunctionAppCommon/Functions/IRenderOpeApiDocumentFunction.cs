﻿using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;

using Microsoft.Extensions.Logging;

namespace Aliencube.AzureFunctions.FunctionAppCommon.Functions
{
    /// <summary>
    /// This provides interfaces to the <see cref="RenderOpeApiDocumentFunction"/> class.
    /// </summary>
    public interface IRenderOpeApiDocumentFunction : IFunction<ILogger>
    {
    }
}