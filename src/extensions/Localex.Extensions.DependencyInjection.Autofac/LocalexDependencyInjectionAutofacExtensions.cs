#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Localex.Abstractions;
using Localex.Abstractions.Builders;
using Localex.Builders;
using Localex.Exceptions;

#endregion

namespace Localex.Extensions.DependencyInjection.Autofac
{
    public static class LocalexDependencyInjectionAutofacExtensions
    {
        private const string LanguageCultureParameterName = "LanguageCulture";
        private const string LocalizationPathParameterName = "Path";

        private static readonly Func<NamedParameter, bool> LanguageCultureParameterPredicate =
            parameter => parameter.Name.Equals(LanguageCultureParameterName);

        private static readonly Func<NamedParameter, bool> LocalizationPathParameterPredicate =
            parameter => parameter.Name.Equals(LocalizationPathParameterName);

        public static ContainerBuilder RegisterLocalization(this ContainerBuilder containerBuilder,
            Action<IComponentContext, ILocalizationEngineBuilder> configureLocalizationEngine)
        {
            if (configureLocalizationEngine is null)
            {
                throw new LocalexException(
                    "Localization engine configurator isn't valid (null-equal). Please specify a valid configurator function.");
            }


            containerBuilder.Register(componentContext =>
                {
                    ILocalizationEngineBuilder localizationEngineBuilder = new LocalizationEngineBuilder();

                    configureLocalizationEngine(componentContext, localizationEngineBuilder);

                    ILocalizationEngine localizationEngine = localizationEngineBuilder.Build();

                    return localizationEngine;
                })
                .As<ILocalizationEngine>().SingleInstance();

            containerBuilder.RegisterAdapter<ILocalizationEngine, ILocalization>((context, engine) =>
            {
                var namedParameters = context.GetParameters<NamedParameter>();

                if (namedParameters.FirstOrDefault(LanguageCultureParameterPredicate)
                        is NamedParameter languageCultureParameter &&
                    languageCultureParameter.Value is CultureInfo languageCulture)
                {
                    return engine.GetLocalization(languageCulture);
                }

                return null;
            });

            containerBuilder.Register((context, engine) =>
            {
                var namedParameters = context.GetParameters<NamedParameter>();

                var localization = context.ResolveOptional<ILocalization>(namedParameters);

                if (namedParameters.FirstOrDefault(LocalizationPathParameterPredicate) is
                        NamedParameter nodePathParameter &&
                    nodePathParameter.Value is string nodePath)
                {
                    return localization[nodePath];
                }

                return null;
            }).As<ILocalizationNode>();

            return containerBuilder;
        }

        public static ILocalization ResolveLocalization(this IContainer container,
            CultureInfo languageCulture)
        {
            return container.ResolveOptional<ILocalization>(new NamedParameter(LanguageCultureParameterName,
                languageCulture));
        }

        public static ILocalizationNode ResolveLocalizationNode(this IContainer container,
            CultureInfo languageCulture,
            string nodePath)
        {
            return container.Resolve<ILocalizationNode>(
                new NamedParameter(LanguageCultureParameterName, languageCulture),
                new NamedParameter(LocalizationPathParameterName, nodePath));
        }

        private static IEnumerable<TParameter> GetParameters<TParameter>(this IComponentContext componentContext)
            where TParameter : Parameter
        {
            if (componentContext is ResolveRequestContext resolveContext)
            {
                return resolveContext.Parameters.OfType<TParameter>();
            }

            return Enumerable.Empty<TParameter>();
        }
    }
}