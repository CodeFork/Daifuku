﻿using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Daifuku.TagHelpers
{
    [HtmlTargetElement("nav-link")]
    public class NavLinkTagHelper : AnchorTagHelper
    {
        public NavLinkTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override async void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var childContent = await output.GetChildContentAsync();
            string content = childContent.GetContent();
            output.TagName = "li";
            output.TagMode = TagMode.StartTagAndEndTag;

            var hrefAttr = output.Attributes.FirstOrDefault(a => a.Name == "href");

            if (hrefAttr != null)
            {
                output.Content.SetHtmlContent($@"<a href=""{hrefAttr.Value}"">{content}</a>");
                output.Attributes.Remove(hrefAttr);
            }
            else
            {
                output.Content.SetHtmlContent(content);
            }

            if (ShouldBeActive())
            {
                MakeActive(output);
            }
        }

        bool ShouldBeActive()
        {
            string currentController = ViewContext.RouteData.Values["Controller"].ToString();
            string currentAction = ViewContext.RouteData.Values["Action"].ToString();

            if (!string.IsNullOrWhiteSpace(Controller) &&
                Controller.ToLower() != currentController.ToLower())
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(Action) &&
                Action.ToLower() != currentAction.ToLower())
            {
                return false;
            }

            foreach (KeyValuePair<string, string> routeValue in RouteValues)
            {
                if (!ViewContext.RouteData.Values.ContainsKey(routeValue.Key) ||
                    ViewContext.RouteData.Values[routeValue.Key].ToString() != routeValue.Value)
                {
                    return false;
                }
            }

            return true;
        }

        void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");

            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttr);
            }
            else if (classAttr.Value == null ||
                classAttr.Value.ToString().IndexOf("active", StringComparison.Ordinal) < 0)
            {
                output.Attributes.SetAttribute("class", classAttr.Value == null ? "active" : classAttr.Value + " active");
            }
        }
    }
}