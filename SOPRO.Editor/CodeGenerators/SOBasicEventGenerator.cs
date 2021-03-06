﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     Questo codice è stato generato da uno strumento.
//     Versione runtime: 15.0.0.0
//  
//     Le modifiche a questo file possono causare un comportamento non corretto e verranno perse se
//     il codice viene rigenerato.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace SOPRO.Editor.CodeGenerators
{
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class SOBasicEventGenerator : SOBasicEventGeneratorBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System.Collections.Generic;\r\nusing UnityEngine;\r\nusing System;\r\nusing SOPRO" +
                    ";\r\n");
            
            #line 7 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
 if(Namespace != null && Namespace.Length > 0)
{
            
            #line default
            #line hidden
            this.Write("namespace ");
            
            #line 9 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write(" \r\n{\r\n");
            
            #line 11 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"

}

            
            #line default
            #line hidden
            this.Write("    /// <summary>\r\n    /// Basic Scriptable Object event\r\n    /// </summary>\r\n   " +
                    " [CreateAssetMenu(fileName = ");
            
            #line 17 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(AssetFileName));
            
            #line default
            #line hidden
            this.Write(", menuName = ");
            
            #line 17 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(AssetMenuName));
            
            #line default
            #line hidden
            this.Write(")]\r\n    [Serializable]\r\n    public class ");
            
            #line 19 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" : ");
            
            #line 19 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(BaseClassName));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n#if UNITY_EDITOR\r\n        /// <summary>\r\n        /// Description of the " +
                    "event, available only in UNITY_EDITOR\r\n        /// </summary>\r\n        [Multilin" +
                    "e]\r\n\t\t[SerializeField]\r\n        private string DEBUG_DeveloperDescription = \"\";\r" +
                    "\n#endif\r\n\r\n\t\t");
            
            #line 30 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
 if(ValidTypes.Length > 0)
		{ 
            
            #line default
            #line hidden
            this.Write("#if UNITY_EDITOR\r\n\t\t ");
            
            #line 33 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
 for(int i = 0 ; i < ValidTypes.Length ; i++)
			{ 
            
            #line default
            #line hidden
            this.Write("\t\t\t    /// <summary>\r\n\t\t\t\t/// Debug field for inspector view, available only in U" +
                    "NITY_EDITOR\r\n\t\t\t\t/// </summary>\r\n\t\t\t\tpublic ");
            
            #line 38 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ValidTypes[i]));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 38 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture("DEBUG_" + ValidTypes[i] + "_" + i));
            
            #line default
            #line hidden
            this.Write(" = default(");
            
            #line 38 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ValidTypes[i]));
            
            #line default
            #line hidden
            this.Write(");\r\n\t\t");
            
            #line 39 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
 } 
            
            #line default
            #line hidden
            this.Write("#endif\r\n\t\t");
            
            #line 41 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
 } 
		
            
            #line default
            #line hidden
            this.Write("        public delegate void ");
            
            #line 43 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName + "Del"));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 43 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenericArgumentsWithTypes));
            
            #line default
            #line hidden
            this.Write(");\r\n        public event ");
            
            #line 44 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName + "Del"));
            
            #line default
            #line hidden
            this.Write(" Event;\r\n\r\n        /// <summary>\r\n        /// Invokes all listeners of this event" +
                    "\r\n        /// </summary>\r\n        public override void Raise(");
            
            #line 49 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenericArgumentsWithTypes));
            
            #line default
            #line hidden
            this.Write(")\r\n        {\r\n\t\t\tif(Event != null)\r\n\t\t\t\tEvent.Invoke(");
            
            #line 52 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GenericArguments));
            
            #line default
            #line hidden
            this.Write(");\r\n        }\r\n    }\r\n");
            
            #line 55 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"
 if(Namespace != null && Namespace.Length > 0)
{
            
            #line default
            #line hidden
            this.Write("}\r\n");
            
            #line 58 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"

}

            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        #line 61 "D:\GitProjects\Self\SOPRO\SOPRO.Editor\CodeGenerators\SOBasicEventGenerator.tt"

public string Namespace { get; set; }
public string ClassName { get; set; }
public string BaseClassName { get; set; }
public string AssetFileName { get; set; }
public string AssetMenuName { get; set; }
public string GenericArgumentsWithTypes { get; set; }
public string GenericArguments { get; set; }
public string[] ValidTypes { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class SOBasicEventGeneratorBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
