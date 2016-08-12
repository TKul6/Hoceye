//------------------------------------------------------------------------------
// <copyright file="HoconClassifierFormat.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Hoceye.Extension
{
    /// <summary>
    /// Defines an editor format for the HoconClassifier type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "HoconClassifier")]
    [Name("HoconClassifier")]
    [UserVisible(true)] // This should be visible to the end user
    [Order(Before = Priority.Default)] // Set the priority to be after the default classifiers
    internal sealed class HoconClassifierFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HoconClassifierFormat"/> class.
        /// </summary>
        public HoconClassifierFormat()
        {
            this.DisplayName = "HoconClassifier"; // Human readable version of the name
            //this.BackgroundColor = Colors.BlueViolet;

        }
    }
}
