﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RulesService.Domain.Services.Rules.Validation.Invariants {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class InvariantResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal InvariantResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RulesService.Domain.Services.Rules.Validation.Invariants.InvariantResources", typeof(InvariantResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified rule name is null or empty. Must input a valid rule name..
        /// </summary>
        internal static string R001 {
            get {
                return ResourceManager.GetString("R001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified rule dates interval is invalid. (DateBegin = {0} | DateEnd = {1}).
        /// </summary>
        internal static string R002 {
            get {
                return ResourceManager.GetString("R002", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified rule priority is invalid. (Priority = {0}).
        /// </summary>
        internal static string R003 {
            get {
                return ResourceManager.GetString("R003", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified content type does not exist. (TenantId = {0} | ContentTypeCode = {1}).
        /// </summary>
        internal static string R004 {
            get {
                return ResourceManager.GetString("R004", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified invalid logical operator code for condition node. (LogicalOperator = {0}).
        /// </summary>
        internal static string R005 {
            get {
                return ResourceManager.GetString("R005", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified empty collection of child nodes for condition node. Must have one child node at least..
        /// </summary>
        internal static string R006 {
            get {
                return ResourceManager.GetString("R006", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified invalid condition type for condition node. (TenantId = {0} | ConditionTypeCode = {1}).
        /// </summary>
        internal static string R007 {
            get {
                return ResourceManager.GetString("R007", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified invalid data type for condition node. (DataTypeCode = {0}).
        /// </summary>
        internal static string R008 {
            get {
                return ResourceManager.GetString("R008", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified invalid operator for condition node. (OperatorCode = {0}).
        /// </summary>
        internal static string R009 {
            get {
                return ResourceManager.GetString("R009", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified invalid right hand operand value for specified data type. (DataType = {0} | Value = {1}).
        /// </summary>
        internal static string R010 {
            get {
                return ResourceManager.GetString("R010", resourceCulture);
            }
        }
    }
}