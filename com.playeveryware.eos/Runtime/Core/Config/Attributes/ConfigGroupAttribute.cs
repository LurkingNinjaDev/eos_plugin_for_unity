/*
 * Copyright (c) 2024 PlayEveryWare
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace PlayEveryWare.EpicOnlineServices
{
    using System;

    /// <summary>
    /// This attribute is used to decorate a config class that represents a
    /// collection of configuration fields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigGroupAttribute : Attribute
    {
        /// <summary>
        /// The label for the collection of config fields that the config class
        /// represents.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Indicates whether the group of configuration values can be
        /// collapsed.
        /// </summary>
        public bool Collapsible { get; }

        /// <summary>
        /// Set the 
        /// </summary>
        public string[] GroupLabels { get; }

        public ConfigGroupAttribute(string label, bool collapsible = true)
        {
            Label = label;
            Collapsible = collapsible;
        }

        public ConfigGroupAttribute(string label, string[] groupLabels, bool collapsible = true)
        : this(label, collapsible)
        {
            GroupLabels = groupLabels;
        }
    }
}