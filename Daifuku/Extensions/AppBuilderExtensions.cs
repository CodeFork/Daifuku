﻿using Daifuku.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;

namespace Daifuku.Extensions
{
    /// <summary>
    /// App builder extensions.
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Redirect domains.
        /// </summary>
        /// <returns>Pipeline.</returns>
        /// <param name="app">App.</param>
        /// <param name="domainRedirects">Domain redirects.</param>
        public static IApplicationBuilder RedirectDomains(this IApplicationBuilder app, Dictionary<string, string> domainRedirects)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<RedirectDomains>(domainRedirects);
        }

        /// <summary>
        /// Uses the server header or remove.
        /// </summary>
        /// <returns>Pipeline.</returns>
        /// <param name="app">App.</param>
        /// <param name="header">Server header. If set to <c>null</c> just remove server header completely.</param>
        public static IApplicationBuilder UseServerHeader(this IApplicationBuilder app, string header = null)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<ServerHeader>(header);
        }

        public static IApplicationBuilder UseNoMimeSniff(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<NoSniff>();
        }

        public static IApplicationBuilder UseReferrerPolicy(this IApplicationBuilder app, ReferrerPolicy policy)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<ReferrerPolicy>(policy);
        }

        public static IApplicationBuilder UsePoweredBy(this IApplicationBuilder app, string header = null)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<XPoweredBy>(header);
        }

        public static IApplicationBuilder UseDaifuku(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseServerHeader(null)
                .UseNoMimeSniff()
                .UseReferrerPolicy(ReferrerPolicy.NoReferrer)
                .UsePoweredBy(null);
        }
    }
}